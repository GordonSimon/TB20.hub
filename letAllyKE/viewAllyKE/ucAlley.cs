using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Diagnostics;

using mdlAllyKE;
using viewLogKE;


namespace viewAllyKE
{
    public partial class ucAlley : UserControl
    {
        static private DateTime _start_date = DateTime.Now.Date;
        //static private DateTime _start_date = new DateTime(2014, 01, 01);

        const int defHeadCount = 4;

        public DateTime LogDay;
        private DateTime _ref_week { get; set; }


        private int HeadCount = defHeadCount;        

        private ucYear _uc_year;
        private List<ucHead> _uc_heads = new List<ucHead>();

        private int _current_week_offset = 1;

        

        private int _org_flpN_w { get; set; }
        private int _org_flpN_h { get; set; }
        // private DataSet _ds_active { get; set; }

        private ComboBox _cbx { get; set; }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public static string AssemblyVersion
        {
            get
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                string version = fvi.FileVersion;

                return version;
            }
        }

        
        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public void RefreshAlley()
        {
            refresh_alley(0);
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/      
                       
        public ucAlley()
        {
            InitializeComponent();

            _org_flpN_w = flpN.Size.Width;
            _org_flpN_h = flpN.Size.Height;

            lblVersion.Text = AssemblyVersion;
        }


        private int determine_headcount()
        {            
            int w = tlpHead.Size.Width;
            int h = tlpHead.Size.Height;
            int x = tlpHead.Location.X;
            
  
            Form frm = this.ParentForm;
            Rectangle rect = Screen.GetWorkingArea(frm);

            w += 260;
            if (rect.Width > w + x) HeadCount = 5;
            w -= 270;
            if (rect.Width < w + x) HeadCount = 3;            

            //tlpHead.Size = new Size(w, h);                
            return HeadCount;
        }


        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        private void ucAlley_Load(object sender, EventArgs e)
        {
            this.Parent.Hide();

            pnlOnline.BackColor = Color.Green;
            
            _ref_week = week_starter();
            //LogDay = DateTime.Now.Date;
            LogDay = _start_date;            

            show_gang();
            show_view();
            load_active();

            HeadCount = determine_headcount();

            tlpHead.SuspendLayout();
            tlpHead.AutoScroll = true;
            tlpHead.CellBorderStyle = TableLayoutPanelCellBorderStyle.OutsetDouble;

            tlpHead.RowStyles.Clear();
            tlpHead.ColumnStyles.Clear();
            tlpHead.AutoSize = true;
            //tlpAlley.AutoSizeMode = AutoSizeMode.GrowAndShrink;
       
            tlpHead.RowCount = 1;
            tlpHead.ColumnCount = HeadCount + 1;
            
            //tlpHead.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            //tlpHead.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            tlpHead.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, ucYear.uWidth + 6));           
            tlpHead.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, ucHead.uWidth + 6));
            tlpHead.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, ucHead.uWidth + 6));
            tlpHead.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, ucHead.uWidth + 6));
            if (HeadCount > 4)
                tlpHead.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, ucHead.uWidth + 6));


            _uc_year = new ucYear(_start_date, _ref_week.AddDays(-7), HeadCount * 7);

            //tlpAlley.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, ucYear.uWidth));
            //tlpAlley.RowStyles.Add(new RowStyle(SizeType.Absolute, ucYear.uHeight));
            
            tlpHead.Controls.Add(_uc_year, 0, 0);
            

            //tlpAlley.Controls.Add(new ucMonth(), 0, 1);
            //tlpAlley.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, ucMonth.uWidth));
            //tlpAlley.RowStyles.Add(new RowStyle(SizeType.Absolute, ucMonth.uHeight));

            int show_week_offset = (_current_week_offset * 7);
            _uc_heads.Add(new ucHead(_ref_week.AddDays(0 - show_week_offset), (_current_week_offset == 0)));
            _uc_heads.Add(new ucHead(_ref_week.AddDays(7 - show_week_offset), (_current_week_offset == 1)));
            _uc_heads.Add(new ucHead(_ref_week.AddDays(14 - show_week_offset), (_current_week_offset == 2)));
            _uc_heads.Add(new ucHead(_ref_week.AddDays(21 - show_week_offset), (_current_week_offset == 3)));
            if (HeadCount > 4)
                _uc_heads.Add(new ucHead(_ref_week.AddDays(28 - show_week_offset), (_current_week_offset == 4)));

            //tlpAlley.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, ucDays.uWidth));
            //tlpAlley.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, ucHead.uWidth));
            
            tlpHead.Controls.Add(_uc_heads[0], 1, 0);
            tlpHead.Controls.Add(_uc_heads[1], 2, 0);
            tlpHead.Controls.Add(_uc_heads[2], 3, 0);
            tlpHead.Controls.Add(_uc_heads[3], 4, 0);
            if (HeadCount > 4)
                tlpHead.Controls.Add(_uc_heads[4], 5, 0);

            //tlpAlley.Controls.Add(new ucDays(), 1, 1);
            //tlpAlley.Controls.Add(new ucDays(), 2, 1);
            //tlpAlley.Controls.Add(new ucDays(), 3, 1);
            //tlpAlley.Controls.Add(new ucDays(), 4, 1);
            //tlpAlley.Controls.Add(new ucDays(), 4, 1);
            //*tlpHead.PerformLayout();
            tlpHead.ResumeLayout(false);
            
            tlpAlley.SuspendLayout();
            tlpAlley.AutoScroll = true;
            tlpAlley.CellBorderStyle = TableLayoutPanelCellBorderStyle.OutsetDouble;
            
            tlpAlley.RowStyles.Clear();
            tlpAlley.ColumnStyles.Clear();
            tlpAlley.AutoSize = false;
            //tlpAlley.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpAlley.RowCount = 1;
            //tlpAlley.ColumnCount = 1;
            tlpAlley.ColumnCount = HeadCount + 1;
            tlpAlley.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            //t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            tlpAlley.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, ucEmp.uWidth+6));           
            tlpAlley.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, ucWeek.uWidth+6));
            tlpAlley.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, ucWeek.uWidth+6));
            tlpAlley.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, ucWeek.uWidth+6));
            if (HeadCount > 4)
                tlpAlley.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, ucWeek.uWidth+6));
            

            //_uc_year = new ucYear(_start_date, _ref_week.AddDays(-7), HeadCount * 7);
            ////tlpAlley.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, ucYear.uWidth));
            ////tlpAlley.RowStyles.Add(new RowStyle(SizeType.Absolute, ucYear.uHeight));
            //tlpAlley.Controls.Add(_uc_year, 0, 0);

            ////tlpAlley.Controls.Add(new ucMonth(), 0, 1);
            ////tlpAlley.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, ucMonth.uWidth));
            ////tlpAlley.RowStyles.Add(new RowStyle(SizeType.Absolute, ucMonth.uHeight));

            //int show_week_offset = (_current_week_offset * 7);
            //_uc_heads.Add(new ucHead(_ref_week.AddDays(0 - show_week_offset), (_current_week_offset == 0)));
            //_uc_heads.Add(new ucHead(_ref_week.AddDays(7 - show_week_offset), (_current_week_offset == 1)));
            //_uc_heads.Add(new ucHead(_ref_week.AddDays(14 - show_week_offset), (_current_week_offset == 2)));
            //_uc_heads.Add(new ucHead(_ref_week.AddDays(21 - show_week_offset), (_current_week_offset == 3)));
            //if (HeadCount > 4)
            //    _uc_heads.Add(new ucHead(_ref_week.AddDays(28 - show_week_offset), (_current_week_offset == 4)));

            ////tlpAlley.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, ucDays.uWidth));
            ////tlpAlley.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, ucHead.uWidth));
            //tlpAlley.Controls.Add(_uc_heads[0], 1, 0);
            ////tlpAlley.Controls.Add(new ucDays(), 1, 1);
            //tlpAlley.Controls.Add(_uc_heads[1], 2, 0);
            ////tlpAlley.Controls.Add(new ucDays(), 2, 1);
            //tlpAlley.Controls.Add(_uc_heads[2], 3, 0);
            ////tlpAlley.Controls.Add(new ucDays(), 3, 1);
            //tlpAlley.Controls.Add(_uc_heads[3], 4, 0);
            ////tlpAlley.Controls.Add(new ucDays(), 4, 1);
            //if (HeadCount > 4)
            //    tlpAlley.Controls.Add(_uc_heads[4], 5, 0);
            ////tlpAlley.Controls.Add(new ucDays(), 4, 1);

            //refresh_gang();

            tlpAlley.ResumeLayout(false);
            //tlpAlley.PerformLayout();

            //load_active();

            //do_query("Working");
            do_query("Profile");
            //mode1();
            cmdBoats.Text = "Hours";

            this.Parent.Show();

            timer1.Start();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void old_gang()
        {
            Button btn = new Button();

            btn = new Button();
            btn.Size = new Size(100, 23);
            btn.Text = "Profile";
            btn.Click += new EventHandler(btn_Click);
            flpView.Controls.Add(btn);

            btn = new Button();
            btn.Size = new Size(100, 23);
            btn.Text = "Working";
            btn.Click += new EventHandler(btn_Click);
            flpGang.Controls.Add(btn);

            btn = new Button();
            btn.Size = new Size(100, 23);
            btn.Text = "FullTime";
            btn.Click += new EventHandler(btn_Click);
            flpView.Controls.Add(btn);

            btn = new Button();
            btn.Size = new Size(100, 23);
            btn.Text = "PartTime";
            btn.Click += new EventHandler(btn_Click);
            flpView.Controls.Add(btn);

            btn = new Button();
            btn.Size = new Size(100, 23);
            btn.Text = "Casual";
            btn.Click += new EventHandler(btn_Click);
            flpView.Controls.Add(btn);

            btn = new Button();
            btn.Size = new Size(100, 23);
            btn.Text = "Office";
            btn.Click += new EventHandler(btn_Click);
            flpGang.Controls.Add(btn);

            btn = new Button();
            btn.Size = new Size(100, 23);
            btn.Text = "Master";
            btn.Click += new EventHandler(btn_Click);
            flpView.Controls.Add(btn);

            btn = new Button();
            btn.Size = new Size(100, 23);
            btn.Text = "Deckhand";
            btn.Click += new EventHandler(btn_Click);
            flpView.Controls.Add(btn);

            btn = new Button();
            btn.Size = new Size(100, 23);
            btn.Text = "All";
            btn.Click += new EventHandler(btn_Click);
            flpGang.Controls.Add(btn);

            btn = new Button();
            btn.Size = new Size(100, 23);
            btn.Text = "Vessel";
            btn.Click += new EventHandler(btn_Click);
            flpView.Controls.Add(btn);

            _cbx = new ComboBox();
            _cbx.Size = new Size(100, 23);
            _cbx.DataSource = dacCache.GetVessel();
            _cbx.DisplayMember = "Short";
            flpView.Controls.Add(_cbx);
        }


        private void show_gang()
        {
            Button btn = new Button();

            btn = new Button();
            btn.Size = new Size(100, 23);
            btn.Text = "Crew";
            btn.Click += new EventHandler(btn_Click);
            flpGang.Controls.Add(btn);

            btn = new Button();
            btn.Size = new Size(100, 23);
            btn.Text = "Office";
            btn.Click += new EventHandler(btn_Click);
            flpGang.Controls.Add(btn);

            btn = new Button();
            btn.Size = new Size(100, 23);
            btn.Text = "All";
            btn.Click += new EventHandler(btn_Click);
            flpGang.Controls.Add(btn);

            btn = new Button();
            btn.Size = new Size(100, 23);
            btn.Text = "Profile";
            btn.Click += new EventHandler(btn_Click);
            flpGang.Controls.Add(btn);

        }


        private void show_view()
        {
            Button btn = new Button();

            btn = new Button();
            btn.Size = new Size(100, 23);
            btn.Text = "FullTime";
            btn.Click += new EventHandler(btn_Click);
            flpView.Controls.Add(btn);

            btn = new Button();
            btn.Size = new Size(100, 23);
            btn.Text = "Archive";
            btn.Click += new EventHandler(btn_Click);
            flpView.Controls.Add(btn);

            btn = new Button();
            btn.Size = new Size(100, 23);
            btn.Text = "Casual";
            btn.Click += new EventHandler(btn_Click);
            flpView.Controls.Add(btn);


            btn = new Button();
            btn.Size = new Size(100, 23);
            btn.Text = "Master";
            btn.Click += new EventHandler(btn_Click);
            flpView.Controls.Add(btn);

            btn = new Button();
            btn.Size = new Size(100, 23);
            btn.Text = "Deckhand";
            btn.Click += new EventHandler(btn_Click);
            flpView.Controls.Add(btn);

            btn = new Button();
            btn.Size = new Size(100, 23);
            btn.Text = "Vessel";
            btn.Click += new EventHandler(btn_Click);
            flpView.Controls.Add(btn);

            _cbx = new ComboBox();
            _cbx.Size = new Size(100, 23);
            _cbx.DataSource = dacCache.GetVessel();
            _cbx.DisplayMember = "Short";
            flpView.Controls.Add(_cbx);
        }


        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        void do_profile()
        {
            qryGang.BuildProfile();
            
            ucList frmList = (ucList)(flpN.Controls[0]);
            frmList.Reload();
            
            tlpAlley.SuspendLayout();
            
            while (tlpAlley.RowCount > 1)
            {
                int row = tlpAlley.RowCount - 1;
                for (int i = 0; i < tlpAlley.ColumnCount; i++)
                {
                    Control c = tlpAlley.GetControlFromPosition(i, row);
                    tlpAlley.Controls.Remove(c);
                    c.Dispose();
                }
                //tlpAlley.RowStyles.RemoveAt(row);
                tlpAlley.RowCount--;
            }
            
            bool ok = refresh_gang();

            tlpAlley.ResumeLayout();
            //tlpAlley.ResumeLayout(false);
            //tlpAlley.PerformLayout();                       

            if (!ok)
                MessageBox.Show("No schedule !");            
        }


        void do_query(string qry_name)
        {

            ucList uc = (ucList)flpN.Controls[0];
            uc.SetGangName(qry_name);

            qryGang.Requery();
            qryGang.GetView(qry_name, _ref_week.AddDays(-7), HeadCount * 7);
            //qryGang.GetView(qry_name);

            ucList frmList = (ucList)(flpN.Controls[0]);
            frmList.Reload();

            tlpAlley.SuspendLayout();

            while (tlpAlley.RowCount > 1)
            {
                int row = tlpAlley.RowCount - 1;
                for (int i = 0; i < tlpAlley.ColumnCount; i++)
                {
                    Control c = tlpAlley.GetControlFromPosition(i, row);
                    tlpAlley.Controls.Remove(c);
                    c.Dispose();
                }
                //tlpAlley.RowStyles.RemoveAt(row);
                tlpAlley.RowCount--;
            }

            bool ok = refresh_gang();

            tlpAlley.ResumeLayout();
            //tlpAlley.ResumeLayout(false);
            //tlpAlley.PerformLayout();                       

            //if (!ok)
            //    MessageBox.Show("No schedule !");
            

        }

        void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            flpView.Controls.Clear();
            if (! btn.Text.Equals("Office"))  show_view();

            string qry_name = btn.Text;            
            if (qry_name.Equals("Vessel")) qry_name = _cbx.Text;

            //if (qry_name.Equals("Profile"))
            if (qry_name.Equals("Guest"))
                do_profile();
            else
                do_query(qry_name);

            //cmdBoats.Text = "Vessels";

        }


        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        private DateTime week_starter()
        { 
            //DateTime d = DateTime.Now;
            DateTime d = _start_date;

            int offset = d.DayOfWeek - DayOfWeek.Monday;
            offset = (offset == -1 ? 6 : offset);

            DateTime lastMonday = d.AddDays(-offset);

            return lastMonday;
        }


        void frmList_PostChange()
        {
            //tlpAlley.Controls.Clear();
            //refresh_employees();

            tlpAlley.SuspendLayout();

            while (tlpAlley.RowCount > 1)
            {
                int row = tlpAlley.RowCount - 1;
                for (int i = 0; i < tlpAlley.ColumnCount; i++)
                {
                    Control c = tlpAlley.GetControlFromPosition(i, row);
                    tlpAlley.Controls.Remove(c);
                    c.Dispose();
                }
                //tlpAlley.RowStyles.RemoveAt(row);
                tlpAlley.RowCount--;
            }

            bool ok = refresh_gang();

            tlpAlley.ResumeLayout();            
        }


        private void load_active()
        {
            //for (int i = 0; i < 4; i++)
            //{
            //    tlpAlley.Controls.Add(new ucEmp("Emp Name", "604-512-6200", "604-278-1472", false), 0, i + 1);
            //    tlpAlley.RowStyles.Add(new RowStyle(SizeType.Absolute, ucEmp.uHeight));
            //}

            //dacCache.GangID = "default";

            ucList uc = new ucList();

            uc.PostChange += new ucList.ChangeHandler(frmList_PostChange);

            //uc.Size = flpN.Size;
            flpN.Controls.Add(uc);
        }


        private bool refresh_gang()
        {
            //DateTime b = DateTime.UtcNow;
            try
            {
                //tlpAlley.SuspendLayout();

                //ucList frmList = (ucList)(flpN.Controls[0]);
                DataTable dt_gang = qryGang.GetDT();
                
                if (dt_gang == null) return false;
                if (dt_gang.Rows.Count <= 0) return false;

                int show_week_offset = (_current_week_offset * 7);
                int gang_row = 1;

                DataTable dtTb = qrySummary.GetView("TimeBook", _ref_week.AddDays(-7), 5*7, null, qryGang.GetDT());
                //DataTable dtTb = null;
                DataTable dtWrksum = qrySummary.GetView("Worksum", _ref_week);
                                
                                
                foreach (DataRow row in dt_gang.Rows)
                {
                    bool check = (bool)(row["Active"]);
                    if (!check) continue;

                    string duty = row["Duty"].ToString();
                    bool master = (row["Master"].Equals(DBNull.Value) ? false : (bool)row["Master"]);                    
                    if (master) duty = "Master";

                    DataRow rowWrksum = dtWrksum.Rows.Find(row["EmpId"]);
                    int day_count = 0;
                    decimal hour_sum = 0M;
                    decimal over_sum = 0M;
                    if (rowWrksum != null)
                    {
                        day_count = (int)rowWrksum["Days"];
                        hour_sum = (decimal)rowWrksum["Hours"];
                        over_sum = (decimal)rowWrksum["Overs"];
                    }
                  


                    //tlpAlley.RowStyles.Add(new RowStyle(SizeType.Absolute, ucEmp.uHeight));                
                    tlpAlley.Controls.Add(new ucEmp(
                        row["EmpID"].ToString(),
                        row["EmpName"].ToString(),
                        row["HomePhone"].ToString(),
                        //row["CellPhone"].ToString(), row["Duty"].ToString(), false), 0, emp_row);
                        row["CellPhone"].ToString(), duty, day_count, hour_sum, over_sum, _ref_week, false), 0, gang_row);


                    for (int i = 0; i < HeadCount; i++)
                        tlpAlley.Controls.Add(new ucWeek(row["EmpId"].ToString(), row["EmpName"].ToString(),
                                _ref_week.AddDays(i * 7 - show_week_offset),
                                (i == _current_week_offset), dtTb), i + 1, gang_row);

                    gang_row += 1;
                }
                 
                tlpAlley.RowCount = gang_row;

                //tlpAlley.ResumeLayout(false);
                //tlpAlley.PerformLayout();

                cmdBoats.Text = "Hours";
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error (refresh_gang GS15) : {0}", ex.Message));
                return false;
            }

            //DateTime e = DateTime.UtcNow;
            //MessageBox.Show((e - b).Milliseconds.ToString());

            return true;
         }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/          
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            //if (!_online)
            //{
            //    bool ok = (bool)this.ParentForm.Tag;

            //    if (ok) { refresh_employees(); pnlOnline.BackColor = Color.Green; }
            //}


            DateTime d = DateTime.Now;                        

            txtToday.Text = string.Format("{0} {1} [ {2} ] Day {3} of 365", d.DayOfWeek.ToString(),
                d.ToLongDateString(),
                d.ToLongTimeString(), d.DayOfYear.ToString());
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void refresh_alley(int offset)
        {
            _start_date = _start_date.AddDays(offset * 7);
            _uc_year.WeekRefresh(_start_date, _ref_week.AddDays(-7), HeadCount * 7);

            DataTable dtTb = qrySummary.GetView("TimeBook", _ref_week.AddDays(-7), 5 * 7, null, qryGang.GetDT());

            if (offset != 0)
                foreach (var h in _uc_heads)
                    h.WeekRefresh(offset);

            int crew_rows = tlpAlley.RowCount;
            for (int r = 1; r < crew_rows; r++)
                for (int c = 1; c <= HeadCount; c++)
                    ((ucWeek)(tlpAlley.GetControlFromPosition(c, r))).WeekRefresh(offset, dtTb);

            //for (int r = 1; r < crew_rows; r++)
            //    ((ucEmp)(tlpAlley.GetControlFromPosition(0, r))).RefreshStats(12);
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void cmdLeft_Click(object sender, EventArgs e)
        {
            int offset = -1;

            LogDay = LogDay.AddDays(offset * 7);
            _ref_week = _ref_week.AddDays(offset * 7);
            //dacCache.RefreshTimebook(_ref_week, false);            
            //qryGang.Requery();
            //qryTimebook.Requery();

            refresh_alley(offset);
        }


        private void cmdRight_Click(object sender, EventArgs e)
        {
            int offset = +1;

            LogDay = LogDay.AddDays(offset * 7);
            _ref_week = _ref_week.AddDays(offset * 7);
            //dacCache.RefreshTimebook(_ref_week, false);
            //qryGang.Requery();
            //qryTimebook.Requery();
                

            refresh_alley(offset);
        }



        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/
        
        private void mode_summary()
        {
            tlpAlley.SuspendLayout();
            int crew_rows = tlpAlley.RowCount;

            for (int r = 1; r < crew_rows; r++)
            {
                ((ucEmp)(tlpAlley.GetControlFromPosition(0, r))).ModeSummary();
                for (int c = 1; c <= HeadCount; c++)
                    ((ucWeek)(tlpAlley.GetControlFromPosition(c, r))).ModeSummary();
            }
            tlpAlley.ResumeLayout();
        }

        private void mode_hours()
        {
            tlpAlley.SuspendLayout();
            int crew_rows = tlpAlley.RowCount;
            for (int r = 1; r < crew_rows; r++)
            {
                ((ucEmp)(tlpAlley.GetControlFromPosition(0, r))).ModeHours();
                for (int c = 1; c <= HeadCount; c++)
                    ((ucWeek)(tlpAlley.GetControlFromPosition(c, r))).ModeHours();
            }
            tlpAlley.ResumeLayout();
        }

        private void cmdBoats_Click(object sender, EventArgs e)
        {
            Button cmd = (Button)sender;

            if (cmd.Text.Equals("Summary"))
            {
                mode_summary();
                cmd.Text = "Hours";
            }
            else
            {
                mode_hours();
                cmd.Text = "Summary";
            }

            
            //frmBoats frm = new frmBoats(DateTime.Now);

            //frm.LoadStart();
            //frm.LoadReady();

            //frm.ShowDialog();
        }


        private void cmdSched_Click(object sender, EventArgs e)
        {
            do_profile();
            
            frmSched frm = new frmSched(DateTime.Now);

            ucList frmList = (ucList)(flpN.Controls[0]);
            //DataTable dt_gang = dacCache.GetGang();
            DataTable dt_gang = qryGang.GetDT();

            frm.LoadStart();
            
            int show_week_offset = (_current_week_offset * 7);
            if (dt_gang != null)
            {
                foreach (DataRow row in dt_gang.Rows)
                {
                    bool check = (bool)(row["Active"]);
                    if (!check) continue;

                    //MessageBox.Show(row["EmpID"].ToString());
                    frm.LoadEmployee(row);
                }
            }

            frm.InitLoadReady();

            frm.ShowDialog();

            refresh_alley(0);
        }


        public void cmdHead_Click()
        {
            if (LogDay.Date <= DateTime.Now.Date)
                cmdLogs_Click(null, null);
            else
                cmdSched_Click(null, null);
       }


        public void cmdLogs_Click(object sender, EventArgs e)
        {
            //this.Parent.Hide();

            //frmLog frm = new frmLog(LogDay);
            letLog frm = new letLog(LogDay);
            

            //frmLog frm = new frmLog(_ref_week);
            //frmLog frm = new frmLog(DateTime.Now);

            frm.ShowDialog();            
            dacCache.RefreshTimebook(_ref_week, false);
            //qryGang.Requery();
            qryTimebook.Requery();

            refresh_alley(0);
            //this.Parent.Show();
        }


        private void flpN_SizeChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(this.ParentForm.Size.ToString() + ", " + flpN.Size.ToString());
            //ucList uc = (ucList)(flpN.Controls[0]);
            
            //Size sz = new Size(uc.Size.Width, flpN.Size.Height);
            //uc.Size = sz;
            
        }

        private void cmdTimeOff_Click(object sender, EventArgs e)
        {
            //frmSched frm = new frmSched(DateTime.Now);
            //frm.ShowDialog();

            frmStaff frm = new frmStaff(DateTime.Now);
        
            //DataTable dt_gang = dacCache.GetGang();
            DataTable dt_gang = qryGang.GetDT();
            ucList frmList = (ucList)(flpN.Controls[0]);

            frm.LoadStart();

            int show_week_offset = (_current_week_offset * 7);
            if (dt_gang != null)
            {
                foreach (DataRow row in dt_gang.Rows)
                {
                    bool check = (bool)(row["Active"]);
                    if (!check) continue;

                    //MessageBox.Show(row["EmpID"].ToString());
                    frm.LoadEmployee(row);
                }
            }

            frm.InitLoadReady();

            frm.ShowDialog();

            refresh_alley(0);
        }


        private void cmdRpt_Click(object sender, EventArgs e)
        {
            do_profile();
            
            ucList frmList = (ucList)(flpN.Controls[0]);
            //DataTable dt_gang = dacCache.GetGang();
            DataTable dt_gang = qryGang.GetDT();

            Form frm = new Form();
            frm.Size = new Size(540, 300);

            frm.Controls.Add(new ucRptTimbebook(dt_gang));

            frm.ShowDialog();
        }


        private void tlpAlley_Click(object sender, EventArgs e)
        {  
            // Turn this off for now
            return;

            //int w = tlpAlley.Size.Width;
            //int h = tlpAlley.Size.Height;

            //Rectangle r = Screen.GetWorkingArea(tlpAlley);

            //Form frm = this.ParentForm;
            //r = Screen.GetWorkingArea(frm);
           
            //w += 280;
            //if (w < r.Size.Width)
            //    tlpAlley.Size = new Size(w, h);
            //else
            //{
            //    w -= 280;
            //    w -= 280;
            //    tlpAlley.Size = new Size(w, h);
            //}

            //MessageBox.Show(string.Format("aw[{0}], ah{1}, sw{2}, sh{3}", w, h, r.Width, r.Height));
        }


        private void xflpN_Resize(object sender, EventArgs e)
        {
            FlowLayoutPanel flp = (FlowLayoutPanel)sender;

            int w = flp.Size.Width;
            int h = flp.Size.Height;
            
            flp.Size = new Size(_org_flpN_w, h);

            //ucList uc = (ucList)flpN.Controls[0];
            //uc.Size = flp.Size;
        }

        private void xflpN_ControlAdded(object sender, ControlEventArgs e)
        {
            FlowLayoutPanel flp = (FlowLayoutPanel)sender;

            int w = flp.Size.Width;
            int h = flp.Size.Height;

            flp.Size = new Size(_org_flpN_w, h);

        }

        private void ucAlley_Click(object sender, EventArgs e)
        {
            int w = this.Size.Width;
            int h = this.Size.Height;

            //MessageBox.Show(string.Format("w{0}, h{1}", w, h));
        }


        private void txtToday_DoubleClick(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Change date to end of previous month ?", "Please Confirm !", MessageBoxButtons.YesNo);

            switch (dr)
            {
                case DialogResult.Yes:
                    int year = _start_date.Year;
                    int month = (_start_date.AddMonths(-1)).Month;
                    _ref_week = new DateTime(year, month, DateTime.DaysInMonth(year, month));
                    break;
                case DialogResult.No:
                    break;
            }
        }


    }
}
