using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using mdlAllyKE;


namespace viewLogKE
{
    interface CueLog { void Msg(); }

    public partial class ucLog : UserControl
    {
        public bool CueRequired { get; set; }
        public string SheetID { get; set; }

        public int Shift { get; set; }
        public string Vessel { get; set; }

        public bool Dirty { get; set; }

        public DateTime BookDate { get; set; }
        public DataTable Timebook { get; set; }
        

        public Decimal CaptHours { get; set; }
        public Decimal Dkh1Hours { get; set; }
        public Decimal Dkh2Hours { get; set; }
        public Decimal Dkh3Hours { get; set; }
        public Decimal MateHours { get; set; }
        public Decimal EngrHours { get; set; }
        public Decimal TotalHours { get; set; }

        public int CrewCount { get; set; }
        public Decimal OnVessel { get; set; }
        public Decimal CrewHours { get; set; }
        public Decimal OverHours { get; set; }
        public Decimal PaidHours { get; set; }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private bool _dirty = false;
        private bool _deleted = false;
        private bool _edit = false;

        private void make_dirty()
        {
            if (_edit)  Dirty = true;
            if (CueRequired)
                ((CueLog)this.ParentForm).Msg();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void startup()
        {
            DataTable dt;

            dt = qryGang.GetView("Master");
            cbxCapt.DataSource = dt;
            cbxCapt.DisplayMember = "EmpName";
            cbxCapt.ValueMember = "EmpID";
            //cbxCapt.SelectedIndex = -1;            

            dt = qryGang.GetView("Deckhand");
            cbxDh1.DataSource = dt;
            cbxDh1.DisplayMember = "EmpName";
            cbxDh1.ValueMember = "EmpID";
            //cbxDh1.SelectedIndex = -1;

            cbxDh2.BindingContext = new BindingContext();
            cbxDh2.DataSource = dt;
            cbxDh2.DisplayMember = "EmpName";
            cbxDh2.ValueMember = "EmpID";
            //cbxDh2.SelectedIndex = -1;

            dt = qryGang.GetView("Master");
            cbxMate.DataSource = dt;
            cbxMate.DisplayMember = "EmpName";
            cbxMate.ValueMember = "EmpID";
            //cbxMate.SelectedIndex = -1;

            dt = qryGang.GetView("Deckhand");
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

            cbxCaShift.DataSource = dacCache.GetShift();
            cbxCaShift.BindingContext = new BindingContext();
            cbxCaShift.DisplayMember = "Short";
            cbxCaShift.ValueMember = "NumID";
            cbxCaShift.SelectedIndex = 1;

            cbxDH1Shift.DataSource = dacCache.GetShift();
            cbxDH1Shift.BindingContext = new BindingContext();
            cbxDH1Shift.DisplayMember = "Short";
            cbxDH1Shift.ValueMember = "NumID";
            cbxDH1Shift.SelectedIndex = 1;

            cbxDH2Shift.DataSource = dacCache.GetShift();
            cbxDH2Shift.BindingContext = new BindingContext();
            cbxDH2Shift.DisplayMember = "Short";
            cbxDH2Shift.ValueMember = "NumID";
            cbxDH2Shift.SelectedIndex = 1;

            cbxMaShift.DataSource = dacCache.GetShift();
            cbxMaShift.BindingContext = new BindingContext();
            cbxMaShift.DisplayMember = "Short";
            cbxMaShift.ValueMember = "NumID";
            cbxMaShift.SelectedIndex = 1;

            cbxEnShift.DataSource = dacCache.GetShift();
            cbxEnShift.BindingContext = new BindingContext();
            cbxEnShift.DisplayMember = "Short";
            cbxEnShift.ValueMember = "NumID";
            cbxEnShift.SelectedIndex = 1;

            cbxC6Shift.DataSource = dacCache.GetShift();
            cbxC6Shift.BindingContext = new BindingContext();
            cbxC6Shift.DisplayMember = "Short";
            cbxC6Shift.ValueMember = "NumID";
            cbxC6Shift.SelectedIndex = 1;

            tbxCaptSheet.Hide();
            tbxDh1Sheet.Hide();
            tbxDh2Sheet.Hide();
            tbxMateSheet.Hide();
            tbxEngSheet.Hide();
            tbxC6Sheet.Hide();

        }


        public ucLog()
        {
            InitializeComponent();

            CueRequired = false;
            Dirty = false;

            startup();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

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

            //refresh_totals();

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

            //refresh_totals();
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

            tbxVCa.Text = "";;
            tbxVMa.Text = "";
            tbxVEn.Text = "";
            tbxVD1.Text = "";
            tbxVD2.Text = "";

            tbxVA.Focus();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public void ShowSheet()
        {
            tbxCaptSheet.Show();
            tbxDh1Sheet.Show();
            tbxDh2Sheet.Show();
            tbxMateSheet.Show();
            tbxEngSheet.Show();
            tbxC6Sheet.Show();
        }
        

        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public void NewShift(int shift)
        {
            //shift = (int)nudShift.Value;

            if (cbxCapt.Text.Equals("") || cbxCaShift.SelectedIndex == 0) cbxCaShift.SelectedIndex = shift;
            if (cbxDh1.Text.Equals("") || cbxDH1Shift.SelectedIndex == 0) cbxDH1Shift.SelectedIndex = shift;
            if (cbxDh2.Text.Equals("") || cbxDH2Shift.SelectedIndex == 0) cbxDH2Shift.SelectedIndex = shift;
            if (cbxMate.Text.Equals("") || cbxMaShift.SelectedIndex == 0) cbxMaShift.SelectedIndex = shift;
            if (cbxEng.Text.Equals("") || cbxEnShift.SelectedIndex == 0) cbxEnShift.SelectedIndex = shift;
            if (cbxC6.Text.Equals("") || cbxC6Shift.SelectedIndex == 0) cbxC6Shift.SelectedIndex = shift;
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
                //lblDel1.Show();

                //CaptHours = hour;
            }
            else if (idx == 2)
            {
                cbxMate.Text = (string)row["EmpName"];
                tbxMate.Text = cbxMate.SelectedValue.ToString();
                cmdC4.Text = "Capt";
                //lblMate.Text = "Capt";
                tbxMaA.Text = hour.ToString("#.#");
                tbxMaO.Text = over.ToString("#.#");
                tbxMaP.Text = (hour >= 12M ? 12M : hour).ToString("#.#");
                chkMate.Checked = hour.Equals(12M);
                cbxMaShift.SelectedIndex = (int)row["Shift"];
                //cbxMaShift.Text = (string)row["ShiftHour"];

                set_captain(tbxMaA, tbxMaP, tbxMaO, chkMate.Checked);
                //lblDel4.Show();

                //MateHours = hour;
            }
            else if (idx == 3)
            {
                if (dh_idx < 3)
                {
                    cbxEng.Text = (string)row["EmpName"];
                    tbxEng.Text = cbxEng.SelectedValue.ToString();
                    cmdC5.Text = "Capt";
                    //lblEng.Text = "Capt";
                    tbxEnA.Text = hour.ToString("#.#");
                    tbxEnO.Text = over.ToString("#.#");
                    tbxEnP.Text = (hour >= 12M ? 12M : hour).ToString("#.#");
                    chkEng.Checked = hour.Equals(12M);
                    cbxEnShift.SelectedIndex = (int)row["Shift"];
                    //cbxEnShift.Text = (string)row["ShiftHour"];
                
                    set_captain(tbxEnA, tbxEnP, tbxEnO, chkEng.Checked);
                    //lblDel5.Show();

                    //EngrHours = hour;
                }
            }
            //else if (idx == 4)
            //{
            //    if (dh_idx < 4)
            //    {
            //        cbxC6.Text = (string)row["EmpName"];
            //        tbxC6.Text = cbxC6.SelectedValue.ToString();
            //        cmdC6.Text = "Capt";
            //        //lblC6.Text = "Capt";
            //        tbxC6A.Text = hour.ToString("#.#");
            //        tbxC6O.Text = over.ToString("#.#");
            //        tbxC6P.Text = (hour >= 12M ? 12M : hour).ToString("#.#");
            //        chkC6.Checked = hour.Equals(12M);
            //        cbxC6Shift.SelectedIndex = (int)row["Shift"];
            //        //cbxEnShift.Text = (string)row["ShiftHour"];
            //        set_captain(tbxC6A, tbxC6P, tbxC6O, chkC6.Checked);

            //        Dkh3Hours = hour;
            //    }
            //}
        }


        private void put_deckhand(int idx, DataRow row, int skip_idx, bool index_0)
        {
            Decimal hour = (Decimal)row["Hours"];
            Decimal over = (Decimal)row["Over"];

            //if (!index_0)
            //{
            //    bool am = ((int)row["Shift"] == 1);
            //    if (!am idx = (idx == 3 ? 4 : idx);
            //    if (!am) idx = (idx == 2 ? 3 : 2);
            //}

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
                //lblDel2.Show();

                //Dkh1Hours = hour;
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
                //lblDel3.Show();

                //Dkh2Hours = hour;
            }
            else if (idx == 3)
            {
                if (skip_idx < 3)
                {
                    cbxC6.Text = (string)row["EmpName"];
                    tbxC6.Text = cbxC6.SelectedValue.ToString();
                    cmdC6.Text = "DH(3)";
                    //lblC6.Text = "DH(3)";
                    tbxC6A.Text = hour.ToString("#.#");
                    tbxC6O.Text = over.ToString("#.#");
                    tbxC6P.Text = (hour >= 12M ? 12M : hour).ToString("#.#");
                    chkC6.Checked = hour.Equals(12M);
                    cbxC6Shift.SelectedIndex = (int)row["Shift"];
                    //cbxC6Shift.Text = (string)row["ShiftHour"];
                    
                    set_standard(tbxC6A, tbxC6P, tbxC6O, chkC6.Checked);
                    //lblDel6.Show();

                    //Dkh3Hours = hour;
                }
            }
            //else if (idx == 4)
            //{
            //    if (skip_idx < 4)
            //    {
            //        cbxEng.Text = (string)row["EmpName"];
            //        tbxEng.Text = cbxEng.SelectedValue.ToString();
            //        cmdC5.Text = "DH(4)";
            //        //lblEng.Text = "DH(4)";
            //        tbxEnA.Text = hour.ToString("#.#");
            //        tbxEnO.Text = over.ToString("#.#");
            //        tbxEnP.Text = (hour >= 12M ? 12M : hour).ToString("#.#");
            //        chkEng.Checked = hour.Equals(12M);
            //        cbxEnShift.SelectedIndex = (int)row["Shift"];
            //        //cbxEnShift.Text = (string)row["ShiftHour"];
            //        set_standard(tbxEnA, tbxEnP, tbxEnO, chkEng.Checked);

            //        EngrHours = hour;
            //    }
            //}

        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void form_vessel(string vessel, bool _edit)
        {
            //bool _readonly = false;

            //int count_skipper = 0;
            //int count_deckhand = 0;
            //int count_other = 0;
            //int count_records = 0;

            //if (vessel == null || vessel.Length == 0) return;


            //form_clear();

            //bool index_0 = scan_index_for_0(vessel);
            //Dictionary<int, int> shifts = scan_index_for_shift(vessel);
            //TabPage tp = tabShift.TabPages[0];
            //tabShift.TabPages.Clear();
            //tabShift.TabPages.Add(tp);
            //foreach (int s in shifts.Keys)
            //{
            //    if (s == 0) tabShift.TabPages[0].Text = "<N/A>";
            //    if (s == 1)
            //    {
            //        tabShift.TabPages[0].Text = "AM";
            //        if (index_0) tabShift.TabPages[0].Text = "AM & N/A";
            //    }
            //    if (s == 2) tabShift.TabPages.Add("PM");
            //    if (s == 3) tabShift.TabPages.Add("PT");
            //    if (s == 4) tabShift.TabPages.Add("24 Hour");
            //}

            //foreach (DataRow row in _dt_time.Rows)
            //{
            //    //if (row.RowState == DataRowState.Deleted) continue;
            //    if ((bool)row["DelMark"]) continue;

            //    if (vessel.Equals(row["Vessel"]))
            //    {
            //        _readonly = true;

            //        cbxShips.SelectedValue = (string)row["Vessel"];
            //        tbxSelVessel.Text = cbxShips.SelectedValue.ToString();                    

            //        count_records += 1;
            //        if (row["Duty"].Equals("SKIPPER"))
            //        {
            //            count_skipper += 1;
            //            put_captain(count_skipper, row, count_deckhand);
            //            //MessageBox.Show(string.Format("[{0}], {1}", cbxCaShift.SelectedIndex,cbxCaShift.Text));
            //        }
            //        else if (row["Duty"].Equals("DECKHAND"))
            //        {
            //            count_deckhand += 1;
            //            put_deckhand(count_deckhand, row, count_skipper, index_0);
            //        }
            //        else
            //        {
            //            count_deckhand += 1;
            //            put_deckhand(count_deckhand, row, count_skipper, index_0);
            //            count_other += 1;
            //            MessageBox.Show(string.Format("Warning (form_vessel) : Employee Duty [{0}] @ [{1}])",
            //                (string)row["Duty"], (string)row["EmpName"]));

            //        }
            //    }
            //}

            //if (count_records >= 5 || count_skipper > 2 || count_deckhand > 3 || count_other > 0)
            //    MessageBox.Show(string.Format("Error (cmdVessel_click) : Too many records @ [{0}], Skippers[{1}] Deckhands[{2}] Other[{3}]",
            //        count_records, count_skipper, (count_other > 0 ? count_deckhand - count_other : count_deckhand), count_other));

            //tbxCrewCount.Text = string.Format("[{0}]", count_records.ToString());
            //tbxV1A.Text = vessel_hours().ToString();

            //cmdNew.Show();
            ////pnlPM.Show();
        }


        private void vessel_render()
        {
            int count_records = 0;
            int count_other = 0;
            int count_skipper = 0;
            int count_deckhand = 0;            

            foreach (DataRow row in Timebook.Rows)
            {
                int shift = (int)row["Shift"];

                if (!Vessel.Equals(row["Vessel"])) continue;
                if (shift != 0 && Shift != shift) continue;
                if (shift == 0 && Shift > 1) continue;

                count_records += 1;
                if (row["Duty"].Equals("SKIPPER"))
                {
                    count_skipper += 1;
                    put_captain(count_skipper, row, count_deckhand);
                    //MessageBox.Show(string.Format("[{0}], {1}", cbxCaShift.SelectedIndex,cbxCaShift.Text));
                }
                else if (row["Duty"].Equals("DECKHAND"))
                {
                    count_deckhand += 1;
                    //put_deckhand(count_deckhand, row, count_skipper, index_0);
                    put_deckhand(count_deckhand, row, count_skipper, false);
                }
                else
                {
                        count_deckhand += 1;
                        //put_deckhand(count_deckhand, row, count_skipper, index_0);
                        put_deckhand(count_deckhand, row, count_skipper, false);
                        count_other += 1;
                        MessageBox.Show(string.Format("Warning (form_vessel) : Employee Duty [{0}] @ [{1}])",
                            (string)row["Duty"], (string)row["EmpName"]));
                }                
            }

            if (count_records >= 5 || count_skipper > 2 || count_deckhand > 3 || count_other > 0)
                MessageBox.Show(string.Format("Error (cmdVessel_click) : Too many records @ [{0}], Skippers[{1}] Deckhands[{2}] Other[{3}]",
                    count_records, count_skipper, (count_other > 0 ? count_deckhand - count_other : count_deckhand), count_other));

            //tbxCrewCount.Text = string.Format("[{0}]", count_records.ToString());
            //tbxV1A.Text = vessel_hours().ToString();

            //cmdNew.Show();
            ////pnlPM.Show();

        }

        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public void ModeReady()
        {
            startup();
            ModeClear();
        }


        public void ModeClear()
        {            
            Dirty = false;
            _deleted = false;
            _edit = false;

            //cmdC4.Text = "Capt";
            //cmdEng.Text = "DH(3)";
            //cmdC4.Text = "DH(3)";
            //cmdC5.Text = "Mate";
            //cmdC6.Text = "Eng.";

            cmdC1.Text = "Capt";
            cmdC2.Text = "DH(1)";
            cmdC3.Text = "DH(2)";
            cmdC4.Text = "Mate";
            cmdC5.Text = "Eng.";
            cmdC6.Text = "DH(3)";

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

            cbxCaShift.SelectedIndex = 1;
            cbxDH1Shift.SelectedIndex = 1;
            cbxDH2Shift.SelectedIndex = 1;
            cbxMaShift.SelectedIndex = 1;
            cbxEnShift.SelectedIndex = 1;
            cbxC6Shift.SelectedIndex = 1;


            vacant(tbxCpA, tbxCpP, tbxCpO);
            vacant(tbxD1A, tbxD1P, tbxD1O);
            vacant(tbxD2A, tbxD2P, tbxD2O);
            vacant(tbxMaA, tbxMaP, tbxMaO);
            vacant(tbxEnA, tbxEnP, tbxEnO);
            vacant(tbxC6A, tbxC6P, tbxC6O);

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

            errorProvider1.Clear();
        }


        public void ModeRO()
        {
            _edit = false;
            vessel_render();
            vessel_totals_from_datatable();
        }


        public void ModeEdit(bool new_flag)
        {
            Dirty = false;
            _deleted = false;
            if (! new_flag)  _edit = true;

            if (!chkCapt.Checked)
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

            trans_start();
        }


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

        private void test_unique(TextBox tbxID, ComboBox cbxShift)
        {
            int count_id = 0;

            errorProvider1.Clear();

            foreach (DataRow row in Timebook.Rows)
            {
                if (!Vessel.Equals(row["Vessel"])) continue;

                if (tbxID.Text.Equals(row["EmpId"])) count_id++;
            }

            if (tbxCapt.Text.Equals(tbxID.Text)) count_id++; 
            if (tbxDh1.Text.Equals(tbxID.Text)) count_id++;
            if (tbxDh2.Text.Equals(tbxID.Text)) count_id++;
            if (tbxMate.Text.Equals(tbxID.Text)) count_id++;
            if (tbxEng.Text.Equals(tbxID.Text)) count_id++;
            if (tbxC6.Text.Equals(tbxID.Text)) count_id++;

            if (count_id > 1) errorProvider1.SetError(cbxShift, "Warning : duplicate crew member !");            
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void cbxCapt_SelectionChangeCommitted(object sender, EventArgs e)
        {
            chkCapt.Enabled = true;
            chkCapt.Checked = true;

            
            tbxCapt.Text = cbxCapt.SelectedValue.ToString();
            test_unique(tbxCapt, cbxCaShift);



            set_standard(tbxCpA, tbxCpP, tbxCpO, chkCapt.Checked);
            lblDel1.Show();

            vessel_totals_from_form();
            make_dirty();
        }

        private void cbxDh1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            chkDH1.Enabled = true;
            chkDH1.Checked = true;

            tbxDh1.Text = cbxDh1.SelectedValue.ToString();
            test_unique(tbxDh1, cbxDH1Shift);

            set_standard(tbxD1A, tbxD1P, tbxD1O, chkDH1.Checked);
            lblDel2.Show();

            vessel_totals_from_form();
            make_dirty();
        }

        private void cbxDh2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            chkDH2.Enabled = true;
            chkDH2.Checked = true;

            tbxDh2.Text = cbxDh2.SelectedValue.ToString();
            test_unique(tbxDh2, cbxDH2Shift);

            set_standard(tbxD2A, tbxD2P, tbxD2O, chkDH2.Checked);

            //cbxDh2.BackColor = Color.LightGreen;
            //tbxDh2.BackColor = Color.LightGreen;

            lblDel3.Show();

            vessel_totals_from_form();
            make_dirty();
        }

        private void cbxMate_SelectionChangeCommitted(object sender, EventArgs e)
        {
            chkMate.Enabled = true;
            chkMate.Checked = true;

            tbxMate.Text = cbxMate.SelectedValue.ToString();
            test_unique(tbxMate, cbxMaShift);

            set_standard(tbxMaA, tbxMaP, tbxMaO, chkMate.Checked);
            lblDel4.Show();

            vessel_totals_from_form();
            make_dirty();
        }

        private void cbxEng_SelectionChangeCommitted(object sender, EventArgs e)
        {
            chkEng.Enabled = true;
            chkEng.Checked = true;

            tbxEng.Text = cbxEng.SelectedValue.ToString();
            test_unique(tbxEng, cbxEnShift);

            set_standard(tbxEnA, tbxEnP, tbxEnO, chkEng.Checked);
            lblDel5.Show();

            vessel_totals_from_form();
            make_dirty();
        }

        private void cbxC6_SelectionChangeCommitted(object sender, EventArgs e)
        {
            chkC6.Enabled = true;
            chkC6.Checked = true;

            tbxC6.Text = cbxC6.SelectedValue.ToString();
            test_unique(tbxC6, cbxC6Shift);

            set_standard(tbxC6A, tbxC6P, tbxC6O, chkC6.Checked);
            lblDel6.Show();

            vessel_totals_from_form();
            make_dirty();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private int needInt32(TextBox tbx)
        {
            string val = tbx.Text;
            int number;
            bool ok = Int32.TryParse(val, out number);
            if (!ok) return 0;

            return number;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private Decimal needDecimal(TextBox tbx)
        {
            string val = tbx.Text;
            Decimal number;
            bool ok = Decimal.TryParse(val, out number);
            if (!ok) return 0M;

            return number;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void total_item(decimal actual, decimal paid, decimal premium, int item)
        {
            CrewHours += actual;
            OverHours += premium;
            PaidHours += paid;
            CrewCount += 1;
            
            if (item == 0) CaptHours += actual;
            if (item == 1) Dkh1Hours += actual;
            if (item == 2) Dkh2Hours += actual;
            if (item == 3) MateHours += actual;
            if (item == 4) EngrHours += actual;
            if (item == 5) Dkh3Hours += actual;

            //OnVessel = CaptHours + MateHours;
            //TotalHours = CaptHours + Dkh1Hours + Dkh2Hours + MateHours + EngrHours + Dkh3Hours;
        }


        private void get_item(TextBox A, TextBox P, TextBox O, out decimal a, out decimal p, out decimal o)
        {
            a = needDecimal(A);
            p = needDecimal(P);
            o = needDecimal(O);
        }


        private void vessel_totals_from_form()
        {
            decimal actual = 0;
            decimal premium = 0;
            decimal paid = 0;

            CrewHours = 0;
            OverHours = 0;
            PaidHours = 0;
            CrewCount = 0;
            OnVessel = 0;

            CaptHours = 0;
            Dkh1Hours = 0;
            Dkh2Hours = 0;
            Dkh3Hours = 0;
            MateHours = 0;
            EngrHours = 0;

            if (tbxCapt.Text.Length > 0)
            {
                get_item(tbxCpA, tbxCpP, tbxCpO, out actual, out paid, out premium);
                total_item(actual, paid, premium, 0);
            }
            
            if (tbxDh1.Text.Length > 0)
            {
                get_item(tbxD1A, tbxD1P, tbxD1O, out actual, out paid, out premium);
                total_item(actual, paid, premium, 1);
            }

            if (tbxDh2.Text.Length > 0)
            {
                get_item(tbxD2A, tbxD2P, tbxD2O, out actual, out paid, out premium);
                total_item(actual, paid, premium, 2);
            }

            if (tbxMate.Text.Length > 0)
            {
                get_item(tbxMaA, tbxMaP, tbxMaO, out actual, out paid, out premium);
                total_item(actual, paid, premium, 3);
            }

            if (tbxEng.Text.Length > 0)
            {
                get_item(tbxEnA, tbxEnP, tbxEnO, out actual, out paid, out premium);
                total_item(actual, paid, premium, 4);
            }

            if (tbxC6.Text.Length > 0)
            {
                get_item(tbxC6A, tbxC6P, tbxC6O, out actual, out paid, out premium);
                total_item(actual, paid, premium, 5);
            }


            OnVessel = CaptHours + MateHours;
            TotalHours = CaptHours + Dkh1Hours + Dkh2Hours + MateHours + EngrHours + Dkh3Hours;
        }


        private void vessel_totals_from_datatable()
        {
            int crew_count = 0;

            Decimal crew_sum = 0M;
            Decimal skipper_sum = 0M;
            Decimal over_sum = 0M;
            Decimal paid_sum = 0M;

            Decimal capt_hour = 0M;
            Decimal dkh1_hour = 0M;
            Decimal mate_hour = 0M;
            Decimal dkh2_hour = 0M;
            Decimal dkh3_hour = 0M;
            Decimal engr_hour = 0M;

            int count_skip = 0;
            int count_deck = 0;

            foreach (DataRow row in Timebook.Rows)
            {
                if (!Vessel.Equals(row["Vessel"])) continue;

                crew_count += 1;

                Decimal hour = (Decimal)row["Hours"];
                Decimal over = (Decimal)row["Over"];
                bool skip = row["Duty"].ToString().Equals("SKIPPER");

                crew_sum += hour;
                over_sum += over;
                paid_sum += (hour >= 12M ? 12M : hour);
                if (skip) skipper_sum += hour;

                if (skip)
                {   
                    count_skip += 1;
                    if (count_skip == 1) capt_hour = hour;
                    if (count_skip == 2) capt_hour += hour;
                    if (count_skip > 2) mate_hour += hour;
                }
                else
                {
                    count_deck += 1;
                    if (count_deck == 1) dkh1_hour = hour;
                    if (count_deck == 2) dkh2_hour = hour;
                    if (count_deck > 2) dkh3_hour += hour;
                }
            }

            CrewHours = crew_sum;
            OverHours = over_sum;
            PaidHours = paid_sum;
            CrewCount = crew_count;
            OnVessel = skipper_sum;

            CaptHours = capt_hour;
            Dkh1Hours = dkh1_hour;
            Dkh2Hours = dkh2_hour;
            Dkh3Hours = dkh3_hour;
            MateHours = mate_hour;
            EngrHours = engr_hour;

            OnVessel = CaptHours + MateHours;
            TotalHours = CaptHours + Dkh1Hours + Dkh2Hours + MateHours + EngrHours + Dkh3Hours;
        }
        


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void chkCapt_CheckedChanged(object sender, EventArgs e)
        {            
            //set_captainX(tbxCpA, tbxCpP, tbxCpO, chkCapt.Checked, tbxV1A, chkCrew1.Checked);
            set_captain(tbxCpA, tbxCpP, tbxCpO, chkCapt.Checked);
            make_dirty();
        }


        private void chkDH1_CheckedChanged(object sender, EventArgs e)
        {            
            set_standard(tbxD1A, tbxD1P, tbxD1O, chkDH1.Checked);
            make_dirty();
        }

        private void chkDH2_CheckedChanged(object sender, EventArgs e)
        {            
            set_standard(tbxD2A, tbxD2P, tbxD2O, chkDH2.Checked);
            make_dirty();
        }

        private void chkMate_CheckedChanged(object sender, EventArgs e)
        {            
            set_standard(tbxMaA, tbxMaP, tbxMaO, chkMate.Checked);
            make_dirty();
        }

        private void chkEng_CheckedChanged(object sender, EventArgs e)
        {            
            set_standard(tbxEnA, tbxEnP, tbxEnO, chkEng.Checked);
            make_dirty();
        }

        private void chkC6_CheckedChanged(object sender, EventArgs e)
        {            
            set_standard(tbxC6A, tbxC6P, tbxC6O, chkC6.Checked);
            make_dirty();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void del(string eid)
        {
            DataTable _dt_time = null;

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
            //del(tbxCapt.Text);

            cbxCapt.SelectedValue = "";            

            //form_vessel(tbxSelVessel.Text, true);
            tbxCpA.Text = "0";
            tbxCpP.Text = "0";
            tbxCpO.Text = "0";

            tbxCapt.Text = "";

            //refresh_totals();
            lblDel1.Hide();
            //cmdOK.Show();

            errorProvider1.Clear();

            vessel_totals_from_form();
            make_dirty();
        }

        private void lblDel2_Click(object sender, EventArgs e)
        {
            //del(tbxDh1.Text);
            //form_vessel(tbxSelVessel.Text, true);

            cbxDh1.SelectedValue = "";            

            tbxD1A.Text = "0";            
            tbxD1P.Text = "0";
            tbxD1O.Text = "0";

            tbxDh1.Text = "";

            //refresh_totals();
            lblDel2.Hide();
            //cmdOK.Show();

            errorProvider1.Clear();

            vessel_totals_from_form();
            make_dirty();
        }

        private void lblDel3_Click(object sender, EventArgs e)
        {
            //del(tbxDh2.Text);
            //form_vessel(tbxSelVessel.Text, true);

            cbxDh2.SelectedValue = "";

            tbxD2A.Text = "0";
            tbxD2P.Text = "0";
            tbxD2O.Text = "0";

            tbxDh2.Text = "";

            //refresh_totals();
            lblDel3.Hide();
            //cmdOK.Show();

            errorProvider1.Clear();

            vessel_totals_from_form();
            make_dirty();
        }

        private void lblDel4_Click(object sender, EventArgs e)
        {
            //del(tbxMate.Text);
            //form_vessel(tbxSelVessel.Text, true);

            cbxMate.SelectedValue = "";

            tbxMaA.Text = "0";
            tbxMaP.Text = "0";
            tbxMaO.Text = "0";

            tbxMate.Text = "";

            //refresh_totals();
            lblDel4.Hide();
            //cmdOK.Show();

            errorProvider1.Clear();

            vessel_totals_from_form();
            make_dirty();
        }

        private void lblDel5_Click(object sender, EventArgs e)
        {
            //del(tbxEng.Text);
            //form_vessel(tbxSelVessel.Text, true);
            cbxEng.SelectedValue = "";

            tbxEnA.Text = "0";
            tbxEnP.Text = "0";
            tbxEnO.Text = "0";

            tbxEng.Text = "";

            //refresh_totals();
            lblDel5.Hide();
            //cmdOK.Show();

            errorProvider1.Clear();

            vessel_totals_from_form();
            make_dirty();
        }

        private void lblDel6_Click(object sender, EventArgs e)
        {
            //del(tbxC6.Text);
            //form_vessel(tbxSelVessel.Text, true);
            
            cbxC6.SelectedValue = "";

            tbxC6A.Text = "0";
            tbxC6P.Text = "0";
            tbxC6O.Text = "0";

            tbxC6.Text = "";

            //refresh_totals();
            lblDel6.Hide();
            //cmdOK.Show();

            errorProvider1.Clear();

            vessel_totals_from_form();
            make_dirty();
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
            
            //tbxV1Ca.Text = actual.ToString();            

            if (!actual.Equals(12M))
                chkCapt.Checked = false;
            else
                chkCapt.Checked = true;

            r = Decimal.TryParse(tbxCpP.Text, out paid);
            if (r && actual > paid)
            {
                r = Decimal.TryParse(tbxCpO.Text, out premium);

                if (!actual.Equals(12M) && paid.Equals(12M)) premium = actual - paid;
                tbxCpO.Text = premium.ToString();
            }

            vessel_totals_from_form();
            make_dirty();
        }


        private void tbxD1A_TextChanged(object sender, EventArgs e)
        {
            decimal actual;
            decimal paid;
            decimal premium;

            bool r = Decimal.TryParse(((TextBox)sender).Text, out actual);
            if (!r) return;
            
            //tbxV1D1.Text = actual.ToString();

            if (!actual.Equals(12M))
                chkDH1.Checked = false;
            else
                chkDH1.Checked = true;

            r = Decimal.TryParse(tbxD1P.Text, out paid);
            if (r && actual > paid)
            {
                r = Decimal.TryParse(tbxD1O.Text, out premium);

                if (!actual.Equals(12M) && paid.Equals(12M)) premium = actual - paid;
                tbxD1O.Text = premium.ToString();
            }

            vessel_totals_from_form();
            make_dirty();
        }

        private void tbxD2A_TextChanged(object sender, EventArgs e)
        {
            //tbxV1D2.Text = ((TextBox)sender).Text;

            decimal actual;
            decimal paid;
            decimal premium;

            bool r = Decimal.TryParse(((TextBox)sender).Text, out actual);
            if (!r) return;            

            if (!actual.Equals(12M))
                chkDH2.Checked = false;
            else
                chkDH2.Checked = true;

            r = Decimal.TryParse(tbxD2P.Text, out paid);
            if (r && actual > paid)
            {
                r = Decimal.TryParse(tbxD2O.Text, out premium);

                if (!actual.Equals(12M) && paid.Equals(12M)) premium = actual - paid;
                tbxD2O.Text = premium.ToString();
            }

            vessel_totals_from_form();
            make_dirty();
        }

        private void tbxMaA_TextChanged(object sender, EventArgs e)
        {            
            //tbxV1Ma.Text = ((TextBox)sender).Text;
            decimal actual;
            decimal paid;
            decimal premium;

            bool r = Decimal.TryParse(((TextBox)sender).Text, out actual);
            if (!r) return;            
  
            if (!actual.Equals(12M))
                chkMate.Checked = false;
            else
                chkMate.Checked = true;

            r = Decimal.TryParse(tbxMaP.Text, out paid);
            if (r && actual > paid)
            {
                r = Decimal.TryParse(tbxMaO.Text, out premium);

                if (!actual.Equals(12M) && paid.Equals(12M)) premium = actual - paid;
                tbxMaO.Text = premium.ToString();
            }

            vessel_totals_from_form();
            make_dirty();
        }

        private void tbxEnA_TextChanged(object sender, EventArgs e)
        {            
            //tbxV1En.Text = ((TextBox)sender).Text;

            decimal actual;
            decimal paid;
            decimal premium;

            bool r = Decimal.TryParse(((TextBox)sender).Text, out actual);
            if (!r) return;            

            if (!actual.Equals(12M))
                chkEng.Checked = false;
            else
                chkEng.Checked = true;

            r = Decimal.TryParse(tbxEnP.Text, out paid);
            if (r && actual > paid)
            {
                r = Decimal.TryParse(tbxEnO.Text, out premium);

                if (!actual.Equals(12M) && paid.Equals(12M)) premium = actual - paid;
                tbxEng.Text = premium.ToString();
            }

            vessel_totals_from_form();
            make_dirty();
        }

        private void tbxC6A_TextChanged(object sender, EventArgs e)
        {            
            //tbxV1C6.Text = ((TextBox)sender).Text;

            decimal actual;
            decimal paid;
            decimal premium;

            bool r = Decimal.TryParse(((TextBox)sender).Text, out actual);
            if (!r) return;
                       
            if (!actual.Equals(12M))
                chkC6.Checked = false;
            else
                chkC6.Checked = true;

            r = Decimal.TryParse(tbxC6P.Text, out paid);
            if (r && actual > paid)
            {
                r = Decimal.TryParse(tbxC6O.Text, out premium);

                if (!actual.Equals(12M) && paid.Equals(12M)) premium = actual - paid;
                tbxC6O.Text = premium.ToString();
            }

            vessel_totals_from_form();
            make_dirty();
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
            cmdC5.Text = "DH(3)";
            load_gang(cbxEng, "DH(3)");
        }


        private void cmdCapt_Click(object sender, EventArgs e)
        {
            cycle_role(sender);            

            load_gang(cbxCapt, ((Button)sender).Text);
            cbxCapt_SelectionChangeCommitted(null, null);
        }

        private void cmdMate_Click(object sender, EventArgs e)
        {
            cycle_role(sender);            

            load_gang(cbxMate, ((Button)sender).Text);
            cbxMate_SelectionChangeCommitted(null, null);
        }

        private void cmdEng_Click(object sender, EventArgs e)
        {
            cycle_role(sender);            

            load_gang(cbxEng, ((Button)sender).Text);
            cbxEng_SelectionChangeCommitted(null, null);
        }

        private void cmdDH1_Click(object sender, EventArgs e)
        {
            cycle_role(sender);            

            load_gang(cbxDh1, ((Button)sender).Text);
            cbxDh1_SelectionChangeCommitted(null, null);
        }

        private void cmdDH2_Click(object sender, EventArgs e)
        {
            cycle_role(sender);

            load_gang(cbxDh2, ((Button)sender).Text);
            cbxDh2_SelectionChangeCommitted(null, null);
        }

        private void cmdC6_Click(object sender, EventArgs e)
        {
            cycle_role(sender);

            load_gang(cbxC6, ((Button)sender).Text);
            cbxC6_SelectionChangeCommitted(null, null);
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \***************************test_unique(tbxDh1, cbxDH1Shift);****************************************************************************************/

        private void cycle_role(object sender)
        {
            string role = ((Button)sender).Text;
            switch (role)
            {
                case "Capt": role = "DH(1)"; break;
                case "DH(1)": role = "DH(2)"; break;
                case "DH(2)": role = "Mate"; break;
                case "Mate": role = "Eng."; break;
                case "Eng.": role = "DH(3)"; break;
                case "DH(3)": role = "Capt"; break;                
            }
            ((Button)sender).Text = role;
        }


        private void load_gang(ComboBox cbx, string button_name)
        {
            string gang_name = "Master";

            if (button_name.Equals("DH(1)")) gang_name = "Deckhand";
            if (button_name.Equals("DH(2)")) gang_name = "Deckhand";
            if (button_name.Equals("DH(3)")) gang_name = "Deckhand";
            if (button_name.Equals("DH(4)")) gang_name = "Deckhand";
            if (button_name.Equals("Mate")) gang_name = "Master";
            if (button_name.Equals("Eng.")) gang_name = "Deckhand";

            DataTable dt = qryGang.GetView(gang_name);
            cbx.BindingContext = new BindingContext();
            cbx.DataSource = dt;
            cbx.DisplayMember = "EmpName";
            cbx.ValueMember = "EmpID";
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void tbxCpO_TextChanged(object sender, EventArgs e)
        {
            vessel_totals_from_form();
            make_dirty();

        }

        private void tbxD1O_TextChanged(object sender, EventArgs e)
        {
            vessel_totals_from_form();
            make_dirty();
        }

        private void tbxD2O_TextChanged(object sender, EventArgs e)
        {
            vessel_totals_from_form();
            make_dirty();
        }

        private void tbxMaO_TextChanged(object sender, EventArgs e)
        {
            vessel_totals_from_form();
            make_dirty();
        }

        private void tbxEnO_TextChanged(object sender, EventArgs e)
        {
            vessel_totals_from_form();
            make_dirty();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private bool no_shift_warning()
        {
            DialogResult OK;
            OK = MessageBox.Show("Warning : This row has no <Shift> selection !",
                "Data Verification", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            return (OK == DialogResult.OK);
        }


        private bool verify_captain()
        {
            int shift_value = cbxCaShift.SelectedIndex;
            if (tbxCapt.Text.Length == 0) return true;
            if (shift_value == 0) return no_shift_warning();

            return true;
        }


        private bool verify_deckhand1()
        {
            int shift_value = cbxDH1Shift.SelectedIndex;
            if (tbxDh1.Text.Length == 0) return true;
            if (shift_value == 0) return no_shift_warning();

            return true;
        }


        private bool verify_deckhand2()
        {
            int shift_value = cbxDH2Shift.SelectedIndex;
            if (tbxDh2.Text.Length == 0) return true;
            if (shift_value == 0) return no_shift_warning();

            return true;
        }


        private bool verify_mate()
        {
            int shift_value = cbxMaShift.SelectedIndex;
            if (tbxMate.Text.Length == 0) return true;
            if (shift_value == 0) return no_shift_warning();

            return true;
        }


        private bool verify_engineer()
        {
            int shift_value = cbxEnShift.SelectedIndex;
            if (tbxEng.Text.Length == 0) return true;
            if (shift_value == 0) return no_shift_warning();

            return true;
        }


        private bool verify_C6()
        {
            int shift_value = cbxC6Shift.SelectedIndex;
            if (tbxC6.Text.Length == 0) return true;
            if (shift_value == 0) return no_shift_warning();

            return true;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void row_captain(DataTable dt, DateTime book_date, bool del)
        {
            DataRow row;

            if (tbxCapt.Text.Length == 0) return;
            //if (cbxCapt.SelectedIndex < 0) return;
            
            row = dt.NewRow();

            row["DelMark"] = del;

            row["BookDate"] = book_date;
            row["EmpID"] = tbxCapt.Text;
            row["EmpName"] = cbxCapt.Text;
            //row["ToffCode"] = null;
            row["Hours"] = needDecimal(tbxCpA);
            row["Over"] = needDecimal(tbxCpO);
            //row["LogVessel"] = cbxShips.SelectedValue;
            row["Vessel"] = Vessel;
            row["Shift"] = cbxCaShift.SelectedIndex;
            row["ShiftHour"] = cbxCaShift.SelectedValue;
            //row["LogNote"] = null;

            dt.Rows.Add(row);

            //dacTimebook.FindAdd(new object[] { book_date, row["EmpID"] }, row);
        }


        private void row_deckhand1(DataTable dt, DateTime book_date, bool del)
        {
            DataRow row;

            if (tbxDh1.Text.Length == 0) return;
            //if (cbxDh1.SelectedIndex < 0) return;

            row = dt.NewRow();

            row["DelMark"] = del;

            row["BookDate"] = book_date;
            row["EmpID"] = cbxDh1.SelectedValue;
            row["EmpName"] = cbxDh1.Text;
            //row["ToffCode"] = null;
            row["Hours"] = needDecimal(tbxD1A);
            row["Over"] = needDecimal(tbxD1O);
            //row["LogVessel"] = cbxShips.SelectedValue;
            row["Vessel"] = Vessel;
            row["Shift"] = cbxDH1Shift.SelectedIndex;
            row["ShiftHour"] = cbxDH1Shift.SelectedValue;

            //row["LogNote"] = null;

            dt.Rows.Add(row);
            //dacTimebook.FindAdd(new object[] { book_date, row["EmpID"] }, row);
        }


        private void row_deckhand2(DataTable dt, DateTime book_date, bool del)
        {
            DataRow row;

            if (tbxDh2.Text.Length == 0) return;
            //if (cbxDh2.SelectedIndex < 0) return;

            row = dt.NewRow();

            row["DelMark"] = del;

            row["BookDate"] = book_date;
            row["EmpID"] = cbxDh2.SelectedValue;
            row["EmpName"] = cbxDh2.Text;
            //row["ToffCode"] = null;
            row["Hours"] = needDecimal(tbxD2A);
            row["Over"] = needDecimal(tbxD2O);
            //row["LogVessel"] = cbxShips.SelectedValue;
            row["Vessel"] = Vessel;
            row["Shift"] = cbxDH2Shift.SelectedIndex;
            row["ShiftHour"] = cbxDH2Shift.SelectedValue;
            //row["LogNote"] = null;

            dt.Rows.Add(row);
            //dacTimebook.FindAdd(new object[] { book_date, row["EmpID"] }, row);
        }


        private void row_mate(DataTable dt, DateTime book_date, bool del)
        {
            DataRow row;

            if (tbxMate.Text.Length == 0) return;
            //if (cbxMate.SelectedIndex < 0) return;

            row = dt.NewRow();

            row["DelMark"] = del;

            row["BookDate"] = book_date;
            row["EmpID"] = cbxMate.SelectedValue;
            row["EmpName"] = cbxMate.Text;
            //row["ToffCode"] = null;
            row["Hours"] = needDecimal(tbxMaA);
            row["Over"] = needDecimal(tbxMaO);
            //row["LogVessel"] = cbxShips.SelectedValue;
            row["Vessel"] = Vessel;
            row["Shift"] = cbxMaShift.SelectedIndex;
            row["ShiftHour"] = cbxMaShift.SelectedValue;
            //row["LogNote"] = null;

            dt.Rows.Add(row);
            //dacTimebook.FindAdd(new object[] { book_date, row["EmpID"] }, row);
        }


        private void row_engineer(DataTable dt, DateTime book_date, bool del)
        {
            DataRow row;

            if (tbxEng.Text.Length == 0) return;
            //if (cbxEng.SelectedIndex < 0) return;

            row = dt.NewRow();

            row["DelMark"] = del;

            row["BookDate"] = book_date;
            row["EmpID"] = cbxEng.SelectedValue;
            row["EmpName"] = cbxEng.Text;
            //row["ToffCode"] = null;
            row["Hours"] = needDecimal(tbxEnA);
            row["Over"] = needDecimal(tbxEnO);
            //row["LogVessel"] = cbxShips.SelectedValue;
            row["Vessel"] = Vessel;
            row["Shift"] = cbxEnShift.SelectedIndex;
            row["ShiftHour"] = cbxEnShift.SelectedValue;
            //row["LogNote"] = null;

            dt.Rows.Add(row);
            //dacTimebook.FindAdd(new object[] { book_date, row["EmpID"] }, row);
        }


        private void row_C6(DataTable dt, DateTime book_date, bool del)
        {
            DataRow row;

            if (tbxC6.Text.Length == 0) return;
            if (cbxC6.SelectedIndex < 0) return;

            row = dt.NewRow();

            row["DelMark"] = del;

            row["BookDate"] = book_date;
            row["EmpID"] = cbxC6.SelectedValue;
            row["EmpName"] = cbxC6.Text;
            //row["ToffCode"] = null;
            row["Hours"] = needDecimal(tbxC6A);
            row["Over"] = needDecimal(tbxC6O);
            //row["LogVessel"] = cbxShips.SelectedValue;
            row["Vessel"] = Vessel;
            row["Shift"] = cbxC6Shift.SelectedIndex;
            row["ShiftHour"] = cbxC6Shift.SelectedValue;
            //row["LogNote"] = null;

            dt.Rows.Add(row);
            //dacTimebook.FindAdd(new object[] { book_date, row["EmpID"] }, row);
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void trans_start()
        {
            row_captain(Timebook, BookDate, true);
            row_deckhand1(Timebook, BookDate, true);
            row_deckhand2(Timebook, BookDate, true);
            row_mate(Timebook, BookDate, true);
            row_engineer(Timebook, BookDate, true);
            row_C6(Timebook, BookDate, true);
        }

        private void trans_finish()
        {
            row_captain(Timebook, BookDate, false);
            row_deckhand1(Timebook, BookDate, false);
            row_deckhand2(Timebook, BookDate, false);
            row_mate(Timebook, BookDate, false);
            row_engineer(Timebook, BookDate, false);
            row_C6(Timebook, BookDate, false);
        }


        public bool Save_Delete()
        {
            if (!verify_captain()) return false;
            if (!verify_deckhand1()) return false;
            if (!verify_deckhand2()) return false;
            if (!verify_mate()) return false;
            if (!verify_engineer()) return false;
            if (!verify_C6()) return false;

            trans_finish();
            return true;

            //if (tbxDh1.Text.Length > 0) MessageBox.Show(tbxDh1.Text);
            //if (tbxDh2.Text.Length > 0) MessageBox.Show(tbxDh2.Text);
            //if (tbxMate.Text.Length > 0) MessageBox.Show(tbxMate.Text);
            //if (tbxEng.Text.Length > 0) MessageBox.Show(tbxEng.Text);
            //if (tbxC6.Text.Length > 0) MessageBox.Show(tbxC6.Text);


        }


        private void do_save_delete()
        {
            DataTable _dt_time = null;

            DataSet ds = null;

            if (_deleted)
            {
                foreach (DataRow row in _dt_time.Rows)
                {
                    if (ds == null) ds = dacTimebook.GetDS((DateTime)row["BookDate"], 1);
                    if ((bool)row["DelMark"])
                        dacTimebook.FindDel(new object[] { row["BookDate"], row["EmpId"] });
                }

                dacTimebook.DeleteData();
                dacCache.PutTimebook();
                _deleted = false;
            }

            if (Dirty)
            {
                //DateTime book_date = dtpLogDate.Value.Date;
                DateTime book_date = DateTime.Now;

                if (ds == null) ds = dacTimebook.GetDS(book_date, 1);

                //row_captain(ds, book_date);
                //row_deckhand1(ds, book_date);
                //row_deckhand2(ds, book_date);
                //row_mate(ds, book_date);
                //row_engineer(ds, book_date);
                //row_C6(ds, book_date);

                dacTimebook.SaveData();
                dacCache.PutTimebook();

                Dirty = false;
            }

            MessageBox.Show("Saved !");
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void tbxCpP_TextChanged(object sender, EventArgs e)
        {
            vessel_totals_from_form();
            make_dirty();
        }

        private void tbxD1P_TextChanged(object sender, EventArgs e)
        {
            vessel_totals_from_form();
            make_dirty();
        }

        private void tbxD2P_TextChanged(object sender, EventArgs e)
        {
            vessel_totals_from_form();
            make_dirty();
        }

        private void tbxMaP_TextChanged(object sender, EventArgs e)
        {
            vessel_totals_from_form();
            make_dirty();
        }

        private void tbxEnP_TextChanged(object sender, EventArgs e)
        {
            vessel_totals_from_form();
            make_dirty();
        }

        private void tbxC6P_TextChanged(object sender, EventArgs e)
        {
            vessel_totals_from_form();
            make_dirty();
        }

        private void tbxCpO_TextChanged_1(object sender, EventArgs e)
        {
            vessel_totals_from_form();
            make_dirty();
        }

        private void tbxD1O_TextChanged_1(object sender, EventArgs e)
        {
            vessel_totals_from_form();
            make_dirty();
        }

        private void tbxD2O_TextChanged_1(object sender, EventArgs e)
        {
            vessel_totals_from_form();
            make_dirty();
        }

        private void tbxMaO_TextChanged_1(object sender, EventArgs e)
        {
            vessel_totals_from_form();
            make_dirty();
        }

        private void tbxEnO_TextChanged_1(object sender, EventArgs e)
        {
            vessel_totals_from_form();
            make_dirty();
        }

        private void tbxC6O_TextChanged(object sender, EventArgs e)
        {
            vessel_totals_from_form();
            make_dirty();
        }


        /*
         * Shift Select 
         */

        private void cbxCaShift_SelectionChangeCommitted(object sender, EventArgs e)
        {         
            vessel_totals_from_form();
            make_dirty();
        }

        private void cbxDH1Shift_SelectedIndexChanged(object sender, EventArgs e)
        {
            vessel_totals_from_form();
            make_dirty();
        }

        private void cbxDH2Shift_SelectedIndexChanged(object sender, EventArgs e)
        {
            vessel_totals_from_form();
            make_dirty();
        }

        private void cbxMaShift_SelectedIndexChanged(object sender, EventArgs e)
        {
            vessel_totals_from_form();
            make_dirty();
        }

        private void cbxEnShift_SelectedIndexChanged(object sender, EventArgs e)
        {
            vessel_totals_from_form();
            make_dirty();
        }

        private void cbxC6Shift_SelectedIndexChanged(object sender, EventArgs e)
        {
            vessel_totals_from_form();
            make_dirty();
        }

        private void tbxCaptSheet_Click(object sender, EventArgs e)
        {
            tbxCaptSheet.Text = SheetID;
        }

        private void tbxDh1Sheet_Click(object sender, EventArgs e)
        {
            tbxDh1Sheet.Text = SheetID;
        }

        private void tbxDh2Sheet_Click(object sender, EventArgs e)
        {
            tbxDh2Sheet.Text = SheetID;
        }

        private void tbxMateSheet_Click(object sender, EventArgs e)
        {
            tbxMateSheet.Text = SheetID;
        }

        private void tbxEngSheet_Click(object sender, EventArgs e)
        {
            tbxEngSheet.Text = SheetID;
        }

        private void tbxC6Sheet_Click(object sender, EventArgs e)
        {
            tbxC6Sheet.Text = SheetID;
        }


        
        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        //private List<string> get_shifts()
        //private DataTable get_shifts()
        //{
        //    return dacCache.GetShift();
        //
        //    //return new List<string> { "N/A", "AM", "PM", "PT", "24 Hours" };
        //}


    }
}
