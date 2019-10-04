using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Globalization;

using mdlAllyKE;
using viewLogKE;


namespace viewAllyKE
{
    interface CueWeek { void Msg(); }

    public partial class ucWeek : UserControl
    {
        static public int uHeight = 77;
        static public int uWidth = 256;
        static public int uAdjust = 52;

        public bool CueRequired { get; set; }

        public DateTime RefWeek { get; set; }
        public bool SetAsCurrent { get; set; }

        private string _emp_id { get; set; }
        private string _emp_name { get; set; }


        //private DataSet _ds;
        private DataTable _dt;


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public ucWeek(string emp_id, string emp_name, DateTime start_week, bool current, DataTable dt)
        {
            InitializeComponent();

            RefWeek = start_week;

            _emp_id = emp_id;
            _emp_name = emp_name;

            //_ds = dacTimebook.GetDS(RefWeek, 7);
            //_dt = qrySummary.GetView("TimeBook", RefWeek, 7, null, qryGang.GetDT());
            //_dt = dacCache.GetTimebook();
            if (dt == null)
                _dt = dacCache.GetTimebook();                
            else
                _dt = dt;

            CueRequired = true; // always required

            init_modesummary();

            SetAsCurrent = current;
            load_dates(RefWeek, emp_id, dt);
            draw_week(RefWeek, DateTime.Now, SetAsCurrent);
            
        }



        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/                     
      
        private int week_of_year(DateTime d)
        {
            CultureInfo info = CultureInfo.CurrentCulture;
            return info.Calendar.GetWeekOfYear(d, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }


        private void draw_week(DateTime start_week, DateTime cal_week, bool current)
        {
            int woy = week_of_year(start_week);

            bool is_future_week = start_week.Ticks > cal_week.Ticks;
            bool is_current_week = week_of_year(start_week) == week_of_year(cal_week);


            //if (is_current_week)
            //    this.BackColor = Color.LightGreen;
            //else
            //    this.BackColor = SystemColors.Control;
            if (is_current_week)
                this.BackColor = Color.LightSteelBlue;
            else
                this.BackColor = SystemColors.Control;

        }


        private void tag_dates(TextBox tbxDH,
            TextBox tbxDO, TextBox tbxDS, DateTime start_week, int offset)
        {
            tbxDH.Tag = start_week.AddDays(offset);            
            tbxDO.Tag = start_week.AddDays(offset);
            tbxDS.Tag = start_week.AddDays(offset);
        }



        private void load_toff1(string emp_id, TextBox tbxDH,
            TextBox tbxDO, TextBox tbxDS, Panel pnlDN, DateTime start_week, int offset)
        {
            DataRow[] rows;

            string qry = "BookDate=#" + start_week.AddDays(offset).Date.ToLongDateString() + "# AND EmpID='" + emp_id + "'";
            rows = _dt.Select(qry);

            tbxDH.ForeColor = SystemColors.WindowText;
            tbxDH.Font = new Font(tbxDH.Font, FontStyle.Regular);

            pnlDN.Tag = null;
            pnlDN.Visible = false;
            if (rows.Length == 0)
            {
                tbxDH.Text = "";
                tbxDO.Text = "";
                tbxDS.Text = "";
                return;
            }


            //*************************
            //* Early Exit
            //**************************

            if (rows.Length > 1) MessageBox.Show("Too many rows !");

            DataRow row = rows[0];
            string toff = (string)row["Top"];
            string note = (string)row["Note"];

            tbxDH.Text = toff;
            tbxDO.Text = (string)row["Mid"];
            tbxDS.Text = (string)row["Bot"];

            if (toff.Equals("!12"))
                tbxDH.ForeColor = Color.DarkOrange;
            else
            {
                decimal d = libAlly.NeedDecimal(tbxDH);
                if (d != Decimal.Zero)
                    tbxDH.Font = new Font(tbxDH.Font, FontStyle.Bold);
            }

            if (note.Length > 0)
            {
                pnlDN.Visible = true;
                pnlDN.Tag = new object[] { note, start_week.AddDays(offset) };
            }
            

            //foreach (DataRow row in rows)
            //{                
                //try
                //{

                //    hour += Convert.ToDecimal(row["LogHours"]);
                //    over += Convert.ToDecimal(row["LogOver"]);
                //    if (vessel.Length == 0)
                //        vessel = row["LogVessel"].ToString();
                //    else
                //        vessel += "/" + row["LogVessel"].ToString();

                //    if (toff.Length == 0)
                //        toff = row["ToffCode"].ToString();
                //    else
                //    {
                //        if (row["ToffCode"].ToString().Length != 0)
                //            toff += "/" + row["ToffCode"].ToString();
                //    }

                //    if (note.Length == 0)
                //        note = row["LogNote"].ToString();
                //    else
                //        note += "\n[" + row["LogNote"].ToString() + "]";

                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(string.Format("Warning ({0}) : Converson error for LogHours [{1}]/[{2}] !",
                //            ex.Message, row["LogHours"].ToString(), row["ToffCode"].ToString()), "ucWeek.load_toff()");
                //    hour = 0;
                //    over = 0;
                //    vessel = "";
                //    toff = "";
                //    break;
                //}
            //}

            //if (hour != decimal.Zero)
            //{
            //    if (toff.Equals("") || toff.Equals("12") || toff.Equals("O"))
            //    {
            //        tbxDH.Text = hour.ToString("#.#");
            //        tbxDH.Font = new Font(tbxDH.Font, FontStyle.Bold);
            //    }
            //    else
            //    {
            //        tbxDH.Text = string.Format("{0:0.#}({1})", hour, toff);
            //        tbxDH.ForeColor = Color.Red;
            //    }
            //}
            //else
            //{
            //    tbxDH.Text = toff;
            //    if (toff.Equals("12"))
            //        tbxDH.ForeColor = Color.DarkOrange;
            //}

            //tbxDO.Text = over.ToString("#.#");
            //tbxDS.Text = vessel;
       
        }


        private void load_toff2(string emp_id, TextBox tbxDH,
            TextBox tbxDO, TextBox tbxDS, Panel pnlDN, DateTime start_week, int offset)
        {            
            DataRow[] rows;
            //DataRow row = null;
            
            DateTime tod = DateTime.Now;
            //row = _ds.Tables[0].Rows.Find(new object[] { start_week.AddDays(offset).Date, emp_id });
            //row = _dt.Rows.Find(new object[] { start_week.AddDays(offset).Date, emp_id });
            string qry = "BookDate=#" + start_week.AddDays(offset).Date.ToLongDateString() + "# AND EmpID='" + emp_id + "'";
            rows = _dt.Select(qry);

            
            decimal hour = 0.0M;
            decimal over = 0.0M;            
            string vessel = "";
            string toff = "";
            string note = "";
            //int count = 0;
            foreach (DataRow row in rows)
            {
                try
                {
                    //if (row["EmpId"].Equals("A6") && row["ToffCode"].ToString().Equals("12")) count += 1;

                    hour += Convert.ToDecimal(row["LogHours"]);
                    over += Convert.ToDecimal(row["LogOver"]);
                    if (vessel.Length == 0)
                        vessel = row["LogVessel"].ToString();
                    else
                        vessel += "/" + row["LogVessel"].ToString();

                    if (toff.Length == 0)
                        toff = row["ToffCode"].ToString();
                    else
                    {
                        if (row["ToffCode"].ToString().Length != 0)
                            toff += "/" + row["ToffCode"].ToString();
                    }

                    if (note.Length == 0)
                        note = row["LogNote"].ToString();
                    else
                        note += "\n[" + row["LogNote"].ToString() + "]";

                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Warning ({0}) : Converson error for LogHours [{1}]/[{2}] !",
                            ex.Message, row["LogHours"].ToString(), row["ToffCode"].ToString()), "ucWeek.load_toff()");
                    hour = 0;
                    over = 0;
                    vessel = "";
                    toff = "";
                    break;
                }
            }

           
            tbxDH.ForeColor = SystemColors.WindowText;
            tbxDH.Font = new Font(tbxDH.Font, FontStyle.Regular);

            pnlDN.Tag = null;
            pnlDN.Visible = false;            
            if (rows.Length == 0)
            {
                tbxDH.Text = "";
                tbxDO.Text = "";
                tbxDS.Text = "";
                return;
            }
            
           
            if (hour != decimal.Zero)
            {
                if (toff.Equals("") || toff.Equals("12") || toff.Equals("O"))
                {
                    tbxDH.Text = hour.ToString("#.#");
                    tbxDH.Font = new Font(tbxDH.Font, FontStyle.Bold);
                }
                else
                {
                    tbxDH.Text = string.Format("{0:0.#}({1})", hour, toff);
                    tbxDH.ForeColor = Color.Red;
                }
            }
            else
            {
                tbxDH.Text = toff;
                if (toff.Equals("12"))
                    tbxDH.ForeColor = Color.DarkOrange;
            }
            
            tbxDO.Text = over.ToString("#.#");
            tbxDS.Text = vessel;

            if (note.Length > 0)
            {
                pnlDN.Visible = true;
                pnlDN.Tag = new object[] { note, start_week.AddDays(offset) };
            }
        }


        private void load_dates(DateTime start_week, string emp_id, DataTable dt)
        {
            if (dt == null)
            {
                load_toff2(emp_id, tbxMH, tbxMO, tbxMS, pnlM, start_week, 0);
                load_toff2(emp_id, tbxTH, tbxTO, tbxTS, pnlT, start_week, 1);
                load_toff2(emp_id, tbxWH, tbxWO, tbxWS, pnlW, start_week, 2);
                load_toff2(emp_id, tbxThH, tbxThO, tbxThS, pnlTh, start_week, 3);
                load_toff2(emp_id, tbxFH, tbxFO, tbxFS, pnlF, start_week, 4);
                load_toff2(emp_id, tbxSaH, tbxSaO, tbxSaS, pnlS, start_week, 5);
                load_toff2(emp_id, tbxSuH, tbxSuO, tbxSuS, pnlSu, start_week, 6);
            }
            else
            {
                load_toff1(emp_id, tbxMH, tbxMO, tbxMS, pnlM, start_week, 0);
                load_toff1(emp_id, tbxTH, tbxTO, tbxTS, pnlT, start_week, 1);
                load_toff1(emp_id, tbxWH, tbxWO, tbxWS, pnlW, start_week, 2);
                load_toff1(emp_id, tbxThH, tbxThO, tbxThS, pnlTh, start_week, 3);
                load_toff1(emp_id, tbxFH, tbxFO, tbxFS, pnlF, start_week, 4);
                load_toff1(emp_id, tbxSaH, tbxSaO, tbxSaS, pnlS, start_week, 5);
                load_toff1(emp_id, tbxSuH, tbxSuO, tbxSuS, pnlSu, start_week, 6);
            }

            tag_dates(tbxMH, tbxMO, tbxMS, start_week, 0);
            tag_dates(tbxTH, tbxTO, tbxTS, start_week, 1);
            tag_dates(tbxWH, tbxWO, tbxWS, start_week, 2);
            tag_dates(tbxThH, tbxThO, tbxThS, start_week, 3);
            tag_dates(tbxFH, tbxFO, tbxFS, start_week, 4);
            tag_dates(tbxSaH, tbxSaO, tbxSaS, start_week, 5);
            tag_dates(tbxSuH, tbxSuO, tbxSuS, start_week, 6);
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public void init_modesummary()
        {
            tbxMO.Hide();
            tbxTO.Hide();
            tbxWO.Hide();
            tbxThO.Hide();
            tbxFO.Hide();
            tbxSaO.Hide();
            tbxSuO.Hide();

            tbxMS.Hide();
            tbxTS.Hide();
            tbxWS.Hide();
            tbxThS.Hide();
            tbxFS.Hide();
            tbxSaS.Hide();
            tbxSuS.Hide();

            this.Size = new Size(this.Size.Width, this.Size.Height - uAdjust);
        }


        public void ModeSummary()
        {
            string emp_id;
            emp_id = _emp_id;

            //tbxMH.Hide();
            //tbxTH.Hide();
            //tbxWH.Hide();
            //tbxThH.Hide();
            //tbxFH.Hide();
            //tbxSaH.Hide();
            //tbxSuH.Hide();

            tbxMO.Hide();
            tbxTO.Hide();
            tbxWO.Hide();
            tbxThO.Hide();
            tbxFO.Hide();
            tbxSaO.Hide();
            tbxSuO.Hide();

            tbxMS.Hide();
            tbxTS.Hide();
            tbxWS.Hide();
            tbxThS.Hide();
            tbxFS.Hide();
            tbxSaS.Hide();
            tbxSuS.Hide();

            //tbxMS.Location = new Point(tbxMS.Location.X, tbxMS.Location.Y - 47);
            //tbxTS.Location = new Point(tbxTS.Location.X, tbxTS.Location.Y - 47);
            //tbxWS.Location = new Point(tbxWS.Location.X, tbxWS.Location.Y - 47);
            //tbxThS.Location = new Point(tbxThS.Location.X, tbxThS.Location.Y - 47);
            //tbxFS.Location = new Point(tbxFS.Location.X, tbxFS.Location.Y - 47);
            //tbxSaS.Location = new Point(tbxSaS.Location.X, tbxSuS.Location.Y - 47);
            //tbxSuS.Location = new Point(tbxSuS.Location.X, tbxSuS.Location.Y - 47);

            this.Size = new Size(this.Size.Width, this.Size.Height - uAdjust);

            //RefWeek = RefWeek.AddDays(0);

            //_dt = dacCache.GetTimebook();
            

            //load_dates(RefWeek, emp_id);
            //draw_week(RefWeek, DateTime.Now, SetAsCurrent);
        }


        public void ModeHours()
        {
            string emp_id;
            emp_id = _emp_id;

            //tbxMS.Location = new Point(tbxMS.Location.X, tbxMS.Location.Y + 47);
            //tbxTS.Location = new Point(tbxTS.Location.X, tbxTS.Location.Y + 47);
            //tbxWS.Location = new Point(tbxWS.Location.X, tbxWS.Location.Y + 47);
            //tbxThS.Location = new Point(tbxThS.Location.X, tbxThS.Location.Y + 47);
            //tbxFS.Location = new Point(tbxFS.Location.X, tbxFS.Location.Y + 47);
            //tbxSaS.Location = new Point(tbxSaS.Location.X, tbxSuS.Location.Y + 47);
            //tbxSuS.Location = new Point(tbxSuS.Location.X, tbxSuS.Location.Y + 47);

            //tbxMH.Show();
            //tbxTH.Show();
            //tbxWH.Show();
            //tbxThH.Show();
            //tbxFH.Show();
            //tbxSaH.Show();
            //tbxSuH.Show();

            tbxMO.Show();
            tbxTO.Show();
            tbxWO.Show();
            tbxThO.Show();
            tbxFO.Show();
            tbxSaO.Show();
            tbxSuO.Show();

            tbxMS.Show();
            tbxTS.Show();
            tbxWS.Show();
            tbxThS.Show();
            tbxFS.Show();
            tbxSaS.Show();
            tbxSuS.Show();


            this.Size = new Size(this.Size.Width, this.Size.Height + uAdjust);

            //RefWeek = RefWeek.AddDays(0);

            //_dt = dacCache.GetTimebook();


            //load_dates(RefWeek, emp_id);
            //draw_week(RefWeek, DateTime.Now, SetAsCurrent);
        }


        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        public void WeekRefresh(int week_offset, DataTable dt)
        {
            string emp_id;

            emp_id = _emp_id;

            RefWeek = RefWeek.AddDays(week_offset * 7);

            //_ds = dacTimebook.GetDS(RefWeek, 7); 
            //dacCache.RefreshTimebook(RefWeek, false);
            //_dt = dacCache.GetTimebook();
            //_dt = qrySummary.GetView("TimeBook", RefWeek, 7, null, qryGang.GetDT());                
            _dt = dt;

            load_dates(RefWeek, emp_id, dt);
            draw_week(RefWeek, DateTime.Now, SetAsCurrent);

        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/                           
        
        private void theclick(object sender)
        {
            //MessageBox.Show(this.ParentForm.Text);

            DateTime day = (DateTime)((TextBox)sender).Tag;


            if (day <= DateTime.Now)
            {
                //frmLog frm = new frmLog(day);
                letLog frm = new letLog(day);

                frm.ShowDialog();
            }
            else
            {
                frmSched frm = new frmSched(day);
                
                //ucList frmList = (ucList)(flpN.Controls[0]);
                //DataTable dt_gang = dacCache.GetGang();
                DataTable dt_gang = qryGang.GetDT();

                frm.LoadStart();

                //int show_week_offset = (_current_week_offset * 7);
                if (dt_gang != null)
                {
                    foreach (DataRow row in dt_gang.Rows)
                    {
                        bool check = (bool)(row["Active"]);
                        if (!check) continue;
                        if (!row["EmpID"].Equals(_emp_id)) continue;

                        //MessageBox.Show(row["EmpID"].ToString());
                        frm.LoadEmployee(row);
                    }
                }

                frm.InitLoadReady();

                frm.ShowDialog();
                //frm.Show();
            }

            if (CueRequired)
                ((CueWeek)this.ParentForm).Msg();

        }

        private void tbxMH_Click(object sender, EventArgs e)
        {
            theclick(sender);
        }

        private void tbxMO_Click(object sender, EventArgs e)
        {
            theclick(sender);
        }

        private void tbxMS_Click(object sender, EventArgs e)
        {
            theclick(sender);
        }

        private void tbxTH_Click(object sender, EventArgs e)
        {
            theclick(sender);
        }

        private void tbxTO_Click(object sender, EventArgs e)
        {
            theclick(sender);
        }

        private void tbxTS_Click(object sender, EventArgs e)
        {
            theclick(sender);
        }

        private void tbxWH_Click(object sender, EventArgs e)
        {
            theclick(sender);
        }

        private void tbxWO_Click(object sender, EventArgs e)
        {
            theclick(sender);
        }

        private void tbxWS_Click(object sender, EventArgs e)
        {
            theclick(sender);
        }

        private void tbxThH_Click(object sender, EventArgs e)
        {
            theclick(sender);
        }

        private void tbxThO_Click(object sender, EventArgs e)
        {
            theclick(sender);
        }

        private void tbxThS_Click(object sender, EventArgs e)
        {
            theclick(sender);
        }

        private void tbxFH_Click(object sender, EventArgs e)
        {
            theclick(sender);
        }

        private void tbxFO_Click(object sender, EventArgs e)
        {
            theclick(sender);
        }

        private void tbxFS_Click(object sender, EventArgs e)
        {
            theclick(sender);
        }

        private void tbxSaH_Click(object sender, EventArgs e)
        {
            theclick(sender);
        }

        private void tbxSaO_Click(object sender, EventArgs e)
        {
            theclick(sender);
        }

        private void tbxSaS_Click(object sender, EventArgs e)
        {
            theclick(sender);
        }

        private void tbxSuH_Click(object sender, EventArgs e)
        {
            theclick(sender);
        }

        private void tbxSuO_Click(object sender, EventArgs e)
        {
            theclick(sender);
        }

        private void tbxSuS_Click(object sender, EventArgs e)
        {
            theclick(sender);
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        //private TableLayoutPanel _tlpNote;

        private void note_click(object sender)
        {
            object[] note = (object[])((Panel)sender).Tag;

            string memo = note[0].ToString();
            DateTime day = (DateTime)note[1];

            ucNote uc = new ucNote(_emp_id, memo, day.Date);
            uc.ShowNote();


            //_tlpNote = (TableLayoutPanel)(this.ParentForm.Controls.Find("tlpNote", false)[0]);
            //_tlpNote.Controls.Add(uc);            
            //_tlpNote.Show();
            //_tlpNote.BringToFront();
        }


        private void pnlM_Click(object sender, EventArgs e)
        {
            note_click(sender);
        }

        private void pnlT_Click(object sender, EventArgs e)
        {
            note_click(sender);
        }

        private void pnlW_Click(object sender, EventArgs e)
        {
            note_click(sender);
        }

        private void pnlTh_Click(object sender, EventArgs e)
        {
            note_click(sender);
        }

        private void pnlF_Click(object sender, EventArgs e)
        {
            note_click(sender);
        }

        private void pnlS_Click(object sender, EventArgs e)
        {
            note_click(sender);
        }

        private void pnlSu_Click(object sender, EventArgs e)
        {
            note_click(sender);
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/


        private string show_toff(string emp_id, DateTime start_week, int offset)
        {
            return "";

            DataRow[] rows;
            
            string qry = "BookDate=#" + start_week.AddDays(offset).Date.ToLongDateString() + "# AND EmpID='" + emp_id + "'";
            rows = _dt.Select(qry);

            decimal hour = 0.0M;
            decimal over = 0.0M;
            string vessel = "";
            string toff = "";
            string note = "";
            //string unit = "";

            int row_count = 0;

            foreach (DataRow row in rows)
            {
                try
                {
                    row_count += 1;
                    hour += Convert.ToDecimal(row["LogHours"]);
                    over += Convert.ToDecimal(row["LogOver"]);
                    //if (row["Duty"].ToString().Equals("Office")) unit = "d";

                    if (vessel.Length == 0)
                        vessel = row["LogVessel"].ToString();
                    else
                        vessel += "/" + row["LogVessel"].ToString();

                    //if (toff.Length == 0)
                    //    toff = row["ToffCode"].ToString() + unit;
                    //else
                    //    toff += "/" + row["ToffCode"].ToString() + unit;


                    if (toff.Length == 0)
                        toff = row["ToffCode"].ToString();
                    else
                    {
                        if (row["ToffCode"].ToString().Length != 0)
                            toff += "/" + row["ToffCode"].ToString();
                    }


                    if (note.Length == 0)
                        note = row["LogNote"].ToString();
                    else
                        note += "\n[" + row["LogNote"].ToString() + "]" + note.Length.ToString();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Warning ({0}) : Converson error for LogHours [{1}]/[{2}] !",
                            ex.Message, row["LogHours"].ToString(), row["ToffCode"].ToString()), "ucWeek.show_toff()");
                    hour = 0;
                    over = 0;
                    vessel = "";
                    toff = "";
                    break;
                }
            }


            if (rows.Length == 0) return "<no record>";

            string msg = string.Format("{0}, {1:0.#}, {2:0.#}, {3}, <{4}>, {5}, {6}", 
                _emp_id, hour, over, vessel, toff, note, row_count);
 
            return msg;
        }


        private void tbxMH_MouseHover(object sender, EventArgs e)
        {
            TextBox tbx = (TextBox)sender;

            ToolTip tt = new ToolTip();
            int offset = 0;

            //switch (tbx.Name)
            //{
            //    case "tbxMH": offset = 0; break;
            //    case "tbxTH": offset = 1; break;
            //    case "tbxWH": offset = 2; break;
            //    case "tbxThH": offset = 3; break;
            //    case "tbxFH": offset = 4; break;
            //    case "tbxSaH": offset = 5; break;
            //    case "tbxSuH": offset = 6; break;
            //}

            string msg = show_toff(_emp_id, (DateTime)tbx.Tag,  offset);

            tt.Show(msg, tbx, 3000);

        }


        private void cmsSched_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem i = e.ClickedItem;
            
            ContextMenuStrip cms = (ContextMenuStrip)sender;
            TextBox tbx = (TextBox)cms.SourceControl;

            DateTime d = (DateTime)tbx.Tag;

            
            //DataRow row = null;
            //DataTable dt = qrySummary.GetView("Employee", d, 1, _emp_id, null);
            //string emp_name = string.Empty;
            //string memo = string.Empty;
            //if (dt == null || dt.Rows.Count == 0)            
            //    emp_name = _emp_name;
            //else
            //{
            //    row = dt.Rows[0];
            //    emp_name = (string)row["EmpName"];
            //    memo = (string)row["Note"];
            //}

            
            DateTime day = d.Date;
            DataSet ds = dacTimebook.GetDS(d, 1);
            DataRow row = ds.Tables[0].Rows.Find(new object[] { d, _emp_id, 0 });

            string memo = string.Empty;
            if (row == null)
            {
                row = ds.Tables[0].NewRow();

                row["EmpName"] = _emp_name;
                row["LogHours"] = 0m;
                row["LogOver"] = 0m;
                row["LogVessel"] = null;

                row["LogShift"] = 0;

                row["LogNote"] = null;
            }
            else
                memo = row["LogNote"].ToString();


            switch (i.Text)
            {
                case "Note":
                    //object[] note = (object[])((Panel)sender).Tag;
                    //string memo = note[0].ToString();            
                    //DateTime day = (DateTime)note[1];
                    
                    ucNote uc = new ucNote(_emp_name, memo, day.Date);
                    uc.EditNote();

                    if (uc.IsAccept() || !uc.IsDelete())
                    {
                        if (uc.IsAccept()) memo = uc.GetMemo();

                        if (uc.IsDelete()) memo = null;

                        row["LogNote"] = memo;

                        dacTimebook.FindAdd(new object[] { d, _emp_id, 0 }, row);
                        dacTimebook.SaveData();
                        dacCache.PutTimebook();

                        if (CueRequired)
                            ((CueWeek)this.ParentForm).Msg();
                    }
    
                    break;

                default :
                    string toff = i.Text;
                    if (toff.Equals("!12")) toff = "12";

                    row["ToffCode"] = toff;
                
                    
                    dacTimebook.FindAdd(new object[] { d, _emp_id, 0 }, row);
                    dacTimebook.SaveData();
                    dacCache.PutTimebook();

                    if (CueRequired)
                        ((CueWeek)this.ParentForm).Msg();

                    break;

            }
        }

    }
}
