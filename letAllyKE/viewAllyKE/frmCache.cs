using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using mdlAllyKE;


namespace viewAllyKE
{
    public partial class frmCache : Form
    {
        private frmApp _frm { get; set; }
        private DataSet _login { get; set; }

        
        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public frmCache(string assembly_version)
        {
            InitializeComponent();

            pbxApp.Hide();
            lblError.Hide();

            lblInfo.Text = info();
            lblVersion.Text = string.Format("Version : {0}", assembly_version);
            this.Text = string.Format("Copyright (C) GarNet RC 2014 - {0}", DateTime.Now.Year);
        }


        private void frmCache_Load(object sender, EventArgs e)
        {
            lblDB.Text = dacCache.GetDBInfo();            

            cmdOK.Hide();

            bwkCache.WorkerReportsProgress = true;
            
            bwkCache.RunWorkerAsync();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void report_login_error(string msg)
        {
            lblWait.Hide();
            lblError.Show();

            errorProvider1.SetError(pbxApp, msg);            
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void bwkCache_DoWork(object sender, DoWorkEventArgs e)
        {
            _login = dacCache.GetLogin();
            if (_login != null)
            {
                bwkCache.ReportProgress(50);
                
                dacCache.GetCache();
            }            
        }


        private void bwkCache_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_login == null)
            {
                pbxApp.Show();
                report_login_error(dacCache.LoginError);
                _frm = new frmApp();
                cmdOK.Show();
            }
            else
            {
                lblWait.Hide();
                cmdOK.Show();
            }

            //if (_frm != null) _frm.Tag = true;
        }


        private void bwkCache_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //cmdLogin1.Show();
            //cmdLogin2.Show();
            //cmdLogin3.Show();

            _frm = new frmApp();
            pbxApp.Show();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/


        private string info()
        {
            int w = Screen.PrimaryScreen.Bounds.Width;
            int h = Screen.PrimaryScreen.Bounds.Height;
            int depth = Screen.PrimaryScreen.BitsPerPixel;
            bool p = Screen.PrimaryScreen.Primary;
            string ps = (p ? "P" : "S"); 

            string msg = string.Format("{0} : {1} x {2} @ {3}", ps, w, h, depth);

            return msg;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void StartApp()
        {
            this.Hide();

            _frm.WindowState = FormWindowState.Maximized;

            _frm.Show(this);
            
        }


        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void cmdLogin1_Click(object sender, EventArgs e)
        {
            StartApp();
        }

        private void cmdLogin2_Click(object sender, EventArgs e)
        {
            StartApp();
        }

        private void cmdLogin3_Click(object sender, EventArgs e)
        {
            StartApp();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            Button cmd = (Button)sender;

            cmd.Hide();
            StartApp();
        }
    }
}
