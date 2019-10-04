using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace letEmp_KF
{
    public partial class frmImport : Form, CueWeek
    {

        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        public void Select(string data)
        {
            make_match(data);
        }


        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        private DataTable _pd_dt { get; set; }
        private DataTable _tb_dt { get; set; }
        private DataTable _tbj_dt { get; set; }
        private DataTable _pdj_dt { get; set; }

        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public frmImport()
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


        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        private void make_match(string emp_id)
        {
            tbxTimebook.Text = emp_id;
            lbMatch.Text = "CHOOSE";
            pnlMatch.BackColor = Color.LightCyan;
            cmdNew.Hide();
            cmdUpdate.Show();

            flpTimebook.Tag = null;
            foreach (ucMatch uc in flpTimebook.Controls)
            {
                if (uc.Choose(emp_id)) { flpTimebook.Tag = uc.RcdID; continue; }
                uc.Clear();
            }
        }


        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/
        
        private ucPaydirt _uc = null;

        private void show_import(DataRow row)
        {
            string emp_id = get_str(row, "strEmployee_number");
            string last = get_str(row, "strEmployee_Lastname");
            string first = get_str(row, "strEmployee_Firstname");

            if (_uc == null)
            {
                _uc = new ucPaydirt();
                _uc.Location = new Point(5, 5);                
                pnlPaydirt.Controls.Add(_uc);
            }

            _uc.LoadRow(row);
            tbxPayroll.Text = emp_id;
            //flpPaydirt.Controls.Add(uc);            

            bool found = false;
            flpTimebook.Controls.Clear();
            foreach (DataRow match in _tb_dt.Rows)
            {
                string match_last = get_str(match, "Last Name");
                if (match_last[0] != last[0]) continue;

                
                //pnlPaydirt.Controls.Add(uc);

                ucMatch ucm = new ucMatch();
                ucm.LoadRow(match);
                bool yes = ucm.Match(last, first, emp_id);
                if (yes)
                {
                    found = true;
                    cmdUpdate.Show();
                    lbMatch.Text = "MATCH";
                    pnlMatch.BackColor = Color.PaleGreen;
                    tbxTimebook.Text = (string)match["EmpId"];

                    flpTimebook.Tag = ucm.RcdID;

                    if (ucm.IsKey(emp_id)) cmdNew.Hide();
                }


                flpTimebook.Controls.Add(ucm);
            }

            if (!found)
            {
                cmdNew.Show();
                cmdUpdate.Hide();
                lbMatch.Text = "NO MATCH !";
                pnlMatch.BackColor = Color.White;
                tbxTimebook.Text = string.Empty;

                flpTimebook.Tag = null;
            }
        }

        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void frmImport_Load(object sender, EventArgs e)
        {
            dacEmployees.LoadDT();
            _tb_dt = dacEmployees.DT;
                
            _pd_dt = dacPaydirt.GetDT();
            //_tb_dt = dacEmployees.GetDT();

            _pdj_dt = qryPaydirt.qOuter_paydirt(_pd_dt, _tb_dt, false);  // no match records

            //dgvPaydirt.DataSource = _pdj_dt;
            BindingSource bsp = new BindingSource(_pdj_dt, _pdj_dt.TableName);
            bnvPaydirt.BindingSource = bsp;
            dgvPaydirt.DataSource = bsp;

            

            DataRow row = _pdj_dt.Rows[0];            
            if (row["EmpId"].Equals(DBNull.Value) || row["EmpId"].Equals(string.Empty))                
                       show_import(row);         
        }

        
        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void dgvPaydirt_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            BindingSource bs = (BindingSource)dgv.DataSource;

            DataRowView drv = (DataRowView)bs.Current;
            DataRow row = drv.Row;

            //DataGridViewSelectedRowCollection c = dgv.SelectedRows;
            //if (c.Count == 0) return;
            //DataRow row = (DataRow)c[0].DataBoundItem;
            

            if (row["EmpId"].Equals(DBNull.Value) || row["EmpId"].Equals(string.Empty))
                show_import(row);         

        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void cmdNew_Click(object sender, EventArgs e)
        {
            BindingSource bs = (BindingSource)dgvPaydirt.DataSource;

            DataRowView drv = (DataRowView)bs.Current;
            DataRow row = drv.Row;

            string emp_id = (string)row["strEmployee_number"];
            string fname = (string)row["strEmployee_Firstname"];
            string lname = (string)row["strEmployee_Lastname"];
           
            dacEmployees.ProcInsert(emp_id, fname, lname);

            MessageBox.Show(Form.ActiveForm, emp_id + ", " + fname + " " + lname, "Import Sucessful", MessageBoxButtons.OK);

            DataTable dt = (DataTable)bs.DataSource;
            dt.Rows.Remove(row);
        }


        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            DialogResult ok = 
                MessageBox.Show("Please ensure this is the correct MATCH for this employee !", "Warning",
                MessageBoxButtons.OKCancel);
            if (ok == DialogResult.Cancel) return;


            BindingSource bs = (BindingSource)dgvPaydirt.DataSource;

            DataRowView drv = (DataRowView)bs.Current;
            DataRow row = drv.Row;

            string emp_id = (string)row["strEmployee_number"];
            string fname = (string)row["strEmployee_Firstname"];
            string lname = (string)row["strEmployee_Lastname"];

            dacEmployees.ProcUpdate((int)flpTimebook.Tag, emp_id, fname, lname);

            MessageBox.Show(Form.ActiveForm, emp_id + ", " + fname + " " + lname,
                "Import Sucessful - Please remember to set the default paycode !", MessageBoxButtons.OK);
            
            DataTable dt = (DataTable)bs.DataSource;
            dt.Rows.Remove(row);
        }
    }
}
