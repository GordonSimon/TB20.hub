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
    public partial class ucVesNum : UserControl
    {
        public void Criteria(string data)
        {
            tbxFilter.Text = data;
        }


        public object Key()
        {
            return lbxVes.SelectedValue;
        }
            

        public ucVesNum()
        {
            InitializeComponent();
        }


        private void ucVesNum_Load(object sender, EventArgs e)
        {
            DataTable dt = qryVessel.GetView("All");
            
            BindingSource bs = new BindingSource();
            bs.DataSource = dt;

            lbxVes.DataSource = bs;
            lbxVes.DisplayMember = "Ident";
            lbxVes.ValueMember = "Short";
        }


        private void tbxFilter_TextChanged(object sender, EventArgs e)
        {
            BindingSource bs = (BindingSource)lbxVes.DataSource;

            bs.Filter = "NumID Like '" + tbxFilter.Text + "%'";
        }
    }
}