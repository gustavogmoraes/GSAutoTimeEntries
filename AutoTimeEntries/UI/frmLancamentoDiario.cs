using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IWshRuntimeLibrary;
using GSAutoTimeEntriesWebApi.Objetos;
using GSAutoTimeEntries.Servicos;
using GSAutoTimeEntries.Utils;
using MetroFramework.Forms;

namespace GSAutoTimeEntries.UI
{
    public partial class frmLancamentoDiario : MetroForm
    {
        public frmLancamentoDiario()
        {
            InitializeComponent();
        }

        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void CbFormaLembrete_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbFormaLembrete.SelectedText)
            {
                case "Horário fixo":
                    lblHorario.Visible = true;
                    dtpHorario.Visible = true;
                    break;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            using (var servicoDeConfiguracao = new ServicoDeConfiguracao())
            {
                if(Configuracao == null) _configuracao = new ConfiguraoLancamentoDiario();

                Configuracao.Habilitar = toggleHabilitar.Checked;
                Configuracao.HorarioDoLembrete = new TimeSpan(dtpHorario.Value.Hour, dtpHorario.Value.Minute, dtpHorario.Value.Second);

                var config = servicoDeConfiguracao.ObtenhaConfiguracao();
                config.ConfiguraoLancamentoDiario = Configuracao;

                servicoDeConfiguracao.SalveConfiguracao(config);
            }
        }

        private void FrmLancamentoDiario_Load(object sender, EventArgs e)
        {
            if (Configuracao != null)
            {
                toggleHabilitar.Checked = Configuracao.Habilitar;
                cbFormaLembrete.SelectedIndex = (int)Configuracao.FormaDoLembrete;
                dtpHorario.Value = Configuracao.HorarioDoLembrete.ParaDateTime();

                return;
            }

            toggleHabilitar.Checked = false;
            cbFormaLembrete.SelectedIndex = 0;
            dtpHorario.Value = DateTime.Now;
        }

        private ConfiguraoLancamentoDiario _configuracao { get; set; }

        public ConfiguraoLancamentoDiario Configuracao => _configuracao ?? (_configuracao = new ServicoDeConfiguracao().ObtenhaConfiguracao().ConfiguraoLancamentoDiario);

        private void FrmLancamentoDiario_FormClosed(object sender, FormClosedEventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                GerenciadorDeForms.Apague<frmLancamentoDiario>();
                GerenciadorDeForms.Obtenha<frmPrincipal>().AlterneBotaoDiario();
            });
        }
    }
}
