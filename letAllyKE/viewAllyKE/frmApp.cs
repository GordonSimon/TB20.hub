using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace viewAllyKE
{
    public partial class frmApp : Form, CueWeek
    {
        //private TableLayoutPanel tlpNote = new TableLayoutPanel();

        private ucAlley _ucAlley = null;

        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        public void Msg() { if (_ucAlley != null)  _ucAlley.RefreshAlley(); }


        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        public frmApp()
        {
            InitializeComponent();
            
            //tlpNote.Name = "tlpNote";
            //tlpNote.Size = new Size(300, 200);
            //tlpNote.Location = new Point(200, 200);
            //tlpNote.BackColor = Color.Yellow;
            //tlpNote.AutoSize = true;

            //tlpNote.Hide();
            //this.Controls.Add(tlpNote);


        }
        
        private void frmMain_Load(object sender, EventArgs e)
        {            
            _ucAlley = new ucAlley();
            
            _ucAlley.Dock = DockStyle.Fill;

            this.Controls.Add(_ucAlley);           
        }

        private void frmApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Owner.Show();
        }


        private void frmApp_SizeChanged(object sender, EventArgs e)
        {
            int w = this.Size.Width;
            int h = this.Size.Height;

            //MessageBox.Show(string.Format("app w{0}, h{1}", w, h));
        }
    
    }
}
