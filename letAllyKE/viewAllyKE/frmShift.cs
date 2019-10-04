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
    public partial class frmShift : Form
    {
        public DateTime LogDate { get; set; }

        public DateTime RefWeek { get; set; }
        public bool SetAsCurrent { get; set; }

        private Boats _nboats { get; set; }
        private Dictionary<string, List<Boatcrew>> _oboats;        

        private TableLayoutPanel tlpNote = new TableLayoutPanel();

        private DataSet _ds_crew { get; set; }
        private DataSet _ds_sched { get; set; }
        private DataSet _ds_vessels { get; set; }

        private Dictionary<string, ucStat> _crew { get; set; }
        


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public frmShift(DateTime user_day)
        {
            InitializeComponent();

            _nboats = new Boats();
            _crew = new Dictionary<string, ucStat>();

            flpVessels.Hide();

            tlpNote.Name = "tlpNote";
            tlpNote.Size = new Size(300, 200);
            tlpNote.Location = new Point(200, 200);
            tlpNote.BackColor = Color.Yellow;
            tlpNote.AutoSize = true;
            this.Controls.Add(tlpNote);


            //this.Tag = flpVessels;
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
        
        private void cbxLegend_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbx = (ComboBox)sender;
            
            for (int i = 0; i < tlpCrew.RowCount; i++)
            {
                ucStat uc = (ucStat)tlpCrew.GetControlFromPosition(0, i);
                
                uc.RefreshWeek();

                //uc.RefreshWeek(cbx.SelectedValue.ToString());
                //int am = (tbxShift.Tag == null ? 0 : (Int32)(tbxShift.Tag));
                //uc.RefreshShift(tbxShift.Text, am);
            }
        }



        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void update_timebook()  // shift_version
        {
            if (RefWeek == null)  return;

            DataSet ds = dacTimebook.GetDS(RefWeek, 14);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var bookdate_ = row["Bookdate"];

                if (bookdate_ == DBNull.Value) continue;
                DateTime bookdate = (DateTime)bookdate_;
                if (bookdate.Date < RefWeek.Date) continue;
                if (bookdate.Date >= RefWeek.AddDays(14).Date) continue;


                //ucStat uc = (ucStat)tlpCrew.GetControlFromPosition(0, 0);
                ucStat uc = _crew[row["EmpID"].ToString()];
                uc.RefreshDay(bookdate, row["ToffCode"].ToString(), 
                    Convert.ToSingle(row["LogHours"]), Convert.ToSingle(row["LogOver"]), 
                    row["LogVessel"].ToString(), Convert.ToInt32(row["LogShift"]), row["LogNote"].ToString());
            }
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
            CheckBox ckb = ckb_make(name, code, code);

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

            for (int i=1; i<bl.Count; i++)  // Skip first one
            {                
                ucVessel uc = new ucVessel(RefWeek, _ds_vessels, bl[i]);
                tlpVessel.Controls.Add(uc);
            }

            //Boatcrew bc = (_oboats[ckb.Text])[0];
            //int shift = 0;

            
            if (ckb.Checked)
            {
                foreach (Control c in tlpVessel.Controls)
                {
                    ucVessel uc = (ucVessel)c;                    
                    //uc.RefreshShift(bc, shift++);
                    uc.RefreshVessel();
                }

            }
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void load_data()
        {
            _ds_crew = dacTimebook.GetDS(RefWeek, 14);
            _ds_sched = dacSched.GetDS(RefWeek, 14);
            _ds_vessels = dacVessels.GetDS();
        }


        private void load_totals()
        {
            cbxVessels.Text = "0";
            cbxShifts.Text = "0";
            cbxCrew.Text = "0";
        }


        private void load_crew_from_row(DateTime start_of_week, DataRow row)
        {
            //ucStat uc = new ucStat(start_of_week, row["EmpName"].ToString());
            //_crew.Add(row["EmpID"].ToString(), uc);
            //tlpCrew.Controls.Add(uc);
            //tlpCrew.RowCount += 1;
        }


        private void load_crew_from_file(DateTime start_of_week)
        {
            //tlpCrew.AutoScroll = true;

            //tlpCrew.RowStyles.Clear();
            //tlpCrew.ColumnStyles.Clear();
            //tlpCrew.RowCount = 0;
            //tlpCrew.ColumnCount = 0;

            DataTable dt = _ds_crew.Tables[0].AsEnumerable()
                    .GroupBy(r => r.Field<string>("EmpId"))
                    .Select(g => g.First())
                        .CopyToDataTable();

            foreach (DataRow row in dt.Rows)
            {
                if (_crew.ContainsKey(row["EmpID"].ToString())) continue;

                ucStat uc = new ucStat(start_of_week, row["EmpName"].ToString(), row["EmpID"].ToString(), false);
                _crew.Add(row["EmpID"].ToString(), uc);
                tlpCrew.Controls.Add(uc);
                tlpCrew.RowCount += 1;
            }
        }


        private void load_shift()
        {
            _oboats = new Dictionary<string, List<Boatcrew>>();
            
            foreach (DataRow row in _ds_sched.Tables[0].Rows)
            {
                //string bc = row["BoatName"].ToString();

                //if (_boats.ContainsKey(bc))
                //{
                //    modify_boatcrew(_boats[bc], row);
                //}
                //else
                //{
                //    _boats.Add(bc, make_boatcrew(row));
                //    flpVessels.Controls.Add(ckb_make_vessel(bc));
                //}
                
                //string bn = row["BoatName"].ToString(); 
                string bc = row["BoatCode"].ToString();
                string bn = row["BoatName"].ToString();
                int shiftid = Convert.ToInt32(row["ShiftID"]);

                //Boat b = _nboats.Add(bc, row);
                _nboats.Add(bc, bn, shiftid, row);
            }

            foreach (var key in _nboats._boats)
            {
                Boat b = _nboats.Get(key.Key);

                flpVessels.Controls.Add(ckb_make_vessel(b.boatname, b.boatcode));

                //List<Boatcrew> lb;
                //if (_oboats.ContainsKey(bn))
                //{
                //    lb = _oboats[bn];
                //    lb.Add(make_boatcrew(row));
                //    //_boats.Add(bn, lb);
                //}
                //else
                //{
                //    lb = new List<Boatcrew>();
                //    lb.Add(make_boatcrew(row));
                //    _oboats.Add(bn, lb);

                //    //flpVessels.Controls.Add(ckb_make_vessel(bn));
                //    flpVessels.Controls.Add(ckb_make_vessel(b.boatname));
                //}
                //ucVessel uc = new ucVessel(RefWeek, _ds_vessels, lb[lb.Count - 1], b);

                //ucVessel uc = new ucVessel(RefWeek, _ds_vessels, b);
                //tlpVessel.Controls.Add(uc);
            
            }

            //ucVessel uc = new ucVessel(RefWeek, _ds_vessels, _boats);
            //tlpVessel.Controls.Add(uc);
            //uc = new ucVessel(RefWeek, _ds_vessels, _boats);
            //tlpVessel.Controls.Add(uc);

            if (flpVessels.Controls.Count != 0)
            {                
                flpVessels.Show();
                
                CheckBox ckb = (CheckBox)(flpVessels.Controls[0]);
                ckb.Checked = true;
                ckb_Click(ckb, null);
            }           
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public void LoadEmployee(DataRow row)
        {
            //load_crew_from_row(RefWeek, row);
        }


        public void LoadStart()
        {
            load_data();
        }


        public void LoadReady()
        {
            load_crew_from_file(RefWeek);
            load_shift();

            update_timebook();

            //load_totals();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void frmSched_Load(object sender, EventArgs e)
        {
            //load_data();

            tlpCrew.AutoScroll = true;

            tlpCrew.RowStyles.Clear();
            tlpCrew.ColumnStyles.Clear();
            tlpCrew.RowCount = 0;
            tlpCrew.ColumnCount = 0;

            
            //load_crew_from_row(RefWeek, row);
            //load_crew_from_file(RefWeek);
            //load_shift();

            //load_totals();

            DataTable dtToff = dacToff.GetDS().Tables[0];
            cbxLegend.DataSource = dtToff;
            cbxLegend.ValueMember = "ToffKey";
            cbxLegend.DisplayMember = "ToffDesc";

            // Kludge : have to fix the auto select problem
            cbxLegend.SelectedIndex = -1;
            cbxLegend.SelectedIndexChanged += new EventHandler(cbxLegend_SelectedIndexChanged);

            //update_timebook();

            this.Text = string.Format("Schedule for Weeks [{0} to {1}]",
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

            update_timebook();

            this.Text = string.Format("Schedule for Weeks [{0} to {1}]",
                RefWeek.ToShortDateString(), RefWeek.AddDays(7).ToShortDateString());
        }

        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void tbxShift_TextChanged(object sender, EventArgs e)
        {
            // Mark the crew

            TextBox tbx = (TextBox)sender;

            //Boatcrew b = (Boatcrew)(tbx.Tag);
            //string name = b.boat.ToString() + b.idx.ToString();            
            //int shift = 0;

            Boat b = _nboats.Get(tbx.Text);
            for (int i = 0; i < tlpCrew.RowCount; i++)
            {
                ucStat uc = (ucStat)tlpCrew.GetControlFromPosition(0, i);

                //uc.RefreshShift((Boatcrew)(tbx.Tag), shift);
                uc.RefreshShift(b);
            }
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void btnMinus_Click(object sender, EventArgs e)
        {
            foreach (CheckBox ckb in flpVessels.Controls)            
                if (ckb.Checked)  flpVessels.Controls.Remove(ckb);            
        }


        private void btnPlus_Click(object sender, EventArgs e)
        {
            foreach (CheckBox ckb in flpVessels.Controls)
                ckb.Checked = false;

            foreach (ucVessel u in tlpVessel.Controls)
                u.ResetVessel();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void update_vessels()
        {
            //foreach (var v in _oboats)
            //{
            //    List<Boatcrew> blst = (List<Boatcrew>)(v.Value);

            //    int shift = 0;
            //    foreach (Boatcrew b in blst)
            //        shift |= b.shiftid;

            //    for (int i = 0; i < tlpCrew.RowCount; i++)
            //    {
            //        ucStat uc = (ucStat)tlpCrew.GetControlFromPosition(0, i);

            //        uc.RefreshShift(blst[0], shift);
            //    }
            //}

            foreach (var v in _nboats._boats)
            {
                //List<Boat> blst = (List<Boat>)(v.Value);

                //int shift = 0;
                //foreach (Boat b in blst)
                //    shift |= b.shiftid;

                Boat b = _nboats.Get(v.Key);

                for (int i = 0; i < tlpCrew.RowCount; i++)
                {
                    ucStat uc = (ucStat)tlpCrew.GetControlFromPosition(0, i);

                    //uc.RefreshShift(blst[0], shift);
                    uc.RefreshShift(b);
                }
            }
            
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void tbxBoat_TextChanged(object sender, EventArgs e)
        {
            TextBox tbx = (TextBox)sender;
            
            update_vessels();            

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
    }    
}
