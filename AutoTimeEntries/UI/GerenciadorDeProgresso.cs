using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GSAutoTimeEntries.UI
{
    
    public class GerenciadorDeProgresso
    {
        public static bool Iniciado { get; set; }

        private const int DELAY = 1000;

        private static frmProgress _formProgresso { get; set; }

        public static int Progresso { get; internal set; }

        public static void Crie()
        {
            GerenciadorDeForms.Obtenha<frmPrincipal>().Invoke(
                (MethodInvoker)delegate 
                {
                    GerenciadorDeForms.Crie<frmProgress>();
                    _formProgresso = GerenciadorDeForms.Obtenha<frmProgress>();
                });

            Iniciado = true;
        }

        public static void AtualizeProgressBar(int porcentagem, string mensagem, int delay = DELAY)
        {
            if (_formProgresso == null) Crie();

            _formProgresso.AtualizeProgressBar(porcentagem, mensagem);
            Progresso = porcentagem;

            Thread.Sleep(delay);
        }

        public static void Apague()
        {
            GerenciadorDeForms.Obtenha<frmPrincipal>().Invoke(
                (MethodInvoker)delegate
                {
                    if (_formProgresso != null)
                    {
                        _formProgresso = null;
                        GerenciadorDeForms.Apague<frmProgress>();
                    }
                    Iniciado = false;
                });
        }

        public static void ExibaErro(string mensagem)
        {
            if (_formProgresso == null) Crie();

            _formProgresso.ExibaErro(mensagem);

            Thread.Sleep(1500);
        }
    }
}
