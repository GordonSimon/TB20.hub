using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using mdlAnnal;


namespace letStaff
{
    interface CuePaycodeDialog { void Show(string emp_id, DateTime refdate, string defpaycode); }

    public partial class ucSalary : UserControl
    {
        const string DBO = "dbo.";


        public bool LoadIt { get; set; }
        public bool ShowIt { get; set; }
        public bool DayUnit { get; set; }

        public bool EmpChk { get; set; }
        public string EmpID { get; set; }
        public string EmpName { get; set; }
        public Decimal[] EmpHour;
        public Decimal[] EmpXtrHour;
        public Decimal[] EmpOver;
        public Decimal[] EmpOver1;
        public string[] EmpToff;
        public int[] EmpShift;
        public string[] EmpVessel;
        public string[] EmpNote;
        public string[] EmpPaycode;
        public DayOfWeek[] EmpDow;

        private Decimal _hour_per_day { get; set; }
        
        private bool _dirty { get; set; }
        private bool _delete { get; set; }


        public string StatCode { get; set; }
        public string ShiftVessel { get; set; }
        //public string ShiftCode { get; set; }
        public int ShiftAM { get; set; }
        public string DefPayCode { get; set; }

        public DateTime RefWeek { get; set; }

        private int NoteCount = 0;

       
        //private Dictionary<string, Boatcrew> _boats { get; set; }

        //private TableLayoutPanel _tlpNote;


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public bool IsDirty()
        {
            return _dirty;
        }


        public bool IsDelete()
        {
            return _delete;
        }


        public void QuickFresh(DateTime refdate, Decimal hour, Decimal over, decimal over1, Decimal xtr_hour)
        {
            int iday = ((TimeSpan)(refdate - RefWeek.Date.Date)).Days;
            if (iday < 0 || iday > 13) return;

            EmpHour[iday] = hour;
            EmpOver[iday] = over;
            EmpOver1[iday] = over1;

            EmpXtrHour[iday] = xtr_hour;

            RedrawDays();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public void SaveReset()
        {
            chkEmp.BackColor = SystemColors.Control;
        }


        public void DeleteReset()
        {
            chkEmp.Checked = true;

            clear_week(0, false);
            clear_week(1, false);
            //clear_shift(0);
            //clear_shift(1);

        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private DayOfWeek help_dow(int daynum)
        {
            if (daynum == 7)
                return DayOfWeek.Sunday;
            else
                return (DayOfWeek)(Enum.ToObject(typeof(DayOfWeek), daynum));
        }

        private void constructor(DateTime start_week)
        {
            InitializeComponent();

            _hour_per_day = 8.0M;

            EmpHour = new Decimal[14];
            EmpXtrHour = new Decimal[14];
            EmpOver = new Decimal[14];
            EmpOver1 = new Decimal[14];
            EmpToff = new string[14];
            EmpShift = new int[14];
            EmpNote = new string[14];
            EmpPaycode = new string[14];
            EmpVessel = new string[14];
            EmpDow = new DayOfWeek[14];


            RefWeek = start_week;
            for (int i = 0; i < 7; i++) EmpDow[i] = help_dow(i + 1);
            for (int i = 7; i < 14; i++) EmpDow[i] = help_dow(i + 1 - 7);

            ShowIt = true;
            _dirty = false;

            chk7F.Checked = false;
            chk7S.Checked = false;

            //clear_shift(0);
            //clear_shift(1);
            clear_note(0);
            clear_note(1);

            //lblVessel.Text = string.Empty;
            //ucAMVessel.Hide();

            //ucAM1.Hide();
            //ucAM2.Hide();
            //ucAM3.Hide();
            //ucAM4.Hide();
            //ucAM5.Hide();
            //ucAM6.Hide();
            //ucAM7.Hide();
            //ucAM8.Hide();
            //ucAM9.Hide();
            //ucAM10.Hide();
            //ucAM11.Hide();
            //ucAM12.Hide();
            //ucAM13.Hide();
            //ucAM14.Hide();

            //DataTable dtToff = dacToff.GetDS().Tables[0];
            DataTable dtToff = dacToff.GetDT(DBO);
            this.cbxLegend.DataSource = dtToff;
            this.cbxLegend.ValueMember = "ToffKey";
            this.cbxLegend.DisplayMember = "ToffDesc";

            // Kludge : have to fix the auto select problem
            cbxLegend.SelectedIndex = -1;
            cbxLegend.SelectedIndexChanged += new EventHandler(cbxLegend_SelectedIndexChanged);

            //cbxItems.Items.Add("Salary");
            //cbxItems.Items.Add("Office");
            //cbxItems.Items.Add("Shore");
        }


        //public ucStat(DateTime start_week, DataSet ds, Dictionary<string, Boatcrew> boats)
        //{
        //    constructor(start_week);

        //    _boats = boats;

        //    foreach (DataRow row in ds.Tables[0].Rows)
        //    {
        //        Item it = new Item();
        //        it.Tag = row["Short"];
        //        it.Text = row["Full Name"].ToString();
        //        cbxItems.Items.Add(it);
        //    }

        //    Item ita = new Item();
        //    ita.Tag = 1;
        //    ita.Text = "AM (6AM-6PM)";
        //    cbxShift.Items.Add(ita);

        //    Item itp = new Item();
        //    itp.Tag = 2;
        //    itp.Text = "PM (6PM-6AM)";
        //    cbxShift.Items.Add(itp);

        //    lblAM.Hide();
        //    cbxShift.Show();

        //    chkEmp.CheckedChanged += new EventHandler(chkVes_CheckedChanged);
        //    chk7F.CheckedChanged += new System.EventHandler(chkV7F_CheckedChanged);
        //    chk7S.CheckedChanged += new System.EventHandler(chkV7S_CheckedChanged);
        //}


        public ucSalary(DateTime start_week, string emp_name, string emp_id, string defpaycode, bool _db)
        {
            constructor(start_week);

            EmpChk = true;
            EmpID = emp_id;
            EmpName = emp_name;
            DefPayCode = defpaycode;
            LoadIt = _db;

            //if (DefPayCode.Equals("Office")) _hour_per_day = 7.0M;
            //if (DefPayCode.Equals("Dispatch")) _hour_per_day = 12.0M;

            //_boats = null;

            //cbxItems.Hide();
            cbxShift.Hide();

            chkEmp.Text = EmpName;
            chkEmp.Checked = EmpChk;

            //cbxItems.Text = PayCode;
            tbxDefPayCode.Text = DefPayCode;
            //lblAM.Hide();

            //chkEmp.CheckedChanged += new EventHandler(chkEmp_CheckedChanged);
            //chk7F.CheckedChanged += new System.EventHandler(chkE7F_CheckedChanged);
            //chk7S.CheckedChanged += new System.EventHandler(chkE7S_CheckedChanged);

            chkEmp.MouseUp += new MouseEventHandler(chkEmp_CheckedChanged);
            chk7F.MouseUp += new MouseEventHandler(chkE7F_CheckedChanged);
            chk7S.MouseUp += new MouseEventHandler(chkE7S_CheckedChanged);
        }


        void cbxLegend_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox ctl = (ComboBox)sender;

            StatCode = (string)(ctl.SelectedValue);
            //if (StatCode.Equals("12"))
            //    lblVessel.Text = ShiftVessel;
            //else
            //    lblVessel.Text = string.Empty;

        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void show_note(Panel pnl, string note)
        {
            pnl.Visible = false;

            if (note == null) return;
            if (note.Length == 0) return;

            pnl.Visible = true;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private string show_day(TextBox tbx, string toff, Decimal hours, string paycode, Decimal xtrhour)
        {
            Color c = Color.White;

            if (toff == null) toff = "";
            if (hours != 0.0M || xtrhour != 0.0M)
            {
                //tbx.Text = string.Format("{0} {1:0.0}", toff, hours);
                if (xtrhour != 0.0M)
                {
                    if (hours == 0.0M)
                        tbx.Text = string.Format("{0:0.0}", xtrhour);
                    else
                        tbx.Text = string.Format("{0:0.0}/{1:0.0}", hours, xtrhour);
                    c = Color.Yellow;
                }
                else
                {
                    tbx.Text = string.Format("{0:0.0}", hours);
                    c = PayCodeColour(paycode);
                }

                tbx.BackColor = c;

                if (xtrhour != 0.0M && hours != 0.0M)
                    tbx.ForeColor = Color.Red;
                else
                    tbx.ForeColor = Color.Black;
            }
            else
            {
                tbx.Text = toff;
                tbx.BackColor = Color.White;


                if (toff.Equals("12") || toff.Equals("!12"))
                {
                    tbx.Text = "!12";
                    tbx.ForeColor = Color.DarkOrange;
                }
                else
                    tbx.ForeColor = SystemColors.WindowText;
            }

            return tbx.Text;
        }


        private string show_over(TextBox tbx, string toff, Decimal over, Decimal over1)
        {
            //if (DayUnit) over = over * 8.0M;

            tbx.Text = "";
            if (toff == null) toff = "";
            if (over != 0.0M)
                tbx.Text = string.Format("{0:0.0}", over);
            if (over1 != 0.0M)
                tbx.Text = string.Format("{0:0.0}+", over1);
            if (over != 0.0M && over1 != 0.0M)
                tbx.Text = string.Format("{0:0.0}+{1:0.0}", over1, over);
            //else
            //    tbx.Text = toff;

            tbx.ForeColor = SystemColors.WindowText;
            if (over == 0.0M  && over1 == 0.0M)
            {
                if (toff.Equals("12") || toff.Equals("!12"))
                    tbx.ForeColor = Color.DarkOrange;
            }
            
            return tbx.Text;
        }


        //private bool all_7F(string toff, string vessel, int shift)
        //{
        //    int cnt1 = 0;
        //    string val1 = toff;

        //    cnt1 += (tbxMH.Text.Equals(val1) ? 1 : 0);
        //    cnt1 += (tbxTH.Text.Equals(val1) ? 1 : 0);
        //    cnt1 += (tbxWH.Text.Equals(val1) ? 1 : 0);
        //    cnt1 += (tbxThH.Text.Equals(val1) ? 1 : 0);
        //    cnt1 += (tbxFH.Text.Equals(val1) ? 1 : 0);
        //    cnt1 += (tbxSaH.Text.Equals(val1) ? 1 : 0);
        //    cnt1 += (tbxSuH.Text.Equals(val1) ? 1 : 0);


        //    int cnt2 = 1;
        //    string val2 = vessel;

        //    cnt2 += (lblT.Text.Equals(val2) ? 1 : 0);
        //    cnt2 += (lblW.Text.Equals(val2) ? 1 : 0);
        //    cnt2 += (lblTh.Text.Equals(val2) ? 1 : 0);
        //    cnt2 += (lblF.Text.Equals(val2) ? 1 : 0);
        //    cnt2 += (lblSa.Text.Equals(val2) ? 1 : 0);
        //    cnt2 += (lblSu.Text.Equals(val2) ? 1 : 0);

        //    int cnt3 = 1;
        //    int vali = shift;

        //    cnt3 += (ucAM2.VAL == vali ? 1 : 0);
        //    cnt3 += (ucAM3.VAL == vali ? 1 : 0);
        //    cnt3 += (ucAM4.VAL == vali ? 1 : 0);
        //    cnt3 += (ucAM5.VAL == vali ? 1 : 0);
        //    cnt3 += (ucAM6.VAL == vali ? 1 : 0);
        //    cnt3 += (ucAM7.VAL == vali ? 1 : 0);

        //    if (cnt1 == 7 && cnt2 == 7 && cnt3 == 7) return true;

        //    return false;
        //}


        //private bool all_7S(string toff, string vessel, int shift)
        //{
        //    int cnt1 = 0;
        //    string val1 = toff;

        //    cnt1 += (tbxMH2.Text.Equals(val1) ? 1 : 0);
        //    cnt1 += (tbxTH2.Text.Equals(val1) ? 1 : 0);
        //    cnt1 += (tbxWH2.Text.Equals(val1) ? 1 : 0);
        //    cnt1 += (tbxThH2.Text.Equals(val1) ? 1 : 0);
        //    cnt1 += (tbxFH2.Text.Equals(val1) ? 1 : 0);
        //    cnt1 += (tbxSaH2.Text.Equals(val1) ? 1 : 0);
        //    cnt1 += (tbxSuH2.Text.Equals(val1) ? 1 : 0);


        //    int cnt2 = 1;
        //    string val2 = vessel;

        //    cnt2 += (lblT2.Text.Equals(val2) ? 1 : 0);
        //    cnt2 += (lblW2.Text.Equals(val2) ? 1 : 0);
        //    cnt2 += (lblTh2.Text.Equals(val2) ? 1 : 0);
        //    cnt2 += (lblF2.Text.Equals(val2) ? 1 : 0);
        //    cnt2 += (lblSa2.Text.Equals(val2) ? 1 : 0);
        //    cnt2 += (lblSu2.Text.Equals(val2) ? 1 : 0);

        //    int cnt3 = 1;
        //    int vali = shift;

        //    cnt3 += (ucAM8.VAL == vali ? 1 : 0);
        //    cnt3 += (ucAM10.VAL == vali ? 1 : 0);
        //    cnt3 += (ucAM11.VAL == vali ? 1 : 0);
        //    cnt3 += (ucAM12.VAL == vali ? 1 : 0);
        //    cnt3 += (ucAM13.VAL == vali ? 1 : 0);
        //    cnt3 += (ucAM14.VAL == vali ? 1 : 0);
            

        //    if (cnt1 == 7 && cnt2 == 7 && cnt3 == 7) return true;

        //    return false;
        //}


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        //public void RefreshLegend(string toff, string vessel)
        //{
        //    cbxLegend.SelectedValue = toff;

        //    StatCode = toff;
        //    ShiftVessel = vessel;

        //    lblVessel
        //        .Hide();
        //    lblVessel.Text = string.Empty;
        //    if (vessel != null && vessel.Length > 0)
        //    {
        //        lblVessel.Text = vessel;
        //        lblVessel.Show();
        //    }
        //}


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void draw_iday(int iday)
        {
            Decimal hours = EmpHour[iday];
            Decimal xtrhour = EmpXtrHour[iday];

            Decimal over = EmpOver[iday]; // not needed here - but, now needed :)
            Decimal over1 = EmpOver1[iday];
            string toff = EmpToff[iday];
            //int shift = EmpShift[iday];
            string vessel = EmpVessel[iday];
            string note = EmpNote[iday];
            string paycode = EmpPaycode[iday];
            
            DayOfWeek dow = EmpDow[iday];

            //if (paycode != null)
            //{
            //    Decimal perday = 8.0M;
            //    if (paycode.Equals("Dispatch")) perday = 12.0M;
            //    if (paycode.Equals("Office")) perday = 7.0M;

            //    if (paycode.Equals(DefPayCode))
            //        if (DayUnit || xtrhour != 0.0M) hours = hours * perday;
            //}
 
            if (iday < 7)
            {
                switch (dow)
                {
                    case DayOfWeek.Monday:
                        show_day(tbxMH, toff, hours, paycode, xtrhour);
                        show_over(tbxMC, toff, over, over1);
                        //tb_apply_shift(0, vessel, shift);
                        show_note(pnlM, note);
                        break;

                    case DayOfWeek.Tuesday:
                        show_day(tbxTH, toff, hours, paycode, xtrhour);
                        show_over(tbxTC, toff, over, over1);
                        //tb_apply_shift(1, vessel, shift);
                        show_note(pnlT, note);
                        break;

                    case DayOfWeek.Wednesday:
                        show_day(tbxWH, toff, hours, paycode, xtrhour);
                        show_over(tbxWC, toff, over, over1);
                        //tb_apply_shift(2, vessel, shift);
                        show_note(pnlW, note);
                        break;

                    case DayOfWeek.Thursday:
                        show_day(tbxThH, toff, hours, paycode, xtrhour);
                        show_over(tbxThC, toff, over, over1);
                        //tb_apply_shift(3, vessel, shift);
                        show_note(pnlTh, note);
                        break;

                    case DayOfWeek.Friday:
                        show_day(tbxFH, toff, hours, paycode, xtrhour);
                        show_over(tbxFC, toff, over, over1);
                        //tb_apply_shift(4, vessel, shift);
                        show_note(pnlF, note);
                        break;

                    case DayOfWeek.Saturday:
                        show_day(tbxSaH, toff, hours, paycode, xtrhour);
                        show_over(tbxSaC, toff, over, over1);
                        //tb_apply_shift(5, vessel, shift);
                        show_note(pnlSa, note);
                        break;

                    case DayOfWeek.Sunday:
                        show_day(tbxSuH, toff, hours, paycode, xtrhour);
                        show_over(tbxSuC, toff, over, over1);
                        //tb_apply_shift(6, vessel, shift);
                        show_note(pnlSu, note);
                        break;
                }

                //if (all_7F(toff, vessel, shift))
                //{
                //    mark_week(0, toff);
                //    mark_shift(0, vessel, shift, toff);
                //}

            }
            else
            {
                switch (dow)
                {
                    case DayOfWeek.Monday:
                        show_day(tbxMH2, toff, hours, paycode, xtrhour);
                        show_over(tbxMC2, toff, over, over1);
                        //tb_apply_shift(7, vessel, shift);
                        show_note(pnlM2, note);
                        break;

                    case DayOfWeek.Tuesday:
                        show_day(tbxTH2, toff, hours, paycode, xtrhour);
                        show_over(tbxTC2, toff, over, over1);
                        //tb_apply_shift(8, vessel, shift);
                        show_note(pnlT2, note);
                        break;

                    case DayOfWeek.Wednesday:
                        show_day(tbxWH2, toff, hours, paycode, xtrhour);
                        show_over(tbxWC2, toff, over, over1);
                        //tb_apply_shift(9, vessel, shift);
                        show_note(pnlW2, note);
                        break;

                    case DayOfWeek.Thursday:
                        show_day(tbxThH2, toff, hours, paycode, xtrhour);
                        show_over(tbxThC2, toff, over, over1);
                        //tb_apply_shift(10, vessel, shift);
                        show_note(pnlTh2, note);
                        break;

                    case DayOfWeek.Friday:
                        show_day(tbxFH2, toff, hours, paycode, xtrhour);
                        show_over(tbxFC2, toff, over, over1);
                        //tb_apply_shift(11, vessel, shift);
                        show_note(pnlF2, note);
                        break;

                    case DayOfWeek.Saturday:
                        show_day(tbxSaH2, toff, hours, paycode, xtrhour);
                        show_over(tbxSaC2, toff, over, over1);
                        //tb_apply_shift(12, vessel, shift);
                        show_note(pnlSa2, note);
                        break;

                    case DayOfWeek.Sunday:
                        show_day(tbxSuH2, toff, hours, paycode, xtrhour);
                        show_over(tbxSuC2, toff, over, over1);
                        //tb_apply_shift(13, vessel, shift);
                        show_note(pnlSu2, note);
                        break;
                }


                //if (all_7S(toff, vessel, shift))
                //{
                //    mark_week(1, toff);
                //    mark_shift(1, vessel, shift, toff);
                //}

            }


            //chk = all_7F(toff);
            ////chk7S.Checked = chk;
            //if (chk)
            //    mark_week(1, toff);


            //chk = all_7S(toff);
            ////chk7S.Checked = chk;
            //if (chk)
            //    mark_week(1, toff);


        }


        public void RedrawDays()
        {
            for (int iday = 0; iday < 14; iday++)
                draw_iday(iday);
        }


        public void RefreshDay(DateTime day, string toff,
            Decimal hours, Decimal over, Decimal over1, string vessel, int shift, string note, string paycode)
        {
            //tbxRef.Text = RefWeek.ToShortDateString();

            int iday = ((TimeSpan)(day - RefWeek.Date.Date)).Days;
            if (iday < 0 || iday > 13) return;

            if (paycode.Equals(DefPayCode))
                EmpHour[iday] += hours;
            else
                EmpXtrHour[iday] += hours;

            EmpOver[iday] += over;
            EmpOver1[iday] += over1;

            if (! toff.Equals(string.Empty)) EmpToff[iday] = toff;
            //EmpShift[iday] = shift;
            if (! vessel.Equals(string.Empty))  EmpVessel[iday] = vessel;
            

            if (!paycode.Equals(string.Empty))
            {
                if (EmpPaycode[iday] == null || EmpPaycode[iday].Equals(string.Empty)) EmpPaycode[iday] = paycode;
                if (!EmpPaycode[iday].Equals(DefPayCode)) EmpPaycode[iday] = paycode;
            }

            if (note != null && note.Length > 0)
            {
                EmpNote[iday] = note;
                NoteCount += 1;
                nudComment.Value += 1;
            }


            if (EmpDow[iday] != day.DayOfWeek)
                //EmpDow[iday] = day.DayOfWeek;
                MessageBox.Show(String.Format("Day of Week Problem ! iday[{0}], EmpDow[{1}] day[{2}]", iday,
                    EmpDow[iday], day.DayOfWeek));

            draw_iday(iday);
        }


        /*
        public void RefreshDay(DateTime day, string toff, int hours, string vessel, int shift)
        {            
            int iday = ((TimeSpan)(day - RefWeek)).Days + 1;            
            if (iday < 0 || iday > 13) return;
                                    
            RcdStat.hour[iday] = hours;            
            RcdStat.toff[iday] = toff;

            DayOfWeek dow = day.DayOfWeek;
            

            if (iday < 7)
            {
                switch (dow)
                {
                    case DayOfWeek.Monday:
                        show_day(tbxMH, toff, hours);
                        tb_apply_shift(0, vessel, shift);
                        break;

                    case DayOfWeek.Tuesday:
                        show_day(tbxTH, toff, hours);
                        tb_apply_shift(1, vessel, shift);
                        break;

                    case DayOfWeek.Wednesday:
                        show_day(tbxWH, toff, hours);
                        tb_apply_shift(2, vessel, shift);
                        break;

                    case DayOfWeek.Thursday:
                        show_day(tbxThH, toff, hours);
                        tb_apply_shift(3, vessel, shift);
                        break;

                    case DayOfWeek.Friday:
                        show_day(tbxFH, toff, hours);
                        tb_apply_shift(4, vessel, shift);
                        break;

                    case DayOfWeek.Saturday:
                        show_day(tbxSaH, toff, hours);
                        tb_apply_shift(5, vessel, shift);
                        break;

                    case DayOfWeek.Sunday:
                        show_day(tbxSuH, toff, hours);
                        tb_apply_shift(6, vessel, shift);
                        break;
                }
            }
            else
            {
                switch (dow)
                {
                    case DayOfWeek.Monday:
                        show_day(tbxMH2, toff, hours);
                        tb_apply_shift(7, vessel, shift);
                        break;

                    case DayOfWeek.Tuesday:
                        show_day(tbxTH2, toff, hours);
                        tb_apply_shift(8, vessel, shift);
                        break;

                    case DayOfWeek.Wednesday:
                        show_day(tbxWH2, toff, hours);
                        tb_apply_shift(9, vessel, shift);
                        break;

                    case DayOfWeek.Thursday:
                        show_day(tbxThH2, toff, hours);
                        tb_apply_shift(10, vessel, shift);
                        break;

                    case DayOfWeek.Friday:
                        show_day(tbxFH2, toff, hours);
                        tb_apply_shift(11, vessel, shift);
                        break;

                    case DayOfWeek.Saturday:
                        show_day(tbxSaH2, toff, hours);
                        tb_apply_shift(12, vessel, shift);
                        break;

                    case DayOfWeek.Sunday:
                        show_day(tbxSuH2, toff, hours);
                        tb_apply_shift(13, vessel, shift);
                        break;
                }
            }

            bool chk = all_7F(toff);
            chk7F.Checked = chk;
            if (chk)
                mark_week(0, toff);

            chk = all_7S(toff);
            chk7S.Checked = chk;
            if (chk)                
                mark_week(1, toff);            

        }
        */

        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/
        
        private void clear_week(int week, bool remove)
        {
            Color shade = Color.White;
            //Color shade = Color.FromArgb(192, 255, 192);
            if (remove) shade = Color.Red;

            if (week == 0)
            {
                tbxMH.BackColor = shade;
                tbxTH.BackColor = shade;
                tbxWH.BackColor = shade;
                tbxThH.BackColor = shade;
                tbxFH.BackColor = shade;
                tbxSaH.BackColor = shade;
                tbxSuH.BackColor = shade;

                tbxMH.Text = "";
                tbxTH.Text = "";
                tbxWH.Text = "";
                tbxThH.Text = "";
                tbxFH.Text = "";
                tbxSaH.Text = "";
                tbxSuH.Text = "";

                tbxMC.Text = "";
                tbxTC.Text = "";
                tbxWC.Text = "";
                tbxThC.Text = "";
                tbxFC.Text = "";
                tbxSaC.Text = "";
                tbxSuC.Text = "";

                EmpXtrHour[0] = EmpXtrHour[1] = EmpXtrHour[2] =
                    EmpXtrHour[3] = EmpXtrHour[4] = EmpXtrHour[5] = EmpXtrHour[6] = 0;
                EmpHour[0] = EmpHour[1] = EmpHour[2] =
                    EmpHour[3] = EmpHour[4] = EmpHour[5] = EmpHour[6] = 0;
                EmpOver[0] = EmpOver[1] = EmpOver[2] =
                    EmpOver[3] = EmpOver[4] = EmpOver[5] = EmpOver[6] = 0;
                EmpOver1[0] = EmpOver1[1] = EmpOver1[2] =
                    EmpOver1[3] = EmpOver1[4] = EmpOver1[5] = EmpOver1[6] = 0;
                EmpToff[0] = EmpToff[1] = EmpToff[2] =
                    EmpToff[3] = EmpToff[4] = EmpToff[5] = EmpToff[6] = "";

                EmpNote[0] = EmpNote[1] = EmpNote[2] =
                 EmpNote[3] = EmpNote[4] = EmpNote[5] = EmpNote[6] = "";

                //EmpVessel[0] = EmpVessel[1] = EmpVessel[2] =
                //    EmpVessel[3] = EmpVessel[4] = EmpVessel[5] = EmpVessel[6] = "";
                //EmpShift[0] = EmpShift[1] = EmpShift[2] =
                //    EmpShift[3] = EmpShift[4] = EmpShift[5] = EmpShift[6] = 0;

                chk7F.Checked = false;
            }
            else
            {
                tbxMH2.BackColor = shade;
                tbxTH2.BackColor = shade;
                tbxWH2.BackColor = shade;
                tbxThH2.BackColor = shade;
                tbxFH2.BackColor = shade;
                tbxSaH2.BackColor = shade;
                tbxSuH2.BackColor = shade;

                tbxMH2.Text = "";
                tbxTH2.Text = "";
                tbxWH2.Text = "";
                tbxThH2.Text = "";
                tbxFH2.Text = "";
                tbxSaH2.Text = "";
                tbxSuH2.Text = "";

                tbxMC2.Text = "";
                tbxTC2.Text = "";
                tbxWC2.Text = "";
                tbxThC2.Text = "";
                tbxFC2.Text = "";
                tbxSaC2.Text = "";
                tbxSuC2.Text = "";

                EmpXtrHour[7] = EmpXtrHour[8] = EmpXtrHour[9] =
                    EmpXtrHour[10] = EmpXtrHour[11] = EmpXtrHour[12] = EmpXtrHour[13] = 0;
                EmpHour[7] = EmpHour[8] = EmpHour[9] =
                    EmpHour[10] = EmpHour[11] = EmpHour[12] = EmpHour[13] = 0;
                EmpOver[7] = EmpOver[8] = EmpOver[9] =
                    EmpOver[10] = EmpOver[11] = EmpOver[12] = EmpOver[13] = 0;
                EmpOver1[7] = EmpOver1[8] = EmpOver1[9] =
                    EmpOver1[10] = EmpOver1[11] = EmpOver1[12] = EmpOver1[13] = 0;
                EmpToff[7] = EmpToff[8] = EmpToff[9] =
                    EmpToff[10] = EmpToff[11] = EmpToff[12] = EmpToff[13] = "";


                EmpNote[7] = EmpNote[8] = EmpNote[9] =
                 EmpNote[10] = EmpNote[11] = EmpNote[12] = EmpNote[13] = "";

                //EmpVessel[7] = EmpVessel[8] = EmpVessel[9] =
                //    EmpVessel[10] = EmpVessel[11] = EmpVessel[12] = EmpVessel[13] = "";
                //EmpShift[7] = EmpShift[8] = EmpShift[9] =
                //    EmpShift[10] = EmpShift[11] = EmpShift[12] = EmpShift[13] = 0;

                chk7S.Checked = false;
            }

        }


        private void mark_week(int week, string val)
        {
            if (week == 0)
            {
                tbxMH.BackColor = Color.AliceBlue;
                tbxTH.BackColor = Color.AliceBlue;
                tbxWH.BackColor = Color.AliceBlue;
                tbxThH.BackColor = Color.AliceBlue;
                tbxFH.BackColor = Color.AliceBlue;
                tbxSaH.BackColor = Color.AliceBlue;
                tbxSuH.BackColor = Color.AliceBlue;

                tbxMH.Text = val;
                tbxTH.Text = val;
                tbxWH.Text = val;
                tbxThH.Text = val;
                tbxFH.Text = val;
                tbxSaH.Text = val;
                tbxSuH.Text = val;

                if (val != null && val.Equals("12"))
                {
                    tbxMH.ForeColor = Color.DarkOrange;
                    tbxTH.ForeColor = Color.DarkOrange;
                    tbxWH.ForeColor = Color.DarkOrange;
                    tbxThH.ForeColor = Color.DarkOrange;
                    tbxFH.ForeColor = Color.DarkOrange;
                    tbxSaH.ForeColor = Color.DarkOrange;
                    tbxSuH.ForeColor = Color.DarkOrange;
                }

                EmpHour[0] = EmpHour[1] = EmpHour[2] =
                    EmpHour[3] = EmpHour[4] = EmpHour[5] = EmpHour[6] = 0;

                EmpXtrHour[0] = EmpXtrHour[1] = EmpXtrHour[2] =
                    EmpXtrHour[3] = EmpXtrHour[4] = EmpXtrHour[5] = EmpXtrHour[6] = 0;


                EmpOver[0] = EmpOver[1] = EmpOver[2] =
                    EmpOver[3] = EmpOver[4] = EmpOver[5] = EmpOver[6] = 0;
                EmpOver1[0] = EmpOver1[1] = EmpOver1[2] =
                   EmpOver1[3] = EmpOver1[4] = EmpOver1[5] = EmpOver1[6] = 0;
                EmpToff[0] = EmpToff[1] = EmpToff[2] =
                    EmpToff[3] = EmpToff[4] = EmpToff[5] = EmpToff[6] = val;

                chk7F.Checked = true;
            }
            else
            {
                tbxMH2.BackColor = Color.AliceBlue;
                tbxTH2.BackColor = Color.AliceBlue;
                tbxWH2.BackColor = Color.AliceBlue;
                tbxThH2.BackColor = Color.AliceBlue;
                tbxFH2.BackColor = Color.AliceBlue;
                tbxSaH2.BackColor = Color.AliceBlue;
                tbxSuH2.BackColor = Color.AliceBlue;

                tbxMH2.Text = val;
                tbxTH2.Text = val;
                tbxWH2.Text = val;
                tbxThH2.Text = val;
                tbxFH2.Text = val;
                tbxSaH2.Text = val;
                tbxSuH2.Text = val;

                if (val.Equals("12"))
                {
                    tbxMH2.ForeColor = Color.DarkOrange;
                    tbxTH2.ForeColor = Color.DarkOrange;
                    tbxWH2.ForeColor = Color.DarkOrange;
                    tbxThH2.ForeColor = Color.DarkOrange;
                    tbxFH2.ForeColor = Color.DarkOrange;
                    tbxSaH2.ForeColor = Color.DarkOrange;
                    tbxSuH2.ForeColor = Color.DarkOrange;
                }

                EmpHour[7] = EmpHour[8] = EmpHour[9] =
                    EmpHour[10] = EmpHour[11] = EmpHour[12] = EmpHour[13] = 0;

                EmpXtrHour[7] = EmpXtrHour[8] = EmpXtrHour[9] =
                    EmpXtrHour[10] = EmpXtrHour[11] = EmpXtrHour[12] = EmpXtrHour[13] = 0;

                EmpOver[7] = EmpOver[8] = EmpOver[9] =
                    EmpOver[10] = EmpOver[11] = EmpOver[12] = EmpOver[13] = 0;


                EmpOver1[7] = EmpOver1[8] = EmpOver1[9] =
                    EmpOver1[10] = EmpOver1[11] = EmpOver1[12] = EmpOver1[13] = 0;

                EmpToff[7] = EmpToff[8] = EmpToff[9] =
                    EmpToff[10] = EmpToff[11] = EmpToff[12] = EmpToff[13] = val;

                chk7S.Checked = true;
            }

        }


        //public void RefreshWeek(string val)
        public void RefreshWeek()
        {
            //StatCode = val;

            if (!chkEmp.Checked) return;

            if (!chk7F.Checked) return;
            mark_week(0, StatCode);

            if (!chk7S.Checked) return;
            mark_week(1, StatCode);
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public void ResetWeek(DateTime start_week)
        {
            RefWeek = start_week;

            clear_week(0, false);
            clear_week(1, false);
            //clear_shift(0);
            //clear_shift(1);
            clear_note(0);
            clear_note(1);
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void clear_note(int week)
        {
            if (week == 0)
            {
                pnlM.Visible = false;
                pnlT.Visible = false;
                pnlW.Visible = false;
                pnlTh.Visible = false;
                pnlF.Visible = false;
                pnlSa.Visible = false;
                pnlSu.Visible = false;
            }
            else
            {
                pnlM2.Visible = false;
                pnlT2.Visible = false;
                pnlW2.Visible = false;
                pnlTh2.Visible = false;
                pnlF2.Visible = false;
                pnlSa2.Visible = false;
                pnlSu2.Visible = false;
            }
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        //private void tb_apply_boat(int idx, Boat b)
        //{
        //    int shft = b.dayshft[idx];
        //    string code = b.daycode[idx];

        //    switch (idx)
        //    {
        //        case 0: lblM.Text = code; ucAM1.On(shft); break;
        //        case 1: lblT.Text = code; ucAM2.On(shft); break;
        //        case 2: lblW.Text = code; ucAM3.On(shft); break;
        //        case 3: lblTh.Text = code; ucAM4.On(shft); break;
        //        case 4: lblF.Text = code; ucAM5.On(shft); break;
        //        case 5: lblSa.Text = code; ucAM6.On(shft); break;
        //        case 6: lblSu.Text = code; ucAM7.On(shft); break;

        //        case 7: lblM2.Text = code; ucAM8.On(shft); break;
        //        case 8: lblT2.Text = code; ucAM9.On(shft); break;
        //        case 9: lblW2.Text = code; ucAM10.On(shft); break;
        //        case 10: lblTh2.Text = code; ucAM11.On(shft); break;
        //        case 11: lblF2.Text = code; ucAM12.On(shft); break;
        //        case 12: lblSa2.Text = code; ucAM13.On(shft); break;
        //        case 13: lblSu2.Text = code; ucAM14.On(shft); break;
        //    }
        //}


        //private void tb_apply_shift(int idx, string v, int s)
        //{
        //    switch (idx)
        //    {
        //        case 0: lblM.Text = v; ucAM1.On(s); break;
        //        case 1: lblT.Text = v; ucAM2.On(s); break;
        //        case 2: lblW.Text = v; ucAM3.On(s); break;
        //        case 3: lblTh.Text = v; ucAM4.On(s); break;
        //        case 4: lblF.Text = v; ucAM5.On(s); break;
        //        case 5: lblSa.Text = v; ucAM6.On(s); break;
        //        case 6: lblSu.Text = v; ucAM7.On(s); break;

        //        case 7: lblM2.Text = v; ucAM8.On(s); break;
        //        case 8: lblT2.Text = v; ucAM9.On(s); break;
        //        case 9: lblW2.Text = v; ucAM10.On(s); break;
        //        case 10: lblTh2.Text = v; ucAM11.On(s); break;
        //        case 11: lblF2.Text = v; ucAM12.On(s); break;
        //        case 12: lblSa2.Text = v; ucAM13.On(s); break;
        //        case 13: lblSu2.Text = v; ucAM14.On(s); break;
        //    }
        //}


        //private void mark_shift(int week, string vessel, int am, string toff)
        //{
        //    if (week == 0)
        //    {
        //        lblM.Text = vessel;
        //        lblT.Text = vessel;
        //        lblW.Text = vessel;
        //        lblTh.Text = vessel;
        //        lblF.Text = vessel;
        //        lblSa.Text = vessel;
        //        lblSu.Text = vessel;

        //        ucAM1.On(am);
        //        ucAM2.On(am);
        //        ucAM3.On(am);
        //        ucAM4.On(am);
        //        ucAM5.On(am);
        //        ucAM6.On(am);
        //        ucAM7.On(am);

        //        if (toff.Equals("12"))
        //        {
        //            EmpVessel[0] = EmpVessel[1] = EmpVessel[2] =
        //                EmpVessel[3] = EmpVessel[4] = EmpVessel[5] = EmpVessel[6] = vessel;
        //            EmpShift[0] = EmpShift[1] = EmpShift[2] =
        //                EmpShift[3] = EmpShift[4] = EmpShift[5] = EmpShift[6] = 0;
        //        }
        //        else
        //        {
        //            EmpVessel[0] = EmpVessel[1] = EmpVessel[2] =
        //                EmpVessel[3] = EmpVessel[4] = EmpVessel[5] = EmpVessel[6] = "";
        //            EmpShift[0] = EmpShift[1] = EmpShift[2] =
        //                EmpShift[3] = EmpShift[4] = EmpShift[5] = EmpShift[6] = 0;
        //        }

        //    }
        //    else
        //    {
        //        lblM2.Text = vessel;
        //        lblT2.Text = vessel;
        //        lblW2.Text = vessel;
        //        lblTh2.Text = vessel;
        //        lblF2.Text = vessel;
        //        lblSa2.Text = vessel;
        //        lblSu2.Text = vessel;

        //        ucAM8.On(am);
        //        ucAM9.On(am);
        //        ucAM10.On(am);
        //        ucAM11.On(am);
        //        ucAM12.On(am);
        //        ucAM13.On(am);
        //        ucAM14.On(am);


        //        if (toff.Equals("12"))
        //        {
        //            EmpVessel[7] = EmpVessel[8] = EmpVessel[9] =
        //                EmpVessel[10] = EmpVessel[11] = EmpVessel[12] = EmpVessel[13] = vessel;
        //            EmpShift[7] = EmpShift[8] = EmpShift[9] =
        //                EmpShift[10] = EmpShift[11] = EmpShift[12] = EmpShift[13] = 0;
        //        }
        //        else
        //        {
        //            EmpVessel[7] = EmpVessel[8] = EmpVessel[9] =
        //                EmpVessel[10] = EmpVessel[11] = EmpVessel[12] = EmpVessel[13] = "";
        //            EmpShift[7] = EmpShift[8] = EmpShift[9] =
        //                EmpShift[10] = EmpShift[11] = EmpShift[12] = EmpShift[13] = 0;
        //        }
        //    }
        //}


        //private void clear_shift(int week)
        //{
        //    lblVessel.Text = "";
        //    ucAMVessel.Hide();

        //    if (week == 0)
        //    {

        //        lblM.Text = "";
        //        lblT.Text = "";
        //        lblW.Text = "";
        //        lblTh.Text = "";
        //        lblF.Text = "";
        //        lblSa.Text = "";
        //        lblSu.Text = "";

        //        ucAM1.Hide();
        //        ucAM2.Hide();
        //        ucAM3.Hide();
        //        ucAM4.Hide();
        //        ucAM5.Hide();
        //        ucAM6.Hide();
        //        ucAM7.Hide();
        //    }
        //    else
        //    {
        //        lblM2.Text = "";
        //        lblT2.Text = "";
        //        lblW2.Text = "";
        //        lblTh2.Text = "";
        //        lblF2.Text = "";
        //        lblSa2.Text = "";
        //        lblSu2.Text = "";

        //        ucAM8.Hide();
        //        ucAM9.Hide();
        //        ucAM10.Hide();
        //        ucAM11.Hide();
        //        ucAM12.Hide();
        //        ucAM13.Hide();
        //        ucAM14.Hide();

        //    }
        //}


        //public void RefreshShift(string val, int am)
        //public void RefreshShift(Boatcrew boat, int shift)
        //public void RefreshShift(Boat boat)
        //{
        //    int shift = 1;
        //    //ShiftCode = val;
        //    //ShiftAM = am;

        //    if (!chkEmp.Checked) return;
        //    if (!chk7F.Checked) return;

        //    //mark_shift(0, ShiftCode, ShiftAM);
        //    mark_shift(0, "12", shift, ShiftVessel);
        //    //for (int i = 0; i < 14; i++) tb_apply_shift(i, boat);
        //    for (int i = 0; i < 14; i++) tb_apply_boat(i, boat);
        //}


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        //private Boatcrew boat_make(string boat, string name, string shift, Dictionary<string, Boatcrew> bc)
        //{
        //    Boatcrew b = null;

        //    if (bc.ContainsKey(boat))
        //        b = _boats[boat];
        //    else
        //    {
        //        b = new Boatcrew();
        //        b.boatname = name;
        //        b.shift = shift;
        //        bc.Add(boat, b);
        //    }

        //    return b;
        //}


        //private Boatcrew boat_update(int week, string boat, int am, Dictionary<string, Boatcrew> bc, int idx)
        //{
        //    Boatcrew b = null;

        //    int s = 0;

        //    if (bc.ContainsKey(boat))
        //    {
        //        b = _boats[boat];

        //        if (idx >= 0)
        //        {
        //            b.boat[s, week * 7 + idx] = boat; b.am[s, week * 7 + idx] = am;
        //        }
        //        else
        //            for (int i = 0; i < 7; i++) { b.boat[s, week * 7 + i] = boat; b.am[s, week * 7 + i] = am; }

        //    }
        //    return b;
        //}


        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        //private void applyB_code(TextBox tbx, int day, string val, string vessel)
        //{
        //    if (day < 7)
        //    {
        //        if (chk7F.Checked) return;
        //    }
        //    else
        //    {
        //        if (chk7S.Checked) return;
        //    }

        //    if (tbx.Text.Equals(""))
        //        tbx.Text = val;
        //    else
        //        tbx.Text = "";

        //    EmpHour[day] = 0;
        //    EmpOver[day] = 0;
        //    EmpOver1[day] = 0;
        //    EmpToff[day] = tbx.Text;
        //    EmpShift[day] = 0;
        //    EmpVessel[day] = vessel;

        //    if (val == null || !val.Equals("12"))
        //        EmpVessel[day] = null;

        //    draw_iday(day);

        //}


        //private void apply0_code(TextBox tbx, string val)
        //{
        //    if (chk7F.Checked) return;

        //    if (tbx.Text.Equals(""))
        //        tbx.Text = val;
        //    else
        //        tbx.Text = "";
        //}


        //private void apply1_code(TextBox tbx, string val)
        //{
        //    if (chk7S.Checked) return;

        //    if (tbx.Text.Equals(""))
        //        tbx.Text = val;
        //    else
        //        tbx.Text = "";
        //}


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/


        private void make_dirty()
        {
            _dirty = true;
            chkEmp.BackColor = Color.Yellow;

            Button cmdSave = (Button)(this.ParentForm.Controls.Find("cmdSave", true)[0]);
            cmdSave.Visible = true;
        }


        private void make_delete(bool undo)
        {
            if (undo)
            {
                _delete = false;
                chkEmp.BackColor = Color.Yellow;

                Button cmdSave = (Button)(this.ParentForm.Controls.Find("cmdSave", true)[0]);
                cmdSave.Text = "Delete";
                cmdSave.Visible = true;

            }
            else
            {
                _delete = true;
                chkEmp.BackColor = SystemColors.Control;

                Button cmdSave = (Button)(this.ParentForm.Controls.Find("cmdSave", true)[0]);
                cmdSave.Text = "Delete";
                cmdSave.Visible = true;
            }
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

        private void apply_hour(int day, TextBox tbx)
        {
            string paycode = tbxDefPayCode.Text;
            if (DayUnit) return;
            if (paycode.Equals("DH") || paycode.Equals("SK")) return;

            Decimal h = needDecimal(tbx);
            //h = (DayUnit ? h / _hour_per_day : h);

            if (tbx.Text.Length == 0)
                h = 1.0M;
            else
            {                
                if (h == 0.5M) h = 0.0M;
                else
                {
                    if (h == 1.0M) h = 0.5M;
                    else
                        h = h + 0.0625M;
                }
            }
            //tbx.Text = (DayUnit ? h * _hour_per_day : h).ToString("G29");
            tbx.Text = h.ToString("G29");
            if (tbx.Text.Equals("1")) tbx.Text = "1.0";
            if (tbx.Text.Equals("0")) tbx.Text = "0.0";

            //if (tbx.Text.Length == 0)
            //    h = 1.0M;
            //else
            //{
            //    if (tbx.Text.Equals("1.0"))
            //        h = 0.5M;
            //    else if (tbx.Text.Equals("0.5"))
            //        h = 0.0M;
            //    else
            //        h = 1.0M;
            //}
            //tbx.Text = h.ToString();

            EmpHour[day] = h;
            EmpToff[day] = null;
            EmpShift[day] = 0;
            EmpVessel[day] = null;

            if (tbx.Equals("12") || tbx.Equals("!12"))
                tbx.ForeColor = Color.DarkOrange;
            else
                tbx.ForeColor = SystemColors.WindowText;

            make_dirty();
        }


        private void apply_over(int day, TextBox tbx)
        {
            string paycode = tbxDefPayCode.Text;
            if (!DayUnit) return;
            if (paycode.Equals("DH") || paycode.Equals("SK")) return;

            // work in hours only
            Decimal h = needDecimal(tbx);

            if (tbx.Text.Length == 0)
                h = 0.5M;
            else
            {
                //if (h == _hour_per_day) h = 0.0M;
                if (h == 12.0M) h = 0.0M;
                else
                    h = h + 0.5M;
            }
            tbx.Text = h.ToString("G29");
            //if (tbx.Text.Equals("1")) tbx.Text = "1.0";
            //if (tbx.Text.Equals("0")) tbx.Text = "0.0";

            //if (tbx.Text.Length == 0)
            //    h = 1.0M;
            //else
            //{
            //    if (tbx.Text.Equals("1.0"))
            //        h = 0.5M;
            //    else if (tbx.Text.Equals("0.5"))
            //        h = 0.0M;
            //    else
            //        h = 1.0M;
            //}
            

            EmpOver[day] = h;
            EmpToff[day] = null;
            EmpShift[day] = 0;
            EmpVessel[day] = null;

            if (tbx.Equals("12") || tbx.Equals("!12"))
                tbx.ForeColor = Color.DarkOrange;
            else
                tbx.ForeColor = SystemColors.WindowText;

            make_dirty();
        }

        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void tbxDayOver_Click(object sender, EventArgs e)
        {
            // tag has day of week
            apply_over(Convert.ToInt32(((TextBox)sender).Tag), (TextBox)sender);
        }

        
        private void tbxDay_Click(object sender, EventArgs e)
        {
            apply_hour(Convert.ToInt32(((TextBox)sender).Tag), (TextBox)sender);
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void nudComment_ValueChanged(object sender, EventArgs e)
        {
            //_tlpNote = (TableLayoutPanel)(this.ParentForm.Controls.Find("tlpNote", false)[0]);
            //_tlpNote.Controls.Add(uc);

            //_tlpNote.Show();
            //_tlpNote.BringToFront();
        }



        private void nudComment_Click(object sender, EventArgs e)
        {
            ucNote uc = new ucNote(EmpName, "", DateTime.Now.Date);
            uc.EditNote();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void chkEmp_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            EmpChk = chk.Checked;

            if (chk.Checked)
            {
                mark_week(0, StatCode);
                mark_week(1, StatCode);
                //mark_shift(0, ShiftVessel, ShiftAM, StatCode);
                //mark_shift(1, ShiftVessel, ShiftAM, StatCode);

                make_delete(true);
            }
            else
            {
                clear_week(0, true);
                clear_week(1, true);
                //clear_shift(0);
                //clear_shift(1);

                make_delete(false);
            }            
        }


        private void chkE7F_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            if (!chkEmp.Checked) return;

            //if (chk.Checked)
            //    mark_week(0, StatCode);
            //else
            //{
            //    clear_week(0, false);
            //    clear_shift(0);
            //}

            if (chk.Checked)
            {
                mark_week(0, StatCode);
                //mark_shift(0, ShiftVessel, ShiftAM, StatCode);
                //clear_shift(0);
            }
            else
            {
                clear_week(0, false);
                //clear_shift(0);
            }

            make_dirty();
        }


        private void chkE7S_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            if (!chkEmp.Checked) return;

            //if (chk.Checked)
            //    mark_week(1, StatCode);
            //else
            //{
            //    clear_week(1, false);
            //    clear_shift(1);
            //}

            if (chk.Checked)
            {
                mark_week(1, StatCode);
                //clear_shift(1);
            }
            else
            {
                clear_week(1, false);
                //clear_shift(1);
            }

            make_dirty();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void chkVes_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            /*
            if (chk.Checked)
            {
                Item its = (Item)(cbxShift.SelectedItem);
                Item itv = (Item)(cbxItems.SelectedItem);

                //ShiftCode = (itv == null ? "" : itv.Tag.ToString()) + (its == null ? "" : its.Tag.ToString());
                ShiftCode = (itv == null ? "" : itv.Tag.ToString());
                ShiftAM = (its == null ? 0 : (Int32)(its.Tag));

                if (chk7F.Checked)  mark_week(0, ShiftCode);
                if (chk7S.Checked)  mark_week(1, ShiftCode);


                TextBox tbxBoat = (TextBox)(this.ParentForm.Tag);
                tbxBoat.Tag = _boats[ShiftCode];
                tbxBoat.Text = itv.Text;

            }
            else
            {
                ShiftCode = "";
                ShiftAM = 0;
                clear_week(0);
                clear_week(1);
            }

            TextBox tbx = (TextBox)(this.ParentForm.Controls.Find("tbxShift", true)[0]);
            tbx.Tag = _boats[ShiftCode];  // needs to be first
            tbx.Text = ShiftCode + ShiftAM.ToString();
            */
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void NoteClick(int day, Panel pnl)
        {
            ucNote uc = new ucNote(EmpName, EmpNote[day], RefWeek.AddDays(day));
            uc.EditNote();
            if ( uc.IsAccept() )
            {
                EmpNote[day] = uc.GetMemo();

                if (uc.IsDelete())
                    pnl.Visible = false;
                else
                    pnl.Visible = true;

                make_dirty();
            }
        }


        //private void HourClick(int day)
        //{
        //    Decimal hour = EmpHour[day];
        //    Decimal over = EmpOver[day];
        //    string vessel = EmpVessel[day];

        //    ucHour uc = new ucHour(EmpName, RefWeek.AddDays(day), hour, over, vessel);
        //    uc.ShowHour();
        //    if ( uc.IsAccept() )
        //    {                
        //        EmpToff[day] = "";

        //        EmpHour[day] = uc.GetHour();
        //        EmpOver[day] = uc.GetOver();
        //        EmpVessel[day] = uc.GetVessel();

        //        draw_iday(day);

        //        make_dirty();
        //    }
        //}


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void cmsNoteM_Click(object sender, EventArgs e)
        {
            NoteClick(0, pnlM);
        }

        private void cmsNoteT_Click(object sender, EventArgs e)
        {
            NoteClick(1, pnlT);
        }


        private void cmsNoteTh_Click(object sender, EventArgs e)
        {
            NoteClick(3, pnlTh);
        }


        private void cmsNoteF_Click(object sender, EventArgs e)
        {
            NoteClick(4, pnlF);
        }


        private void cmsNoteSa_Click(object sender, EventArgs e)
        {
            NoteClick(5, pnlSa);
        }


        private void cmsNoteSu_Click(object sender, EventArgs e)
        {
            NoteClick(6, pnlSu);
        }


        private void cmsNoteT2_Click(object sender, EventArgs e)
        {
            NoteClick(8, pnlT2);
        }


        private void cmsNoteW2_Click(object sender, EventArgs e)
        {
            NoteClick(9, pnlW2);
        }


        private void cmsNoteTh2_Click(object sender, EventArgs e)
        {
            NoteClick(10, pnlTh2);
        }


        private void cmsNoteF2_Click(object sender, EventArgs e)
        {
            NoteClick(11, pnlF2);
        }


        private void cmsNoteSu2_Click(object sender, EventArgs e)
        {
            NoteClick(13, pnlSu2);
        }

        
        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void PaycodeClick(int day)
        {
            //string toff = EmpToff[day];
            //if (! toff.Equals(string.Empty))
            //{
            //    MessageBox.Show("Please remove schedule code, before you add hours !", "Error");
            //    return;
            //}

            Decimal hour = EmpHour[day];
            Decimal over = EmpOver[day];
            string vessel = EmpVessel[day];
            string defpaycode = DefPayCode;

            DateTime d = RefWeek.AddDays(day);

            ((CuePaycodeDialog)this.ParentForm).Show(EmpID, d, defpaycode);

            //ucHour uc = new ucHour(EmpName, RefWeek.AddDays(day), hour, over, vessel);
            //uc.ShowHour();
            //if (uc.IsAccept())
            //{
            //    EmpToff[day] = "";

            //    EmpHour[day] = uc.GetHour();
            //    EmpOver[day] = uc.GetOver();
            //    EmpVessel[day] = uc.GetVessel();

            //    draw_iday(day);

            //    make_dirty();
            //}
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void cmsHour_Click(object sender, EventArgs e)
        {
            ToolStripItem item = (sender as ToolStripItem);
            if (item != null)
            {
                ContextMenuStrip owner = item.Owner as ContextMenuStrip;
                if (owner != null)
                    //HourClick(Convert.ToInt32(((TextBox)owner.SourceControl).Tag));
                    PaycodeClick(Convert.ToInt32(((TextBox)owner.SourceControl).Tag));
        
            }                       
        }



        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private Color PayCodeColour(string paycode)
        {
            Color c = Color.FromArgb(192, 255, 192);

            if (paycode != DefPayCode) c = Color.LightBlue;

            //switch (paycode)
            //{
            //    case "Salary": c = Color.FromArgb(192, 255, 192); break;
            //    case "Office": c = Color.LightSalmon; break;
            //    case "Shore": c = Color.LightSkyBlue; break;
            //}

            return c;
        }


        private void cbxItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ComboBox cbx = (ComboBox)sender;

            //cbx.BackColor = PayCodeColour(PayCode);
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void pnlM_Click(object sender, EventArgs e)
        {
            NoteClick(0, pnlM);
        }

        private void pnlT_Click(object sender, EventArgs e)
        {
            NoteClick(1, pnlT);
        }

        private void pnlW_Click(object sender, EventArgs e)
        {
            NoteClick(2, pnlW);
        }

        private void pnlTh_Click(object sender, EventArgs e)
        {
            NoteClick(3, pnlTh);
        }

        private void pnlF_Click(object sender, EventArgs e)
        {
            NoteClick(4, pnlF);
        }

        private void pnlSa_Click(object sender, EventArgs e)
        {
            NoteClick(5, pnlSa);
        }

        private void pnlSu_Click(object sender, EventArgs e)
        {
            NoteClick(6, pnlSu);
        }

        private void pnlM2_Click(object sender, EventArgs e)
        {
            NoteClick(7, pnlM2);
        }

        private void pnlT2_Click(object sender, EventArgs e)
        {
            NoteClick(8, pnlT2);
        }

        private void pnlW2_Click(object sender, EventArgs e)
        {
            NoteClick(9, pnlW2);
        }

        private void pnlTh2_Click(object sender, EventArgs e)
        {
            NoteClick(10, pnlTh2);
        }

        private void pnlF2_Click(object sender, EventArgs e)
        {
            NoteClick(11, pnlF2);
        }

        private void pnlSa2_Click(object sender, EventArgs e)
        {
            NoteClick(12, pnlSa2);
        }

        private void pnlSu2_Click(object sender, EventArgs e)
        {
            NoteClick(13, pnlSu2);
        }
    }
   
}

