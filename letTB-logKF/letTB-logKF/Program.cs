using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using System.Diagnostics;
using System.Runtime.InteropServices;  // for DllImport


//GS181202 - add Other category to allow both Office & { Master, Deckhand } employee duty

namespace letTB_logKF
{
    static class Program
    {
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string procName = Process.GetCurrentProcess().ProcessName;
            Process[] processes = Process.GetProcessesByName(procName);

            if (processes.Length > 1)
            {
                MessageBox.Show("Program is open : please click the icon on the taskbar !");
                SetForegroundWindow(processes[0].MainWindowHandle);
                return;
            }

            Application.Run(new frmMain());
        }
    }
}
