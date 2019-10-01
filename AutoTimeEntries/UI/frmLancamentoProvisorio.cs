using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GSAutoTimeEntries.Properties;
using GSAutoTimeEntries.Servicos;
using GSAutoTimeEntriesWebApi.Objetos;
using MetroFramework.Forms;

namespace GSAutoTimeEntries.UI
{
    public partial class frmLancamentoProvisorio : MetroForm
    {
        public frmLancamentoProvisorio()
        {
            Lancamentos = new Dictionary<string, List<Lancamento>>();
            InitializeComponent();
        }

        public Dictionary<string, List<Lancamento>> Lancamentos { get; set; }

        private void Button1_Click(object sender, EventArgs e)
        {
            var dataInicio = dtpDataInicio.Value;
            var dataFim = dtpDataFim.Value;

            while (dataInicio < dataFim)
            {
                if (dataInicio.DayOfWeek != DayOfWeek.Saturday &&
                    dataInicio.DayOfWeek != DayOfWeek.Sunday)
                {
                    var data = dataInicio.ToString("dd/MM/yyyy");
                    if (!dataGridView1.Rows.OfType<DataGridViewRow>().ToList()
                        .Any(x => x.Cells[0].Value.ToString().Equals(data)))
                    {
                        dataGridView1.Rows.Add(data);
                    }
                }

                dataInicio = dataInicio.AddDays(1);
            }
        }

        private void DataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == 3)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Resources.detalhar.Width;
                var h = Resources.detalhar.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Resources.detalhar, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private object[] CrieFrmPopup(string data, string atividade)
        {
            return new object[]
            {
                new Lancamento
                {
                    Data = data,
                    Batidas = new List<TimeSpan>
                    {
                        new TimeSpan(8, 0, 0),
                        new TimeSpan(12, 0, 0),
                        new TimeSpan(14, 0, 0),
                        new TimeSpan(18, 0, 0)
                    },
                    LinkAtividade = atividade
                },
                new Dictionary<string, string>()
            };
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView) sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                senderGrid.Columns[e.ColumnIndex] == colunaEditar &&
                e.RowIndex >= 0)
            {
                var data = senderGrid[0, e.RowIndex].Value.ToString();
                var valorGrid = (senderGrid[2, e.RowIndex].Value ?? string.Empty).ToString();
                if (valorGrid == "Ok")
                {
                    Lancamentos[data].Clear();
                }

                GerenciadorDeForms.Crie<frmPopupDiario>(CrieFrmPopup(data, string.Empty));
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            ((MethodInvoker) delegate
            {
                using (var servicoDeConfig = new ServicoDeConfiguracao())
                using (var servicoDeLancamento = new ServicoDeLancamento(servicoDeConfig.ObtenhaConfiguracao()))
                {
                    GerenciadorDeProgresso.Crie();
                    GerenciadorDeProgresso.AtualizeProgressBar(10, "Iniciando!");
                    servicoDeLancamento.RealizeLancamentoMultiplo(Lancamentos.SelectMany(x => x.Value).ToList());
                    GerenciadorDeProgresso.AtualizeProgressBar(100, "Finalizado!");
                    GerenciadorDeProgresso.Apague();
                }
            }).Invoke();
        }

        private void FrmLancamentoProvisorio_FormClosed(object sender, FormClosedEventArgs e)
        {
            GerenciadorDeForms.Apague<frmLancamentoProvisorio>();
        }

        public void CheckButtonLancamento()
        {
            if (Lancamentos.SelectMany(x => x.Value).ToList().Count == 0 ||
                dataGridView1.Rows.OfType<DataGridViewRow>().Any(x => x.Cells[2].Value?.ToString() != "Ok"))
            {
                button2.Enabled = false;
                return;
            }

            button2.Enabled = true;
        }

        private void DataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                var selectedRows = dataGridView1.SelectedCells.OfType<DataGridViewCell>()
                                                .Select(x => dataGridView1.Rows[x.RowIndex]);
                foreach (var selectedRow in selectedRows)
                {
                    var data = selectedRow.Cells[0].Value.ToString();
                    if (Lancamentos.ContainsKey(data))
                    {
                        Lancamentos[data].Clear();
                    }

                    dataGridView1.Rows.Remove(selectedRow);
                }
            }

            CheckButtonLancamento();
        }

        private void FrmLancamentoProvisorio_Load(object sender, EventArgs e)
        {
            CheckButtonLancamento();
        }
    }
}
