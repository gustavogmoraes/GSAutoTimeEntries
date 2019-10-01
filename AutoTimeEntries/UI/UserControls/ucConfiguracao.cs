using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GSAutoTimeEntries.Utils;
using System.Threading;
using System.Threading.Tasks;
using VisualEffects;
using VisualEffects.Animations.Effects;
using VisualEffects.Easing;
using GSAutoTimeEntries.Servicos;
using GSAutoTimeEntriesWebApi.Objetos;

namespace GSAutoTimeEntries.UI.UserControls
{
    public partial class ucConfiguracao : UserControl
    {
        public bool Toggle { get; set; }

        public Configuracao Configuracoes
        {
            get
            {
                using (var servicoDeConfiguracao = new ServicoDeConfiguracao())
                {
                    return servicoDeConfiguracao.ObtenhaConfiguracao();
                }
            }
        }

        public ucConfiguracao()
        {
            InitializeComponent();

            // Tentar deixar a animação mais lisa
            // Property Double Buffered também está marcada como true
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            DefinaConfiguracoes(Configuracoes);
        }

        public void Animacao()
        {
            if (Toggle)
            {
                var animacao = Animator.Animate(this, new XLocationEffect(), EasingFunctions.QuintEaseOut, 801, 1000, 50);

                Task.Run(() =>
                {
                    while (animacao.ElapsedMilliseconds < 250)
                    {
                        Thread.Sleep(50);
                    }

                    Invoke((MethodInvoker)delegate 
                    {
                        label1.Text = "<<";
                    });
                });
            }
            else
            {
                var animacao = this.Animate(new XLocationEffect(), EasingFunctions.QuintEaseOut, 30, 1000, 50);

                Task.Run(() =>
                {
                    while (animacao.ElapsedMilliseconds < 250)
                    {
                        Thread.Sleep(50);
                    }

                    Invoke((MethodInvoker)delegate
                    {
                        label1.Text = ">>";
                    });
                });
            }

            Toggle = !Toggle;
        }

        private void AlterneMostrarConfiguracao()
        {
            Animacao();

            if (label1.Text == ">>")
            {
                SalveConfiguracao();
            }

            if (label1.Text == "<<")
            {
                (Parent as frmPrincipal).DesabiliteBotoes();
            }
            else
            {
                (Parent as frmPrincipal).InicieBotoes();
            }
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            AlterneMostrarConfiguracao();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            AlterneMostrarConfiguracao();
        }

        private void txtLinkLoginSharepoint_Enter(object sender, EventArgs e)
        {
            txtLinkSharepointSub.AlterneEstilo();
        }

        private void txtLinkLoginSharepoint_Leave(object sender, EventArgs e)
        {
            txtLinkSharepointSub.AlterneEstilo();
        }

        private void txtUsuarioSharepoint_Enter(object sender, EventArgs e)
        {
            txtUsuarioSharepointSub.AlterneEstilo();
        }

        private void txtUsuarioSharepoint_Leave(object sender, EventArgs e)
        {
            txtUsuarioSharepointSub.AlterneEstilo();
        }

        private void txtSenhaSharepoint_Enter(object sender, EventArgs e)
        {
            txtSenhaSharepointSub.AlterneEstilo();
        }

        private void txtSenhaSharepoint_Leave(object sender, EventArgs e)
        {
            txtSenhaSharepointSub.AlterneEstilo();
        }

        private void txtLinkLoginRedmine_Enter(object sender, EventArgs e)
        {
            txtLinkLoginRedmineSub.AlterneEstilo();
        }

        private void txtLinkLoginRedmine_Leave(object sender, EventArgs e)
        {
            txtLinkLoginRedmineSub.AlterneEstilo();
        }

        private void txtUsuarioRedmine_Enter(object sender, EventArgs e)
        {
            txtUsuarioRedmineSub.AlterneEstilo();
        }

        private void txtUsuarioRedmine_Leave(object sender, EventArgs e)
        {
            txtUsuarioRedmineSub.AlterneEstilo();
        }

        private void txtSenhaRedmine_Enter(object sender, EventArgs e)
        {
            txtSenhaRedmineSub.AlterneEstilo();
        }

        private void txtSenhaRedmine_Leave(object sender, EventArgs e)
        {
            txtSenhaRedmineSub.AlterneEstilo();
        }

        private void SalveConfiguracao()
        {
            if (!string.IsNullOrEmpty(txtUsuarioRedmine.Text) && !string.IsNullOrEmpty(txtUsuarioSharepoint.Text))
            {
                using (var servicoDeConfiguracao = new ServicoDeConfiguracao())
                {
                    servicoDeConfiguracao.SalveConfiguracao(
                        new Configuracao
                        {
                            NomeUtilizador = txtNomeUtilizador.Text.Trim(),
                            OcultarNavegador = cbOcultarNavegador.Checked,
                            ConfiguracaoRedmine = new ConfiguracaoRedmine
                            {
                                LinkLogin = txtLinkLoginRedmine.Text,
                                Usuario = txtUsuarioRedmine.Text,
                                Senha = txtSenhaRedmine.Text,
                            },
                            ConfiguracaoAutotendimento = new ConfiguracaoAutotendimento
                            {
                                LinkLogin = txtLinkLoginSharepoint.Text,
                                Usuario = txtUsuarioSharepoint.Text,
                                Senha = txtSenhaSharepoint.Text
                            },
                            ConfiguraoLancamentoDiario = servicoDeConfiguracao.ObtenhaConfiguracao() != null 
                                                       ? servicoDeConfiguracao.ObtenhaConfiguracao().ConfiguraoLancamentoDiario
                                                       : null
                        });
                }
            }
        }

        private void DefinaConfiguracoes(Configuracao configuracao)
        {
            if (configuracao != null)
            {
                txtLinkLoginRedmine.Text = configuracao.ConfiguracaoRedmine.LinkLogin;
                txtUsuarioRedmine.Text = configuracao.ConfiguracaoRedmine.Usuario;
                txtSenhaRedmine.Text = configuracao.ConfiguracaoRedmine.Senha;

                txtLinkLoginSharepoint.Text = configuracao.ConfiguracaoAutotendimento.LinkLogin;
                txtUsuarioSharepoint.Text = configuracao.ConfiguracaoAutotendimento.Usuario;
                txtSenhaSharepoint.Text = configuracao.ConfiguracaoAutotendimento.Senha;

                cbOcultarNavegador.Checked = configuracao.OcultarNavegador;
                txtNomeUtilizador.Text = configuracao.NomeUtilizador;
            }
        }

        private void btnApagarConfig_Click(object sender, EventArgs e)
        {
            using (var servicoDeConfiguracao = new ServicoDeConfiguracao())
            {
                servicoDeConfiguracao.ApagueConfiguracao();
                DefinaConfiguracoes(Configuracoes);
                LimpeCamposConfiguracao();
            }
        }

        private void LimpeCamposConfiguracao()
        {
            txtLinkLoginRedmine.Text = @"http://srv-redmine/redmine/login";
            txtUsuarioRedmine.Clear();
            txtSenhaRedmine.Clear();

            txtUsuarioSharepoint.Clear();
            txtSenhaSharepoint.Clear();
            txtLinkLoginSharepoint.Text = @"https://login.lg.com.br/autenticacao/produtos/saaa/Principal2.aspx?c=lg";

            cbOcultarNavegador.Checked = false;
            txtNomeUtilizador.Clear();
        }

        private void txtNomeUtilizador_Enter(object sender, EventArgs e)
        {
            txtNomeUtilizadorSub.AlterneEstilo();
        }

        private void txtNomeUtilizador_Leave(object sender, EventArgs e)
        {
            txtNomeUtilizadorSub.AlterneEstilo();
        }
    }
}
