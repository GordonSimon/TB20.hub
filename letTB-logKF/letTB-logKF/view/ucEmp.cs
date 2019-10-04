using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Diagnostics;


namespace letTB_logKF
{
    public partial class ucEmp : UserControl
    {
        public DataTable DT { get; set; }        
        public DateTime DAY { get; set; }
        public string EMP { get; set; }

        private List<string> _boat { get; set; }
        private Dictionary<string, int> _boats { get; set; }
        

        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public ucEmp()
        {
            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        }



        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void show_boats(DataTable dt)
        {
            if (_boats == null) _boats = new Dictionary<string, int>();

            foreach (DataRow row in dt.Rows)
            {
                string boat = null;
                if (!row["LogVessel"].Equals(DBNull.Value)) boat = (string)row["LogVessel"];

                int shift = ((int)row["LogShift"]);

                if (boat != null)
                    _boats.Add(boat, shift);
            }

            foreach (var b in _boats)
            {
                int shift = b.Value;

                Color bk = Color.Green;
                if (shift == 1) bk = Color.Blue;
                if (shift == 2) bk = Color.Black;


                Label lbl = new Label();
                lbl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                lbl.BackColor = bk;
                lbl.ForeColor = Color.White;
                lbl.Size = new Size(lbl.Size.Width - 20, lbl.Size.Height);
                lbl.TextAlign = ContentAlignment.TopCenter;

                lbl.Tag = shift;
                lbl.Text = b.Key;

                flpBoats.Controls.Add(lbl);
            }
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void map_head()
        {
            //string line1 = string.Format("{0, -6} {1, -21} {2, -10} {3, -7}{4, 1} {5, 6} {6, 6} {7, 5} {8, 5}", 
            //    "EmpId", "Employee Name", "Log Date", "Paycode", " ", "Actual", "Strght", "OT2", "OT1");
            //string line2 = string.Format("{0, -6} {1, -21} {2, -10} {3, -7}{4, 1} {5, 6} {6, 6} {7, 5} {8, 5}",
            //    new string('-', 6), new string('-', 21),
            //    new string('-', 10), new string('-', 7), '-', new string('-', 6), new string('-', 6), new string('-', 5), new string('-', 5));

            string line1 = string.Format("{0, -6} {1, -19} {2, -10} {3, -11} {4, 6} {5, 6} {6, 4} {7, 4}",
                "EmpId", "Employee Name", "Log Date", "Paycode", "Actual", "Strght", "OT2", "OT1");
            string line2 = string.Format("{0, -6} {1, -19} {2, -10} {3, -11} {4, 6} {5, 6} {6, 4} {7, 4}",
                new string('-', 6), new string('-', 19),
                new string('-', 10), new string('-', 11),  new string('-', 6), new string('-', 6), new string('-', 4), new string('-', 4));
            
            lbxEmp.Items.Add(line1);
            lbxEmp.Items.Add(line2);
        }


        private void map_sum(DataRow row, total tot)
        {
            string actual = tot.a.ToString("#.##");
            string overtime = tot.o.ToString("#.##");
            string paid = tot.p.ToString("#.##");
            string overtime1 = tot.ot1.ToString("#.##");

            if (tot.hourly_)
            {
                //actual = (tot.a * 8.0M).ToString("#.#");
                //overtime = (tot.o * 8.0M).ToString("#.#");
                //paid = (tot.p * 8.0M).ToString("#.#");
                //overtime1 = (tot.ot1 * 8.0M).ToString("#.#");
            }

            
            string line1 = string.Format("{0, -6} {1, -19} {2, -10} {3, -11} {4, 6} {5, 6} {6, 4} {7, 4}",
                new string(' ', 6), new string(' ', 19),
                new string(' ', 10), new string(' ', 11), new string('=', 6), new string('=', 6), new string('=', 4), new string('=', 4));
            string line2 = string.Format("{0, -6} {1, -19} {2, -10} {3, -11} {4, 6} {5, 6} {6, 4} {7, 4}",
                            new string(' ', 6), new string(' ', 19),
                            new string(' ', 10), new string(' ', 11), actual, paid, overtime, overtime1);

            lbxEmp.Items.Add(line1);
            lbxEmp.Items.Add(line2);
        }


        private void map_emp(string resp, string paycode, string emp_id, string emp_name, int shift, string actual, string overtime, string overtime1, string paid, DateTime bookdate, string boat)
        {
            string code = resp;
            //if (!boat.Equals("")) code = string.Format("{0, -5}{1, 1}", boat, resp);
            if (!boat.Equals(""))
                code = string.Format("{0}/{1}", boat, resp);
            else
                code = paycode;

            string line = string.Format("{0, -6} {1, -19} {2, -10} {3, -11} {4, 6} {5, 6} {6, 4} {7, 4}", 
                emp_id, emp_name, bookdate.ToShortDateString(), code, actual, paid, overtime, overtime1 );

            lbxEmp.Items.Add(line);
        }


        private void map_row(DataRow row, string paycode)
        {
            DateTime bookdate = (DateTime)row["BookDate"];

            decimal a = ((decimal)row["LogHours"]);
            decimal o = ((decimal)row["LogOver"]);
            decimal p = a - o;

            //GS180316 - fix for Staff OverTime
            if (a == 0) p = 0;


            decimal ot1 = 0.0M;
            if (!row["LogOver1"].Equals(DBNull.Value)) ot1 = ((decimal)row["LogOver1"]);

            int logshift = (int)row["LogShift"];
            //if (logshift >= 10) MessageBox.Show("Stop");

            string boat = "";
            string resp = "Salary";
            if (!row["LogVessel"].Equals(DBNull.Value))
            {
                boat = (string)row["LogVessel"];
                resp = (string)row["Resp"];
                if (resp.Trim().Equals("Mate") || resp.Trim().Equals("Mate2"))
                    resp = "M";
                else
                    resp = (resp.Trim().Equals("Capt") ? resp = "S" : resp = "D");
                
            }
            else
            {
                if (o == 0.0M && ot1 == 0.0M && logshift == 0)
                {
                    decimal hour_per_day = 8.0M;
                    if (paycode.Equals("Office")) hour_per_day = 7.0M;
                    if (paycode.Equals("Dispatch"))  hour_per_day = 12.0M;

                    a = a * hour_per_day;
                    p = p * hour_per_day;
                }
            }

            //else
            //{
                //a = a * 8.0M;
                //o = o * 8.0M;
                //p = p * 8.0M;
                //ot1 = ot1 * 8.0M;
            //}

            string actual = a.ToString("#.##");
            string overtime = o.ToString("#.##");
            string paid = p.ToString("#.##");
            string overtime1 = ot1.ToString("#.##");
            bool regular = (a.Equals(12M) && a.Equals(p));            

            int shift = (int)row["LogShift"];
            string emp_id = (string)row["EmpId"];
            
            string emp_name = (string)row["EmpName"];
               
            map_emp(resp, paycode, emp_id, emp_name, shift, actual, overtime, overtime1, paid, bookdate, boat);        
        }


        private void sum_row(DataRow row, total tot, string paycode)
        {
            decimal a = ((decimal)row["LogHours"]);
            decimal o = ((decimal)row["LogOver"]);
            decimal p = a - o;

            //GS180316 - fix for Staff OverTime
            if (a == 0) p = 0;

            decimal ot1 = 0.0M;
            if (!row["LogOver1"].Equals(DBNull.Value)) ot1 = ((decimal)row["LogOver1"]);

            int logshift = (int)row["LogShift"];

            string boat = "";
            string resp = "Salary";
            if (!row["LogVessel"].Equals(DBNull.Value))
            {
                boat = (string)row["LogVessel"];
                resp = (string)row["Resp"];
                resp = (resp.Trim().Equals("Capt") ? resp = "S" : resp = "D");
            }
            else
            {
                if (o == 0.0M && ot1 == 0.0M && logshift == 0)
                {
                    decimal hour_per_day = 8.0M;
                    if (paycode.Equals("Office")) hour_per_day = 7.0M;
                    if (paycode.Equals("Dispatch")) hour_per_day = 12.0M;

                    a = a * hour_per_day;
                    p = p * hour_per_day;
                }
            }

            //else
            //{
            //a = a * 8.0M;
            //o = o * 8.0M;
            //p = p * 8.0M;
            //ot1 = ot1 * 8.0M;
            //}

            tot.a += a;
            tot.o += o;
            tot.p += p;
            tot.ot1 += ot1;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void draw_emp(DataTable dt, DateTime d, string emp_id)
        {
            try
            {
                total tot = new total();
                tot.hourly_ = false;
                tot.a = 0M; tot.p = 0M; tot.p = 0M; tot.ot1 = 0M;
                 
                string sql = string.Format("BookDate = '{0}' and EmpName = '{1}'", d, emp_id);
                DataTable v = (new DataView(dt, sql, null, System.Data.DataViewRowState.CurrentRows)).ToTable();

                if (v == null) return;

                DataRow headrow = v.Rows[0];
                //string duty = (string)headrow["Duty"];
                string defpaycode = (string)headrow["DefPayCode"];

                //if (emp_id.Equals("HUNTER, Yasser")) MessageBox.Show("Hunter");

                map_head();
                foreach (DataRow row in v.Rows)
                {
                    string paycode = defpaycode;
                    if (!row["PayCode"].Equals(DBNull.Value)) paycode = (string)row["Paycode"];

                    decimal ot1 = 0.0M;
                    if (!row["LogOver1"].Equals(DBNull.Value)) ot1 = (decimal)row["LogOver1"];

                    if (row["LogVessel"].Equals(DBNull.Value) && (decimal)row["LogHours"] == 0.0M
                        && (decimal)row["LogOver"] == 0.0M && ot1 == 0.0M)
                    {
                         //MessageBox.Show("Error : empty record <LogHours == 0, but LogOver == 0 and LogOver1 == 0");
                    }
                    else
                    {
                        map_row(row, paycode);
                        sum_row(row, tot, paycode);
                    }

                    //if (!row["LogVessel"].Equals(DBNull.Value))
                    //{
                    //    if (tot.hourly_) MessageBox.Show("Error : Conversion to hourly detected !");
                    //    map_row(row);
                    //    sum_row(row, tot);
                    //}
                    //if (row["LogVessel"].Equals(DBNull.Value) && (decimal)row["LogHours"] != 0.0M)
                    //{
                    //    tot.hourly_ = true;
                    //    map_row(row);
                    //    sum_row(row, tot);
                    //}
                    //if (row["LogVessel"].Equals(DBNull.Value) && (decimal)row["LogHours"] == 0.0M
                    //    && (decimal)row["LogOver"] != 0.0M)
                    //{
                    //    MessageBox.Show("Error : LogHours = 0, but LogOver != 0 !");
                    //}

                }
                map_sum(v.Rows[0], tot);

            }
            catch (Exception ex)
            {
                errDash.Fail(System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
        }


        private void draw_emp(DataTable dt, DateTime d, DateTime f, string emp_id)
        {
            //try
            //{
                total tot = new total();
                tot.hourly_ = false;
                tot.a = 0M; tot.p = 0M; tot.p = 0M; tot.ot1 = 0M;

                //string sql = string.Format("BookDate = '{0}' and EmpName = '{1}'", d, emp_id);
                string sql = string.Format("EmpName = '{0}' and BookDate >= '{1}' and BookDate <= '{2}'", emp_id, d.Date, f.Date);
                DataTable v = (new DataView(dt, sql, null, System.Data.DataViewRowState.CurrentRows)).ToTable();

                if (v == null) return;

                DataRow headrow = v.Rows[0];
                //string duty = (string)headrow["Duty"];
                string defpaycode = (string)headrow["DefPayCode"];
    
                map_head();
                foreach (DataRow row in v.Rows)
                {
                    string paycode = defpaycode;
                    if (!row["PayCode"].Equals(DBNull.Value)) paycode = (string)row["Paycode"];

                    decimal ot1 = 0.0M;
                    if (!row["LogOver1"].Equals(DBNull.Value)) ot1 = (decimal)row["LogOver1"];
                    if (row["LogVessel"].Equals(DBNull.Value) && (decimal)row["LogHours"] == 0.0M
                        && (decimal)row["LogOver"] == 0.0M && ot1 == 0.0M )
                    {
                         //MessageBox.Show("Error : empty record <LogHours == 0, but LogOver == 0 and LogOver1 == 0");
                    }
                    else
                    {
                        map_row(row, paycode);
                        sum_row(row, tot, paycode);
                    }

                    //if (!row["LogVessel"].Equals(DBNull.Value))
                    //{
                    //    if (tot.hourly_) MessageBox.Show("Error : Conversion to hourly detected !");
                    //    map_row(row);
                    //    sum_row(row, tot);
                    //}
                    //if (row["LogVessel"].Equals(DBNull.Value) && (decimal)row["LogHours"] != 0.0M)
                    //{
                    //    tot.hourly_ = true;
                    //    map_row(row);
                    //    sum_row(row, tot);
                    //}
                    //if (row["LogVessel"].Equals(DBNull.Value) && (decimal)row["LogHours"] == 0.0M
                    //    && (decimal)row["LogOver"] != 0.0M)
                    //{
                    //    MessageBox.Show("Error : LogHours = 0, but LogOver != 0 !");
                    //}

                    //decimal ot1 = 0.0M;
                    //if (!row["LogOver1"].Equals(DBNull.Value)) ot1 = (decimal)row["LogOver1"];

                    //if (row["LogVessel"].Equals(DBNull.Value) && (decimal)row["LogHours"] == 0.0M
                    //    && ot1 != 0.0M)
                    //{
                    //    MessageBox.Show("Error : LogHours = 0, but LogOver1 != 0 !");
                    //}
                }
            try {
                map_sum(v.Rows[0], tot);

            }
            catch (Exception ex)
            {
                errDash.Fail(System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
        }



        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public void LoadEmp(DataTable dt, DateTime d, string emp_id)
        {
            DT = dt;
            DAY = d;
            EMP = emp_id;


            lblEmp.Text = emp_id;

            this.Refresh();
            Application.DoEvents();

            string sql = string.Format("BookDate = '{0}' and EmpName = '{1}'", d, emp_id);            
            DataTable v = (new DataView(dt, sql, null, System.Data.DataViewRowState.CurrentRows)).ToTable();

            dgvEmp.DataSource = v;

            this.Refresh();
            Application.DoEvents();

            show_boats(v);

            DataRow row = v.Rows[0];
            string duty = row["Duty"].ToString();
            lblEID.Text = duty;

            draw_emp(dt, d, emp_id);
        }


        public void LoadEmp(DataTable dt, DateTime d, DateTime f, string emp_id)
        {
            DT = dt;
            DAY = d;
            EMP = emp_id;


            lblEmp.Text = emp_id;

            this.Refresh();
            Application.DoEvents();
            
            //string sql = string.Format("BookDate = '{0}' and EmpName = '{1}'", d, emp_id);
            string sql = string.Format("EmpName = '{0}' and BookDate >= '{1}' and BookDate <= '{2}'", emp_id, d.Date, f.Date);
            DataTable v = (new DataView(dt, sql, null, System.Data.DataViewRowState.CurrentRows)).ToTable();

            dgvEmp.DataSource = v;

            this.Refresh();
            Application.DoEvents();
            
            //show_boats(v);

            DataRow row = v.Rows[0];
            string duty = row["Duty"].ToString();
            lblEID.Text = duty;

            draw_emp(dt, d, f, emp_id);
        }

        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public string[] OutArray()
        {
            string[] a = new string[lbxEmp.Items.Count];
            lbxEmp.Items.CopyTo(a, 0);

            return a;
        }


        public string Output()
        {  
            StringBuilder sb = new StringBuilder();
            foreach (var line in lbxEmp.Items)
                sb.AppendLine(line.ToString());

            return sb.ToString();
        }


        public void Print(string fname)
        {
            string sPath = fname;

            System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(sPath);
            foreach (var line in lbxEmp.Items)            
                SaveFile.WriteLine(line);            
            SaveFile.Close();

            Process.Start("notepad.exe", sPath);           
        }
    }


    /*******************************************************************************************************************\
     *                                                                                                                 *
    \*******************************************************************************************************************/

    class total { public bool hourly_; public decimal a; public decimal p; public decimal o; public decimal ot1; };

}
