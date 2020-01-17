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

        Dictionary<string, string> Atividades { get; set; }

        public GSMultiTextBox(Lancamento lancamento, Dictionary<string, string> atividades)
        {
            InitializeComponent();

            Atividades = atividades;

            cbLinkAtividade.Items.AddRange(Atividades.Keys.Cast<object>().ToArray());

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
                if (value == 0)
                {
                    return;
                }

                lblHoras.Text = $"{value:F1}";

                var frmPopup = GerenciadorDeForms.Obtenha<frmPopupDiario>();
                if (frmPopup == null) return;

                var totalHoras = frmPopup.TotalDeHoras;
                const int totalWidth = 132;

                pnlHoras.Width = Convert.ToInt32(totalWidth * value / totalHoras);
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
                    ((GSMultiTextBox) controle).btnAdicionar.Visible = false;
                    ((GSMultiTextBox) controle).btnRemover.Visible = true;
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

            ((GSMultiTextBox) painel.Controls[index - 1]).Horas += Horas;

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

        private void CbLinkAtividade_Click(object sender, EventArgs e)
        {
            //const int tempSize = 272;

            //cbLinkAtividade.Width = tempSize;
        }

        private void CbLinkAtividade_Leave(object sender, EventArgs e)
        {
            //const int originalSize = 166;

            //cbLinkAtividade.Width = originalSize;
        }

        private void CbLinkAtividade_DropDown(object sender, EventArgs e)
        {
            const int tempLocation = 6;
            const int tempSize = 500;
            const int dropTempSize = 700;

            cbLinkAtividade.Location = new Point(tempLocation, cbLinkAtividade.Location.Y);
            cbLinkAtividade.Width = tempSize;
            cbLinkAtividade.DropDownWidth = dropTempSize;
        }

        private void CbLinkAtividade_DropDownClosed(object sender, EventArgs e)
        {
            const int originalLocation = 272;
            const int originalSize = 166;
            const int dropOriginalSize = 400;

            cbLinkAtividade.Location = new Point(originalLocation, cbLinkAtividade.Location.Y);
            cbLinkAtividade.Width = originalSize;
            cbLinkAtividade.DropDownWidth = dropOriginalSize;
        }

        private void CbLinkAtividade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string)cbLinkAtividade.SelectedItem == ">>> Entrar com link manualmente <<<")
            {
                cbLinkAtividade.Visible = false;
                txtLink.Visible = true;
                txtLink.BringToFront();

                btnVoltarComboLink.Visible = true;

                PodeProsseguir = false;
                return;
            }

            if (cbLinkAtividade.SelectedItem == null) return;

            LinkAtividade = Atividades[cbLinkAtividade.SelectedItem?.ToString()];
            PodeProsseguir = true;
        }

        private void BtnVoltarComboLink_Click(object sender, EventArgs e)
        {
            txtLink.Visible = false;
            btnVoltarComboLink.Visible = false;

            cbLinkAtividade.Visible = true;
            cbLinkAtividade.SelectedItem = null;
        }

        private void TxtLink_Enter(object sender, EventArgs e)
        {
            txtLink.Width = 400;
        }

        private void TxtLink_Leave(object sender, EventArgs e)
        {
            txtLink.Width = 140;
        }
    }
}
