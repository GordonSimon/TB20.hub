using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Windows.Forms;

using System.Diagnostics;


namespace letTB_logKF
{
    class dlgGeneric
    {
        const string fname = "PayrollSlip.txt";

        public int PageSize { get; set; }
        public int MaxPageSize { get; set; }

        public int TopLines { get; set; }
        public int MaxTopLines { get; set; }

        public int LinesPerPage { get; set; }
        public int MaxLinesPerPage { get; set; }

        public int OddCountPerReport { get; set; }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public List<string[]> AllData { get; set; }

        private ListBox lbxPage { get; set; }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public dlgGeneric()
        {
            PageSize = 2;
            MaxPageSize = 4;

            TopLines = 5;
            MaxTopLines = 10;

            LinesPerPage = 28;
            MaxLinesPerPage = 40;

            AllData = null;
            lbxPage = new ListBox();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public void Print(string fname)
        {
            string sPath = fname;

            System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(sPath);
            foreach (var line in lbxPage.Items)
                SaveFile.WriteLine(line);
            SaveFile.Close();

            Process.Start("wordpad.exe", sPath);       
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/


        private void draw_blank()
        {
            int lines = LinesPerPage;
            for (int newline = 0; newline < lines; newline++) lbxPage.Items.Add("");
        }


        private void draw_slip(int n, string[] slip)
        {            
            int lines = LinesPerPage;
            int number_of_lines_for_this_slip = slip.Length;
            //lbxPage.Items.Add(string.Format("Slip # : [{0}], Number of lines : [{1}]", (n + 1), number_of_lines_for_this_slip));            
            //int adjusted_toplines = TopLines - 1;

            int adjusted_toplines = TopLines - 0; // not need if Slip # not printed

            if (number_of_lines_for_this_slip <= (LinesPerPage - adjusted_toplines))
            {
                for (int newline = 0; newline < adjusted_toplines; newline++) lbxPage.Items.Add("");
                lbxPage.Items.AddRange(slip);
                lines = lines - TopLines - slip.Length;
                for (int newline = 0; newline < lines; newline++) lbxPage.Items.Add("");
            }
            else
            {
                lines += LinesPerPage;

                if (n % 2 == OddCountPerReport) draw_blank();

                for (int newline = 0; newline < adjusted_toplines; newline++) lbxPage.Items.Add("");
                lbxPage.Items.AddRange(slip);
                lines = lines - TopLines - slip.Length;
                for (int newline = 0; newline < lines; newline++) lbxPage.Items.Add("");

                OddCountPerReport = (OddCountPerReport == 1 ? 0 : 1);
            }

        }


        private void draw_page()
        {            
            int sz = PageSize + 1;    
            int slips = 0; 
            foreach (var i in AllData)
            {
                //if (slips == 17) MessageBox.Show(string.format("Found this slip number {0} : {1}", slips, i[0]));

                draw_slip(slips, i);

                if (++slips >= sz) break;
            }
        }


        //private void draw_page()
        //{
        //    int n = 0;
        //    int sz = PageSize + 1;
        //    int lines;
        //    int slips = 0;
        //    foreach (var i in AllData)
        //    {
        //        lines = LinesPerPage;
        //        if (n % sz == 0) n = 0;
        //        n++;
        //        for (int newline = 0; newline < TopLines; newline++) lbxPage.Items.Add("");
        //        lbxPage.Items.AddRange(i);
        //        lines = lines - TopLines - ((string[])i).Length;
        //        for (int newline = 0; newline < lines; newline++) lbxPage.Items.Add("");
        //        if (++slips >= sz) break; // break out after printing last slip
        //        //if (n % sz == 0)
        //        //    break;
        //    }
        //}


        public DialogResult Render(List<string[]> data)
        {
            const string title = "Print PaySlips with NotePad";

            AllData = data;
            DialogResult r;

            //if (_dsPacket == null) { MessageBox.Show("No data to send !"); return DialogResult.Cancel; }

            try
            {
                using (Form frm = new Form())
                {
                    //frm.Text = "Update Rows";

                    PageSize = data.Count;
                    MaxPageSize = data.Count;
                    OddCountPerReport = 1;
        
                    //ListBox lbx = new ListBox();
                    lbxPage.Font = new Font("Courier New", 9.75f);
                    lbxPage.Location = new Point(10, 10);
                    lbxPage.Size = new Size(frm.Width - 40, frm.Height - 100);
                    lbxPage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

                    PageSize = PageSize - 1; // adjust for zero base
                    TopLines = TopLines - 1; // adjust for zero base                
                    LinesPerPage = LinesPerPage - 1; // adjust for zero base                

                    draw_page();

                    frm.Controls.Add(lbxPage);

                    frm.Text = title;
                    //frm.WindowState = FormWindowState.Maximized;
                    frm.StartPosition = FormStartPosition.CenterParent;



                    //frm.Size = new Size(240, 120);
                    frm.Height = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Height * 0.75);
                    frm.Width = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Width * 0.50);

                    Button btnO = new Button();
                    btnO.Text = "OK";
                    btnO.Location = new Point(frm.Width - 200, frm.Height - 75);
                    btnO.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

                    Button btnC = new Button();
                    btnC.Text = "Cancel";
                    btnC.Location = new Point(frm.Width - 104, frm.Height - 75);
                    btnC.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

                    Button btnSize = new Button();
                    btnSize.Text = string.Format("Slips [{0}]", PageSize + 1);
                    btnSize.Location = new Point(frm.Width - 300, frm.Height - 75);
                    btnSize.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

                    Button btnTop = new Button();
                    btnTop.Text = string.Format("Top [{0}]", TopLines + 1);
                    btnTop.Location = new Point(frm.Width - 400, frm.Height - 75);
                    btnTop.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

                    Button btnPage = new Button();
                    btnPage.Text = string.Format("Lines [{0}]", LinesPerPage + 1);
                    btnPage.Location = new Point(frm.Width - 500, frm.Height - 75);
                    btnPage.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;


                    frm.AcceptButton = btnO;
                    frm.CancelButton = btnC;

                    btnO.Click += new EventHandler(btnOK_Click);
                    btnC.Click += new EventHandler(btnC_Click);
                    btnSize.Click += new EventHandler(btnSize_Click);
                    btnTop.Click += new EventHandler(btnTop_Click);
                    btnPage.Click += new EventHandler(btnPage_Click);

                    frm.Controls.Add(btnO);
                    frm.Controls.Add(btnC);
                    frm.Controls.Add(btnSize);
                    frm.Controls.Add(btnTop);
                    frm.Controls.Add(btnPage);

                    r = frm.ShowDialog();
                    return r;
                }
            }
            catch (Exception ex)
            {
                errDash.Fail(System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }

            return DialogResult.Cancel;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        void btnSize_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Form frm = (Form)btn.Parent;

            PageSize = ++PageSize % MaxPageSize;

            btn.Text = string.Format("Slips [{0}]", PageSize + 1);
            lbxPage.Items.Clear();
            draw_page();
                   
            frm.Refresh();
        }


        void btnTop_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Form frm = (Form)btn.Parent;

            TopLines = ++TopLines % MaxTopLines;

            btn.Text = string.Format("Top [{0}]", TopLines + 1);
            lbxPage.Items.Clear();
            draw_page();

            frm.Refresh();
        }


        void btnPage_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Form frm = (Form)btn.Parent;

            LinesPerPage = ++LinesPerPage % MaxLinesPerPage;

            btn.Text = string.Format("Lines [{0}]", LinesPerPage + 1);
            lbxPage.Items.Clear();
            draw_page();

            frm.Refresh();
        }


        void btnOK_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Form frm = (Form)btn.Parent;
            frm.DialogResult = DialogResult.OK;

            Print(fname);
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        static void btnC_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Form frm = (Form)btn.Parent;
            frm.DialogResult = DialogResult.Cancel;
        }

        static void btnO_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Form frm = (Form)btn.Parent;
            frm.DialogResult = DialogResult.OK;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public static DialogResult DialogNext(List<string[]> page)
        {
            const string title = "Continue ...";

            DialogResult r;

            //if (_dsPacket == null) { MessageBox.Show("No data to send !"); return DialogResult.Cancel; }

            try
            {
                using (Form frm = new Form())
                {
                    //frm.Text = "Update Rows";

                    Label msg = new Label();
                    msg.Text = "Print more ?";
                    msg.Location = new Point(frm.Width - 210, frm.Height - 100);
                    frm.FormBorderStyle = FormBorderStyle.FixedDialog;

                    ListBox lbx = new ListBox();
                    lbx.Font = new Font("Courier New", 9.75f);
                    lbx.Location = new Point(10, 10);
                    lbx.Size = new Size(frm.Width - 40, frm.Height - 100);                    
                    lbx.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

                    foreach (var i in page)
                        lbx.Items.AddRange(i);
                    
                    frm.Controls.Add(lbx);

                    frm.Text = title;
                    //frm.WindowState = FormWindowState.Maximized;
                    frm.StartPosition = FormStartPosition.CenterParent;
                    
                    

                    //frm.Size = new Size(240, 120);
                    frm.Height = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Height * 0.75);
                    frm.Width = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Width * 0.50);

                    Button btnO = new Button();
                    btnO.Text = "OK";
                    btnO.Location = new Point(frm.Width - 200, frm.Height - 80);
                    btnO.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

                    Button btnC = new Button();
                    btnC.Text = "Cancel";
                    btnC.Location = new Point(frm.Width - 104, frm.Height - 80);
                    btnC.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

                    frm.AcceptButton = btnO;
                    frm.CancelButton = btnC;

                    btnO.Click += new EventHandler(btnO_Click);
                    btnC.Click += new EventHandler(btnC_Click);



                    frm.Controls.Add(btnO);
                    frm.Controls.Add(btnC);
                    
                    //frm.Controls.Add(dgv);

                    r = frm.ShowDialog();
                    return r;
                }
            }
            catch (Exception ex)
            {
                errDash.Fail(System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }

            return DialogResult.Cancel;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public static DialogResult DialogNext(string fname)
        {
            DialogResult r;

            //if (_dsPacket == null) { MessageBox.Show("No data to send !"); return DialogResult.Cancel; }

            try
            {
                using (Form frm = new Form())
                {
                    //frm.Text = "Update Rows";


                    Label msg = new Label();
                    msg.Text = "Print more ?";
                    msg.Location = new Point(frm.Width - 210, frm.Height - 100);
                    frm.FormBorderStyle = FormBorderStyle.FixedDialog;


                    string title = "Continue ...";

                    frm.Text = title;
                    //frm.WindowState = FormWindowState.Maximized;
                    frm.StartPosition = FormStartPosition.CenterParent;

                    //frm.Size = new Size(240, 120);
                    frm.Height = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Height * 0.75);
                    frm.Width = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Width * 0.75);

                    Button btnO = new Button();
                    btnO.Text = "OK";
                    btnO.Location = new Point(frm.Width - 200, frm.Height - 80);
                    btnO.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

                    Button btnC = new Button();
                    btnC.Text = "Cancel";
                    btnC.Location = new Point(frm.Width - 104, frm.Height - 80);
                    btnC.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

                    frm.AcceptButton = btnO;
                    frm.CancelButton = btnC;

                    btnO.Click += new EventHandler(btnO_Click);
                    btnC.Click += new EventHandler(btnC_Click);

  

                    frm.Controls.Add(btnO);
                    frm.Controls.Add(btnC);
                    frm.Controls.Add(msg);
                    //frm.Controls.Add(dgv);

                    r = frm.ShowDialog();
                    return r;
                }
            }
            catch (Exception ex)
            {
                errDash.Fail(System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }

            return DialogResult.Cancel;
        }
         

    }
}
