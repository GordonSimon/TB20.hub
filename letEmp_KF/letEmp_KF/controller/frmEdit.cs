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
    public partial class frmEdit : Form
    {
        public bool NOVA_ { get; set; }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private ucEmp _uc = null;
        private DataRow _row { get; set; }

     
        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public frmEdit()
        {
            InitializeComponent();

            NOVA_ = false;
        }

        
        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void mode_edit()
        {
            cmdUpdate.Enabled = true;
            cmdClose.Enabled = false;
            cmdCancel.Enabled = true;
            cmdEdit.Enabled = false;

            _uc.ModeEdit();
        }


        private void mode_save()
        {
            cmdUpdate.Enabled = false;
            cmdClose.Enabled = true;
            cmdCancel.Enabled = false;
            cmdEdit.Enabled = true;

            _uc.ModeRead();
        }


        private void mode_nova()
        {
            cmdUpdate.Enabled = false;
            cmdClose.Enabled = false;
            cmdCancel.Enabled = true;
            cmdEdit.Enabled = false;

            _uc.ModeNova();
        }

        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public void LoadRow(DataRow row)
        {
            _row = row;
        }

        
        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void frmEdit_Load(object sender, EventArgs e)
        {
            _uc = new ucEmp();
            _uc.Location = new Point(5, 5);
            pnlEmp.Controls.Add(_uc);


            if (NOVA_)
                mode_nova();
            else
            {
                _uc.LoadRow(_row);
                mode_edit();
            }

        }


        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            string emp_id = _uc.EMP_ID;

            //DateTime tod = DateTime.Now;
            //DataSet ds = dacEmployees.GetKey(emp_id);
            //if (ds.Tables[0].Rows.Count != 1)
            //    MessageBox.Show(" Warning : Number of Employee rows returnwed is not 1 !");
            //DataRow row = ds.Tables[0].Rows[0];
            
            DataRow row = dacEmployees.DT.Rows.Find(emp_id);

            row.BeginEdit();
            row["First Name"] = _uc.FNAME;
            row["Last Name"] = _uc.LNAME;
            row["Duty"] = (_uc.DUTY.Length == 0 ? null : _uc.DUTY);
            row["Employment"] = (_uc.EMPLOYMENT.Length == 0 ? null : _uc.EMPLOYMENT);
            row["DefPayCode"] = (_uc.PAYCODE.Length == 0 ? null : _uc.PAYCODE);
            row["Archive"] = _uc.ARCHIVE_;

            row["Master"] = _uc.MASTER_;
            row["Master Certification"] = (_uc.MASTER_CERT.Length == 0 ? null : _uc.MASTER_CERT);
            row["Master Classification"] = (_uc.MASTERFICATION.Length == 0 ? null : _uc.MASTERFICATION);
            row["Master Expire Date"] = DBNull.Value; //_uc.MASTER_DATE
            if (_uc.MASTER_DATE > DateTimePicker.MinimumDateTime) row["Master Expire Date"] = _uc.MASTER_DATE;

            row["Marine Emergency Duties"] = _uc.MED_;
            row["MED Certificate"] = (_uc.MED_CERT.Length == 0 ? null : _uc.MED_CERT);
            row["Marine Med Exp Date"] = DBNull.Value; //_uc.MED_DATE
            if (_uc.MED_DATE > DateTimePicker.MinimumDateTime) row["Marine Med Exp Date"] = _uc.MED_DATE;

            row["Marine Medical"] = _uc.MEDICAL_;
            row["Marine Medical Certificate Date"] = DBNull.Value; //_uc.MEDICAL_DATE
            if (_uc.MEDICAL_DATE > DateTimePicker.MinimumDateTime) row["Marine Medical Certificate Date"] = _uc.MEDICAL_DATE;


            row.EndEdit();
            
            //dacEmployees.SaveData(ds.GetChanges());
            dacEmployees.SaveDT();

            mode_save();
        }


        private void cmdEdit_Click(object sender, EventArgs e)
        {
            mode_edit();
        }


        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }



    }
}
