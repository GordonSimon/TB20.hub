using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using mdlAllyKE;


namespace viewAllyKE
{
    public partial class ucShfNum : UserControl
    {
        public void Criteria(string data)
        {
            tbxFilter.Text = data;
        }


        public object Key()
        {
            // return lbxShf.SelectedValue;
            int time;
            bool r = Int32.TryParse(tbxFilter.Text, out time);

            if (lbxShf.SelectedValue == null)
            {
                if (r & time < 1200) return 1; // am
                return 2; // else pm
            }
            else
            {
                if (r & time == 600) return 1; // am
                if (r & time == 1800) return 2; // pm
                if (r & time == 1200) return 3; // noon
                if (r & time == 2400) return 4; // 24 hours
            }

            return 0;            
        }
            

        public ucShfNum()
        {
            InitializeComponent();
        }


        private void ucShfNum_Load(object sender, EventArgs e)
        {
            DataTable dt = qryShift.GetView("All");
            
            BindingSource bs = new BindingSource();
            bs.DataSource = dt;

            lbxShf.DataSource = bs;
            lbxShf.DisplayMember = "Ident";
            lbxShf.ValueMember = "NumID";
        }


        private void tbxFilter_TextChanged(object sender, EventArgs e)
        {
            BindingSource bs = (BindingSource)lbxShf.DataSource;

            bs.Filter = "NumID Like '" + tbxFilter.Text + "%'";
        }
    }
}