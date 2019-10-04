using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data;


namespace letTB_logKF
{
    public partial class ucLog : UserControl
    {
        public DataTable DT { get; set; }        
        public DateTime DAY { get; set; }
        public string BOAT { get; set; }

        private List<int> _shifts { get; set; }
        private Dictionary<int, string> _codes { get; set; }
        

        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public ucLog()
        {
            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        }



        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void size_uc()
        {
            if (_shifts == null) return;
            int count = _shifts.Count;

            int panel_height = panelPM.Size.Height;
            int panel_default = panelAll.Size.Height;
            
            if (count >= 3) return;
            
            bool found_def = false;
            foreach (int s in _shifts)
                if (s != 1 && s != 2) found_def = true;

            if (count == 2)
            {
                if (! found_def)
                    this.Size = new Size(this.Size.Width, this.Size.Height - panel_default);                
            }
            if (count == 1)
            {
                if (_shifts[0] == 1)                                    
                    this.Size = new Size(this.Size.Width, this.Size.Height - panel_default - panel_height);                

                if (_shifts[0] == 2)
                {
                    panelPM.Location = panelAM.Location;
                    this.Size = new Size(this.Size.Width, this.Size.Height - panel_default - panel_height);
                }

                if (_shifts[0] != 1 && _shifts[0] != 2)
                {
                    panelAll.Location = panelAM.Location;
                    this.Size = new Size(this.Size.Width, this.Size.Height - panel_height - panel_height);
                }
            }

            if (found_def) return;

            panelAll.Hide();
            panelAM.Hide();
            panelPM.Hide();
            foreach (int s in _shifts)
            {
                if (s == 1) panelAM.Show();
                if (s == 2) panelPM.Show();
                //if (s >= 3) panelAll.Show();
            }
           
        }


        private void load_shifts(DataTable dt, DateTime d, string boat)
        {
            try
            {
                if (_shifts == null) _shifts = new List<int>();                
                if (_codes == null) _codes = new Dictionary<int, string>();                

                DataTable tbl_codes = dacShift.GetDT();
                DataTable r = qryTimebook.qShifts(dt, d, boat);
                if (r == null) return;

                int idx = 0;
                foreach (DataRow row in tbl_codes.Rows)
                    _codes.Add(idx++, (string)row["Short"]);

                foreach (DataRow row in r.Rows)
                    _shifts.Add((int) row["LogShift"]);

                foreach (int s in _shifts)
                {
                    Color bk = Color.Green;
                    if (s == 1) bk = Color.Blue;
                    if (s == 2) bk = Color.Black;

                    Label lbl = new Label();
                    lbl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                    lbl.BackColor = bk;
                    lbl.ForeColor = Color.White;
                    lbl.Size = new Size(lbl.Size.Width - 20, lbl.Size.Height);
                    lbl.TextAlign = ContentAlignment.TopCenter;

                    lbl.Tag = s;
                    lbl.Text = _codes[s];                    

                    lbl.Click += new EventHandler(lbl_Click);

                    flpShifts.Controls.Add(lbl);
                }
            }
            catch (Exception ex)
            {
                errDash.Fail(System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }

        }


        private void load_dgv(DataGridView dgv, DataTable dt, DateTime d, string boat, int shift)
        {
            string sql = string.Format("BookDate = '{0}' and LogVessel = '{1}' and LogShift = {2}", d, boat, shift);
            DataTable v = (new DataView(dt, sql, null, System.Data.DataViewRowState.CurrentRows)).ToTable();

            dgv.AutoResizeColumns();
            dgv.DataSource = v;            

        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void set_controls(TextBox I, TextBox E, TextBox S, TextBox A, TextBox O, TextBox P, CheckBox chk,
            string emp_id, string emp_name, string code, string actual, string overtime, string paid, bool regular)
        {
            I.Text = emp_id; E.Text = emp_name; S.Text = code; A.Text = actual; O.Text = overtime; P.Text = paid; chk.Checked = regular;
        }


        private void map_am(string resp, string emp_id, string emp_name, int shift, string actual, string overtime, string paid, bool regular)
        {
            string code = _codes[shift];

            switch (resp.Trim())
            {
                case "Capt": set_controls(tbxI1, tbxE1, tbxS1, tbxA1, tbxO1, tbxP1, chkH1, emp_id, emp_name, code, actual, overtime, paid, regular);  break;
                case "DH(1)": set_controls(tbxI2, tbxE2, tbxS2, tbxA2, tbxO2, tbxP2, chkH2, emp_id, emp_name, code, actual, overtime, paid, regular); break;
                case "DH(2)": set_controls(tbxI3, tbxE3, tbxS3, tbxA3, tbxO3, tbxP3, chkH3, emp_id, emp_name, code, actual, overtime, paid, regular); break;
                case "DH(3)": set_controls(tbxI4, tbxE4, tbxS4, tbxA4, tbxO4, tbxP4, chkH4, emp_id, emp_name, code, actual, overtime, paid, regular); break;

                default: MessageBox.Show(emp_name, resp); break;
            }                
        }


        private void map_pm(string resp, string emp_id, string emp_name, int shift, string actual, string overtime, string paid, bool regular)
        {
            string code = _codes[shift];

            switch (resp.Trim())
            {
                case "Capt": set_controls(tbxI5, tbxE5, tbxS5, tbxA5, tbxO5, tbxP5, chkH5, emp_id, emp_name, code, actual, overtime, paid, regular); break;
                case "DH(1)": set_controls(tbxI6, tbxE6, tbxS6, tbxA6, tbxO6, tbxP6, chkH6, emp_id, emp_name, code, actual, overtime, paid, regular); break;
                case "DH(2)": set_controls(tbxI7, tbxE7, tbxS7, tbxA7, tbxO7, tbxP7, chkH7, emp_id, emp_name, code, actual, overtime, paid, regular); break;
                case "DH(3)": set_controls(tbxI8, tbxE8, tbxS8, tbxA8, tbxO8, tbxP8, chkH8, emp_id, emp_name, code, actual, overtime, paid, regular); break;

                default: MessageBox.Show(emp_name, resp); break;
            }
        }


        private void map_all(string resp, string emp_id, string emp_name, int shift, string actual, string overtime, string paid, bool regular)
        {
            string code = _codes[shift];

            switch (resp.Trim())
            {
                case "Capt": set_controls(tbxIx1, tbxEx1, tbxSx1, tbxAx1, tbxOx1, tbxPx1, chkHx1, emp_id, emp_name, code, actual, overtime, paid, regular); break;
                case "Mate": set_controls(tbxIx2, tbxEx2, tbxSx2, tbxAx2, tbxOx2, tbxPx2, chkHx2, emp_id, emp_name, code, actual, overtime, paid, regular); break;
                case "Mate2": set_controls(tbxIx3, tbxEx3, tbxSx3, tbxAx3, tbxOx3, tbxPx3, chkHx3, emp_id, emp_name, code, actual, overtime, paid, regular); break;
                case "Eng.": set_controls(tbxIx3, tbxEx3, tbxSx3, tbxAx3, tbxOx3, tbxPx3, chkHx3, emp_id, emp_name, code, actual, overtime, paid, regular); break;
                case "DH(1)": set_controls(tbxIx4, tbxEx4, tbxSx4, tbxAx4, tbxOx4, tbxPx4, chkHx4, emp_id, emp_name, code, actual, overtime, paid, regular); break;
                case "DH(2)": set_controls(tbxIx5, tbxEx5, tbxSx5, tbxAx5, tbxOx5, tbxPx5, chkHx5, emp_id, emp_name, code, actual, overtime, paid, regular); break;
                case "DH(3)": set_controls(tbxIx6, tbxEx6, tbxSx6, tbxAx6, tbxOx6, tbxPx6, chkHx6, emp_id, emp_name, code, actual, overtime, paid, regular); break;

                default: MessageBox.Show(emp_name, resp); break;
            }
        }


        private void map_row(DataRow row)
        {
            decimal a = ((decimal)row["LogHours"]);
            decimal o = ((decimal)row["LogOver"]);
            decimal p = a - o;

            int shift = (int)row["LogShift"];
            string emp_id = (string)row["EmpId"];
            string emp_name = (string)row["EmpName"];
            string resp = (string)row["Resp"];
            string actual = a.ToString("#.##");
            string overtime = o.ToString("#.##");
            string paid = p.ToString("#.##");
            bool regular = (a.Equals(12M) && a.Equals(p));

            if (shift == 1)            
                map_am(resp, emp_id, emp_name, shift, actual, overtime, paid, regular);
            
            if (shift == 2)            
                map_pm(resp, emp_id, emp_name, shift, actual, overtime, paid, regular);

            if (shift != 1 && shift != 2)
                map_all(resp, emp_id, emp_name, shift, actual, overtime, paid, regular);
            
        }


        private void draw_log(DataTable dt, DateTime d, string boat)
        {
            try
            {
                string sql = string.Format("BookDate = '{0}' and LogVessel = '{1}'", d, boat);
                DataTable v = (new DataView(dt, sql, null, System.Data.DataViewRowState.CurrentRows)).ToTable();

                if (v == null) return;

                foreach (DataRow row in v.Rows)                
                    map_row(row);
                
            }
            catch (Exception ex)
            {
                errDash.Fail(System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }
        }
           

        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public void LoadLog(DataTable dt, DateTime d, string boat)
        {
            // Note that WM_SetRedraw = 0XB

            // Suspend drawing.
            //UnsafeSharedNativeMethods.SendMessage(handle, WindowMessages.WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero);

            DT = dt;
            DAY = d;
            BOAT = boat;

            lblBoat.Text = boat;
            lblDate.Text = d.ToShortDateString();

            load_shifts(dt, d, boat);
            size_uc();

            this.Refresh();
            Application.DoEvents();

            draw_log(dt, d, boat);
            this.Refresh();
            Application.DoEvents();


            //load_dgv(dgvLog, dt, d, boat, 1);

            // Resume drawing.
            //UnsafeSharedNativeMethods.SendMessage(handle, WindowMessages.WM_SETREDRAW, new IntPtr(1), IntPtr.Zero);

        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        //private void ucLog_Click(object sender, EventArgs e)
        //{
        //    pnlLog.Visible = false;

        //    dgvLog.Dock = DockStyle.Fill;
        //    dgvLog.Visible = true;
        //}

        //private void pnlLog_Click(object sender, EventArgs e)
        //{
        //    pnlLog.Visible = false;

        //    dgvLog.Dock = DockStyle.Fill;
        //    dgvLog.Visible = true;

        //}


        void lbl_Click(object sender, EventArgs e)
        {
            return;

            Label lbl = (Label)sender;

            DataGridView dgv = new DataGridView();

            load_dgv(dgv, DT, DAY, BOAT, (int)lbl.Tag);

            this.Controls.Add(dgv);
            dgv.Dock = DockStyle.Fill;
            dgv.BringToFront();
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void lblEdit_Click(object sender, EventArgs e)
        {

        }

    }
}
