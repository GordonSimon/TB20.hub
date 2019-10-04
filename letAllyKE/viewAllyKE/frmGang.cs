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
    public partial class frmGang : Form
    {
        //private DataSet _ds;

        public frmGang()
        {
            InitializeComponent();

            ucNewEmp uc_emp = new ucNewEmp();
            

            //frm.Controls.Add(uc_emp);
            //frm.ShowDialog();

            uc_emp.ModeReady();
            uc_emp.ReadOnly();
            
            pnlEmp.Controls.Add(uc_emp);

        }


        private void frmGang_Load(object sender, EventArgs e)
        {

            //DataSet ds = dacEmployees.GetDS();            
            //DataTable dt = dacCache.GetEmployee();

            DataTable dt = dacEmployees.GetDT();
            DataView dv = dt.DefaultView;
            dv.Sort = "[Last Name], [First Name]";
            dt = dv.ToTable();

            //_ds = ds;
            //_ds = new DataSet();
            //_ds.Tables.Add(dt);

            int count_all = 0;
            int count_show = 0;
            foreach (DataRow row in dt.Rows)
            {                
                count_all++;
                
                //bool found = dacGang.IsKey(row["EmpId"].ToString());
                bool found = qryGang.IsKeyActive(row["EmpId"].ToString());
                if (found) continue;

                count_show++;
                //string emp_name = string.Format("{0} {1}", row["First Name"].ToString(), row["Last Name"].ToString());
                string emp_name = string.Format("{0}, {1}", ((string)row["Last Name"]).ToUpper(), (string)row["First Name"]);

                CheckListBoxItem ni = new CheckListBoxItem();
                ni.Tag = row["EmpId"];
                ni.Text = emp_name;

                clbItems.Items.Add(ni, false);

                //clbItems.Items.Add(ni,
                //    (((bool)row["Active"]) ? CheckState.Checked : CheckState.Unchecked));
            }

            tbxCount.Text = string.Format("{0} of {1}", count_show, count_all);

            //clbItems.Refresh();
        }


        private void clbItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            ucNewEmp uc_emp = (ucNewEmp)(pnlEmp.Controls[0]);
           
            CheckListBoxItem bi = (CheckListBoxItem)(clbItems.SelectedItem);
            string emp_id = bi.Tag.ToString();

            uc_emp.ViewEmp(emp_id);
        }


        private void cmdSave_Click(object sender, EventArgs e)
        {
            DataSet ds = dacGang.GetDS();
            DataTable dt = dacCache.GetGang();

            DateTime tod = DateTime.Now;

            foreach (CheckListBoxItem ni in clbItems.CheckedItems)
            {
                //System.Windows.Forms.MessageBox.Show(ni.Tag.ToString());

                string emp_id = ni.Tag.ToString();

                DataSet eds = dacEmployees.GetKey(emp_id);

                DataRow found_row = dt.Rows.Find(emp_id);
                if (found_row != null)
                {
                    found_row.BeginEdit();
                    found_row["Active"] = true;
                    found_row.EndEdit();

                    continue;
                }


                //DataRow row = ds.Tables[0].NewRow();
                DataRow row = dt.NewRow();

                string fname = eds.Tables[0].Rows[0]["First Name"].ToString();
                string lname = eds.Tables[0].Rows[0]["Last Name"].ToString().ToUpper();

                row["EmpId"] = eds.Tables[0].Rows[0]["EmpId"];
                row["EmpName"] = string.Format("{0}, {1}", lname, fname);
                row["HomePhone"] = eds.Tables[0].Rows[0]["Main Phone"];
                row["CellPhone"] = eds.Tables[0].Rows[0]["Cell Phone"];

                row["Duty"] = eds.Tables[0].Rows[0]["Duty"];
                row["Master"] = eds.Tables[0].Rows[0]["Master"];
                row["Employment"] = eds.Tables[0].Rows[0]["Employment"];                   

                row["Active"] = true;
                //row["CreateDate"] = tod;
                //row["UpdateDate"] = tod;
                //row["UserAudit"] = "<system>";

                //ds.Tables[0].Rows.Add(row);
                dt.Rows.Add(row);
            }

            //dacGang.SaveData(ds.GetChanges());
            
            //dacGang.SaveData(dt.GetChanges());
            //dt.AcceptChanges();
            //dacCache.SetGang(dt);


            this.Close();
        }


        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
