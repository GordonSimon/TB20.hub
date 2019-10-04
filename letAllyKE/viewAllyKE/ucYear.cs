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
    public partial class ucYear : UserControl
    {
        static public int uHeight = 74;
        static public int uWidth = 150;

        public int RefDays { get; set; }
        public DateTime RefDate { get; set; }
        public DateTime ShowWeek { get; set; }
        

        private void draw_year(DateTime refdate, DateTime start_week, int refdays)
        {
            lblYear.Text = refdate.Year.ToString();
            //lblDay.Text = string.Format("{0} / {1}", refdate.ToString("MMM"), refdate.Day.ToString());
            lblDay.Text = string.Format("{0}", refdate.ToString("MMMM"));
            lblEnd.Text = string.Format("to {0}", start_week.AddDays(refdays).ToString("MMM/dd"));
        }


        public ucYear(DateTime refdate, DateTime show_week, int days)
        {
            InitializeComponent();

            RefDays = days;
            RefDate = refdate;
            ShowWeek = show_week;

            draw_year(RefDate, ShowWeek, RefDays);
        }

        public void WeekRefresh(DateTime refdate, DateTime show_week, int days)
        {
            RefDays = days;
            RefDate = refdate;
            ShowWeek = show_week;

            draw_year(RefDate, ShowWeek, RefDays);
        }
    }
}
