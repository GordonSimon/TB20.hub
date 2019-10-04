using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace letEmp_KF
{
    interface CueWeek { void Select(string emp_id); }

    public partial class ucMatch : UserControl
    {
        public int RcdID { get; set; }


        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        public ucMatch()
        {
            InitializeComponent();
        }

        
        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        private string get_str(DataRow row, string colname)
        {
            return row[colname].Equals(DBNull.Value) ? "" : (string)row[colname];
        }


        private void show_timebook(string emp_id, DataRow row)
        {
            RcdID = (int)row["ID"];

            tbxTab.Text = emp_id;

            lblID.Text = RcdID.ToString();
            lblID.Tag = RcdID;

            // 1, 2, 3, 4, 5
            tbxEmpId.Text = get_str(row, "EmpId");
            tbxFirst.Text = get_str(row, "First Name");
            tbxLast.Text = get_str(row, "Last Name");
            
            chkArchive.Checked = (bool)row["Archive"];
        }


        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        public bool IsKey(string emp_id)
        {
            return emp_id.Equals(tbxEmpId.Text);
        }


        public void Clear()
        {
            tbxTab.BackColor = Color.FromArgb(255, 255, 192);
            pnlUI.BackColor = Color.FromArgb(255, 255, 192);

        }


        public bool Choose(string emp_id)
        {
            if (emp_id.Equals(tbxEmpId.Text))
            {
                tbxTab.BackColor = Color.PaleGreen;
                pnlUI.BackColor = Color.PaleGreen;

                return true;
            }

            return false;
        }


        public bool Match(string last_name, string first_name, string emp_id)
        {
            bool m = false;

            if (emp_id.Equals(tbxEmpId.Text)) m = true;

            if (last_name.Equals(tbxLast.Text)
                && first_name.Equals(tbxFirst.Text)) m = true;

            if (m)
            {
                tbxTab.BackColor = Color.PaleGreen;
                pnlUI.BackColor = Color.PaleGreen;

                return true;
            }

            return false;
        }


        public void LoadRow(DataRow row)
        {
            string emp_id = get_str(row, "EmpId");
           
            show_timebook(emp_id, row);
            
        }

        private void tbxTab_Click(object sender, EventArgs e)
        {
            ((CueWeek)this.ParentForm).Select(this.tbxEmpId.Text);
        }

        private void groupBox1_Click(object sender, EventArgs e)
        {
            ((CueWeek)this.ParentForm).Select(this.tbxEmpId.Text);
        }
    }
}
