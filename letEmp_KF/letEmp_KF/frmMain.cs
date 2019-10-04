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
    public partial class frmMain : Form
    {
        private DataTable _pd_dt { get; set; }
        private DataTable _tb_dt { get; set; }
        private DataTable _tbj_dt { get; set; }
        private DataTable _pdj_dt { get; set; }

        //private BindingSource _bs = null;


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

        public frmMain()
        {
            InitializeComponent();

            // Set the control style to double buffer.
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            //this.SetStyle(ControlStyles.SupportsTransparentBackColor, false);
            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            pnlTimebook.Visible = true;
            pnlPayroll.Visible = false;
            flpPaydirt.Visible = false;

            this.WindowState = FormWindowState.Maximized;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void init_load()
        {
            //string sql = string.Format("BookDate = '{0}'", d);
            //string sql = "";            
            //_dt.DefaultView.RowFilter = sql;

            dacEmployees.LoadDT();
            _tb_dt = dacEmployees.DT;

            _pd_dt = dacPaydirt.GetDT();
            //_tb_dt = dacEmployees.GetDT();

            //_tb_dt.PrimaryKey = new DataColumn[] { _tb_dt.Columns["Id"] };


            //_r_dt = qryPaydirt.qInner(_tb_dt, _pd_dt);
            //_r_dt = qryPaydirt.qImport(_tb_dt, _pd_dt);
            _tbj_dt = qryPaydirt.qOuter_timebook(_tb_dt, _pd_dt);
            _pdj_dt = qryPaydirt.qOuter_paydirt(_pd_dt, _tb_dt, true); // all records
            //dgvR.DataSource = _r_dt;

            filter_all();


            //DataView dv = new DataView(_r_dt);
            //clbFilter.DataSource = dv;
            //clbFilter.DisplayMember = "Last Name";
        }


        private void mark_timebook()
        {
            foreach (DataGridViewRow row in dgvEmployees.Rows)
            {
                if (row.Cells["strEmployee_number"].Value.Equals(DBNull.Value) || row.Cells["strEmployee_number"].Value.Equals(string.Empty))
                {
                    row.DefaultCellStyle.ForeColor = Color.Red;
                }
            }
        }


        private void mark_paydirt()
        {
            foreach (DataGridViewRow row in dgvPaydirt.Rows)
            {
                if (row.Cells["EmpId"].Value.Equals(DBNull.Value) || row.Cells["EmpId"].Value.Equals(string.Empty))
                {
                    row.DefaultCellStyle.ForeColor = Color.Green;
                }
            }
        }

        private void show_detail(bool all)
        {
            flpPaydirt.Controls.Clear();

            foreach (liFILTER li in clbFilter.CheckedItems)
            {
                DataRow row = _tbj_dt.Rows.Find(li.Tag);

                ucEmp uc = new ucEmp();
                uc.LoadRow(row);
                flpPaydirt.Controls.Add(uc);
            }

            if (clbFilter.CheckedItems.Count > 0) return;
            if (!all) return;

            foreach (DataRow row in _tbj_dt.Rows)
            {
                //if (row["strEmployee_number"].Equals(DBNull.Value)) continue;

                ucEmp uc = new ucEmp();
                uc.LoadRow(row);
                flpPaydirt.Controls.Add(uc);
            }


            //DataTable boats = qryTimebook.qBoats(_dt, d);
            //foreach (DataRow row in boats.Rows)
            //foreach (DataRow tb_row in _pd_dt.Rows)
            //foreach (DataRow row in _r_dt.Rows)
            //{
            //    //if (row["strEmployee_number"].Equals(DBNull.Value)) continue;

            //    ucPaydirt uc = new ucPaydirt();
            //    uc.LoadRow(row);
            //    flpPaydirt.Controls.Add(uc);
            //}
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void show_panels()
        {
            init_load();

            dacEmployees.MoveColumn(_tbj_dt, "ID");

            //init_logs();
            //init_months();

            //DataTable dtp = dacPaydirt.GetDT();
            //DataTable dte = dacEmployees.GetDT();
            //DataTable dtp = _pdj_dt;
            //DataTable dte = _tb_dt;
            //DataTable dtr = _tbj_dt;

            //dgvPaydirt.DataSource = _pdj_dt;
            //dgvTimebook.DataSource = _tb_dt;
            //dgvEmployees.DataSource = _tbj_dt;

            //BindingSource bsp = new BindingSource(_pdj_dt, _pdj_dt.TableName);
            //bnvPaydirt.BindingSource = bsp;

            //BindingSource bse = new BindingSource(_tbj_dt, _tbj_dt.TableName);
            //bnvEmployees.BindingSource = bse;

            //BindingSource bsr = new BindingSource(_tb_dt, _tb_dt.TableName);
            //bnvFilter.BindingSource = bsr;

            BindingSource bsp = new BindingSource(_pdj_dt, _pdj_dt.TableName);            
            BindingSource bsr = new BindingSource(_tb_dt, _tb_dt.TableName);
            BindingSource bse = new BindingSource(_tbj_dt, _tbj_dt.TableName);

            dgvPaydirt.DataSource = bsp;
            dgvTimebook.DataSource = bsr;
            dgvEmployees.DataSource = bse;
            
            bnvPaydirt.BindingSource = bsp;
            bnvEmployees.BindingSource = bse;

            bnvFilter.BindingSource = bsr;

            mark_paydirt();
            mark_timebook();
        }


        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Text += " [" + Gap.AssemblyVersion + "]";

            show_panels();

            cmdTimebook.Enabled = false;
            tbxFilter.Select();
        }


        private void dgvPaydirt_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            flpPaydirt.Controls.Clear();
            foreach (DataGridViewRow vrow in dgv.SelectedRows)
            {
                DataRow row = ((DataRowView)vrow.DataBoundItem).Row;

                ucPaydirt uc = new ucPaydirt();
                uc.LoadRow(row);
                flpPaydirt.Controls.Add(uc);
            }
        }


        private void flpPaydirt_MouseEnter(object sender, EventArgs e)
        {
            flpPaydirt.Focus();
            //flpPaydirt.Select();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void filter_timebook()
        {
            string sql = "false";


            foreach (liFILTER li in clbFilter.CheckedItems)
                sql = sql + string.Format(" or [EmpId] = '{0}'", li.Tag);

            _tbj_dt.DefaultView.RowFilter = string.Empty;
            if (clbFilter.CheckedItems.Count == 0) return;

            _tbj_dt.DefaultView.RowFilter = sql;
        }


        private void filter_paydirt()
        {
            string sql = "false";


            foreach (liFILTER li in clbFilter.CheckedItems)
                sql = sql + string.Format(" or [strEmployee_number] = '{0}'", li.Tag);


            _pd_dt.DefaultView.RowFilter = string.Empty;
            if (clbFilter.CheckedItems.Count == 0) return;

            _pd_dt.DefaultView.RowFilter = sql;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void filter_all()
        {
            clbFilter.Items.Clear();

            foreach (DataRow row in _tb_dt.Rows)
                //clbFilter.Items.Add(new liFILTER((string)row["EmpId"], (string)row["First Name"], (string)row["Last Name"]));
                clbFilter.Items.Add(new liFILTER(get_str(row, "EmpId"), get_str(row, "First Name"), get_str(row, "Last Name")));

            _tb_dt.DefaultView.RowFilter = string.Empty;

            clbFilter.Sorted = false;
        }


        private void filter_like(string v)
        {
            string sql = string.Format("[Last Name] LIKE '%{0}%' or [First Name] LIKE '%{1}%' or EmpId LIKE '%{2}%'", v, v, v);
            _tb_dt.DefaultView.RowFilter = sql;

            clbFilter.Items.Clear();
            foreach (DataRow row in _tb_dt.DefaultView.ToTable().Rows)
                clbFilter.Items.Add(new liFILTER((string)row["EmpId"], (string)row["First Name"], (string)row["Last Name"]));

        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void tbxFilter_TextChanged(object sender, EventArgs e)
        {
            TextBox tbx = (TextBox)sender;

            if (chkApply.Checked)
                filter_like(tbx.Text);
        }


        private void chkApply_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cbx = (CheckBox)sender;

            if (!cbx.Checked)
                filter_all();
            else
                filter_like(tbxFilter.Text);

            tbxFilter.Select();
        }


        private void tbxFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chkApply.Checked = true;
            }
        }


        private void clbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        private void clbFilter_SelectedValueChanged(object sender, EventArgs e)
        {
            filter_timebook();
            mark_timebook();

            filter_paydirt();
            mark_paydirt();

            show_detail(false);
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void nova_employee()
        {
            frmEdit frm = new frmEdit();
            frm.StartPosition = FormStartPosition.CenterScreen;

            frm.NOVA_ = true;
            frm.ShowDialog();

            show_panels();
        }


        private void edit_employee()
        {
            DataGridViewRow dgvr = dgvEmployees.CurrentRow;
            DataRow row = ((DataRowView)dgvr.DataBoundItem).Row;

            frmEdit frm = new frmEdit();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.LoadRow(row);
            frm.NOVA_ = false;
            frm.ShowDialog();

            DataRow dac = dacEmployees.DT.Rows.Find(row["EmpId"]);
            foreach (DataColumn c in dac.Table.Columns)
                row[c.ColumnName] = dac[c.ColumnName];


            //DataRow find = _tbj_dt.Rows.Find(row["EmpId"]);
            //foreach (DataGridViewCell cell in dgvr.Cells)
            //    cell.Value = 

            //for (int i = 0; i < dgvr.Cells.Count; i++)
            //    dgvr.Cells[i].Value = row.ItemArray[i];

            //show_panels();                        
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            edit_employee();
        }

        private void dgvEmployees_DoubleClick(object sender, EventArgs e)
        {
            edit_employee();
        }


        private void cmdNew_Click(object sender, EventArgs e)
        {
            nova_employee();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void cmdTimebook_Click(object sender, EventArgs e)
        {
            cmdTimebook.Enabled = false;
            cmdPayroll.Enabled = true;
            cmdDetail.Enabled = true;
            cmdImport.Enabled = true;

            cmdEdit.Enabled = true;
            cmdNew.Enabled = true;


            pnlTimebook.Visible = true;
            pnlPayroll.Visible = false;
            flpPaydirt.Visible = false;

            mark_timebook();
        }


        private void cmdPayroll_Click(object sender, EventArgs e)
        {
            cmdTimebook.Enabled = true;
            cmdPayroll.Enabled = false;
            cmdDetail.Enabled = true;
            cmdImport.Enabled = true;

            cmdEdit.Enabled = false;
            cmdNew.Enabled = false;

            pnlTimebook.Visible = false;
            pnlPayroll.Visible = true;
            flpPaydirt.Visible = false;

            mark_paydirt();
        }


        private void cmdDetail_Click(object sender, EventArgs e)
        {
            show_detail(true);

            cmdTimebook.Enabled = true;
            cmdPayroll.Enabled = true;
            cmdDetail.Enabled = false;
            cmdImport.Enabled = true;

            cmdEdit.Enabled = false;
            cmdNew.Enabled = false;

            pnlTimebook.Visible = false;
            pnlPayroll.Visible = false;
            flpPaydirt.Visible = true;
        }


        private void cmdImport_Click(object sender, EventArgs e)
        {
            frmImport frm = new frmImport();

            frm.ShowDialog();

            show_panels();

            cmdTimebook.Enabled = true;
            cmdPayroll.Enabled = true;
            cmdDetail.Enabled = true;
            cmdImport.Enabled = false;

            cmdEdit.Enabled = false;
            cmdNew.Enabled = false;

        }


        private void cmdExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }

    /*******************************************************************************************************************\
     *                                                                                                                 *
    \*******************************************************************************************************************/

    class liFILTER
    {
        public object Tag;
        public string Text;
        public override string ToString() { return Text; }

        public liFILTER(string eid, string fn, string ln)
        {
            string EmpName = string.Format("[{0, -6}] ", eid) +
                (ln == null ? "N/A" : string.Format("{0}, {1}", ln.ToUpper(), fn));

            this.Tag = eid;
            this.Text = EmpName;
        }
    }
}
