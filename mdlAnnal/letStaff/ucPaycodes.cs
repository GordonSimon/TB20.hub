using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using mdlAnnal;


namespace letStaff
{
    public partial class ucPaycodes : UserControl
    {
        public Decimal TotalRegHours { get; set; }
        public Decimal TotalXtrHours { get; set; }
        public Decimal TotalOver { get; set; }
        public Decimal TotalOver1 { get; set; }

        private DataTable _dt { get; set; }

        private Form _frm { get; set; }

        private int _max_shift { get; set; }

        private string _emp_id { get; set; }
        private string _emp_name { get; set; }
        private string _duty { get; set; }
        private DateTime  _bookdate { get; set; }

        private string _defpaycode { get; set; }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public ucPaycodes()
        {
            InitializeComponent();

            _frm = new Form();
            _frm.FormBorderStyle = FormBorderStyle.None;

            lblEdit.Hide();
            cmdSave.Show();

            tbxEmpId.BackColor = Color.White;

            _frm.Controls.Add(this);

            _frm.Size = new Size(300, 180);
            _frm.BackColor = Color.MediumAquamarine;
            _frm.AutoSize = true;

            _frm.StartPosition = FormStartPosition.CenterParent;

            //chkHourly.Checked = true;
            //chkHourly.Enabled = false;
            _max_shift = 10;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void prepare_columns()
        {
            
            foreach (DataGridViewColumn dc in dgvPaycodes.Columns)
                dc.Visible = false;

            dgvPaycodes.Columns["EmpID"].Visible = true;           
            dgvPaycodes.Columns["EmpName"].Visible = true;
            //dgvPaycodes. Columns["PayCode"].Visible = true;
            dgvPaycodes.Columns["ToffCode"].Visible = true;
            dgvPaycodes.Columns["LogShift"].Visible = true;
            dgvPaycodes.Columns["LogHours"].Visible = true;
            dgvPaycodes.Columns["LogOver"].Visible = true;
            dgvPaycodes.Columns["LogOver1"].Visible = true;
            dgvPaycodes.Columns["LogNote"].Visible = true;


            dgvPaycodes.Columns["ID"].DefaultCellStyle.BackColor = Color.LightYellow;
            dgvPaycodes.Columns["EmpID"].DefaultCellStyle.BackColor = Color.LightYellow;
            dgvPaycodes.Columns["EmpName"].DefaultCellStyle.BackColor = Color.LightYellow;
            dgvPaycodes.Columns["LogShift"].DefaultCellStyle.BackColor = Color.LightYellow;
            dgvPaycodes.Columns["ToffCode"].DefaultCellStyle.BackColor = Color.LightYellow;

            var column = new DataGridViewComboBoxColumn();
            column.Name = "Pay Code";
            //column.DataSource = new List<string>
            //{ "Hourly", "DH", "SK",
            //    "Charting 1", "Charting 2", "Charting", "Dispatch", "Dock", "Office", "Meeting", "Salary", "Shore", "Storage M1", "Storage M2", "Water Taxi" };

            column.DataSource = new List<string>
            { "Hourly", "DH", "SK",
                "Charting 1", "Charting 2", "Charting", "Dispatch", "Dock", "Office", "Meeting", "Salary", "Shore", "Storage M1", "Storage M2", "Training", "Water Taxi" };

            column.DataPropertyName = "PayCode";
            
            dgvPaycodes.Columns.Add(column);

            int o = dgvPaycodes.Columns["Pay Code"].DisplayIndex;
            dgvPaycodes.Columns["Pay Code"].DisplayIndex = dgvPaycodes.Columns["LogNote"].DisplayIndex;
            dgvPaycodes.Columns["LogNote"].DisplayIndex = o;

            //dgvPaycodes.AutoResizeColumns();
        }

        
        public void LoadDatatable(DataTable dt)
        {
            _dt = dt;

            dgvPaycodes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvPaycodes.DataSource = _dt;
            prepare_columns();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            add_mode();
        }


        private void cmdSave_Click(object sender, EventArgs e)
        {
            save_mode();
            show_totals();
        }


        private void cmdDelete_Click(object sender, EventArgs e)
        {
            delete_mode();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void lblSql_DoubleClick(object sender, EventArgs e)
        {
            DataGridView dgv = new DataGridView();
            dgv.Dock = DockStyle.Fill;

            dgv.DataSource = _dt.DefaultView;

            this.Controls.Add(dgv);
            dgv.BringToFront();

            dgv.Click += new EventHandler(dgv_Click);
        }


        void dgv_Click(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            dgv.Dispose();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void add_mode()
        {
            DataRow row = _dt.NewRow();

            row["EmpId"] = _emp_id;
            row["EmpName"] = _emp_name;
            row["BookDate"] = _bookdate;
            row["LogShift"] = _max_shift;
            row["PayCode"] = _defpaycode;

            _max_shift += 1;

            _dt.Rows.Add(row);

            //dgvPaycodes.Refresh();

            cmdSave.Enabled = true;

            edit_mode();
        }


        private void save_mode()
        {
            DateTime update_date = DateTime.Now.Date;
            string user = "<update>";

            Decimal sum_reghours = 0.0M;
            Decimal sum_xtrhours = 0.0M;
            Decimal sum_over = 0.0M;
            Decimal sum_over1 = 0.0M;

            DataTable dtchg = _dt.GetChanges(DataRowState.Deleted);
            if (dtchg != null)
            {
                foreach (DataRow row in dtchg.Rows)

                    dacTimebook.vwDelTimebook("dbo.", row["ID", DataRowVersion.Original].ToString(), update_date, user);
            }


            dtchg = _dt.GetChanges(DataRowState.Added | DataRowState.Modified);
            //foreach (DataRowView row in _dt.DefaultView)
            if (dtchg != null)
            {
                foreach (DataRow row in dtchg.Rows)
                {
                    if (!(row["LogVessel"].Equals(DBNull.Value) || row["LogVessel"].Equals(string.Empty))) continue;

                    _emp_name = (string)row["EmpName"];

                    int shift = (Int32)row["LogShift"];

                    Decimal hour = 0.0M;
                    Decimal over = 0.0M;
                    Decimal over1 = 0.0M;

                    if (!row["LogHours"].Equals(DBNull.Value)) hour = (Decimal)row["LogHours"];
                    if (!row["LogOver"].Equals(DBNull.Value)) over = (Decimal)row["LogOver"];
                    if (!row["LogOver1"].Equals(DBNull.Value)) over1 = (Decimal)row["LogOver1"];

                    string paycode = (string)row["PayCode"];

                    if (shift >= 10)
                        sum_xtrhours += hour;
                    else
                        sum_reghours += hour;

                    sum_over += over;
                    sum_over1 += over1;

                    //if (_duty.Equals("Office"))
                    //{
                    //    //decimal perday = 8.0M;
                    //    //if (_defpaycode.Equals("Dispatch")) perday = 12.0M;
                    //    //if (_defpaycode.Equals("Office")) perday = 7.0M;

                    //    //over = over / perday;
                    //    //over1 = over1 / perday;

                    //    if (paycode.Equals("Salary") ||
                    //        paycode.Equals("Dispatch") ||
                    //        paycode.Equals("Shore") ||
                    //        paycode.Equals("Office"))
                    //    {
                    //        sum_reghours += hour;
                    //        //hour = hour / perday;
                    //    }
                    //    else
                    //        sum_xtrhours += hour;

                    //    sum_over += over;
                    //    sum_over1 += over1;

                    //}
                    //else
                    //{
                    //    if (paycode.Equals("DH") || paycode.Equals("SK"))
                    //        sum_reghours += hour;
                    //    else
                    //        sum_xtrhours += hour;

                    //    sum_over += over;
                    //    sum_over1 += over1;
                    //}

                    bool test1 = (hour == over && over == over1 && hour == 0.0M);
                    bool test2 = (paycode.Equals("DH") || paycode.Equals("SK"));
                    if (test1 || test2)
                    {
                        if (shift < 10) continue;

                        MessageBox.Show("Data for this record is incomplete; it will not be saved!", "Error");
                        continue;
                    }

                    int id = 0;
                    if (!row["ID"].Equals(DBNull.Value)) id = (int)row["ID"];
                    

                    dacTimebook.vwAddTimebook("dbo.", id, _bookdate, _emp_id, shift, _emp_name, hour, over, over1, paycode,
                        update_date, user);

                }


                _dt.AcceptChanges();

                //TotalRegHours += sum_reghours;
                //TotalXtrHours += sum_xtrhours;

                if (sum_reghours != 0.0M & sum_xtrhours != 0.0M)
                    MessageBox.Show("Warning : mixed salary & hours !");

                if (TotalOver != sum_over || TotalOver1 != sum_over1)
                    MessageBox.Show("Warning : Overtime hours do not reconcile !");

                _max_shift = 10;
            }

            cmdSave.Enabled = false;
            lblEdit.Visible = true;
        }


        private void cancel_mode()
        {            
            _dt.RejectChanges();
            _max_shift = 10;

            //foreach (DataRowView row in _dt.DefaultView)
            //{
            //    Decimal hour = 0.0M;
            //    Decimal over = 0.0M;

            //    if (!row["LogHours"].Equals(DBNull.Value)) hour = (Decimal)row["LogHours"];
            //    if (!row["LogOver"].Equals(DBNull.Value)) over = (Decimal)row["LogOver"];

            //    hour = hour / 8.0M;
            //    over = over / 8.0M;

            //    row["LogHours"] = hour;
            //    row["LogOver"] = over;
            //}
        }


        private void edit_mode()
        {
            dgvPaycodes.ReadOnly = false;
            lblEdit.Visible = false;

            dgvPaycodes.Columns["ID"].ReadOnly = true;
            dgvPaycodes.Columns["EmpID"].ReadOnly = true;
            dgvPaycodes.Columns["EmpName"].ReadOnly = true;
            dgvPaycodes.Columns["LogShift"].ReadOnly = true;
            dgvPaycodes.Columns["ToffCode"].ReadOnly = true;

            cmdSave.Enabled = true;

            dgvPaycodes.Focus();
        }


        private void read_mode()
        {
            try
            {
                dgvPaycodes.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Waning : dgv readonly", "Ignore error !");
            }

            show_totals();
            //lblEdit.Visible = true;

            //dgvPaycodes.Columns["ID"].ReadOnly = true;
            //dgvPaycodes.Columns["EmpName"].ReadOnly = true;
            //dgvPaycodes.Columns["LogShift"].ReadOnly = true;

            lblEdit.Visible = (_dt.DefaultView.Count > 0);
            cmdSave.Enabled = false;
        }


        private void delete_mode()
        {
            DataGridViewRow row = dgvPaycodes.SelectedRows[0];

            int shift = Convert.ToInt32(row.Cells["LogShift"].Value);
            if (shift >= 10)
            {
                //int id = Convert.ToInt32(row.Cells["ID"].Value);
                //DataRow pk = _dt.Rows.Find(id);
                //if (pk != null) { pk.Delete(); cmdSave.Enabled = true; }

                dgvPaycodes.Rows.Remove(row); cmdSave.Enabled = true;                
            }
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public void show_totals()
        {            
            TotalRegHours = 0.0M;
            TotalXtrHours = 0.0M;
            TotalOver = 0.0M;
            TotalOver1 = 0.0M;

            foreach (DataRowView row in _dt.DefaultView)
            {
                Decimal salary = 0.0M;
                Decimal over = 0.0M;
                Decimal over1 = 0.0M;
                Decimal hourly = 0.0M;

                string duty = "Hourly";
                if (!row["Duty"].Equals(DBNull.Value)) duty = (string)row["Duty"];                
                

                string paycode = _defpaycode;
                if (! row["PayCode"].Equals(DBNull.Value)) paycode = (string)row["Paycode"];
                
                Decimal h = 0.0M;
                if (!row["LogHours"].Equals(DBNull.Value)) h = (Decimal)row["LogHours"];

                if (paycode.Equals(_defpaycode))
                {
                    if (duty.Equals("Office")) salary = h; else hourly = h;                
                    //if (paycode.Equals("Dispatch") ||
                    //    paycode.Equals("Office") ||
                    //    paycode.Equals("Shore") ||
                    //    paycode.Equals("Salary"))
                    //{
                    //    salary = h;
                    //}
                    //else
                    //    hourly = h;
                }
                else
                    hourly = h;

                if (!row["LogOver"].Equals(DBNull.Value)) over = (Decimal)row["LogOver"];
                if (!row["LogOver1"].Equals(DBNull.Value)) over1 = (Decimal)row["LogOver1"];

                TotalRegHours += salary;
                TotalXtrHours += hourly;
                TotalOver += over;
                TotalOver1 += over1;
            }

            tbxSalary.Text = TotalRegHours.ToString();
            tbxHourly.Text = TotalXtrHours.ToString();
            tbxOT1.Text = TotalOver1.ToString();
            tbxOT2.Text = TotalOver.ToString();
        }


        public void ShowCRUD(string emp_id, DateTime refdate, string defpaycode)
        {
            _emp_id = emp_id;
            _bookdate = refdate.Date;
            _defpaycode = defpaycode;

            //string filter = string.Format("EmpId='{0}' and BookDate = #{1}# and IsNull(ToffCode, '') = '' and IsNull(LogVessel, '') = ''", 
            //    emp_id, refdate.ToString("MM/dd/yyyy"));
            //string filter = string.Format("EmpId='{0}' and BookDate = #{1}# and IsNull(ToffCode, '') = ''",
            //    emp_id, refdate.ToString("MM/dd/yyyy"));

            string filter = string.Format("EmpId='{0}' and BookDate = #{1}#",
                emp_id, refdate.ToString("MM/dd/yyyy"));

            //string filter = string.Format("EmpId='{0}' and BookDate = #{1}#",
            //    emp_id, refdate.ToString("MM/dd/yyyy"));
            lblSql.Text = filter;

            dtpDay.Value = refdate;

            _dt.DefaultView.RowFilter = filter;

            if (_dt.DefaultView.Count == 0)
            {
                _dt.DefaultView.RowFilter = string.Format("EmpId='{0}'", emp_id);
                DataRowView vr = _dt.DefaultView[0];
                _emp_name = string.IsNullOrEmpty((string)vr["EmpName"]) ? "<no name>" : (string)vr["EmpName"];
                _duty = string.IsNullOrEmpty((string)vr["Duty"]) ? "<no duty>" : (string)vr["Duty"];
                _dt.DefaultView.RowFilter = filter;
            }

            dgvPaycodes.DataSource = _dt;
            foreach (DataRowView row in _dt.DefaultView)
            {
                _emp_name = (string)row["EmpName"];
                //_duty = (string)row["Duty"]; //GS180502 - BUG
                if ( row["Duty"] != System.DBNull.Value ) _duty = (string)row["Duty"];

                int shift = (Int32)row["LogShift"];
                _max_shift = (shift >= _max_shift ? shift+1 : _max_shift);

                if (row["PayCode"].Equals(DBNull.Value)) row["PayCode"] = _defpaycode;
                //if (row["ToffCode"].Equals(DBNull.Value) || row["ToffCode"].Equals(string.Empty)) continue;
                //if (!row["LogVessel"].Equals(DBNull.Value)) continue;
                
                //string paycode = (string)row["PayCode"];

                //Decimal hour = 0.0M;
                ////Decimal over = 0.0M;
                ////Decimal over1 = 0.0M;

                //if (!row["LogHours"].Equals(DBNull.Value)) hour = (Decimal)row["LogHours"];
                ////if (!row["LogOver"].Equals(DBNull.Value)) over = (Decimal)row["LogOver"];
                ////if (!row["LogOver1"].Equals(DBNull.Value)) over1 = (Decimal)row["LogOver1"];

                //////chkHourly.Checked = false;
                //chkHourly.Checked = true;
                //if (_duty.Equals("Office"))
                //{
                //    //chkHourly.Checked = true;
                    
                //    decimal perday = 8.0M;
                //    if (paycode.Equals("Dispatch")) perday = 12.0M;
                //    if (paycode.Equals("Office")) perday = 7.0M;

                //    if (paycode.Equals("Salary") ||
                //        paycode.Equals("Dispatch") ||
                //        paycode.Equals("Shore") ||
                //        paycode.Equals("Office"))
                //    {
                //        hour = hour * perday;
                //        row["LogHours"] = hour;
                //    }

                ////    if (hour != 0.0M) row["LogHours"] = hour * perday;
                ////    if (over != 0.0M) row["LogOver"] = over * perday;
                //}

            }

            tbxEmpId.Text = _emp_name;
            tbxDuty.Text = _duty;

            read_mode();

            _frm.ShowDialog();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void lblClose_Click(object sender, EventArgs e)
        {
            cancel_mode();
            _frm.Hide();
        }

        private void lblEdit_Click(object sender, EventArgs e)
        {
            edit_mode();
        }


        private void dgvPaycodes_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            show_totals();
        }


        private void dgvPaycodes_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            cmdDelete.Visible = false;
            if (dgv.SelectedRows.Count == 1)
            {
                DataGridViewRow row = dgv.SelectedRows[0];

                int shift = Convert.ToInt32(row.Cells["LogShift"].Value);
                if (shift >= 10)
                    cmdDelete.Visible = true;
            }
        }


        //private void dgvPaycodes_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        //{
        //    e.Row.Cells["Pay Code"].Value = "Salary";
        //}
    }
}
