using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using mdlAllyKE;


namespace viewAllyKE
{
    public partial class frmEmployee : Form
    {
        //private DataSet _ds;

        private ucNewEmp _uc_emp = null;


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public string info()
        {
            int w = this.Size.Width;
            int h = this.Size.Height;

            Rectangle r = Screen.GetWorkingArea(this);


            return string.Format("{0}x{1}/({2}, {3})", w, h, r.Width, r.Height);
        }

        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public frmEmployee()
        {
            InitializeComponent();

            _uc_emp = new ucNewEmp();

            //frm.Controls.Add(uc_emp);
            //frm.ShowDialog();

            _uc_emp.ModeReady();
            //_uc_emp.ReadOnly();
            //_uc_emp.EditForm();


            pnlEmp.Controls.Add(_uc_emp);

            dgvEmployee.Hide();
            crud_ready();

        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void crud_ready()
        {
            cmdSave.Visible = false;
            cmdCancel.Visible = true;
            cmdNew.Visible = true;

            _uc_emp.CancelAddEmp();

            cmdCancel.Text = "Done";
        }


        private void crud_edit()
        {
            cmdSave.Visible = true;
            cmdCancel.Visible = true;
            cmdNew.Visible = false;

            cmdCancel.Text = "Cancel";
        }


        private void crud_new()
        {
            cmdSave.Visible = true;
            cmdCancel.Visible = true;
            cmdNew.Visible = false;

            _uc_emp.AddEmp();

            cmdCancel.Text = "Cancel";
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void frmEmployee_Load(object sender, EventArgs e)
        {

            //DataSet ds = dacEmployees.GetDS();            
            //DataTable dt = dacCache.GetEmployee();

            DataTable dtEmp = dacEmployees.GetDT();
            var q = from tEmp in dtEmp.AsEnumerable()
                    //where !(bool)tEmp["Archive"] && !tEmp["Duty"].Equals(DBNull.Value) && ((string)tEmp["Duty"] == "Master" || (string)tEmp["Duty"] == "Deckhand")
                    //orderby tEmp["EmpID"]
                    orderby (string)tEmp["Last Name"]
                    select new
                    {
                        EmpID = (string)tEmp["EmpID"],
                        EmpName = (tEmp["Last Name"] == System.DBNull.Value ? "N/A" : string.Format("{0}, {1}", ((string)tEmp["Last Name"]).ToUpper(), (string)tEmp["First Name"]))
                    };


                
            DataTable dt = toDataTable(q.ToList());

            //_ds = ds;
            //_ds = new DataSet();
            //_ds.Tables.Add(dt);

            int count_all = 0;
            int count_show = 0;
            foreach (DataRow row in dt.Rows)
            {                
                count_all++;
                
                //bool found = dacGang.IsKey(row["EmpId"].ToString());
                //bool found = qryGang.IsKey(row["EmpId"].ToString());
                //if (found) continue;

                count_show++;
                //string emp_name = string.Format("{0} {1}", row["First Name"].ToString(), row["Last Name"].ToString());
                string emp_name = (string)row["EmpName"];

                CheckListBoxItem ni = new CheckListBoxItem();
                ni.Tag = row["EmpId"];
                ni.Text = emp_name;

                clbItems.Items.Add(ni, false);

                //clbItems.Items.Add(ni,
                //    (((bool)row["Active"]) ? CheckState.Checked : CheckState.Unchecked));
            }

            tbxCount.Text = string.Format("{0} of {1}", count_show, count_all);
            this.Text += " : " + info();

            //clbItems.Refresh();
        }


        private void clbItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            ucNewEmp uc_emp = (ucNewEmp)(pnlEmp.Controls[0]);
           
            CheckListBoxItem bi = (CheckListBoxItem)(clbItems.SelectedItem);
            string emp_id = bi.Tag.ToString();

            uc_emp.ViewEmp(emp_id);
            crud_edit();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void cmdSave_Click(object sender, EventArgs e)
        {
            //DataSet ds = dacGang.GetDS();
            //DataTable dt = dacCache.GetGang();
         
            DateTime tod = DateTime.Now;

            string emp_id = _uc_emp.gEmpId;

            DataSet ds = dacEmployees.GetKey(emp_id);
            if (ds.Tables[0].Rows.Count != 1)
                MessageBox.Show(" Warning : Number of Employee rows returned is not 1 !");

            DataRow row = ds.Tables[0].Rows[0];

            row.BeginEdit();
            row["First Name"] = _uc_emp.gFName;
            row["Last Name"] = _uc_emp.gLName;
            row["Duty"] = (_uc_emp.gDuty.Length == 0 ? null : _uc_emp.gDuty);
            row["Employment"] = (_uc_emp.gEmployment.Length == 0 ? null : _uc_emp.gEmployment);
            row["Archive"] = _uc_emp.gArchiveBool;

            row["Master"] = _uc_emp.gMasterBool;
            row["Master Certification"] = (_uc_emp.gMasterCert.Length == 0 ? null : _uc_emp.gMasterCert);
            row["Master Classification"] = (_uc_emp.gMasterfication.Length == 0 ? null : _uc_emp.gMasterfication);            
            row["Master Expire Date"] = DBNull.Value; //_uc_emp.gMasterDate;
            if (_uc_emp.gMasterDate > DateTimePicker.MinimumDateTime) row["Master Expire Date"] = _uc_emp.gMasterDate;

            row["Marine Emergency Duties"] = _uc_emp.gMEDBool;
            row["MED Certificate"] = (_uc_emp.gMEDCert.Length == 0 ? null : _uc_emp.gMEDCert);
            row["Marine Med Exp Date"] = DBNull.Value; //_uc_emp.gMEDDate;
            if (_uc_emp.gMEDDate > DateTimePicker.MinimumDateTime) row["Marine Med Exp Date"] = _uc_emp.gMEDDate;

            row["Marine Medical"] = _uc_emp.gMedicalBool;
            row["Marine Medical Certificate Date"] = DBNull.Value; //_uc_emp.gMedicalDate;
            if (_uc_emp.gMedicalDate > DateTimePicker.MinimumDateTime) row["Marine Medical Certificate Date"] = _uc_emp.gMedicalDate;


            row.EndEdit();


            dacEmployees.SaveData(ds.GetChanges());
            dacCache.RefreshEmployee();
            crud_ready();


            //foreach (CheckListBoxItem ni in clbItems.CheckedItems)
            //{
            //    //System.Windows.Forms.MessageBox.Show(ni.Tag.ToString());

            //    DataSet eds = dacEmployees.GetKey(ni.Tag.ToString());

            //    //DataRow row = ds.Tables[0].NewRow();
            //    DataRow row = dt.NewRow();

            //    string fname = eds.Tables[0].Rows[0]["First Name"].ToString();
            //    string lname = eds.Tables[0].Rows[0]["Last Name"].ToString().ToUpper();

            //    row["EmpId"] = eds.Tables[0].Rows[0]["EmpId"];
            //    row["EmpName"] = string.Format("{0}, {1}", lname, fname);
            //    row["HomePhone"] = eds.Tables[0].Rows[0]["Main Phone"];
            //    row["CellPhone"] = eds.Tables[0].Rows[0]["Cell Phone"];

            //    row["Duty"] = eds.Tables[0].Rows[0]["Duty"];
            //    row["Master"] = eds.Tables[0].Rows[0]["Master"];
            //    row["Employment"] = eds.Tables[0].Rows[0]["Employment"];                   

            //    row["Active"] = true;
            //    //row["CreateDate"] = tod;
            //    //row["UpdateDate"] = tod;
            //    //row["UserAudit"] = "<system>";

            //    //ds.Tables[0].Rows.Add(row);
            //    dt.Rows.Add(row);
            //}
            //dacGang.SaveData(ds.GetChanges());
            //this.Close();
        }


        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Button cmd = (Button)sender;

            if (cmd.Text.Equals("Done")) this.Close();

            crud_ready();
        }


        private void cmdNew_Click(object sender, EventArgs e)
        {
            crud_new();
        }

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
        private void cmdGrid_Click(object sender, EventArgs e)
        {
            dgvEmployee.DataSource = dacEmployees.GetDT();
            
            dgvEmployee.Dock = DockStyle.Fill;

            dgvEmployee.Show();

            this.WindowState = FormWindowState.Maximized;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void dgvEmployee_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            string emp_id;
            DataGridViewRow vrow;

            if (dgv.SelectedRows.Count != 0)
            {
                vrow = dgv.SelectedRows[0];

                emp_id = vrow.Cells["EmpId"].Value.ToString();
                _uc_emp.ViewEmp(emp_id);
                crud_edit();
            }

            dgv.Hide();
            this.WindowState = FormWindowState.Normal;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public DataTable toDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
    }
}
