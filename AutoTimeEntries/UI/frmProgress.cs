using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GSAutoTimeEntries.UI
{
    public partial class frmProgress : Form
    {
        public frmProgress()
        {
            InitializeComponent();
        }

        public void AtualizeProgressBar(int porcentagem, string mensagem)
        {
            Invoke((MethodInvoker)delegate
            {
                progressBar.Value = porcentagem;
                status.Text = mensagem.Trim();
            });
        }

        private void frmProgress_Load(object sender, EventArgs e)
        {
            var larguraTela = Screen.PrimaryScreen.WorkingArea.Width;
            var alturaTela = Screen.PrimaryScreen.WorkingArea.Height;

            var larguraForm = Width;
            var alturaForm = Height;

            Location = new Point(larguraTela - larguraForm, alturaTela - alturaForm);
            Opacity = 100;
        }

        public void ExibaErro(string mensagem)
        {
            Invoke((MethodInvoker)delegate
            {
                progressBar.ForeColor = Color.PaleVioletRed;
                status.Text = mensagem.Trim();
            });
        }
    }
}
