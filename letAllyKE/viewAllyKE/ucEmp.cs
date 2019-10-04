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
    public partial class ucEmp : UserControl
    {
        static public int uHeight = 77;
        static public int uWidth = 150;
        static public int uAdjust = 52;
        static public int uAdjust2 = 56;

        //private ToolTip _ttSummary = null;


        private DateTime _ref_date;

        private string _daysT;
        private string _hourT;
        private string _overT;

        private string _daysP;
        private string _daysN;

        private void set_eid(string eid)
        {
            lblEid.Text = eid;
        }


        private void set_name(string ename)
        {
            lblName.Text = ename;
        }


        private void set_homephone(string phone)
        {
            lblHome.Text = string.Format("H : {0}", phone);
        }


        private void set_cellphone(string phone)
        {
            lblCell.Text = string.Format("C : {0}", phone);
        }


        private void set_duty(string duty)
        {
            //if (duty.Equals("Skipper"))
            //    lblSkipper.Show();
            //else
            //    lblSkipper.Hide();

            if (duty.Equals("Master"))
                lblMaster.Show();
            else
                lblMaster.Hide();
        }


        private void set_days(int day)
        {
            _daysT = string.Format("{0,5}", day);
            lblDays.Text = _daysT;
        }

        private void set_hour(decimal hour)
        {
            _hourT = string.Format("{0:N1}", hour);
            //lblDays.Text = _daysT;
        }

        private void set_over(decimal over)
        {
            _overT = string.Format("{0:N1}", over);
            //lblDays.Text = _daysT;
        }


        private void set_prev(int p)
        {
            _daysP = string.Format("{0,5}", p);
            //lblPrev.Text = string.Format("{0,5}", p);
        }


        private void set_next(int n)
        {
            _daysN = string.Format("{0,5}", n);
            //lblNext.Text = string.Format("{0,5}", n);
        }


        private void set_refdate(DateTime ref_date)
        {
            _ref_date = ref_date;
        }


        public void RefreshStats(int day)
        {
            set_days(day);
        }


        public void RefreshEmp(string eid, string ename, string home, string cell, string duty, int day, decimal hours, decimal overs, DateTime ref_date, bool active)
        {
            set_eid(eid);
            set_name(ename);
            set_homephone(home);
            set_cellphone(cell);
            set_duty(duty);

            set_days(day);
            set_hour(hours);
            set_over(overs);

            set_prev(0);
            set_next(0);

            set_refdate(ref_date);

            if (active)
                this.BackColor = Color.SandyBrown;
            else
                this.BackColor = SystemColors.Control;
        }


        public ucEmp(string eid, string ename, string home, string cell, string duty, int day, decimal hours, decimal overs, DateTime ref_date, bool active)
        {
            InitializeComponent();

            init_modesummary();
            RefreshEmp(eid, ename, home, cell, duty, day, hours, overs, ref_date, active);
        }


        private void theclick()
        {            
            //DateTime log_date;

            this.BackColor = Color.LightSeaGreen;

            bool r = qryGang.ProfileAdd(lblEid.Text);
            

            //bool valid = false;
            //while (! valid)
            //{
            //    frmLog frm = new frmLog(DateTime.Now);
            //    DialogResult ok = frm.ShowDialog();

            //    if (ok == DialogResult.OK)
            //    {
            //        log_date = frm.LogDate;

            //        valid = true;
            //        this.BackColor = Color.LightSeaGreen;
            //    }
            //}
        }

        private void ucEmp_Click(object sender, EventArgs e)
        {
            theclick();
        }

        private void lblName_Click(object sender, EventArgs e)
        {
            theclick();
        }

        private void lblHome_Click(object sender, EventArgs e)
        {
            theclick();
        }

        private void lblCell_Click(object sender, EventArgs e)
        {
            theclick();
        }


        private void init_modesummary()
        {
            //this.lblPrev.Hide();
            //this.lblNext.Hide();
            this.pnlBox.Size = new Size(this.pnlBox.Width, this.pnlBox.Size.Height - uAdjust2);
            this.Size = new Size(this.Size.Width, this.Size.Height - uAdjust);
        }


        public void ModeSummary()
        {
            //this.lblPrev.Hide();
            //this.lblNext.Hide();
            this.pnlBox.Size = new Size(this.pnlBox.Width, this.pnlBox.Size.Height - uAdjust2);
            this.Size = new Size(this.Size.Width, this.Size.Height - uAdjust);
        }


        public void ModeHours()
        {
            //this.lblPrev.Show();
            //this.lblNext.Show();
            this.pnlBox.Size = new Size(this.pnlBox.Width, this.pnlBox.Size.Height + uAdjust2);
            this.Size = new Size(this.Size.Width, this.Size.Height + uAdjust);
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEmployee frm = new frmEmployee();
            frm.ShowDialog();
        }


        private void archiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEmployee frm = new frmEmployee();
            frm.ShowDialog();
        }

        private void pnlBox_MouseHover(object sender, EventArgs e)
        {
            //if (_ttSummary == null) _ttSummary = new ToolTip();

            //string msg = string.Format(" Prev[{0}] Month[{1}] Next[{2}]", _daysP, _daysT, _daysN);
            //_ttSummary.Show(msg, pnlBox, 3000);
        }

        private void lblDays_Click(object sender, EventArgs e)
        {
            frmEmpSum frm = new frmEmpSum(_ref_date, lblEid.Text, lblName.Text);

            frm.SetToday = _daysT;
            frm.SetTodayHour = _hourT;
            frm.SetTodayOver = _overT;

            //frm.SetPrev = _daysP;            
            //frm.SetNext = _daysN;
            //frm.SetPrevSched = _daysP;
            //frm.SetTodaySched = _daysT;
            //frm.SetNextSched = _daysN;

            frm.ShowDialog();

        }

 

    }
}
