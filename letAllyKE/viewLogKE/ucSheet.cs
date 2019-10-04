using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace viewLogKE
{
    public partial class ucSheet : UserControl
    {
        public string LogID { get; set; }
        public string LogShift { get; set; }
        public string LogHoursStart { get; set; }
        public string LogHoursFinish { get; set; }
        public string LogFuel { get; set; }

        private List<string>[] log_parts { get; set; }
        private Dictionary<string, List<string>> log_list { get; set; }


        private void add_log()
        {
            List<string> item;
            LogID = cbxLogID.Text;
            LogShift = tbxShift.Text;
            LogHoursStart = tbxStart.Text;
            LogHoursFinish = tbxFinish.Text;
            LogFuel = tbxFuel.Text;

            if (log_list.ContainsKey(LogID))
                item = log_list[LogID];
            else
                log_list.Add(LogID, item = new List<string>(new string[5]));

            item[0] = LogID;
            item[1] = LogShift;
            item[2] = LogHoursStart;
            item[3] = LogHoursFinish;
            item[4] = LogFuel;

            //cbxLogID.DataSource = new BindingSource(log_list, null);
            //cbxLogID.DisplayMember = "Key";

            cbxLogID.DataSource = new BindingSource(log_list, null);
            cbxLogID.DisplayMember = "Key";

        }


        public ucSheet()
        {
            InitializeComponent();

            log_list = new Dictionary<string, List<string>>();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            add_log();            
            this.ParentForm.DialogResult = DialogResult.OK;
            this.ParentForm.Hide();                       
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.ParentForm.DialogResult = DialogResult.Cancel;
            this.ParentForm.Hide();
        }


        private void clear_form()
        {
            tbxShift.Text = "";
            tbxStart.Text = "";
            tbxFinish.Text = "";
            tbxFuel.Text = "";
        }


        private void cbxLogID_SelectionChangeCommitted(object sender, EventArgs e)
        {
            List<string> item;

            string s = cbxLogID.GetItemText(cbxLogID.SelectedItem);

            if (log_list.ContainsKey(s))
            {

                item = log_list[s];

                LogShift = item[1];
                LogHoursStart = item[2];
                LogHoursFinish = item[3];
                LogFuel = item[4];

                tbxShift.Text = LogShift;
                tbxStart.Text = LogHoursStart;
                tbxFinish.Text = LogHoursFinish;
                tbxFuel.Text = LogFuel;
            }
        }


        private void ucSheet_Load(object sender, EventArgs e)
        {           
            if (log_list.Count > 0)
            {
                cbxLogID_SelectionChangeCommitted(null, null);
            }
        }

        private void cbxLogID_TextChanged(object sender, EventArgs e)
        {            
        }
  
    }
}
