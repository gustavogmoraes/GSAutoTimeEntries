using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GSAutoTimeEntries.Objetos;
using GSAutoTimeEntriesWebApi.Objetos;
using GSAutoTimeEntries.UI;
using GSAutoTimeEntries.Utils;

namespace GSAutoTimeEntries.Servicos
{
    public static class ServicoDeLancamentoDiario
    {
        private static ConfiguraoLancamentoDiario _configuracao { get; set; }
        private static ConfiguraoLancamentoDiario Configuracao => _configuracao ?? (_configuracao = new ServicoDeConfiguracao().ObtenhaConfiguracao().ConfiguraoLancamentoDiario);

        public static bool EstahRodando => Tarefa != null && Tarefa.Status == TaskStatus.Running;

        private static readonly TimeSpan Intervalo = TimeSpan.FromMinutes(2);

        private static Task Tarefa { get; set; }

        private static bool LancamentoEstahSendoExecutado { get; set; }

        private static CancellationTokenSource FonteDoToken { get; set; }

        public static void Inicie()
        {
            if (EstahRodando) Pare();

            FonteDoToken = new CancellationTokenSource();
            Task.Run(() =>
            {
                while (!FonteDoToken.Token.IsCancellationRequested)
                {
                    if (VerifiqueSeEhHoraDeLancar())
                    {
                        var dataAnterior = GetPreviousDate();

                        //Se não tiver lançamentos nessa data
                        if (!Persistencia.VerifiqueSeExistemLancamentosDeUmaData(dataAnterior.Date))
                        {
                            new MethodInvoker(() => Task.Run(() => ExecuteLancamento(dataAnterior))).Invoke();
                        }
                    }

                    Thread.Sleep(Intervalo);
                }
            },
            FonteDoToken.Token);
        }

        private static bool VerifiqueSeEhHoraDeLancar()
        {
            return DateTime.Now >= Configuracao.HorarioDoLembrete.ParaDateTime() && !LancamentoEstahSendoExecutado;
        }

        public static void Pare()
        {
            if(Tarefa != null)
            {
                FonteDoToken?.Cancel();
            }
        }

        private static DateTime GetPreviousDate()
        {
            var data = DateTime.Today;
            DateTime? dataValida = null;
            while (dataValida == null)
            {
                data = data.AddDays(-1);
                if (data.DayOfWeek == DayOfWeek.Saturday || data.DayOfWeek == DayOfWeek.Sunday)
                {
                    continue;
                }

                dataValida = data;
            }

            return dataValida.Value.Date;
        }

        [STAThread]
        public static void ExecuteLancamento(DateTime data)
        {
            #region Mock de teste

            //LancamentoEstahSendoExecutado = true;
            //var lancamentoValido = JsonConvert.DeserializeObject<Lancamento>(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lancamentosSalvo.json")));
            //var atividades = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "atvsSalva.json")));

            //GerenciadorDeForms.Obtenha<frmPrincipal>().Invoke((MethodInvoker)delegate
            //{
            //    GerenciadorDeForms.Crie<frmPopupDiario>(new object[] { lancamentoValido, atividades });
            //});

            #endregion

            #region Produção

            var configGeral = new ServicoDeConfiguracao().ObtenhaConfiguracao();
            if (configGeral == null) return;

            LancamentoEstahSendoExecutado = true;
            using (var servicoDeLancamento = new ServicoDeLancamento(configGeral))
            using (var db = Persistencia.AbraConexao())
            {
                var lancamentosRealizados = db.ObtenhaCollectionLancamentosRealizados();

                var ultimoLancamento = lancamentosRealizados.Find(x => x.Data != DateTime.Today)
                                                            .OrderBy(x => x.Data)
                                                            .LastOrDefault();
                Lancamento lancamentoValido = null;

                data = data.AddDays(1);
                while (lancamentoValido == null)
                {
                    data = data.AddDays(-1);
                    if (data.DayOfWeek == DayOfWeek.Saturday || data.DayOfWeek == DayOfWeek.Sunday)
                    {
                        continue;
                    }

                    var lancamento = servicoDeLancamento.ObtenhaLancamentos(data, data, false);

                    if (lancamento != null &&
                        lancamento.Count > 0 &&
                        lancamentosRealizados.FindAll() != null &&
                        !lancamentosRealizados.FindAll().ToList().Select(x => x.Data)
                                                                 .Contains(lancamento.First().Data))
                    {
                        lancamentoValido = lancamento.FirstOrDefault();
                    }


                    if (ultimoLancamento != null && lancamento?.First().Data == ultimoLancamento.Data)
                        break;
                }

                var atividades = servicoDeLancamento.ObtenhaAtividadesAtribuidas();

                GerenciadorDeForms.Obtenha<frmPrincipal>().Invoke((MethodInvoker)delegate
               {
                   GerenciadorDeForms.Crie<frmPopupDiario>(new object[] { lancamentoValido, atividades });
               });
            }

            #endregion
        }

        public static void EfetueLancamentos(List<Lancamento> lancamentos)
        {
            GerenciadorDeProgresso.Crie();

            using (var servicoDeLancamento = new ServicoDeLancamento(new ServicoDeConfiguracao().ObtenhaConfiguracao()))
            {
                servicoDeLancamento.RealizeLancamentoMultiplo(lancamentos);
            }

            GerenciadorDeProgresso.Apague();

            LancamentoEstahSendoExecutado = false;
        }

        public static void DispenseLancamento(Lancamento lancamento)
        {
            lancamento.Dispensado = true;

            using (var db = Persistencia.AbraConexao())
            {
                var lancamentosDispensados = db.GetCollection<Lancamento>(Persistencia.NomeCollectionLancamentosDispensados);
                lancamentosDispensados.Insert(lancamento);
            }

            LancamentoEstahSendoExecutado = false;
        }
    }
}
