using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Diagnostics;

using mdlAnnal;


namespace letTimebook
{
    public partial class frmMain : Form
    {
        const string DBO = "dbo.";

        static long m1;
        static long m2;
        static long m3;
        static long m4;

        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public frmMain()
        {
            InitializeComponent();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void cmdLoad_Click(object sender, EventArgs e)
        {
            DataTable dt = dacTimebook.GetDT(DBO, "Office");
            dgvTimebook.DataSource = dt;
            lblCountTimebook.Text = dt.Rows.Count.ToString();

            dt = dacEmployee.GetDT(DBO, "Office");
            dgvEmployee.DataSource = dt;
            lblCountEmployee.Text = dt.Rows.Count.ToString();

            dt = dacToff.GetDT(DBO);
            dgvToff.DataSource = dt;
            lblCountToff.Text = dt.Rows.Count.ToString();


        }


        private void cmdMemory_Click(object sender, EventArgs e)
        {
            long memory = GC.GetTotalMemory(true);
            //MessageBox.Show(memory.ToString());

            Process cp = Process.GetCurrentProcess();

            long c1 = memory / 1024 / 1024;
            long c2 = cp.PrivateMemorySize64 / 1024 / 1024;
            long c3 = cp.PeakVirtualMemorySize64 / 1024 / 1024;
            long c4 = cp.WorkingSet64 / 1024 / 1024;

            string msg = string.Format("Memory Usage :\n Used : {0}/{1}\n Peak : {2}/{3}\n Private : {4}/{5}\n Working : {6}/{7}", c1, m1, c2, m2, c3, m3, c4, m4);
            MessageBox.Show(msg);

            m1 = c1;
            m2 = c2;
            m3 = c3;
            m4 = c4;

        }
    }
}
