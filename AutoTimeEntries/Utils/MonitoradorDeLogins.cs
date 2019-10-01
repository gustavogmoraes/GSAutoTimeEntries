using GSAutoTimeEntriesWebApi.Objetos;
using GSAutoTimeEntries.UI;
using Microsoft.Win32;

namespace GSAutoTimeEntries.Utils
{
    public class MonitoradorDeLogins
    {
        public static void OnSessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            switch (e.Reason)
            {
                case SessionSwitchReason.SessionLock | SessionSwitchReason.SessionLogoff:
                    Persistencia.UltimoBloqueio = Persistencia.HorarioAtual;

                    break;

                case SessionSwitchReason.SessionUnlock | SessionSwitchReason.SessionLogon:
                    if (Persistencia.EhPrimeiroDesbloqueioDoDia) GerenciadorDeForms.Crie<frmLancamentoDiario>();
                    Persistencia.UltimoDesbloqueio = Persistencia.HorarioAtual;

                    break;
            }
        }
    }
}
