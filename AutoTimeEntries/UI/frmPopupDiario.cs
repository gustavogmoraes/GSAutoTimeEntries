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
            DataLancamento = lancamento.Data;
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

            flpLancamentos.Controls.Add(new GSMultiTextBox(lancamento, Atividades.Keys.ToList()));
        }

        public void Trate(object sender, object e)
        {
            if(!Limpando)
            {
                var regex = @"^(?:[01]\d|2[0123]):(?:[012345]\d)$";
                if ((sender as MetroTextBox).Text.Length > 6 || !Regex.IsMatch((sender as MetroTextBox).Text, regex, RegexOptions.None))
                {
                    MessageBox.Show(@"Formato inválido para a batida, o formato correto é no padrão hh:mm");
                    (sender as MetroTextBox).Text = string.Empty;

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
                _lancamento.Batidas.Add(ObtenhaBatida((textBox as MetroTextBox).Text.ToString()));
            }

            _lancamento.CalculeHoras();

            Inicialize(_lancamento);
        }

        private TimeSpan ObtenhaBatida(string batida)
        {
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
                Dispose();
                //ServicoDeLancamentoDiario.DispenseLancamento(_lancamento);
            }
        }

        private void BtnEfetuarLancamento_Click(object sender, EventArgs e)
        {
            var form = GerenciadorDeForms.Obtenha<frmLancamentoProvisorio>();
            var lancamentos = ObtenhaLancamentosEmTela();
            var row = form.dataGridView1.Rows.OfType<DataGridViewRow>()
                                        .FirstOrDefault(x => x.Cells[0].Value.ToString() == DataLancamento);

            if (form.Lancamentos.ContainsKey(_lancamento.Data))
            {
                form.Lancamentos[_lancamento.Data].Clear();
                form.Lancamentos[_lancamento.Data].AddRange(lancamentos);
            }
            else
            {
                form.Lancamentos.Add(_lancamento.Data, lancamentos);
            }

            row.Cells[2].Value = "Ok";
            row.Cells[1].Value = TotalDeHoras.ToString();
            form.CheckButtonLancamento();

            GerenciadorDeForms.Apague<frmPopupDiario>();

            //ServicoDeLancamentoDiario.EfetueLancamentos(lancamentos);
        }

        private Lancamento LeiaLancamento(GSMultiTextBox multiTextBox) =>
            new Lancamento
            {
                Batidas = _lancamento.Batidas,
                Data = DataLancamento,
                Horas = multiTextBox.Horas,
                LinkAtividade = multiTextBox.LinkAtividade,
                TipoAtividade = multiTextBox.Atividade//Atividades[multiTextBox.Atividade]
            };

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
    }
}
