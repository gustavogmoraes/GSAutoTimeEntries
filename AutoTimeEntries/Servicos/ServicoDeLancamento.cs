using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Forms;
using AutomationBase;
using GSAutoTimeEntries.Objetos;
using GSAutoTimeEntriesWebApi.Objetos;
using GSAutoTimeEntries.UI;
using GSAutoTimeEntries.Utils;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using File = System.IO.File;
using Keys = OpenQA.Selenium.Keys;

namespace GSAutoTimeEntries.Servicos
{
    public class ServicoDeLancamento : IDisposable
    {
        private Configuracao _configuracao;
        private Configuracao Configuracao => _configuracao ?? (_configuracao = new ServicoDeConfiguracao().ObtenhaConfiguracao());

        private ChromeDriver _chromeDriver;
        private ChromeDriver Driver => _chromeDriver ?? (_chromeDriver = CrieChromeDriver());

        private static string DefaultDownloadPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ExecutionFiles");

        private static string DownloadPath { get; set; }

        private ChromeDriver CrieChromeDriver()
        {
            ChromeDriverHelper.CheckUpdateChromeDriver();

            var driverBuilder = ChromeDriverHelper
                .GetDriverBuilder()
                .AllowRunningInsecureContent()
                .DisablePopupBlocking()
                .SetDownloadPath(DownloadPath);

            if (Configuracao.OcultarNavegador)
            {
                driverBuilder.Headless();
            }

            return driverBuilder.Build();
        }

        public ServicoDeLancamento(Configuracao configuracao, Guid sessionId)
        {
            _configuracao = configuracao;

            if (!Directory.Exists(DefaultDownloadPath))
            {
                Directory.CreateDirectory(DefaultDownloadPath);
            }

            DownloadPath = Path.Combine(DefaultDownloadPath, sessionId.ToString());
            Directory.CreateDirectory(DownloadPath);

        }

        private Validador _validador;

        private Validador Validador => _validador ?? (_validador = new Validador());

        public List<Lancamento> ObtenhaLancamentos(DateTime dataInicio, DateTime dataFim, bool exibirProgresso = true)
        {
            var caminhoArquivoDePonto = ObtenhaLancamentosInterno(dataInicio, dataFim, exibirProgresso);
            if (caminhoArquivoDePonto == null)
            {
                return null;
            }

            using (var servicoDeImportacao = new ServicoDeImportacao())
            {
                return servicoDeImportacao.ObtenhaLancamentosPorImportacao(caminhoArquivoDePonto, dataInicio, dataFim, exibirProgresso);
                //var valido = true; Validador.ValideSeArquivoEhRegistroDePonto(caminhoArquivoDePonto, exibirProgresso);
            }
        }

        private string ObtenhaLancamentosInterno(DateTime dataInicio, DateTime dataFim, bool exibirProgresso = true)
        {
            if (exibirProgresso)
            {
                GerenciadorDeProgresso.AtualizeProgressBar(5, "Iniciando obtenção de lançamentos");
            }

            return TenteObterLancamentos(dataInicio, dataFim, exibirProgresso);
        }

        private string TenteObterLancamentos(DateTime dataInicio, DateTime dataFim, bool exibirProgresso = true)
        {
            try
            {
                RealizeLoginAutoatendimentoLG(Configuracao.ConfiguracaoAutotendimento, exibirProgresso);
                Driver.EsperePaginaCarregar();

                if (exibirProgresso)
                {
                    GerenciadorDeProgresso.AtualizeProgressBar(40, "Acessando Autoatendimento");
                }

                Driver.EsperePaginaCarregar();

                var dtIni = dataInicio.ConvertaParaDataPtBr();
                var dtFim = dataFim.ConvertaParaDataPtBr();

                return ObtenhaRelatorioDePonto(dtIni, dtFim, exibirProgresso);
            }
            catch (Exception e)
            {
                if (exibirProgresso)
                {
                    GerenciadorDeProgresso.ExibaErro("Aconteceu um erro! Reiniciando...");
                }

                Thread.Sleep(2000);
                SalveDataParaRetry(dataInicio, dataFim);

                Console.WriteLine(e);

                Driver.Dispose();
                Application.Restart();
                Environment.Exit(0);
            }

            return null;
        }

        public class AtividadeLancada
        {
            public string LinkAtividade { get; set; }

            public string LinkEditar { get; set; }

            public double Horas { get; set; }

            public DateTime Data { get; set; }
        }

        private List<AtividadeLancada> ObtenhaAtividadesEntreDatas(DateTime data1, DateTime data2)
        {
            Driver.Navigate().GoToUrl("http://srv-redmine/redmine/projects/fpr-2017_1_juo/time_entries");
            Driver.EsperePaginaCarregar();

            ApliqueFiltroDeUsuarioIgualAMim();
            var elData = Driver.FindElementById("operators_spent_on");
            elData.SendKeys("entre");

            Driver.EspereElementoSerExibido(By.Id("values_spent_on_1"));
            var elDataSelector = Driver.FindElementById("values_spent_on_1");
            elDataSelector.SendKeys(data1.ObtenhaDataRedmine());

            Driver.EspereElementoSerExibido(By.Id("values_spent_on_2"));
            var elDataSelector2 = Driver.FindElementById("values_spent_on_2");
            elDataSelector2.SendKeys(data2.ObtenhaDataRedmine());

            ApliquePesquisaRedmine();

            IWebElement table;
            try
            {
                table = Driver.FindElement(By.CssSelector("table.list.time-entries"));
            }
            catch
            {
                return new List<AtividadeLancada>();
            }

            return ExtraiaAtividadesDaTable(table);
        }

        public DateTime ObtenhaDataMaisProximaQuePossuaAtividade(DateTime data)
        {
            var data1 = data.AddDays(-21);
            var data2 = data.AddDays(-1);

            var atividades20DiasAntes = ObtenhaAtividadesEntreDatas(data1, data2);

            data1 = data.AddDays(1);
            data2 = data.AddDays(21);
            var atividades20DiasDepois = ObtenhaAtividadesEntreDatas(data1, data2);

            var range = new List<DateTime>();
            range.AddRange(atividades20DiasAntes.Select(x => x.Data));
            range.AddRange(atividades20DiasDepois.Select(x => x.Data));

            return data.GetClosestDateTime(range);
        }

        private void ApliqueFiltroDeUsuarioIgualAMim()
        {
            var elFilter = Driver.FindElement(By.Id("add_filter_select"));
            elFilter.SendKeys("Usuário");
        }

        private List<AtividadeLancada> ExtraiaAtividadesDaTable(IWebElement table)
        {
            var tbody = table.FindElement(By.TagName("tbody"));
            var trs = tbody.FindElements(By.TagName("tr"));

            return trs.Select(x => new AtividadeLancada
            {
                LinkAtividade = x.FindElement(By.ClassName("issue")).FindElement(By.TagName("a")).GetAttribute("href"),
                LinkEditar = ObtenhaLinkEditar(x),
                Horas = Convert.ToDouble(x.FindElement(By.ClassName("hours"))?.Text),
                Data = x.FindElement(By.ClassName("spent_on")).Text.ParaDateTime()
            }).ToList();
        }

        public List<AtividadeLancada> ObtenhaMinhasAtividadesDeUmDia(DateTime data)
        {
            Driver.Navigate().GoToUrl("http://srv-redmine/redmine/projects/fpr-2017_1_juo/time_entries");
            Driver.EsperePaginaCarregar();

            ApliqueFiltroDeUsuarioIgualAMim();

            var elData = Driver.FindElementById("operators_spent_on");
            elData.SendKeys("igual a");

            Driver.EspereElementoSerExibido(By.Id("values_spent_on_1"));
            var elDataSelector = Driver.FindElementById("values_spent_on_1");
            elDataSelector.SendKeys(data.ObtenhaDataRedmine());

            ApliquePesquisaRedmine();

            IWebElement table;
            try
            {
                table = Driver.FindElement(By.CssSelector("table.list.time-entries"));
            }
            catch
            {
                return new List<AtividadeLancada>();
            }

            return ExtraiaAtividadesDaTable(table);
        }

        private void ApliquePesquisaRedmine()
        {
            var elAplicar = Driver.FindElementByCssSelector("a.icon.icon-checked");
            elAplicar.Click();
        }

        private string ObtenhaLinkEditar(IWebElement el)
        {
            try
            {
                var a = el.FindElement(By.ClassName("buttons"));
                var b = a.FindElements(By.TagName("a"));
                var c = b.FirstOrDefault(y => y.GetAttribute("title") == "Editar");
                var d = c.GetAttribute("href");

                return d;
            }
            catch
            {
                return string.Empty;
            }
        }

        public bool ReduzaHorasDeUmaAtividade(string linkEditar, double deducao)
        {
            if (string.IsNullOrEmpty(linkEditar))
            {
                return false;
            }

            Driver.Navigate().GoToUrl($"{linkEditar}");
            Driver.EsperePaginaCarregar();

            var elHoras = Driver.FindElementById("time_entry_hours");
            var valor = elHoras.GetAttribute("value");
            elHoras.Clear();
            elHoras.SendKeys(Math.Round(Convert.ToDouble(valor) - deducao, 2).ToString(CultureInfo.InvariantCulture));

            var elCommit = Driver.FindElementByName("commit");
            elCommit.Click();

            return true;
        }

        public bool AumenteHorasDeUmaAtividade(string linkEditar, double aumento)
        {
            if (string.IsNullOrEmpty(linkEditar))
            {
                return false;
            }

            Driver.Navigate().GoToUrl($"{linkEditar}");
            Driver.EsperePaginaCarregar();

            var elHoras = Driver.FindElementById("time_entry_hours");
            var valor = elHoras.GetAttribute("value");
            elHoras.Clear();
            elHoras.SendKeys(Math.Round(Convert.ToDouble(valor) + aumento, 2).ToString(CultureInfo.InvariantCulture));

            var elCommit = Driver.FindElementByName("commit");
            elCommit.Click();

            return true;
        }

        private string ObtenhaTextoMelhoriaPai(string url)
        {
            Driver.Navigate().GoToUrl(url);
            Driver.EsperePaginaCarregar();

            var elMelhoria = Driver.FindElementsByTagName("p").FirstOrDefault(x => x.Text.ToLowerInvariant().Contains("melhoria"));
            return elMelhoria?.Text.Split('#').Last();
        }

        public Dictionary<string, string> ObtenhaAtividadesAtribuidas()
        {
            RealizeLoginRedmine(Configuracao.ConfiguracaoRedmine, false);

            Driver.Navigate().GoToUrl(@"http://srv-redmine/redmine/my/page");
            Driver.EsperePaginaCarregar();

            var elMyPageBox = Driver.FindElementByClassName("mypage-box");
            var elHasNoData = elMyPageBox.FindElements(By.ClassName("nodata"));
            if (elHasNoData.Count > 0)
            {
                return new Dictionary<string, string>();
            }

            var elTable = Driver.FindElementByClassName("list");
            var elTasks = elTable.FindElements(By.ClassName("subject")).ToList();

            return elTasks.Select(x => x.FindElement(By.TagName("a")))
                                .ToDictionary(y => y.Text, y => y.GetAttribute("href"))
                                .ToDictionary(z => ObtenhaTextoMelhoriaPai(z.Value) + $" - {z.Key}", z => z.Value);

        }

        private void SalveDataParaRetry(DateTime dataInicio, DateTime dataFim)
        {
            var retry = new DatasRetry
            {
                DataInicio = dataInicio,
                DataFim = dataFim
            };

            var jsonRetry = JsonConvert.SerializeObject(retry);

            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "retryFile.json", jsonRetry);
        }

        public void RealizeLancamentoMultiplo(IList<Lancamento> listaDeLancamentos, bool exibirProgresso = true)
        {
            RealizeLoginRedmine(Configuracao.ConfiguracaoRedmine);

            Driver.EsperePaginaCarregar();

            if (exibirProgresso)
            {
                GerenciadorDeProgresso.AtualizeProgressBar(35, $"{Configuracao.ConfiguracaoRedmine.Usuario} - Calculando lançamentos");
                GerenciadorDeProgresso.AtualizeProgressBar(50, $"{Configuracao.ConfiguracaoRedmine.Usuario} - {listaDeLancamentos.Count} lançamentos para realizar");
            }

            var progressPorLancamento = 45 / listaDeLancamentos.Count;
            int i = 1;
            foreach (var lancamento in listaDeLancamentos)
            {
                if (exibirProgresso)
                {
                    GerenciadorDeProgresso.AtualizeProgressBar(
                        GerenciadorDeProgresso.Progresso + progressPorLancamento,
                        $"Realizando lançamento {i}/{listaDeLancamentos.Count}");
                }

                i++;

                Driver.Navigate().GoToUrl(lancamento.LinkAtividade + "/time_entries/new");
                Driver.EsperePaginaCarregar();

                // Data
                var elData = Driver.FindElementById("time_entry_spent_on");
                elData.Clear();
                elData.SendKeys(lancamento.Data.ObtenhaDataRedmine());

                // Tempo gasto
                Driver.FindElementById("time_entry_hours").SendKeys(lancamento.Horas.ToString(CultureInfo.InvariantCulture));

                // Comentário
                Driver.FindElementById("time_entry_comments").SendKeys(lancamento.Comentario ?? string.Empty);

                // Atividade
                Driver.FindElementById("time_entry_activity_id").SendKeys(lancamento.TipoAtividade);

                //Submit
                var criar = By.Name("commit");
                Driver.FindElement(criar).Submit();

                using (var db = Persistencia.AbraConexao())
                {
                    var lancamentosRealizados = db.GetCollection<Lancamento>("LancamentosRealizados");
                    lancamentosRealizados.Insert(lancamento);
                }
            }

            Driver.Quit();
        }

        public void RealizeLancamento(string paginaDaTarefa, IList<Lancamento> listaDeLancamentos, bool exibirProgresso = true, bool jaLogado = false)
        {
            if (!jaLogado)
            {
                RealizeLoginRedmine(Configuracao.ConfiguracaoRedmine);
            }

            Driver.EsperePaginaCarregar();

            if (exibirProgresso)
            {
                GerenciadorDeProgresso.AtualizeProgressBar(35, $"{Configuracao.ConfiguracaoRedmine.Usuario} - Calculando lançamentos");
                GerenciadorDeProgresso.AtualizeProgressBar(50, $"{Configuracao.ConfiguracaoRedmine.Usuario} - {listaDeLancamentos.Count} lançamentos para realizar");
            }

            var progressPorLancamento = 45 / listaDeLancamentos.Count;
            int i = 1;
            foreach (var lancamento in listaDeLancamentos)
            {
                if (exibirProgresso)
                    GerenciadorDeProgresso.AtualizeProgressBar(
                        GerenciadorDeProgresso.Progresso + progressPorLancamento,
                        $"Realizando lançamento {i}/{listaDeLancamentos.Count}");
                i++;

                Driver.Navigate().GoToUrl(lancamento.LinkAtividade + "/time_entries/new");

                Driver.EsperePaginaCarregar();

                // Data
                var elData = Driver.FindElementById("time_entry_spent_on");
                elData.Clear();

                var novaData = lancamento.Data.ObtenhaDataRedmine();

                elData.SendKeys(novaData);

                // Tempo gasto
                Driver.FindElementById("time_entry_hours").SendKeys(lancamento.Horas.ToString(CultureInfo.InvariantCulture));

                // Comentário
                Driver.FindElementById("time_entry_comments").SendKeys(lancamento.Comentario ?? string.Empty);

                // Atividade
                Driver.FindElementById("time_entry_activity_id").SendKeys(lancamento.TipoAtividade);

                //Submit
                var criar = By.Name("commit");
                Driver.FindElement(criar).Submit();

                using (var db = Persistencia.AbraConexao())
                {
                    var lancamentosRealizados = db.GetCollection<Lancamento>("LancamentosRealizados");
                    lancamentosRealizados.Insert(lancamento);
                }
            }

            //Driver.Quit();
        }

        private const string LinkPrdAa1Lg = @"https://prd-aa1.lg.com.br/lg/";

        private const string LinkAutoAtendimento = LinkPrdAa1Lg + @"Produtos/AA/AAExibicaoDadosDoColaborador/Inicio";

        private const string LinkEmissaoDeCartaoDePontoLayout1 = LinkPrdAa1Lg + @"Produtos/Infraestrutura/IntegracaoPonto?urlFuncionalidade=/Login/Index/RelatorioCartaodePontoLayout1";


        private const string RelatorioIdComboFiltroColaboradores = "comboGridFiltroColaboradoresIndividualAux";

        private string ObtenhaRelatorioDePonto(string dataInicio, string dataFim, bool exibirProgresso = true)
        {
            Driver.EsperePaginaCarregar();

            Driver.Navigate().GoToUrl(LinkAutoAtendimento);

            Driver.EsperePaginaCarregar();

            var matricula = Driver.FindElementsByTagName("li")[3].Text;

            if (exibirProgresso)
            {
                GerenciadorDeProgresso.AtualizeProgressBar(50, "Acessando emissão de relatório");
            }

            // Em vez de ficar batendo cabeça, descobrindo como que eu ia entrar naquele maldito iFrame sem title
            // peguei logo a src dele e navegamos pra ele, já que é somente isso que estou fazendo aqui, e nem vou querer voltar na outra página
            Driver.Navigate().GoToUrl(LinkEmissaoDeCartaoDePontoLayout1);
            Driver.EsperePaginaCarregar();
            Driver.EspereTempoEspecifico(5);

            var byComboPesquisa = By.Id(RelatorioIdComboFiltroColaboradores);

            Driver.EspereElementoSerExibidoESerClicavel(byComboPesquisa);

            var elComboPesquisa = Driver.FindElement(byComboPesquisa);

            Driver.EspereTempoEspecifico(2);

            elComboPesquisa.Click();
            Driver.EspereTempoEspecifico(1);
            elComboPesquisa.SendKeys(matricula);

            Driver.EspereTempoEspecifico(3);

            var bySubItem = By.ClassName("cg-SubDivItem");
            var elSubItem = Driver.FindElement(bySubItem);
            elSubItem.Click();
            Driver.EsperePaginaCarregar();
            Driver.EspereTempoEspecifico(2);

            var byDataInicio = By.Id("DataInicioPeriodoFechamento");
            var byDataFim = By.Id("DataFimPeriodoFechamento");

            Driver.EspereTempoEspecifico(2);

            Driver.EsperePaginaCarregar();

            Driver.EspereElementoSerExibidoESerClicavel(byDataInicio);
            Driver.EspereElementoSerExibidoESerClicavel(byDataFim);

            if (exibirProgresso)
            {
                GerenciadorDeProgresso.AtualizeProgressBar(50, "Preenchendo campos");
            }

            Driver.EspereTempoEspecifico(3);

            var elDataInicio = Driver.FindElement(byDataInicio);
            elDataInicio.Click();
            elDataInicio.SendKeys(dataInicio);
            elDataInicio.SendKeys(Keys.Return);

            Driver.EspereTempoEspecifico(2);

            var elDataFim = Driver.FindElement(byDataFim);
            elDataFim.SendKeys(dataFim);
            elDataFim.SendKeys(Keys.Return);

            Driver.EspereTempoEspecifico(5);

            Driver.FindElementById("btnEmitirTopo").Click();
            Driver.EsperePaginaCarregar();
            Driver.EspereTempoEspecifico(2);

            Driver.EspereElementoSerExibidoESerClicavel(By.Id("resultListaTarefas"));

            //Ficar estuprando o botao de recarregar de 2 em 2 segundos

            //var elId = Driver.FindElement(By.CssSelector("#GridListaTarefas>div.t-grid-content.classeOverflowGrid>table>tbody>tr>td:nth-child(1)"));
            //var taskId = elId.GetAttribute("title");

            //Driver.EspereTempoEspecifico(2);

            //Driver.Navigate().GoToUrl($@"https://prd-pt1.lg.com.br/Ponto/VisualizarRelatorio/Inicio?id={taskId}&idServico=CartaoDePontoLayout1");
            
            //Driver.ExecuteScript($"document.getElementById('DataInicioPeriodoFechamento').value='{dataInicio}'");
            //Driver.ExecuteScript($"document.getElementById('DataInicioPeriodoFechamento').value='{dataFim}'");

            if (exibirProgresso)
            {
                GerenciadorDeProgresso.AtualizeProgressBar(60, "Aguardando emissão");
            }

            var contagemDeJanelas = Driver.WindowHandles.Count;
            Driver.EspereCondicao(x => x.WindowHandles.Count == contagemDeJanelas + 1, 60);

            string parentWindowHandler = Driver.CurrentWindowHandle;
            string subWindowHandler = null;

            var handles = Driver.WindowHandles;
            using (var enumerator = handles.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    subWindowHandler = enumerator.Current;
                }

                Driver.SwitchTo().Window(subWindowHandler);
            }
            
            Driver.EsperePaginaCarregar();

            var allXlsxFiles = Directory.GetFiles(DownloadPath, "*.xlsx").ToList();
            allXlsxFiles.ForEach(fileName =>
            {
                if (fileName.Contains("Relatorio_CartaoDePontoLayout"))
                {
                    File.Delete(Path.Combine(DownloadPath, fileName));
                }
            });

            if (Configuracao.OcultarNavegador)
            {
                Driver.EnableHeadlessDownload(DownloadPath);
            }

            var id = Driver.Url.Between(@"VisualizarRelatorio/Inicio?id=", @"&idServico");
            Driver.Navigate().GoToUrl(@"https://prd-pt1.lg.com.br/Ponto/EmissaoRelatorio/DownloadXls?id=" + id);

            if (exibirProgresso)
            {
                GerenciadorDeProgresso.AtualizeProgressBar(70, "Lendo relatório");
            }
            
            var timeout = new Stopwatch();
            timeout.Start();

            while (true)
            {
                if (timeout.Elapsed > TimeSpan.FromSeconds(30))
                {
                    break;
                }

                foreach (var fileName in Directory.GetFiles(DownloadPath, "*.xlsx").ToList())
                {
                    if (fileName.Contains("Relatorio_CartaoDePontoLayout"))
                    {
                        return fileName;
                    }
                }

                Thread.Sleep(TimeSpan.FromMilliseconds(500));
            }

            return null;
        }

        public void RealizeLoginRedmine(ConfiguracaoRedmine configuracao, bool exibirProgresso = true)
        {
            if (exibirProgresso)
                GerenciadorDeProgresso.AtualizeProgressBar(20, $"{configuracao.Usuario} - Iniciando automatização");
            Driver.Navigate().GoToUrl(configuracao.LinkLogin);

            if (exibirProgresso)
                GerenciadorDeProgresso.AtualizeProgressBar(30, $"{configuracao.Usuario} - Realizando login no Redmine");
            Driver.EsperePaginaCarregar();
            Driver.EspereTempoEspecifico(2);

            Driver.FindElementById("username").SendKeys(configuracao.Usuario);
            Driver.FindElementById("password").SendKeys(configuracao.Senha);

            var elBotaoPesquisa = By.Name("login");

            var botaoPesquisa = Driver.FindElement(elBotaoPesquisa);

            botaoPesquisa.Submit();
        }

        private void RealizeLoginAutoatendimentoLG(ConfiguracaoAutotendimento configuracao, bool exibirProgresso = true)
        {
            if (exibirProgresso)
                GerenciadorDeProgresso.AtualizeProgressBar(15, "Iniciando automatização");
            Driver.Navigate().GoToUrl(configuracao.LinkLogin);

            if (exibirProgresso)
                GerenciadorDeProgresso.AtualizeProgressBar(30, "Fazendo login no autoatendimento");
            Driver.EsperePaginaCarregar();
            Driver.EspereTempoEspecifico(2);

            var byLogin = By.Name("ctl00$ContentPlaceHolder$txtLoginT");
            Driver.EspereElementoSerExibidoESerClicavel(byLogin);
            var elLogin = Driver.FindElement(byLogin);
            elLogin.Click();
            elLogin.SendKeys(configuracao.Usuario);

            var byPasswd = By.Name("ctl00$ContentPlaceHolder$txtSenhaT");
            Driver.EspereElementoSerExibidoESerClicavel(byPasswd);
            var elPasswd = Driver.FindElement(byPasswd);
            elPasswd.Click();
            elPasswd.SendKeys(configuracao.Senha);

            var elBotaoSignIn = By.Name("ctl00$ContentPlaceHolder$btnEntrar");
            var botaoSignIn = Driver.FindElement(elBotaoSignIn);
            botaoSignIn.Click();
        }

        public void Dispose()
        {
            _configuracao = null;

            Driver?.Dispose();
        }

        public bool TenteAcessarLink(string linkAtividade)
        {
            Driver.Navigate().GoToUrl($@"{linkAtividade}/time_entries/new");

            try
            {
                var idError = Driver.FindElementById("errorExplanation");
                return false;
            }
            catch (Exception e)
            {
                return true;
            }
        }
    }
}
