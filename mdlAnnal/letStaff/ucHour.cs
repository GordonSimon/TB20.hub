using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace letStaff
{
    public partial class ucHour : UserControl
    {
        private bool _save_exit { get; set; }

        private Decimal _hour { get; set; }
        private Decimal _over { get; set; }
        private string _vessel { get; set; }

        private string _emp_id { get; set; }
        private DateTime _day { get; set; }

        private Form _frm_hour { get; set; }

        private int _org_x { get; set; }
        private int _org_y { get; set; }


        public bool IsAccept()
        {
            return _save_exit;
        }

        public Decimal GetHour()
        {
            return _hour;
        }

        public Decimal GetOver()
        {
            return _over;
        }

        public string GetVessel()
        {
            return _vessel;
        }
        
    
        public ucHour(string emp_id, DateTime day, Decimal hour, Decimal over, string vessel)
        {
            //InitializeComponent();

            _frm_hour = new Form();

            _frm_hour.FormBorderStyle = FormBorderStyle.None;
            
            //_frm_note.MaximizeBox = false;            
            //_frm_note.MinimizeBox = false;
            
            //_frm_note.StartPosition = FormStartPosition.CenterScreen;

            _day = day;
            _hour = hour;
            _over = over;
            _vessel = vessel;
            _emp_id = emp_id;
        }


        public void ShowHour()
        {
            InitializeComponent();

            _frm_hour.BackColor = Color.FromArgb(192, 255, 192);

            //tbxVessel.KeyPress += new KeyPressEventHandler(tbxVessel_KeyPress);
            
            dtpDay.Value = _day;
            tbxHour.Text = _hour.ToString();
            tbxOver.Text = _over.ToString();
            tbxVessel.Text = _vessel;
            tbxEmpId.Text = _emp_id;

            lblEdit.Show();
            cmdSave.Hide();

            tbxHour.ReadOnly = true;
            tbxOver.ReadOnly = true;
            tbxVessel.ReadOnly = true;
            
            _frm_hour.Controls.Add(this);
            
            _frm_hour.Size = new Size(300, 180);
            _frm_hour.BackColor = Color.FromArgb(192, 255, 192);
            _frm_hour.AutoSize = true;

                //tlpNote.Location = new Point(200, 200);
                //tlpNote.BackColor = Color.Yellow;
                //tlpNote.AutoSize = true;

            _frm_hour.StartPosition = FormStartPosition.CenterParent;            
            _frm_hour.ShowDialog();
        }


        //void tbxHour_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    e.Handled = true;
        //}


        //void tbxOver_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    e.Handled = true;
        //}


        //void tbxVessel_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    e.Handled = true;
        //}


        void tbxEmpId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }


        public void EditHour()
        {
            InitializeComponent();

            _frm_hour.BackColor = Color.FromArgb(192, 255, 192);
            
            dtpDay.Value = _day;            
            tbxEmpId.Text = _emp_id;

            tbxHour.Text = _hour.ToString();
            tbxOver.Text = _over.ToString();
            tbxVessel.Text = _vessel;

            lblEdit.Hide();
            cmdSave.Show();

            _frm_hour.Controls.Add(this);

            _frm_hour.Size = new Size(300, 180);
            _frm_hour.BackColor = Color.FromArgb(192, 255, 192);
            _frm_hour.AutoSize = true;
            
            //tlpNote.Location = new Point(200, 200);
            //tlpNote.BackColor = Color.Yellow;
            //tlpNote.AutoSize = true;

            _frm_hour.ShowDialog();            
        }


        private void ucHour_MouseUp(object sender, MouseEventArgs e)
        {
            _org_x = 0;
            _org_y = 0;
        }


        private void ucHour_MouseDown(object sender, MouseEventArgs e)
        {
            _org_x = e.X;
            _org_y = e.Y;
        }


        private void ucHour_MouseMove(object sender, MouseEventArgs e)
        {
            if (_org_x != 0 && _org_y != 0)
                _frm_hour.SetDesktopLocation(MousePosition.X - _org_x, MousePosition.Y - _org_y);
        }


        private void lblEdit_Click(object sender, EventArgs e)
        {
            lblEdit.Hide();
            cmdSave.Show();

            tbxHour.ReadOnly = false;
            tbxOver.ReadOnly = false;
            tbxVessel.ReadOnly = false;
        }


        private void cmdSave_Click(object sender, EventArgs e)
        {
            _save_exit = true;

            _hour = Convert.ToDecimal(tbxHour.Text);
            _over = Convert.ToDecimal(tbxOver.Text);
            _vessel = tbxVessel.Text;

            _frm_hour.Close();            
        }


        private void lblClose_Click(object sender, EventArgs e)
        {
            _save_exit = false;

            _frm_hour.Close();
        }
    }
}
