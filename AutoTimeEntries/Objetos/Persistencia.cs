using System;
using System.Collections.Generic;
using System.IO;
using GSAutoTimeEntries.Servicos;
using GSAutoTimeEntries.Utils;
using Newtonsoft.Json;

namespace GSAutoTimeEntriesWebApi.Objetos
{
    public static class Persistencia
    {
        static Persistencia()
        {
            if (_dados == null)
            {
                BusqueDados();
            }

            AppDomain.CurrentDomain.ProcessExit += StaticClass_Dtor;
        }

        static void StaticClass_Dtor(object sender, EventArgs e) => SalveDados();

        private static readonly string _caminhoArquivo = AppDomain.CurrentDomain.BaseDirectory + $@"\Dados.json";

        private static Dados _dados { get; set; }

        private static void BusqueDados()
        {
            if (File.Exists(_caminhoArquivo))
            {
                var conteudo = File.ReadAllText(_caminhoArquivo);
                if (conteudo != null && !string.IsNullOrEmpty(conteudo))
                {
                    _dados = JsonConvert.DeserializeObject<Dados>(conteudo);
                }
            }

            _dados = new Dados();
        }

        public static void SalveDados()
        {
            if (File.Exists(_caminhoArquivo))
            {
                File.Delete(_caminhoArquivo);
            }

            var jsonDados = JsonConvert.SerializeObject(_dados);

            File.WriteAllText(_caminhoArquivo, jsonDados);
        }

        #region Wrapping Dados

        public static PersistenceList<KeyValuePair<DateTime, Lancamento>> LancamentosRealizados
        {
            get
            {
                return _dados.LancamentosRealizados;
            }

            set
            {
                _dados.LancamentosRealizados = value;

                SalveDados();
            }
        }

        public static DateTime HorarioAtual =>
            AssistenteHorario.VerifiqueConexao() ? AssistenteHorario.ObtenhaHorarioAtualBrasiliaViaApi() : DateTime.Now;

        public static bool EhPrimeiroDesbloqueioDoDia => HorarioAtual.Day > UltimoDesbloqueio.Day; // && HorarioAtual.TimeOfDay > ConfiguracaoDiario.HorarioInicioDoDia;

        public static ConfiguraoLancamentoDiario ConfiguracaoDiario
        {
            get
            {
                using (var servico = new ServicoDeConfiguracao())
                {
                    return servico.ObtenhaConfiguracao().ConfiguraoLancamentoDiario;
                }
            }
        }

        public static bool RegistroDePontoJaFoiRecuperadoHoje
        {
            get => _dados.RegistroDePontoJaFoiRecuperadoHoje;

            set
            {
                _dados.RegistroDePontoJaFoiRecuperadoHoje = value;
                SalveDados();
            }
        }

        public static Ponto RegistroDePontoDeHoje
        {
            get => _dados.RegistroDePontoDeHoje;

            set
            {
                _dados.RegistroDePontoDeHoje = value;
                SalveDados();
            }
        }

        public static bool LancamentoJaFoiRealizado
        {
            get => _dados.LancamentoJaFoiRealizado;

            set
            {
                _dados.LancamentoJaFoiRealizado = value;
                SalveDados();
            }
        }

        public static Lancamento LancamentoDeHoje
        {
            get => _dados.LancamentoDeHoje;

            set
            {
                _dados.LancamentoDeHoje = value;
                SalveDados();
            }
        }

        public static DateTime UltimoBloqueio
        {
            get => _dados.UltimoBloqueio;

            set
            {
                _dados.UltimoBloqueio = value;
                SalveDados();
            }
        }

        public static DateTime UltimoDesbloqueio
        {
            get => _dados.UltimoDesbloqueio;

            set
            {
                _dados.UltimoDesbloqueio = value;
                SalveDados();
            }
        }

        #endregion
    }
}