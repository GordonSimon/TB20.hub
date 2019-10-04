using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using mdlAllyKE;

namespace viewLogKE
{
    public partial class letLog : Form, CueLog
    {
        private const int adjScreen = 190;
        private const int adjW8Screen = 90;
        private const int adjPanel = 175;


        public DateTime LogDate { get; set; }
        private Dictionary<string, Button> _vessels;

        private ucLog _uc_log;
        private ucSheet _uc_sheet;

        //private DataTable _dt_time { get; set; }
        

        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public void Msg() { refresh_totals(); cmdOK.Show();  }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private int get_adj_screen()
        {
            //MessageBox.Show(Environment.OSVersion.Version.Major.ToString());

            return adjScreen;
            //return adjW8Screen;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public string info()
        {
            int w = this.Size.Width;
            int h = this.Size.Height;

            Rectangle r = Screen.GetWorkingArea(this);

            return string.Format("{0}x{1}/({2}, {3})", w, h, r.Width, r.Height);
        }

        
        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public letLog(DateTime day)
        {
            InitializeComponent();

            _uc_log = new ucLog();
            _uc_sheet = new ucSheet();

                                    
            LogDate = day;
            dtpLogDate.Value = LogDate;
            _uc_log.BookDate = LogDate;

            _vessels = new Dictionary<string, Button>();

            lblLogDate.Text = LogDate.ToShortDateString();
            lblVerifyDate.Text = LogDate.ToShortDateString();            
            lblLogUser.Text = "<system>";
            lblVerifyUser.Text = "<update>";
            cbxVerifyDate.Items.Add(string.Format("Update : [Chris] [{0}]", lblVerifyDate.Text));
            cbxVerifyDate.Items.Add(string.Format("Update : [Travis] [{0}]", DateTime.Now.AddDays(-5).ToShortDateString()));
            cbxVerifyDate.Items.Add(string.Format("Create : [Robert] [{0}]", lblLogDate.Text));            
            cbxVerifyDate.Text = "3";

     
            cbxShips.DataSource = dacCache.GetVessel();
            cbxShips.DisplayMember = "Full Name";            
            //cbxShips.DataSource = new BindingSource(dacCache.GetVessel(), "Full Name");
            cbxShips.ValueMember = "Short";
            //cbxShips.SelectedIndex = -1;
            

            cbxShipShift.SelectedIndex = -1;
            //cbxShipShift.DataSource = get_shifts();
            cbxShipShift.BindingContext = new BindingContext();
            cbxShipShift.DataSource = dacCache.GetShift();            
            cbxShipShift.DisplayMember = "Short";
            cbxShipShift.ValueMember = "NumID";

            //cbxShips.SelectedIndex = -1;

            this.Width -= get_adj_screen();
            this.panel1.Width -= adjPanel;


            form_clear();
            
            //cmdOK.Hide(); 
            //cmdCancel.Focus();
        }


        private void frmLog_Load(object sender, EventArgs e)
        {
            //DateTime ref_week = set_date(LogDate);

            //dacCache.RefreshTimebook(ref_week, false);
            //qryGang.Requery();
            //qryTimebook.Requery();
            
            show_timebook();

            if (flpVessel.Controls.Count == 0)
            {
                cbxShips.SelectedItem = -1;
                cbxShips.Text = "";
                //tbxSelVessel.Text = "";

                cmdOK.Hide();
            }

            this.Text = string.Format("Log Sheet for Date [{0}, {1}] : {2}",
                LogDate.DayOfWeek.ToString(),
                LogDate.ToLongDateString(),
                info());

            _uc_log.CueRequired = true;


            cmdNew.Hide();
            //cmdCancel.Focus();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void form_clear()
        {
            //lblMate.Text = "Mate";
            //lblEng.Text = "Eng.";
            //lblC6.Text = "DH(3)";

            //_uc_log.ModeClear();
            _uc_log.ModeReady();


            //bool index_0 = scan_index_for_0(vessel);
            //Dictionary<int, int> shifts = scan_index_for_shift(vessel, dt);

            tabShift.SelectedIndex = -1;
            foreach (TabPage t in tabShift.TabPages) { tabShift.TabPages.Remove(t); t.Controls.Clear();  t.Dispose(); }            
            tabShift.TabPages.Clear();
            tabShift.TabPages.Add("<New>");
            tabShift.TabPages[0].Controls.Add(_uc_log);

            tbxCrewCount.Text = string.Empty;
            tbxOnVessel.Text = string.Empty;
            tbxCrewHours.Text = string.Empty;
            tbxOverHours.Text = string.Empty;
            tbxPaidHours.Text = string.Empty;

            
            //cbxShips.Text = string.Empty;
            cbxShips.SelectedIndex = -1;

            nudShift.Value = 0;

            cmdCancel.Text = "Done";
            cmdSheet.Text = "6 Crew";

            cmdNew.Hide();
            cmdAdd.Hide();
            cmdOK.Hide();
        }


        private void form_readonly()
        {
            errorProvider1.Clear();

            _uc_log.ModeRO();

            //cbxShips.Enabled = false;
            //nudShift.Enabled = false;
            //cbxShipShift.Enabled = false;
            
            cmdOK.Text = "Edit";
            cmdCancel.Text = "Cancel";
            cmdOK.Show();            
            cmdAdd.Show();

            cmdOK.Focus();
        }

      
        private void form_edit()
        {
            errorProvider1.Clear();

            cbxShips.Enabled = true;
            nudShift.Enabled = true;
            cbxShipShift.Enabled = true;

            _uc_log.ModeEdit(false);

            cmdOK.Text = "Save";
            //cmdCancel.Text = "Cancel";
            //cmdOK.Show();
            cmdOK.Hide();
            //cmdNew.Show();
            cmdNew.Hide();
            cmdAdd.Hide();


            //tbxCpA.Focus();
            cmdCancel.Focus();
        }


        private void form_add()
        {
            errorProvider1.Clear();

            cbxShips.Enabled = true;
            nudShift.Enabled = true;
            cbxShipShift.Enabled = true;

            //tabShift.TabPages.Add(tabShift.TabPages[0].Text);
            TabPage tp = new TabPage(tabShift.TabPages[0].Text);
            tp.Tag = -1;
            tabShift.TabPages.Add(tp);
            tabShift.TabPages[0].Text = "<Add>";
            tabShift.SelectTab(0);

            _uc_log.ModeClear();
            _uc_log.ModeEdit(true);
            //_uc_log.NewShift((int)nudShift.Value);

            tbxCrewCount.Text = string.Empty;
            tbxOnVessel.Text = string.Empty;
            tbxCrewHours.Text = string.Empty;
            tbxOverHours.Text = string.Empty;
            tbxPaidHours.Text = string.Empty;

            cbxShips.Text = string.Empty;

            nudShift.Value = 0;


            cbxShips.SelectedIndex = -1;
            cbxShips.SelectedValue = _uc_log.Vessel;

            change_format(false, _uc_log.Shift);

            cmdOK.Text = "Save";
            cmdCancel.Text = "Cancel";            
            cmdOK.Hide();
            cmdOK.Show();
            //cmdCancel.Text = "Cancel";
            //cmdOK.Show();
            cmdNew.Hide();
            cmdAdd.Hide();

            //tbxCpA.Focus();
            cmdCancel.Focus();
        }


        private void form_new()
        {
            errorProvider1.Clear();

            cbxShips.Enabled = true;
            nudShift.Enabled = true;
            cbxShipShift.Enabled = true;

            tabShift.TabPages.Clear();
            tabShift.TabPages.Add("<New>");
            tabShift.TabPages[0].Controls.Add(_uc_log);

            //_uc_log.ChangeShift(-1);
            _uc_log.ModeClear();
            _uc_log.ModeEdit(false);

            //_uc_log.NewShift((int)nudShift.Value);

            
            tabShift.TabPages[0].Text = "<New>";

            cmdOK.Text = "Save";
            cmdOK.Hide();
            cmdCancel.Text = "Cancel";
            cmdSheet.Text = "6 Crew";
            //cmdOK.Show();
            cmdNew.Hide();
            cmdAdd.Hide();

            //tbxCpA.Focus();
            cmdCancel.Focus();
        }






        private void clear_vessels()
        {
            flpVessel.Controls.Clear();
            _vessels.Clear();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void tabShift_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            TabControl t = (TabControl)sender;

            if (_uc_log.Dirty || _uc_log.Busy)
            {
                errorProvider1.SetError(cmdOK,
                    "Please <Save> or <Cancel> before you change tab !");
                e.Cancel = true;
            }
        }


        private void tabShift_Selected(object sender, TabControlEventArgs e)
        {
            //_uc_log.Shift = e.TabPageIndex + 1;
            _uc_log.Shift = 0;

            if (e.TabPageIndex != -1)
            {
                int shift;

                _uc_log.ModeClear();
                tabShift.TabPages[e.TabPageIndex].Controls.Add(_uc_log);

                shift = (int)e.TabPage.Tag;
                //_uc_log.ChangeShift((shift != -1), shift);
                change_format(false, shift);

                //_uc_log.ModeRO();
                form_readonly();

                refresh_totals();

                //cbxShipShift.SelectedIndex = e.TabPageIndex + 1;
                int ship_shift = (int)e.TabPage.Tag;
                if (ship_shift > 5)
                    cbxShipShift.SelectedIndex = -1;
                else
                    cbxShipShift.SelectedIndex = (int)e.TabPage.Tag;

                cmdOK.Text = "Edit";
                cmdOK.Show();
                cmdNew.Show();
            }
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private bool scan_index_for_0(string vessel)
        {
            foreach (DataRow row in _uc_log.Timebook.Rows)
            {
                //if (row.RowState == DataRowState.Deleted) continue;
                //if ((bool)row["DelMark"]) continue;

                if (vessel.Equals(row["Vessel"]))
                {
                    int idx = (int)row["Shift"];
                    if (idx == 0) return true;
                }
            }

            return false;
        }


        private bool scan_index_for_shift_12(string vessel)
        {
            foreach (DataRow row in _uc_log.Timebook.Rows)
            {
                if (vessel.Equals(row["Vessel"]))
                {
                    int idx = (int)row["Shift"];
                    if (idx == 1) return true;
                    if (idx == 2) return true;
                }
            }

            return false;
        }


        private bool scan_index_for_mate_eng(string vessel)
        {
            foreach (DataRow row in _uc_log.Timebook.Rows)
            {
                if (vessel.Equals(row["Vessel"]))
                {
                    string resp = (string)row["Resp"];
                    if (resp.Equals("Mate")) return true;
                    if (resp.Equals("Mate2")) return true;
                    if (resp.Equals("Eng.")) return true;
                }
            }

            return false;
        }



        private Dictionary<int, string> scan_index_for_shift(string vessel, DataTable dt)
        {
            DataTable sdt;
            sdt = dacCache.GetShift();

            Dictionary<int, string> d = new Dictionary<int, string>();

            foreach (DataRow row in dt.Rows)
            {
                if (vessel.Equals(row["Vessel"]))
                {
                    int idx = (int)row["Shift"];
                    if (idx == -1 || idx > 7) idx = 8;
                    //if (idx > 5) idx = idx - 5;
              
                    if (!d.ContainsKey(idx))
                    {
                        if (idx == 8)
                            d.Add(idx, "<No Shift>");
                        else if (idx == 0)
                            d.Add(idx, "<Sched>");
                        else
                        {
                            int idx_error = (idx >= 0 ? idx : 0);
                            DataRow srow = sdt.Rows[idx_error];
                            //DataRow srow = sdt.Rows[idx];
                            if (srow != null)
                                d.Add(idx, (string)srow["Short"]);
                        }
                    }
                }
            }

            //int result;
            //foreach (DataRow row in dt.Rows)
            //{
            //    if (vessel.Equals(row["Vessel"]))
            //    {
            //        int idx = (int)row["Shift"];
            //        d.TryGetValue(idx, out result);
            //        if (result == 0)
            //            d.Add(idx, result + 1);
            //        else
            //            d[idx] = result + 1;
            //    }
            //}

            return d;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/                     

        private void show_vessel(string vessel)
        {
            if (!_vessels.ContainsKey(vessel))
            {
                Button cmd = mkbtn_vessel(vessel);

                _vessels.Add(vessel, cmd);
                flpVessel.Controls.Add(cmd);
            }
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/
        
        private Button mkbtn_vessel(string name)
        {
            Button cmd = new Button();
            cmd.Text = name;

            cmd.Click += new EventHandler(cmdVessel_Click);

            return cmd;
        }


        void change_vessel(string vessel)
        {
            form_clear();

            //tabShift.TabPages.Clear();
            //tabShift.TabPages.Add("AM");
            //tabShift.TabPages[0].Controls.Add(_uc_log);            

            //bool index_0 = scan_index_for_0(vessel);
            bool mate_eng = scan_index_for_mate_eng(vessel);
            bool index_12 = scan_index_for_shift_12(vessel);
            Dictionary<int, string> shifts = scan_index_for_shift(vessel, _uc_log.Timebook);

            //if (!index_12)
            //    tabShift.TabPages.RemoveAt(0);
            //else
            //{
                tabShift.TabPages[0].Text = vessel;
                tabShift.TabPages[0].Tag = -1;
            //}

            //_uc_log.Shift = 1;
            //_uc_log.ChangeShift(-1);

            //_uc_log.ChangeShift(false, -1);
            //_uc_log.Vessel = vessel;
            //cmdSheet.Text = "6 Crew";

            if (mate_eng)
            {
                _uc_log.ChangeShift(true, -1);
                _uc_log.Vessel = vessel;
                cmdSheet.Text = "4 Crew";
            }
            else
            {
                _uc_log.ChangeShift(false, -1);
                _uc_log.Vessel = vessel;
                cmdSheet.Text = "6 Crew";
            }


            foreach (int s in shifts.Keys)
            {                
                TabPage tp = new TabPage(shifts[s]);
                tp.Tag = s;
                tabShift.TabPages.Add(tp);
            }

            
            //bool flag = true;
            //bool index_0 = scan_index_for_0(vessel);
            //Dictionary<int, string> shifts = scan_index_for_shift(vessel, _uc_log.Timebook);
            //foreach (int s in shifts.Keys)
            //{
            //    if (s == 0) { tabShift.TabPages[0].Text = shifts[0]; tabShift.TabPages[0].Tag = s; }
            //    if (s == 1)
            //    {
            //        tabShift.TabPages[0].Tag = s; 
            //        tabShift.TabPages[0].Text = shifts[1];             
            //        if (index_0) tabShift.TabPages[0].Text = shifts[1] + " & " + shifts[0] + "   ";
            //        flag = false;
            //    }

            //    if (s > 1)
            //    {
            //        if (!index_0 && flag)
            //        {
            //            tabShift.TabPages[0].Tag = s;
            //            tabShift.TabPages[0].Text = shifts[s];
            //            _uc_log.Shift = s;
            //            flag = false;
            //        }
            //        else
            //        {
            //            TabPage tp = new TabPage(shifts[s]);
            //            tp.Tag = s;
            //            tabShift.TabPages.Add(tp);
            //        }
            //    }
                    
            //}

            //cbxShipShift.SelectedIndex = e.TabPageIndex + 1;
            //cbxShipShift.SelectedIndex = (shifts.Keys.First() == 0 ? 1 : shifts.Keys.First());

            
            //_uc_log.ModeRO();
            form_readonly();
            
            cbxShips.SelectedValue = vessel;
            //tbxSelVessel.Text = cbxShips.SelectedValue.ToString();                    

            refresh_totals();

            //if (_edit) { form_edit(); cmdOK.Show(); }
            //else
            //{
            //    if (_readonly) { form_readonly(); cmdNew.Show(); }
            //}

            cmdLeft.Visible = false;
            cmdRight.Visible = false;

            cmdOK.Text = "Edit";
            cmdOK.Show();            
            cmdAdd.Show();
            //cmdNew.Show();

        }


        void cmdVessel_Click(object sender, EventArgs e)
        {
            Button cmd = (Button)sender;
            //form_vessel(cmd.Text, false);
            //form_readonly();

            change_vessel(cmd.Text);
            //form_readonly();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void refresh_totals()
        {
            tbxCrewCount.Text = _uc_log.CrewCount.ToString();
            tbxOnVessel.Text = _uc_log.OnVessel.ToString("#.#");
            tbxCrewHours.Text = _uc_log.CrewHours.ToString("#.#");
            tbxOverHours.Text = _uc_log.OverHours.ToString("#.#");
            tbxPaidHours.Text = _uc_log.PaidHours.ToString("#.#");

            tbxV1A.Text = _uc_log.TotalHours.ToString("#.#");

            //on_vessel();
            //crew_hours();
            //paid_hours();
            //over_hours();

            //crew_count();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/                             
        
        private void show_timebook()
        {
            DateTime ref_week = set_date(LogDate);
            mboTimebook.Load(ref_week);

            //DataTable dt = qryTimebook.GetView("Vessel", LogDate.Date);
            DataTable dt_tb = mboTimebook.GetTimebook(LogDate.Date);

            _uc_log.Timebook = dt_tb;            
                        
            clear_vessels();
            form_clear();
            

            //_dt_time = dt;

            foreach (DataRow row in dt_tb.Rows)
                show_vessel((string)row["Vessel"]);

        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/                             
        
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

        private Decimal needDecimal(TextBox tbx)
        {
            string val = tbx.Text;
            Decimal number;
            bool ok = Decimal.TryParse(val, out number);
            if (!ok) return 0M;

            return number;
        }


        private Decimal vessel_hours()
        {
            Decimal sum = 0;
    

            return sum;
        }


        //private void fullcrew(TextBox tbxVA, TextBox tbxVCa,
        //    TextBox tbxVMa, TextBox tbxVEn, TextBox tbxVD1, TextBox tbxVD2)
        //{
        //    tbxVA.Text = vessel_hours().ToString();

        //    tbxVA.BackColor = Color.Yellow;
        //    tbxVA.ReadOnly = true;

        //    tbxVCa.BackColor = Color.AliceBlue;
        //    tbxVMa.BackColor = Color.AliceBlue;
        //    tbxVEn.BackColor = Color.AliceBlue;
        //    tbxVD1.BackColor = Color.AliceBlue;
        //    tbxVD2.BackColor = Color.AliceBlue;
        //    tbxVCa.ReadOnly = true;
        //    tbxVMa.ReadOnly = true;
        //    tbxVEn.ReadOnly = true;
        //    tbxVD1.ReadOnly = true;
        //    tbxVD2.ReadOnly = true;

        //    tbxVCa.Text = tbxCpA.Text;
        //    tbxVMa.Text = tbxMaA.Text;
        //    tbxVEn.Text = tbxEnA.Text;
        //    tbxVD1.Text = tbxD1A.Text;
        //    tbxVD2.Text = tbxD2A.Text;
        //}

        //private void set_vessel1(TextBox tbxVA, TextBox tbxVCa,
        //    TextBox tbxVMa, TextBox tbxVEn, TextBox tbxVD1, TextBox tbxVD2, bool crew)
        //{
        //    if (crew)
        //        fullcrew(tbxVA, tbxVCa, tbxVMa, tbxVEn, tbxVD1, tbxVD2);
        //    else
        //        partcrew(tbxVA, tbxVCa, tbxVMa, tbxVEn, tbxVD1, tbxVD2);

        //    refresh_totals(); 
        //}


        //private void chkCrew1_CheckedChanged(object sender, EventArgs e)
        //{
        //    set_vessel1(tbxV1A, tbxV1Ca, tbxV1Ma,tbxV1En, tbxV1D1, tbxV1D2, chkCrew1.Checked);
        //}


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void cbxShips_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox cbx = (ComboBox)sender;

            if (cbx.SelectedIndex == -1) return;

            //int idx = cbx.SelectedIndex;
            string vessel = (string)cbx.SelectedValue;
            if (_vessels.ContainsKey(vessel))
            {
                change_vessel(vessel);
                form_readonly();                
            }
            else
            {
                //form_clear();

                _uc_log.Vessel = vessel;
        
                form_new();

                //cmdOK.Text = "Save";
                //cmdOK.Show();
            }

        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/                             

        private void dtpLogDate_CloseUp(object sender, EventArgs e)
        {
            LogDate = ((DateTimePicker)sender).Value;
            
            this.Text = string.Format("Log Sheet for Date [{0}, {1}]",
                LogDate.DayOfWeek.ToString(),
                LogDate.ToLongDateString());


            //DateTime user_day = ((DateTimePicker)sender).Value;
            //DateTime ref_week = set_date(LogDate);

            
            //GS14V1 //DataSet ds = dacTimebook.GetDS(ref_week, 14);
            //GS14V1 //_dt_crew = ds.Tables[0];
            //dacCache.RefreshTimebook(ref_week, false);
            //qryGang.Requery();
            //qryTimebook.Requery();
            

            //reset_week(RefWeek);
            //refresh_data(RefWeek, _show_all);

            _uc_log.BookDate = LogDate;

            show_timebook();
            cmdCancel.Focus();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Button cmd = (Button)sender;

            if (cmd.Text.Equals("Done"))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                //this.DialogResult = DialogResult.Cancel;
                //this.Close();
                _uc_log.ModeCancel();

                form_clear();
            }
        }


        private void cmdOK_Click(object sender, EventArgs e)
        {
            Button cmd = (Button)sender;

            if (cmd.Text.Equals("Save"))
            {
                if (! _uc_log.Dirty )
                //if (! _dirty && ! _deleted)
                    errorProvider1.SetError(cmdOK, "Error (Save) : Please add crew member !");
                else
                {
                    //do_save_delete();
                                        
                    bool result = _uc_log.Save_Delete();
                    if (!result)
                    {
                        errorProvider1.SetError(cmdOK, "Warning (Save) : No changes to save !");
                        return;
                    }
                    
                    mboTimebook.SetTimebook(_uc_log.Timebook, _uc_log.BookDate, _uc_log.Vessel);

                    //_uc_log.ModeReady();
                    frmLog_Load(null, null);
                    _uc_log.ModeReady();
                    
                    //form_vessel(tbxSelVessel.Text, false);
                    //form_readonly();
                    //_uc_log.ReadOnly(tbxSelVessel.Text);

                    //_uc_log.ModeReady();
                    cmdCancel.Text = "Done";
                    cmdOK.Hide();

                    errorProvider1.Clear();

                    //MessageBox.Show("Saved !");

                    //this.DialogResult = DialogResult.OK;
                    //this.Close();
                }
            }
            else if (cmd.Text.Equals("Edit"))
            {
                form_edit();
            }                
        }
        

        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void tbxV1Ca_TextChanged(object sender, EventArgs e)
        {
            //_dirty = true;
            //tbxV1A.Text = vessel_hours().ToString();
        }

        private void tbxV1Ma_TextChanged(object sender, EventArgs e)
        {
            //_dirty = true;
            //tbxV1A.Text = vessel_hours().ToString();
        }

        private void tbxV1En_TextChanged(object sender, EventArgs e)
        {
            //_dirty = true;
            //tbxV1A.Text = vessel_hours().ToString();
        }

        private void tbxV1D1_TextChanged(object sender, EventArgs e)
        {
            //_dirty = true;
            //tbxV1A.Text = vessel_hours().ToString();
        }

        private void tbxV1D2_TextChanged(object sender, EventArgs e)
        {
            //_dirty = true;
            //tbxV1A.Text = vessel_hours().ToString();
        }



        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void tbxCpP_TextChanged(object sender, EventArgs e)
        {
            //_dirty = true;
        }

        private void tbxD1P_TextChanged(object sender, EventArgs e)
        {
            //_dirty = true;
        }

        private void tbxD2P_TextChanged(object sender, EventArgs e)
        {
            //_dirty = true;
        }

        private void tbxMaP_TextChanged(object sender, EventArgs e)
        {
            //_dirty = true;
        }

        private void tbxEnP_TextChanged(object sender, EventArgs e)
        {
            //_dirty = true;
        }



        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        /* don't use */
        private void nudShift_ValueChanged(object sender, EventArgs e)
        {
            cbxShipShift.SelectedIndex = (int)nudShift.Value;  
        }


        private void cbxShipShift_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //nudShift.Value = cbxShipShift.SelectedIndex;

            ComboBox cbx = (ComboBox)sender;
            int shift = cbx.SelectedIndex;
            _uc_log.NewShift(shift);

            //if (shift == 5)  // 24Hour shift
            //    cmdSheet_Click(cmdSheet, null);

            //change_format(shift);
           
            //cmdSheet.Text = (shift == 5 ? "24Hour" : "Shift");
        }

        /* don't use */
        //private void cbxShipShift_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //int shift = (int)nudShift.Value;
        //    ComboBox cbx = (ComboBox)sender;
        //    int shift = cbx.SelectedIndex;
        //    _uc_log.NewShift(shift);
        //}



        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        private void change_format(bool force, int shift)
        {
            if (force || shift == -1)
            {
                cmdSheet.Text = "6 Crew";
                _uc_log.ChangeShift(false, shift);
            }
            else
            {
                cmdSheet.Text = "4 Crew";
                _uc_log.ChangeShift(true, shift);
            }
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void cmdNew_Click(object sender, EventArgs e)
        {
            form_clear();            
            //form_edit();
            form_new();
            //pnlPM.Hide();            

            cbxShips.SelectedIndex = -1;
        }


        private void cmdAdd_Click(object sender, EventArgs e)
        {
            //form_clear();
            form_add();            
        }



        private void cmdGrid_Click(object sender, EventArgs e)
        {
            Button cmd = (Button)sender;

            if (cmd.Text.Equals("V"))
            {
                dgvTB.DataSource = _uc_log.Timebook;
                dgvTB.Location = gbxCrew.Location;
                dgvTB.Size = new Size(gbxCrew.Width, gbxCrew.Height);
                dgvTB.AutoResizeColumns();
                dgvTB.BringToFront();

                dgvTB.Show();
                cmd.Text = "^";
            }
            else
            {
                dgvTB.DataSource = null;
                dgvTB.Hide();
                cmd.Text = "V";
            }
        }


        private void cmdSheet_Click(object sender, EventArgs e)
        {
            Button cmd = (Button)sender;
            
            change_format(cmd.Text.Equals("4 Crew"), _uc_log.Shift);
            
            //if (cmd.Text.Equals("6 Crew"))
            //{
            //    cmd.Text = "4 Crew";
            //    _uc_log.ChangeFormat(true);
            //}
            //else
            //{
            //    cmd.Text = "6 Crew";
            //    _uc_log.ChangeFormat(false);
            //}            
        }


        // Not used now GS160912
        private void V1cmdSheet_Click(object sender, EventArgs e)
        {
            lblLog.Show();
            lblFuel.Show();
            lblHourStart.Show();
            lblHourFinish.Show();
            lblShift.Show();
            
            Form frm = new Form
            {
                Text = "Log Sheet",
                MinimumSize = new System.Drawing.Size(300, 200),
                MaximumSize = new System.Drawing.Size(Screen.PrimaryScreen.WorkingArea.Width,
                    Screen.PrimaryScreen.WorkingArea.Height),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            

            frm.Controls.Add(_uc_sheet);
            DialogResult ok = frm.ShowDialog();

            if (ok == DialogResult.OK)
            {
                lblLog.Text = _uc_sheet.LogID;
                lblShift.Text = _uc_sheet.LogShift;
                lblHourStart.Text = _uc_sheet.LogHoursStart;
                lblHourFinish.Text = _uc_sheet.LogHoursFinish;
                lblFuel.Text = _uc_sheet.LogFuel;

                _uc_log.SheetID = lblLog.Text;
                _uc_log.ShowSheet();
            }

        }

        private void tabShift_DoubleClick(object sender, EventArgs e)
        {
            TabControl tc = (TabControl)sender;

            TabPage tp = tc.SelectedTab;

            if (!tp.Text.Equals("<New>")) return;

            change_format(false, _uc_log.Shift);

            //if (_uc_log.Shift == 0)
            //{
            //    cmdSheet.Text = "6 Crew";
            //    _uc_log.ChangeShift(true, -1);
            //}
            //else
            //{
            //    cmdSheet.Text = "4 Crew";
            //    _uc_log.ChangeShift(false, 0);
            //}
        }
    }
}


