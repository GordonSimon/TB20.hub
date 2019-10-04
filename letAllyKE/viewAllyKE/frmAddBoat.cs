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
    public partial class frmAddBoat : Form
    {
        public string SelectCode { get; set; }
        public string SelectName { get; set; }

        private DataSet _ds_vessels;
        private List<string> _vessel_list;


        public frmAddBoat(DataSet ds_vessels, List<string> vessel_list)
        {
            InitializeComponent();

            _ds_vessels = ds_vessels;
            _vessel_list = vessel_list;
        }

        private void frmAddBoat_Load(object sender, EventArgs e)
        {
            foreach (DataRow row in _ds_vessels.Tables[0].Rows)
            {
                if (_vessel_list.Contains(row["Short"])) continue;

                Item it = new Item();
                it.Tag = row["Short"];
                it.Text = row["Full Name"].ToString();
                cbxItems.Items.Add(it);
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            SelectCode = "";
            SelectName = "";

            this.Close();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            Item itv = (Item)(cbxItems.SelectedItem);

            SelectCode = (itv == null ? "" : itv.Tag.ToString());
            SelectName = itv.ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

    }
}

