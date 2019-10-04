using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


// GS180502 - Disable single-click on Salary box (and hide save as part of that)
// GS180907 - Handle Incomplete Employee Data
// GS180907 - Debug Stop
// GS180907 - Add GapLib
// GS180907 - Modify Version using GapLib


namespace letStaff
{
    static class Program
    {
        //^~(:b*//).*your_search_term

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmStaff());
        }
    }
}
