using GSAutoTimeEntriesWebApi.Objetos;
using GSAutoTimeEntries.UI;
using GSAutoTimeEntries.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GSAutoTimeEntries.Servicos
{
    public static class ServicoDeLancamentoDiario
    {
        private static ConfiguraoLancamentoDiario _configuracao { get; set; }

        private static ConfiguraoLancamentoDiario Configuracao => _configuracao ?? (_configuracao = new ServicoDeConfiguracao().ObtenhaConfiguracao().ConfiguraoLancamentoDiario);

        public static bool EstahRodando => Tarefa != null && Tarefa.Status == TaskStatus.Running;

        private static readonly TimeSpan Intervalo = new TimeSpan(0, 2, 0);

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
                        if (!Persistencia.LancamentoJaFoiRealizado)
                        {
                            new MethodInvoker(() => Task.Run(ExecuteLancamento)).Invoke();
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
            if(Tarefa != null && FonteDoToken != null)
            {
                FonteDoToken.Cancel();
            }
        }

        [STAThread]
        public static void ExecuteLancamento()
        {
            #region Mock de teste

            //var atividadesMock = new ServicoDeLancamento(new ServicoDeConfiguracao().ObtenhaConfiguracao()).ObtenhaAtividadesAtribuidas();

            //LancamentoEstahSendoExecutado = true;
            //GerenciadorDeForms.Obtenha<frmPrincipal>().Invoke(
            //        (MethodInvoker)delegate
            //        {
            //            GerenciadorDeForms.Crie<frmPopupDiario>(
            //                    new object[]
            //                    {
            //                        new Lancamento
            //                        {
            //                            Data = "16/05/19",
            //                            Batidas = new List<TimeSpan>
            //                            {
            //                                { new TimeSpan(8,  0, 0) },
            //                                { new TimeSpan(12, 0, 0) },
            //                                { new TimeSpan(14, 0, 0) },
            //                                { new TimeSpan(18, 0, 0) }
            //                            },
            //                            Horas = 8
            //                        },
            //                        atividadesMock
            //                    });
            //        });

            #endregion

            #region Produção

            var configGeral = new ServicoDeConfiguracao().ObtenhaConfiguracao();
            if (configGeral == null) return;

            LancamentoEstahSendoExecutado = true;
            using (var servicoDeLancamento = new ServicoDeLancamento(configGeral))
            {
                var ultimoLancamento = Persistencia.LancamentosRealizados.Where(x => x.Key != DateTime.Today)
                                                                         .OrderBy(x => x.Key)
                                                                         .LastOrDefault();
                Lancamento lancamentoValido = null;
                var data = DateTime.Today;

                while (lancamentoValido == null)
                {
                    data = data.AddDays(-1);

                    var lancamento = servicoDeLancamento.ObtenhaLancamentos(data, data, false);

                    if (lancamento != null && 
                        lancamento.Count > 0 && 
                        Persistencia.LancamentosRealizados != null && 
                        !Persistencia.LancamentosRealizados.Select(x => x.Key.ConvertaParaDataPtBr())
                                                           .Contains(lancamento.FirstOrDefault().Data))
                    {
                        lancamentoValido = lancamento.FirstOrDefault();
                    }


                    if (ultimoLancamento.Value != null && lancamento.FirstOrDefault().Data == ultimoLancamento.Key.ConvertaParaDataPtBr())
                        break;
                }

                var atividades = servicoDeLancamento.ObtenhaAtividadesAtribuidas();

                GerenciadorDeForms.Obtenha<frmPrincipal>().Invoke(
                    (MethodInvoker)delegate { GerenciadorDeForms.Crie<frmPopupDiario>(new object[] { lancamentoValido, atividades }); });
            }

            #endregion
        }

        public static void EfetueLancamentos(List<Lancamento> lancamentos)
        {
            GerenciadorDeProgresso.Crie();
            using (var servicoDeLancamento = new ServicoDeLancamento(new ServicoDeConfiguracao().ObtenhaConfiguracao()))
            {
                lancamentos.ForEach(x => servicoDeLancamento.RealizeLancamento(x.LinkAtividade, lancamentos));
            }
            GerenciadorDeProgresso.Apague();
        }

        public static void DispenseLancamento(Lancamento lancamento)
        {
            throw new NotImplementedException();
        }
    }
}
