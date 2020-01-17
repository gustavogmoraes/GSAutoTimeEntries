using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using IWshRuntimeLibrary;
using GSAutoTimeEntriesWebApi.Objetos;
using GSAutoTimeEntries.UI;
using GSAutoTimeEntries.Utils;

namespace GSAutoTimeEntries.Servicos
{
    public class ServicoDeImportacao : IDisposable
    {
        public List<Ponto> ImporteRegistroDePonto(string[] linhas)
        {
            var listaDeLinhas = linhas.ToList();
            // Removendo a primeira linha, pois são somente colunas
            listaDeLinhas.RemoveAt(0);

            var DIA_DA_SEMANA = 53;
            var DATA = 57;
            var BATIDAS = 58;

            var listaDePonto = new List<Ponto>();
            for (int i = 0; i < listaDeLinhas.Count; i++)
            {
                if (!listaDeLinhas[i].Contains("BANCO DE HORAS")) continue;

                var splitted = listaDeLinhas[i].Split(',');

                if (splitted.Length >= 64)
                {
                    DIA_DA_SEMANA = 60;
                    DATA = 64;
                    BATIDAS = 65;
                }

                var diaDaSemana = splitted[DIA_DA_SEMANA];
                var data = splitted[DATA];

                //if (string.IsNullOrEmpty(diaDaSemana))
                //    continue;

                string batidas;
                if (i == listaDeLinhas.Count - 1)
                    batidas = splitted[BATIDAS];
                else
                {
                    batidas = !string.IsNullOrEmpty(diaDaSemana)
                        ? splitted[BATIDAS]
                        : listaDeLinhas[i + 1].Split(',')[BATIDAS];
                }

                // Pulamos uma linha, no caso de o relatório de 1 dia ocupar 2 linhas
                //if (string.IsNullOrEmpty(batidas) && string.IsNullOrEmpty(diaDaSemana)) i++;

                var correcao = string.Empty;
                if (!string.IsNullOrEmpty(batidas))
                    listaDePonto.Add(new Ponto
                    {
                        Data = data,
                        HorariosDasBatidas = batidas.Trim()
                            .Split(new[] { "  " }, StringSplitOptions.None)
                            .Select(x =>
                            {
                                if (x.Length <= 5) return ObtenhaBatida(x);

                                if (x.Last() == ':')
                                    return ObtenhaBatida(x.Remove(x.Length - 1, 1));

                                var valores = x.Split(' ');
                                correcao = valores[1];
                                return ObtenhaBatida(valores[0].Remove(5, 1));
                            })
                            .ToList()
                    });

                if (!string.IsNullOrEmpty(correcao))
                {
                    listaDePonto.Last().HorariosDasBatidas.Add(ObtenhaBatida(correcao));
                    correcao = string.Empty;
                }
            }

            // Remove os dias sem data, corrigindo duplicatas
            listaDePonto.RemoveAll(x => string.IsNullOrEmpty(x.Data));

            return listaDePonto;
        }

        private TimeSpan ObtenhaBatida(string batida)
        {
            var splitted = batida.Split(':');

            return new TimeSpan(Convert.ToInt32(splitted[0]), Convert.ToInt32(splitted[1]), 0);
        }

        public List<Lancamento> ObtenhaLancamentosPorImportacao(string[] linhas, DateTime dataInicio, DateTime dataFim, bool exibirProgresso)
        {
            if (exibirProgresso)
                GerenciadorDeProgresso.AtualizeProgressBar(80, "Processando registros");
            Thread.Sleep(1000);

            var listaDeLancamentos = new List<Lancamento>();
            var listaDePontos = ImporteRegistroDePonto(linhas);

            return listaDePontos.Select(x => new Lancamento
            {
                Data = x.Data.ParaDateTime(),
                Batidas = x.HorariosDasBatidas.OrderBy(y => y).ToList()
            }).ToList();
        }

        private string ObtenhaDataDoLancamento(string dataImportada)
        {
            var splitted = dataImportada.Split('/');

            return splitted[2] + "-" + splitted[1] + "-" + splitted[0];
        }

        public void Dispose()
        {

        }
    }
}
