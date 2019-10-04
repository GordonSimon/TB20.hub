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
    public partial class ucEmp : UserControl
    {
        public string EMP_ID { get { return tbxEmpId.Text; } }

        //public string EMP_ID { get { return lblID.Text; } }
        
        public string FNAME { get { return tbxFirst.Text; } }
        public string LNAME { get { return tbxLast.Text; } }
        public string DUTY { get { return cbxDuty.Text; } }
        //public string gCell { get { return tbxCell.Text; } }
        //public string gHome { get { return tbxHome.Text; } }
        public string EMPLOYMENT { get { return cbxEmployment.Text; } }
        public string PAYCODE { get { return cbxPayCode.Text; } }
        public bool ARCHIVE_ { get { return chkArchive.Checked; } }


        public bool MASTER_ { get { return chkMaster.Checked; } }
        public string MASTER_CERT { get { return tbxMarineID.Text; } }
        public string MASTERFICATION { get { return cbxMasterFication.Text; } }
        public DateTime MASTER_DATE { get { return dtpMarineExp.Value; } }

        public bool MED_ { get { return chkMED.Checked; } }
        public string MED_CERT { get { return tbxMED.Text; } }
        public DateTime MED_DATE { get { return dtpMEDExp.Value; } }

        public bool MEDICAL_ { get { return chkMedical.Checked; } }
        public DateTime MEDICAL_DATE { get { return dtpMedical.Value; } }


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
        
        public ucEmp()
        {
            InitializeComponent();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public void ModeEdit()
        {
            cbxDuty.DropDownStyle = ComboBoxStyle.DropDown;
            cbxPayCode.DropDownStyle = ComboBoxStyle.DropDown;
            cbxEmployment.DropDownStyle = ComboBoxStyle.DropDown;
            cbxMasterFication.DropDownStyle = ComboBoxStyle.DropDown;

            chkArchive.AutoCheck = true;
            chkMaster.AutoCheck = true;
            chkMED.AutoCheck = true;
            chkMedical.AutoCheck = true;

            pnlNewId.Visible = false;
            pnlUI.Enabled = true;           
        }


        public void ModeRead()
        {
            cbxDuty.DropDownStyle = ComboBoxStyle.Simple;
            cbxPayCode.DropDownStyle = ComboBoxStyle.Simple;
            cbxEmployment.DropDownStyle = ComboBoxStyle.Simple;
            cbxMasterFication.DropDownStyle = ComboBoxStyle.Simple;

            chkArchive.AutoCheck = false;
            chkMaster.AutoCheck = false;
            chkMED.AutoCheck = false;
            chkMedical.AutoCheck = false;

            pnlNewId.Visible = false;
            pnlUI.Enabled = true;           
        }


        public void ModeNova()
        {
            pnlNewId.Visible = true;
            pnlUI.Enabled = false;

            tbxTab.Text = "New";
            tbxNewFirst.Select();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void show_paydirt(string emp_id, string last, string first, string status, string freq,
            string phone, string addr1, string addr2, string city, string prov, string postal, string country,
            string hire, string start, string end)
        {
            tbxFirst.Text = first;
            tbxLast.Text = last;
            tbxTab.Text = emp_id;
            tbxStatus.Text = status;

            tbxPhone.Text = phone;
            tbxAddr1.Text = addr1;
            tbxAddr2.Text = addr2;
            tbxCity.Text = city;
            tbxProv.Text = prov;
            tbxPostal.Text = postal;
            tbxCountry.Text = country;

            tbxFreq.Text = freq;

            tbxHire.Text = hire;
            tbxStart.Text = start;
            tbxEnd.Text = end;
        }

        
        private void show_timebook(string emp_id, DataRow row)
        {
            tbxEmpId.Text = emp_id;

            lblID.Text = row["ID"].ToString();            

            // 1, 2, 3, 4, 5
            cbxDuty.Text = row["Duty"].ToString();
            string employment = "Other";
            if (! row["Employment"].Equals(DBNull.Value))
                employment = row["Employment"].ToString();
            
            //string paycode = cbxDuty.Text;
            string paycode = string.Empty;
            if (! row["DefPayCode"].Equals(DBNull.Value))
                paycode = row["DefPayCode"].ToString();
            cbxPayCode.Text = paycode;

            cbxEmployment.Text = employment;            
            chkArchive.Checked = (bool)row["Archive"];

            // 5, 6, 7, 8
            chkMaster.Checked = (bool)row["Master"];
            cbxMasterFication.Enabled = false;
            if (chkMaster.Checked) cbxMasterFication.Enabled = true;
            cbxMasterFication.Text = row["Master Classification"].ToString();
            tbxMarineID.ReadOnly = true;
            if (chkMaster.Checked) tbxMarineID.ReadOnly = false;
            tbxMarineID.Text = row["Master Certification"].ToString();
            if (row["Master Expire Date"] == DBNull.Value)
            {
                dtpMarineExp.Value = DateTimePicker.MinimumDateTime;
                dtpMarineExp.Format = DateTimePickerFormat.Custom;
                dtpMarineExp.CustomFormat = " ";
                dtpMarineExp.Checked = false;
                dtpMarineExp.Enabled = false;
            }
            else
            {
                //if (row["Master Expire Date"].ToString().Equals(""))
                //{
                //    dtpMarineExp.Format = DateTimePickerFormat.Custom;
                //    dtpMarineExp.CustomFormat = " ";
                //    dtpMarineExp.Checked = false;
                //    dtpMarineExp.Enabled = false;
                //}
                //else
                //{
                dtpMarineExp.Format = DateTimePickerFormat.Short;
                dtpMarineExp.CustomFormat = null;
                dtpMarineExp.Value = (DateTime)(row["Master Expire Date"]);
                dtpMarineExp.Checked = true;
                dtpMarineExp.Enabled = true;
                //}
            }

            // 9, 10, 11
            chkMED.Checked = (bool)row["Marine Emergency Duties"];
            tbxMED.ReadOnly = true;
            if (chkMED.Checked) tbxMED.ReadOnly = false;
            tbxMED.Text = row["MED Certificate"].ToString();
            if (row["Marine Med Exp Date"].Equals(DBNull.Value))
            {
                dtpMEDExp.Value = DateTimePicker.MinimumDateTime;
                dtpMEDExp.Format = DateTimePickerFormat.Custom;
                dtpMEDExp.CustomFormat = " ";
                dtpMEDExp.Checked = false;
                dtpMEDExp.Enabled = false;
            }
            else
            {
                //if (row["Marine Med Exp Date"].ToString().Equals(""))
                //{
                //    dtpMEDExp.Format = DateTimePickerFormat.Custom;
                //    dtpMEDExp.CustomFormat = " ";
                //    dtpMEDExp.Checked = false;
                //    dtpMEDExp.Enabled = false;
                //}
                //else
                //{
                dtpMEDExp.Format = DateTimePickerFormat.Short;
                dtpMEDExp.CustomFormat = null;
                dtpMEDExp.Value = (DateTime)(row["Marine Med Exp Date"]);
                dtpMEDExp.Checked = true;
                dtpMEDExp.Enabled = true;
                //}
            }

            // 12, 13
            chkMedical.Checked = (bool)row["Marine Medical"];
            if (row["Marine Medical Certificate Date"].Equals(DBNull.Value))
            {
                dtpMedical.Value = DateTimePicker.MinimumDateTime;
                dtpMedical.Format = DateTimePickerFormat.Custom;
                dtpMedical.CustomFormat = " ";
                dtpMedical.Checked = false;
                dtpMedical.Enabled = false;
            }
            else
            {
                //if (row["Marine Medical Certificate Date"].ToString().Equals(""))
                //{
                //    dtpMedical.Format = DateTimePickerFormat.Custom;
                //    dtpMedical.CustomFormat = " ";
                //    dtpMedical.Checked = false;
                //    dtpMedical.Enabled = false;
                //}
                //else
                //{
                dtpMedical.Format = DateTimePickerFormat.Short;
                dtpMedical.CustomFormat = null;
                dtpMedical.Value = (DateTime)(row["Marine Medical Certificate Date"]);
                dtpMedical.Checked = true;
                dtpMedical.Enabled = true;
                //}
            }
           

        }


        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        public void LoadRow(DataRow row)
        {
            try
            {
                if (row["strEmployee_number"].Equals(DBNull.Value))
                {
                    string last = get_str(row, "Last Name");
                    string first = get_str(row, "First Name");

                    tbxFirst.Text = first;
                    tbxLast.Text = last;
                }
                else
                {
                    string emp_id = (string)row["strEmployee_number"];
                    string last = (string)row["strEmployee_Lastname"];

                    string first = "";
                    if (row["strEmployee_Middlename"].Equals(DBNull.Value))
                        first = (string)row["strEmployee_Firstname"];
                    else
                        first = string.Format("{0} {1}", (string)row["strEmployee_Firstname"], (string)row["strEmployee_Middlename"]);

                    string status = get_str(row, "strEmployee_Status");
                    string freq = get_str(row, "strEmployee_Frequency");

                    string phone = get_str(row, "strEmployee_Telephone");
                    string addr1 = get_str(row, "strEmployee_Address1");
                    string addr2 = get_str(row, "strEmployee_Address2");
                    string city = (string)row["strEmployee_City"];
                    string prov = (string)row["strEmployee_Province"];
                    string postal = (string)row["strEmployee_Postal"];
                    string country = (string)row["strEmployee_Country"];

                    string hire = ((DateTime)row["dtmEmployee_OriginalHire"]).ToShortDateString();
                    string start = "";
                    string end = "";

                    if (!row["dtmEmployee_Start"].Equals(DBNull.Value))
                        start = ((DateTime)row["dtmEmployee_Start"]).ToShortDateString();

                    if (!row["dtmEmployee_End"].Equals(DBNull.Value))
                        end = ((DateTime)row["dtmEmployee_End"]).ToShortDateString();

                    show_paydirt(emp_id, last, first, status, freq,
                        phone, addr1, addr2, city, prov, postal, country, hire, start, end);
                }

                if (!row["EmpId"].Equals(DBNull.Value))
                {
                    string emp_id = (string)row["EmpId"];
                    show_timebook(emp_id, row);
                                      

                }

            }
            catch (Exception ex)
            {
                errDash.Error("Error (ucEmp.LoadRow) : " + ex.Message);
            }

            //sql += " strEmployee_OrderName";
            //sql += ", strEmployee_Status";
            //sql += ", strEmployee_Telephone";
            //sql += ", strEmployee_number";
            //sql += ", dtmEmployee_OriginalHire";
            //sql += ", strEmployee_Address1";
            //sql += ", strEmployee_Address2";
            //sql += ", strEmployee_City";
            //sql += ", strEmployee_Province";
            //sql += ", strEmployee_Postal";
            //sql += ", strEmployee_Country";
            //sql += ", dtmEmployee_Start";
            //sql += ", dtmEmployee_End";
            //sql += ", strEmployee_Frequency";
            //sql += ", strEmployee_Lastname";
            //sql += ", strEmployee_Firstname";
            //sql += ", strEmployee_Middlename";
            //sql += " FROM " + _tname;
        }


        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        private string calc_new_idV2(string lname, string fname)
        {
            DateTime today = DateTime.Now.Date;

            string id = lname.First().ToString().ToUpper();
            id += fname.First().ToString().ToUpper();

            id += today.ToString("MMdd");
            
            DataSet ds = dacEmployees.GetKey(id);
            if (ds == null) return id;

            DialogResult ok = 
                MessageBox.Show("Warning : This Employee ID already exists.", "Confirm !", MessageBoxButtons.OKCancel);
            if (ok == DialogResult.OK) return id;

            string test_id;
            for (int i = 1; i < 10; i++)
            {
                test_id = id + "-" + i.ToString();
                ds = dacEmployees.GetKey(test_id);
                if (ds == null) return test_id;
            }

            string part_id = id + "-" + fname.ToUpper();
            for (int i = 1; i < 10; i++)
            {
                test_id = part_id + i.ToString();
                ds = dacEmployees.GetKey(test_id);
                if (ds == null) return test_id;
            }

            return id + "-99";
        }


        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        private void tbxNewFirst_TextChanged(object sender, EventArgs e)
        {
            //TextBox tbx = (TextBox)sender;

            //if (tbx.Text.Length == 0)
            //    tbxInitial.Text = string.Empty;
            //else
            //    tbxInitial.Text = tbx.Text.First().ToString().ToUpper();
        }


        private void tbxNewLast_TextChanged(object sender, EventArgs e)
        {
            TextBox tbx = (TextBox)sender;

            if (tbx.Text.Length == 0)
                tbxNewId.Text = string.Empty;
            else
                //tbxNewId.Text = calc_new_idV2(tbx.Text.First().ToString().ToUpper(), tbxInitial.Text);
                tbxNewId.Text = calc_new_idV2(tbx.Text, tbxNewFirst.Text);
        }


        private void tbxNewId_TextChanged(object sender, EventArgs e)
        {
            TextBox tbx = (TextBox)sender;

            cmdCreate.Enabled = false;
            if (tbx.Text.Length != 0
                && tbxNewFirst.Text.Length != 0
                && tbxNewLast.Text.Length != 0)
                //&& tbxInitial.Text.Length != 0)
                cmdCreate.Enabled = true;
        }


        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        private void cmdCreate_Click(object sender, EventArgs e)
        {
            //Button btn = (Button)sender;

            string emp_id = tbxNewId.Text;
            string fname = tbxNewFirst.Text;
            string lname = tbxNewLast.Text;

            DataSet ds = dacEmployees.GetKey(emp_id);
            if (ds != null)
            {
                MessageBox.Show(" Error : This Employee ID exists !");
                return;
            }

            dacEmployees.AddKey(emp_id, fname, lname);

            tbxEmpId.Text = emp_id;
            tbxFirst.Text = fname;
            tbxLast.Text = lname;

            //AddForm();
            //btn.Visible = false;

            ModeRead();
        }

    }
}
