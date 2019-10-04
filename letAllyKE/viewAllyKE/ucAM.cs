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
    public partial class ucAM : UserControl
    {
        public int VAL;

        public ucAM()
        {
            InitializeComponent();
        }


        public void On(int am)
        {
            VAL = am;

            if (am == 0) this.Hide();


            lblAM.Hide();
            lblPM.Hide();
            
            if (am == 1) lblAM.Show();
            if (am == 2) lblPM.Show();
            if (am == 3) { lblAM.Show(); lblPM.Show(); }

            this.Show();
        }
    }
}
