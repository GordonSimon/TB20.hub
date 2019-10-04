using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data;
using System.IO;
using System.Diagnostics;


namespace letTB_logKF
{
    public partial class frmMain : Form
    {
        private DataTable _dt { get; set; }
        private BindingSource _bs = null;


        private Dictionary<string, string> _code;

        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void set_codes()
        {
            if (_code == null) _code = new Dictionary<string, string>();

            _code.Add("HR", "HR"); //1
            _code.Add("HK", "HKing"); //2 1
            _code.Add("HP", "HPride"); //3 3
            _code.Add("HN", "HN"); //4 2

            _code.Add("J", "HR"); //5
            _code.Add("JR", "HR"); //6
            _code.Add("KG", "HR"); //7

            _code.Add("PWY", "Pway"); //8
            _code.Add("RAS", "Rasp"); //9 4
            _code.Add("REB", "Rebel"); //10 6
            _code.Add("RB", "RBrave"); //11 5
            _code.Add("RE", "RE"); //12
            _code.Add("RN", "RN"); //13 7
            _code.Add("RR", "RRaider"); //14 8
            _code.Add("RS", "RStar"); //15 9
            _code.Add("RY", "RY"); //16

            
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public frmMain()
        {
            InitializeComponent();

            this.Text += string.Format(" [V{0}]", Gap.AssemblyVersion);

            this.WindowState = FormWindowState.Maximized;
            //calLog.TodayDate = DateTime.Now.AddDays(-21);
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/
        
        private void init_load()
        {
            DateTime d = calLog.TodayDate.Date;

            load_n_filter(d);
            show_boats(d);
            show_crew(d);
            show_shifts(d);
        }


        private void init_logs()
        {
            DateTime d = calLog.TodayDate.Date;

            if (_dt == null) return;

            load_logs(d);

        }


        private void init_months()
        {
            if (_dt == null) return;

            load_months();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void load_months()
        {
            //calLog.RemoveAllBoldedDates();

            DataTable dt = qryTimebook.qDays(_dt);

            foreach (DataRow row in dt.Rows)            
                calLog.AddBoldedDate((DateTime)row["BookDate"]);

            calLog.UpdateBoldedDates();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void load_n_filter(DateTime d)
        {
            string sql = string.Format("BookDate = '{0}'", d);
            _dt = dacTimebook.GetDT();
            _dt.DefaultView.RowFilter = sql;
        }


        private void show_boats(DateTime d)
        {
            flpBoats.Controls.Clear();
            
            DataTable boats = qryTimebook.qBoats(_dt, d);
            foreach (DataRow row in boats.Rows)
            {
                Button btn = new Button();
                btn.Text = (string)row["LogVessel"];
                btn.Click += new EventHandler(btn_Click);

                flpBoats.Controls.Add(btn);
            }
        }


        private void show_boats(DateTime d, DateTime f)
        {
            flpBoats.Controls.Clear();

            DataTable boats = qryTimebook.qBoats(_dt, d, f);
            foreach (DataRow row in boats.Rows)
            {
                Button btn = new Button();
                btn.Text = (string)row["LogVessel"];
                btn.Click += new EventHandler(btn_Click);

                flpBoats.Controls.Add(btn);
            }
        }


        private void show_shifts(DateTime d)
        {
            //flpShifts.Controls.Clear();

            DataTable shifts = qryTimebook.qShifts(_dt, d);
            foreach (DataRow row in shifts.Rows)
            {
                Button btn = new Button();
                btn.Text = string.Format("{0}.{1}", (string)row["LogVessel"], row["LogShift"].ToString());
                //btn.Click += new EventHandler(btn_Click);

                //flpShifts.Controls.Add(btn);
            }
        }


        private void show_crew(DateTime d)
        {
            lbxEmp.Items.Clear();

            DataTable emps = qryTimebook.qCrew(_dt, d);            
            foreach (DataRow row in emps.Rows)
            {
                lbxEmp.Items.Add(row["EmpName"]);
            }
        }


        private void show_crew(DateTime d, DateTime f)
        {
            lbxEmp.Items.Clear();

            DataTable emps = qryTimebook.qCrew(_dt, d, f);
            foreach (DataRow row in emps.Rows)
            {
                lbxEmp.Items.Add(row["EmpName"]);
            }

        }

        private void show_staff(DateTime d)
        {
            lbxEmp.Items.Clear();

            DataTable emps = qryTimebook.qStaff(_dt, d);
            foreach (DataRow row in emps.Rows)
            {
                lbxEmp.Items.Add(row["EmpName"]);
            }
        }


        private void show_staff(DateTime d, DateTime f)
        {
            lbxEmp.Items.Clear();

            DataTable emps = qryTimebook.qStaff(_dt, d, f);
            foreach (DataRow row in emps.Rows)
            {
                lbxEmp.Items.Add(row["EmpName"]);
            }
        }

        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        //GS161113 - not used
        private void load_emp(DateTime d, DateTime f, string emp_id)
        {
            flpEmps.Controls.Clear();

            ucEmp uc = new ucEmp();

            //uc.LoadEmp(_dt, d, emp_id);
            //flpEmps.Controls.Add(uc);


            uc.LoadEmp(_dt, d, f, emp_id);
            flpEmps.Controls.Add(uc);
        }


        private void load_emps_selected(DateTime d, DateTime f)
        {
            flpEmps.Controls.Clear();

            foreach (string e in lbxEmp.SelectedItems)
            {
                ucEmp uc = new ucEmp();

                uc.LoadEmp(_dt, d, f, e);
                flpEmps.Controls.Add(uc);
            }
        }


        private void load_emps(DateTime d)
        {
            flpEmps.Controls.Clear();

            foreach (string e in lbxEmp.Items)
            {
                ucEmp uc = new ucEmp();

                uc.LoadEmp(_dt, d, e);
                flpEmps.Controls.Add(uc);
            }
        }


        private void load_emps(DateTime d, DateTime f)
        {
            flpEmps.Controls.Clear();

            foreach (string e in lbxEmp.Items)
            {
                ucEmp uc = new ucEmp();

                uc.LoadEmp(_dt, d, f, e);
                flpEmps.Controls.Add(uc);
            }
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void load_log(DateTime d, string boat)
        {
            flpLogs.Controls.Clear();

            ucLog uc = new ucLog();

            uc.LoadLog(_dt, d, boat);
            flpLogs.Controls.Add(uc);
        }


        private void load_logs(DateTime d)
        {
            flpLogs.Controls.Clear();

            foreach (Button b in flpBoats.Controls)
            {
                ucLog uc = new ucLog();

                uc.LoadLog(_dt, d, b.Text);
                flpLogs.Controls.Add(uc);
            }
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        void btn_Click(object sender, EventArgs e)
        {
            DateTime d = calLog.SelectionStart;

            Button btn = (Button)sender;

            load_log(d, btn.Text);

            cmdLogs_Click(null, null);
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void view_crew(DateTime d, DateTime f)
        {
            if (d.Date.Equals(f.Date))
            {
                //cmdLogs_Click(null, null);

                //GS181202 - filter for other
                //string sql = string.Format("BookDate = '{0}' and Duty <> 'Office'", d);
                string sql = string.Format("BookDate = '{0}' and (Duty <> 'Office' or Duty <> 'Other')", d);
                _dt.DefaultView.RowFilter = sql;

                //DataTable v = (new DataView(_dt, sql, null, System.Data.DataViewRowState.CurrentRows)).ToTable();

                show_boats(d);
                load_logs(d);
                show_crew(d);
                show_shifts(d);

                load_emps(d);
            }
            else
            {
                //cmdCrew_Click(null, null);

                //GS181202 - filter for other
                //string sql = string.Format("BookDate >= '{0}' and BookDate <= '{1}' and Duty <> 'Office'", d.Date, f.Date);
                string sql = string.Format("BookDate >= '{0}' and BookDate <= '{1}' and (Duty <> 'Office' or Duty <> 'Other') ", d.Date, f.Date);
                _dt.DefaultView.RowFilter = sql;

                show_boats(d, f);
                //load_logs(d);
                show_crew(d, f);
                //show_shifts(d);

                load_emps(d, f);
            }
        }


        private void view_staff(DateTime d, DateTime f)
        {
            if (d.Date.Equals(f.Date))
            {
                //cmdLogs_Click(null, null);

                //GS181202 - filter for other
                //string sql = string.Format("BookDate = '{0}' and Duty = 'Office'", d);
                string sql = string.Format("BookDate = '{0}' and (Duty = 'Office' or Duty = 'Other')", d);
                _dt.DefaultView.RowFilter = sql;

                flpBoats.Controls.Clear();
                load_logs(d);
                show_staff(d);
                show_shifts(d);

                load_emps(d);
            }
            else
            {
                //cmdCrew_Click(null, null);

                //GS181202 - filter for other
                //string sql = string.Format("BookDate >= '{0}' and BookDate <= '{1}' and Duty = 'Office'", d.Date, f.Date);
                string sql = string.Format("BookDate >= '{0}' and BookDate <= '{1}' and (Duty = 'Office' or Duty = 'Other') ", d.Date, f.Date);
                _dt.DefaultView.RowFilter = sql;

                ////flpBoats.Controls.Clear();
                //load_logs(d);
                show_staff(d, f);
                //show_shifts(d);

                load_emps(d, f);
            }
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void frmMain_Load(object sender, EventArgs e)
        {
            set_codes();

            init_load();
            init_logs();
            init_months();
        }


        private void calLog_DateSelected(object sender, DateRangeEventArgs e)
        {
            mode_busy();

            //MonthCalendar cal = (MonthCalendar)sender;
            //cal.Enabled = false;

            //DateTime d = calLog.SelectionStart;
            //DateTime f = calLog.SelectionEnd;

            DateTime d = e.Start;
            DateTime f = e.End;

            if (lblPayroll.Text.Equals("Crew"))
                view_crew(d, f);
            else
                view_staff(d, f);

            //cal.Enabled = true;

            mode_ready();
        }


        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            int w = this.Size.Width;

            int fw = this.flpLogs.Size.Width + this.flpEmps.Size.Width;

            if (w > fw)
            {
                this.lblPayroll.Show();
                this.flpEmps.Show();
            }
            else
            {
                this.lblPayroll.Hide();
                this.flpEmps.Hide();
            }
        }


        private void lbxEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ListBox lbx = (ListBox)sender;

            cmdView.Visible = true;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/


        private void mode_busy()
        {
            cmdLogs.Enabled = false;
            cmdCrew.Enabled = false;
            cmdStaff.Enabled = false;

            cmdNew.Enabled = false;
            cmdView.Enabled = false;
            cmdExport.Enabled = false;
            cmdPrint.Enabled = false;

            calLog.Enabled = false;
            lbxEmp.Enabled = false;

            this.UseWaitCursor = true;
        }


        private void mode_ready()
        {
            cmdLogs.Enabled = true;
            cmdCrew.Enabled = true;
            cmdStaff.Enabled = true;

            cmdNew.Enabled = true;
            cmdView.Enabled = true;
            cmdExport.Enabled = true;
            cmdPrint.Enabled = true;

            calLog.Enabled = true;
            lbxEmp.Enabled = true;

            this.UseWaitCursor = false;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void cmdLogs_Click(object sender, EventArgs e)
        {
            lblPayroll.Hide();
            flpEmps.Hide();

            lblLogs.Show();
            flpLogs.Show();            
        }


        private void cmdCrew_Click(object sender, EventArgs e)
        {
            mode_busy();

            DateTime d = calLog.SelectionStart;
            DateTime f = calLog.SelectionEnd;

            view_crew(d, f);

            flpLogs.Hide();
            lblLogs.Hide();

            lblPayroll.Text = "Crew";
            lblPayroll.Location = lblLogs.Location;
            flpEmps.Location = flpLogs.Location;

            lblPayroll.Show();            
            flpEmps.Show();

            mode_ready();
        }


        private void cmdStaff_Click(object sender, EventArgs e)
        {
            mode_busy();

            lblPayroll.Text = "Staff";
            lblPayroll.Location = lblLogs.Location;
            flpEmps.Location = flpLogs.Location;

            DateTime d = calLog.SelectionStart;
            DateTime f = calLog.SelectionEnd;

            view_staff(d, f);

            flpLogs.Hide();
            lblLogs.Hide();

            //lblPayroll.Text = "Staff";
            //lblPayroll.Location = lblLogs.Location;
            //flpEmps.Location = flpLogs.Location;

            //lblPayroll.Show();
            //flpEmps.Show();

            mode_ready();
        }

        //private void cmdLoad_Click(object sender, EventArgs e)
        //{
        //    init_load();

        //    //_bs = new BindingSource(_dt, _dt.TableName);
        //    //bnvCRUD.BindingSource = _bs;
        //    //dgvCRUD.DataSource = _bs;            
        //}


        //private void cmdShift_Click(object sender, EventArgs e)
        //{
        //    init_logs();

        //    //ucLog uc = new ucLog();                        
        //    //uc.Load(dt);
        //    //flpLogs.Controls.Add(uc);
        //}



        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private string make_csv_crew(string path, string fname)
        {
            DateTime d = calLog.SelectionStart;
            DateTime f = calLog.SelectionEnd;

            var csv = new StringBuilder();

            DataView dv = _dt.DefaultView;
            //dv.Sort = "EmpName, Resp, LogVessel";
            dv.Sort = "EmpId, Resp, LogVessel";

            // GS171113 - switch to multiple select
            //string selected_emp_name = string.Empty;
            //if (lbxEmp.SelectedIndex >= 0)
            //    selected_emp_name = lbxEmp.SelectedItems[0].ToString();

            List<string> selected_emps = new List<string>();
            foreach (var emp in lbxEmp.SelectedItems)  selected_emps.Add(emp.ToString());

            DataTable sdt = dv.ToTable();

            //string last_empid = string.Empty;
            //string last_code = string.Empty;
            //decimal tot_straight = 0.0M;
            //decimal tot_ot2 = 0.0M;

            if (selected_emps.Count > 0)
            {
                foreach (DataRow row in sdt.Rows)
                {
                    DateTime bookdate = (DateTime)row["BookDate"];

                    string emp_id = (string)row["EmpId"];
                    string emp_name = (string)row["EmpName"];
                    //GS171113 - switch to multiple select
                    //if (!selected_emp_name.Equals(string.Empty) && !selected_emp_name.Equals(emp_name)) continue;

                    if (! selected_emps.Contains(emp_name)) continue;
                    //if (emp_name.ToLower().StartsWith("harmon")) MessageBox.Show("Randy");

                    //if (bookdate.CompareTo(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)) == -1) continue;
                    if (bookdate.CompareTo(d.Date) == -1) continue;
                    if (bookdate.CompareTo(f.Date) == 1) continue;

                    //if (!row["Duty"].Equals(DBNull.Value) && row["Duty"].Equals("Deckhand"))
                    //if (!row["Duty"].Equals(DBNull.Value))
                    //{
                    //    //if (row["Duty"].Equals("Deckhand")) continue;
                    //}
                    //else
                    //{
                        if (row["Duty"].Equals("Office")) continue;

                        //GS181202 - don't process when no Vessel
                        //if (row["LogVessel"].Equals(DBNull.Value)) continue;
                        //if (!row["LogVessel"].Equals(DBNull.Value)) MessageBox.Show(row["LogVessel"].ToString());
                    //}

                    decimal a = ((decimal)row["LogHours"]);
                    decimal o = ((decimal)row["LogOver"]);
                    decimal p = a - o;

                    decimal o1 = 0.0M;
                    if (!row["LogOver1"].Equals(DBNull.Value)) o1 = ((decimal)row["LogOver1"]);

                    //string boat = (string)row["LogVessel"];
                    string boat = null;

                    int shift = (int)row["LogShift"];
                    //string emp_id = (string)row["EmpId"];

                    //string emp_name = (string)row["EmpName"];
                    //string resp = (string)row["Resp"];
                    string actual = a.ToString("0.##");
                    string overtime = o.ToString("0.##");
                    string overtime1 = o1.ToString("0.##");
                    string paid = p.ToString("0.##");
                    bool regular = (a.Equals(12M) && a.Equals(p));

                    string paycode = "Other";
                    if (row["LogVessel"].Equals(DBNull.Value))
                    {
                        if (!row["PayCode"].Equals(DBNull.Value))
                            paycode = (string)row["PayCode"];

                        Decimal s = a + o + o1;
                        if (s == 0.0M) continue;
                    }
                    else
                    {
                        boat = (string)row["LogVessel"];
                        string resp = ((string)row["Resp"]).Trim();

                        string code1 = "DH ";
                        if (resp.Equals("Capt")) code1 = "SK ";
                        if (resp.Equals("Mate")) code1 = "MATE ";
                        if (resp.Equals("Mate2")) code1 = "MATE ";

                        string code2 = "<boat>";
                        if (_code.ContainsKey(boat)) code2 = _code[boat];

                        paycode = code1 + code2;
                    }

                    //if (emp_id.Equals("HR8201"))
                    //{
                    //    MessageBox.Show("HR8201");
                    //    if (paycode.Equals("Charting 2"))
                    //        MessageBox.Show("charting");
                    //}

                    // make totals
                    //if (last_empid.Equals(emp_id) && last_code.Equals(code))
                    //{
                    //    tot_straight += p;
                    //    tot_ot2 += o;
                    //}
                    //else
                    //{
                    //    last_empid = emp_id;
                    //    last_code = code;
                    //    tot_straight = p;
                    //    tot_ot2 = o;
                    //}
                    //string straight = tot_straight.ToString("0.#");
                    //string ot2 = tot_ot2.ToString("0.#");


                    //var line = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}{10}",
                    //    bookdate.ToShortDateString(), emp_id, emp_name, boat, shift, actual, resp, code, paid, overtime, Environment.NewLine);
                    //var line = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}{10}",
                    //    emp_name, code, bookdate.ToShortDateString(), emp_id, boat, resp, shift, actual, paid, overtime, Environment.NewLine);

                    //var line = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}{8}",
                    //    emp_id, emp_name, code, straight, ot2, paid, overtime, bookdate.ToShortDateString(), Environment.NewLine);
                    //var line = string.Format("{0},{1},{2},{3},{4},{5},{6}",
                    //    emp_id, emp_name, paycode, paid, overtime, overtime1, Environment.NewLine);

                    var line = string.Format("{0},{1},{2},{3},{4},{5},{6}",
                        emp_id, emp_name, paycode, paid, overtime1, overtime, Environment.NewLine);


                    csv.Append(line);
                }
            }

            //string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = string.Format("{0}/{1}", path, fname);
            File.WriteAllText(filePath, csv.ToString());

            return filePath;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private string make_csv_staff(string path, string fname)
        {
            var csv = new StringBuilder();

            try
            {
                DateTime d = calLog.SelectionStart;
                DateTime f = calLog.SelectionEnd;

                DataView dv = _dt.DefaultView;
                dv.Sort = "EmpId, BookDate";

                // GS180131 - switch to multiple select
                //string selected_emp_name = string.Empty;
                //if (lbxEmp.SelectedIndex >= 0)
                //    selected_emp_name = lbxEmp.SelectedItems[0].ToString();

                List<string> selected_emps = new List<string>();
                foreach (var emp in lbxEmp.SelectedItems) selected_emps.Add(emp.ToString());

                DataTable sdt = dv.ToTable();

                if (selected_emps.Count > 0)
                {
                    foreach (DataRow row in sdt.Rows)
                    {
                        DateTime bookdate = (DateTime)row["BookDate"];

                        int rcd_id = (int)row["ID"];
                        string emp_id = (string)row["EmpId"];
                        string emp_name = (string)row["EmpName"];
                        //GS180131 - switch to multiple select
                        //if (!selected_emp_name.Equals(string.Empty) && !selected_emp_name.Equals(emp_name)) continue;

                        if (!selected_emps.Contains(emp_name)) continue;

                        if (bookdate.CompareTo(d.Date) == -1) continue;
                        if (bookdate.CompareTo(f.Date) == 1) continue;

                        //if (!row["Duty"].Equals("Office")) continue;
                        if (!row["Duty"].Equals("Office") && !row["Duty"].Equals("Other") ) continue;
                        if (!row["LogVessel"].Equals(DBNull.Value)) continue;

                        if (row["LogHours"].Equals(DBNull.Value)) continue;

                        decimal a = ((decimal)row["LogHours"]);
                        //if (a > 1) MessageBox.Show(a.ToString()); //GS180601

                        decimal o = ((decimal)row["LogOver"]);
                        decimal p = a - o;

                        int logshift = (int)row["LogShift"];

                        decimal ot1 = 0.0M;
                        if (!row["LogOver1"].Equals(DBNull.Value)) ot1 = ((decimal)row["LogOver1"]);
                        //decimal o1 = 0.0M;
                        //if (!row["LogOver1"].Equals(DBNull.Value))  o1 = ((decimal)row["LogOver"]);

                        if (a == 0.0M && o == 0.0M && ot1 == 0.0M) continue;

                        string defpaycode = (string)row["DefPayCode"];
                        string paycode = defpaycode;
                        if (!row["PayCode"].Equals(DBNull.Value))
                            paycode = (string)row["PayCode"];

                        if (o == 0.0M && ot1 == 0.0M && logshift == 0)
                        {
                            decimal hour_per_day = 8.0M;
                            if (paycode.Equals("Office")) hour_per_day = 7.0M;
                            if (paycode.Equals("Dispatch")) hour_per_day = 12.0M;

                            a = a * hour_per_day;
                            p = p * hour_per_day;
                        }

                        //if (paycode.Equals("Salary"))
                        //{
                        //    a = a * 8.0M;
                        //    p = p * 8.0M;

                        //    if (o != 0.0M || ot1 != 0.0M) MessageBox.Show("Error : For Salary employee; mix of day & hour !");
                        //}
                        //a = a * 8.0M;
                        //o = o * 8.0M;
                        //p = p * 8.0M;
                        //o1 = o1 * 8.0M;

                        //string emp_id = (string)row["EmpId"];
                        //string emp_name = (string)row["EmpName"];

                        string actual = a.ToString("0.#");
                        string overtime = o.ToString("0.#");
                        string overtime1 = ot1.ToString("0.#");
                        string paid = p.ToString("0.#");
                        bool regular = (a.Equals(12M) && a.Equals(p));

                        //var line = string.Format("{0},{1},{2},{3},{4},{5},{6}",
                        //    emp_id, emp_name, paycode, paid, overtime, overtime1, Environment.NewLine);

                        var line = string.Format("{0},{1},{2},{3},{4},{5},{6}",
                            emp_id, emp_name, paycode, paid, overtime1, overtime, Environment.NewLine);

                        csv.Append(line);
                    }
                }
            }
            catch (Exception ex)
            {
                csv.Append(ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.Message, "make_csv_staff");
            }

            //string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = string.Format("{0}/{1}", path, fname);
            File.WriteAllText(filePath, csv.ToString());

            return filePath;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void export_csv(bool crew_)
        {
            string filename = "payDirtStaff";
            if (crew_) filename = "payDirtCrew";
            
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + filename + ".csv";


            try
            {
                if (System.Windows.Forms.SystemInformation.TerminalServerSession)
                {
                    if (Gap.Path.Equals("<none>"))
                        Gap.SetPath(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));


                    fbdCSV.SelectedPath = Gap.Path;
                    DialogResult ok = fbdCSV.ShowDialog();

                    if (ok == System.Windows.Forms.DialogResult.Cancel) return;

                    //string folder = fbdCSV.SelectedPath;
                    //string path = folder + @"\myFile.csv";
                    string path = fbdCSV.SelectedPath;

                    string fname = filename + "-" + DateTime.Now.ToString("yyMMdd") + ".csv";

                    if (crew_)
                        filePath = make_csv_crew(path, fname);
                    else
                        filePath = make_csv_staff(path, fname);                    
                        

                    Gap.SetPath(path);

                    MessageBox.Show("File Saved to \n" + filePath);
                }
                else
                    MessageBox.Show("Local file save not available !");
            }
            catch (Exception ex)
            {
                errDash.Message(ex.Message);
            }


            try
            {
                Process.Start(filePath);
                //Process.Start("excel.exe", filePath);
            }
            catch
            {
                Process.Start("notepad.exe", filePath);
            }

        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void cmdExport_Click(object sender, EventArgs e)
        {
            if (lblPayroll.Text.Equals("Crew"))
                export_csv(true);
            else
                export_csv(false);
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private string mk_header()
        {
            var csv = new StringBuilder();

            //csv.Append("Bookdate, EmpId, Lname, Fname, Boat, Shift, Actual, Job, Code, Straight, OT 2");
            csv.Append("Lname, Fname, Code, Bookdate, EmpId, Boat, Job, Shift, Actual, Straight, OT 2");
            csv.Append(Environment.NewLine);

            return csv.ToString();
        }


        private string make_detail_csv_file(string path, string fname)
        {
            DateTime d = calLog.SelectionStart;
            DateTime f = calLog.SelectionEnd;

            var csv = new StringBuilder();

            DataView dv = _dt.DefaultView;
            dv.Sort = "EmpName, Resp, LogVessel, BookDate";

            DataTable sdt = dv.ToTable();

            foreach (DataRow row in sdt.Rows)
            {
                DateTime bookdate = (DateTime)row["BookDate"];

                //if (bookdate.CompareTo(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)) == -1) continue;
                if (bookdate.CompareTo(d.Date) == -1) continue;
                if (bookdate.CompareTo(f.Date) == 1) continue;

                decimal a = ((decimal)row["LogHours"]);
                decimal o = ((decimal)row["LogOver"]);
                decimal p = a - o;

                //string boat = (string)row["LogVessel"];
                string boat = null;

                int shift = (int)row["LogShift"];
                string emp_id = (string)row["EmpId"];
                string emp_name = (string)row["EmpName"];
                //string resp = (string)row["Resp"];
                string actual = a.ToString("0.#");
                string overtime = o.ToString("0.#");
                string paid = p.ToString("0.#");
                bool regular = (a.Equals(12M) && a.Equals(p));

                if (row["LogVessel"].Equals(DBNull.Value)) continue;

                boat = (string)row["LogVessel"];
                string resp = ((string)row["Resp"]).Trim();

                string code1 = "DH ";
                if (resp.Equals("Capt")) code1 = "SK ";
                if (resp.Equals("Mate")) code1 = "MATE ";
                if (resp.Equals("Mate2")) code1 = "MATE ";

                string code2 = "<boat>";
                if (_code.ContainsKey(boat)) code2 = _code[boat];

                string code = code1 + code2;

                //var line = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}{10}",
                //    bookdate.ToShortDateString(), emp_id, emp_name, boat, shift, actual, resp, code, paid, overtime, Environment.NewLine);

                var line = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}{10}",
                    emp_name, code, bookdate.ToShortDateString(), emp_id, boat, resp, shift, actual, paid, overtime, Environment.NewLine);

                csv.Append(line);
            }

            //string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = string.Format("{0}/{1}", path, fname);
            File.WriteAllText(filePath, mk_header() + csv.ToString());

            return filePath;
        }


        private void cmdExportDetail_Click(object sender, EventArgs e)
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "payDirt.csv";

            try
            {
                if (System.Windows.Forms.SystemInformation.TerminalServerSession)
                {
                    if (Gap.Path.Equals("<none>"))  
                        Gap.SetPath(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));


                    fbdCSV.SelectedPath = Gap.Path;
                    DialogResult ok = fbdCSV.ShowDialog();

                    if (ok == System.Windows.Forms.DialogResult.Cancel) return;

                    //string folder = fbdCSV.SelectedPath;
                    //string path = folder + @"\myFile.csv";
                    string path = fbdCSV.SelectedPath;

                    string fname = "paydirt-" + DateTime.Now.ToString("yyMMdd") + ".csv";
                    
                    filePath = make_detail_csv_file(path, fname);

                    Gap.SetPath(path);

                    MessageBox.Show("File Saved to \n" + filePath);
                }
                else
                    MessageBox.Show("Local file save not available !");
            }
            catch (Exception ex)
            {
                errDash.Message(ex.Message);
            }



            try
            {
                Process.Start(filePath);
                //Process.Start("excel.exe", filePath);
            }
            catch (Exception ex)
            {
                Process.Start("notepad.exe", filePath);
            }


            //// ref : http://csharp.net-informations.com/excel/csharp-create-excel.htm
            //Excel.Application xlApp;
            //Excel.Workbook xlWorkBook;
            //Excel.Worksheet xlWorkSheet;
            //object misValue = System.Reflection.Missing.Value;

            //xlApp = new Excel.ApplicationClass();
            //xlWorkBook = xlApp.Workbooks.Add(misValue);

            //xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            //xlWorkSheet.Cells[1, 1] = "http://csharp.net-informations.com";

            //xlWorkBook.SaveAs("csharp-Excel.xls", 
            //    Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue,
            //    Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            //xlWorkBook.Close(true, misValue, misValue);
            //xlApp.Quit();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void cmdExit_Click(object sender, EventArgs e)
        {
            //Environment.Exit(0);
            this.Close();
        }


        private void cmdNew_Click(object sender, EventArgs e)
        {
            frmAdd frm = new frmAdd();
            frm.ShowDialog();
        }


        private void cmdPrint_Click(object sender, EventArgs e)
        {           
            List<string[]> slips = new List<string[]>();

            foreach (var uc in flpEmps.Controls)
            {
                string[] a = ((ucEmp)uc).OutArray();
                slips.Add(a);
            }

            dlgGeneric dlg = new dlgGeneric();
            dlg.Render(slips);


            //int n = 0;
            //int pagesize = 2;
            //List<string[]> page = new List<string[]>();
            //foreach (var s in slips)
            //{
            //    if (n % pagesize == 0) { n = 0; page.Clear(); }
            //    n++;
            //    page.Add(s);
            //    if (n % pagesize == 0)
            //        dlgGeneric.DialogNext(page);
            //}
            //return;


            //string fname = "PayrollSlip.txt";
            //foreach (var uc in flpEmps.Controls)
            //{
            //    ((ucEmp)uc).Print(fname);

            //    DialogResult ok = dlgGeneric.DialogNext(fname);
            //    if (ok != DialogResult.OK) break;
            //}
        }

        private void cmdView_Click(object sender, EventArgs e)
        {
            DateTime d = calLog.SelectionStart;
            DateTime f = calLog.SelectionEnd;
            //MessageBox.Show(d.ToString());
            //MessageBox.Show(f.ToString());

            //string emp_id = lbx.SelectedItems[0].ToString();
            //load_emp(d, f, emp_id);
            load_emps_selected(d, f);

        }

        //GarNet-GS171227 : replace with button
        //private void chkAll_CheckedChanged(object sender, EventArgs e)
        //{
        //    CheckBox chk = (CheckBox)sender;


        //    for (int i = 0; i < lbxEmp.Items.Count; i++)            
        //        lbxEmp.SetSelected(i, true);


        //    chk.Hide();
        //}

        private void cmdGetAll_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < lbxEmp.Items.Count; i++)
                lbxEmp.SetSelected(i, true);

        }


    }
}
