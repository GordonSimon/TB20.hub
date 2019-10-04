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
    public partial class frmLog : Form
    {
        private const int adjScreen = 190;

        public DateTime LogDate { get; set; }

        private DataTable _dt_time { get; set; }
        
        private Dictionary<string, Button> _vessels;


        private bool _dirty = false;
        private bool _deleted = false;

        private int sm_state = 1;

        private string cur_lognumber = "";


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/                             

        public frmLog(DateTime day)
        {
            InitializeComponent();

            cmdOK.Hide();

            
            LogDate = day;
            dtpLogDate.Value = LogDate;

            _vessels = new Dictionary<string, Button>();

            //lblLogDate.Text = DateTime.Now.AddDays(-9).ToShortDateString();
            //lblVerifyDate.Text = DateTime.Now.ToShortDateString();
            lblLogDate.Text = LogDate.ToShortDateString();
            lblVerifyDate.Text = LogDate.ToShortDateString();
            
            lblLogUser.Text = "<system>";
            lblVerifyUser.Text = "<update>";

            cbxVerifyDate.Items.Add(string.Format("Update : [Chris] [{0}]", lblVerifyDate.Text));
            cbxVerifyDate.Items.Add(string.Format("Update : [Travis] [{0}]", DateTime.Now.AddDays(-5).ToShortDateString()));
            cbxVerifyDate.Items.Add(string.Format("Create : [Robert] [{0}]", lblLogDate.Text));
            
            cbxVerifyDate.Text = "3";

            DataTable dt = qryGang.GetView("Master", false);            
            cbxCapt.DataSource = dt;
            cbxCapt.DisplayMember = "EmpName";
            cbxCapt.ValueMember = "EmpID";
            //cbxCapt.SelectedIndex = -1;            

            dt = qryGang.GetView("Deckhand", false);
            cbxDh1.DataSource = dt;
            cbxDh1.DisplayMember = "EmpName";
            cbxDh1.ValueMember = "EmpID";
            //cbxDh1.SelectedIndex = -1;

            cbxDh2.BindingContext = new BindingContext();
            cbxDh2.DataSource = dt;
            cbxDh2.DisplayMember = "EmpName";
            cbxDh2.ValueMember = "EmpID";
            //cbxDh2.SelectedIndex = -1;

            dt = qryGang.GetView("Master", false);
            cbxMate.DataSource = dt;
            cbxMate.DisplayMember = "EmpName";
            cbxMate.ValueMember = "EmpID";
            //cbxMate.SelectedIndex = -1;

            dt = qryGang.GetView("Deckhand", false);
            cbxEng.BindingContext = new BindingContext();
            cbxEng.DataSource = dt;
            cbxEng.DisplayMember = "EmpName";
            cbxEng.ValueMember = "EmpID";
            //cbxEng.SelectedIndex = -1;

            cbxC6.BindingContext = new BindingContext();
            cbxC6.DataSource = dt;
            cbxC6.DisplayMember = "EmpName";
            cbxC6.ValueMember = "EmpID";
            //cbxC6.SelectedIndex = -1;

            dt = dacCache.GetVessel();                        
            cbxShips.DataSource = dt;
            cbxShips.DisplayMember = "Full Name";
            cbxShips.ValueMember = "Short";
            //cbxShips.SelectedIndex = -1;


            //cbxCapt.SelectedIndexChanged += new EventHandler(cbxCapt_SelectedIndexChanged);
            //cbxDh1.SelectedIndexChanged += new EventHandler(cbxDh1_SelectedIndexChanged);
            //cbxDh2.SelectedIndexChanged += new EventHandler(cbxDh2_SelectedIndexChanged);
            //cbxMate.SelectedIndexChanged += new EventHandler(cbxMate_SelectedIndexChanged);
            //cbxEng.SelectedIndexChanged += new EventHandler(cbxEng_SelectedIndexChanged);
        
            //cbxCaShift.DataSource = new List<string>(get_shifts());
            //cbxDH1Shift.DataSource = new List<string>(get_shifts());
            //cbxDH2Shift.DataSource = new List<string>(get_shifts());
            //cbxMaShift.DataSource = new List<string>(get_shifts());
            //cbxEnShift.DataSource = new List<string>(get_shifts());
            //cbxShipShift.DataSource = new List<string>(get_shifts());

            cbxCaShift.DataSource = get_shifts();
            cbxCaShift.BindingContext = new BindingContext();
            cbxCaShift.DisplayMember = "Short";
            cbxCaShift.ValueMember = "NumID";
            cbxCaShift.SelectedIndex = 1;

            cbxDH1Shift.DataSource = get_shifts();
            cbxDH1Shift.BindingContext = new BindingContext();
            cbxDH1Shift.DisplayMember = "Short";
            cbxDH1Shift.ValueMember = "NumID";
            cbxDH1Shift.SelectedIndex = 1;

            cbxDH2Shift.DataSource = get_shifts();
            cbxDH2Shift.BindingContext = new BindingContext();
            cbxDH2Shift.DisplayMember = "Short";
            cbxDH2Shift.ValueMember = "NumID";
            cbxDH2Shift.SelectedIndex = 1;

            cbxMaShift.DataSource = get_shifts();
            cbxMaShift.BindingContext = new BindingContext();
            cbxMaShift.DisplayMember = "Short";
            cbxMaShift.ValueMember = "NumID";
            cbxMaShift.SelectedIndex = 2;

            cbxEnShift.DataSource = get_shifts();
            cbxEnShift.BindingContext = new BindingContext();
            cbxEnShift.DisplayMember = "Short";
            cbxEnShift.ValueMember = "NumID";
            cbxEnShift.SelectedIndex = 2;

            cbxC6Shift.DataSource = get_shifts();
            cbxC6Shift.BindingContext = new BindingContext();
            cbxC6Shift.DisplayMember = "Short";
            cbxC6Shift.ValueMember = "NumID";
            cbxC6Shift.SelectedIndex = 2;


            cbxShipShift.DataSource = get_shifts();
            cbxShipShift.BindingContext = new BindingContext();
            cbxShipShift.DisplayMember = "Short";
            cbxShipShift.ValueMember = "NumID";

            cbxShips.SelectedIndex = -1;

            lblLog.Hide();
            lblShift.Hide();
            lblShift.Hide();
            lblHourStart.Hide();
            lblHourFinish.Hide();
            lblFuel.Hide();

            pnlPM.Hide();

            this.Width -= adjScreen;
            cmdCancel.Focus();
        }


        private void frmLog_Load(object sender, EventArgs e)
        {
            DateTime ref_week = set_date(LogDate);

            //dacCache.RefreshTimebook(ref_week, false);
            //qryGang.Requery();
            //qryTimebook.Requery();
            mboTimebook.Load(ref_week);

            pnlEmp.Hide();
            pnlVes.Hide();
            pnlShf.Hide();

            show_timebook();

            if (flpVessel.Controls.Count == 0)
            {
                cbxShips.SelectedItem = -1;
                cbxShips.Text = "";
                tbxSelVessel.Text = "";

                cmdOK.Hide();
            }

            this.Text = string.Format("Log Sheet for Date [{0}, {1}]",
                LogDate.DayOfWeek.ToString(),
                LogDate.ToLongDateString());
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        //private List<string> get_shifts()
        private DataTable get_shifts()
        {
             return dacCache.GetShift();

            //return new List<string> { "N/A", "AM", "PM", "24 Hours" };
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void form_clear()
        {          
            cmdC4.Text = "Capt";
            cmdEng.Text = "DH(3).";

            lblMate.Text = "Capt";
            lblEng.Text = "DH(3)";
            lblC6.Text = "DH(4)";

            cbxCapt.SelectedIndex = -1;
            cbxDh1.SelectedIndex = -1;
            cbxDh2.SelectedIndex = -1;
            cbxMate.SelectedIndex = -1;
            cbxEng.SelectedIndex = -1;
            cbxC6.SelectedIndex = -1;
            //if (! _new)  cbxShips.SelectedIndex = -1;

            tbxCapt.Text = string.Empty;
            tbxDh1.Text = string.Empty;
            tbxDh2.Text = string.Empty;
            tbxMate.Text = string.Empty;
            tbxEng.Text = string.Empty;
            tbxC6.Text = string.Empty;

            //cbxCapt.Text = string.Empty;
            //cbxDh1.Text = string.Empty;
            //cbxDh2.Text = string.Empty;
            //cbxMate.Text = string.Empty;
            //cbxEng.Text = string.Empty;
            //cbxShips.Text = string.Empty;

            chkCapt.Checked = false;
            chkDH1.Checked = false;
            chkDH2.Checked = false;
            chkMate.Checked = false;
            chkEng.Checked = false;
            chkC6.Checked = false;

            //    lblDel1.Hide();
            //    lblDel2.Hide();
            //    lblDel3.Hide();
            //    lblDel4.Hide();
            //    lblDel5.Hide();
            //    lblDel6.Hide();

            vacant(tbxCpA, tbxCpP, tbxCpO);
            vacant(tbxD1A, tbxD1P, tbxD1O);
            vacant(tbxD2A, tbxD2P, tbxD2O);
            vacant(tbxMaA, tbxMaP, tbxMaO);
            vacant(tbxEnA, tbxEnP, tbxEnO);
            vacant(tbxC6A, tbxC6P, tbxC6O);

            cbxCaShift.SelectedIndex = 1;
            cbxDH1Shift.SelectedIndex = 1;
            cbxDH2Shift.SelectedIndex = 1;

            cbxMaShift.SelectedIndex = 2;
            cbxEnShift.SelectedIndex = 2;
            cbxC6Shift.SelectedIndex = 2;

            //_dirty = false;
            //_deleted = false;
            
            cmdOK.Hide();
        }


        private void form_readonly()
        {
            errorProvider1.Clear();

            //tbxCpA.ReadOnly = true;
            //tbxD1A.ReadOnly = true;
            //tbxD2A.ReadOnly = true;
            //tbxMaA.ReadOnly = true;
            //tbxEnA.ReadOnly = true;
            //tbxC6A.ReadOnly = true;

            tbxCpA.ReadOnly = false;
            tbxD1A.ReadOnly = false;
            tbxD2A.ReadOnly = false;
            tbxMaA.ReadOnly = false;
            tbxEnA.ReadOnly = false;
            tbxC6A.ReadOnly = false;

            tbxCpO.ReadOnly = true;
            tbxD1O.ReadOnly = true;
            tbxD2O.ReadOnly = true;
            tbxMaO.ReadOnly = true;
            tbxEnO.ReadOnly = true;
            tbxC6O.ReadOnly = true;

            tbxCpP.ReadOnly = true;
            tbxD1P.ReadOnly = true;
            tbxD2P.ReadOnly = true;
            tbxMaP.ReadOnly = true;
            tbxEnP.ReadOnly = true;
            tbxC6P.ReadOnly = true;

            cbxCapt.Enabled = false;
            cbxDh1.Enabled = false;
            cbxDh2.Enabled = false;
            cbxMate.Enabled = false;
            cbxEng.Enabled = false;
            cbxC6.Enabled = false;

            cbxCaShift.Enabled = false;
            cbxDH1Shift.Enabled = false;
            cbxDH2Shift.Enabled = false;
            cbxMaShift.Enabled = false;
            cbxEnShift.Enabled = false;
            cbxC6Shift.Enabled = false;

            cbxShips.Enabled = false;
            nudShift.Enabled = false;
            cbxShipShift.Enabled = false;

            chkCapt.Enabled = false;
            chkDH1.Enabled = false;
            chkDH2.Enabled = false;
            chkMate.Enabled = false;
            chkEng.Enabled = false;
            chkC6.Enabled = false;

            lblDel1.Hide();
            lblDel2.Hide();
            lblDel3.Hide();
            lblDel4.Hide();
            lblDel5.Hide();
            lblDel6.Hide();
            
            cmdOK.Text = "Edit";
            cmdOK.Show();
            cmdNew.Show();

            cmdOK.Focus();
        }

      
        private void form_edit()
        {
            errorProvider1.Clear();

            if (! chkCapt.Checked)
            {
                tbxCpA.ReadOnly = false;
                tbxCpO.ReadOnly = false;
                tbxCpP.ReadOnly = false;
            }

            if (!chkDH1.Checked)
            {
                tbxD1A.ReadOnly = false;
                tbxD1O.ReadOnly = false;
                tbxD1P.ReadOnly = false;
            }

            if (!chkDH2.Checked)
            {
                tbxD2A.ReadOnly = false;
                tbxD2O.ReadOnly = false;
                tbxD2P.ReadOnly = false;
            }

            if (!chkMate.Checked)
            {
                tbxMaA.ReadOnly = false;
                tbxMaO.ReadOnly = false;
                tbxMaP.ReadOnly = false;
            }

            if (!chkEng.Checked)
            {
                tbxEnA.ReadOnly = false;
                tbxEnO.ReadOnly = false;
                tbxEnP.ReadOnly = false;
            }

            if (!chkC6.Checked)
            {
                tbxC6A.ReadOnly = false;
                tbxC6O.ReadOnly = false;
                tbxC6P.ReadOnly = false;
            }


            cbxCapt.Enabled = true;
            cbxDh1.Enabled = true;
            cbxDh2.Enabled = true;
            cbxMate.Enabled = true;
            cbxEng.Enabled = true;
            cbxC6.Enabled = true;

            cbxCaShift.Enabled = true;
            cbxDH1Shift.Enabled = true;
            cbxDH2Shift.Enabled = true;
            cbxMaShift.Enabled = true;
            cbxEnShift.Enabled = true;
            cbxC6Shift.Enabled = true;

            cbxShips.Enabled = true;
            nudShift.Enabled = true;
            cbxShipShift.Enabled = true;

            chkCapt.Enabled = true;
            chkDH1.Enabled = true;
            chkDH2.Enabled = true;
            chkMate.Enabled = true;
            chkEng.Enabled = true;
            chkC6.Enabled = true;
            
            lblDel1.Show();
            lblDel2.Show();
            lblDel3.Show();
            lblDel4.Show();
            lblDel5.Show();
            lblDel6.Show();

            _dirty = false;
            _deleted = false;

            cmdOK.Text = "Save";
            //cmdCancel.Text = "Cancel";
            //cmdOK.Show();
            cmdNew.Show();


            //tbxCpA.Focus();
            cmdCancel.Focus();
        }


        private void clear_vessels()
        {
            flpVessel.Controls.Clear();
            _vessels.Clear();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/                     

        private void put_captain(int idx, DataRow row, int dh_idx)
        {
            Decimal hour = (Decimal)row["Hours"];
            Decimal over = (Decimal)row["Over"];

            if (idx == 1)
            {                
                cbxCapt.Text = (string)row["EmpName"];
                tbxCapt.Text = cbxCapt.SelectedValue.ToString();
                tbxCpA.Text = hour.ToString("#.#");
                tbxCpO.Text = over.ToString("#.#");
                tbxCpP.Text = (hour >= 12M ? 12M : hour).ToString("#.#");
                chkCapt.Checked = hour.Equals(12M);
                cbxCaShift.SelectedIndex = (int)row["Shift"];
                //cbxCaShift.Text = (string)row["ShiftHour"];
                //set_captainX(tbxCpA, tbxCpP, tbxCpO, chkCapt.Checked, tbxV1A, chkCrew1.Checked);
                set_captain(tbxCpA, tbxCpP, tbxCpO, chkCapt.Checked);
            }
            else if (idx == 2)
            {
                cbxMate.Text = (string)row["EmpName"];
                tbxMate.Text = cbxMate.SelectedValue.ToString();
                cmdC4.Text = "Capt";
                lblMate.Text = "Capt";
                tbxMaA.Text = hour.ToString("#.#");
                tbxMaO.Text = over.ToString("#.#");
                tbxMaP.Text = (hour >= 12M ? 12M : hour).ToString("#.#");
                chkMate.Checked = hour.Equals(12M);
                cbxMaShift.SelectedIndex = (int)row["Shift"];
                //cbxMaShift.Text = (string)row["ShiftHour"];
                set_captain(tbxMaA, tbxMaP, tbxMaO, chkMate.Checked);
            }
            else if (idx == 3)
            {
                if (dh_idx < 3)
                {
                    cbxEng.Text = (string)row["EmpName"];
                    tbxEng.Text = cbxEng.SelectedValue.ToString();
                    cmdEng.Text = "Capt";
                    lblEng.Text = "Capt";
                    tbxEnA.Text = hour.ToString("#.#");
                    tbxEnO.Text = over.ToString("#.#");
                    tbxEnP.Text = (hour >= 12M ? 12M : hour).ToString("#.#");
                    chkEng.Checked = hour.Equals(12M);
                    cbxEnShift.SelectedIndex = (int)row["Shift"];
                    //cbxEnShift.Text = (string)row["ShiftHour"];
                    set_captain(tbxEnA, tbxEnP, tbxEnO, chkEng.Checked);
                }
            }
            else if (idx == 4)
            {
                if (dh_idx < 4)
                {
                    cbxC6.Text = (string)row["EmpName"];
                    tbxC6.Text = cbxC6.SelectedValue.ToString();
                    cmdC6.Text = "Capt";
                    lblC6.Text = "Capt";
                    tbxC6A.Text = hour.ToString("#.#");
                    tbxC6O.Text = over.ToString("#.#");
                    tbxC6P.Text = (hour >= 12M ? 12M : hour).ToString("#.#");
                    chkC6.Checked = hour.Equals(12M);
                    cbxC6Shift.SelectedIndex = (int)row["Shift"];
                    //cbxEnShift.Text = (string)row["ShiftHour"];
                    set_captain(tbxC6A, tbxC6P, tbxC6O, chkC6.Checked);
                }
            }


        }


        private void put_deckhand(int idx, DataRow row, int skip_idx, bool index_0)
        {
            Decimal hour = (Decimal)row["Hours"];
            Decimal over = (Decimal)row["Over"];
            
            if (! index_0)
            {
                bool am = ((int)row["Shift"] == 1);
                if (! am) idx = (idx == 3 ? 4 : idx);
                if (! am) idx = (idx == 2 ? 3 : 2);                
            }
            
            if (idx == 1)
            {
                cbxDh1.Text = (string)row["EmpName"];
                tbxDh1.Text = cbxDh1.SelectedValue.ToString();
                tbxD1A.Text = hour.ToString("#.#");
                tbxD1O.Text = over.ToString("#.#");
                tbxD1P.Text = (hour >= 12M ? 12M : hour).ToString("#.#");
                chkDH1.Checked = hour.Equals(12M);
                cbxDH1Shift.SelectedIndex = (int)row["Shift"];
                //cbxDH1Shift.Text = (string)row["ShiftHour"];
                set_standard(tbxD1A, tbxD1P, tbxD1O, chkDH1.Checked);
            }
            else if (idx == 2)
            {
                cbxDh2.Text = (string)row["EmpName"];
                tbxDh2.Text = cbxDh2.SelectedValue.ToString();
                tbxD2A.Text = hour.ToString("#.#");
                tbxD2O.Text = over.ToString("#.#");
                tbxD2P.Text = (hour >= 12M ? 12M : hour).ToString("#.#");
                chkDH2.Checked = hour.Equals(12M);
                cbxDH2Shift.SelectedIndex = (int)row["Shift"];
                //cbxDH2Shift.Text = (string)row["ShiftHour"];
                set_standard(tbxD2A, tbxD2P, tbxD2O, chkDH2.Checked);
            }
            else if (idx == 3)
            {
                if (skip_idx < 3)
                {
                    cbxEng.Text = (string)row["EmpName"];
                    tbxEng.Text = cbxEng.SelectedValue.ToString();
                    cmdEng.Text = "DH(3)";
                    lblEng.Text = "DH(3)";
                    tbxEnA.Text = hour.ToString("#.#");
                    tbxEnO.Text = over.ToString("#.#");
                    tbxEnP.Text = (hour >= 12M ? 12M : hour).ToString("#.#");
                    chkEng.Checked = hour.Equals(12M);
                    cbxEnShift.SelectedIndex = (int)row["Shift"];
                    //cbxEnShift.Text = (string)row["ShiftHour"];
                    set_standard(tbxEnA, tbxEnP, tbxEnO, chkEng.Checked);
                }
            }
            else if (idx == 4)
            {
                if (skip_idx < 4)
                {
                    cbxC6.Text = (string)row["EmpName"];
                    tbxC6.Text = cbxC6.SelectedValue.ToString();
                    cmdC6.Text = "DH(4)";
                    lblC6.Text = "DH(4)";
                    tbxC6A.Text = hour.ToString("#.#");
                    tbxC6O.Text = over.ToString("#.#");
                    tbxC6P.Text = (hour >= 12M ? 12M : hour).ToString("#.#");
                    chkC6.Checked = hour.Equals(12M);
                    cbxC6Shift.SelectedIndex = (int)row["Shift"];
                    //cbxC6Shift.Text = (string)row["ShiftHour"];
                    set_standard(tbxC6A, tbxC6P, tbxC6O, chkC6.Checked);
                }
            }

        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/                     

        private void show_vessel(string vessel)
        {
            if (!_vessels.ContainsKey(vessel))
            {
                Button cmd = mkbtn_vessel(vessel);

                _vessels.Add(vessel, cmd);
                flpVessel.Controls.Add(cmd);
            }
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/                     

        private Button mkbtn_vessel(string name)
        {
            Button cmd = new Button();
            cmd.Text = name;

            cmd.Click += new EventHandler(cmdVessel_Click);

            return cmd;
        }


        private bool scan_index_for_0(string vessel)
        {
            foreach (DataRow row in _dt_time.Rows)
            {
                //if (row.RowState == DataRowState.Deleted) continue;
                if ((bool)row["DelMark"]) continue;

                if (vessel.Equals(row["Vessel"]))
                {
                    int idx = (int)row["Shift"];
                    if (idx == 0) return true;
                }
            }

            return false;
        }


        private void form_vessel(string vessel, bool _edit)
        {
            //bool _readonly = false;

            int count_skipper = 0;
            int count_deckhand = 0;
            int count_other = 0;
            int count_records = 0;

            if (vessel == null || vessel.Length == 0) return;


            form_clear();

            bool index_0 = scan_index_for_0(vessel);
            foreach (DataRow row in _dt_time.Rows)
            {
                //if (row.RowState == DataRowState.Deleted) continue;
                if ((bool)row["DelMark"]) continue;

                if (vessel.Equals(row["Vessel"]))
                {
                    //_readonly = true;

                    cbxShips.SelectedValue = (string)row["Vessel"];
                    tbxSelVessel.Text = cbxShips.SelectedValue.ToString();                    

                    count_records += 1;
                    if (row["Duty"].Equals("MASTER"))
                    {
                        count_skipper += 1;
                        put_captain(count_skipper, row, count_deckhand);
                        //MessageBox.Show(string.Format("[{0}], {1}", cbxCaShift.SelectedIndex,cbxCaShift.Text));
                    }
                    else if (row["Duty"].Equals("DECKHAND"))
                    {
                        count_deckhand += 1;
                        put_deckhand(count_deckhand, row, count_skipper, index_0);
                    }
                    else
                    {
                        count_deckhand += 1;
                        put_deckhand(count_deckhand, row, count_skipper, index_0);
                        count_other += 1;
                        MessageBox.Show(string.Format("Warning (form_vessel) : Employee Duty [{0}] @ [{1}])",
                            (string)row["Duty"], (string)row["EmpName"]));

                    }
                }
            }

            if (count_records >= 5 || count_skipper > 2 || count_deckhand > 3 || count_other > 0)
                MessageBox.Show(string.Format("Error (cmdVessel_click) : Too many records @ [{0}], Skippers[{1}] Deckhands[{2}] Other[{3}]",
                    count_records, count_skipper, (count_other > 0 ? count_deckhand - count_other : count_deckhand), count_other));

            tbxCrewCount.Text = string.Format("[{0}]", count_records.ToString());
            tbxV1A.Text = vessel_hours().ToString();

            cmdNew.Show();
            pnlPM.Show();
        }


        void cmdVessel_Click(object sender, EventArgs e)
        {
            Button cmd = (Button)sender;
            form_vessel(cmd.Text, false);


            //if (_edit) { form_edit(); cmdOK.Show(); }
            //else
            //{
            //    if (_readonly) { form_readonly(); cmdNew.Show(); }
            //}

            
            form_readonly(); 

            //cmdNew.Show();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/                     
        
        private void show_timebook()
        {
            //DataTable dt = qryTimebook.GetView("Vessel", LogDate.Date);
            DataTable dt = mboTimebook.GetTimebook(LogDate.Date);

            dgvTB.DataSource = dt;

            clear_vessels();
            form_clear();
            
            _dt_time = dt;
            //DateTime day = dtpLogDate.Value;            

            foreach (DataRow row in dt.Rows)
            {
                show_vessel((string)row["Vessel"]);

                //cbxShips.Text = (string)row["Vessel"];
            }

            refresh_totals();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/                             
        
        private DateTime set_date(DateTime d)
        {
            int offset = d.DayOfWeek - DayOfWeek.Monday;
            offset = (offset == -1 ? 6 : offset);

            DateTime lastMonday = d.AddDays(-offset);

            return lastMonday;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/                             
        
        private void vessel_standard(TextBox tbxV, bool crew)
        {
            if (crew)
            {
                tbxV.BackColor = Color.Yellow;
                tbxV.Text = "12";
                tbxV.ReadOnly = true;
            }
        }

        private void vessel_premium(TextBox tbxV, bool crew)
        {
            if (crew)
            {
                tbxV.BackColor = Color.White;
                tbxV.Text = "";
                tbxV.ReadOnly = false;
            }
        }


        private void vacant(TextBox tbxA, TextBox tbxP, TextBox tbxO)
        {
            tbxA.BackColor = Color.AliceBlue;            
            //tbxA.ReadOnly = true;
            tbxA.ReadOnly = false;

            tbxP.BackColor = Color.AliceBlue;
            tbxO.BackColor = Color.AliceBlue;
            tbxP.ReadOnly = true;
            tbxO.ReadOnly = true;

            tbxA.Text = string.Empty;
            tbxP.Text = string.Empty;
            tbxO.Text = string.Empty;

        }


        private void standard(TextBox tbxA, TextBox tbxP, TextBox tbxO)
        {
            tbxA.BackColor = Color.Yellow;
            tbxA.Text = "12";
            //tbxA.ReadOnly = true;
            tbxA.ReadOnly = false;

            tbxP.BackColor = Color.AliceBlue;
            tbxO.BackColor = Color.AliceBlue;
            tbxP.ReadOnly = true;
            tbxO.ReadOnly = true;

            tbxP.Text = "12";
            tbxO.Text = "";
        }

    
        private void premium(TextBox tbxA, TextBox tbxP, TextBox tbxO)
        {
            tbxA.BackColor = Color.White;
            //tbxA.Text = "";
            tbxA.ReadOnly = false;
            
            tbxP.BackColor = Color.White;
            tbxO.BackColor = Color.White;
            tbxP.ReadOnly = false;
            tbxO.ReadOnly = false;

            //tbxP.Text = "";
            //tbxO.Text = "";
            
            tbxA.Focus();
        }


        private void set_standard(TextBox tbxA, 
            TextBox tbxP, TextBox tbxO, bool regular_hours)
        {
            if (regular_hours)
                standard(tbxA, tbxP, tbxO);                
            else            
                premium(tbxA, tbxP, tbxO);

            refresh_totals();

            tbxA.Focus();
        }


        //private void set_captainX(TextBox tbxA,
        //    TextBox tbxP, TextBox tbxO, bool regular_hours, TextBox tbxV, bool crew)
        private void set_captain(TextBox tbxA,
            TextBox tbxP, TextBox tbxO, bool regular_hours)
        {
            if (regular_hours)
            {
                standard(tbxA, tbxP, tbxO);
                //vessel_standard(tbxV, crew);
            }
            else
            { 
                premium(tbxA, tbxP, tbxO);
                //vessel_premium(tbxV, crew);
            }

            refresh_totals();
        }


        private int needInt32(TextBox tbx)        
        {
            string val = tbx.Text;
            int number;
            bool ok = Int32.TryParse(val, out number);
            if (!ok) return 0;

            return number;
        }

        
        private Decimal needDecimal(TextBox tbx)
        {
            string val = tbx.Text;
            Decimal number;
            bool ok = Decimal.TryParse(val, out number);
            if (!ok) return 0M;

            return number;
        }


        private Decimal vessel_hours()
        {
            Decimal sum = 0;
            if (lblCapt.Text.Equals("Capt")) sum += needDecimal(tbxV1Ca);
            if (lblMate.Text.Equals("Capt")) sum += needDecimal(tbxV1Ma);
            if (lblEng.Text.Equals("Capt")) sum += needDecimal(tbxV1En);
            if (lblDH1.Text.Equals("Capt")) sum += needDecimal(tbxV1D1);
            if (lblDH2.Text.Equals("Capt")) sum += needDecimal(tbxV1D2);
            if (lblC6.Text.Equals("Capt")) sum += needDecimal(tbxV1C6);

            return sum;
        }


        private void fullcrew(TextBox tbxVA, TextBox tbxVCa,
            TextBox tbxVMa, TextBox tbxVEn, TextBox tbxVD1, TextBox tbxVD2)
        {
            tbxVA.BackColor = Color.Yellow;
            tbxVA.Text = vessel_hours().ToString();
            tbxVA.ReadOnly = true;

            tbxVCa.BackColor = Color.AliceBlue;
            tbxVMa.BackColor = Color.AliceBlue;
            tbxVEn.BackColor = Color.AliceBlue;
            tbxVD1.BackColor = Color.AliceBlue;
            tbxVD2.BackColor = Color.AliceBlue;
            tbxVCa.ReadOnly = true;
            tbxVMa.ReadOnly = true;
            tbxVEn.ReadOnly = true;
            tbxVD1.ReadOnly = true;
            tbxVD2.ReadOnly = true;

            tbxVCa.Text = tbxCpA.Text;
            tbxVMa.Text = tbxMaA.Text;
            tbxVEn.Text = tbxEnA.Text;
            tbxVD1.Text = tbxD1A.Text;
            tbxVD2.Text = tbxD2A.Text;
        }


        private void partcrew(TextBox tbxVA, TextBox tbxVCa,
            TextBox tbxVMa, TextBox tbxVEn, TextBox tbxVD1, TextBox tbxVD2)
        {
            tbxVA.BackColor = Color.White; 
            tbxVA.Text = "";
            tbxVA.ReadOnly = false;
            tbxVD1.BackColor = Color.White;

            tbxVCa.BackColor = Color.White;
            tbxVMa.BackColor = Color.White;
            tbxVEn.BackColor = Color.White;
            tbxVD2.BackColor = Color.White;
            tbxVCa.ReadOnly = false;
            tbxVMa.ReadOnly = false;
            tbxVEn.ReadOnly = false;
            tbxVD1.ReadOnly = false;
            tbxVD2.ReadOnly = false;

            tbxVCa.Text = "";
            tbxVMa.Text = "";
            tbxVEn.Text = "";
            tbxVD1.Text = "";
            tbxVD2.Text = "";

            tbxVA.Focus();
        }


        //private void set_vessel1(TextBox tbxVA, TextBox tbxVCa,
        //    TextBox tbxVMa, TextBox tbxVEn, TextBox tbxVD1, TextBox tbxVD2, bool crew)
        //{
        //    if (crew)
        //        fullcrew(tbxVA, tbxVCa, tbxVMa, tbxVEn, tbxVD1, tbxVD2);
        //    else
        //        partcrew(tbxVA, tbxVCa, tbxVMa, tbxVEn, tbxVD1, tbxVD2);

        //    refresh_totals(); 
        //}


        //private void chkCrew1_CheckedChanged(object sender, EventArgs e)
        //{
        //    set_vessel1(tbxV1A, tbxV1Ca, tbxV1Ma,tbxV1En, tbxV1D1, tbxV1D2, chkCrew1.Checked);
        //}


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/                             

        private void cbxCapt_SelectedIndexChanged(object sender, EventArgs e)
        {
            set_standard(tbxCpA, tbxCpP, tbxCpO, chkCapt.Checked = true);

            //ComboBox cbx = (ComboBox)sender;

            //tbxV1A.ReadOnly = true;
            //if (cbx.SelectedIndex == 0)
            //{
            //    tbxV1A.BackColor = Color.PowderBlue;
            //    tbxV1A.Text = string.Empty;

            //    chkCapt.Enabled = false;
            //    chkCapt.Checked = false;

            //    chkCrew1.Enabled = false;
            //    chkCrew1.Checked = false;
            //}
            //else
            //{
            //    tbxV1A.BackColor = Color.Yellow;
            //    tbxV1A.Text = "12";                

            //    chkCapt.Enabled = true;
            //    chkCapt.Checked = true;

            //    chkCrew1.Enabled = true;
            //    chkCrew1.Checked = true;
            //}
        }


        private void cbxDh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            set_standard(tbxD1A, tbxD1P, tbxD1O, chkDH1.Checked = true);

            //chkDH1.Enabled = true;
            //chkDH1.Checked = true;
        }


        private void cbxDh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            set_standard(tbxD2A, tbxD2P, tbxD2O, chkDH2.Checked = true);

            //chkDH2.Enabled = true;
            //chkDH2.Checked = true;            
        }


        private void cbxMate_SelectedIndexChanged(object sender, EventArgs e)
        {
            set_standard(tbxMaA, tbxMaP, tbxMaO, chkMate.Checked = true);

            //chkMate.Enabled = true;
            //chkMate.Checked = true;            
        }


        private void cbxEng_SelectedIndexChanged(object sender, EventArgs e)
        {
            set_standard(tbxEnA, tbxEnP, tbxEnO, chkEng.Checked = true);

            //chkEng.Enabled = true;
            //chkEng.Checked = true;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/ 

        private void cbxCapt_SelectionChangeCommitted(object sender, EventArgs e)
        {
            chkCapt.Enabled = true;
            chkCapt.Checked = true;

            tbxCapt.Text = cbxCapt.SelectedValue.ToString();

            set_standard(tbxCpA, tbxCpP, tbxCpO, chkCapt.Checked);
            lblDel1.Show();
        }
        
        private void cbxDh1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            chkDH1.Enabled = true;
            chkDH1.Checked = true;

            tbxDh1.Text = cbxDh1.SelectedValue.ToString();

            set_standard(tbxD1A, tbxD1P, tbxD1O, chkDH1.Checked);
            lblDel2.Show();
        }

        private void cbxDh2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            chkDH2.Enabled = true;
            chkDH2.Checked = true;

            tbxDh2.Text = cbxDh2.SelectedValue.ToString();

            set_standard(tbxD2A, tbxD2P, tbxD2O, chkDH2.Checked);
            lblDel3.Show();
        }

        private void cbxMate_SelectionChangeCommitted(object sender, EventArgs e)
        {
            chkMate.Enabled = true;
            chkMate.Checked = true;

            tbxMate.Text = cbxMate.SelectedValue.ToString();

            set_standard(tbxMaA, tbxMaP, tbxMaO, chkMate.Checked);
            lblDel4.Show();
        }

        private void cbxEng_SelectionChangeCommitted(object sender, EventArgs e)
        {
            chkEng.Enabled = true;
            chkEng.Checked = true;

            tbxEng.Text = cbxEng.SelectedValue.ToString();

            set_standard(tbxEnA, tbxEnP, tbxEnO, chkEng.Checked);
            lblDel5.Show();
        }

        private void cbxC6_SelectionChangeCommitted(object sender, EventArgs e)
        {
            chkC6.Enabled = true;
            chkC6.Checked = true;

            tbxC6.Text = cbxC6.SelectedValue.ToString();

            set_standard(tbxC6A, tbxC6P, tbxC6O, chkC6.Checked);
            lblDel6.Show();
        }

        
        private void cbxShips_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox cbx = (ComboBox)sender;

            if (cbx.SelectedIndex == -1) return;

            //int idx = cbx.SelectedIndex;
            string vessel = (string)cbx.SelectedValue;
            if (_vessels.ContainsKey(vessel))
            {
                form_vessel(vessel, false);
                form_readonly(); 
                //cmdNew.Show();
            }
            else
            {
                tbxSelVessel.Text = vessel;

                cmdOK.Text = "Save";
                cmdOK.Show();
            }
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void crew_count()
        {
            int count = 0;
            count += (needInt32(tbxCpA) == 0 ? 0 : 1);
            count += (needInt32(tbxMaA) == 0 ? 0 : 1);
            count += (needInt32(tbxEnA) == 0 ? 0 : 1);
            count += (needInt32(tbxD1A) == 0 ? 0 : 1);
            count += (needInt32(tbxD2A) == 0 ? 0 : 1);

            tbxCrewCount.Text = count.ToString();
        }

        private void over_hours()
        {
            Decimal sum = 0;
            sum += needDecimal(tbxCpO);
            sum += needDecimal(tbxMaO);
            sum += needDecimal(tbxEnO);
            sum += needDecimal(tbxD1O);
            sum += needDecimal(tbxD2O);

            tbxOverHours.Text = sum.ToString();
        }

        private void paid_hours()
        {
            Decimal sum = 0;
            sum += needDecimal(tbxCpP);
            sum += needDecimal(tbxMaP);
            sum += needDecimal(tbxEnP);
            sum += needDecimal(tbxD1P);
            sum += needDecimal(tbxD2P);

            tbxPaidHours.Text = sum.ToString();
        }

        private void crew_hours()
        {
            Decimal sum = 0;
            sum += needDecimal(tbxCpA);
            sum += needDecimal(tbxMaA);
            sum += needDecimal(tbxEnA);
            sum += needDecimal(tbxD1A);
            sum += needDecimal(tbxD2A);

            tbxCrewHours.Text = sum.ToString();
        }

        private void on_vessel()
        {
            Decimal sum = 0;

            if (cmdC1.Text.Equals("Capt")) sum += needDecimal(tbxV1Ca);
            if (cmdC4.Text.Equals("Capt")) sum += needDecimal(tbxV1Ma);
            if (cmdEng.Text.Equals("Capt")) sum += needDecimal(tbxV1En);
            if (cmdDH1.Text.Equals("Capt")) sum += needDecimal(tbxV1D1);
            if (cmdDH2.Text.Equals("Capt")) sum += needDecimal(tbxV1D2);
            if (cmdC6.Text.Equals("Capt")) sum += needDecimal(tbxV1C6);

            tbxOnVessel.Text = sum.ToString();
        }

        private void refresh_totals()
        {
            on_vessel();
            crew_hours();
            paid_hours();
            over_hours();

            crew_count();
        }


        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/                             
        private void chkCapt_CheckedChanged(object sender, EventArgs e)
        {
            _dirty = true;
            //set_captainX(tbxCpA, tbxCpP, tbxCpO, chkCapt.Checked, tbxV1A, chkCrew1.Checked);
            set_captain(tbxCpA, tbxCpP, tbxCpO, chkCapt.Checked);
        }


        private void chkDH1_CheckedChanged(object sender, EventArgs e)
        {
            _dirty = true;
            set_standard(tbxD1A, tbxD1P, tbxD1O, chkDH1.Checked);
        }

        private void chkDH2_CheckedChanged(object sender, EventArgs e)
        {
            _dirty = true;
            set_standard(tbxD2A, tbxD2P, tbxD2O, chkDH2.Checked);
        }

        private void chkMate_CheckedChanged(object sender, EventArgs e)
        {
            _dirty = true;
            set_standard(tbxMaA, tbxMaP, tbxMaO, chkMate.Checked);
        }

        private void chkEng_CheckedChanged(object sender, EventArgs e)
        {
            _dirty = true;
            set_standard(tbxEnA, tbxEnP, tbxEnO, chkEng.Checked);
        }

        private void chkC6_CheckedChanged(object sender, EventArgs e)
        {
            _dirty = true;
            set_standard(tbxC6A, tbxC6P, tbxC6O, chkC6.Checked);
        }



        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/                             

        private void dtpLogDate_CloseUp(object sender, EventArgs e)
        {
            LogDate = ((DateTimePicker)sender).Value;
            this.Text = string.Format("Log Sheet for Date [{0}, {1}]",
                LogDate.DayOfWeek.ToString(),
                LogDate.ToLongDateString());


            //DateTime user_day = ((DateTimePicker)sender).Value;
            DateTime ref_week = set_date(LogDate);

            
            //GS14V1 //DataSet ds = dacTimebook.GetDS(ref_week, 14);
            //GS14V1 //_dt_crew = ds.Tables[0];
            //dacCache.RefreshTimebook(ref_week, false);
            //qryGang.Requery();
            //qryTimebook.Requery();
            mboTimebook.Load(ref_week);


            //reset_week(RefWeek);
            //refresh_data(RefWeek, _show_all);

            show_timebook();
            cmdCancel.Focus();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void row_captain(DataSet ds, DateTime book_date)
        {
            DataRow row;

            if (cbxCapt.SelectedIndex < 0) return;
            
            row = ds.Tables[0].NewRow();
            row["BookDate"] = book_date;
            row["EmpID"] = cbxCapt.SelectedValue;
            row["EmpName"] = cbxCapt.Text;
            row["ToffCode"] = null;
            row["LogHours"] = needDecimal(tbxCpA);
            row["LogOver"] = needDecimal(tbxCpO);
            row["LogVessel"] = cbxShips.SelectedValue;
            row["LogShift"] = cbxCaShift.SelectedIndex;
            row["LogHour"] = cbxCaShift.SelectedValue;

            if (! lblLog.Text.Equals("<log sheet>")) row["LogSheet"] = lblLog.Text;
            if (! lblShift.Text.Equals("<log shift>")) row["LogShift"] = lblShift.Text;
            if (! lblHourStart.Text.Equals("<engine hours>")) row["LogEngineStart"] = lblHourStart.Text;
            if (! lblHourFinish.Text.Equals("<engine hours>")) row["LogEngineFinish"] = lblHourFinish.Text;
            if (! lblFuel.Text.Equals("<fuel>")) row["LogFuel"] = lblFuel.Text;

            row["LogNote"] = null;
            dacTimebook.FindAdd(new object[] { book_date, row["EmpID"] }, row);
        }


        private void row_deckhand1(DataSet ds, DateTime book_date)
        {
            DataRow row;

            if (cbxDh1.SelectedIndex < 0) return;
            
            row = ds.Tables[0].NewRow();
            row["BookDate"] = book_date;
            row["EmpID"] = cbxDh1.SelectedValue;
            row["EmpName"] = cbxDh1.Text;
            row["ToffCode"] = null;
            row["LogHours"] = needDecimal(tbxD1A);
            row["LogOver"] = needDecimal(tbxD1O);
            row["LogVessel"] = cbxShips.SelectedValue;
            row["LogShift"] = cbxDH1Shift.SelectedIndex;
            row["LogNote"] = null;
            dacTimebook.FindAdd(new object[] { book_date, row["EmpID"] }, row);
        }


        private void row_deckhand2(DataSet ds, DateTime book_date)
        {
            DataRow row;

            if (cbxDh2.SelectedIndex < 0) return;
            
            row = ds.Tables[0].NewRow();
            row["BookDate"] = book_date;
            row["EmpID"] = cbxDh2.SelectedValue;
            row["EmpName"] = cbxDh2.Text;
            row["ToffCode"] = null;
            row["LogHours"] = needDecimal(tbxD2A);
            row["LogOver"] = needDecimal(tbxD2O);
            row["LogVessel"] = cbxShips.SelectedValue;
            row["LogShift"] = cbxDH2Shift.SelectedIndex;
            row["LogNote"] = null;
            dacTimebook.FindAdd(new object[] { book_date, row["EmpID"] }, row);
        }


        private void row_mate(DataSet ds, DateTime book_date)
        {
            DataRow row;

            if (cbxMate.SelectedIndex < 0) return;
            
            row = ds.Tables[0].NewRow();
            row["BookDate"] = book_date;
            row["EmpID"] = cbxMate.SelectedValue;
            row["EmpName"] = cbxMate.Text;
            row["ToffCode"] = null;
            row["LogHours"] = needDecimal(tbxMaA);
            row["LogOver"] = needDecimal(tbxMaO);
            row["LogVessel"] = cbxShips.SelectedValue;
            row["LogShift"] = cbxMaShift.SelectedIndex;
            row["LogNote"] = null;
            dacTimebook.FindAdd(new object[] { book_date, row["EmpID"] }, row);
        }


        private void row_engineer(DataSet ds, DateTime book_date)
        {
            DataRow row;

            if (cbxEng.SelectedIndex < 0) return;
            
            row = ds.Tables[0].NewRow();
            row["BookDate"] = book_date;
            row["EmpID"] = cbxEng.SelectedValue;
            row["EmpName"] = cbxEng.Text;
            row["ToffCode"] = null;
            row["LogHours"] = needDecimal(tbxEnA);
            row["LogOver"] = needDecimal(tbxEnO);
            row["LogVessel"] = cbxShips.SelectedValue;
            row["LogShift"] = cbxEnShift.SelectedIndex;
            row["LogNote"] = null;
            dacTimebook.FindAdd(new object[] { book_date, row["EmpID"] }, row);

        }


        private void row_C6(DataSet ds, DateTime book_date)
        {
            DataRow row;

            if (cbxC6.SelectedIndex < 0) return;

            row = ds.Tables[0].NewRow();
            row["BookDate"] = book_date;
            row["EmpID"] = cbxC6.SelectedValue;
            row["EmpName"] = cbxC6.Text;
            row["ToffCode"] = null;
            row["LogHours"] = needDecimal(tbxC6A);
            row["LogOver"] = needDecimal(tbxC6O);
            row["LogVessel"] = cbxShips.SelectedValue;
            row["LogShift"] = cbxC6Shift.SelectedIndex;
            row["LogNote"] = null;
            dacTimebook.FindAdd(new object[] { book_date, row["EmpID"] }, row);

        }


        private void do_save_delete()
        {
            DataSet ds = null;

            if (_deleted)
            {
                foreach (DataRow row in _dt_time.Rows)
                {
                    if (ds == null)  ds = dacTimebook.GetDS((DateTime)row["BookDate"], 1);
                    if ((bool)row["DelMark"])     
                        dacTimebook.FindDel(new object[] { row["BookDate"], row["EmpId"] });                    
                }

                dacTimebook.DeleteData();
                dacCache.PutTimebook();
                _deleted = false;
            }

            if (_dirty)
            {
                DateTime book_date = dtpLogDate.Value.Date;

                if (ds == null)  ds = dacTimebook.GetDS(book_date, 1);

                row_captain(ds, book_date);
                row_deckhand1(ds, book_date);
                row_deckhand2(ds, book_date);
                row_mate(ds, book_date);
                row_engineer(ds, book_date);
                row_C6(ds, book_date);

                dacTimebook.SaveData();
                dacCache.PutTimebook();

                _dirty = false;
            }

            MessageBox.Show("Saved !");
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Button cmd = (Button)sender;

            if (cmd.Text.Equals("OK"))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }


        private void cmdOK_Click(object sender, EventArgs e)
        {
            Button cmd = (Button)sender;
            
            if (cmd.Text.Equals("Save"))
            {
                if (! _dirty && ! _deleted)
                    errorProvider1.SetError(cmdOK, "Error (Save) : Please add crew member !");
                else
                {
                    do_save_delete();
                                 
                    frmLog_Load(null, null);
                    form_vessel(tbxSelVessel.Text, false);
                    form_readonly();
                    cmdOK.Hide();

                    //this.DialogResult = DialogResult.OK;
                    //this.Close();
                }
            }
            else if (cmd.Text.Equals("Edit"))
            {
                form_edit();
            }                
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void cycle_role(object sender)
        {
            string role = ((Button)sender).Text;
            switch (role)
            {
                case "Capt": role = "Mate"; break;
                case "Mate": role = "Eng."; break;
                case "Eng.": role = "DH(1)"; break;
                case "DH(1)": role = "DH(2)"; break;
                case "DH(2)": role = "DH(3)"; break;
                case "DH(3)": role = "DH(4)"; break;
                case "DH(4)": role = "Capt"; break;
            }
            ((Button)sender).Text = role;
        }

        //Audit-GS170605
        private void load_gang(ComboBox cbx, string button_name)
        {
            string gang_name = "Master";

            if (button_name.Equals("DH(1)")) gang_name = "Deckhand";
            if (button_name.Equals("DH(2)")) gang_name = "Deckhand";
            if (button_name.Equals("DH(3)")) gang_name = "Deckhand";
            if (button_name.Equals("DH(4)")) gang_name = "Deckhand";
            //if (button_name.Equals("Mate")) gang_name = "Master";
            //if (button_name.Equals("Mate(2)")) gang_name = "Master";
            if (button_name.Equals("Eng")) gang_name = "Deckhand";
            

            DataTable dt = qryGang.GetView(gang_name);
            cbx.BindingContext = new BindingContext();
            cbx.DataSource = dt;
            cbx.DisplayMember = "EmpName";
            cbx.ValueMember = "EmpID";
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void cmdMate_to_Capt()
        {
            cmdC4.Text = "Capt";
            load_gang(cbxMate, "Mate");
        }


        private void cmdEng_to_Dh3()
        {
            cmdEng.Text = "DH(3)";
            load_gang(cbxEng, "DH(3)");
        }


        private void cmdCapt_Click(object sender, EventArgs e)
        {
            cycle_role(sender);
            lblCapt.Text = ((Button)sender).Text;

            load_gang(cbxCapt, ((Button)sender).Text);
        }

        private void cmdMate_Click(object sender, EventArgs e)
        {
            cycle_role(sender);
            lblMate.Text = ((Button)sender).Text;

            load_gang(cbxMate, ((Button)sender).Text);
        }

        private void cmdEng_Click(object sender, EventArgs e)
        {
            cycle_role(sender);
            lblEng.Text = ((Button)sender).Text;

            load_gang(cbxEng, ((Button)sender).Text);
        }

        private void cmdDH1_Click(object sender, EventArgs e)
        {
            cycle_role(sender);
            lblDH1.Text = ((Button)sender).Text;

            load_gang(cbxDh1, ((Button)sender).Text);
        }

        private void cmdDH2_Click(object sender, EventArgs e)
        {
            cycle_role(sender);
            lblDH2.Text = ((Button)sender).Text;

            load_gang(cbxDh2, ((Button)sender).Text);
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void tbxV1Ca_TextChanged(object sender, EventArgs e)
        {
            _dirty = true;
            tbxV1A.Text = vessel_hours().ToString();
        }

        private void tbxV1Ma_TextChanged(object sender, EventArgs e)
        {
            _dirty = true;
            tbxV1A.Text = vessel_hours().ToString();
        }

        private void tbxV1En_TextChanged(object sender, EventArgs e)
        {
            _dirty = true;
            tbxV1A.Text = vessel_hours().ToString();
        }

        private void tbxV1D1_TextChanged(object sender, EventArgs e)
        {
            _dirty = true;
            tbxV1A.Text = vessel_hours().ToString();
        }

        private void tbxV1D2_TextChanged(object sender, EventArgs e)
        {
            _dirty = true;
            tbxV1A.Text = vessel_hours().ToString();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void tbxCpA_TextChanged(object sender, EventArgs e)
        {
            decimal actual;
            decimal paid;
            decimal premium;

            bool r = Decimal.TryParse(((TextBox)sender).Text, out actual);
            if (!r) return;

            _dirty = true;
            tbxV1Ca.Text = actual.ToString();

            if (!actual.Equals(12M))
                chkCapt.Checked = false;
            else
                chkCapt.Checked = true;
                        
            r = Decimal.TryParse(tbxCpP.Text, out paid);
            if (!r) return;

            if (actual < paid) return;

            r = Decimal.TryParse(tbxCpO.Text, out premium);         

            if (!actual.Equals(12M) && paid.Equals(12M)) premium = actual - paid;
            tbxCpO.Text = premium.ToString();
        }


        private void tbxD1A_TextChanged(object sender, EventArgs e)
        {
            decimal actual;
            decimal paid;
            decimal premium;

            bool r = Decimal.TryParse(((TextBox)sender).Text, out actual);
            if (!r) return;

            _dirty = true;
            tbxV1D1.Text = actual.ToString();

            if (!actual.Equals(12M))
                chkDH1.Checked = false;
            else
                chkDH1.Checked = true;

            r = Decimal.TryParse(tbxD1P.Text, out paid);
            if (!r) return;

            if (actual < paid) return;

            r = Decimal.TryParse(tbxD1O.Text, out premium);

            if (!actual.Equals(12M) && paid.Equals(12M)) premium = actual - paid;
            tbxD1O.Text = premium.ToString();

        }
        
        private void tbxD2A_TextChanged(object sender, EventArgs e)
        {
            _dirty = true;
            tbxV1D2.Text = ((TextBox)sender).Text;
        }

        private void tbxMaA_TextChanged(object sender, EventArgs e)
        {
            _dirty = true;
            tbxV1Ma.Text = ((TextBox)sender).Text;
        }

        private void tbxEnA_TextChanged(object sender, EventArgs e)
        {
            _dirty = true;
            tbxV1En.Text = ((TextBox)sender).Text;
        }

        private void tbxC6A_TextChanged(object sender, EventArgs e)
        {
            _dirty = true;
            tbxV1C6.Text = ((TextBox)sender).Text;
        }



        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void tbxCpP_TextChanged(object sender, EventArgs e)
        {
            _dirty = true;
        }

        private void tbxD1P_TextChanged(object sender, EventArgs e)
        {
            _dirty = true;
        }

        private void tbxD2P_TextChanged(object sender, EventArgs e)
        {
            _dirty = true;
        }

        private void tbxMaP_TextChanged(object sender, EventArgs e)
        {
            _dirty = true;
        }

        private void tbxEnP_TextChanged(object sender, EventArgs e)
        {
            _dirty = true;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void tbxCpO_TextChanged(object sender, EventArgs e)
        {
            _dirty = true;
        }

        private void tbxD1O_TextChanged(object sender, EventArgs e)
        {
            _dirty = true;
        }

        private void tbxD2O_TextChanged(object sender, EventArgs e)
        {
            _dirty = true;
        }

        private void tbxMaO_TextChanged(object sender, EventArgs e)
        {
            _dirty = true;
        }

        private void tbxEnO_TextChanged(object sender, EventArgs e)
        {
            _dirty = true;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void nudShift_ValueChanged(object sender, EventArgs e)
        {
            cbxShipShift.SelectedIndex = (int)nudShift.Value;  
        }

        private void cbxShipShift_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxCapt.Text.Equals("")) cbxCaShift.SelectedIndex = (int)nudShift.Value;
            if (cbxDh1.Text.Equals("")) cbxDH1Shift.SelectedIndex = (int)nudShift.Value;
            if (cbxDh2.Text.Equals("")) cbxDH2Shift.SelectedIndex = (int)nudShift.Value;
            if (cbxMate.Text.Equals("")) cbxMaShift.SelectedIndex = (int)nudShift.Value;
            if (cbxEng.Text.Equals("")) cbxEnShift.SelectedIndex = (int)nudShift.Value;
            if (cbxC6.Text.Equals("")) cbxC6Shift.SelectedIndex = (int)nudShift.Value;
        }

        private void cbxShipShift_SelectionChangeCommitted(object sender, EventArgs e)
        {
            nudShift.Value = cbxShipShift.SelectedIndex;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private Button mk_log_button(string log_number)
        {
            Button cmd = new Button();
            cmd.BackColor = SystemColors.Control;
            cmd.Width = 60;

            cmd.Text = "#" + log_number;

            return cmd;
        }

        
        private string sm_logs(string data)
        {
            int num;
            string ret = "Error";

            if (data.Equals("Engine Hrs. Start")) data = "0";
            if (data.Equals("Engine Hrs Finish")) data = "0";
            if (data.Equals("Fuel Taken")) data = "0";
            if (data.Equals("Master Actual")) data = "0";
            if (data.Equals("Deckhand Actual")) data = "0";
            if (data.Equals("Master Premium")) data = "0";
            if (data.Equals("Deckhand Premium")) data = "0";

            bool r = Int32.TryParse(data, out num);


            switch (sm_state)
            {
                case 1:
                    //flpLogs.Controls.Add(mk_log_button(data));
                    lbxAudit.Items.Add("Log # : " + data);
                    cur_lognumber = data;
                    lblLog.Text = data;
                    sm_state += 1;
                    ret = "Vessel";
                    break;

                case 2:                    
                    lbxAudit.Items.Add("Vessel : " + data);
                    cbxShips.SelectedValue = UC_VES.Key();
                    sm_state += 1;
                    ret = "Vessel Shift";
                    break;

                case 3:
                    lbxAudit.Items.Add("Shift : " + data);
                    //cbxShipShift.SelectedIndex = 1;
                    //cbxShipShift.SelectedValue = UC_SHF.Key();
                    lblShift.Text = data;
                    nudShift.Value = (int)UC_SHF.Key();

                    sm_state += 1;
                    ret = "Engine Hrs. Start";
                    break;

                case 4 :
                    lbxAudit.Items.Add("Engine Hrs. Start : " + data);
                    lblHourStart.Text = data;
                    if (r) lblHourStart.Text = num.ToString();
                    sm_state += 1;
                    ret = "Engine Hrs Finish";
                    break;
                    
                case 5:
                    lbxAudit.Items.Add("Engine Hrs Finish : " + data);
                    sm_state += 1;
                    lblHourFinish.Text = data;
                    if (r) lblHourFinish.Text = num.ToString();
                    ret = "Fuel Taken";
                    break;

                case 6:
                    lbxAudit.Items.Add("Fuel Taken : " + data);
                    sm_state += 1;
                    lblFuel.Text = data;
                    if (r) lblFuel.Text = num.ToString();
                    UC_EMP.PutBinding("Master");                    
                    ret = "Master";
                    break;

                case 7:
                    lbxAudit.Items.Add("Master : " + data);

                    if (cbxCapt.SelectedIndex < 0)
                    {
                        cbxCapt.SelectedValue = UC_EMP.Key();
                        tbxCaptSheet.Text = cur_lognumber;
                    }
                    else
                    {
                        cmdMate_to_Capt();
                        cbxMate.SelectedValue = UC_EMP.Key();
                        tbxMateSheet.Text = cur_lognumber;
                    }
                        
                    sm_state += 1;
                    ret = "Master Actual";
                    break;

                case 8:
                    lbxAudit.Items.Add("Hrs Actual : " + data);
                    tbxCpA.Text = data;
                    if (r) tbxCpA.Text = num.ToString();
                    sm_state += 1;
                    ret = "Master Premium";
                    break;

                case 9:
                    lbxAudit.Items.Add("Hrs Overtime : " + data);
                    tbxCpO.Text = data;
                    if (r) tbxCpO.Text = num.ToString();
                    sm_state += 1;
                    UC_EMP.PutBinding("Deckhand");                    
                    ret = "Deckhand";
                    break;

                case 10:
                    lbxAudit.Items.Add("Deckhand : " + data);
                    if (cbxDh1.SelectedIndex < 0)
                    {
                        cbxDh1.SelectedValue = UC_EMP.Key();
                        tbxDh1Sheet.Text = cur_lognumber; ;
                    }
                    else if (cbxDh2.SelectedIndex < 0)
                    {
                        cbxDh2.SelectedValue = UC_EMP.Key();
                        tbxDh2Sheet.Text = cur_lognumber;
                    }
                    else
                    {
                        cmdEng_to_Dh3();
                        cbxEng.SelectedValue = UC_EMP.Key();
                        tbxEngSheet.Text = cur_lognumber;
                    }

                    sm_state += 1;
                    ret = "Deckhand Actual";
                    break;

                case 11:
                    lbxAudit.Items.Add("Hrs Actual : " + data);
                    tbxD1A.Text = data;
                    if (r) tbxD1A.Text = num.ToString();
                    sm_state += 1;
                    ret = "Deckhand Premium";
                    break;

                case 12:
                    lbxAudit.Items.Add("Hrs Overtime : " + data);
                    lbxAudit.Items.Add("----------------------------------");
                    lbxAudit.Items.Add("");
                    tbxD1O.Text = data;
                    if (r) tbxD1O.Text = num.ToString();
                    sm_state = 1;
                    ret = "Log #";
                    break;


            }

            return ret;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void del(string eid)
        {            
            foreach (DataRow row in _dt_time.Rows)
            {
                //if (row.RowState == DataRowState.Deleted) continue;
                if ((bool)row["DelMark"]) continue;

                if (row["EmpId"].Equals(eid))
                {
                    //DateTime book_date = (DateTime)row["BookDate"];
                    //DataSet ds = dacTimebook.GetDS(book_date, 1);
                    //dacTimebook.FindDel(new object[] { book_date, (string)row["EmpId"] });

                    //row.Delete();
                    row["DelMark"] = true;
                    //_dt_time.AcceptChanges();
                    _deleted = true;
                    return;
                }
            }
        }        

                
        private void lblDel1_Click(object sender, EventArgs e)
        {
            del(tbxCapt.Text);            
            form_vessel(tbxSelVessel.Text,true);
            tbxCpA.Text = "0";
            refresh_totals();
            lblDel1.Hide();
            cmdOK.Show();
        }

        private void lblDel2_Click(object sender, EventArgs e)
        {
            del(tbxDh1.Text);
            form_vessel(tbxSelVessel.Text, true);
            tbxD1A.Text = "0";
            refresh_totals();
            lblDel2.Hide();
            cmdOK.Show();
        }

        private void lblDel3_Click(object sender, EventArgs e)
        {
            del(tbxDh2.Text);
            form_vessel(tbxSelVessel.Text, true);
            tbxD2A.Text = "0";
            refresh_totals();
            lblDel3.Hide();
            cmdOK.Show();
        }

        private void lblDel4_Click(object sender, EventArgs e)
        {
            del(tbxMate.Text);
            form_vessel(tbxSelVessel.Text, true);
            tbxMaA.Text = "0";
            refresh_totals();
            lblDel4.Hide();
            cmdOK.Show();
        }

        private void lblDel5_Click(object sender, EventArgs e)
        {
            del(tbxEng.Text);
            form_vessel(tbxSelVessel.Text, true);
            tbxEnA.Text = "0";
            refresh_totals();
            lblDel5.Hide();
            cmdOK.Show();
        }

        private void lblDel6_Click(object sender, EventArgs e)
        {
            del(tbxC6.Text);
            form_vessel(tbxSelVessel.Text, true);
            tbxC6A.Text = "0";
            refresh_totals();
            lblDel6.Hide();
            cmdOK.Show();
        }



        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void cmdLogs_Click(object sender, EventArgs e)
        {
            dgvTB.Hide();
            cmdLogs.Hide();

            lblLog.Show();
            lblShift.Show();
            lblHourStart.Show();
            lblHourFinish.Show();
            lblFuel.Show();

            this.Width += adjScreen;
            

            if (UC_EMP == null)
            {
                UC_EMP = new ucEmpNum();
                pnlEmp.Controls.Add(UC_EMP);
            }
            
            if (UC_VES == null)
            {
                UC_VES = new ucVesNum();
                pnlVes.Controls.Add(UC_VES);
            }

            if (UC_SHF == null)
            {
                UC_SHF = new ucShfNum();
                pnlShf.Controls.Add(UC_SHF);
            }

            pnlEmp.Hide();
            pnlVes.Hide();
            pnlShf.Hide();

            tbxEnter.Focus();
        }

        static ucEmpNum UC_EMP = null;
        static ucVesNum UC_VES = null;
        static ucShfNum UC_SHF = null;

        private void tbxEnter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                string prompt = sm_logs(tbxEnter.Text);
                tbxEnter.Text = prompt;
                tbxEnter.SelectAll();

                if (tbxEnter.Text.Equals("Master") || tbxEnter.Text.Equals("Deckhand"))
                    pnlEmp.Show();
                else
                    pnlEmp.Hide();

                
                if (tbxEnter.Text.Equals("Vessel"))
                    pnlVes.Show();
                else
                    pnlVes.Hide();


                if (tbxEnter.Text.Equals("Vessel Shift"))
                    pnlShf.Show();
                else
                    pnlShf.Hide();

            }
        }

        private void tbxEnter_TextChanged(object sender, EventArgs e)
        {
            if (pnlEmp.Visible)
                UC_EMP.Criteria(tbxEnter.Text);

            if (pnlVes.Visible)
                UC_VES.Criteria(tbxEnter.Text);

            if (pnlShf.Visible)
                UC_SHF.Criteria(tbxEnter.Text);

        }

        private void cmdEnter_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Click the <Enter> key when the textbox has focus",
                "Warning",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            tbxEnter.Focus();
        }

        private void cmdNew_Click(object sender, EventArgs e)
        {
            form_clear();            
            form_edit();
            pnlPM.Hide();

            cbxShips.SelectedIndex = -1;
        }

        private void cmdGrid_Click(object sender, EventArgs e)
        {
            Button cmd = (Button)sender;

            if (cmd.Text.Equals("V"))
            {
                dgvTB.Location = gbxCrew.Location;
                dgvTB.Size = new Size(gbxCrew.Width, gbxCrew.Height);
                dgvTB.BringToFront();

                dgvTB.Show();
                cmd.Text = "^";
            }
            else
            {
                dgvTB.Hide();
                cmd.Text = "V";
            }
        }

    }
}
