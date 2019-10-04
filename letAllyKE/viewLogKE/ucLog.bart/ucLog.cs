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
            cbxEx1.DataSource = dt;
            cbxEx1.DisplayMember = "EmpName";
            cbxEx1.ValueMember = "EmpID";
            //cbxCapt.SelectedIndex = -1;            

            dt = qryGang.GetView("Deckhand");
            cbxEx2.DataSource = dt;
            cbxEx2.DisplayMember = "EmpName";
            cbxEx2.ValueMember = "EmpID";
            //cbxDh1.SelectedIndex = -1;

            cbxEx3.BindingContext = new BindingContext();
            cbxEx3.DataSource = dt;
            cbxEx3.DisplayMember = "EmpName";
            cbxEx3.ValueMember = "EmpID";
            //cbxDh2.SelectedIndex = -1;

            dt = qryGang.GetView("Master");
            cbxEx4.DataSource = dt;
            cbxEx4.DisplayMember = "EmpName";
            cbxEx4.ValueMember = "EmpID";
            //cbxMate.SelectedIndex = -1;

            dt = qryGang.GetView("Deckhand");
            cbxEx5.BindingContext = new BindingContext();
            cbxEx5.DataSource = dt;
            cbxEx5.DisplayMember = "EmpName";
            cbxEx5.ValueMember = "EmpID";
            //cbxEng.SelectedIndex = -1;

            cbxEx6.BindingContext = new BindingContext();
            cbxEx6.DataSource = dt;
            cbxEx6.DisplayMember = "EmpName";
            cbxEx6.ValueMember = "EmpID";
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

            cbxSx1.DataSource = dacCache.GetShift();
            cbxSx1.BindingContext = new BindingContext();
            cbxSx1.DisplayMember = "Short";
            cbxSx1.ValueMember = "NumID";
            cbxSx1.SelectedIndex = 1;

            cbcSx2.DataSource = dacCache.GetShift();
            cbcSx2.BindingContext = new BindingContext();
            cbcSx2.DisplayMember = "Short";
            cbcSx2.ValueMember = "NumID";
            cbcSx2.SelectedIndex = 1;

            cbxSx3.DataSource = dacCache.GetShift();
            cbxSx3.BindingContext = new BindingContext();
            cbxSx3.DisplayMember = "Short";
            cbxSx3.ValueMember = "NumID";
            cbxSx3.SelectedIndex = 1;

            cbxSx4.DataSource = dacCache.GetShift();
            cbxSx4.BindingContext = new BindingContext();
            cbxSx4.DisplayMember = "Short";
            cbxSx4.ValueMember = "NumID";
            cbxSx4.SelectedIndex = 1;

            cbxSx5.DataSource = dacCache.GetShift();
            cbxSx5.BindingContext = new BindingContext();
            cbxSx5.DisplayMember = "Short";
            cbxSx5.ValueMember = "NumID";
            cbxSx5.SelectedIndex = 1;

            cbxSx6.DataSource = dacCache.GetShift();
            cbxSx6.BindingContext = new BindingContext();
            cbxSx6.DisplayMember = "Short";
            cbxSx6.ValueMember = "NumID";
            cbxSx6.SelectedIndex = 1;

            tbxLx1.Hide();
            tbxLx2.Hide();
            tbxLx3.Hide();
            tbxLx4.Hide();
            tbxLx5.Hide();
            tbxLx6.Hide();

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
            tbxLx1.Show();
            tbxLx2.Show();
            tbxLx3.Show();
            tbxLx4.Show();
            tbxLx5.Show();
            tbxLx6.Show();
        }
        

        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public void NewShift(int shift)
        {
            //shift = (int)nudShift.Value;

            if (cbxEx1.Text.Equals("") || cbxSx1.SelectedIndex == 0) cbxSx1.SelectedIndex = shift;
            if (cbxEx2.Text.Equals("") || cbcSx2.SelectedIndex == 0) cbcSx2.SelectedIndex = shift;
            if (cbxEx3.Text.Equals("") || cbxSx3.SelectedIndex == 0) cbxSx3.SelectedIndex = shift;
            if (cbxEx4.Text.Equals("") || cbxSx4.SelectedIndex == 0) cbxSx4.SelectedIndex = shift;
            if (cbxEx5.Text.Equals("") || cbxSx5.SelectedIndex == 0) cbxSx5.SelectedIndex = shift;
            if (cbxEx6.Text.Equals("") || cbxSx6.SelectedIndex == 0) cbxSx6.SelectedIndex = shift;
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
                cbxEx1.Text = (string)row["EmpName"];
                tbxIx1.Text = cbxEx1.SelectedValue.ToString();
                tbxAx1.Text = hour.ToString("#.#");
                tbxOx1.Text = over.ToString("#.#");
                tbxPx1.Text = (hour >= 12M ? 12M : hour).ToString("#.#");
                chkHx1.Checked = hour.Equals(12M);
                cbxSx1.SelectedIndex = (int)row["Shift"];
                //cbxCaShift.Text = (string)row["ShiftHour"];
                //set_captainX(tbxCpA, tbxCpP, tbxCpO, chkCapt.Checked, tbxV1A, chkCrew1.Checked);
                
                set_captain(tbxAx1, tbxPx1, tbxOx1, chkHx1.Checked);
                //lblDel1.Show();

                //CaptHours = hour;
            }
            else if (idx == 2)
            {
                cbxEx4.Text = (string)row["EmpName"];
                tbxIx4.Text = cbxEx4.SelectedValue.ToString();
                cmdCx4.Text = "Capt";
                //lblMate.Text = "Capt";
                tbxAx4.Text = hour.ToString("#.#");
                tbxOx4.Text = over.ToString("#.#");
                tbxPx4.Text = (hour >= 12M ? 12M : hour).ToString("#.#");
                chkHx4.Checked = hour.Equals(12M);
                cbxSx4.SelectedIndex = (int)row["Shift"];
                //cbxMaShift.Text = (string)row["ShiftHour"];

                set_captain(tbxAx4, tbxPx4, tbxOx4, chkHx4.Checked);
                //lblDel4.Show();

                //MateHours = hour;
            }
            else if (idx == 3)
            {
                if (dh_idx < 3)
                {
                    cbxEx5.Text = (string)row["EmpName"];
                    tbxIx5.Text = cbxEx5.SelectedValue.ToString();
                    cmdCx5.Text = "Capt";
                    //lblEng.Text = "Capt";
                    tbxAx5.Text = hour.ToString("#.#");
                    tbxOx5.Text = over.ToString("#.#");
                    tbxPx5.Text = (hour >= 12M ? 12M : hour).ToString("#.#");
                    chkHx5.Checked = hour.Equals(12M);
                    cbxSx5.SelectedIndex = (int)row["Shift"];
                    //cbxEnShift.Text = (string)row["ShiftHour"];
                
                    set_captain(tbxAx5, tbxPx5, tbxOx5, chkHx5.Checked);
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
                cbxEx2.Text = (string)row["EmpName"];
                tbxIx2.Text = cbxEx2.SelectedValue.ToString();
                tbxAx2.Text = hour.ToString("#.#");
                tbxOx2.Text = over.ToString("#.#");
                tbxPx2.Text = (hour >= 12M ? 12M : hour).ToString("#.#");
                chkHx2.Checked = hour.Equals(12M);
                cbcSx2.SelectedIndex = (int)row["Shift"];
                //cbxDH1Shift.Text = (string)row["ShiftHour"];
                
                set_standard(tbxAx2, tbxPx2, tbxOx2, chkHx2.Checked);
                //lblDel2.Show();

                //Dkh1Hours = hour;
            }
            else if (idx == 2)
            {
                cbxEx3.Text = (string)row["EmpName"];
                tbxIx3.Text = cbxEx3.SelectedValue.ToString();
                tbxAx3.Text = hour.ToString("#.#");
                tbxOx3.Text = over.ToString("#.#");
                tbPx3.Text = (hour >= 12M ? 12M : hour).ToString("#.#");
                chkHx3.Checked = hour.Equals(12M);
                cbxSx3.SelectedIndex = (int)row["Shift"];
                //cbxDH2Shift.Text = (string)row["ShiftHour"];

                set_standard(tbxAx3, tbPx3, tbxOx3, chkHx3.Checked);
                //lblDel3.Show();

                //Dkh2Hours = hour;
            }
            else if (idx == 3)
            {
                if (skip_idx < 3)
                {
                    cbxEx6.Text = (string)row["EmpName"];
                    tbxIx6.Text = cbxEx6.SelectedValue.ToString();
                    cmdCx6.Text = "DH(3)";
                    //lblC6.Text = "DH(3)";
                    tbxAx6.Text = hour.ToString("#.#");
                    tbxOx6.Text = over.ToString("#.#");
                    tbxPx6.Text = (hour >= 12M ? 12M : hour).ToString("#.#");
                    chkHx6.Checked = hour.Equals(12M);
                    cbxSx6.SelectedIndex = (int)row["Shift"];
                    //cbxC6Shift.Text = (string)row["ShiftHour"];
                    
                    set_standard(tbxAx6, tbxPx6, tbxOx6, chkHx6.Checked);
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

            cmdCx1.Text = "Capt";
            cmdCx2.Text = "DH(1)";
            cmdCx3.Text = "DH(2)";
            cmdCx4.Text = "Mate";
            cmdCx5.Text = "Eng.";
            cmdCx6.Text = "DH(3)";

            cbxEx1.SelectedIndex = -1;
            cbxEx2.SelectedIndex = -1;
            cbxEx3.SelectedIndex = -1;
            cbxEx4.SelectedIndex = -1;
            cbxEx5.SelectedIndex = -1;
            cbxEx6.SelectedIndex = -1;
            //if (! _new)  cbxShips.SelectedIndex = -1;

            tbxIx1.Text = string.Empty;
            tbxIx2.Text = string.Empty;
            tbxIx3.Text = string.Empty;
            tbxIx4.Text = string.Empty;
            tbxIx5.Text = string.Empty;
            tbxIx6.Text = string.Empty;

            //cbxCapt.Text = string.Empty;
            //cbxDh1.Text = string.Empty;
            //cbxDh2.Text = string.Empty;
            //cbxMate.Text = string.Empty;
            //cbxEng.Text = string.Empty;
            //cbxShips.Text = string.Empty;

            chkHx1.Checked = false;
            chkHx2.Checked = false;
            chkHx3.Checked = false;
            chkHx4.Checked = false;
            chkHx5.Checked = false;
            chkHx6.Checked = false;

            cbxSx1.SelectedIndex = 1;
            cbcSx2.SelectedIndex = 1;
            cbxSx3.SelectedIndex = 1;
            cbxSx4.SelectedIndex = 1;
            cbxSx5.SelectedIndex = 1;
            cbxSx6.SelectedIndex = 1;


            vacant(tbxAx1, tbxPx1, tbxOx1);
            vacant(tbxAx2, tbxPx2, tbxOx2);
            vacant(tbxAx3, tbPx3, tbxOx3);
            vacant(tbxAx4, tbxPx4, tbxOx4);
            vacant(tbxAx5, tbxPx5, tbxOx5);
            vacant(tbxAx6, tbxPx6, tbxOx6);

            tbxAx1.ReadOnly = false;
            tbxAx2.ReadOnly = false;
            tbxAx3.ReadOnly = false;
            tbxAx4.ReadOnly = false;
            tbxAx5.ReadOnly = false;
            tbxAx6.ReadOnly = false;

            tbxOx1.ReadOnly = true;
            tbxOx2.ReadOnly = true;
            tbxOx3.ReadOnly = true;
            tbxOx4.ReadOnly = true;
            tbxOx5.ReadOnly = true;
            tbxOx6.ReadOnly = true;

            tbxPx1.ReadOnly = true;
            tbxPx2.ReadOnly = true;
            tbPx3.ReadOnly = true;
            tbxPx4.ReadOnly = true;
            tbxPx5.ReadOnly = true;
            tbxPx6.ReadOnly = true;

            cbxEx1.Enabled = false;
            cbxEx2.Enabled = false;
            cbxEx3.Enabled = false;
            cbxEx4.Enabled = false;
            cbxEx5.Enabled = false;
            cbxEx6.Enabled = false;

            cbxSx1.Enabled = false;
            cbcSx2.Enabled = false;
            cbxSx3.Enabled = false;
            cbxSx4.Enabled = false;
            cbxSx5.Enabled = false;
            cbxSx6.Enabled = false;

            chkHx1.Enabled = false;
            chkHx2.Enabled = false;
            chkHx3.Enabled = false;
            chkHx4.Enabled = false;
            chkHx5.Enabled = false;
            chkHx6.Enabled = false;

            lblDx1.Hide();
            lblDx2.Hide();
            lblDx3.Hide();
            lblDx4.Hide();
            lblDx5.Hide();
            lblDx6.Hide();

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

            if (!chkHx1.Checked)
            {
                tbxAx1.ReadOnly = false;
                tbxOx1.ReadOnly = false;
                tbxPx1.ReadOnly = false;
            }

            if (!chkHx2.Checked)
            {
                tbxAx2.ReadOnly = false;
                tbxOx2.ReadOnly = false;
                tbxPx2.ReadOnly = false;
            }

            if (!chkHx3.Checked)
            {
                tbxAx3.ReadOnly = false;
                tbxOx3.ReadOnly = false;
                tbPx3.ReadOnly = false;
            }

            if (!chkHx4.Checked)
            {
                tbxAx4.ReadOnly = false;
                tbxOx4.ReadOnly = false;
                tbxPx4.ReadOnly = false;
            }

            if (!chkHx5.Checked)
            {
                tbxAx5.ReadOnly = false;
                tbxOx5.ReadOnly = false;
                tbxPx5.ReadOnly = false;
            }

            if (!chkHx6.Checked)
            {
                tbxAx6.ReadOnly = false;
                tbxOx6.ReadOnly = false;
                tbxPx6.ReadOnly = false;
            }


            cbxEx1.Enabled = true;
            cbxEx2.Enabled = true;
            cbxEx3.Enabled = true;
            cbxEx4.Enabled = true;
            cbxEx5.Enabled = true;
            cbxEx6.Enabled = true;

            cbxSx1.Enabled = true;
            cbcSx2.Enabled = true;
            cbxSx3.Enabled = true;
            cbxSx4.Enabled = true;
            cbxSx5.Enabled = true;
            cbxSx6.Enabled = true;

            chkHx1.Enabled = true;
            chkHx2.Enabled = true;
            chkHx3.Enabled = true;
            chkHx4.Enabled = true;
            chkHx5.Enabled = true;
            chkHx6.Enabled = true;

            lblDx1.Show();
            lblDx2.Show();
            lblDx3.Show();
            lblDx4.Show();
            lblDx5.Show();
            lblDx6.Show();

            trans_start();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void cbxCapt_SelectedIndexChanged(object sender, EventArgs e)
        {
            set_standard(tbxAx1, tbxPx1, tbxOx1, chkHx1.Checked = true);

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
            set_standard(tbxAx2, tbxPx2, tbxOx2, chkHx2.Checked = true);

            //chkDH1.Enabled = true;
            //chkDH1.Checked = true;
        }


        private void cbxDh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            set_standard(tbxAx3, tbPx3, tbxOx3, chkHx3.Checked = true);

            //chkDH2.Enabled = true;
            //chkDH2.Checked = true;            
        }


        private void cbxMate_SelectedIndexChanged(object sender, EventArgs e)
        {
            set_standard(tbxAx4, tbxPx4, tbxOx4, chkHx4.Checked = true);

            //chkMate.Enabled = true;
            //chkMate.Checked = true;            
        }


        private void cbxEng_SelectedIndexChanged(object sender, EventArgs e)
        {
            set_standard(tbxAx5, tbxPx5, tbxOx5, chkHx5.Checked = true);

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

            if (tbxIx1.Text.Equals(tbxID.Text)) count_id++; 
            if (tbxIx2.Text.Equals(tbxID.Text)) count_id++;
            if (tbxIx3.Text.Equals(tbxID.Text)) count_id++;
            if (tbxIx4.Text.Equals(tbxID.Text)) count_id++;
            if (tbxIx5.Text.Equals(tbxID.Text)) count_id++;
            if (tbxIx6.Text.Equals(tbxID.Text)) count_id++;

            if (count_id > 1) errorProvider1.SetError(cbxShift, "Warning : duplicate crew member !");            
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void cbxCapt_SelectionChangeCommitted(object sender, EventArgs e)
        {
            chkHx1.Enabled = true;
            chkHx1.Checked = true;

            
            tbxIx1.Text = cbxEx1.SelectedValue.ToString();
            test_unique(tbxIx1, cbxSx1);



            set_standard(tbxAx1, tbxPx1, tbxOx1, chkHx1.Checked);
            lblDx1.Show();

            vessel_totals_from_form();
            make_dirty();
        }

        private void cbxDh1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            chkHx2.Enabled = true;
            chkHx2.Checked = true;

            tbxIx2.Text = cbxEx2.SelectedValue.ToString();
            test_unique(tbxIx2, cbcSx2);

            set_standard(tbxAx2, tbxPx2, tbxOx2, chkHx2.Checked);
            lblDx2.Show();

            vessel_totals_from_form();
            make_dirty();
        }

        private void cbxDh2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            chkHx3.Enabled = true;
            chkHx3.Checked = true;

            tbxIx3.Text = cbxEx3.SelectedValue.ToString();
            test_unique(tbxIx3, cbxSx3);

            set_standard(tbxAx3, tbPx3, tbxOx3, chkHx3.Checked);

            //cbxDh2.BackColor = Color.LightGreen;
            //tbxDh2.BackColor = Color.LightGreen;

            lblDx3.Show();

            vessel_totals_from_form();
            make_dirty();
        }

        private void cbxMate_SelectionChangeCommitted(object sender, EventArgs e)
        {
            chkHx4.Enabled = true;
            chkHx4.Checked = true;

            tbxIx4.Text = cbxEx4.SelectedValue.ToString();
            test_unique(tbxIx4, cbxSx4);

            set_standard(tbxAx4, tbxPx4, tbxOx4, chkHx4.Checked);
            lblDx4.Show();

            vessel_totals_from_form();
            make_dirty();
        }

        private void cbxEng_SelectionChangeCommitted(object sender, EventArgs e)
        {
            chkHx5.Enabled = true;
            chkHx5.Checked = true;

            tbxIx5.Text = cbxEx5.SelectedValue.ToString();
            test_unique(tbxIx5, cbxSx5);

            set_standard(tbxAx5, tbxPx5, tbxOx5, chkHx5.Checked);
            lblDx5.Show();

            vessel_totals_from_form();
            make_dirty();
        }

        private void cbxC6_SelectionChangeCommitted(object sender, EventArgs e)
        {
            chkHx6.Enabled = true;
            chkHx6.Checked = true;

            tbxIx6.Text = cbxEx6.SelectedValue.ToString();
            test_unique(tbxIx6, cbxSx6);

            set_standard(tbxAx6, tbxPx6, tbxOx6, chkHx6.Checked);
            lblDx6.Show();

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

            if (tbxIx1.Text.Length > 0)
            {
                get_item(tbxAx1, tbxPx1, tbxOx1, out actual, out paid, out premium);
                total_item(actual, paid, premium, 0);
            }
            
            if (tbxIx2.Text.Length > 0)
            {
                get_item(tbxAx2, tbxPx2, tbxOx2, out actual, out paid, out premium);
                total_item(actual, paid, premium, 1);
            }

            if (tbxIx3.Text.Length > 0)
            {
                get_item(tbxAx3, tbPx3, tbxOx3, out actual, out paid, out premium);
                total_item(actual, paid, premium, 2);
            }

            if (tbxIx4.Text.Length > 0)
            {
                get_item(tbxAx4, tbxPx4, tbxOx4, out actual, out paid, out premium);
                total_item(actual, paid, premium, 3);
            }

            if (tbxIx5.Text.Length > 0)
            {
                get_item(tbxAx5, tbxPx5, tbxOx5, out actual, out paid, out premium);
                total_item(actual, paid, premium, 4);
            }

            if (tbxIx6.Text.Length > 0)
            {
                get_item(tbxAx6, tbxPx6, tbxOx6, out actual, out paid, out premium);
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
            set_captain(tbxAx1, tbxPx1, tbxOx1, chkHx1.Checked);
            make_dirty();
        }


        private void chkDH1_CheckedChanged(object sender, EventArgs e)
        {            
            set_standard(tbxAx2, tbxPx2, tbxOx2, chkHx2.Checked);
            make_dirty();
        }

        private void chkDH2_CheckedChanged(object sender, EventArgs e)
        {            
            set_standard(tbxAx3, tbPx3, tbxOx3, chkHx3.Checked);
            make_dirty();
        }

        private void chkMate_CheckedChanged(object sender, EventArgs e)
        {            
            set_standard(tbxAx4, tbxPx4, tbxOx4, chkHx4.Checked);
            make_dirty();
        }

        private void chkEng_CheckedChanged(object sender, EventArgs e)
        {            
            set_standard(tbxAx5, tbxPx5, tbxOx5, chkHx5.Checked);
            make_dirty();
        }

        private void chkC6_CheckedChanged(object sender, EventArgs e)
        {            
            set_standard(tbxAx6, tbxPx6, tbxOx6, chkHx6.Checked);
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

            cbxEx1.SelectedValue = "";            

            //form_vessel(tbxSelVessel.Text, true);
            tbxAx1.Text = "0";
            tbxPx1.Text = "0";
            tbxOx1.Text = "0";

            tbxIx1.Text = "";

            //refresh_totals();
            lblDx1.Hide();
            //cmdOK.Show();

            errorProvider1.Clear();

            vessel_totals_from_form();
            make_dirty();
        }

        private void lblDel2_Click(object sender, EventArgs e)
        {
            //del(tbxDh1.Text);
            //form_vessel(tbxSelVessel.Text, true);

            cbxEx2.SelectedValue = "";            

            tbxAx2.Text = "0";            
            tbxPx2.Text = "0";
            tbxOx2.Text = "0";

            tbxIx2.Text = "";

            //refresh_totals();
            lblDx2.Hide();
            //cmdOK.Show();

            errorProvider1.Clear();

            vessel_totals_from_form();
            make_dirty();
        }

        private void lblDel3_Click(object sender, EventArgs e)
        {
            //del(tbxDh2.Text);
            //form_vessel(tbxSelVessel.Text, true);

            cbxEx3.SelectedValue = "";

            tbxAx3.Text = "0";
            tbPx3.Text = "0";
            tbxOx3.Text = "0";

            tbxIx3.Text = "";

            //refresh_totals();
            lblDx3.Hide();
            //cmdOK.Show();

            errorProvider1.Clear();

            vessel_totals_from_form();
            make_dirty();
        }

        private void lblDel4_Click(object sender, EventArgs e)
        {
            //del(tbxMate.Text);
            //form_vessel(tbxSelVessel.Text, true);

            cbxEx4.SelectedValue = "";

            tbxAx4.Text = "0";
            tbxPx4.Text = "0";
            tbxOx4.Text = "0";

            tbxIx4.Text = "";

            //refresh_totals();
            lblDx4.Hide();
            //cmdOK.Show();

            errorProvider1.Clear();

            vessel_totals_from_form();
            make_dirty();
        }

        private void lblDel5_Click(object sender, EventArgs e)
        {
            //del(tbxEng.Text);
            //form_vessel(tbxSelVessel.Text, true);
            cbxEx5.SelectedValue = "";

            tbxAx5.Text = "0";
            tbxPx5.Text = "0";
            tbxOx5.Text = "0";

            tbxIx5.Text = "";

            //refresh_totals();
            lblDx5.Hide();
            //cmdOK.Show();

            errorProvider1.Clear();

            vessel_totals_from_form();
            make_dirty();
        }

        private void lblDel6_Click(object sender, EventArgs e)
        {
            //del(tbxC6.Text);
            //form_vessel(tbxSelVessel.Text, true);
            
            cbxEx6.SelectedValue = "";

            tbxAx6.Text = "0";
            tbxPx6.Text = "0";
            tbxOx6.Text = "0";

            tbxIx6.Text = "";

            //refresh_totals();
            lblDx6.Hide();
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
                chkHx1.Checked = false;
            else
                chkHx1.Checked = true;

            r = Decimal.TryParse(tbxPx1.Text, out paid);
            if (r && actual > paid)
            {
                r = Decimal.TryParse(tbxOx1.Text, out premium);

                if (!actual.Equals(12M) && paid.Equals(12M)) premium = actual - paid;
                tbxOx1.Text = premium.ToString();
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
                chkHx2.Checked = false;
            else
                chkHx2.Checked = true;

            r = Decimal.TryParse(tbxPx2.Text, out paid);
            if (r && actual > paid)
            {
                r = Decimal.TryParse(tbxOx2.Text, out premium);

                if (!actual.Equals(12M) && paid.Equals(12M)) premium = actual - paid;
                tbxOx2.Text = premium.ToString();
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
                chkHx3.Checked = false;
            else
                chkHx3.Checked = true;

            r = Decimal.TryParse(tbPx3.Text, out paid);
            if (r && actual > paid)
            {
                r = Decimal.TryParse(tbxOx3.Text, out premium);

                if (!actual.Equals(12M) && paid.Equals(12M)) premium = actual - paid;
                tbxOx3.Text = premium.ToString();
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
                chkHx4.Checked = false;
            else
                chkHx4.Checked = true;

            r = Decimal.TryParse(tbxPx4.Text, out paid);
            if (r && actual > paid)
            {
                r = Decimal.TryParse(tbxOx4.Text, out premium);

                if (!actual.Equals(12M) && paid.Equals(12M)) premium = actual - paid;
                tbxOx4.Text = premium.ToString();
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
                chkHx5.Checked = false;
            else
                chkHx5.Checked = true;

            r = Decimal.TryParse(tbxPx5.Text, out paid);
            if (r && actual > paid)
            {
                r = Decimal.TryParse(tbxOx5.Text, out premium);

                if (!actual.Equals(12M) && paid.Equals(12M)) premium = actual - paid;
                tbxIx5.Text = premium.ToString();
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
                chkHx6.Checked = false;
            else
                chkHx6.Checked = true;

            r = Decimal.TryParse(tbxPx6.Text, out paid);
            if (r && actual > paid)
            {
                r = Decimal.TryParse(tbxOx6.Text, out premium);

                if (!actual.Equals(12M) && paid.Equals(12M)) premium = actual - paid;
                tbxOx6.Text = premium.ToString();
            }

            vessel_totals_from_form();
            make_dirty();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void cmdMate_to_Capt()
        {
            cmdCx4.Text = "Capt";
            load_gang(cbxEx4, "Mate");
        }


        private void cmdEng_to_Dh3()
        {
            cmdCx5.Text = "DH(3)";
            load_gang(cbxEx5, "DH(3)");
        }


        private void cmdCapt_Click(object sender, EventArgs e)
        {
            cycle_role(sender);            

            load_gang(cbxEx1, ((Button)sender).Text);
            cbxCapt_SelectionChangeCommitted(null, null);
        }

        private void cmdMate_Click(object sender, EventArgs e)
        {
            cycle_role(sender);            

            load_gang(cbxEx4, ((Button)sender).Text);
            cbxMate_SelectionChangeCommitted(null, null);
        }

        private void cmdEng_Click(object sender, EventArgs e)
        {
            cycle_role(sender);            

            load_gang(cbxEx5, ((Button)sender).Text);
            cbxEng_SelectionChangeCommitted(null, null);
        }

        private void cmdDH1_Click(object sender, EventArgs e)
        {
            cycle_role(sender);            

            load_gang(cbxEx2, ((Button)sender).Text);
            cbxDh1_SelectionChangeCommitted(null, null);
        }

        private void cmdDH2_Click(object sender, EventArgs e)
        {
            cycle_role(sender);

            load_gang(cbxEx3, ((Button)sender).Text);
            cbxDh2_SelectionChangeCommitted(null, null);
        }

        private void cmdC6_Click(object sender, EventArgs e)
        {
            cycle_role(sender);

            load_gang(cbxEx6, ((Button)sender).Text);
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
            int shift_value = cbxSx1.SelectedIndex;
            if (tbxIx1.Text.Length == 0) return true;
            if (shift_value == 0) return no_shift_warning();

            return true;
        }


        private bool verify_deckhand1()
        {
            int shift_value = cbcSx2.SelectedIndex;
            if (tbxIx2.Text.Length == 0) return true;
            if (shift_value == 0) return no_shift_warning();

            return true;
        }


        private bool verify_deckhand2()
        {
            int shift_value = cbxSx3.SelectedIndex;
            if (tbxIx3.Text.Length == 0) return true;
            if (shift_value == 0) return no_shift_warning();

            return true;
        }


        private bool verify_mate()
        {
            int shift_value = cbxSx4.SelectedIndex;
            if (tbxIx4.Text.Length == 0) return true;
            if (shift_value == 0) return no_shift_warning();

            return true;
        }


        private bool verify_engineer()
        {
            int shift_value = cbxSx5.SelectedIndex;
            if (tbxIx5.Text.Length == 0) return true;
            if (shift_value == 0) return no_shift_warning();

            return true;
        }


        private bool verify_C6()
        {
            int shift_value = cbxSx6.SelectedIndex;
            if (tbxIx6.Text.Length == 0) return true;
            if (shift_value == 0) return no_shift_warning();

            return true;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void row_captain(DataTable dt, DateTime book_date, bool del)
        {
            DataRow row;

            if (tbxIx1.Text.Length == 0) return;
            //if (cbxCapt.SelectedIndex < 0) return;
            
            row = dt.NewRow();

            row["DelMark"] = del;

            row["BookDate"] = book_date;
            row["EmpID"] = tbxIx1.Text;
            row["EmpName"] = cbxEx1.Text;
            //row["ToffCode"] = null;
            row["Hours"] = needDecimal(tbxAx1);
            row["Over"] = needDecimal(tbxOx1);
            //row["LogVessel"] = cbxShips.SelectedValue;
            row["Vessel"] = Vessel;
            row["Shift"] = cbxSx1.SelectedIndex;
            row["ShiftHour"] = cbxSx1.SelectedValue;
            //row["LogNote"] = null;

            dt.Rows.Add(row);

            //dacTimebook.FindAdd(new object[] { book_date, row["EmpID"] }, row);
        }


        private void row_deckhand1(DataTable dt, DateTime book_date, bool del)
        {
            DataRow row;

            if (tbxIx2.Text.Length == 0) return;
            //if (cbxDh1.SelectedIndex < 0) return;

            row = dt.NewRow();

            row["DelMark"] = del;

            row["BookDate"] = book_date;
            row["EmpID"] = cbxEx2.SelectedValue;
            row["EmpName"] = cbxEx2.Text;
            //row["ToffCode"] = null;
            row["Hours"] = needDecimal(tbxAx2);
            row["Over"] = needDecimal(tbxOx2);
            //row["LogVessel"] = cbxShips.SelectedValue;
            row["Vessel"] = Vessel;
            row["Shift"] = cbcSx2.SelectedIndex;
            row["ShiftHour"] = cbcSx2.SelectedValue;

            //row["LogNote"] = null;

            dt.Rows.Add(row);
            //dacTimebook.FindAdd(new object[] { book_date, row["EmpID"] }, row);
        }


        private void row_deckhand2(DataTable dt, DateTime book_date, bool del)
        {
            DataRow row;

            if (tbxIx3.Text.Length == 0) return;
            //if (cbxDh2.SelectedIndex < 0) return;

            row = dt.NewRow();

            row["DelMark"] = del;

            row["BookDate"] = book_date;
            row["EmpID"] = cbxEx3.SelectedValue;
            row["EmpName"] = cbxEx3.Text;
            //row["ToffCode"] = null;
            row["Hours"] = needDecimal(tbxAx3);
            row["Over"] = needDecimal(tbxOx3);
            //row["LogVessel"] = cbxShips.SelectedValue;
            row["Vessel"] = Vessel;
            row["Shift"] = cbxSx3.SelectedIndex;
            row["ShiftHour"] = cbxSx3.SelectedValue;
            //row["LogNote"] = null;

            dt.Rows.Add(row);
            //dacTimebook.FindAdd(new object[] { book_date, row["EmpID"] }, row);
        }


        private void row_mate(DataTable dt, DateTime book_date, bool del)
        {
            DataRow row;

            if (tbxIx4.Text.Length == 0) return;
            //if (cbxMate.SelectedIndex < 0) return;

            row = dt.NewRow();

            row["DelMark"] = del;

            row["BookDate"] = book_date;
            row["EmpID"] = cbxEx4.SelectedValue;
            row["EmpName"] = cbxEx4.Text;
            //row["ToffCode"] = null;
            row["Hours"] = needDecimal(tbxAx4);
            row["Over"] = needDecimal(tbxOx4);
            //row["LogVessel"] = cbxShips.SelectedValue;
            row["Vessel"] = Vessel;
            row["Shift"] = cbxSx4.SelectedIndex;
            row["ShiftHour"] = cbxSx4.SelectedValue;
            //row["LogNote"] = null;

            dt.Rows.Add(row);
            //dacTimebook.FindAdd(new object[] { book_date, row["EmpID"] }, row);
        }


        private void row_engineer(DataTable dt, DateTime book_date, bool del)
        {
            DataRow row;

            if (tbxIx5.Text.Length == 0) return;
            //if (cbxEng.SelectedIndex < 0) return;

            row = dt.NewRow();

            row["DelMark"] = del;

            row["BookDate"] = book_date;
            row["EmpID"] = cbxEx5.SelectedValue;
            row["EmpName"] = cbxEx5.Text;
            //row["ToffCode"] = null;
            row["Hours"] = needDecimal(tbxAx5);
            row["Over"] = needDecimal(tbxOx5);
            //row["LogVessel"] = cbxShips.SelectedValue;
            row["Vessel"] = Vessel;
            row["Shift"] = cbxSx5.SelectedIndex;
            row["ShiftHour"] = cbxSx5.SelectedValue;
            //row["LogNote"] = null;

            dt.Rows.Add(row);
            //dacTimebook.FindAdd(new object[] { book_date, row["EmpID"] }, row);
        }


        private void row_C6(DataTable dt, DateTime book_date, bool del)
        {
            DataRow row;

            if (tbxIx6.Text.Length == 0) return;
            if (cbxEx6.SelectedIndex < 0) return;

            row = dt.NewRow();

            row["DelMark"] = del;

            row["BookDate"] = book_date;
            row["EmpID"] = cbxEx6.SelectedValue;
            row["EmpName"] = cbxEx6.Text;
            //row["ToffCode"] = null;
            row["Hours"] = needDecimal(tbxAx6);
            row["Over"] = needDecimal(tbxOx6);
            //row["LogVessel"] = cbxShips.SelectedValue;
            row["Vessel"] = Vessel;
            row["Shift"] = cbxSx6.SelectedIndex;
            row["ShiftHour"] = cbxSx6.SelectedValue;
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
            tbxLx1.Text = SheetID;
        }

        private void tbxDh1Sheet_Click(object sender, EventArgs e)
        {
            tbxLx2.Text = SheetID;
        }

        private void tbxDh2Sheet_Click(object sender, EventArgs e)
        {
            tbxLx3.Text = SheetID;
        }

        private void tbxMateSheet_Click(object sender, EventArgs e)
        {
            tbxLx4.Text = SheetID;
        }

        private void tbxEngSheet_Click(object sender, EventArgs e)
        {
            tbxLx5.Text = SheetID;
        }

        private void tbxC6Sheet_Click(object sender, EventArgs e)
        {
            tbxLx6.Text = SheetID;
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
