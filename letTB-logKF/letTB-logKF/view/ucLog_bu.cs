using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace letTB_logKF
{
    public partial class ucLog_bu : UserControl
    {
        public ucLog_bu()
        {
            InitializeComponent();
        }


        public void LoadLog(DataTable dt, DateTime d, string boat)
        {
            int shift = 1;

            string sql = string.Format("BookDate = '{0}' and LogVessel = '{1}' and LogShift = {2}", d, boat, shift);
            DataTable v = (new DataView(dt, sql, null, System.Data.DataViewRowState.CurrentRows)).ToTable();

            dgvLog.DataSource = v;                       
        }

        private void ucLog_Click(object sender, EventArgs e)
        {
            pnlLog.Visible = false;

            dgvLog.Dock = DockStyle.Fill;
            dgvLog.Visible = true;
        }

        private void pnlLog_Click(object sender, EventArgs e)
        {
            pnlLog.Visible = false;

            dgvLog.Dock = DockStyle.Fill;
            dgvLog.Visible = true;

        }
    }
}
