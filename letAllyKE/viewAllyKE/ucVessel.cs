using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace viewAllyKE
{
    public partial class ucVessel : UserControl
    {
        public string StatCode { get; set; }
        public string ShiftCode { get; set; }
        public int ShiftAM { get; set; }

        public DateTime RefWeek { get; set; }

        //private RCD _rcd;

        private Dictionary<string, Boatcrew> _oboats { get; set; }        

        private TableLayoutPanel _tlpNote;


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void constructor(DateTime start_week)
        {
            InitializeComponent();

            RefWeek = start_week;

            chk7F.Checked = true;
            chk7S.Checked = false;

            clear_shift(0);
            clear_shift(1);
            clear_note(0);
            clear_note(1);

            ucAM1.Hide();
            ucAM2.Hide();
            ucAM3.Hide();
            ucAM4.Hide();
            ucAM5.Hide();
            ucAM6.Hide();
            ucAM7.Hide();

            ucAM8.Hide();
            ucAM9.Hide();
            ucAM10.Hide();
            ucAM11.Hide();
            ucAM12.Hide();
            ucAM13.Hide();
            ucAM14.Hide();

            Item ita = new Item();
            ita.Tag = 1;
            ita.Text = "AM (6AM-6PM)";
            cbxShift.Items.Add(ita);

            Item itp = new Item();
            itp.Tag = 2;
            itp.Text = "PM (6PM-6AM)";
            cbxShift.Items.Add(itp);
        }

        public ucVessel(DateTime start_week, DataSet ds_vessels, string code, string name)
        {
            constructor(start_week);

            tbxItems.Text = name;
            tbxItems.Tag = code;
            tbxItems.Enabled = false;

            lblAM.Hide();
            cbxShift.Show();

            chk7F.Checked = true;

            chkVes.MouseUp += new MouseEventHandler(chkVes_CheckedChanged);
            chk7F.MouseUp += new MouseEventHandler(chkV7F_CheckedChanged);
            chk7S.MouseUp += new MouseEventHandler(chkV7S_CheckedChanged);
        }


        //public ucVessel(DateTime start_week, DataSet ds_vessels, Boatcrew boat, Boat b)
        public ucVessel(DateTime start_week, DataSet ds_vessels, Boat b)
        {
            constructor(start_week);

            //_boats = boats;
            //this.Tag = boat;
            this.Tag = b;
            
            foreach (DataRow row in ds_vessels.Tables[0].Rows)
            {
                Item it = new Item();
                it.Tag = row["Short"];
                it.Text = row["Full Name"].ToString();
                cbxItems.Items.Add(it);
            }


            lblAM.Hide();
            cbxShift.Show();

            //if (boat != null)
            //{
            //    cbxShift.Text = boat.shift;
            //    cbxItems.Text = boat.boatname;
            //}

            //chkVes.CheckedChanged += new EventHandler(chkVes_CheckedChanged);s
            //chk7F.CheckedChanged += new System.EventHandler(chkV7F_CheckedChanged);
            //chk7S.CheckedChanged += new System.EventHandler(chkV7S_CheckedChanged);

            chkVes.MouseUp +=new MouseEventHandler(chkVes_CheckedChanged);
            chk7F.MouseUp +=new MouseEventHandler(chkV7F_CheckedChanged);
            chk7S.MouseUp += new MouseEventHandler(chkV7S_CheckedChanged);
        }


        public void ResetVessel()
        {
            tbxItems.Text = "";
            //cbxItems.Text = "";
            cbxShift.Text = "";

            chkVes.Checked = false;
            //cbxItems.Enabled = true;
            tbxItems.Enabled = true;
            cbxShift.Enabled = true;

            clear_week(0);
            clear_week(1);
        }
        

        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void show_day(TextBox tbx, string toff, int hours)
        {
            if (hours != 0 && toff.Equals(""))
                tbx.Text = string.Format("{0} ({1})", toff, hours);
            else
                tbx.Text = toff;
        }

        
        //public void RefreshDay(DateTime day, string toff, int hours)
        //{
        //    int iday = ((TimeSpan)(day - RefWeek)).Days;
        //    if (iday < 0 || iday > 14) return;
                        
            
        //    _rcd.hour[iday] = hours;            
        //    _rcd.toff[iday] = toff;


        //    DayOfWeek dow = day.DayOfWeek;

        //    if (iday < 7)
        //    {
        //        switch (dow)
        //        {
        //            case DayOfWeek.Monday:
        //                show_day(tbxMH, toff, hours);
        //                break;

        //            case DayOfWeek.Tuesday:
        //                show_day(tbxTH, toff, hours);
        //                break;

        //            case DayOfWeek.Wednesday:
        //                show_day(tbxWH, toff, hours);
        //                break;

        //            case DayOfWeek.Thursday:
        //                show_day(tbxThH, toff, hours);
        //                break;

        //            case DayOfWeek.Friday:
        //                show_day(tbxFH, toff, hours);
        //                break;

        //            case DayOfWeek.Saturday:
        //                show_day(tbxSaH, toff, hours);
        //                break;

        //            case DayOfWeek.Sunday:
        //                show_day(tbxSuH, toff, hours);
        //                break;
        //        }
        //    }
        //    else
        //    {
        //        switch (dow)
        //        {
        //            case DayOfWeek.Monday:
        //                show_day(tbxMH2, toff, hours);
        //                break;

        //            case DayOfWeek.Tuesday:
        //                show_day(tbxTH2, toff, hours);
        //                break;

        //            case DayOfWeek.Wednesday:
        //                show_day(tbxWH2, toff, hours);
        //                break;

        //            case DayOfWeek.Thursday:
        //                show_day(tbxThH2, toff, hours);
        //                break;

        //            case DayOfWeek.Friday:
        //                show_day(tbxFH2, toff, hours);
        //                break;

        //            case DayOfWeek.Saturday:
        //                show_day(tbxSaH2, toff, hours);
        //                break;

        //            case DayOfWeek.Sunday:
        //                show_day(tbxSuH2, toff, hours);
        //                break;
        //        }
        //    }

        //}


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void clear_week(int week)
        {
            if (week == 0)
            {
                chk7F.Checked = false;
                //chk7F.Checked = true;

                tbxMH.BackColor = Color.White;
                tbxTH.BackColor = Color.White;
                tbxWH.BackColor = Color.White;
                tbxThH.BackColor = Color.White;
                tbxFH.BackColor = Color.White;
                tbxSaH.BackColor = Color.White;
                tbxSuH.BackColor = Color.White;

                tbxMH.Text = "";
                tbxTH.Text = "";
                tbxWH.Text = "";
                tbxThH.Text = "";
                tbxFH.Text = "";
                tbxSaH.Text = "";
                tbxSuH.Text = "";
            }
            else
            {
                chk7S.Checked = false;
                //chk7S.Checked = true;

                tbxMH2.BackColor = Color.White;
                tbxTH2.BackColor = Color.White;
                tbxWH2.BackColor = Color.White;
                tbxThH2.BackColor = Color.White;
                tbxFH2.BackColor = Color.White;
                tbxSaH2.BackColor = Color.White;
                tbxSuH2.BackColor = Color.White;

                tbxMH2.Text = "";
                tbxTH2.Text = "";
                tbxWH2.Text = "";
                tbxThH2.Text = "";
                tbxFH2.Text = "";
                tbxSaH2.Text = "";
                tbxSuH2.Text = "";
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
            }
        }

             
        public void RefreshWeek(string val)
        {
            StatCode = val;

            if (!chkVes.Checked) return;

            if (!chk7F.Checked) return;
            mark_week(0, StatCode);

            if (!chk7S.Checked) return;
            mark_week(1, StatCode);
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

        private bool all_V7F(string code)
        {
            int cnt1 = 0;
            string val1 = code;

            cnt1 += (tbxMH.Text.Equals(val1) ? 1 : 0);
            cnt1 += (tbxTH.Text.Equals(val1) ? 1 : 0);
            cnt1 += (tbxWH.Text.Equals(val1) ? 1 : 0);
            cnt1 += (tbxThH.Text.Equals(val1) ? 1 : 0);
            cnt1 += (tbxFH.Text.Equals(val1) ? 1 : 0);
            cnt1 += (tbxSaH.Text.Equals(val1) ? 1 : 0);
            cnt1 += (tbxSuH.Text.Equals(val1) ? 1 : 0);

            if (cnt1 == 7) return true;

            return false;
        }


        private bool all_V7S(string code)
        {
            int cnt1 = 0;
            string val1 = code;

            cnt1 += (tbxMH2.Text.Equals(val1) ? 1 : 0);
            cnt1 += (tbxTH2.Text.Equals(val1) ? 1 : 0);
            cnt1 += (tbxWH2.Text.Equals(val1) ? 1 : 0);
            cnt1 += (tbxThH2.Text.Equals(val1) ? 1 : 0);
            cnt1 += (tbxFH2.Text.Equals(val1) ? 1 : 0);
            cnt1 += (tbxSaH2.Text.Equals(val1) ? 1 : 0);
            cnt1 += (tbxSuH2.Text.Equals(val1) ? 1 : 0);

            if (cnt1 == 7) return true;

            return false;
        }


        private void load_shift(int idx, Boat b, int s)
        //private void load_shift(int idx, Boatcrew boat, int s, Boat b)
        {
            string code = b.daycode[idx];

            switch (idx)
            {
                case 0: tbxMH.Text = code; break;
                case 1: tbxTH.Text = code; break;
                case 2: tbxWH.Text = code; break;
                case 3: tbxThH.Text = code; break;
                case 4: tbxFH.Text = code; break;
                case 5: tbxSaH.Text = code; break;
                case 6: tbxSuH.Text = code; break;

                case 7: tbxMH2.Text = code; break;
                case 8: tbxTH2.Text = code; break;
                case 9: tbxWH2.Text = code; break;
                case 10: tbxThH2.Text = code; break;
                case 11: tbxFH2.Text = code; break;
                case 12: tbxSaH2.Text = code; break;
                case 13: tbxSuH2.Text = code; break;

                //case 0: tbxMH.Text = boat.boat[s, 0]; break;
                //case 1: tbxTH.Text = boat.boat[s, 1]; break;
                //case 2: tbxWH.Text = boat.boat[s, 2]; break;
                //case 3: tbxThH.Text = boat.boat[s, 3]; break;
                //case 4: tbxFH.Text = boat.boat[s, 4]; break;
                //case 5: tbxSaH.Text = boat.boat[s, 5]; break;
                //case 6: tbxSuH.Text = boat.boat[s, 6]; break;

                //case 7: tbxMH2.Text = boat.boat[s, 7]; break;
                //case 8: tbxTH2.Text = boat.boat[s, 8]; break;
                //case 9: tbxWH2.Text = boat.boat[s, 9]; break;
                //case 10: tbxThH2.Text = boat.boat[s, 10]; break;
                //case 11: tbxFH2.Text = boat.boat[s, 11]; break;
                //case 12: tbxSaH2.Text = boat.boat[s, 12]; break;
                //case 13: tbxSuH2.Text = boat.boat[s, 13]; break;
            }

            bool chk = all_V7F(code);
            chk7F.Checked = chk;
            if (chk)
                mark_week(0, code);

            chk = all_V7S(code);
            chk7S.Checked = chk;
            if (chk)
                mark_week(1, code);            

        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void apply_boat(int idx, Boat b)
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

        private void apply_shift(int idx, Boatcrew boat, int s)
        {
            switch (idx)
            {
                case 0: lblM.Text = boat.boat[s, 0]; ucAM1.On(boat.am[s, 0]); break;
                case 1: lblT.Text = boat.boat[s, 1]; ucAM2.On(boat.am[s, 1]); break;
                case 2: lblW.Text = boat.boat[s, 2]; ucAM3.On(boat.am[s, 2]); break;
                case 3: lblTh.Text = boat.boat[s, 3]; ucAM4.On(boat.am[s, 3]); break;
                case 4: lblF.Text = boat.boat[s, 4]; ucAM5.On(boat.am[s, 4]); break;
                case 5: lblSa.Text = boat.boat[s, 5]; ucAM6.On(boat.am[s, 5]); break;
                case 6: lblSu.Text = boat.boat[s, 6]; ucAM7.On(boat.am[s, 6]); break;

                case 7: lblM2.Text = boat.boat[s, 7]; ucAM8.On(boat.am[s, 7]); break;
                case 8: lblT2.Text = boat.boat[s, 8]; ucAM9.On(boat.am[s, 8]); break;
                case 9: lblW2.Text = boat.boat[s, 9]; ucAM10.On(boat.am[s, 9]); break;
                case 10: lblTh2.Text = boat.boat[s, 10]; ucAM11.On(boat.am[s, 10]); break;
                case 11: lblF2.Text = boat.boat[s, 11]; ucAM12.On(boat.am[s, 11]); break;
                case 12: lblSa2.Text = boat.boat[s, 12]; ucAM13.On(boat.am[s, 12]); break;
                case 13: lblSu2.Text = boat.boat[s, 13]; ucAM14.On(boat.am[s, 13]); break;
            }
        }


        private void mark_shift(int week, string val, int am)
        {
            if (week == 0)
            {
                lblM.Text = val;
                lblT.Text = val;
                lblW.Text = val;
                lblTh.Text = val;
                lblF.Text = val;
                lblSa.Text = val;
                lblSu.Text = val;

                ucAM1.On(am);
                ucAM2.On(am);
                ucAM3.On(am);
                ucAM4.On(am);
                ucAM5.On(am);
                ucAM6.On(am);
                ucAM7.On(am);
            }
            else
            {
                lblM2.Text = val;
                lblT2.Text = val;
                lblW2.Text = val;
                lblTh2.Text = val;
                lblF2.Text = val;
                lblSa2.Text = val;
                lblSu2.Text = val;

                ucAM8.On(am);
                ucAM9.On(am);
                ucAM10.On(am);
                ucAM11.On(am);
                ucAM12.On(am);
                ucAM13.On(am);
                ucAM14.On(am);

            }
        }


        private void clear_shift(int week)
        {
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
        public void RefreshBoat(Boat b)
        {
            //ShiftCode = val;
            //ShiftAM = am;

            if (!chkVes.Checked) return;
            //if (!chk7F.Checked) return;

            //mark_shift(0, ShiftCode, ShiftAM);
            for (int i=0; i<14; i++)  apply_boat(i, b);
        }


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
        //            for (int i = 0; i < 7; i++) { b.boat[s,week * 7 + i] = boat; b.am[s, week * 7 + i] = am; }

        //    }
        //    return b;
        //}


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void chkV7F_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            if (!chkVes.Checked) return;
            
            TextBox tbx = (TextBox)(this.ParentForm.Tag);
            //TextBox tbx = (TextBox)(this.ParentForm.Controls.Find("tbxShift", true)[0]);
            
            if (chk.Checked)
            {
                mark_week(0, ShiftCode);
                //tbx.Tag = boat_update(0, ShiftCode, ShiftAM, _boats, -1);  // needs to be first
                //tbx.Text = ShiftCode + ShiftAM.ToString();
                tbx.Text = ShiftCode;
            }
            else
            {
                clear_week(0);
                //tbx.Tag = boat_update(0, "", 0, _boats, -1);  // needs to be first
                tbx.Text = "";                
            }                        
        }


        private void chkV7S_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            if (!chkVes.Checked) return;

            TextBox tbx = (TextBox)(this.ParentForm.Controls.Find("tbxShift", true)[0]);            

            if (chk.Checked)
            {
                mark_week(1, ShiftCode);
                //tbx.Tag = boat_update(1, ShiftCode, ShiftAM, _boats, -1);  // needs to be first
                tbx.Text = ShiftCode + ShiftAM.ToString();
            }
            else
            {
                clear_week(1);
                //tbx.Tag = boat_update(1, "", 0, _boats, -1);  // needs to be first
                tbx.Text = "";
            }

        }


        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/                

        private void apply0_code(TextBox tbx, string val)
        {
            if (chk7F.Checked)  return;
 
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
        
        private void tbxMH_Click(object sender, EventArgs e)
        {
            apply0_code(tbxMH, ShiftCode);
            return;

            //if (_oboats == null)
            //    apply0_code(tbxMH, StatCode);
            //else
            //{
            //    apply0_code(tbxMH, ShiftCode);                
            //    TextBox tbx = (TextBox)(this.ParentForm.Controls.Find("tbxShift", true)[0]);
            //    //tbx.Tag = boat_update(0, ShiftCode, ShiftAM, _boats, 0);  // needs to be first
            //    tbx.Text = "0";
            //}
        }

        private void tbxTH_Click(object sender, EventArgs e)
        {
            apply0_code(tbxTH, ShiftCode);
            return;

            //if (_oboats == null)
            //    apply0_code(tbxTH, StatCode);
            //else
            //{
            //    apply0_code(tbxTH, ShiftCode);
            //    TextBox tbx = (TextBox)(this.ParentForm.Controls.Find("tbxShift", true)[0]);
            //    //tbx.Tag = boat_update(0, ShiftCode, ShiftAM, _boats, 1);  // needs to be first
            //    tbx.Text = "1";
            //}
        }

        private void tbxWH_Click(object sender, EventArgs e)
        {
            apply0_code(tbxWH, ShiftCode);
            return;

            //if (_oboats == null)
            //    apply0_code(tbxWH, StatCode);
            //else
            //{
            //    apply0_code(tbxWH, ShiftCode);
            //    TextBox tbx = (TextBox)(this.ParentForm.Controls.Find("tbxShift", true)[0]);
            //    //tbx.Tag = boat_update(0, ShiftCode, ShiftAM, _boats, 2);  // needs to be first
            //    tbx.Text = "2";
            //}
        }

        private void tbxThH_Click(object sender, EventArgs e)
        {
            apply0_code(tbxThH, ShiftCode);
            return;

            //if (_oboats == null)
            //    apply0_code(tbxThH, StatCode);
            //else
            //{
            //    apply0_code(tbxThH, ShiftCode);
            //    TextBox tbx = (TextBox)(this.ParentForm.Controls.Find("tbxShift", true)[0]);
            //    //tbx.Tag = boat_update(0, ShiftCode, ShiftAM, _boats, 3);  // needs to be first
            //    tbx.Text = "3";
            //}
        }

        private void tbxFH_Click(object sender, EventArgs e)
        {
            apply0_code(tbxFH, ShiftCode);
            return;

            //if (_oboats == null)
            //    apply0_code(tbxFH, StatCode);
            //else
            //{
            //    apply0_code(tbxFH, ShiftCode);
            //    TextBox tbx = (TextBox)(this.ParentForm.Controls.Find("tbxShift", true)[0]);
            //    //tbx.Tag = boat_update(0, ShiftCode, ShiftAM, _boats, 4);  // needs to be first
            //    tbx.Text = "4";
            //}
        }

        private void tbxSaH_Click(object sender, EventArgs e)
        {
            apply0_code(tbxSaH, ShiftCode);
            return;

            //if (_oboats == null)
            //    apply0_code(tbxSaH, StatCode);
            //else
            //{
            //    apply0_code(tbxSaH, ShiftCode);
            //    TextBox tbx = (TextBox)(this.ParentForm.Controls.Find("tbxShift", true)[0]);
            //    //tbx.Tag = boat_update(0, ShiftCode, ShiftAM, _boats, 5);  // needs to be first
            //    tbx.Text = "5";
            //}
        }

        private void tbxSuH_Click(object sender, EventArgs e)
        {
            apply0_code(tbxSuH, ShiftCode);
            return;

            //if (_oboats == null)
            //    apply0_code(tbxSuH, StatCode);
            //else
            //{
            //    apply0_code(tbxSuH, ShiftCode);
            //    TextBox tbx = (TextBox)(this.ParentForm.Controls.Find("tbxShift", true)[0]);
            //    //tbx.Tag = boat_update(0, ShiftCode, ShiftAM, _boats, 6);  // needs to be first
            //    tbx.Text = "6";
            //}
        }


        private void tbxMH2_Click(object sender, EventArgs e)
        {
            apply1_code(tbxMH2, ShiftCode);
            return;

            //if (_oboats == null)
            //    apply1_code(tbxMH2, StatCode);
            //else
            //{
            //    apply1_code(tbxMH2, ShiftCode);
            //    TextBox tbx = (TextBox)(this.ParentForm.Controls.Find("tbxShift", true)[0]);
            //    //tbx.Tag = boat_update(1, ShiftCode, ShiftAM, _boats, 0);  // needs to be first
            //    tbx.Text = "7";
            //}
        }

        private void tbxTH2_Click(object sender, EventArgs e)
        {
            apply1_code(tbxTH2, ShiftCode);
            return;

            //if (_oboats == null)
            //    apply1_code(tbxTH2, StatCode);
            //else
            //{
            //    apply1_code(tbxTH2, ShiftCode);
            //    TextBox tbx = (TextBox)(this.ParentForm.Controls.Find("tbxShift", true)[0]);
            //    //tbx.Tag = boat_update(1, ShiftCode, ShiftAM, _boats, 1);  // needs to be first
            //    tbx.Text = "8";
            //}
        }

        private void tbxWH2_Click(object sender, EventArgs e)
        {
            apply1_code(tbxWH2, ShiftCode);
            return;

            //if (_oboats == null)
            //    apply1_code(tbxWH2, StatCode);
            //else
            //{
            //    apply1_code(tbxWH2, ShiftCode);
            //    TextBox tbx = (TextBox)(this.ParentForm.Controls.Find("tbxShift", true)[0]);
            //    //tbx.Tag = boat_update(1, ShiftCode, ShiftAM, _boats, 2);  // needs to be first
            //    tbx.Text = "9";
            //}
        }

        private void tbxThH2_Click(object sender, EventArgs e)
        {
            apply1_code(tbxThH2, StatCode);
            return;

            //if (_oboats == null)
            //    apply1_code(tbxThH2, StatCode);
            //else
            //{
            //    apply1_code(tbxThH2, ShiftCode);                
            //    TextBox tbx = (TextBox)(this.ParentForm.Controls.Find("tbxShift", true)[0]);
            //    //tbx.Tag = boat_update(1, ShiftCode, ShiftAM, _boats, 3);  // needs to be first
            //    tbx.Text = "10";
            //}
        }

        private void tbxFH2_Click(object sender, EventArgs e)
        {
            apply1_code(tbxFH2, ShiftCode);
            return;

            //if (_oboats == null)
            //    apply1_code(tbxFH2, StatCode);
            //else
            //{
            //    apply1_code(tbxFH2, ShiftCode);
            //    TextBox tbx = (TextBox)(this.ParentForm.Controls.Find("tbxShift", true)[0]);
            //    //tbx.Tag = boat_update(1, ShiftCode, ShiftAM, _boats, 4);  // needs to be first
            //    tbx.Text = "11";
            //}
        }

        private void tbxSaH2_Click(object sender, EventArgs e)
        {
            apply1_code(tbxSaH2, ShiftCode);
            return;

            //if (_oboats == null)
            //    apply1_code(tbxSaH2, StatCode);
            //else
            //{
            //    apply1_code(tbxSaH2, ShiftCode);
            //    TextBox tbx = (TextBox)(this.ParentForm.Controls.Find("tbxShift", true)[0]);
            //    //tbx.Tag = boat_update(1, ShiftCode, ShiftAM, _boats, 5);  // needs to be first
            //    tbx.Text = "12";
            //}
        }

        private void tbxSuH2_Click(object sender, EventArgs e)
        {
            apply1_code(tbxSuH2, ShiftCode);
            return;

            //if (_oboats == null)
            //    apply1_code(tbxSuH2, StatCode);
            //else
            //{
            //    apply1_code(tbxSuH2, ShiftCode);
            //    TextBox tbx = (TextBox)(this.ParentForm.Controls.Find("tbxShift", true)[0]);
            //    //tbx.Tag = boat_update(1, ShiftCode, ShiftAM, _boats, 6);  // needs to be first
            //    tbx.Text = "13";
            //}
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void nudComment_ValueChanged(object sender, EventArgs e)
        {
            ucNote uc = new ucNote("X", "NA", DateTime.Now);
            _tlpNote = (TableLayoutPanel)(this.ParentForm.Controls.Find("tlpNote", false)[0]);
            _tlpNote.Controls.Add(uc);

            _tlpNote.Show();
            _tlpNote.BringToFront();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public void RefreshVessel()
        {            
            Boat b = (Boat)(this.Tag);
                        
            int shiftid = b.shiftid;
            cbxShift.Text = b.shiftname; 
            //cbxItems.Text = b.boatname;
            tbxItems.Text = b.boatname;
            tbxItems.Tag = b.boatcode;
            

            //Boatcrew b = (Boatcrew)(this.Tag);
            //int shiftid = b.shiftid;
            //cbxItems.SelectedItem = b.boatname;
            //cbxShift.SelectedItem = b.shift;
            //cbxItems.Text = b.boatname;
            //cbxShift.Text = b.shift;
            
            //mark_week(0, boat.code);
            //mark_week(1, boat.code);
            //for (int i = 0; i < 14; i++) apply_shift(i, b, shiftid);

            for (int i = 0; i < 14; i++) load_shift(i, b, shiftid);
            

            //cbxItems.Enabled = false;
            tbxItems.Enabled = false;
            cbxShift.Enabled = false;

            chk7F.Checked = b.chk7F;
            chk7S.Checked = b.chk7S;

            chkVes.Checked = true;
        }
        

        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void chkVes_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            if (chk.Checked)
            {
                Item its = (Item)(cbxShift.SelectedItem);
                //Item itv = (Item)(cbxItems.SelectedItem);
                

                //ShiftCode = (itv == null ? "" : itv.Tag.ToString()) + (its == null ? "" : its.Tag.ToString());
                //ShiftCode = (itv == null ? "" : itv.Tag.ToString());
                ShiftCode = tbxItems.Tag.ToString();
                ShiftAM = (its == null ? 0 : (Int32)(its.Tag));

                if (chk7F.Checked) mark_week(0, ShiftCode);
                if (chk7S.Checked) mark_week(1, ShiftCode);

                //TextBox tbxBoat = (TextBox)(this.ParentForm.Tag);
                //tbxBoat.Tag = _boats[ShiftCode];
                //tbxBoat.Text = itv.Text;

            }
            else
            {
                ShiftCode = "";
                ShiftAM = 0;
                clear_week(0);
                clear_week(1);
            }

            Boatcrew b = (Boatcrew)(this.Tag);
            if (b != null)
            {
                b.code = ShiftCode;
                b.boatname = cbxItems.Text;
                b.shiftid = ShiftAM;
                for (int i = 0; i < 14; i++) apply_shift(i, b, b.shiftid);

                ((TextBox)(this.ParentForm.Tag)).Text = ShiftCode;
            }
        }


        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        private void cbxItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            Item its = (Item)(cbxShift.SelectedItem);
            Item itv = (Item)(cbxItems.SelectedItem);

            //ShiftCode = (itv == null ? "" : itv.Tag.ToString()) + (its == null ? "" : its.Tag.ToString());
            ShiftCode = (itv == null ? "" : itv.Tag.ToString());
            ShiftAM = (its == null ? 0 : (Int32)(its.Tag));

            string shift = (ShiftAM == 0 ? "" : its.Text);


            if (chk7F.Checked) mark_week(0, ShiftCode);

            //boat_make(ShiftCode, itv.Text, shift, _boats);

            //if (chk7F.Checked)
            //    //for (int i = 0; i < 7; i++) _boat.boat[i] = ShiftCode;
            //     boat_update(0, ShiftCode, ShiftAM, _boats, -1);

            //if (chk7S.Checked)
            //    //for (int i = 7; i < 14; i++) _boat.boat[i] = ShiftCode;
            //    boat_update(1, ShiftCode, ShiftAM, _boats, -1);
        }


        private void cbxShift_SelectedIndexChanged(object sender, EventArgs e)
        {
            Item its = (Item)(cbxShift.SelectedItem);
            //Item itv = (Item)(cbxItems.SelectedItem);

            //ShiftCode = (itv == null ? "" : itv.Tag.ToString()) + (its == null ? "" : its.Tag.ToString());
            //ShiftCode = (itv == null ? "" : itv.Tag.ToString());
            ShiftAM = (its == null ? 0 : (Int32)(its.Tag));
            ShiftCode = tbxItems.Tag.ToString();

            //string name = (itv == null ? "" : itv.Text);
            //boat_make(ShiftCode, name, its.Text, _boats);

            //if (chk7F.Checked)
            //    boat_update(0, ShiftCode, ShiftAM, _boats, -1);
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

            //if (chk7S.Checked)
            //    boat_update(1, ShiftCode, ShiftAM, _boats, -1);
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


            chkVes.Focus();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public bool IsChecked()
        {
            return this.chkVes.Checked;
        }

    }



    /*******************************************************************************************************************\
     *                                                                                                                 *
    \*******************************************************************************************************************/

    public class Boatcrew
    {
        public int idx;
        public string code;
        public string boatname;
        public int shiftid;
        public string shift;        
        //public string vessel;
        public int[,] am;        
        public string[,] boat;        

        public Boatcrew()
        {
            idx = 0;
            am = new int[3,14];
            boat = new string[3,14];            
        }        
    }


    /*******************************************************************************************************************\
     *                                                                                                                 *
    \*******************************************************************************************************************/

    public class Boat
    {
        public int idx { set; get; }
        public string boatcode { set; get; }
        public string boatname { set; get; }
        public int shiftid { set; get; }
        public string shiftname { set; get; }
        
        public int[] dayshft;
        public string[] daycode;

        public bool chk7F;
        public bool chk7S;

        public DateTime ref_week { set; get; }


        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        //private Boatcrew make_boatcrew(DataRow row)
        //{
        //    Boatcrew b = new Boatcrew();

        //    string bc = row["BoatCode"].ToString();
        //    int s = Convert.ToInt32(row["ShiftID"]);

        //    b.code = bc;
        //    b.shiftid = s;
        //    b.boatname = row["BoatName"].ToString();
        //    b.shift = row["Shift"].ToString();            

        //    if (Convert.ToBoolean(row["OnM"])) { daycode[0] = bc; dayshft[0] = s; }
        //    if (Convert.ToBoolean(row["OnT"])) { daycode[1] = bc; dayshft[1] = s; }
        //    if (Convert.ToBoolean(row["OnW"])) { daycode[2] = bc; dayshft[2] = s; }
        //    if (Convert.ToBoolean(row["OnTh"])) { daycode[3] = bc; dayshft[3] = s; }
        //    if (Convert.ToBoolean(row["OnF"])) { daycode[4] = bc; dayshft[4] = s; }
        //    if (Convert.ToBoolean(row["OnSa"])) { daycode[5] = bc; dayshft[5] = s; }
        //    if (Convert.ToBoolean(row["OnSu"])) { daycode[6] = bc; dayshft[6] = s; }

        //    return b;
        //}


        //private void modify_boatcrew(Boatcrew b, DataRow row)
        //{
        //    string bc = row["BoatCode"].ToString();

        //    b.code = bc;
        //    b.boatname = row["BoatName"].ToString();
        //    b.shiftid = Convert.ToInt32(row["ShiftID"]);
        //    b.shift = row["Shift"].ToString();

        //    int s = Convert.ToInt32(row["ShiftID"]);

        //    if (Convert.ToBoolean(row["OnM"])) b.boat[s, 0] = bc;
        //    if (Convert.ToBoolean(row["OnT"])) b.boat[s, 0] = bc;
        //    if (Convert.ToBoolean(row["OnW"])) b.boat[s, 0] = bc;
        //    if (Convert.ToBoolean(row["OnTh"])) b.boat[s, 0] = bc;
        //    if (Convert.ToBoolean(row["OnF"])) b.boat[s, 0] = bc;
        //    if (Convert.ToBoolean(row["OnSa"])) b.boat[s, 0] = bc;
        //    if (Convert.ToBoolean(row["OnSu"])) b.boat[s, 0] = bc;

        //}


        public Boat(string code, string name, int sid, DataRow row)
        {
            shiftid = sid;

            boatcode = code;
            boatname = name;

            idx = 0;
            dayshft = new int[14];
            daycode = new string[14];

            if (row == null)
            {
                shiftname = "";
                ref_week = DateTime.Now;
            }
            else
            {
                //string name = row["BoatName"].ToString();

                shiftname = row["Shift"].ToString();
                //shiftid = Convert.ToInt32(row["ShiftId"]);

                ref_week = Convert.ToDateTime(row["ShipDate"]);

                Shift0(row);
                //make_boatcrew(row);
            }
        }

        public void Shift0(DataRow row)
        {
            string bc = row["BoatCode"].ToString();
            int s = Convert.ToInt32(row["ShiftID"]);

            int count_days = 0;

            if (Convert.ToBoolean(row["OnM"])) { daycode[0] = bc; dayshft[0] = s; count_days++;  }
            if (Convert.ToBoolean(row["OnT"])) { daycode[1] = bc; dayshft[1] = s; count_days++; }
            if (Convert.ToBoolean(row["OnW"])) { daycode[2] = bc; dayshft[2] = s; count_days++; }
            if (Convert.ToBoolean(row["OnTh"])) { daycode[3] = bc; dayshft[3] = s; count_days++; }
            if (Convert.ToBoolean(row["OnF"])) { daycode[4] = bc; dayshft[4] = s; count_days++; }
            if (Convert.ToBoolean(row["OnSa"])) { daycode[5] = bc; dayshft[5] = s; count_days++; }
            if (Convert.ToBoolean(row["OnSu"])) { daycode[6] = bc; dayshft[6] = s; count_days++; }

            chk7F = false;
            if (count_days == 7) chk7F = true;
        }


        public void Shift1(DataRow row)
        {
            string bc = row["BoatCode"].ToString();
            int s = Convert.ToInt32(row["ShiftID"]);

            int count_days = 0;

            if (Convert.ToBoolean(row["OnM"])) { daycode[7] = bc; dayshft[7] = s; count_days++; }
            if (Convert.ToBoolean(row["OnT"])) { daycode[8] = bc; dayshft[8] = s; count_days++; }
            if (Convert.ToBoolean(row["OnW"])) { daycode[9] = bc; dayshft[9] = s; count_days++; }
            if (Convert.ToBoolean(row["OnTh"])) { daycode[10] = bc; dayshft[10] = s; count_days++; }
            if (Convert.ToBoolean(row["OnF"])) { daycode[11] = bc; dayshft[11] = s; count_days++; }
            if (Convert.ToBoolean(row["OnSa"])) { daycode[12] = bc; dayshft[12] = s; count_days++; }
            if (Convert.ToBoolean(row["OnSu"])) { daycode[13] = bc; dayshft[13] = s; count_days++; }

            chk7S = false;
            if (count_days == 7) chk7S = true;
        }


        public void Week0(int shiftid, string shift)
        {
            for (int i = 0; i < 7; i++) { dayshft[i] = shiftid; daycode[i] = shift; }
        }

        public void Week1(int shiftid, string shift)
        {
            for (int i = 7; i < 14; i++) { dayshft[i] = shiftid; daycode[i] = shift; }
        }

    }


    /*******************************************************************************************************************\
     *                                                                                                                 *
    \*******************************************************************************************************************/

    public class Boats
    {
        public Dictionary<string, List<Boat>> _boats;


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        //private Boatcrew make_boatcrew(DataRow row)
        //{
        //    Boatcrew b = new Boatcrew();

        //    string bc = row["BoatCode"].ToString();
        //    int s = Convert.ToInt32(row["ShiftID"]);

        //    b.code = bc;
        //    b.shiftid = s;
        //    b.boatname = row["BoatName"].ToString();
        //    b.shift = row["Shift"].ToString();

        //    if (Convert.ToBoolean(row["OnM"])) { b.boat[s, 0] = bc; b.am[s, 0] = s; }
        //    if (Convert.ToBoolean(row["OnT"])) { b.boat[s, 0] = bc; b.am[s, 0] = s; }
        //    if (Convert.ToBoolean(row["OnW"])) { b.boat[s, 0] = bc; b.am[s, 0] = s; }
        //    if (Convert.ToBoolean(row["OnTh"])) { b.boat[s, 0] = bc; b.am[s, 0] = s; }
        //    if (Convert.ToBoolean(row["OnF"])) { b.boat[s, 0] = bc; b.am[s, 0] = s; }
        //    if (Convert.ToBoolean(row["OnSa"])) { b.boat[s, 0] = bc; b.am[s, 0] = s; }
        //    if (Convert.ToBoolean(row["OnSu"])) { b.boat[s, 0] = bc; b.am[s, 0] = s; }

        //    return b;
        //}


        //private void modify_boatcrew(Boatcrew b, DataRow row)
        //{
        //    string bc = row["BoatCode"].ToString();

        //    b.code = bc;
        //    b.boatname = row["BoatName"].ToString();
        //    b.shiftid = Convert.ToInt32(row["ShiftID"]);
        //    b.shift = row["Shift"].ToString();

        //    int s = Convert.ToInt32(row["ShiftID"]);

        //    if (Convert.ToBoolean(row["OnM"])) b.boat[s, 0] = bc;
        //    if (Convert.ToBoolean(row["OnT"])) b.boat[s, 0] = bc;
        //    if (Convert.ToBoolean(row["OnW"])) b.boat[s, 0] = bc;
        //    if (Convert.ToBoolean(row["OnTh"])) b.boat[s, 0] = bc;
        //    if (Convert.ToBoolean(row["OnF"])) b.boat[s, 0] = bc;
        //    if (Convert.ToBoolean(row["OnSa"])) b.boat[s, 0] = bc;
        //    if (Convert.ToBoolean(row["OnSu"])) b.boat[s, 0] = bc;

        //}


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public Boats()
        {
            _boats = new Dictionary<string, List<Boat>>();
        }

        
        public Boat Get(string code) { return (_boats[code])[0]; }
        public List<Boat> GetList(string code) { return _boats[code]; }

        public Boat FindBoat(string name)
        {
            foreach (var key in _boats)
            {
                Boat b = Get(key.Key);
                if (b.boatname.Equals(name)) return b;
            }

            return null;
        }


        public Predicate<Boat> byCodeShift(string code, int shiftid)
        {
            return delegate(Boat b) { return b.boatcode.Equals(code) && b.shiftid == shiftid; };
        }
        
        public Boat Add(string code, string name, int shiftid, DataRow row)
        {                
            List<Boat> blst;
                        
            // find boat in dict or add boat
            if (_boats.ContainsKey(code)) 
                blst = _boats[code];
            else
            {
                blst = new List<Boat>();
                blst.Add(new Boat(code, name, 0, row));
            }

            if (!_boats.ContainsKey(code)) _boats.Add(code, blst);

            if (row == null)  return null;
                
            // find shift for boat
            Boat b = blst.Find(byCodeShift(code, shiftid));
            if (b != null)
                b.Shift1(row);
            else
            {
                b = new Boat(code, name, shiftid, row);
                blst.Add(b);
            }                                    

            return b;
        }
    }
}

