using GSAutoTimeEntriesWebApi.Objetos;
using GSAutoTimeEntries.Servicos;
using GSAutoTimeEntries.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GSAutoTimeEntries.UI
{
    public static class GerenciadorDeForms
    {
        private static Dictionary<Type, Form> ControladorDeInstancias
        {
            get => _controladorDeInstancias ??
                (_controladorDeInstancias = new Dictionary<Type, Form>
                {
                        { typeof(frmPrincipal), null },
                        { typeof(frmLancamentoCorretivo), null },
                        { typeof(frmLancamentoDiario), null },
                        { typeof(frmProgress), null },
                        { typeof(frmPopupDiario), null },
                        { typeof(frmLancamentoProvisorio), null }
                });

            set => _controladorDeInstancias = value;
        }
        private static Dictionary<Type, Form> _controladorDeInstancias { get; set; }

        public static Form Crie<T>()
            where T : Form
        {
            if(ControladorDeInstancias[typeof(T)] == null)
            {
                var instancia = Activator.CreateInstance<T>();
                (ControladorDeInstancias[typeof(T)] = instancia).Show();

                return instancia as Form;
            }
            
            return null;
        }

        public static Form CrieFormPrincipal()
        {
            var instancia = ControladorDeInstancias[typeof(frmPrincipal)] = new frmPrincipal();
            instancia.Show();

            return instancia;
        }

        public static Form Crie<T>(object[] parametros, bool focar = false, Form invokerOuParent = null, bool exibir = true)
            where T : Form
        {
            if (ControladorDeInstancias[typeof(T)] == null)
            {
                Form instancia = null;
                //(invokerOuParent ?? CrieFormPrincipal()).Invoke(
                //    (MethodInvoker)delegate 
                //    {
                        instancia = (Form)Activator.CreateInstance(typeof(T), parametros);
                        ControladorDeInstancias[typeof(T)] = instancia;

                        if(exibir) instancia.Show();

                        if (focar) instancia.Focar();
                    //});

                return instancia;
            }

            return null;
        }

        public static void Consuma()
        {
            var frmPrincipal = (frmPrincipal)ControladorDeInstancias[typeof(frmPrincipal)];
            frmPrincipal.RestaureJanela();
        }

        public static T Obtenha<T>()
            where T : Form
        {
            if (ControladorDeInstancias[typeof(T)] != null)
            {
                return ControladorDeInstancias[typeof(T)] as T;
            }

            return null;
        }

        public static void Apague<T>()
            where T : Form
        {
            if (ControladorDeInstancias[typeof(T)] != null)
            {
                var form = ControladorDeInstancias[typeof(T)];
                form.Invoke((MethodInvoker)delegate
                {
                    form.Dispose();
                });

                ControladorDeInstancias[typeof(T)] = null;
            }
        }
    }
}
