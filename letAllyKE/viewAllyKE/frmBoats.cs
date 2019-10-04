using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Globalization;

using mdlAllyKE;


namespace viewAllyKE
{
    public partial class frmBoats : Form
    {
        public DateTime LogDate { get; set; }

        public DateTime RefWeek { get; set; }
        public bool SetAsCurrent { get; set; }

        private Boats _nboats { get; set; }
        
        private TableLayoutPanel tlpNote = new TableLayoutPanel();
        
        private DataSet _ds_sched { get; set; }
        private DataSet _ds_vessels { get; set; }

        

        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public frmBoats(DateTime user_day)
        {
            InitializeComponent();

            _nboats = new Boats();

            btnPlus.Show();
            btnMinus.Show();


            flpVessels.Hide();

            tlpNote.Name = "tlpNote";
            tlpNote.Size = new Size(300, 200);
            tlpNote.Location = new Point(200, 200);
            tlpNote.BackColor = Color.Yellow;
            tlpNote.AutoSize = true;
            this.Controls.Add(tlpNote);

            this.Tag = tbxBoat;

            RefWeek = set_date(user_day);

            draw_head(RefWeek.AddDays(7), DateTime.Now, SetAsCurrent, lblWeek2,
                tbxMon2, tbxTue2, tbxWed2, tbxThu2, tbxFri2, tbxSat2, tbxSun2);

            draw_head(RefWeek, DateTime.Now, SetAsCurrent, lblWeek,
                tbxMon, tbxTue, tbxWed, tbxThu, tbxFri, tbxSat, tbxSun);            
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/                     
        
        private int week_of_year(DateTime d)
        {
            CultureInfo info = CultureInfo.CurrentCulture;
            return info.Calendar.GetWeekOfYear(d, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }


        private DateTime week_starter()
        {
            DateTime d = DateTime.Now;
            int offset = d.DayOfWeek - DayOfWeek.Monday;
            offset = (offset == -1 ? 6 : offset);

            DateTime lastMonday = d.AddDays(-offset);

            return lastMonday;
        }


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
        
        private void draw_week(DateTime start_week, DateTime cal_week, bool current)
        {
            int woy = week_of_year(start_week);

            bool is_future_week = start_week.Ticks > cal_week.Ticks;
            bool is_current_week = week_of_year(start_week) == week_of_year(cal_week);


            if (is_current_week)
                this.BackColor = Color.LightGreen;
            else
                this.BackColor = SystemColors.Control;
        }


        private void tag_dates(TextBox tbxDH, DateTime start_week, int offset)
        {
            tbxDH.Tag = start_week.AddDays(offset);
        }

       
        private void draw_head(DateTime start_week, DateTime cal_week, bool current, Label lblWk,
            TextBox tbxM, TextBox tbxT, TextBox tbxW, TextBox tbxTh, TextBox tbxF, TextBox tbxSa, TextBox tbxSu)
        {
            int woy = week_of_year(start_week);

            bool is_future_week = start_week.Ticks > cal_week.Ticks;
            //bool is_current_week = (current ? week_of_year(start_week) == week_of_year(cal_week) : false);
            bool is_current_week = week_of_year(start_week) == week_of_year(cal_week);


            //if (is_current_week)
            //    this.BackColor = Color.LightGreen;
            //else
            //{
            //    if (is_future_week)
            //        if (woy % 2 == 1)
            //            this.BackColor = System.Drawing.Color.LightSalmon;
            //        else
            //            this.BackColor = System.Drawing.Color.DarkKhaki;
            //    else
            //        if (woy % 2 == 1)
            //            this.BackColor = Color.LightYellow;
            //        else
            //            this.BackColor = Color.SeaShell;
            //}
            if (is_current_week)
                this.BackColor = Color.LightSteelBlue;
            else
            {
                if (is_future_week)
                    if (woy % 2 == 1)
                        this.BackColor = System.Drawing.Color.DarkKhaki;
                    else
                        this.BackColor = System.Drawing.Color.DarkKhaki;
                else
                    if (woy % 2 == 1)
                        this.BackColor = System.Drawing.Color.LightSalmon;
                    else
                        this.BackColor = System.Drawing.Color.LightSalmon;
            }

            lblWk.Text = start_week.ToLongDateString();

            tbxM.Text = start_week.Day.ToString();
            tbxT.Text = start_week.AddDays(1).Day.ToString();
            tbxW.Text = start_week.AddDays(2).Day.ToString();
            tbxTh.Text = start_week.AddDays(3).Day.ToString();
            tbxF.Text = start_week.AddDays(4).Day.ToString();
            tbxSa.Text = start_week.AddDays(5).Day.ToString();
            tbxSu.Text = start_week.AddDays(6).Day.ToString();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private CheckBox ckb_make(string ctrl_name, string ctrl_text, string tag)
        {
            CheckBox ckb = new CheckBox();
            ckb.Size = new Size(ckb.Size.Width, ckb.Size.Height);
            ckb.BackColor = SystemColors.Control;
            ckb.Appearance = Appearance.Button;
            ckb.FlatStyle = FlatStyle.Flat;
            ckb.TextAlign = ContentAlignment.MiddleCenter;

            ckb.Name = ctrl_name;
            ckb.Text = ctrl_text;
            ckb.Tag = tag;

            return ckb;
        }


        //private CheckBox ckb_make_vessel(string ctrl_name, string ctrl_text, object ctrl_tag)
        //{
        //    CheckBox ckb = ckb_make(ctrl_name, ctrl_text);

        //    ckb.Tag = ctrl_tag;
        //    ckb.Click += new EventHandler(ckb_Click);

        //    return ckb;
        //}


        private CheckBox ckb_make_vessel(string name, string code)
        {
            CheckBox ckb = ckb_make(name, code, name);

            //ckb.Tag = ctrl_tag;
            ckb.Click += new EventHandler(ckb_Click);

            return ckb;
        }


        //void ckb_Click(object sender, EventArgs e)
        //{
        //    CheckBox ckb = (CheckBox)sender;

        //    Boatcrew b = (Boatcrew)(ckb.Tag);

        //    string name = b.name;
        //    string shift = b.shift;

        //    foreach (Control c in tlpVessel.Controls)
        //    {
        //        ucVessel uc = (ucVessel)c;

        //        ComboBox cbxItem = (ComboBox)(uc.Controls.Find("cbxItems", false)[0]);
        //        ComboBox cbxShift = (ComboBox)(uc.Controls.Find("cbxShift", false)[0]);

        //        cbxItem.Text = name;
        //        cbxShift.Text = shift;

        //        cbxItem.Enabled = false;
        //    }

        //    //for (int i = 0; i < tlpCrew.RowCount; i++)
        //    //{
        //    //    ucStat uc = (ucStat)tlpCrew.GetControlFromPosition(0, i);
        //    //    uc.RefreshShift((Boatcrew)(tbx.Tag));
        //    //}
        //}


        void ckb_Click(object sender, EventArgs e)
        {
            CheckBox ckb = (CheckBox)sender;

            tlpVessel.Controls.Clear();
            
            List<Boat> bl = _nboats.GetList(ckb.Tag.ToString());
            for (int i=1; i<bl.Count; i++)  // Skip first one - it is a summary
            {                
                ucVessel uc = new ucVessel(RefWeek, _ds_vessels, bl[i]);
                tlpVessel.Controls.Add(uc);
            }
            
            if (ckb.Checked)
            {
                foreach (Control c in tlpVessel.Controls)
                {
                    ucVessel uc = (ucVessel)c;                    
                    uc.RefreshVessel();
                }


                foreach (CheckBox c in flpVessels.Controls) c.Checked = false;
                ckb.Checked = true;
            }
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void load_data()
        {            
            _ds_sched = dacSched.GetDS(RefWeek, 14);
            _ds_vessels = dacVessels.GetDS();

        }


        private void load_totals()
        {
            cbxVessels.Text = "0";
            cbxShifts.Text = "0";
            cbxCrew.Text = "0";
        }
       
        

        private void load_shift()
        {            
            foreach (DataRow row in _ds_sched.Tables[0].Rows)
            {
                string bc = row["BoatCode"].ToString();
                string bn = row["BoatName"].ToString();
                int shiftid = Convert.ToInt32(row["ShiftID"]);

                _nboats.Add(bc, bn, shiftid, row);
            }

            foreach (var key in _nboats._boats)
            {
                Boat b = _nboats.Get(key.Key);
                //flpVessels.Controls.Add(ckb_make_vessel(b.boatname, b.boatcode));
                flpVessels.Controls.Add(ckb_make_vessel(b.boatcode, b.boatname));
            }

            foreach (CheckBox ckb in flpVessels.Controls) ckb.Checked = false;

           
            if (flpVessels.Controls.Count != 0)
            {                
                //btnPlus.Show();
                //btnMinus.Show();
                flpVessels.Show();
                
                CheckBox ckb = (CheckBox)(flpVessels.Controls[0]);
                ckb.Checked = true;
                ckb_Click(ckb, null);
            }           
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public void LoadStart()
        {
            load_data();
        }


        public void LoadReady()
        {
            load_shift();

            cmdCancel.Focus();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void frmBoats_Load(object sender, EventArgs e)
        {
            //load_data();
            
            //load_crew_from_row(RefWeek, row);
            //load_crew_from_file(RefWeek);
            //load_shift();

            //load_totals();


            //update_timebook();

            this.Text = string.Format("Boats for Weeks [{0} to {1}]",
                RefWeek.ToShortDateString(), RefWeek.AddDays(7).ToShortDateString());

        }


        private void dtpLogDate_ValueChanged(object sender, EventArgs e)
        {
            DateTime user_day = ((DateTimePicker)sender).Value;

            RefWeek = set_date(user_day);

            draw_head(RefWeek.AddDays(7), DateTime.Now, SetAsCurrent, lblWeek2,
                tbxMon2, tbxTue2, tbxWed2, tbxThu2, tbxFri2, tbxSat2, tbxSun2);

            draw_head(RefWeek, DateTime.Now, SetAsCurrent, lblWeek,
                tbxMon, tbxTue, tbxWed, tbxThu, tbxFri, tbxSat, tbxSun);

            //update_timebook();

            this.Text = string.Format("Schedule for Weeks [{0} to {1}]",
                RefWeek.ToShortDateString(), RefWeek.AddDays(7).ToShortDateString());
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void btnMinus_Click(object sender, EventArgs e)
        {
            //foreach (CheckBox ckb in flpVessels.Controls)            
            //    if (ckb.Checked)  flpVessels.Controls.Remove(ckb);

            foreach (ucVessel uc in tlpVessel.Controls)
                if (uc.IsChecked())  tlpVessel.Controls.Remove(uc);
                
        }


        private void btnPlus_Click(object sender, EventArgs e)
        {
            string bc = "";
            string bn = "";

            foreach (CheckBox c in flpVessels.Controls)
                if (c.Checked) { bc = c.Tag.ToString(); bn = c.Text; }

            //Boat b = _nboats.FindBoat(bc);
            //Boat b = _nboats.Get(bc);
            //if (b != null)  bn = b.boatname;


            ucVessel uc = new ucVessel(RefWeek, _ds_vessels, bc, bn);
            
            tlpVessel.Controls.Add(uc);

        }




        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void tbxBoat_TextChanged(object sender, EventArgs e)
        {
            TextBox tbx = (TextBox)sender;

            //string itv = tbx.Text;
            ////FlowLayoutPanel flp = (FlowLayoutPanel)(this.ParentForm.Tag);
            //FlowLayoutPanel flp = flpVessels;
            //Control[] test = flp.Controls.Find(itv, false);
            //if (test.Length == 0)
            //{
            //    btnPlus.Show();
            //    btnMinus.Show();
            //    //CheckBox ckb = ckb_make_vessel(itv, itv, tbx.Tag); 
            //    CheckBox ckb = ckb_make_vessel(itv);                
            //    flp.Controls.Add(ckb);
            //    //flp.Controls.SetChildIndex(ckb, flp.Controls.GetChildIndex(ckb) - 1);
            //    flp.Show();
            //    ckb.Checked = true;
            //    ckb_Click(ckb, null);
            //}
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            //btnPlus.Hide();
            //btnMinus.Hide();

            //tlpVessel.Controls.Clear();

            //ucVessel uc = new ucVessel(RefWeek, _ds_vessels);
            //tlpVessel.Controls.Add(uc);

            //foreach (CheckBox ckb in flpVessels.Controls)
            //    ckb.Checked = false;

            //foreach (ucVessel u in tlpVessel.Controls)
            //    u.ResetVessel();

        }


        private void cmdAddBoat_Click(object sender, EventArgs e)
        {
            List<string> vessel_list = new List<string>();

            foreach (CheckBox c in flpVessels.Controls) vessel_list.Add(c.Text);
               
            tlpVessel.Controls.Clear();
            foreach (CheckBox ckb in flpVessels.Controls) ckb.Checked = false;

            frmAddBoat frm = new frmAddBoat(_ds_vessels, vessel_list);
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                string bc = frm.SelectCode;
                string bn = frm.SelectName;
                int shiftid = 0;
                
                _nboats.Add(bc, bn, shiftid, null);
                

                CheckBox ckb = ckb_make_vessel(frm.SelectCode, frm.SelectName);
                flpVessels.Controls.Add(ckb);

                ckb.Checked = true;
            }
        }
    }    
}
