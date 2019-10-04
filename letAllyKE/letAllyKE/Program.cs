using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using System.Diagnostics;
using System.Runtime.InteropServices;

using viewAllyKE;
using viewLogKE;


namespace letAllyKE
{
    static class Program
    {
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string procName = Process.GetCurrentProcess().ProcessName;
            Process[] processes = Process.GetProcessesByName(procName);

            //if (processes.Length > 1)
            //{
            //    MessageBox.Show("Program is open : please click the icon on the taskbar !");
            //    SetForegroundWindow(processes[0].MainWindowHandle);
            //    return;
            //}

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Application.Run(new frmMain());
            //Application.Run(new frmTest());
            //Application.Run(new frmAlly());

            int app = 0;
            if (args.Length > 0) Int32.TryParse(args[0], out app);
  
            switch (app)
            {
                case 1: Application.Run(new frmEmployee()); break;
                case 2: //Application.Run(new letLog(DateTime.Now));
                    MessageBox.Show("Sorry : This feature is not yet available ?");
                    break;

                default: Application.Run(new frmCache(GAPP.Gap.AssemblyVersion)); break;
            }

        }
    }
}
