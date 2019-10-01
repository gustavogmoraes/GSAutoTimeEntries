using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GSAutoTimeEntries.Utils;
using GSAutoTimeEntriesWebApi.Objetos;
using MetroFramework.Controls;

namespace GSAutoTimeEntries.UI.UserControls
{
    public partial class GSMultiTextBox : UserControl
    {

        List<string> Atividades { get; set; }

        public GSMultiTextBox(Lancamento lancamento, List<string> atividades)
        {
            InitializeComponent();

            Atividades = atividades;

            Horas = lancamento.Horas;
            LinkAtividade = lancamento.LinkAtividade;
            Atividade = lancamento.TipoAtividade;

            ValideAtividade(LinkAtividade);

        }

        public double Horas
        {
            get => Convert.ToDouble(lblHoras.Text);

            set
            {
                lblHoras.Text = $"{value:F1}";

                var frmPopup = GerenciadorDeForms.Obtenha<frmPopupDiario>();
                if (frmPopup != null)
                {
                    var totalHoras = frmPopup.TotalDeHoras;
                    var totalWidth = 225;

                    pnlHoras.Width = Convert.ToInt32(totalWidth * value / totalHoras);
                }
            }
        }

        public void Recarregue(FlowLayoutPanel painel)
        {
            if (painel.Controls.Count == 1)
            {
                var unicaBox = painel.Controls.Find("GSMultiTextBox", false).FirstOrDefault() as GSMultiTextBox;

                unicaBox.btnAdicionar.Visible = true;
                unicaBox.btnRemover.Visible = false;
            }
            else
            {
                foreach (var controle in painel.Controls)
                {
                    (controle as GSMultiTextBox).btnAdicionar.Visible = false;
                    (controle as GSMultiTextBox).btnRemover.Visible = true;
                }

                var ultimaBox = painel.Controls.Find("GSMultiTextBox", false).Last() as GSMultiTextBox;

                ultimaBox.btnAdicionar.Visible = true;
                ultimaBox.btnRemover.Visible = true;
            }
        }

        public string LinkAtividade { get; set; }

        public string Atividade { get; set; }

        private bool ToggleAdd { get; set; }

        private void BtnAdicionar_Click_1(object sender, EventArgs e)
        {
            if (ToggleAdd)
            {
                Efetive();
                return;
            }

            ToggleAdd = !ToggleAdd;
            btnAdicionar.Location = new Point(707, 1);
            txtInputBox.Visible = true;
            txtInputBox.Focus();
            PodeProsseguir = false;
        }

        private void BtnRemover_Click_1(object sender, EventArgs e)
        {
            var painel = Parent as FlowLayoutPanel;

            var index = painel.Controls.IndexOf(this);

            // Caso seja o primeiro, soma com o debaixo
            if (index == 0) index = 2;

            (painel.Controls[index - 1] as GSMultiTextBox).Horas += Horas;

            painel.Controls.Remove(this);

            Recarregue(painel);
        }

        private void Efetive()
        {
            // Troca vírgula por ponto
            txtInputBox.Text = txtInputBox.Text.Replace(',', '.');

            var horas = Convert.ToDouble(txtInputBox.Text);

            if(horas < 0 || // Negativo
               horas >= Horas || // Negativo pro próximo
               Horas - horas < 0.25) // Menos de 1/4 de hora  
            {
                txtInputBox.ForeColor = Color.Red;

                return;
            }

            txtInputBox.Visible = false;
            ToggleAdd = !ToggleAdd;
            btnAdicionar.Location = new Point(676, 1);
            Horas -= horas;

            var painel = Parent as FlowLayoutPanel;
            painel.Controls.Add(new GSMultiTextBox(new Lancamento { Horas = horas}, Atividades));

            Recarregue(painel);
        }

        private void TxtInputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (new[] { Keys.Return, Keys.Enter }.Contains(e.KeyCode))
                Efetive();
        }

        private void TxtLink_TextChanged(object sender, EventArgs e)
        {
            if (ValideAtividade(txtLink.Text))
            {
                LinkAtividade = txtLink.Text;
                txtLink.ForeColor = Color.Black;

                PodeProsseguir = true;

                return;
            }

            txtLink.UseCustomForeColor = true;
            txtLink.ForeColor = Color.Red;
            PodeProsseguir = false;
        }

        private bool _podeProsseguir;
        private bool PodeProsseguir
        {
            get => _podeProsseguir;
            set
            {
                _podeProsseguir = value;
                var frmPopup = GerenciadorDeForms.Obtenha<frmPopupDiario>();
                frmPopup.btnEfetuarLancamento.Enabled = _podeProsseguir;
            }
        }

        private bool ValideAtividade(string link)
        {
            try
            {
                return (link.Contains(@"srv-redmine") &&
                        link.Contains(@"issues") &&
                        link.Substring(link.LastIndexOf('/') + 1, 5).IsNumeric());
            }
            catch
            {
                return false;
            }
            
        }

        private void CbAtividade_SelectedIndexChanged(object sender, EventArgs e)
        {
            Atividade = (string)cbAtividade.SelectedItem;
        }
    }
}
