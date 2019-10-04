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
    public partial class ucEmpNum : UserControl
    {
        private BindingSource _bs_M;
        private BindingSource _bs_D;


        public void PutBinding(string name)
        {
            _bs_M.Filter = null;
            _bs_D.Filter = null;

            lbxEmp.DataSource = (name.Equals("Master") ? _bs_M : _bs_D);
            lbxEmp.Refresh();
        }


        public void Criteria(string data)
        {
            tbxFilter.Text = data;
        }


        public object Key()
        {
            if (lbxEmp.SelectedValue == null)
                return "<error>";

            return lbxEmp.SelectedValue;
        }
            

        public ucEmpNum()
        {
            InitializeComponent();

            DataTable dt = qryGang.GetView("Master");

            BindingSource bs = new BindingSource();
            bs.DataSource = dt;
            
            _bs_M = bs;

            dt = qryGang.GetView("Deckhand");

            bs = new BindingSource();
            bs.DataSource = dt;

            _bs_D = bs;
        }


        private void ucEmpNum_Load(object sender, EventArgs e)
        {
            lbxEmp.DataSource = _bs_M;
            lbxEmp.DisplayMember = "Ident";
            lbxEmp.ValueMember = "EmpID";
        }


        private void tbxFilter_TextChanged(object sender, EventArgs e)
        {
            BindingSource bs = (BindingSource)lbxEmp.DataSource;

            bs.Filter = "EmpNum Like '" + tbxFilter.Text + "%'";
        }
    }
}