using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GSAutoTimeEntries.UI.UserControls
{
    public partial class ucBorders : UserControl
    {
        #region Draggable Form

        public const int WM_NCLBUTTONDOWN = 0xA1;

        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void ucBorders_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.ParentForm.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        #endregion

        public ucBorders()
        {
            InitializeComponent();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(MousePosition);
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        //https://stackoverflow.com/questions/7625421/minimize-app-to-system-tray
        private void ImportStatusForm_Resize(object sender, EventArgs e)
        {
            if (ParentForm.WindowState == FormWindowState.Minimized)
            {
                ((frmPrincipal)ParentForm).notifyIcon1.Visible = true;
                ((frmPrincipal)ParentForm).notifyIcon1.ShowBalloonTip(3000);
                ParentForm.ShowInTaskbar = false;
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ParentForm.WindowState = FormWindowState.Normal;
            ParentForm.ShowInTaskbar = true;
            ((frmPrincipal)ParentForm).notifyIcon1.Visible = false;
        }

        private void itemMinimizar_Click(object sender, EventArgs e)
        {
            ParentForm.WindowState = FormWindowState.Minimized;
        }

        private void itemBandeja_Click(object sender, EventArgs e)
        {
            ((frmPrincipal)ParentForm).ColoqueAppNaSystemTray();
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
