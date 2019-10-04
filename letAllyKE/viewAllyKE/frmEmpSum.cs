using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using mdlAllyKE;

namespace viewAllyKE
{
    public partial class frmEmpSum : Form
    {
        public string SetEid { set { tbxEID.Text = value; } }
        public string SetName { set { tbxName.Text = value; } }

        public string SetDate { set { tbxDate.Text = value; } }
        public string SetYear { set { tbxYear.Text = value; } }

        public string SetToday { set { tbxT.Text = value; } }
        public string SetTodayHour { set { tbxTH.Text = value; } }
        public string SetTodayOver { set { tbxTO.Text = value; } }

        public string SetPrev { set { tbxP.Text = value; } }
        public string SetPrevHour { set { tbxPH.Text = value; } }
        public string SetPrevOver { set { tbxPO.Text = value; } }

        //public string SetNext { set { tbxN.Text = value; } }
        
        //public string SetPrevSched { set { tbxPS.Text = value; } }

        public string SetSched { set { tbxTS.Text = value; } }
        public string SetNextSched { set { tbxNS.Text = value; } }

        public string SetHoliday { set { tbxH.Text = value; } }
        public string SetO { set { tbxO.Text = value; } }
        public string SetU { set { tbxU.Text = value; } }
        public string SetS { set { tbxS.Text = value; } }

        public string SetYHoliday { set { tbxHY.Text = value; } }
        public string SetOY { set { tbxOY.Text = value; } }
        public string SetUY { set { tbxUY.Text = value; } }
        public string SetSY { set { tbxSY.Text = value; } }


        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        private DateTime _ref_week;




        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        public frmEmpSum(DateTime ref_week, string emp_id, string emp_name)
        {
            InitializeComponent();
            
            _ref_week = ref_week;

            DataTable dtSumPrev = qrySummary.GetView("Worksum", _ref_week.AddMonths(-1));
            DataTable dtSumNext = qrySummary.GetView("Worksum", _ref_week.AddMonths(1));

            DataTable dtSchd = qrySummary.GetView("Schedsum", _ref_week);
            DataTable dtSchdNext = qrySummary.GetView("Schedsum", _ref_week.AddMonths(1));

            DataTable dtYear = qrySummary.GetView("Yearsum", _ref_week, 0, emp_id, null);


            DataRow rowPrevsum = dtSumPrev.Rows.Find(emp_id);
            int day_prev = 0;
            decimal hour_prev = 0M;
            decimal over_prev = 0M;
            if (rowPrevsum != null)
            {
                day_prev = (int)rowPrevsum["Days"];
                hour_prev = (decimal)rowPrevsum["Hours"];
                over_prev = (decimal)rowPrevsum["Overs"];
            }
            
            //DataRow rowNextsum = dtNextsum.Rows.Find(emp_id);
            //int day_next = 0;
            //if (rowNextsum != null)
            //{
            //    day_next = (int)rowNextsum["Days"];
            //}


            DataRow rowSched = dtSchd.Rows.Find(emp_id);
            int sched = 0;
            int mHoliday = 0;
            int mOff = 0;
            int mUnavail = 0;
            int mSick = 0;
            if (rowSched != null)
            {
                sched = (int)rowSched["Days"];
                mHoliday = (int)rowSched["Holiday"];
                mOff = (int)rowSched["Off"];
                mUnavail = (int)rowSched["Unavail"];
                mSick = (int)rowSched["Sick"];

            }

            DataRow rowSchedsum = dtSchdNext.Rows.Find(emp_id);
            int next_sched = 0;
            if (rowSchedsum != null)
            {
                next_sched = (int)rowSchedsum["Days"];
            }


            DataRow rowHoliday = dtYear.Rows.Find(emp_id);
            int yHoliday = 0;
            int yOff = 0;
            int yUnavail = 0;
            int ySick = 0;
            if (rowHoliday != null)
            {
                yHoliday = (int)rowHoliday["Holiday"];
                yOff = (int)rowHoliday["Off"];
                yUnavail = (int)rowHoliday["Unavail"];
                ySick = (int)rowHoliday["Sick"];
            }


            SetEid = emp_id;
            SetName = emp_name;

            SetDate = _ref_week.ToString("MMMM/yyyy");
            SetYear = _ref_week.ToString("yyyy") + " [Annual Count]";

            SetPrev = string.Format("{0,5}", day_prev);
            SetPrevHour = string.Format("{0:N1}", hour_prev);
            SetPrevOver = string.Format("{0:N1}", over_prev);

            SetSched = string.Format("{0,5}", sched);
            SetNextSched = string.Format("{0,5}", next_sched);

            SetHoliday = string.Format("{0,5}", mHoliday);
            SetO = string.Format("{0,5}", mOff);
            SetU = string.Format("{0,5}", mUnavail);
            SetS = string.Format("{0,5}", mSick);

            SetYHoliday = string.Format("{0,5}", yHoliday);
            SetOY = string.Format("{0,5}", yOff);
            SetUY = string.Format("{0,5}", yUnavail);
            SetSY = string.Format("{0,5}", ySick);

            cmdOk.Focus();
        }



        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        private void cmdOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdMore_Click(object sender, EventArgs e)
        {
            Button cmd = (Button)sender;

            this.Width += 160;
            cmd.Enabled = false;

            cmdOk.Focus();

        }
    }
}
