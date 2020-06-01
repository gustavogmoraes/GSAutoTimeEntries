using GSAutoTimeEntries.Servicos;
using GSAutoTimeEntries.UI;
using GSAutoTimeEntries.Utils;
using GSAutoTimeEntriesWebApi.Objetos;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using GSAutoTimeEntries.Objetos;

namespace GSAutoTimeEntries
{
    public static class Program
    {
        public static readonly string NomeRetryFile = @"retryFile.json";

        public static readonly string CaminhoRetryFile = $@"{AppDomain.CurrentDomain.BaseDirectory}/{NomeRetryFile}";

        public static readonly string StartupFolder = Environment.GetFolderPath(Environment.SpecialFolder.Startup);

        public static readonly string NomeAtalho = @"GSAutoTimeEntries.lnk";

        public static readonly string CaminhoDoAtalho = $@"{StartupFolder}/{NomeAtalho}";

        public static void InicieAplicacaoEmContextoNormal()
        {
            Application.Run(GerenciadorDeForms.Crie<frmPrincipal>());
        }

        [STAThread]
        public static void Main(params string[] arguments)
        {
            Sessao.Id = Guid.NewGuid();
            Sessao.StartingTime = DateTime.Now;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            switch (ObtenhaContextoInicializacao(arguments))
            {
                case EnumContextoInicializacao.NORMAL:
                    InicieAplicacaoEmContextoNormal();
                    break;

                case EnumContextoInicializacao.INICIAR_SERVICO_LANCAMENTO_DIARIO:
                    Application.Run(GerenciadorDeForms.Crie<frmPrincipal>(new object[] { true }, false, null, false));
                    break;

                case EnumContextoInicializacao.APLICACAO_JA_ABERTA:
                    ElimineProcessos();
                    InicieAplicacaoEmContextoNormal();
                    break;

                case EnumContextoInicializacao.RETRY:
                    Application.Run();
                    using (var servicoDeConfiguracao = new ServicoDeConfiguracao())
                    {
                        var configuracao = servicoDeConfiguracao.ObtenhaConfiguracao();
                        if (configuracao == null)
                        {
                            InicieAplicacaoEmContextoNormal();
                            return;
                        }

                        using (var servicoDeLancamento = new ServicoDeLancamento(configuracao, Sessao.Id))
                        {
                            var jsonRetry = System.IO.File.ReadAllText(CaminhoRetryFile);
                            var retry = JsonConvert.DeserializeObject<DatasRetry>(jsonRetry);

                            servicoDeLancamento.ObtenhaLancamentos(retry.DataInicio, retry.DataFim);
                        }
                    }
                    break;
            }
        }

        private static void InicieMonitoramentoDeEventosDeTrocaDeSessao()
        {
            SystemEvents.SessionSwitch += MonitoradorDeLogins.OnSessionSwitch;
        }

        private static Process[] ObtenhaProcessosDessaAplicacaoAbertos()
        {
            var entryAssembly = Assembly.GetExecutingAssembly();// .GetEntryAssembly();

            var processName = Path.GetFileNameWithoutExtension(entryAssembly.Location);
            var retorno = Process.GetProcessesByName(processName);

            return retorno;
        }

        public static void ElimineProcessos()
        {
            var processos = ObtenhaProcessosDessaAplicacaoAbertos();
            processos.Where(x => x.Id != Process.GetCurrentProcess().Id)
                     .ToList()
                     .ForEach(x => x.Kill());
        }

        private static bool VerifiqueSeAplicacaoJaEstahAberta()
        {
            return ObtenhaProcessosDessaAplicacaoAbertos().Length >= 2;
        }

        private static EnumContextoInicializacao ObtenhaContextoInicializacao(params string[] arguments)
        {
            if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "retryFile.json"))
                return EnumContextoInicializacao.RETRY;

            else if (arguments.Contains("-inicieServicoLancamento"))
                return EnumContextoInicializacao.INICIAR_SERVICO_LANCAMENTO_DIARIO;

            else if (VerifiqueSeAplicacaoJaEstahAberta())
                return EnumContextoInicializacao.APLICACAO_JA_ABERTA;

            return EnumContextoInicializacao.NORMAL;
        }
    }
}
