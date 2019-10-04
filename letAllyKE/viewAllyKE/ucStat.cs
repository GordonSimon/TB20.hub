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
    public partial class ucStat : UserControl
    {
        public bool LoadIt { get; set; }
        public bool ShowIt { get; set; }

        public bool EmpChk { get; set; }
        public string EmpID { get; set; }
        public string EmpName { get; set; }

        public int[] EmpCount;
        public string[] EmpToff;
        public int[] Emp0Shift;
        public string[] Emp0Vessel;
        public string[] Emp0Note;

        public float[] EmpHour;
        public float[] EmpOver;
        public int[] EmpXShift;
        public string[] EmpXVessel;
        public string[] EmpXNote;

                     

        public DayOfWeek[] EmpDow;
        private bool[] EmpLock;

        private bool _dirty { get; set; }
        private bool _delete { get; set; }


        public string StatCode { get; set; }
        public string ShiftVessel { get; set; }
        //public string ShiftCode { get; set; }
        public int ShiftAM { get; set; }

        public DateTime RefWeek { get; set; }

        private int NoteCount = 0;

        private ToolTip _ttHours = null;

        private Dictionary<string, Boatcrew> _boats { get; set; }

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
            clear_shift(0);
            clear_shift(1);

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

            EmpCount = new int[14];
            
            EmpToff = new string[14];
            Emp0Shift = new int[14];
            Emp0Note = new string[14];
            Emp0Vessel = new string[14];

            EmpHour = new float[14];
            EmpOver = new float[14];
            EmpXShift = new int[14];
            EmpXNote = new string[14];
            EmpXVessel = new string[14];


            EmpDow = new DayOfWeek[14];
            EmpLock = new bool[14];


            RefWeek = start_week;
            for (int i = 0; i < 7; i++) EmpDow[i] = help_dow(i + 1);
            for (int i = 7; i < 14; i++) EmpDow[i] = help_dow(i + 1 - 7);

            ShowIt = true;
            _dirty = false;

            chk7F.Checked = false;
            chk7S.Checked = false;

            clear_shift(0);
            clear_shift(1);
            clear_note(0);
            clear_note(1);

            lblVessel.Text = string.Empty;
            ucAMVessel.Hide();

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

            DataTable dtToff = dacToff.GetDS().Tables[0];
            this.cbxLegend.DataSource = dtToff;
            this.cbxLegend.ValueMember = "ToffKey";
            this.cbxLegend.DisplayMember = "ToffDesc";

            // Kludge : have to fix the auto select problem
            cbxLegend.SelectedIndex = -1;
            cbxLegend.SelectedIndexChanged += new EventHandler(cbxLegend_SelectedIndexChanged);

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


        public ucStat(DateTime start_week, string emp_name, string emp_id, bool _db)
        {
            constructor(start_week);

            EmpChk = true;
            EmpID = emp_id;
            EmpName = emp_name;
            LoadIt = _db;

            _boats = null;

            cbxItems.Hide();
            cbxShift.Hide();

            chkEmp.Text = EmpName;
            chkEmp.Checked = EmpChk;

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
            if (StatCode.Equals("12"))
                lblVessel.Text = ShiftVessel;
            else
                lblVessel.Text = string.Empty;

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

        private string show_day(TextBox tbx, string toff, float hours, TextBox tbxC, int count)
        {
            tbx.Text = toff;
            if (toff.Equals("12")) tbx.Text = "!12";
            if (hours != 0)
                tbxC.Text = string.Format("{0:0.0}", hours);

            if (count > 2)
                tbxC.ForeColor = Color.Red;

            //if (count > 1)
            //{
            //    tbx.Text = toff;
            //    tbxC.Text = string.Format("{0:0.0}", hours);
            //    if (count > 2)
            //    {
            //        //tbxC.Text = string.Format("{0}/{1:0.0}", count, hours);
            //        tbxC.Text = string.Format("{0:0.0}", hours);                    
            //        tbxC.ForeColor = Color.Red;
            //    }
            //}
            //else
            //{
            //    //if (hours != 0 && toff.Equals(""))
            //    //    tbx.Text = string.Format("{0:0.0}", hours);
            //    //else if (hours != 0 && !toff.Equals(""))
            //    //    tbx.Text = string.Format("{0}/{1:0.0}", toff, hours);
            //    //else
            //    //    tbx.Text = toff;
            //}

            if (hours == 0 && toff.Equals("12"))            
                tbx.ForeColor = Color.DarkOrange;            
            else if (hours != 0 && toff.Equals("12"))
                tbx.ForeColor = Color.Red;
            else
                tbx.ForeColor = SystemColors.WindowText;
           
            return tbx.Text;
        }


        private bool all_7F(string toff, string vessel, int shift)
        {
            int cnt1 = 0;
            string val1 = toff;

            cnt1 += (tbxMH.Text.Equals(val1) ? 1 : 0);
            cnt1 += (tbxTH.Text.Equals(val1) ? 1 : 0);
            cnt1 += (tbxWH.Text.Equals(val1) ? 1 : 0);
            cnt1 += (tbxThH.Text.Equals(val1) ? 1 : 0);
            cnt1 += (tbxFH.Text.Equals(val1) ? 1 : 0);
            cnt1 += (tbxSaH.Text.Equals(val1) ? 1 : 0);
            cnt1 += (tbxSuH.Text.Equals(val1) ? 1 : 0);


            int cnt2 = 1;
            string val2 = vessel;

            cnt2 += (lblT.Text.Equals(val2) ? 1 : 0);
            cnt2 += (lblW.Text.Equals(val2) ? 1 : 0);
            cnt2 += (lblTh.Text.Equals(val2) ? 1 : 0);
            cnt2 += (lblF.Text.Equals(val2) ? 1 : 0);
            cnt2 += (lblSa.Text.Equals(val2) ? 1 : 0);
            cnt2 += (lblSu.Text.Equals(val2) ? 1 : 0);

            int cnt3 = 1;
            int vali = shift;

            cnt3 += (ucAM2.VAL == vali ? 1 : 0);
            cnt3 += (ucAM3.VAL == vali ? 1 : 0);
            cnt3 += (ucAM4.VAL == vali ? 1 : 0);
            cnt3 += (ucAM5.VAL == vali ? 1 : 0);
            cnt3 += (ucAM6.VAL == vali ? 1 : 0);
            cnt3 += (ucAM7.VAL == vali ? 1 : 0);

            if (cnt1 == 7 && cnt2 == 7 && cnt3 == 7) return true;

            return false;
        }


        private bool all_7S(string toff, string vessel, int shift)
        {
            int cnt1 = 0;
            string val1 = toff;

            cnt1 += (tbxMH2.Text.Equals(val1) ? 1 : 0);
            cnt1 += (tbxTH2.Text.Equals(val1) ? 1 : 0);
            cnt1 += (tbxWH2.Text.Equals(val1) ? 1 : 0);
            cnt1 += (tbxThH2.Text.Equals(val1) ? 1 : 0);
            cnt1 += (tbxFH2.Text.Equals(val1) ? 1 : 0);
            cnt1 += (tbxSaH2.Text.Equals(val1) ? 1 : 0);
            cnt1 += (tbxSuH2.Text.Equals(val1) ? 1 : 0);


            int cnt2 = 0;
            string val2 = vessel;
            
            cnt2 += (lblM2.Text.Equals(val2) ? 1 : 0);
            cnt2 += (lblT2.Text.Equals(val2) ? 1 : 0);
            cnt2 += (lblW2.Text.Equals(val2) ? 1 : 0);
            cnt2 += (lblTh2.Text.Equals(val2) ? 1 : 0);
            cnt2 += (lblF2.Text.Equals(val2) ? 1 : 0);
            cnt2 += (lblSa2.Text.Equals(val2) ? 1 : 0);
            cnt2 += (lblSu2.Text.Equals(val2) ? 1 : 0);

            int cnt3 = 0;
            int vali = shift;

            cnt3 += (ucAM8.VAL == vali ? 1 : 0);
            cnt3 += (ucAM9.VAL == vali ? 1 : 0);
            cnt3 += (ucAM10.VAL == vali ? 1 : 0);
            cnt3 += (ucAM11.VAL == vali ? 1 : 0);
            cnt3 += (ucAM12.VAL == vali ? 1 : 0);
            cnt3 += (ucAM13.VAL == vali ? 1 : 0);
            cnt3 += (ucAM14.VAL == vali ? 1 : 0);
            

            if (cnt1 == 7 && cnt2 == 7 && cnt3 == 7) return true;

            return false;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public void RefreshLegend(string toff, string vessel)
        {
            cbxLegend.SelectedValue = toff;

            StatCode = toff;
            ShiftVessel = vessel;

            lblVessel
                .Hide();
            lblVessel.Text = string.Empty;
            if (vessel != null && vessel.Length > 0)
            {
                lblVessel.Text = vessel;
                lblVessel.Show();
            }
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void draw_iday(int iday)
        {
            int count = (int)EmpCount[iday];

            float hours = EmpHour[iday];
            float over = EmpOver[iday]; // not needed here
            string toff = EmpToff[iday];
            int shift = Emp0Shift[iday];
            string vessel = Emp0Vessel[iday];
            string note = Emp0Note[iday];
            
            DayOfWeek dow = EmpDow[iday];

            if (toff.Equals("12")) shift = -1;
            //if (!EmpXShift[iday].Equals(DBNull.Value)) { shift = EmpXShift[iday]; vessel = EmpXVessel[iday]; }
            if (EmpXShift[iday] != 0) { shift = EmpXShift[iday]; vessel = EmpXVessel[iday]; }

            if (iday < 7)
            {
                switch (dow)
                {
                    case DayOfWeek.Monday:
                        show_day(tbxMH, toff, hours, tbxMC, count);
                        tb_apply_shift(0, vessel, shift);
                        show_note(pnlM, note);
                        break;

                    case DayOfWeek.Tuesday:
                        show_day(tbxTH, toff, hours, tbxTC, count);
                        tb_apply_shift(1, vessel, shift);
                        show_note(pnlT, note);
                        break;

                    case DayOfWeek.Wednesday:
                        show_day(tbxWH, toff, hours, tbxWC, count);
                        tb_apply_shift(2, vessel, shift);
                        show_note(pnlW, note);
                        break;

                    case DayOfWeek.Thursday:
                        show_day(tbxThH, toff, hours, tbxThC, count);
                        tb_apply_shift(3, vessel, shift);
                        show_note(pnlTh, note);
                        break;

                    case DayOfWeek.Friday:
                        show_day(tbxFH, toff, hours, tbxFC, count);
                        tb_apply_shift(4, vessel, shift);
                        show_note(pnlF, note);
                        break;

                    case DayOfWeek.Saturday:
                        show_day(tbxSaH, toff, hours, tbxSaC, count);
                        tb_apply_shift(5, vessel, shift);
                        show_note(pnlSa, note);
                        break;

                    case DayOfWeek.Sunday:
                        show_day(tbxSuH, toff, hours, tbxSuC, count);
                        tb_apply_shift(6, vessel, shift);
                        show_note(pnlSu, note);
                        break;
                }

                if (all_7F(toff, vessel, shift))
                {
                    mark_week(0, toff);
                    mark_shift(0, vessel, shift, toff);
                }

            }
            else
            {
                switch (dow)
                {
                    case DayOfWeek.Monday:
                        show_day(tbxMH2, toff, hours, tbxMC2, count);
                        tb_apply_shift(7, vessel, shift);
                        show_note(pnlM2, note);
                        break;

                    case DayOfWeek.Tuesday:
                        show_day(tbxTH2, toff, hours, tbxTC2, count);
                        tb_apply_shift(8, vessel, shift);
                        show_note(pnlT2, note);
                        break;

                    case DayOfWeek.Wednesday:
                        show_day(tbxWH2, toff, hours, tbxWC2, count);
                        tb_apply_shift(9, vessel, shift);
                        show_note(pnlW2, note);
                        break;

                    case DayOfWeek.Thursday:
                        show_day(tbxThH2, toff, hours, tbxThC2, count);
                        tb_apply_shift(10, vessel, shift);
                        show_note(pnlTh2, note);
                        break;

                    case DayOfWeek.Friday:
                        show_day(tbxFH2, toff, hours, tbxFC2, count);
                        tb_apply_shift(11, vessel, shift);
                        show_note(pnlF2, note);
                        break;

                    case DayOfWeek.Saturday:
                        show_day(tbxSaH2, toff, hours, tbxSaC2, count);
                        tb_apply_shift(12, vessel, shift);
                        show_note(pnlSa2, note);
                        break;

                    case DayOfWeek.Sunday:
                        show_day(tbxSuH2, toff, hours, tbxSuC2, count);
                        tb_apply_shift(13, vessel, shift);
                        show_note(pnlSu2, note);
                        break;
                }


                if (all_7S(toff, vessel, shift))
                {
                    mark_week(1, toff);
                    mark_shift(1, vessel, shift, toff);
                }

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

        public void RefreshDay(DateTime day, string toff, float hours, float over, string vessel, int shift, string note)
        {
            //tbxRef.Text = RefWeek.ToShortDateString();

            int iday = ((TimeSpan)(day - RefWeek.Date.Date)).Days;
            if (iday < 0 || iday > 13) return;

            //if (EmpID.Equals("AR1401") && iday == 0) MessageBox.Show("Ryan");

            EmpCount[iday] += 1;
            EmpHour[iday] += hours;
            EmpOver[iday] += over;
            //if (hours != 0 || over != 0) EmpLock[iday] = true;

            if (shift == 0)
            {
                EmpToff[iday] = toff;
                Emp0Vessel[iday] = vessel;
                
                if (note != null && note.Length != 0) Emp0Note[iday] = note;
            }
            else
            {
                if ((EmpXVessel[iday] == null) && vessel != null) EmpXVessel[iday] = "";
                if ((EmpXNote[iday] == null) && note != null) EmpXNote[iday] = "";
                EmpXShift[iday] = shift;
                if (vessel != null && vessel.Length != 0)
                    EmpXVessel[iday] += (((string)EmpXVessel[iday]).Length > 0 ? "/" : "") + vessel;                    
                if (note != null && note.Length != 0)
                    EmpXNote[iday] += (((string)EmpXNote[iday]).Length > 0 ? "\n" : "") + note;                
            }

            if (note != null && note.Length > 0)
            {
                NoteCount += 1;
                nudComment.Value += 1;
            }


            if (EmpDow[iday] != day.DayOfWeek)
                //EmpDow[iday] = day.DayOfWeek;
                MessageBox.Show(String.Format("Day of Week Problem ! iday[{0}], EmpDow[{1}] day[{2}]", iday,
                    EmpDow[iday], day.DayOfWeek), "ucStat.RefreshDay");

            draw_iday(iday);
        }


        //public void RefreshDay(DateTime day, string toff, float hours, float over, string vessel, int shift, string note)
        //{
        //    //tbxRef.Text = RefWeek.ToShortDateString();

        //    int iday = ((TimeSpan)(day - RefWeek.Date.Date)).Days;
        //    if (iday < 0 || iday > 13) return;

        //    EmpCount[iday] += 1;
        //    if (shift == 0)  EmpToff[iday] = toff;
        //    EmpHour[iday] += hours;
        //    EmpOver[iday] += over;
        //    if (shift != 0)  EmpShift[iday] = shift;
        //    EmpVessel[iday] += vessel;
        //    if (note != null && note.Length != 0)   EmpNote[iday] += note + "\n";
        //    if (hours != 0 || over != 0) EmpLock[iday] = true;

        //    if (note != null && note.Length > 0)
        //    {
        //        NoteCount += 1;
        //        nudComment.Value += 1;
        //    }


        //    if (EmpDow[iday] != day.DayOfWeek)
        //        //EmpDow[iday] = day.DayOfWeek;
        //        MessageBox.Show(String.Format("Day of Week Problem ! iday[{0}], EmpDow[{1}] day[{2}]", iday,
        //            EmpDow[iday], day.DayOfWeek));

        //    draw_iday(iday);
        //}


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

                EmpHour[0] = EmpHour[1] = EmpHour[2] =
                    EmpHour[3] = EmpHour[4] = EmpHour[5] = EmpHour[6] = 0;
                EmpOver[0] = EmpOver[1] = EmpOver[2] =
                    EmpOver[3] = EmpOver[4] = EmpOver[5] = EmpOver[6] = 0;
                EmpToff[0] = EmpToff[1] = EmpToff[2] =
                    EmpToff[3] = EmpToff[4] = EmpToff[5] = EmpToff[6] = "";

                Emp0Vessel[0] = Emp0Vessel[1] = Emp0Vessel[2] =
                    Emp0Vessel[3] = Emp0Vessel[4] = Emp0Vessel[5] = Emp0Vessel[6] = "";

                EmpXVessel[0] = EmpXVessel[1] = EmpXVessel[2] =
                    EmpXVessel[3] = EmpXVessel[4] = EmpXVessel[5] = EmpXVessel[6] = "";

                Emp0Shift[0] = Emp0Shift[1] = Emp0Shift[2] =
                    Emp0Shift[3] = Emp0Shift[4] = Emp0Shift[5] = Emp0Shift[6] = 0;

                EmpXShift[0] = EmpXShift[1] = EmpXShift[2] =
                    EmpXShift[3] = EmpXShift[4] = EmpXShift[5] = EmpXShift[6] = 0;

                Emp0Note[0] = Emp0Note[1] = Emp0Note[2] =
                    Emp0Note[3] = Emp0Note[4] = Emp0Note[5] = Emp0Note[6] = "";

                EmpXNote[0] = EmpXNote[1] = EmpXNote[2] =
                    EmpXNote[3] = EmpXNote[4] = EmpXNote[5] = EmpXNote[6] = "";


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

                EmpHour[7] = EmpHour[8] = EmpHour[9] =
                    EmpHour[10] = EmpHour[11] = EmpHour[12] = EmpHour[13] = 0;
                EmpOver[7] = EmpOver[8] = EmpOver[9] =
                    EmpOver[10] = EmpOver[11] = EmpOver[12] = EmpOver[13] = 0;
                EmpToff[7] = EmpToff[8] = EmpToff[9] =
                    EmpToff[10] = EmpToff[11] = EmpToff[12] = EmpToff[13] = "";

                Emp0Vessel[7] = Emp0Vessel[8] = Emp0Vessel[9] =
                    Emp0Vessel[10] = Emp0Vessel[11] = Emp0Vessel[12] = Emp0Vessel[13] = "";

                EmpXVessel[7] = EmpXVessel[8] = EmpXVessel[9] =
                    EmpXVessel[10] = EmpXVessel[11] = EmpXVessel[12] = EmpXVessel[13] = "";

                Emp0Shift[7] = Emp0Shift[8] = Emp0Shift[9] =
                    Emp0Shift[10] = Emp0Shift[11] = Emp0Shift[12] = Emp0Shift[13] = 0;

                EmpXShift[7] = EmpXShift[8] = EmpXShift[9] =
                    EmpXShift[10] = EmpXShift[11] = EmpXShift[12] = EmpXShift[13] = 0;

                Emp0Note[7] = Emp0Note[8] = Emp0Note[9] =
                    Emp0Note[10] = Emp0Note[11] = Emp0Note[12] = Emp0Note[13] = "";


                EmpXNote[7] = EmpXNote[8] = EmpXNote[9] =
                    EmpXNote[10] = EmpXNote[11] = EmpXNote[12] = EmpXNote[13] = "";


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
                EmpOver[0] = EmpOver[1] = EmpOver[2] =
                    EmpOver[3] = EmpOver[4] = EmpOver[5] = EmpOver[6] = 0;
                EmpToff[0] = EmpToff[1] = EmpToff[2] =
                    EmpToff[3] = EmpToff[4] = EmpToff[5] = EmpToff[6] = val;

                //chk7F.Checked = true;
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

                //if (val.Equals("12"))
                if (val != null && val.Equals("12"))
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
                EmpOver[7] = EmpOver[8] = EmpOver[9] =
                    EmpOver[10] = EmpOver[11] = EmpOver[12] = EmpOver[13] = 0;
                EmpToff[7] = EmpToff[8] = EmpToff[9] =
                    EmpToff[10] = EmpToff[11] = EmpToff[12] = EmpToff[13] = val;

                //chk7S.Checked = true;
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
            clear_shift(0);
            clear_shift(1);
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

        private void tb_apply_boat(int idx, Boat b)
        {
            int shft = b.dayshft[idx];
            string code = b.daycode[idx];

            switch (idx)
            {
                case 0: lblM.Text = code; ucAM1.On(shft); break;
                case 1: lblT.Text = code; ucAM2.On(shft); break;
                case 2: lblW.Text = code; ucAM3.On(shft); break;
                case 3: lblTh.Text = code; ucAM4.On(shft); break;
                case 4: lblF.Text = code; ucAM5.On(shft); break;
                case 5: lblSa.Text = code; ucAM6.On(shft); break;
                case 6: lblSu.Text = code; ucAM7.On(shft); break;

                case 7: lblM2.Text = code; ucAM8.On(shft); break;
                case 8: lblT2.Text = code; ucAM9.On(shft); break;
                case 9: lblW2.Text = code; ucAM10.On(shft); break;
                case 10: lblTh2.Text = code; ucAM11.On(shft); break;
                case 11: lblF2.Text = code; ucAM12.On(shft); break;
                case 12: lblSa2.Text = code; ucAM13.On(shft); break;
                case 13: lblSu2.Text = code; ucAM14.On(shft); break;
            }
        }


        private void tb_apply_shift(int idx, string v, int s)
        {
            Color colour = (s < 0 ? Color.DarkOrange : SystemColors.ControlText);

            switch (idx)
            {
                case 0: lblM.Text = v; ucAM1.On(s); lblM.ForeColor = colour; break;
                case 1: lblT.Text = v; ucAM2.On(s); lblT.ForeColor = colour; break;
                case 2: lblW.Text = v; ucAM3.On(s); lblW.ForeColor = colour; break;
                case 3: lblTh.Text = v; ucAM4.On(s); lblTh.ForeColor = colour; break;
                case 4: lblF.Text = v; ucAM5.On(s); lblF.ForeColor = colour; break;
                case 5: lblSa.Text = v; ucAM6.On(s); lblSa.ForeColor = colour; break;
                case 6: lblSu.Text = v; ucAM7.On(s); lblSu.ForeColor = colour; break;

                case 7: lblM2.Text = v; ucAM8.On(s); lblM2.ForeColor = colour; break;
                case 8: lblT2.Text = v; ucAM9.On(s); lblT2.ForeColor = colour; break;
                case 9: lblW2.Text = v; ucAM10.On(s); lblW2.ForeColor = colour; break;
                case 10: lblTh2.Text = v; ucAM11.On(s); lblTh2.ForeColor = colour; break;
                case 11: lblF2.Text = v; ucAM12.On(s); lblF2.ForeColor = colour; break;
                case 12: lblSa2.Text = v; ucAM13.On(s); lblSa2.ForeColor = colour; break;
                case 13: lblSu2.Text = v; ucAM14.On(s); lblSu2.ForeColor = colour; break;
            }
        }


        private void mark_shift(int week, string vessel, int am, string toff)
        {
            if (week == 0)
            {
                lblM.Text = vessel;
                lblT.Text = vessel;
                lblW.Text = vessel;
                lblTh.Text = vessel;
                lblF.Text = vessel;
                lblSa.Text = vessel;
                lblSu.Text = vessel;

                ucAM1.On(am);
                ucAM2.On(am);
                ucAM3.On(am);
                ucAM4.On(am);
                ucAM5.On(am);
                ucAM6.On(am);
                ucAM7.On(am);

                if (toff != null && toff.Equals("12"))
                {
                    Emp0Vessel[0] = Emp0Vessel[1] = Emp0Vessel[2] =
                        Emp0Vessel[3] = Emp0Vessel[4] = Emp0Vessel[5] = Emp0Vessel[6] = vessel;
                    Emp0Shift[0] = Emp0Shift[1] = Emp0Shift[2] =
                        Emp0Shift[3] = Emp0Shift[4] = Emp0Shift[5] = Emp0Shift[6] = 0;
                }
                else
                {
                    Emp0Vessel[0] = Emp0Vessel[1] = Emp0Vessel[2] =
                        Emp0Vessel[3] = Emp0Vessel[4] = Emp0Vessel[5] = Emp0Vessel[6] = "";
                    Emp0Shift[0] = Emp0Shift[1] = Emp0Shift[2] =
                        Emp0Shift[3] = Emp0Shift[4] = Emp0Shift[5] = Emp0Shift[6] = 0;
                }

            }
            else
            {
                lblM2.Text = vessel;
                lblT2.Text = vessel;
                lblW2.Text = vessel;
                lblTh2.Text = vessel;
                lblF2.Text = vessel;
                lblSa2.Text = vessel;
                lblSu2.Text = vessel;

                ucAM8.On(am);
                ucAM9.On(am);
                ucAM10.On(am);
                ucAM11.On(am);
                ucAM12.On(am);
                ucAM13.On(am);
                ucAM14.On(am);


                if (toff != null && toff.Equals("12"))
                {
                    Emp0Vessel[7] = Emp0Vessel[8] = Emp0Vessel[9] =
                        Emp0Vessel[10] = Emp0Vessel[11] = Emp0Vessel[12] = Emp0Vessel[13] = vessel;
                    Emp0Shift[7] = Emp0Shift[8] = Emp0Shift[9] =
                        Emp0Shift[10] = Emp0Shift[11] = Emp0Shift[12] = Emp0Shift[13] = 0;
                }
                else
                {
                    Emp0Vessel[7] = Emp0Vessel[8] = Emp0Vessel[9] =
                        Emp0Vessel[10] = Emp0Vessel[11] = Emp0Vessel[12] = Emp0Vessel[13] = "";
                    Emp0Shift[7] = Emp0Shift[8] = Emp0Shift[9] =
                        Emp0Shift[10] = Emp0Shift[11] = Emp0Shift[12] = Emp0Shift[13] = 0;
                }
            }
        }


        private void clear_shift(int week)
        {
            //lblVessel.Text = "";
            //ucAMVessel.Hide();

            if (week == 0)
            {
                
                lblM.Text = "";
                lblT.Text = "";
                lblW.Text = "";
                lblTh.Text = "";
                lblF.Text = "";
                lblSa.Text = "";
                lblSu.Text = "";
                
                ucAM1.Hide();
                ucAM2.Hide();
                ucAM3.Hide();
                ucAM4.Hide();
                ucAM5.Hide();
                ucAM6.Hide();
                ucAM7.Hide();
            }
            else
            {
                lblM2.Text = "";
                lblT2.Text = "";
                lblW2.Text = "";
                lblTh2.Text = "";
                lblF2.Text = "";
                lblSa2.Text = "";
                lblSu2.Text = "";

                ucAM8.Hide();
                ucAM9.Hide();
                ucAM10.Hide();
                ucAM11.Hide();
                ucAM12.Hide();
                ucAM13.Hide();
                ucAM14.Hide();

            }
        }


        //public void RefreshShift(string val, int am)
        //public void RefreshShift(Boatcrew boat, int shift)
        public void RefreshShift(Boat boat)
        {
            int shift = 1;
            //ShiftCode = val;
            //ShiftAM = am;

            if (!chkEmp.Checked) return;
            if (!chk7F.Checked) return;

            //mark_shift(0, ShiftCode, ShiftAM);
            mark_shift(0, "12", shift, ShiftVessel);
            //for (int i = 0; i < 14; i++) tb_apply_shift(i, boat);
            for (int i = 0; i < 14; i++) tb_apply_boat(i, boat);
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private Boatcrew boat_make(string boat, string name, string shift, Dictionary<string, Boatcrew> bc)
        {
            Boatcrew b = null;

            if (bc.ContainsKey(boat))
                b = _boats[boat];
            else
            {
                b = new Boatcrew();
                b.boatname = name;
                b.shift = shift;
                bc.Add(boat, b);
            }

            return b;
        }


        private Boatcrew boat_update(int week, string boat, int am, Dictionary<string, Boatcrew> bc, int idx)
        {
            Boatcrew b = null;

            int s = 0;

            if (bc.ContainsKey(boat))
            {
                b = _boats[boat];

                if (idx >= 0)
                {
                    b.boat[s, week * 7 + idx] = boat; b.am[s, week * 7 + idx] = am;
                }
                else
                    for (int i = 0; i < 7; i++) { b.boat[s, week * 7 + i] = boat; b.am[s, week * 7 + i] = am; }

            }
            return b;
        }


        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        private bool applyB_code(TextBox tbx, int day, string val, string vessel)
        {
            if (EmpLock[day]) return true;

            if (day < 7)
            {
                if (chk7F.Checked) return false;
            }
            else
            {
                if (chk7S.Checked) return false;
            }

            if (tbx.Text.Equals(""))
                tbx.Text = val;
            else
                tbx.Text = "";

            EmpHour[day] = 0;
            EmpOver[day] = 0;
            EmpToff[day] = tbx.Text;
            Emp0Shift[day] = 0;
            Emp0Vessel[day] = vessel;

            if (val == null || !val.Equals("12"))
                Emp0Vessel[day] = null;

            draw_iday(day);

            return false;
        }


        private void apply0_code(TextBox tbx, string val)
        {
            if (chk7F.Checked) return;

            if (tbx.Text.Equals(""))
                tbx.Text = val;
            else
                tbx.Text = "";
        }


        private void apply1_code(TextBox tbx, string val)
        {
            if (chk7S.Checked) return;

            if (tbx.Text.Equals(""))
                tbx.Text = val;
            else
                tbx.Text = "";
        }


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

        private void tbxMH_Click(object sender, EventArgs e)
        {
            if (_boats == null)
            {
                //apply0_code(tbxMH, StatCode);
                bool lck = applyB_code(tbxMH, 0, StatCode, ShiftVessel);
                if (! lck)  make_dirty();
            }
            else
            {
                apply0_code(tbxMH, ShiftVessel);
                //TextBox tbx = (TextBox)(this.ParentForm.Controls.Find("tbxShift", true)[0]);
                //tbx.Tag = boat_update(0, ShiftCode, ShiftAM, _boats, 0);  // needs to be first
                //tbx.Text = "0";
            }
        }

        private void tbxTH_Click(object sender, EventArgs e)
        {
            if (_boats == null)
            {
                //apply0_code(tbxTH, StatCode);
                bool lck = applyB_code(tbxTH, 1, StatCode, ShiftVessel);
                if (! lck)  make_dirty();
            }
            else
            {
                apply0_code(tbxTH, ShiftVessel);
                //TextBox tbx = (TextBox)(this.ParentForm.Controls.Find("tbxShift", true)[0]);
                //tbx.Tag = boat_update(0, ShiftCode, ShiftAM, _boats, 1);  // needs to be first
                //tbx.Text = "1";
            }
        }

        private void tbxWH_Click(object sender, EventArgs e)
        {
            if (_boats == null)
            {
                //apply0_code(tbxWH, StatCode);
                bool lck = applyB_code(tbxWH, 2, StatCode, ShiftVessel);
                if (! lck)  make_dirty();
            }
            else
            {
                apply0_code(tbxWH, ShiftVessel);
                //TextBox tbx = (TextBox)(this.ParentForm.Controls.Find("tbxShift", true)[0]);
                //tbx.Tag = boat_update(0, ShiftCode, ShiftAM, _boats, 2);  // needs to be first
                //tbx.Text = "2";
            }
        }

        private void tbxThH_Click(object sender, EventArgs e)
        {
            if (_boats == null)
            {
                //apply0_code(tbxThH, StatCode);
                bool lck = applyB_code(tbxThH, 3, StatCode, ShiftVessel);
                if (! lck)  make_dirty();
            }
            else
            {
                apply0_code(tbxThH, ShiftVessel);
                //TextBox tbx = (TextBox)(this.ParentForm.Controls.Find("tbxShift", true)[0]);
                //tbx.Tag = boat_update(0, ShiftCode, ShiftAM, _boats, 3);  // needs to be first
                //tbx.Text = "3";
            }
        }

        private void tbxFH_Click(object sender, EventArgs e)
        {
            if (_boats == null)
            {
                //apply0_code(tbxFH, StatCode);
                bool lck = applyB_code(tbxFH, 4, StatCode, ShiftVessel);
                if (! lck)  make_dirty();
            }
            else
            {
                apply0_code(tbxFH, ShiftVessel);
                //TextBox tbx = (TextBox)(this.ParentForm.Controls.Find("tbxShift", true)[0]);
                //tbx.Tag = boat_update(0, ShiftCode, ShiftAM, _boats, 4);  // needs to be first
                //tbx.Text = "4";
            }
        }

        private void tbxSaH_Click(object sender, EventArgs e)
        {
            if (_boats == null)
            {
                //apply0_code(tbxSaH, StatCode);
                bool lck = applyB_code(tbxSaH, 5, StatCode, ShiftVessel);
                if (!lck) make_dirty();
            }
            else
            {
                apply0_code(tbxSaH, ShiftVessel);
                //TextBox tbx = (TextBox)(this.ParentForm.Controls.Find("tbxShift", true)[0]);
                //tbx.Tag = boat_update(0, ShiftCode, ShiftAM, _boats, 5);  // needs to be first
                //tbx.Text = "5";
            }
        }

        private void tbxSuH_Click(object sender, EventArgs e)
        {
            if (_boats == null)
            {
                //apply0_code(tbxSuH, StatCode);
                bool lck = applyB_code(tbxSuH, 6, StatCode, ShiftVessel);
                if (!lck)  make_dirty();
            }
            else
            {
                apply0_code(tbxSuH, ShiftVessel);
                //TextBox tbx = (TextBox)(this.ParentForm.Controls.Find("tbxShift", true)[0]);
                //tbx.Tag = boat_update(0, ShiftCode, ShiftAM, _boats, 6);  // needs to be first
                //tbx.Text = "6";
            }
        }


        private void tbxMH2_Click(object sender, EventArgs e)
        {
            if (_boats == null)
            {
                //apply1_code(tbxMH2, StatCode);
                bool lck = applyB_code(tbxMH2, 7, StatCode, ShiftVessel);
                if (!lck) make_dirty();
            }
            else
            {
                apply1_code(tbxMH2, ShiftVessel);
                //TextBox tbx = (TextBox)(this.ParentForm.Controls.Find("tbxShift", true)[0]);
                //tbx.Tag = boat_update(1, ShiftCode, ShiftAM, _boats, 0);  // needs to be first
                //tbx.Text = "7";
            }
        }

        private void tbxTH2_Click(object sender, EventArgs e)
        {
            if (_boats == null)
            {
                //apply1_code(tbxTH2, StatCode);
                bool lck = applyB_code(tbxTH2, 8, StatCode, ShiftVessel);
                if (!lck) make_dirty();
            }
            else
            {
                apply1_code(tbxTH2, ShiftVessel);
                //TextBox tbx = (TextBox)(this.ParentForm.Controls.Find("tbxShift", true)[0]);
                //tbx.Tag = boat_update(1, ShiftCode, ShiftAM, _boats, 1);  // needs to be first
                //tbx.Text = "8";
            }
        }

        private void tbxWH2_Click(object sender, EventArgs e)
        {
            if (_boats == null)
            {
                //apply1_code(tbxWH2, StatCode);
                bool lck = applyB_code(tbxWH2, 9, StatCode, ShiftVessel);
                if (!lck) make_dirty();
            }
            else
            {
                apply1_code(tbxWH2, ShiftVessel);
                //TextBox tbx = (TextBox)(this.ParentForm.Controls.Find("tbxShift", true)[0]);
                //tbx.Tag = boat_update(1, ShiftCode, ShiftAM, _boats, 2);  // needs to be first
                //tbx.Text = "9";
            }
        }

        private void tbxThH2_Click(object sender, EventArgs e)
        {
            if (_boats == null)
            {
                //apply1_code(tbxThH2, StatCode);
                bool lck = applyB_code(tbxThH2, 10, StatCode, ShiftVessel);
                if (!lck) make_dirty();
            }
            else
            {
                apply1_code(tbxThH2, ShiftVessel);
                //TextBox tbx = (TextBox)(this.ParentForm.Controls.Find("tbxShift", true)[0]);
                //tbx.Tag = boat_update(1, ShiftCode, ShiftAM, _boats, 3);  // needs to be first
                //tbx.Text = "10";
            }
        }

        private void tbxFH2_Click(object sender, EventArgs e)
        {
            if (_boats == null)
            {
                //apply1_code(tbxFH2, StatCode);
                bool lck = applyB_code(tbxFH2, 11, StatCode, ShiftVessel);
                if (!lck) make_dirty();
            }
            else
            {
                apply1_code(tbxFH2, ShiftVessel);
                //TextBox tbx = (TextBox)(this.ParentForm.Controls.Find("tbxShift", true)[0]);
                //tbx.Tag = boat_update(1, ShiftCode, ShiftAM, _boats, 4);  // needs to be first
                //tbx.Text = "11";
            }
        }

        private void tbxSaH2_Click(object sender, EventArgs e)
        {
            if (_boats == null)
            {
                //apply1_code(tbxSaH2, StatCode);
                bool lck = applyB_code(tbxSaH2, 12, StatCode, ShiftVessel);
                if (!lck) make_dirty();
            }
            else
            {
                apply1_code(tbxSaH2, ShiftVessel);
                //TextBox tbx = (TextBox)(this.ParentForm.Controls.Find("tbxShift", true)[0]);
                //tbx.Tag = boat_update(1, ShiftCode, ShiftAM, _boats, 5);  // needs to be first
                //tbx.Text = "12";
            }
        }

        private void tbxSuH2_Click(object sender, EventArgs e)
        {
            if (_boats == null)
            {
                //apply1_code(tbxSuH2, StatCode);
                bool lck = applyB_code(tbxSuH2, 13, StatCode, ShiftVessel);
                if (!lck) make_dirty();
            }
            else
            {
                apply1_code(tbxSuH2, ShiftVessel);
                //TextBox tbx = (TextBox)(this.ParentForm.Controls.Find("tbxShift", true)[0]);
                //tbx.Tag = boat_update(1, ShiftCode, ShiftAM, _boats, 6);  // needs to be first
                //tbx.Text = "13";
            }
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
                mark_shift(0, ShiftVessel, ShiftAM, StatCode);
                mark_shift(1, ShiftVessel, ShiftAM, StatCode);

                make_delete(true);
            }
            else
            {
                clear_week(0, true);
                clear_week(1, true);
                clear_shift(0);
                clear_shift(1);

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
                mark_shift(0, ShiftVessel, ShiftAM, StatCode);
                //clear_shift(0);
            }
            else
            {
                clear_week(0, false);
                clear_shift(0);
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
                clear_shift(1);
            }
            else
            {
                clear_week(1, false);
                clear_shift(1);
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


        private void cbxItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            Item its = (Item)(cbxShift.SelectedItem);
            Item itv = (Item)(cbxItems.SelectedItem);

            //ShiftCode = (itv == null ? "" : itv.Tag.ToString()) + (its == null ? "" : its.Tag.ToString());
            ShiftCode = (itv == null ? "" : itv.Tag.ToString());
            ShiftAM = (its == null ? 0 : (Int32)(its.Tag));

            string shift = (ShiftAM == 0 ? "" : its.Text);

            boat_make(ShiftCode, itv.Text, shift, _boats);

            if (chk7F.Checked)
                //for (int i = 0; i < 7; i++) _boat.boat[i] = ShiftCode;
                 boat_update(0, ShiftCode, ShiftAM, _boats, -1);

            if (chk7S.Checked)
                //for (int i = 7; i < 14; i++) _boat.boat[i] = ShiftCode;
                boat_update(1, ShiftCode, ShiftAM, _boats, -1);
            */
        }


        private void cbxShift_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            Item its = (Item)(cbxShift.SelectedItem);
            Item itv = (Item)(cbxItems.SelectedItem);

            //ShiftCode = (itv == null ? "" : itv.Tag.ToString()) + (its == null ? "" : its.Tag.ToString());
            ShiftCode = (itv == null ? "" : itv.Tag.ToString());
            ShiftAM = (its == null ? 0 : (Int32)(its.Tag));

            string name = (itv == null ? "" : itv.Text);

            boat_make(ShiftCode, name, its.Text, _boats);

            if (chk7F.Checked)
                boat_update(0, ShiftCode, ShiftAM, _boats, -1);
                //for (int i = 0; i < 7; i++)
                //{
                //    if (_boat.am[i] != 0)
                //    {
                //        if (_boat.boat[i].Equals(ShiftCode))
                //            _boat.am[i] = (_boat.am[i] == ShiftAM ? ShiftAM : 3);
                //        else
                //            _boat.am[i] = ShiftAM;
                //    }
                //    else
                //        _boat.am[i] = ShiftAM;
                //}

            if (chk7S.Checked)
                boat_update(1, ShiftCode, ShiftAM, _boats, -1);
                //for (int i = 7; i < 14; i++)
                //{
                //    if (_boat.am[i] != 0)
                //    {
                //        if (_boat.boat[i].Equals(ShiftCode))
                //            _boat.am[i] = (_boat.am[i] == ShiftAM ? ShiftAM : 3);
                //        else
                //            _boat.am[i] = ShiftAM;
                //    }
                //    else
                //        _boat.am[i] = ShiftAM;
                //}


            chkEmp.Focus();
            */
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void NoteClick(int day, Panel pnl)
        {
            ucNote uc = new ucNote(EmpName, Emp0Note[day], RefWeek.AddDays(day));
            uc.EditNote();
            if ( uc.IsAccept() )
            {
                Emp0Note[day] = uc.GetMemo();

                if (uc.IsDelete())
                    pnl.Visible = false;
                else
                    pnl.Visible = true;

                make_dirty();
            }
        }


        private void HourClick(int day, Panel pnl)
        {
            float hour = EmpHour[day];
            float over = EmpOver[day];
            string vessel = Emp0Vessel[day];

            ucHour uc = new ucHour(EmpName, RefWeek.AddDays(day), hour, over, vessel);
            uc.ShowHour();
            if ( uc.IsAccept() )
            {
                //EmpToff[day] = null;
                EmpToff[day] = "";

                EmpHour[day] = uc.GetHour();
                EmpOver[day] = uc.GetOver();
                Emp0Vessel[day] = uc.GetVessel();

                //EmpNote[day] = uc.GetMemo();
                //pnl.Visible = true;

                draw_iday(day);

                make_dirty();
            }
        }


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

        private void cmsNoteW_Click(object sender, EventArgs e)
        {
            NoteClick(2, pnlW);
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


        private void cmsNoteM2_Click(object sender, EventArgs e)
        {
            NoteClick(7, pnlM2);
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


        private void cmsNoteSa2_Click(object sender, EventArgs e)
        {
            NoteClick(12, pnlSa2);
        }


        private void cmsNoteSu2_Click(object sender, EventArgs e)
        {
            NoteClick(13, pnlSu2);
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void cmsHourM_Click(object sender, EventArgs e)
        {
            HourClick(0, pnlM);
        }

        private void cmsHourT_Click(object sender, EventArgs e)
        {
            HourClick(1, pnlM);
        }

        private void cmsHourW_Click(object sender, EventArgs e)
        {
            HourClick(2, pnlM);
        }

        private void cmsHourTh_Click(object sender, EventArgs e)
        {
            HourClick(3, pnlM);
        }

        private void cmsHourF_Click(object sender, EventArgs e)
        {
            HourClick(4, pnlM);
        }

        private void cmsHourSa_Click(object sender, EventArgs e)
        {
            HourClick(5, pnlM);
        }

        private void cmsHourSu_Click(object sender, EventArgs e)
        {
            HourClick(6, pnlM);
        }

        private void cmdHourM2_Click(object sender, EventArgs e)
        {
            HourClick(7, pnlM2);
        }

        private void cmsHourT2_Click(object sender, EventArgs e)
        {
            HourClick(8, pnlT2);
        }

        private void cmsHourW2_Click(object sender, EventArgs e)
        {
            HourClick(9, pnlW2);
        }

        private void cmsHourTh2_Click(object sender, EventArgs e)
        {
            HourClick(10, pnlTh2);
        }

        private void cmsHourF2_Click(object sender, EventArgs e)
        {
            HourClick(11, pnlF2);
        }

        private void cmsHourSa2_Click(object sender, EventArgs e)
        {
            HourClick(12, pnlSa2);
        }

        private void cmsHourSu3_Click(object sender, EventArgs e)
        {
            HourClick(13, pnlSu2);
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
            NoteClick(7, pnlM);
        }

        private void pnlT2_Click(object sender, EventArgs e)
        {
            NoteClick(8, pnlT);
        }

        private void pnlW2_Click(object sender, EventArgs e)
        {
            NoteClick(9, pnlW);
        }

        private void pnlTh2_Click(object sender, EventArgs e)
        {
            NoteClick(10, pnlTh);
        }

        private void pnlF2_Click(object sender, EventArgs e)
        {
            NoteClick(11, pnlF);
        }

        private void pnlSa2_Click(object sender, EventArgs e)
        {
            NoteClick(12, pnlSa);
        }

        private void pnlSu2_Click(object sender, EventArgs e)
        {
            NoteClick(13, pnlSu);
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private int day_index(string ctl_text)
        {
            switch (ctl_text)
            {
                case "tbxMH": return 0; //break;
                default: return 1; //break;
            }

            //return 0;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void tbxMC_MouseHover(object sender, EventArgs e)
        {
            TextBox tbx = (TextBox)sender;

            if (_ttHours == null) _ttHours = new ToolTip();

            int iday = day_index(tbx.Name);

            _ttHours.Show(EmpCount[iday].ToString(), tbx, 2000);
        }
    }


    /*******************************************************************************************************************\
     *                                                                                                                 *
    \*******************************************************************************************************************/

    public class RCD
    {
        string ename;
        public int[] hour;
        public string[] toff;

        public RCD(string emp_name) { ename = emp_name;  toff = new string[14]; hour = new int[14]; }
    }

  
    /*******************************************************************************************************************\
    *                                                                                                                 *
   \*******************************************************************************************************************/

    class Item
    {
        public object Tag;
        public string Text;
        public override string ToString() { return Text; }
    }
   
}

