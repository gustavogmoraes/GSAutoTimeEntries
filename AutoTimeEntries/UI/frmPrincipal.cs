using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.IO.Pipes;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GSAutoTimeEntriesWebApi.Objetos;
using GSAutoTimeEntries.Servicos;
using GSAutoTimeEntries.Utils;
using MetroFramework.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using GSAutoTimeEntries.Objetos;
using IWshRuntimeLibrary;
using File = System.IO.File;

namespace GSAutoTimeEntries.UI
{
    public partial class frmPrincipal : MetroForm
    {
        private List<Button> _listaDeBotoes => new List<Button>
        {
            { btnCorretivo },
            { btnDiario },
            { btnRelatorios }
        };

        public void InicieBotoes()
        {
            if (ucConfiguracao1.Configuracoes != null)
            {
                HabiliteBotoes();
            }
            else
            {
                DesabiliteBotoes();
            }
        }

        private void HabiliteBotoes()
        {
            _listaDeBotoes.ForEach(x => x.Enabled = true);
        }

        public void DesabiliteBotoes()
        {
            _listaDeBotoes.ForEach(x => x.Enabled = false);
        }

        private Size TamanhoPadrao = new Size(829, 396);

        // Minimizar e maximizar clicando no ícone na taskbar
        const int WS_MINIMIZEBOX = 0x20000;
        const int CS_DBLCLKS = 0x8;
        protected override CreateParams CreateParams
        {
            get
            {
                var parametrosDeCriacao = base.CreateParams;
                parametrosDeCriacao.Style |= WS_MINIMIZEBOX;
                parametrosDeCriacao.ClassStyle |= CS_DBLCLKS;
                return parametrosDeCriacao;
            }
        }

        private ServicoDeConfiguracao _servicoDeConfiguracao { get; set; }

        private Configuracao Configuracao => (_servicoDeConfiguracao ?? (_servicoDeConfiguracao = new ServicoDeConfiguracao())).ObtenhaConfiguracao();

        public frmPrincipal(bool colocarAppNaSystemTray)
        {
            InitializeComponent();

            InicializePadrao();

            Hide = colocarAppNaSystemTray;
            ColoqueAppNaSystemTray();
        }

        public frmPrincipal(DatasRetry retry)
        {
            InitializeComponent();
            InicializePadrao();

            ColoqueAppNaSystemTray();

            GerenciadorDeForms.Crie<frmLancamentoCorretivo>(new[] { retry });
            btnCorretivo.Enabled = true;
        }

        public frmPrincipal()
        {
            InitializeComponent();
            InicializePadrao();
        }

        private void InicializePadrao()
        {
            // Esconder o ícone da taskbar até que o mesmo sejá colocado lá
            notifyIcon1.Visible = false;
             
            Size = TamanhoPadrao;

            InicieBotoes();


            if (Configuracao != null) CarregueConfiguracao();

            //

            //using (var servicoDeLancamento = new ServicoDeLancamento(Configuracao))
            //{
            //    var atribs = servicoDeLancamento.ObtenhaAtividadesAtribuidas();
            //}

            //
            if (Configuracao?.ConfiguraoLancamentoDiario != null && Configuracao.ConfiguraoLancamentoDiario.Habilitar)
            {
                Task.Run(ServicoDeLancamentoDiario.Inicie);
            }
        }

        private void CarregueConfiguracao()
        {
            if (Configuracao == null) return;

            if (Configuracao.NomeUtilizador != null)
            {
                lblBemVindo.Text = string.Format(lblBemVindo.Text, Configuracao.NomeUtilizador);
            }

            if (Configuracao.ConfiguraoLancamentoDiario != null)
            {
                lblStatus0.Text = "Configurado para";
            }
        }

        public void ColoqueAppNaSystemTray()
        {
            if (WindowState == FormWindowState.Normal || WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Minimized;
            }


            Hide();
            notifyIcon1.Visible = true;
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ColoqueAppNaSystemTray();
        }

        private void itemMostrarApp_Click(object sender, EventArgs e)
        {
            RestaureJanela();
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            RestaureJanela();
        }

        public void RestaureJanela()
        {
            Show();
            WindowState = FormWindowState.Normal;
            Activate();
        }

        private void btnCorretivo_Click(object sender, EventArgs e)
        {
            GerenciadorDeForms.Crie<frmLancamentoCorretivo>();

            btnCorretivo.Enabled = false;
        }

        public void AlterneBotaoCorretivo()
        {
            btnCorretivo.Enabled = true;
        }

        public void AlterneBotaoDiario()
        {
            btnDiario.Enabled = true;
        }

        private void btnConfigurar_Click_1(object sender, EventArgs e)
        {
            
        }

        private void ucBorders1_Load(object sender, EventArgs e)
        {

        }

        private void BtnDiario_Click(object sender, EventArgs e)
        {
            GerenciadorDeForms.Crie<frmLancamentoDiario>();

            btnDiario.Enabled = false;
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            if (Hide)
                Hide();
        }

        bool Hide { get; set; }

        private async void Button1_Click_1(object sender, EventArgs e)
        {
            var configuracao = new ServicoDeConfiguracao().ObtenhaConfiguracao();
            var lancamentos = new List<Lancamento>
            {
                new Lancamento { LinkAtividade = "Retrabalho - Implementação", Data = "20/06/19".ParaDateTime(), Horas = 8.0 }
            };

            var values = new Dictionary<string, string>
            {
                { "Configuracao", JsonConvert.SerializeObject(configuracao.ConfiguracaoRedmine) },
                { "PaginaDaTarefa", "http://srv-redmine/redmine/issues/77269" },
                { "Lancamentos", JsonConvert.SerializeObject(lancamentos) }
            };

            var obj = new
            {
                Configuracao = configuracao.ConfiguracaoRedmine,
                PaginaDaTarefa = "http://srv-redmine/redmine/issues/77269",
                Lancamentos = lancamentos
            };

            //var content = new FormUrlEncodedContent(values);
            var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://PC-1902")
            };

            var response = await client.PostAsync("api/Expose/ExecuteLancamento", content);

            var responseString = await response.Content.ReadAsStringAsync();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            GerenciadorDeForms.Crie<frmLancamentoProvisorio>();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            // var openFileDialog = new OpenFileDialog();
            // openFileDialog.ShowDialog();

            //var listaBloqueados = JsonConvert.DeserializeObject<List<Lancamento>>(File.ReadAllText(@"C:\Users\gustavo.moraes\Desktop\Correcao\FaltaLancar.json"));

            //using (var servicoDeConfiguracao = new ServicoDeConfiguracao())
            //using (var servicoDeLancamento = new ServicoDeLancamento(servicoDeConfiguracao.ObtenhaConfiguracao()))
            //{
            //    servicoDeLancamento.RealizeLancamento(listaBloqueados[0].LinkAtividade, listaBloqueados, dataJahTratada: true);
            //}

            #region MyRegion2 
            //var correcoes = ObtenhaCorrecoes(File.ReadAllLines(openFileDialog.FileName).Skip(1)).ToList();

            //var diasSemAtividade = JsonConvert.DeserializeObject<List<DateTime>>(File.ReadAllText(@"C:\Users\gustavo.moraes\Desktop\Correcao\DiasSemAtividade.json"));

            //var listaBloqueados = new List<Lancamento>();
            //using (var servicoDeConfiguracao = new ServicoDeConfiguracao())
            //using (var servicoDeLancamento = new ServicoDeLancamento(servicoDeConfiguracao.ObtenhaConfiguracao()))
            //{
            //    servicoDeLancamento.RealizeLoginRedmine(servicoDeConfiguracao.ObtenhaConfiguracao().ConfiguracaoRedmine);

            //    foreach (var data in diasSemAtividade)
            //    {
            //        var dataProxima = servicoDeLancamento.ObtenhaDataMaisProximaQuePossuaAtividade(data);
            //        var atividades = servicoDeLancamento.ObtenhaMinhasAtividadesDeUmDia(dataProxima);

            //        var linkAtividade = atividades.OrderBy(x => x.Horas).FirstOrDefault().LinkAtividade;

            //        var lancamento = new Lancamento()
            //        {
            //            LinkAtividade = atividades.First().LinkAtividade,
            //            Data = data.ObtenhaDataRedmine(),
            //            TipoAtividade = "Implementação",
            //            Horas = Math.Round(((TimeSpan) correcoes.FirstOrDefault(x => x.Data == data).Diferenca).TotalHours, 2)
            //        };

            //        if (servicoDeLancamento.TenteAcessarLink(linkAtividade))
            //        {
            //            servicoDeLancamento.RealizeLancamento(lancamento.LinkAtividade, new List<Lancamento> { lancamento }, jaLogado: true, dataJahTratada: true);
            //            continue;
            //        }

            //        listaBloqueados.Add(lancamento);

            //    }

            //    File.WriteAllText(@"C:\Users\gustavo.moraes\Desktop\Correcao\FaltaLancar.json", JsonConvert.SerializeObject(listaBloqueados));
            //}
            #endregion

            #region MyRegion

            //var listaDeDiasSemAtividades = new List<DateTime>();
            //var listaDeAtividadesBloqueadas = new Dictionary<DateTime, List<ServicoDeLancamento.AtividadeLancada>>();
            //var listaDeExcecoes = new Dictionary<DateTime, List<ServicoDeLancamento.AtividadeLancada>>();

            //using (var servicoDeConfiguracao = new ServicoDeConfiguracao())
            //using (var servicoDeLancamento = new ServicoDeLancamento(servicoDeConfiguracao.ObtenhaConfiguracao()))
            //{
            //    servicoDeLancamento.RealizeLoginRedmine(servicoDeConfiguracao.ObtenhaConfiguracao().ConfiguracaoRedmine);

            //    foreach (var correcao in correcoes)
            //    {
            //        var atividadesDeUmDia = (List<ServicoDeLancamento.AtividadeLancada>)servicoDeLancamento.ObtenhaAtividadesMinhasAtividadesDeUmDia(correcao.Data);
            //        if (!atividadesDeUmDia.Any())
            //        {
            //            listaDeDiasSemAtividades.Add(correcao.Data);
            //            continue;
            //        }

            //        var bloqueadas = atividadesDeUmDia.Where(x => string.IsNullOrEmpty(x.LinkEditar)).ToList();
            //        listaDeAtividadesBloqueadas.Add(correcao.Data, bloqueadas);

            //        bloqueadas.ForEach(x => atividadesDeUmDia.Remove(x));

            //        if (!atividadesDeUmDia.Any())
            //        {
            //            continue;
            //        }

            //        if (correcao.HorasTrabalhadas > correcao.HorasRedmine)
            //        {
            //            servicoDeLancamento.AumenteHorasDeUmaAtividade(atividadesDeUmDia.First().LinkEditar, Math.Round(((TimeSpan)correcao.Diferenca).TotalHours, 2));

            //            continue;
            //        }

            //        var deducao = Math.Round(((TimeSpan)correcao.Diferenca).TotalHours, 2);
            //        var atividadeApta = atividadesDeUmDia.FirstOrDefault(x => x.Horas > deducao);
            //        if (atividadeApta == null)
            //        {
            //            listaDeExcecoes.Add(correcao.Data, atividadesDeUmDia);
            //            continue;
            //        }

            //        servicoDeLancamento.ReduzaHorasDeUmaAtividade(atividadeApta.LinkEditar, Math.Round(((TimeSpan)correcao.Diferenca).TotalHours, 2));
            //    }
            //}

            //File.WriteAllText(@"C:\Users\gustavo.moraes\Desktop\Correcao\DiasSemAtividade.json", JsonConvert.SerializeObject(listaDeDiasSemAtividades));
            //File.WriteAllText(@"C:\Users\gustavo.moraes\Desktop\Correcao\AtividadesBloqueadas.json", JsonConvert.SerializeObject(listaDeAtividadesBloqueadas));
            //File.WriteAllText(@"C:\Users\gustavo.moraes\Desktop\Correcao\Excecoes.json", JsonConvert.SerializeObject(listaDeExcecoes));

            #endregion
        }

        private IEnumerable<dynamic> ObtenhaCorrecoes(IEnumerable<string> linhas)
        {
            var novasLinhas = linhas.Where(x => !string.IsNullOrEmpty(x.Split(',')[4])).ToList();

            return novasLinhas.Select(linha =>
                {
                    var campos = linha.Split(',');
                    return new
                    {
                        Data = MakeDateTime(campos[0].ToString()),
                        HorasTrabalhadas = campos[1].ToString().ObtenhaTimeSpan(),
                        HorasRedmine = campos[2].ToString().ObtenhaTimeSpan(),
                        Diferenca = campos[3].ToString().ObtenhaTimeSpan()
                    };
                })
                .OrderBy(x => x.Data)
                .ToList();
        }


        private DateTime MakeDateTime(string valor)
        {
            var splitted = valor.Split('/');
            return new DateTime(Convert.ToInt32(splitted[2]), Convert.ToInt32(splitted[0]), Convert.ToInt32(splitted[1]));
        }
    }
}
