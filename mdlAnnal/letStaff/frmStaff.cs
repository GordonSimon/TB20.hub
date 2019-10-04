using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Globalization;
using System.Threading;

using mdlAnnal;


namespace letStaff
{    
    public partial class frmStaff : Form, CuePaycodeDialog
    {
        const string DBO = "dbo.";


        public DateTime LogDate { get; set; }

        public DateTime RefWeek { get; set; }
        public bool SetAsCurrent { get; set; }  

        private TableLayoutPanel tlpNote = new TableLayoutPanel();

        private DataTable _dt_tb { get; set; }
        private DataTable _dt_employee { get; set; }

        private bool _show_all { get; set; }

        private ucPaycodes _uc_paycode { get; set; }

        private Dictionary<string, ucSalary> _employee { get; set; }


        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        public void Show(string emp_id, DateTime refdate, string defpaycode)
        {
            this.Enabled = false;
            this.UseWaitCursor = true;
            
            if (_uc_paycode != null)  _uc_paycode.ShowCRUD(emp_id, refdate, defpaycode);
            
            ucSalary uc = _employee[emp_id];

            uc.QuickFresh(refdate, _uc_paycode.TotalRegHours, _uc_paycode.TotalOver, _uc_paycode.TotalOver1, _uc_paycode.TotalXtrHours);
            Application.DoEvents();

            //backgroundWorker1.RunWorkerAsync();

            _dt_tb = dacTimebook.GetDTCrew(DBO);
            //_uc_paycode.LoadDatatable(_dt_tb);

            this.Enabled = true;
            this.UseWaitCursor = false;
        }

        
        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        //public frmStaff(DateTime user_day)
        public frmStaff()
        {
            DateTime user_day = DateTime.Now;

            InitializeComponent();

            _employee = new Dictionary<string, ucSalary>();
            _uc_paycode = new ucPaycodes();
            
            //tlpStaff.Hide();

            this.chkShow.Checked = false;
            this.cmdSave.Visible = false;  // don't use this save button any more

            tlpNote.Name = "tlpNote";
            tlpNote.Size = new Size(300, 200);
            tlpNote.Location = new Point(200, 200);
            tlpNote.BackColor = Color.Yellow;
            tlpNote.AutoSize = true;
            this.Controls.Add(tlpNote);

            tlpStaff.AutoScroll = true;
            tlpStaff.RowStyles.Clear();
            tlpStaff.ColumnStyles.Clear();
            tlpStaff.RowCount = 0;
            tlpStaff.ColumnCount = 0;

            RefWeek = set_date(user_day);

            draw_head(RefWeek.AddDays(7), DateTime.Now, SetAsCurrent, lblWeek2,
                tbxMon2, tbxTue2, tbxWed2, tbxThu2, tbxFri2, tbxSat2, tbxSun2);

            draw_head(RefWeek, DateTime.Now, SetAsCurrent, lblWeek,
                tbxMon, tbxTue, tbxWed, tbxThu, tbxFri, tbxSat, tbxSun);

            //Application.DoEvents();
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
        
        //private void draw_week(DateTime start_week, DateTime cal_week, bool current)
        //{
        //    int woy = week_of_year(start_week);

        //    bool is_future_week = start_week.Ticks > cal_week.Ticks;
        //    bool is_current_week = week_of_year(start_week) == week_of_year(cal_week);


        //    if (is_current_week)
        //        this.BackColor = Color.LightGreen;
        //    else
        //        this.BackColor = SystemColors.Control;
        //}


        //private void tag_dates(TextBox tbxDH, DateTime start_week, int offset)
        //{
        //    tbxDH.Tag = start_week.AddDays(offset);
        //}

       
        private void draw_head(DateTime start_week, DateTime cal_week, bool current, Label lblWk,
            TextBox tbxM, TextBox tbxT, TextBox tbxW, TextBox tbxTh, TextBox tbxF, TextBox tbxSa, TextBox tbxSu)
        {
            int woy = week_of_year(start_week);

            bool is_future_week = start_week.Ticks > cal_week.Ticks;
            bool is_current_week = week_of_year(start_week) == week_of_year(cal_week);

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
       
        private void update_timebook(bool show_all)
        {
            if (RefWeek == null)  return;

            //DataSet ds = dacTimebook.GetDS(RefWeek, 14);
            //foreach (DataRow row in ds.Tables[0].Rows)

            foreach (DataRow row in _dt_tb.Rows)
            {
                var bookdate_ = row["Bookdate"];
                if (bookdate_ == DBNull.Value) continue;

                DateTime bookdate = (DateTime)bookdate_;

                //if (bookdate.Date < RefWeek.Date) continue;
                //if (bookdate.Date >= RefWeek.AddDays(14).Date) continue;
                if (bookdate.Date.CompareTo(RefWeek.Date) == -1) continue;
                if (bookdate.Date.CompareTo(RefWeek.AddDays(14).Date) == 1) continue;
                                

                //ucStat uc = (ucStat)tlpCrew.GetControlFromPosition(0, 0);
                //if (!_crew.ContainsKey(row["EmpId"].ToString()))
                //    load_crew_from_row(RefWeek, row);

                try
                {
                    //if (row["EmpID"].Equals("FS0623")) MessageBox.Show("A");

                    if (!show_all && !_employee.ContainsKey(row["EmpID"].ToString())) continue;

                    // GS180907 - Debug Stop
                    //string eid = row["EmpID"].ToString();
                    //if (eid.Equals("K1")) MessageBox.Show("Here");

                    ucSalary uc = _employee[row["EmpID"].ToString()];

                    string notebug = null;  // can't use row["LogNote"].ToString()
                    if (!row["LogNote"].Equals(DBNull.Value))
                        notebug = row["LogNote"].ToString();

                    Decimal over1 = 0.0M;
                    if (!row["LogOver1"].Equals(DBNull.Value)) over1 = (Decimal)row["LogOver1"];

                    // GS180907 - Handle Incomplete Employee Data
                    if (row["DefPayCode"].Equals(DBNull.Value))
                    {
                        string msg = string.Format("{0} : Employee : [{1}:{2}] {3}",
                            "WARNING - This employee is not properly setup. The DefPayCode is not valid",                            
                            row["EmpID"].ToString(),
                            row["EmpName"].ToString(),
                            bookdate                            
                            );

                        MessageBox.Show(msg, System.Reflection.MethodBase.GetCurrentMethod().ToString());

                        continue;
                    }

                    string paycode = (string)row["DefPayCode"];
                    if (!row["PayCode"].Equals(DBNull.Value)) paycode = (string)row["PayCode"];

                    string toff = string.Empty;
                    if (!row["ToffCode"].Equals(DBNull.Value)) toff = (string)row["ToffCode"];

                    string boat = string.Empty;
                    if (!row["LogVessel"].Equals(DBNull.Value)) boat = (string)row["LogVessel"];

                    uc.RefWeek = RefWeek;
                    uc.RefreshDay(bookdate, toff,
                        Convert.ToDecimal(row["LogHours"]),
                        Convert.ToDecimal(row["LogOver"]),
                        over1,
                        boat,
                        Convert.ToInt32(row["LogShift"]), notebug, paycode);
                }
                catch (Exception ex)
                {
                    string msg = string.Format("Error ({0}) : Employee : [{1}:{2}] {3} => TOFF Code [{4}]",
                        ex.Message,                        
                        row["EmpID"].ToString(),
                        row["EmpName"].ToString(),
                        bookdate,
                        row["ToffCode"].ToString());

                    MessageBox.Show(msg, System.Reflection.MethodBase.GetCurrentMethod().ToString());
                }
                    
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
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void reset_week(DateTime start_of_week)
        {
            foreach (var c in _employee)
            {
                ucSalary uc = c.Value;

                if (!uc.LoadIt)
                    uc.ShowIt = false;

                uc.ResetWeek(start_of_week);
            }
        }


        private void refresh_data(DateTime start_of_week, bool show_all)
        {
            tlpStaff.Controls.Clear();
            tlpStaff.RowCount = 0;            

            //_ds_crew = dacTimebook.GetDS(RefWeek, 14);
            // _dt_crew is already loaded

            //load_shift();

            string where = string.Format("BookDate >= '{0}' and BookDate <= '{1}'",
                start_of_week.ToShortDateString(), start_of_week.AddDays(14).ToShortDateString());

            DataView v = new DataView(_dt_tb);
            //v.Table = _ds_crew.Tables[0];
            //v.Table = _dt_crew;
            v.RowFilter = where;

            DataTable view_data = v.ToTable(true, new string[] { "EmpId", "EmpName", "DefPayCode" });

            foreach (DataRow row in view_data.Rows)
            {
                ucSalary uc;

                if (_employee.ContainsKey(row["EmpID"].ToString()))
                {
                    uc = _employee[row["EmpID"].ToString()];

                    if (show_all)
                        uc.ShowIt = true;
                    continue;                
                }

                if (show_all)
                {
                    string paycode = "Hourly";
                    if (!row["DefPayCode"].Equals(DBNull.Value)) paycode = (string)row["DefPayCode"];
                    uc = new ucSalary(start_of_week, row["EmpName"].ToString(), row["EmpID"].ToString(), paycode, false);           
                    _employee.Add(row["EmpID"].ToString(), uc);
                }
                

                //tlpCrew.Controls.Add(uc);
                //tlpCrew.RowCount += 1;
            }


            foreach (var c in _employee)
            {
                ucSalary uc = c.Value;

                if (uc.LoadIt)
                {
                    tlpStaff.Controls.Add(uc);
                    tlpStaff.RowCount += 1;
                }
            }


            foreach (var c in _employee)
            {
                ucSalary uc = c.Value;

                if (uc.ShowIt && ! uc.LoadIt)
                {
                    tlpStaff.Controls.Add(uc);
                    tlpStaff.RowCount += 1;
                }
            }
        }


        private void load_employee_from_row(DateTime start_of_week, DataRow row)
        {
            //ucSalary uc = new ucSalary(start_of_week, row["EmpName"].ToString(),row["EmpID"].ToString(), true);
            //_employee.Add(row["EmpID"].ToString(), uc);
            //tlpStaff.Controls.Add(uc);
            //tlpStaff.RowCount += 1;
        }


        private void load_employee_from_file(DateTime start_of_week)
        {
            string where = string.Format("BookDate >= '{0}' and BookDate <= '{1}'",
               start_of_week.ToShortDateString(), start_of_week.AddDays(14).ToShortDateString());

            DataView v = new DataView(_dt_tb);
            v.RowFilter = where;

            DataTable view_data = v.ToTable(true, new string[] { "EmpId", "EmpName", "DefPayCode" });  // select distinct
           
            foreach (DataRow row in view_data.Rows)
            {
                if (_employee.ContainsKey(row["EmpID"].ToString())) continue;

                string paycode = "Hourly";
                if (!row["DefPayCode"].Equals(DBNull.Value)) paycode = (string)row["DefPayCode"];
                ucSalary uc = new ucSalary(start_of_week, row["EmpName"].ToString(), row["EmpID"].ToString(), paycode, true);
                
                _employee.Add(row["EmpID"].ToString(), uc);
                //tlpStaff.Controls.Add(uc);
                //tlpStaff.RowCount += 1;
            }
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public void LoadEmployee(DataRow row)
        {
            load_employee_from_row(RefWeek, row);
        }


        private void load_ready()
        {
            load_employee_from_file(RefWeek);

            //this.SuspendLayout();
            //tlpStaff.SuspendLayout();
            foreach (ucSalary uc in _employee.Values)
            {
                tlpStaff.Controls.Add(uc);
                tlpStaff.RowCount += 1;
            }
            //tlpStaff.ResumeLayout(false);
            //this.ResumeLayout(true);

            reset_week(RefWeek);
            refresh_data(RefWeek, _show_all);
            update_timebook(_show_all);

            //load_totals();
        }


        public void InitLoadReady()
        {
            _dt_tb = dacTimebook.GetDT(DBO, "Office");

            //_dt_staff = dacTimebook.GetDTCrew(DBO);

            load_ready();
        }


        private void load_work_from_dac_add_pk(string where = "")
        {
            if (where.Equals(string.Empty))
                _dt_tb = dacTimebook.GetDTCrew(DBO);
            else
                _dt_tb = dacTimebook.GetDT(DBO, where);

            _dt_tb.PrimaryKey = new DataColumn[] { _dt_tb.Columns["ID"] };
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void frmStaff_Load(object sender, EventArgs e)
        {
            //load_data();

            
            //tlpCrew.AutoScroll = true;

            //tlpCrew.RowStyles.Clear();
            //tlpCrew.ColumnStyles.Clear();
            //tlpCrew.RowCount = 0;
            //tlpCrew.ColumnCount = 0;

            
            //load_crew_from_row(RefWeek, row);
            //load_crew_from_file(RefWeek);
            //load_shift();

            //load_totals();


            //update_timebook();

            InitLoadReady();

            tlpStaff.Show();

            // GS180907 - Modify Version using GapLib
            //this.Text = string.Format("PayCodes for [{0} to {1}] V1.18.2.0",
            this.Text = string.Format("PayCodes for [{0} to {1}] V{2}",
                RefWeek.ToShortDateString(), RefWeek.AddDays(7).ToShortDateString(), GAPP.GapLib.AssemblyVersion);

        }


        private void dtpLogDate_CloseUp(object sender, EventArgs e)
        {
            DateTime user_day = ((DateTimePicker)sender).Value;

            RefWeek = set_date(user_day);

            //DataSet ds = dacTimebook.GetDS(RefWeek, 14);
            //_dt_crew = ds.Tables[0];
            //_dt_crew = dacTimebook.GetDT(DBO);

            reset_week(RefWeek);
            refresh_data(RefWeek, _show_all);

            draw_head(RefWeek.AddDays(7), DateTime.Now, SetAsCurrent, lblWeek2,
                tbxMon2, tbxTue2, tbxWed2, tbxThu2, tbxFri2, tbxSat2, tbxSun2);

            draw_head(RefWeek, DateTime.Now, SetAsCurrent, lblWeek,
                tbxMon, tbxTue, tbxWed, tbxThu, tbxFri, tbxSat, tbxSun);

            update_timebook(_show_all);  // after date change on form

            this.Text = string.Format("Schedule for Weeks [{0} to {1}]",
                RefWeek.ToShortDateString(), RefWeek.AddDays(7).ToShortDateString());
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void tbxShift_TextChanged(object sender, EventArgs e)
        {
            //// Mark the crew

            //TextBox tbx = (TextBox)sender;

            ////Boatcrew b = (Boatcrew)(tbx.Tag);
            ////string name = b.boat.ToString() + b.idx.ToString();            
            ////int shift = 0;

            //Boat b = _nboats.Get(tbx.Text);
            //for (int i = 0; i < tlpCrew.RowCount; i++)
            //{
            //    ucSalary uc = (ucSalary)tlpCrew.GetControlFromPosition(0, i);

            //    //uc.RefreshShift((Boatcrew)(tbx.Tag), shift);
            //    uc.RefreshShift(b);
            //}
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void update_vessels()
        {
            ////foreach (var v in _oboats)
            ////{
            ////    List<Boatcrew> blst = (List<Boatcrew>)(v.Value);

            ////    int shift = 0;
            ////    foreach (Boatcrew b in blst)
            ////        shift |= b.shiftid;

            ////    for (int i = 0; i < tlpCrew.RowCount; i++)
            ////    {
            ////        ucStat uc = (ucStat)tlpCrew.GetControlFromPosition(0, i);

            ////        uc.RefreshShift(blst[0], shift);
            ////    }
            ////}

            //foreach (var v in _nboats._boats)
            //{
            //    //List<Boat> blst = (List<Boat>)(v.Value);

            //    //int shift = 0;
            //    //foreach (Boat b in blst)
            //    //    shift |= b.shiftid;

            //    Boat b = _nboats.Get(v.Key);

            //    for (int i = 0; i < tlpCrew.RowCount; i++)
            //    {
            //        ucSalary uc = (ucSalary)tlpCrew.GetControlFromPosition(0, i);

            //        //uc.RefreshShift(blst[0], shift);
            //        uc.RefreshShift(b);
            //    }
            //}
            
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

        private void cmdCrew_Click(object sender, EventArgs e)
        {
            Button cmd = (Button)sender;

            tlpStaff.Controls.Clear();
            _employee.Clear();
            load_ready();

            //chkHourly.Checked = false;
            //chkHourly.Enabled = false;

            gbxStaff.Text = "Crew";
            cmd.Enabled = false;
        }


        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void do_delete()
        {
            //bool flag_save = false;
            ////DataSet ds = dacTimebook.GetDS(RefWeek, 14);

            //for (int i = 0; i < tlpCrew.RowCount; i++)
            //{
            //    ucSalary uc = (ucSalary)tlpCrew.GetControlFromPosition(0, i);

            //    if (uc.EmpChk)
            //    {
            //        if (uc.IsDirty()) flag_save = true;
            //        continue;
            //    }

            //    if (! uc.IsDelete()) continue;

               
            //    //DataRow row;
            //    DateTime next_date;

            //    for (int day = 0; day < 14; day++)
            //    {
            //        next_date = RefWeek.Date.AddDays(day);

            //        dacTimebook.FindDel(new object[] { next_date, uc.EmpID, 0 });
            //    }

            //    uc.DeleteReset();
            //}

            //dacTimebook.DeleteData();
            ////dacCache.PutTimebook();

            //cmdSave.Text = "Save";
            //cmdSave.Visible = flag_save;

        }

        private void do_save()
        {
            //DataSet ds = dacTimebook.GetDS(RefWeek, 14);

            for (int i = 0; i < tlpStaff.RowCount; i++)
            {
                ucSalary uc = (ucSalary)tlpStaff.GetControlFromPosition(0, i);

                if (!uc.EmpChk) continue;
                if (uc.IsDelete()) continue;
                if (!uc.IsDirty()) continue;

                MessageBox.Show(uc.EmpName, System.Reflection.MethodBase.GetCurrentMethod().ToString());

                //RCD rcd = uc.RcdStat;
                //for (int j = 0; j <= 13; j++)
                //    if (rcd.toff[j] != null)  MessageBox.Show(rcd.toff[j].ToString());


                DataRow row;
                DateTime next_date;

                for (int day = 0; day < 14; day++)
                {
                    next_date = RefWeek.Date.AddDays(day);

                    string toff = uc.EmpToff[day];
                    //string vessel = uc.EmpVessel[day];

                    string paycode = uc.DefPayCode;

                    string empname = uc.EmpName;

                    Decimal hour = uc.EmpHour[day];
                    Decimal over = uc.EmpOver[day];
                    Decimal over1 = uc.EmpOver1[day];

                    int shift = 0;
                    string lognote = uc.EmpNote[day];
                    
                    MessageBox.Show(string.Format("nd{0} e{1}", next_date, uc.EmpID), System.Reflection.MethodBase.GetCurrentMethod().ToString());
                    //dacTimebook.vwUpdTimebook(DBO, next_date, uc.EmpID, shift, toff, null, hour, over, over1, null, paycode);


                    //row = ds.Tables[0].NewRow();
                    //row["EmpName"] = uc.EmpName;

                    //if (toff != null && toff.Length == 0)
                    //    row["ToffCode"] = null;
                    //else
                    //    row["ToffCode"] = toff;

                    //row["LogHours"] = uc.EmpHour[day];
                    //row["LogOver"] = uc.EmpOver[day];

                    //if (vessel != null && vessel.Length == 0)
                    //    row["LogVessel"] = null;
                    //else
                    //    row["LogVessel"] = uc.EmpVessel[day];

                    //row["LogShift"] = 0;

                    //row["LogNote"] = null;
                    //if (uc.EmpNote[day] != null && uc.EmpNote[day].Length > 0)
                    //    row["LogNote"] = uc.EmpNote[day];

                    //dacTimebook.FindAdd(new object[] { next_date, uc.EmpID, 0 }, row);

                    //uc.SaveReset();
                }
            }

            //dacTimebook.SaveData();
            //dacCache.PutTimebook();
            cmdSave.Visible = false;



            //DataSet ds = dacTimebook.GetDS(RefWeek, 14);

            //for (int i = 0; i < tlpCrew.RowCount; i++)
            //{
            //    ucSalary uc = (ucSalary)tlpCrew.GetControlFromPosition(0, i);

            //    if (!uc.EmpChk) continue;
            //    if (uc.IsDelete()) continue;
            //    if (!uc.IsDirty()) continue;

            //    //MessageBox.Show(uc.EmpName);

            //    //RCD rcd = uc.RcdStat;
            //    //for (int j = 0; j <= 13; j++)
            //    //    if (rcd.toff[j] != null)  MessageBox.Show(rcd.toff[j].ToString());


            //    DataRow row;
            //    DateTime next_date;

            //    for (int day = 0; day < 14; day++)
            //    {
            //        next_date = RefWeek.Date.AddDays(day);

            //        string toff = uc.EmpToff[day];
            //        string vessel = uc.EmpVessel[day];

            //        row = ds.Tables[0].NewRow();
            //        row["EmpName"] = uc.EmpName;

            //        if (toff != null && toff.Length == 0)                        
            //            row["ToffCode"] = null;
            //        else
            //            row["ToffCode"] = toff;

            //        row["LogHours"] = uc.EmpHour[day];
            //        row["LogOver"] = uc.EmpOver[day];

            //        if (vessel != null && vessel.Length == 0)
            //            row["LogVessel"] = null;
            //        else
            //            row["LogVessel"] = uc.EmpVessel[day];

            //        row["LogShift"] = 0;

            //        row["LogNote"] = null;
            //        if (uc.EmpNote[day] != null && uc.EmpNote[day].Length > 0)
            //            row["LogNote"] = uc.EmpNote[day];
                    
            //        dacTimebook.FindAdd(new object[] { next_date, uc.EmpID, 0 }, row);

            //        uc.SaveReset();
            //    }
            //}
            
            //dacTimebook.SaveData();
            ////dacCache.PutTimebook();
            //cmdSave.Visible = false;

        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.Text.Equals("Save"))
                do_save();
            else
                do_delete();
        }


        private void chkShow_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            _show_all = cb.Checked;

            if (_employee != null)
            {                
                reset_week(RefWeek);
                refresh_data(RefWeek, _show_all);
                update_timebook(_show_all);
            }
            
        }


        private void chkHourly_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            foreach (var c in _employee)
            {
                ucSalary uc = c.Value;
                uc.DayUnit = cb.Checked; ;
                uc.RedrawDays();
            }            
        }

        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //_dt_employee = dacEmployee.GetDT(DBO);
            //_dt_employee = dacEmployee.GetDT(DBO, "Office");

           
            _dt_tb = dacTimebook.GetDTCrew(DBO);
            _uc_paycode.LoadDatatable(_dt_tb);
        }


        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //chkShow.Show();
            //cmdCrew.Show();
            if (this.InvokeRequired)
                this.BeginInvoke(new Action(cmdCrew.Show));                
            else
                cmdCrew.Show();
        }

        private void frmStaff_Shown(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }
    }    
}
