using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Globalization;


namespace viewAllyKE
{
    public partial class ucHead : UserControl
    {
        static public int uHeight = 74;
        static public int uWidth = 256;
        

        public DateTime RefWeek { get; set; }
        public bool SetAsCurrent { get; set; }
        

        private int week_of_year(DateTime d)
        {
            CultureInfo info = CultureInfo.CurrentCulture;
            return info.Calendar.GetWeekOfYear(d, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }


        private void draw_week(DateTime start_week, DateTime cal_week, bool current)
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

            lblWeek.Text = start_week.ToLongDateString();

            tbxMon.Text = start_week.Day.ToString();
            tbxTue.Text = start_week.AddDays(1).Day.ToString();
            tbxWed.Text = start_week.AddDays(2).Day.ToString();
            tbxThu.Text = start_week.AddDays(3).Day.ToString();
            tbxFri.Text = start_week.AddDays(4).Day.ToString();
            tbxSat.Text = start_week.AddDays(5).Day.ToString();
            tbxSun.Text = start_week.AddDays(6).Day.ToString();
        }

        
        public ucHead(DateTime start_week, bool current)
        {
            InitializeComponent();

            RefWeek = start_week;
            
            SetAsCurrent = current;
            draw_week(RefWeek, DateTime.Now, SetAsCurrent);                        
        }


        public void WeekRefresh(int week_offset)
        {
            RefWeek = RefWeek.AddDays(week_offset * 7);
            draw_week(RefWeek, DateTime.Now, SetAsCurrent);
        }


        private void tbxMon_Click(object sender, EventArgs e)
        {
            DateTime day = RefWeek.AddDays(0);
            
            TableLayoutPanel pnl = (TableLayoutPanel)(this.Parent);
            ucAlley uc = (ucAlley)(pnl.Parent);

            uc.LogDay = day.Date;            
            uc.cmdHead_Click();
            
        }

        private void tbxTue_Click(object sender, EventArgs e)
        {
            DateTime day = RefWeek.AddDays(1);

            TableLayoutPanel pnl = (TableLayoutPanel)(this.Parent);
            ucAlley uc = (ucAlley)(pnl.Parent);

            uc.LogDay = day.Date;
            uc.cmdHead_Click();
            //uc.cmdLogs_Click(null, null);
        }

        private void tbxWed_Click(object sender, EventArgs e)
        {
            DateTime day = RefWeek.AddDays(2);

            TableLayoutPanel pnl = (TableLayoutPanel)(this.Parent);
            ucAlley uc = (ucAlley)(pnl.Parent);

            uc.LogDay = day.Date;
            uc.cmdHead_Click();
            //uc.cmdLogs_Click(null, null);
        }

        private void tbxThu_Click(object sender, EventArgs e)
        {
            DateTime day = RefWeek.AddDays(3);

            TableLayoutPanel pnl = (TableLayoutPanel)(this.Parent);
            ucAlley uc = (ucAlley)(pnl.Parent);

            uc.LogDay = day.Date;
            uc.cmdHead_Click();
            //uc.cmdLogs_Click(null, null);
        }

        private void tbxFri_Click(object sender, EventArgs e)
        {
            DateTime day = RefWeek.AddDays(4);

            TableLayoutPanel pnl = (TableLayoutPanel)(this.Parent);
            ucAlley uc = (ucAlley)(pnl.Parent);

            uc.LogDay = day.Date;
            uc.cmdHead_Click();
            //uc.cmdLogs_Click(null, null);
        }

        private void tbxSat_Click(object sender, EventArgs e)
        {
            DateTime day = RefWeek.AddDays(5);

            TableLayoutPanel pnl = (TableLayoutPanel)(this.Parent);
            ucAlley uc = (ucAlley)(pnl.Parent);

            uc.LogDay = day.Date;
            uc.cmdHead_Click();
            //uc.cmdLogs_Click(null, null);
        }

        private void tbxSun_Click(object sender, EventArgs e)
        {
            DateTime day = RefWeek.AddDays(6);

            TableLayoutPanel pnl = (TableLayoutPanel)(this.Parent);
            ucAlley uc = (ucAlley)(pnl.Parent);

            uc.LogDay = day.Date;
            uc.cmdHead_Click();
            //uc.cmdLogs_Click(null, null);
        }
    }
}
