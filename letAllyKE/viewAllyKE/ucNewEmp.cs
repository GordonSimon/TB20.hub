using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using mdlAllyKE;


namespace viewAllyKE
{
    public partial class ucNewEmp : UserControl
    {
        public string gEmpId { get { return tbxEmpId.Text; } }
        public string gFName { get { return tbxFirst.Text; } }
        public string gLName { get { return tbxLast.Text; } }
        public string gDuty { get { return cbxDuty.Text; } }
        //public string gCell { get { return tbxCell.Text; } }
        //public string gHome { get { return tbxHome.Text; } }
        public string gEmployment { get { return cbxEmployment.Text; } }
        public bool gArchiveBool { get { return chkArchive.Checked; } }


        public bool gMasterBool { get { return chkMaster.Checked; } }
        public string gMasterCert { get { return tbxMarineID.Text; } }
        public string gMasterfication { get { return cbxMasterFication.Text; } }
        public DateTime gMasterDate { get { return dtpMarineExp.Value; } }

        public bool gMEDBool { get { return chkMED.Checked; } }
        public string gMEDCert { get { return tbxMED.Text; } }
        public DateTime gMEDDate { get { return dtpMEDExp.Value; } }

        public bool gMedicalBool { get { return chkMedical.Checked; } }
        public DateTime gMedicalDate { get { return dtpMedical.Value; } }
        


        public ucNewEmp()
        {
            InitializeComponent();                   
        }


        public void ModeReady()
        {
            cmdEdit.Visible = false;
            pnlNewId.Visible = false;

            cmdSave.Visible = false;
            cmdCancel.Visible = false;


            dtpMarineExp.Format = DateTimePickerFormat.Custom;
            dtpMarineExp.CustomFormat = " ";
            dtpMarineExp.Checked = false;
            dtpMarineExp.Enabled = false;

            dtpMEDExp.Format = DateTimePickerFormat.Custom;
            dtpMEDExp.CustomFormat = " ";
            dtpMEDExp.Checked = false;
            dtpMEDExp.Enabled = false;

            dtpMedical.Format = DateTimePickerFormat.Custom;
            dtpMedical.CustomFormat = " ";
            dtpMedical.Checked = false;
            dtpMedical.Enabled = false;

            dtpHire.Format = DateTimePickerFormat.Custom;
            dtpHire.CustomFormat = " ";
            dtpHire.Checked = false;
            dtpHire.Enabled = false;     

        }


        public void AddEmp()
        {
            pnlUI.Enabled = false;

            pnlNewId.Visible = true;
            cmdEdit.Enabled = false;
            cmdEdit.Visible = true;

            tbxTab.Text = "New";
            //tbxFirst.Focus();
            //cmdSave.Visible = true;
            //cmdCancel.Visible = true;

            tbxNewFirst.Focus();
        }


        public void CancelAddEmp()
        {
            pnlNewId.Visible = false;
            //pnlUI.Enabled = true;
            pnlUI.Enabled = false;

            tbxTab.Text = string.Empty;

            tbxFirst.Focus();
        }


        private void display_employee(string emp_id, DataRow row)
        {
            tbxTab.Text = emp_id;

            // 1, 2, 3, 4, 5
            tbxEmpId.Text = row["EmpId"].ToString();
            tbxFirst.Text = row["First Name"].ToString();
            tbxLast.Text = row["Last Name"].ToString();
            cbxDuty.Text = row["Duty"].ToString();
            string employment = "Other";
            if (row["Employment"] != null)
                employment = row["Employment"].ToString();
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
                //dtpMarineExp.Value = DateTimePicker.MinimumDateTime;
                dtpMarineExp.Value = DateTime.Now;
                dtpMarineExp.Format = DateTimePickerFormat.Custom;
                dtpMarineExp.CustomFormat = " ";
                dtpMarineExp.Checked = false;
                dtpMarineExp.Enabled = true;
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
            if (row["Marine Med Exp Date"] == DBNull.Value)
            {
                //dtpMEDExp.Value = DateTimePicker.MinimumDateTime;
                dtpMEDExp.Value = DateTime.Now;
                dtpMEDExp.Format = DateTimePickerFormat.Custom;
                dtpMEDExp.CustomFormat = " ";
                dtpMEDExp.Checked = false;
                dtpMEDExp.Enabled = true;
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
            if (row["Marine Medical Certificate Date"] == DBNull.Value)
            {
                //dtpMedical.Value = DateTimePicker.MinimumDateTime;
                dtpMedical.Value = DateTime.Now;
                dtpMedical.Format = DateTimePickerFormat.Custom;
                dtpMedical.CustomFormat = " ";
                dtpMedical.Checked = false;
                dtpMedical.Enabled = true;
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


        public void ViewEmp(string emp_id)
        {
            DataSet ds = dacEmployees.GetKey(emp_id);
           
            pnlUI.Enabled = true;
            DataRow row = ds.Tables[0].Rows[0];
            display_employee(emp_id, row);

            cmdSave.Visible = false;
            cmdCancel.Visible = false;
        }


        public void EditEmp(string emp_id)
        {            
            DataSet ds = dacEmployees.GetKey(emp_id);            

            DataRow row = ds.Tables[0].Rows[0];
            display_employee(emp_id, row);

            cmdSave.Visible = true;
            cmdCancel.Visible = true;
        }



        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public void ReadOnly()
        {
            bool on = true;

            pnlUI.Enabled = false;

            tbxTab.ReadOnly = on;
            tbxEmpId.ReadOnly = on;

            tbxFirst.ReadOnly = on;
            tbxLast.ReadOnly = on;
            //dcbxDuty.ReadOnly = on;

            tbxMarineID.ReadOnly = on;
            tbxComment.ReadOnly = on;

            //chkMaster.Enabled = !on;
            //chkMED1.Enabled = !on;
            //chkMED2.Enabled = !on;

            dtpHire.Enabled = !on;
            dtpMarineExp.Enabled = !on;
            dtpMEDExp.Enabled = !on;
            dtpVerify.Enabled = !on;
            dtpVisit.Enabled = !on;

            tbxEmpId.BackColor = Color.LightYellow;
            tbxFirst.BackColor = Color.LightYellow;
            tbxLast.BackColor = Color.LightYellow;

            tbxMarineID.BackColor = Color.LightYellow;
            tbxComment.BackColor = Color.LightYellow;

        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public void EditForm()
        {
            bool off = false;

            tbxTab.ReadOnly = off;
            tbxEmpId.ReadOnly = off;

            tbxFirst.ReadOnly = off;
            tbxLast.ReadOnly = off;
            //cbxDuty.ReadOnly = off;

            tbxMarineID.ReadOnly = off;
            tbxComment.ReadOnly = off;

            //chkMaster.Enabled = !on;
            //chkMED1.Enabled = !on;
            //chkMED2.Enabled = !on;

            
            dtpMarineExp.Enabled = !off;
            dtpMEDExp.Enabled = !off;
            dtpMedical.Enabled = !off;
            dtpHire.Enabled = !off;
            dtpVerify.Enabled = !off;
            dtpVisit.Enabled = !off;

            tbxEmpId.ResetBackColor();
            tbxFirst.ResetBackColor();
            tbxLast.ResetBackColor();

            tbxMarineID.ResetBackColor();
            tbxComment.ResetBackColor();

        }

        public void AddForm()
        {
            bool off = false;

            tbxTab.ReadOnly = off;
            tbxEmpId.ReadOnly = off;

            tbxFirst.ReadOnly = off;
            tbxLast.ReadOnly = off;
            //cbxDuty.ReadOnly = off;

            tbxMarineID.ReadOnly = off;
            tbxComment.ReadOnly = off;

            //chkMaster.Enabled = !on;
            //chkMED1.Enabled = !on;
            //chkMED2.Enabled = !on;

            dtpHire.Enabled = !off;
            dtpMarineExp.Enabled = !off;
            dtpMEDExp.Enabled = !off;
            dtpVerify.Enabled = !off;
            dtpVisit.Enabled = !off;

            tbxEmpId.ResetBackColor();
            tbxFirst.ResetBackColor();
            tbxLast.ResetBackColor();

            tbxMarineID.ResetBackColor();
            tbxComment.ResetBackColor();


            tbxNewId.Enabled = false;
            pnlNewId.Visible = false;

            dtpMarineExp.Value = DateTimePicker.MinimumDateTime;
            dtpMarineExp.Format = DateTimePickerFormat.Custom;
            dtpMarineExp.CustomFormat = " ";
            dtpMarineExp.Checked = false;
            dtpMarineExp.Enabled = false;

            dtpMEDExp.Value = DateTimePicker.MinimumDateTime;
            dtpMEDExp.Format = DateTimePickerFormat.Custom;
            dtpMEDExp.CustomFormat = " ";
            dtpMEDExp.Checked = false;
            dtpMEDExp.Enabled = false;

            dtpMedical.Value = DateTimePicker.MinimumDateTime;
            dtpMedical.Format = DateTimePickerFormat.Custom;
            dtpMedical.CustomFormat = " ";
            dtpMedical.Checked = false;
            dtpMedical.Enabled = false;

            dtpHire.Value = DateTimePicker.MinimumDateTime;
            dtpHire.Format = DateTimePickerFormat.Custom;
            dtpHire.CustomFormat = " ";
            dtpHire.Checked = false;
            dtpHire.Enabled = false;

            pnlUI.Enabled = true;


            //pnlUI.Enabled = false;
            tbxFirst.Focus();
        }

        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void master_section(bool on)
        {
            if (on)
            {
                tbxMarineID.ReadOnly = false;
                cbxMasterFication.Enabled = true;

                dtpMarineExp.Format = DateTimePickerFormat.Short;
                dtpMarineExp.CustomFormat = null;                
                dtpMarineExp.Enabled = true;
                dtpMarineExp.Checked = true;

                if (dtpMarineExp.Value == DateTimePicker.MinimumDateTime)
                    dtpMarineExp.Value = DateTime.Now.Date;

                //tbxMarineID.ResetBackColor();
            }
            else
            {
                tbxMarineID.ResetText();
                cbxMasterFication.ResetText();
                cbxMasterFication.Enabled = false;
                
                dtpMarineExp.ResetText();
                tbxMarineID.ReadOnly = true;

                dtpMarineExp.Value = DateTimePicker.MinimumDateTime;
                dtpMarineExp.Format = DateTimePickerFormat.Custom;
                dtpMarineExp.CustomFormat = " ";
                dtpMarineExp.Enabled = false;
                dtpMarineExp.Checked = false;

                //tbxMarineID.BackColor = Color.LightYellow;
            }
        }


        private void MED_section(bool on)
        {
            if (on)
            {
                tbxMED.ReadOnly = false;

                dtpMEDExp.Format = DateTimePickerFormat.Short;
                dtpMEDExp.CustomFormat = null;
                dtpMEDExp.Enabled = true;
                dtpMEDExp.Checked = true;

                if (dtpMEDExp.Value == DateTimePicker.MinimumDateTime)
                    dtpMEDExp.Value = DateTime.Now.Date;

                //tbxMED.ResetBackColor();
            }
            else
            {
                tbxMED.ResetText();
                dtpMEDExp.ResetText();

                tbxMED.ReadOnly = true;

                dtpMEDExp.Value = DateTimePicker.MinimumDateTime;
                dtpMEDExp.Format = DateTimePickerFormat.Custom;
                dtpMEDExp.CustomFormat = " ";
                dtpMEDExp.Enabled = false;
                dtpMEDExp.Checked = false;

                //tbxMED.BackColor = Color.LightYellow;
            }
        }


        private void medical_section(bool on)
        {
            if (on)
            {
                dtpMedical.Enabled = true;

                dtpMedical.Format = DateTimePickerFormat.Short;
                dtpMedical.CustomFormat = null;
                dtpMedical.Checked = true;

                if (dtpMedical.Value == DateTimePicker.MinimumDateTime)
                    dtpMedical.Value = DateTime.Now.Date;

            }
            else
            {
                dtpMedical.ResetText();

                dtpMedical.Value = DateTimePicker.MinimumDateTime;
                dtpMedical.Format = DateTimePickerFormat.Custom;
                dtpMedical.CustomFormat = " ";
                dtpMedical.Enabled = false;
                dtpMedical.Checked = false;
            }
        }

    
        private void chkMaster_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            
            master_section(chk.Checked);
        }


        private void chkMED_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            MED_section(chk.Checked);
        }


        private void chkMedical_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            medical_section(chk.Checked);
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void cmdSave_Click(object sender, EventArgs e)
        {
            List<string> gang = new List<string>();

            gang.Add(tbxLast.Text.ElementAt(0) + "9");

            gang.Add(tbxFirst.Text);
            gang.Add(tbxLast.Text);
            //gang.Add("604-111-2222");
            //gang.Add("778-344-9814");

            this.ParentForm.Tag = gang;

            this.ParentForm.Close();
        }


        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();            
        }


        private void cmdEdit_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            string emp_id = tbxNewId.Text;
            string fname = tbxNewFirst.Text;
            string lname = tbxNewLast.Text;

            DataSet ds = dacEmployees.GetKey(emp_id);
            if (ds != null)
            {
                MessageBox.Show(" Warning : This Employee ID exists !");
                return;
            }

            dacEmployees.AddKey(emp_id);

            tbxEmpId.Text = emp_id;
            tbxFirst.Text = fname;
            tbxLast.Text = lname;

            AddForm();
            btn.Visible = false;

        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void cmsDates_Click(object sender, EventArgs e)
        {
            ContextMenuStrip cms = (ContextMenuStrip)sender;
            DateTimePicker dtp = (DateTimePicker)cms.SourceControl;

            switch (dtp.Name)
            {
                case "dtpMarineExp" :
                    dtpMarineExp.Value = DateTimePicker.MinimumDateTime;
                    dtpMarineExp.Format = DateTimePickerFormat.Custom;
                    dtpMarineExp.CustomFormat = " ";
                    break;

                case "dtpMEDExp" :
                    dtpMEDExp.Value = DateTimePicker.MinimumDateTime;
                    dtpMEDExp.Format = DateTimePickerFormat.Custom;
                    dtpMEDExp.CustomFormat = " ";
                    break;

                case "dtpMedical" :
                    dtpMedical.Value = DateTimePicker.MinimumDateTime;
                    dtpMedical.Format = DateTimePickerFormat.Custom;
                    dtpMedical.CustomFormat = " ";                    
                    break;
            }

        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private string calc_new_id(string lname, string fname)
        {
            string test_id;
            string part_id = lname.First().ToString().ToUpper();

            for (int i = 1; i < 10; i++)
            {
                test_id = part_id + i.ToString();
                DataSet ds = dacEmployees.GetKey(test_id);
                if (ds == null) return test_id;
            }

            part_id = part_id + fname.First().ToString().ToUpper();
            for (int i = 1; i < 10; i++)
            {
                test_id = part_id + i.ToString();
                DataSet ds = dacEmployees.GetKey(test_id);
                if (ds == null) return test_id;
            }

            return part_id + "99";                
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void tbxNewFirst_TextChanged(object sender, EventArgs e)
        {
            TextBox tbx = (TextBox)sender;

            if (tbx.Text.Length == 0)
                tbxInitial.Text = string.Empty;
            else
                tbxInitial.Text = tbx.Text.First().ToString().ToUpper();
        }


        private void tbxNewLast_TextChanged(object sender, EventArgs e)
        {
            TextBox tbx = (TextBox)sender;

            if (tbx.Text.Length == 0)
                tbxNewId.Text = string.Empty;
            else
                tbxNewId.Text = calc_new_id(tbx.Text.First().ToString().ToUpper(), tbxInitial.Text);
        }


        private void tbxNewId_TextChanged(object sender, EventArgs e)
        {
            TextBox tbx = (TextBox)sender;

            cmdEdit.Enabled = false;
            if (tbx.Text.Length != 0
                && tbxNewFirst.Text.Length != 0
                && tbxInitial.Text.Length != 0)
                cmdEdit.Enabled = true;                            
        }


        private void dtpMarineExp_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dtp = (DateTimePicker)sender;
            
            if (!dtp.Checked) dtp.Checked = true;
            dtp.CustomFormat = null;
        }

        private void dtpMEDExp_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dtp = (DateTimePicker)sender;

            if (!dtp.Checked) dtp.Checked = true;
            dtp.CustomFormat = null;
        }

        private void dtpMedical_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dtp = (DateTimePicker)sender;

            if (!dtp.Checked) dtp.Checked = true;
            dtp.CustomFormat = null;
        }
    }
}
