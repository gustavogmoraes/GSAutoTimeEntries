using GSAutoTimeEntriesWebApi.Objetos;
using GSAutoTimeEntries.Servicos;
using GSAutoTimeEntries.UI.UserControls;
using GSAutoTimeEntries.Utils;
using MetroFramework.Controls;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GSAutoTimeEntries.UI
{
    public partial class frmPopupDiario : MetroForm
    {
        private string DataLancamento { get; set; }

        public double TotalDeHoras { get; set; }

        private Lancamento _lancamento { get; set; }

        private bool Limpando { get; set; }

        private void Inicialize(Lancamento lancamento)
        {
            Limpando = true;
            flpBatidas.Controls.Clear();
            flpLancamentos.Controls.Clear();
            Limpando = false;

            _lancamento = lancamento;
            TotalDeHoras = lancamento.Horas;
            DataLancamento = lancamento.Data.ConvertaParaDataPtBr();
            lblOque.Text = string.Format(lblOque.Text, DataLancamento);
            lblTotalHoras.Text = TotalDeHoras.ToString();

            foreach (var batida in _lancamento.Batidas)
            {
                var textBox = new MetroTextBox
                {
                    Size = new Size(43, 23),
                    FontSize = MetroFramework.MetroTextBoxSize.Medium,
                    Text = batida.ToString().Remove(batida.ToString().Length - 3, 3)
                };
                textBox.Leave += Trate;
                textBox.KeyDown += (sender, e) =>
                {
                    if (new[] { Keys.Return, Keys.Enter }.Contains(e.KeyCode))
                    {
                        Trate(sender, e);
                    }
                };

                flpBatidas.Controls.Add(textBox);
            }

            flpLancamentos.Controls.Add(new GSMultiTextBox(lancamento, Atividades));
        }

        public void Trate(object sender, object e)
        {
            if(!Limpando)
            {
                var regex = @"^(?:[01]\d|2[0123]):(?:[012345]\d)$";
                if (((MetroTextBox) sender).Text.Length > 6 || !Regex.IsMatch((sender as MetroTextBox).Text, regex, RegexOptions.None))
                {
                    MessageBox.Show(@"Formato inválido para a batida, o formato correto é no padrão hh:mm");
                    ((MetroTextBox) sender).Text = string.Empty;

                    return;
                }

                RecalculeTotalDeHoras();
            }
        }

        public frmPopupDiario()
        {
            InitializeComponent();
        }

        private Dictionary<string, string> Atividades { get; set; }

        public frmPopupDiario(Lancamento lancamento, Dictionary<string, string> atividades)
        {
            Atividades = atividades;

            InitializeComponent();

            Inicialize(lancamento);
        }

        private void RecalculeTotalDeHoras()
        {
            _lancamento.Batidas.Clear();
            foreach(var textBox in flpBatidas.Controls)
            {
                _lancamento.Batidas.Add(ObtenhaBatida(((MetroTextBox) textBox).Text));
            }

            _lancamento.CalculeHoras();

            Inicialize(_lancamento);
        }

        private TimeSpan ObtenhaBatida(string batida)
        {
            if (string.IsNullOrEmpty(batida))
            {
                return TimeSpan.Zero;
            }

            var splitted = batida.Split(':');

            return new TimeSpan(Convert.ToInt32(splitted[0]), Convert.ToInt32(splitted[1]), 0);
        }

        private void FrmPopupDiario_Load(object sender, EventArgs e)
        {
            var larguraTela = Screen.PrimaryScreen.WorkingArea.Width;
            var alturaTela = Screen.PrimaryScreen.WorkingArea.Height;

            var larguraForm = Width;
            var alturaForm = Height;

            Location = new Point(larguraTela - larguraForm, alturaTela - alturaForm);
            Opacity = 100;

            //WindowState = FormWindowState.Normal;
            //Activate();
            Focus();
        }

        private string ObtenhaMensagemConfirmacaoDispensar(string data) =>
            $"Você tem certeza que deseja dispensar esse lançamento?\n" +
            $"O dia {DataLancamento} ficará marcado como lançado e o próximo PopUp voltará a acorrrer somente amanhã.\n" +
            $"Confirma?";

        private void BtnNaoLancar_Click(object sender, EventArgs e)
        {
            var dialogResult = 
                MessageBox.Show(
                    ObtenhaMensagemConfirmacaoDispensar(DataLancamento),
                    "Confirmação",
                    MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                ServicoDeLancamentoDiario.DispenseLancamento(_lancamento);
                Dispose();
            }
        }

        private void BtnEfetuarLancamento_Click(object sender, EventArgs e)
        {
            var lancamentos = ObtenhaLancamentosEmTela();

            GerenciadorDeForms.Apague<frmPopupDiario>();

            ServicoDeLancamentoDiario.EfetueLancamentos(lancamentos);
        }

        private Lancamento LeiaLancamento(GSMultiTextBox multiTextBox) 
        {
            return new Lancamento
            {
                Batidas = _lancamento.Batidas,
                Data = DataLancamento.ParaDateTime(),
                Horas = multiTextBox.Horas,
                LinkAtividade = multiTextBox.LinkAtividade,
                TipoAtividade = multiTextBox.Atividade//Atividades[multiTextBox.Atividade]
            };
        }
            

        private List<Lancamento> ObtenhaLancamentosEmTela()
        {
            var listaDeLancamentos = new List<Lancamento>();
            foreach (var multiBox in flpLancamentos.Controls)
            {
                if(multiBox.GetType() == typeof(GSMultiTextBox))
                {
                    listaDeLancamentos.Add(LeiaLancamento(multiBox as GSMultiTextBox));
                }
            }

            return listaDeLancamentos;
        }

        private void MetroButton1_Click(object sender, EventArgs e)
        {
            var textBox = new MetroTextBox
            {
                Size = new Size(43, 23),
                FontSize = MetroFramework.MetroTextBoxSize.Medium,
                Text = string.Empty
            };

            textBox.Leave += Trate;
            textBox.KeyDown += (sender1, e1) =>
            {
                if (new[] { Keys.Return, Keys.Enter }.Contains(e1.KeyCode))
                {
                    Trate(sender1, e1);
                }
            };

            flpBatidas.Controls.Add(textBox);
        }

        private void MetroButton2_Click(object sender, EventArgs e)
        {
            ((MetroTextBox) flpBatidas.Controls.OfType<Control>().Last()).Text = "00:00";
            Trate(flpBatidas.Controls.OfType<Control>().Last(), e);

            flpBatidas.Controls.Remove(flpBatidas.Controls.OfType<Control>().Last());
        }
    }
}
