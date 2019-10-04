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
    public partial class ucNote : UserControl
    {
        private bool _save_exit { get; set; }
        private bool _delete_exit { get; set; }

        private string _memo { get; set; }
        private string _emp_id { get; set; }
        private DateTime _day { get; set; }

        private Form _frm_note { get; set; }

        private int _org_x { get; set; }
        private int _org_y { get; set; }


        public bool IsAccept()
        {
            return _save_exit;
        }

        public bool IsDelete()
        {
            return _delete_exit;
        }


        public string GetMemo()
        {
            return _memo;
        }


        public ucNote(string emp_id, string memo, DateTime day)
        {
            //InitializeComponent();

            _frm_note = new Form();

            _frm_note.FormBorderStyle = FormBorderStyle.None;
            
            //_frm_note.MaximizeBox = false;            
            //_frm_note.MinimizeBox = false;
            
            //_frm_note.StartPosition = FormStartPosition.CenterScreen;

            _day = day;
            _memo = memo;
            _emp_id = emp_id;
        }


        public void ShowNote()
        {
            InitializeComponent(); 
            
            _frm_note.BackColor = Color.Yellow;

            tbxMemo.KeyPress += new KeyPressEventHandler(tbxMemo_KeyPress);

            _save_exit = false;
            _delete_exit = false;

            nudNote.Value = 1;
            dtpDay.Value = _day;
            tbxMemo.Text = _memo;
            tbxEmpId.Text = _emp_id;

            lblEdit.Show();
            cmdSave.Hide();
            cmdDelete.Hide();

            _frm_note.Controls.Add(this);
            
            _frm_note.Size = new Size(300, 180);
            _frm_note.BackColor = Color.Yellow;
            _frm_note.AutoSize = true;

                //tlpNote.Location = new Point(200, 200);
                //tlpNote.BackColor = Color.Yellow;
                //tlpNote.AutoSize = true;

            lblClose.Focus();

            _frm_note.ShowDialog();
        }


        void tbxMemo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }



        public void EditNote()
        {
            InitializeComponent(); 
            
            _frm_note.BackColor = Color.Yellow;

            _save_exit = false;
            _delete_exit = false;

            nudNote.Value = 1;
            dtpDay.Value = _day;            
            tbxEmpId.Text = _emp_id;
            tbxMemo.Text = _memo;

            lblEdit.Hide();
            cmdSave.Show();
            
            cmdDelete.Hide();
            if (_memo != null && _memo.Length > 0)
                cmdDelete.Show();

            _frm_note.Controls.Add(this);

            _frm_note.Size = new Size(300, 180);
            _frm_note.BackColor = Color.Yellow;
            _frm_note.AutoSize = true;
            
            //tlpNote.Location = new Point(200, 200);
            //tlpNote.BackColor = Color.Yellow;
            //tlpNote.AutoSize = true;

            _frm_note.ShowDialog();            
        }


        private void ucNote_MouseUp(object sender, MouseEventArgs e)
        {
            _org_x = 0;
            _org_y = 0;
        }

        private void ucNote_MouseDown(object sender, MouseEventArgs e)
        {
            _org_x = e.X;
            _org_y = e.Y;
        }

        private void ucNote_MouseMove(object sender, MouseEventArgs e)
        {
            if (_org_x != 0 && _org_y != 0)
                _frm_note.SetDesktopLocation(MousePosition.X - _org_x, MousePosition.Y - _org_y);
        }


        private void txtEmpid_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        
        private void tbxNote_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            _save_exit = true;
            _memo = tbxMemo.Text;

            _frm_note.Close();            
        }


        private void lblClose_Click(object sender, EventArgs e)
        {
            _save_exit = false;

            _frm_note.Close();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            Button cmd = (Button)sender;

            tbxMemo.Text = string.Empty;

            tbxMemo.Hide();
            cmd.Hide();

            _delete_exit = true;
        }

    }
}
