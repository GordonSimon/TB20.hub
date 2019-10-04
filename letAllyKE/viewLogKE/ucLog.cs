using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using mdlAllyKE;


namespace viewLogKE
{
    interface CueLog { void Msg(); }

    public partial class ucLog : UserControl
    {
        public class Item
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString() { return Text; }
        }

        public bool CueRequired { get; set; }
        public string SheetID { get; set; }

        public bool Man6 { get; set; }
        public int Shift { get; set; }
        public string Vessel { get; set; }

        public bool Dirty { get; set; }
        public bool Busy { get; set; }

        public DateTime BookDate { get; set; }
        public DataTable Timebook { get; set; }

        // Not used ?
        public Decimal CaptHours { get; set; }
        public Decimal Dkh1Hours { get; set; }
        public Decimal Dkh2Hours { get; set; }
        public Decimal Dkh3Hours { get; set; }
        public Decimal MateHours { get; set; }
        //Audit-GS170605
        public Decimal Mate2Hours { get; set; }
        public Decimal EngrHours { get; set; }
        public Decimal TotalHours { get; set; }

        public int CrewCount { get; set; }
        public Decimal OnVessel { get; set; }
        public Decimal CrewHours { get; set; }
        public Decimal OverHours { get; set; }
        public Decimal PaidHours { get; set; }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        //private bool _dirty = false;
        //private bool _deleted = false;
        private bool _add = false;
        private bool _edit = false;

        private void make_dirty()
        {
            if (_add) Dirty = true;
            if (_edit) Dirty = true;
            if (CueRequired)
                ((CueLog)this.ParentForm).Msg();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void reset_cbx(ComboBox cbxE, ComboBox cbxS, int shift)
        {
            cbxE.SelectedIndex = -1;
            cbxE.ResetText();
            cbxS.SelectedIndex = shift;
        }


        private void set_cbx(ComboBox cbxE, ComboBox cbxS, int shift, string gang)
        {
            cbxE.BindingContext = new BindingContext();
            cbxE.DataSource = qryGang.GetView(gang, false);
            cbxE.DisplayMember = "EmpName";
            cbxE.ValueMember = "EmpID";

            //cbxS.DataSource = dacCache.GetShift();
            //cbxS.BindingContext = new BindingContext();
            //cbxS.DisplayMember = "Short";
            //cbxS.ValueMember = "NumID";
            //cbxS.SelectedIndex = shift;
            
            cbxS.DataSource = new BindingSource(qryShift.BindShifts(), null);
            cbxS.ValueMember = "Key";
            cbxS.DisplayMember = "Value";


            //cbxE.SelectedIndex = -1;
            //cbxS.SelectedIndex = -1;            
        }


        private void chg_cbx(ComboBox cbxE, ComboBox cbxS, int shift, string gang, TextBox tbxR, string resp, bool chg_resp)
        {
            //GS180803 - Don't change Emp dropdowns as bug results

            //cbxE.BindingContext = new BindingContext();
            //cbxE.DataSource = qryGang.GetView(gang, false);
            //cbxE.DisplayMember = "EmpName";
            //cbxE.ValueMember = "EmpID";

            cbxE.BackColor = Color.LightYellow;

            if (chg_resp)  tbxR.Text = resp;
        }


        private void reload()
        {
            reset_cbx(cbxE1, cbxS1, 1);
            reset_cbx(cbxE2, cbxS2, 1);
            reset_cbx(cbxE3, cbxS3, 1);
            reset_cbx(cbxE4, cbxS4, 1);

            reset_cbx(cbxE5, cbxS5, 2);
            reset_cbx(cbxE6, cbxS6, 2);
            reset_cbx(cbxE7, cbxS7, 2);
            reset_cbx(cbxE8, cbxS8, 2);

            reset_cbx(cbxEx1, cbxSx1, -1);
            reset_cbx(cbxEx2, cbxSx2, -1);
            reset_cbx(cbxEx3, cbxSx3, -1);
            reset_cbx(cbxEx4, cbxSx4, -1);
            reset_cbx(cbxEx5, cbxSx5, -1);
            reset_cbx(cbxEx6, cbxSx6, -1);
        }


        private void startup()
        {
            set_cbx(cbxE1, cbxS1, 1, "Master");
            set_cbx(cbxE2, cbxS2, 1, "Deckhand");
            set_cbx(cbxE3, cbxS3, 1, "Deckhand");
            set_cbx(cbxE4, cbxS4, 1, "All");

            set_cbx(cbxE5, cbxS5, 2, "Master");
            set_cbx(cbxE6, cbxS6, 2, "Deckhand");
            set_cbx(cbxE7, cbxS7, 2, "Deckhand");
            set_cbx(cbxE8, cbxS8, 2, "All");

            set_cbx(cbxEx1, cbxSx1, 1, "Master");
            set_cbx(cbxEx2, cbxSx2, 1, "All");
            set_cbx(cbxEx3, cbxSx3, 1, "All");
            set_cbx(cbxEx4, cbxSx4, 1, "Deckhand");
            set_cbx(cbxEx5, cbxSx5, 1, "Deckhand");
            set_cbx(cbxEx6, cbxSx6, 1, "Deckhand");

            tbxL1.Visible = false;
            tbxL2.Visible = false;
            tbxL3.Visible = false;
            tbxL4.Visible = false;
            tbxL5.Visible = false;
            tbxL6.Visible = false;
            tbxL7.Visible = false;
            tbxL8.Visible = false;

            tbxLx1.Visible = false;
            tbxLx2.Visible = false;
            tbxLx3.Visible = false;
            tbxLx4.Visible = false;
            tbxLx5.Visible = false;
            tbxLx6.Visible = false;
        }


        public ucLog()
        {
            InitializeComponent();

            CueRequired = false;
            Dirty = false;
            Busy = false;

            startup();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public void ChangeShift(bool on, int shift)
        {
            Shift = shift;
            ChangeFormat(on);
        }


        public void ChangeFormat(bool on)
        //public void ChangeShift(int shift)
        {
            //Shift = shift;

            //if (shift != -1)  // 24hour panel
            if (on)
            {
                //Shift = shift;
                Man6 = true;

                panelAM.Hide();
                panelPM.Hide();

                panelAll.Top = panelAM.Top;
                panelAll.Show();
            }
            else  // Day & Night Panel
            {
                //Shift = 1;
                Man6 = false;

                panelAM.Show();
                panelPM.Show();

                panelAll.Hide();
            }
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void vacant(TextBox tbxA, TextBox tbxP, TextBox tbxO)
        {
            tbxA.BackColor = Color.AliceBlue;
            //tbxA.ReadOnly = true;
            tbxA.ReadOnly = false;

            tbxP.BackColor = Color.AliceBlue;
            tbxO.BackColor = Color.AliceBlue;
            tbxP.ReadOnly = true;
            tbxO.ReadOnly = true;

            tbxA.Text = string.Empty;
            tbxP.Text = string.Empty;
            tbxO.Text = string.Empty;

        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void vessel_standard(TextBox tbxV, bool crew)
        {
            if (crew)
            {
                tbxV.BackColor = Color.Yellow;
                tbxV.Text = "12";
                tbxV.ReadOnly = true;
            }
        }


        private void vessel_premium(TextBox tbxV, bool crew)
        {
            if (crew)
            {
                tbxV.BackColor = Color.White;
                tbxV.Text = "";
                tbxV.ReadOnly = false;
            }
        }


        private void standard(TextBox tbxA, TextBox tbxP, TextBox tbxO)
        {
            tbxA.BackColor = Color.Yellow;
            tbxA.Text = "12";
            //tbxA.ReadOnly = true;
            tbxA.ReadOnly = false;

            tbxP.BackColor = Color.AliceBlue;
            tbxO.BackColor = Color.AliceBlue;
            tbxP.ReadOnly = false;
            tbxO.ReadOnly = false;
            //tbxP.ReadOnly = true;
            //tbxO.ReadOnly = true;

            tbxP.Text = "12";
            tbxO.Text = "";
        }


        private void premium(TextBox tbxA, TextBox tbxP, TextBox tbxO)
        {
            tbxA.BackColor = Color.White;
            //tbxA.Text = "";
            tbxA.ReadOnly = false;

            tbxP.BackColor = Color.White;
            tbxO.BackColor = Color.White;
            tbxP.ReadOnly = false;
            tbxO.ReadOnly = false;

            //tbxP.Text = "";
            //tbxO.Text = "";

            tbxA.Focus();
        }


        //private void set_captainX(TextBox tbxA,
        //    TextBox tbxP, TextBox tbxO, bool regular_hours, TextBox tbxV, bool crew)
        private void set_captain(TextBox tbxA,
            TextBox tbxP, TextBox tbxO, bool regular_hours)
        {
            if (regular_hours)
            {
                standard(tbxA, tbxP, tbxO);
                //vessel_standard(tbxV, crew);
            }
            else
            {
                premium(tbxA, tbxP, tbxO);
                //vessel_premium(tbxV, crew);
            }

            //refresh_totals();
        }


        private void partcrew(TextBox tbxVA, TextBox tbxVCa,
            TextBox tbxVMa, TextBox tbxVEn, TextBox tbxVD1, TextBox tbxVD2)
        {
            tbxVA.BackColor = Color.White;
            tbxVA.Text = "";
            tbxVA.ReadOnly = false;
            tbxVD1.BackColor = Color.White;

            tbxVCa.BackColor = Color.White;
            tbxVMa.BackColor = Color.White;
            tbxVEn.BackColor = Color.White;
            tbxVD2.BackColor = Color.White;
            tbxVCa.ReadOnly = false;
            tbxVMa.ReadOnly = false;
            tbxVEn.ReadOnly = false;
            tbxVD1.ReadOnly = false;
            tbxVD2.ReadOnly = false;

            tbxVCa.Text = ""; ;
            tbxVMa.Text = "";
            tbxVEn.Text = "";
            tbxVD1.Text = "";
            tbxVD2.Text = "";

            tbxVA.Focus();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public void ShowSheet()
        {
            tbxLx1.Show();
            tbxLx2.Show();
            tbxLx3.Show();
            tbxLx4.Show();
            tbxLx5.Show();
            tbxLx6.Show();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void change_shift(TextBox tbxI, ComboBox cbxS, int shift)
        {
            if (tbxI.Text.Equals("") || cbxS.SelectedIndex == 0) cbxS.SelectedIndex = shift;
        }


        public void NewShift(int shift)
        {
            Shift = shift;
            int night = (shift == 1 ? 2 : shift); // day is 1

            change_shift(tbxI1, cbxS1, shift);
            change_shift(tbxI2, cbxS2, shift);
            change_shift(tbxI3, cbxS3, shift);
            change_shift(tbxI4, cbxS4, shift);

            change_shift(tbxI5, cbxS5, night);
            change_shift(tbxI6, cbxS6, night);
            change_shift(tbxI7, cbxS7, night);
            change_shift(tbxI8, cbxS8, night);

            change_shift(tbxIx1, cbxSx1, shift);
            change_shift(tbxIx2, cbxSx2, shift);
            change_shift(tbxIx3, cbxSx3, shift);
            change_shift(tbxIx4, cbxSx4, shift);
            change_shift(tbxIx5, cbxSx5, shift);
            change_shift(tbxIx6, cbxSx6, shift);

            //shift = (int)nudShift.Value;
            //if (cbxCapt.Text.Equals("") || cbxCaShift.SelectedIndex == 0) cbxCaShift.SelectedIndex = shift;
            //if (cbxDh1.Text.Equals("") || cbxDH1Shift.SelectedIndex == 0) cbxDH1Shift.SelectedIndex = shift;
            //if (cbxDh2.Text.Equals("") || cbxDH2Shift.SelectedIndex == 0) cbxDH2Shift.SelectedIndex = shift;
            //if (cbxMate.Text.Equals("") || cbxMaShift.SelectedIndex == 0) cbxMaShift.SelectedIndex = shift;
            //if (cbxEng.Text.Equals("") || cbxEnShift.SelectedIndex == 0) cbxEnShift.SelectedIndex = shift;    
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void set_row(ComboBox cbxE, ComboBox cbxS, TextBox txtID, TextBox tbxA,
            TextBox tbxO, TextBox tbxP, TextBox tbxR, CheckBox chkH, DataRow row)
        {
            Decimal hour = (Decimal)row["Hours"];
            Decimal over = (Decimal)row["Over"];
            Decimal paid = hour - over;

            cbxE.Tag = null;
            cbxE.Text = (string)row["EmpName"];
            cbxE.Tag = cbxE.SelectedIndex;

            //txtID.Text = cbxE.SelectedValue.ToString();
            txtID.Text = (string)row["EmpID"];
            tbxA.Text = hour.ToString("#.#");
            tbxO.Text = over.ToString("#.#");
            //tbxP.Text = (hour >= 12M ? 12M : hour).ToString("#.#");
            tbxP.Text = (hour >= 12M ? 12M : paid).ToString("#.#");
            chkH.Checked = hour.Equals(12M);

            if (!row["Resp"].Equals(DBNull.Value)) tbxR.Text = (string)row["Resp"];

            int s = (int)row["Shift"];
            s = (s > 6 ? s - 6 : s);
            cbxS.Tag = null;
            cbxS.SelectedIndex = s;
            cbxS.Tag = s;
        }


        private void put_captain(int idx, DataRow row, int dh_idx, int idx2)
        {
            int shift = (int)row["shift"];
            //if (shift < 0) shift = 0;
    
            if (shift == 0 || shift > 3)
            {
                if (idx == 1)
                {
                    set_row(cbxEx1, cbxSx1, tbxIx1, tbxAx1, tbxOx1, tbxPx1, tbxRx1, chkHx1, row);
                }
                else
                {
                    set_row(cbxEx2, cbxSx2, tbxIx2, tbxAx2, tbxOx2, tbxPx2, tbxRx2, chkHx2, row);
                }
            }

            if (shift == 1 || shift == 3)
            {
                if (idx == 1)
                {
                    set_row(cbxE1, cbxS1, tbxI1, tbxA1, tbxO1, tbxP1, tbxR1, chkH1, row);
                    set_row(cbxEx1, cbxSx1, tbxIx1, tbxAx1, tbxOx1, tbxPx1, tbxRx1, chkHx1, row);
                }
                else
                {
                    set_row(cbxE5, cbxS5, tbxI5, tbxA5, tbxO5, tbxP5, tbxR5, chkH5, row);
                    set_row(cbxEx2, cbxSx2, tbxIx2, tbxAx2, tbxOx2, tbxPx2, tbxRx2, chkHx2, row);
                }
            }


            if (shift == 2)
            {
                if (idx == 1)
                {
                    set_row(cbxE5, cbxS5, tbxI5, tbxA5, tbxO5, tbxP5, tbxR5, chkH5, row);
                    set_row(cbxEx1, cbxSx1, tbxIx1, tbxAx1, tbxOx1, tbxPx1, tbxRx1, chkHx1, row);
                }
                else
                {
                    if (tbxI5.Text.Length == 0)
                    {
                        set_row(cbxE5, cbxS5, tbxI5, tbxA5, tbxO5, tbxP5, tbxR5, chkH5, row);
                        set_row(cbxEx1, cbxSx1, tbxIx1, tbxAx1, tbxOx1, tbxPx1, tbxRx1, chkHx1, row);
                    }
                    else
                    {
                        //MessageBox.Show("Warning (Suspicious Data) : I have 3 Skippers !", "Please reveiew data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //set_row(cbxE1, cbxS1, tbxI1, tbxA1, tbxO1, tbxP1, chkH1, row);
                        set_row(cbxEx3, cbxSx3, tbxIx3, tbxAx3, tbxOx3, tbxPx3, tbxRx3, chkHx3, row);
                    }
                }
            }

            //if (idx == 1)
            //{
            //    set_row(cbxE1, cbxS1, tbxI1, tbxA1, tbxO1, tbxP1, chkH1, row);
            //    set_row(cbxEx1, cbxSx1, tbxIx1, tbxAx1, tbxOx1, tbxPx1, chkHx1, row);
            //}
            //else if (idx == 2)
            //{
            //    set_row(cbxE5, cbxS5, tbxI5, tbxA5, tbxO5, tbxP5, chkH5, row);
            //    set_row(cbxEx2, cbxSx2, tbxIx2, tbxAx2, tbxOx2, tbxPx2, chkHx2, row);
            //}
            //else if (idx == 3)
            //{
            //    if (dh_idx < 3)
            //    {
            //        set_row(cbxE4, cbxS4, tbxI4, tbxA4, tbxP4, tbxP4, chkH4, row);
            //        set_row(cbxEx6, cbxSx6, tbxIx6, tbxAx6, tbxPx6, tbxPx6, chkHx6, row);
            //    }
            //}
        }


        private void put_mate(int idx, DataRow row, int dh_idx, int idx2)
        {
            int shift = (int)row["shift"];
            //if (shift < 0) shift = 0;

            if (shift == 0 || shift > 3)
            {
                if (idx == 1)
                    set_row(cbxEx2, cbxSx2, tbxIx2, tbxAx2, tbxOx2, tbxPx2, tbxRx2, chkHx2, row);
                else
                    set_row(cbxEx3, cbxSx3, tbxIx3, tbxAx3, tbxOx3, tbxPx3, tbxRx3, chkHx3, row);
            }

            if (shift == 1 || shift == 3)
            {
                if (idx == 1)
                    set_row(cbxEx2, cbxSx2, tbxIx2, tbxAx2, tbxOx2, tbxPx2, tbxRx2, chkHx2, row);                    
                else
                    set_row(cbxEx3, cbxSx3, tbxIx3, tbxAx3, tbxOx3, tbxPx3, tbxRx3, chkHx3, row);                    
            }

            if (shift == 2)
            {
                if (idx == 1)
                    set_row(cbxEx2, cbxSx2, tbxIx2, tbxAx2, tbxOx2, tbxPx2, tbxRx2, chkHx2, row);
                else            
                    set_row(cbxEx3, cbxSx3, tbxIx3, tbxAx3, tbxOx3, tbxPx3, tbxRx3, chkHx3, row);
            }
        }


        private void put_deckhand(int idx, DataRow row, int skip_idx, int idx2, bool index_0)
        {
            int shift = (int)row["shift"];
            if (shift < 0) shift = 0;

            if (shift == 0 || shift > 3)
            {
                if (idx == 1)
                {
                    set_row(cbxEx4, cbxSx4, tbxIx4, tbxAx4, tbxOx4, tbxPx4, tbxRx4, chkHx4, row);
                }
                else if (idx == 2)
                {
                    set_row(cbxEx5, cbxSx5, tbxIx5, tbxAx5, tbxOx5, tbxPx5, tbxRx5, chkHx5, row);
                }
                else
                {
                    set_row(cbxEx6, cbxSx6, tbxIx6, tbxAx6, tbxOx6, tbxPx6, tbxRx6, chkHx6, row);
                }
            }


            if (shift == 1 || shift == 3)
            {
                if (idx == 1)
                {
                    set_row(cbxE2, cbxS2, tbxI2, tbxA2, tbxO2, tbxP2, tbxR2, chkH2, row);
                    set_row(cbxEx4, cbxSx4, tbxIx4, tbxAx4, tbxOx4, tbxPx4, tbxRx4, chkHx4, row);
                }
                else if (idx == 2)
                {
                    set_row(cbxE3, cbxS3, tbxI3, tbxA3, tbxO3, tbxP3, tbxR3, chkH3, row);
                    set_row(cbxEx5, cbxSx5, tbxIx5, tbxAx5, tbxOx5, tbxPx5, tbxRx5, chkHx5, row);
                }
                else
                {
                    set_row(cbxE4, cbxS4, tbxI4, tbxA4, tbxP4, tbxP4, tbxR4, chkH4, row);
                    set_row(cbxEx6, cbxSx6, tbxIx6, tbxAx6, tbxPx6, tbxPx6, tbxRx6, chkHx6, row);
                }
            }

            if (shift == 2)
            {
                if (idx2 == 1)
                {
                    set_row(cbxE6, cbxS6, tbxI6, tbxA6, tbxO6, tbxP6, tbxR6, chkH6, row);
                }
                else if (idx2 == 2)
                {
                    set_row(cbxE7, cbxS7, tbxI7, tbxA7, tbxO7, tbxP7, tbxR7, chkH7, row);
                }
                else if (idx2 > 2)
                {
                    set_row(cbxE8, cbxS8, tbxI8, tbxA8, tbxP8, tbxP8, tbxR8, chkH8, row);
                }

                if (idx == 1)
                {                    
                    set_row(cbxEx4, cbxSx4, tbxIx4, tbxAx4, tbxOx4, tbxPx4, tbxRx4, chkHx4, row);
                }
                else if (idx == 2)
                {                    
                    set_row(cbxEx5, cbxSx5, tbxIx5, tbxAx5, tbxOx5, tbxPx5, tbxRx5, chkHx5, row);
                }
                else
                {
                    set_row(cbxEx6, cbxSx6, tbxIx6, tbxAx6, tbxPx6, tbxPx6, tbxRx6, chkHx6, row);
                }



            }


            //if (idx == 1)
            //{
            //    set_row(cbxE2, cbxS2, tbxI2, tbxA2, tbxO2, tbxP2, chkH2, row);
            //    set_row(cbxEx4, cbxSx4, tbxIx4, tbxAx4, tbxOx4, tbxPx4, chkHx4, row);
            //}
            //else if (idx == 2)
            //{
            //    set_row(cbxE3, cbxS3, tbxI3, tbxA3, tbxO3, tbxP3, chkH3, row);
            //    set_row(cbxEx5, cbxSx5, tbxIx5, tbxAx5, tbxOx5, tbxPx5, chkHx5, row);
            //}
            //else if (idx == 3)
            //{
            //    if (skip_idx < 3)
            //    {
            //        set_row(cbxE4, cbxS4, tbxI4, tbxA4, tbxP4, tbxP4, chkH4, row);
            //        set_row(cbxEx6, cbxSx6, tbxIx6, tbxAx6, tbxPx6, tbxPx6, chkHx6, row);
            //    }
            //    else
            //    {
            //        set_row(cbxE8, cbxS8, tbxI8, tbxA8, tbxP8, tbxP8, chkH8, row);
            //        set_row(cbxEx3, cbxSx3, tbxIx3, tbxAx3, tbxPx3, tbxPx3, chkHx3, row);
            //    }
            //}
        }



        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        //private void put_captainX(int idx, DataRow row, int dh_idx)
        //{
        //    Decimal hour = (Decimal)row["Hours"];
        //    Decimal over = (Decimal)row["Over"];

        //    if (idx == 1)
        //    {
        //        cbxCapt.Text = (string)row["EmpName"];
        //        tbxCapt.Text = cbxCapt.SelectedValue.ToString();
        //        tbxCpA.Text = hour.ToString("#.#");
        //        tbxCpO.Text = over.ToString("#.#");
        //        tbxCpP.Text = (hour >= 12M ? 12M : hour).ToString("#.#");
        //        chkCapt.Checked = hour.Equals(12M);
        //        cbxCaShift.SelectedIndex = (int)row["Shift"];
        //        //cbxCaShift.Text = (string)row["ShiftHour"];
        //        //set_captainX(tbxCpA, tbxCpP, tbxCpO, chkCapt.Checked, tbxV1A, chkCrew1.Checked);

        //        set_captain(tbxCpA, tbxCpP, tbxCpO, chkCapt.Checked);
        //        //lblDel1.Show();

        //        //CaptHours = hour;
        //    }
        //    else if (idx == 2)
        //    {
        //        cbxMate.Text = (string)row["EmpName"];
        //        tbxMate.Text = cbxMate.SelectedValue.ToString();
        //        cmdC4a.Text = "Capt";
        //        //lblMate.Text = "Capt";
        //        tbxMaA.Text = hour.ToString("#.#");
        //        tbxMaO.Text = over.ToString("#.#");
        //        tbxMaP.Text = (hour >= 12M ? 12M : hour).ToString("#.#");
        //        chkMate.Checked = hour.Equals(12M);
        //        cbxMaShift.SelectedIndex = (int)row["Shift"];
        //        //cbxMaShift.Text = (string)row["ShiftHour"];

        //        set_captain(tbxMaA, tbxMaP, tbxMaO, chkMate.Checked);
        //        //lblDel4.Show();

        //        //MateHours = hour;
        //    }
        //    else if (idx == 3)
        //    {
        //        if (dh_idx < 3)
        //        {
        //            cbxEng.Text = (string)row["EmpName"];
        //            tbxEng.Text = cbxEng.SelectedValue.ToString();
        //            cmdC5a.Text = "Capt";
        //            //lblEng.Text = "Capt";
        //            tbxEnA.Text = hour.ToString("#.#");
        //            tbxEnO.Text = over.ToString("#.#");
        //            tbxEnP.Text = (hour >= 12M ? 12M : hour).ToString("#.#");
        //            chkEng.Checked = hour.Equals(12M);
        //            cbxEnShift.SelectedIndex = (int)row["Shift"];
        //            //cbxEnShift.Text = (string)row["ShiftHour"];

        //            set_captain(tbxEnA, tbxEnP, tbxEnO, chkEng.Checked);
        //            //lblDel5.Show();

        //            //EngrHours = hour;
        //        }
        //    }
        //    //else if (idx == 4)
        //    //{
        //    //    if (dh_idx < 4)
        //    //    {
        //    //        cbxC6.Text = (string)row["EmpName"];
        //    //        tbxC6.Text = cbxC6.SelectedValue.ToString();
        //    //        cmdC6.Text = "Capt";
        //    //        //lblC6.Text = "Capt";
        //    //        tbxC6A.Text = hour.ToString("#.#");
        //    //        tbxC6O.Text = over.ToString("#.#");
        //    //        tbxC6P.Text = (hour >= 12M ? 12M : hour).ToString("#.#");
        //    //        chkC6.Checked = hour.Equals(12M);
        //    //        cbxC6Shift.SelectedIndex = (int)row["Shift"];
        //    //        //cbxEnShift.Text = (string)row["ShiftHour"];
        //    //        set_captain(tbxC6A, tbxC6P, tbxC6O, chkC6.Checked);

        //    //        Dkh3Hours = hour;
        //    //    }
        //    //}
        //}


        //private void put_deckhandX(int idx, DataRow row, int skip_idx, bool index_0)
        //{
        //    Decimal hour = (Decimal)row["Hours"];
        //    Decimal over = (Decimal)row["Over"];

        //    //if (!index_0)
        //    //{
        //    //    bool am = ((int)row["Shift"] == 1);
        //    //    if (!am idx = (idx == 3 ? 4 : idx);
        //    //    if (!am) idx = (idx == 2 ? 3 : 2);
        //    //}

        //    if (idx == 1)
        //    {
        //        cbxDh1.Text = (string)row["EmpName"];
        //        tbxDh1.Text = cbxDh1.SelectedValue.ToString();
        //        tbxD1A.Text = hour.ToString("#.#");
        //        tbxD1O.Text = over.ToString("#.#");
        //        tbxD1P.Text = (hour >= 12M ? 12M : hour).ToString("#.#");
        //        chkDH1.Checked = hour.Equals(12M);
        //        cbxDH1Shift.SelectedIndex = (int)row["Shift"];
        //        //cbxDH1Shift.Text = (string)row["ShiftHour"];

        //        set_standard(tbxD1A, tbxD1P, tbxD1O, chkDH1.Checked);
        //        //lblDel2.Show();

        //        //Dkh1Hours = hour;
        //    }
        //    else if (idx == 2)
        //    {
        //        cbxDh2.Text = (string)row["EmpName"];
        //        tbxDh2.Text = cbxDh2.SelectedValue.ToString();
        //        tbxD2A.Text = hour.ToString("#.#");
        //        tbxD2O.Text = over.ToString("#.#");
        //        tbxD2P.Text = (hour >= 12M ? 12M : hour).ToString("#.#");
        //        chkDH2.Checked = hour.Equals(12M);
        //        cbxDH2Shift.SelectedIndex = (int)row["Shift"];
        //        //cbxDH2Shift.Text = (string)row["ShiftHour"];

        //        set_standard(tbxD2A, tbxD2P, tbxD2O, chkDH2.Checked);
        //        //lblDel3.Show();

        //        //Dkh2Hours = hour;
        //    }
        //    //else if (idx == 4)
        //    //{
        //    //    if (skip_idx < 4)
        //    //    {
        //    //        cbxEng.Text = (string)row["EmpName"];
        //    //        tbxEng.Text = cbxEng.SelectedValue.ToString();
        //    //        cmdC5.Text = "DH(4)";
        //    //        //lblEng.Text = "DH(4)";
        //    //        tbxEnA.Text = hour.ToString("#.#");
        //    //        tbxEnO.Text = over.ToString("#.#");
        //    //        tbxEnP.Text = (hour >= 12M ? 12M : hour).ToString("#.#");
        //    //        chkEng.Checked = hour.Equals(12M);
        //    //        cbxEnShift.SelectedIndex = (int)row["Shift"];
        //    //        //cbxEnShift.Text = (string)row["ShiftHour"];
        //    //        set_standard(tbxEnA, tbxEnP, tbxEnO, chkEng.Checked);

        //    //        EngrHours = hour;
        //    //    }
        //    //}

        //}


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void form_vessel(string vessel, bool _edit)
        {
            //bool _readonly = false;

            //int count_skipper = 0;
            //int count_deckhand = 0;
            //int count_other = 0;
            //int count_records = 0;

            //if (vessel == null || vessel.Length == 0) return;


            //form_clear();

            //bool index_0 = scan_index_for_0(vessel);
            //Dictionary<int, int> shifts = scan_index_for_shift(vessel);
            //TabPage tp = tabShift.TabPages[0];
            //tabShift.TabPages.Clear();
            //tabShift.TabPages.Add(tp);
            //foreach (int s in shifts.Keys)
            //{
            //    if (s == 0) tabShift.TabPages[0].Text = "<N/A>";
            //    if (s == 1)
            //    {
            //        tabShift.TabPages[0].Text = "AM";
            //        if (index_0) tabShift.TabPages[0].Text = "AM & N/A";
            //    }
            //    if (s == 2) tabShift.TabPages.Add("PM");
            //    if (s == 3) tabShift.TabPages.Add("PT");
            //    if (s == 4) tabShift.TabPages.Add("24 Hour");
            //}

            //foreach (DataRow row in _dt_time.Rows)
            //{
            //    //if (row.RowState == DataRowState.Deleted) continue;
            //    if ((bool)row["DelMark"]) continue;

            //    if (vessel.Equals(row["Vessel"]))
            //    {
            //        _readonly = true;

            //        cbxShips.SelectedValue = (string)row["Vessel"];
            //        tbxSelVessel.Text = cbxShips.SelectedValue.ToString();                    

            //        count_records += 1;
            //        if (row["Duty"].Equals("SKIPPER"))
            //        {
            //            count_skipper += 1;
            //            put_captain(count_skipper, row, count_deckhand);
            //            //MessageBox.Show(string.Format("[{0}], {1}", cbxCaShift.SelectedIndex,cbxCaShift.Text));
            //        }
            //        else if (row["Duty"].Equals("DECKHAND"))
            //        {
            //            count_deckhand += 1;
            //            put_deckhand(count_deckhand, row, count_skipper, index_0);
            //        }
            //        else
            //        {
            //            count_deckhand += 1;
            //            put_deckhand(count_deckhand, row, count_skipper, index_0);
            //            count_other += 1;
            //            MessageBox.Show(string.Format("Warning (form_vessel) : Employee Duty [{0}] @ [{1}])",
            //                (string)row["Duty"], (string)row["EmpName"]));

            //        }
            //    }
            //}

            //if (count_records >= 5 || count_skipper > 2 || count_deckhand > 3 || count_other > 0)
            //    MessageBox.Show(string.Format("Error (cmdVessel_click) : Too many records @ [{0}], Skippers[{1}] Deckhands[{2}] Other[{3}]",
            //        count_records, count_skipper, (count_other > 0 ? count_deckhand - count_other : count_deckhand), count_other));

            //tbxCrewCount.Text = string.Format("[{0}]", count_records.ToString());
            //tbxV1A.Text = vessel_hours().ToString();

            //cmdNew.Show();
            ////pnlPM.Show();
        }


        private bool good_shift(int shift)
        {
            //if (Shift < 0 && shift == -1) return false;

            if (Shift < 0 && shift == 1) return true;
            if (Shift < 0 && shift == 2) return true;
            if (Shift < 0 && shift == 3) return true;

            if (Shift == shift) return true;

            return false;
        }


        private void vessel_render()
        {
            int count_records = 0;
            int count_other = 0;
            int count_skipper = 0;
            int count_deckhand = 0;
            int count_mate = 0;

            int count_shift2_skipper = 0;
            int count_shift2_deckhand = 0;
            int count_shift2_mate = 0;

            foreach (DataRow row in Timebook.Rows)
            {
                if (! row["Vessel"].ToString().Equals(Vessel)) continue;

                int shift = (int)row["Shift"];
                if (shift == -1) shift = 7;

                if (!good_shift(shift)) continue;
                //if (shift != 0 && Shift != shift) continue;
                //if (shift == 0 && Shift > 1) continue;

                bool skipper_ = ((string)row["Resp"]).Trim().Equals("Capt");
                //if (! skipper_)                
                //    skipper_ = row["Duty"].Equals("MASTER") || row["Duty"].Equals("SKIPPER");
                

                bool deckhand_ = ((string)row["Resp"]).Trim().Equals("DH(1)");
                if (! deckhand_) deckhand_ = ((string)row["Resp"]).Trim().Equals("DH(2)");
                if (! deckhand_) deckhand_ = ((string)row["Resp"]).Trim().Equals("DH(3)");
                //if (! deckhand_) deckhand_ = row["Duty"].Equals("DECKHAND");

                bool mate_ = ((string)row["Resp"]).Trim().Equals("Mate");
                if (!mate_) mate_ = row["Resp"].Equals("Mate2");
                if (!mate_) mate_ = row["Resp"].Equals("Eng.");
                

                count_records += 1;
                //if (row["Duty"].Equals("MASTER"))
                if (skipper_)
                {
                    count_skipper += 1;
                    if (shift == 2) count_shift2_skipper += 1;
                    put_captain(count_skipper, row, count_deckhand, count_shift2_skipper);
                    //MessageBox.Show(string.Format("[{0}], {1}", cbxCaShift.SelectedIndex,cbxCaShift.Text));
                }
                //else if (row["Duty"].Equals("DECKHAND"))
                else if (deckhand_)
                {
                    count_deckhand += 1;
                    if (shift == 2) count_shift2_deckhand += 1;
                    //put_deckhand(count_deckhand, row, count_skipper, index_0);
                    put_deckhand(count_deckhand, row, count_skipper, count_shift2_deckhand, false);
                }
                else if (mate_)
                {
                    count_mate += 1;
                    if (shift == 2) count_shift2_mate += 1;
                    put_mate(count_mate, row, count_deckhand, count_shift2_mate);
                }
                else
                {
                    count_deckhand += 1;
                    if (shift == 2) count_shift2_deckhand += 1;
                    //put_deckhand(count_deckhand, row, count_skipper, index_0);
                    put_deckhand(count_deckhand, row, count_skipper, count_shift2_deckhand, false);
                    count_other += 1;
                    MessageBox.Show(string.Format("Warning (vessel_render) : Employee Duty [{0}] Resp[{1}] @ [{2}])",
                        (string)row["Duty"], (string)row["Resp"], (string)row["EmpName"]),
                        "Please review the data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            //GS1707270 - VISIT HERE
            //if (count_records > 6 || count_skipper > 1 || count_deckhand > 4 || count_other > 0 || count_mate > 2)
            //    MessageBox.Show(string.Format("Warning (vessel_render) : Too many records @ [{0}], Skippers[{1}] Deckhands[{2}] Other[{3}] Mate[{4}]",
            //        count_records, count_skipper, (count_other > 0 ? count_deckhand - count_other : count_deckhand), count_other, count_mate),
            //        "Please review the data", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //tbxCrewCount.Text = string.Format("[{0}]", count_records.ToString());
            //tbxV1A.Text = vessel_hours().ToString();

            //cmdNew.Show();
            ////pnlPM.Show();

            //ChangeShift(1);
        }

        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void clear_row(ComboBox cbxE, ComboBox cbxS, TextBox txtID, TextBox tbxA,
            TextBox tbxO, TextBox tbxP, CheckBox chkH, Label lblD, TextBox tbxR, Button btn)
        {
            cbxS.Tag = null;
            cbxE.Tag = null;
            cbxS.SelectedIndex = -1;
            cbxE.SelectedIndex = -1;
            cbxE.Tag = -1;
            //cbxS.Tag = -1;

            cbxE.ResetText();

            txtID.Text = string.Empty;
            tbxA.Text = string.Empty;
            tbxO.Text = string.Empty;
            tbxP.Text = string.Empty;
            chkH.Checked = false;

            tbxA.ReadOnly = true;
            tbxO.ReadOnly = true;
            tbxP.ReadOnly = true;

            btn.Enabled = false;

            lblD.Hide();
            tbxR.ResetText();
        }


        private void edit_row(ComboBox cbxE, ComboBox cbxS, TextBox txtID, TextBox tbxA,
            TextBox tbxO, TextBox tbxP, CheckBox chkH, Label lblD, Button btn)
        {
            tbxA.ReadOnly = false;
            tbxO.ReadOnly = false;
            tbxP.ReadOnly = false;

            btn.Enabled = true;
        }



        public void ModeReady()
        {
            //reload();
            ModeClear();
        }



        public void ModeClear()
        {
            Dirty = false;
            Busy = false;
            //_deleted = false;
            _edit = false;
            _add = false;

            this.BackColor = SystemColors.Control;
            //ChangeShift(-1);]
            ChangeShift(false, -1);

            cmdC1.Text = "Capt";
            cmdC2.Text = "DH(1)";
            cmdC3.Text = "DH(2)";
            cmdC4.Text = "DH(3)";

            cmdC5.Text = "Capt";
            cmdC6.Text = "DH(1)";
            cmdC7.Text = "DH(2)";
            cmdC8.Text = "DH(3)";

            cmdCx1.Text = "Capt";
            cmdCx2.Text = "Mate";
            //cmdCx3.Text = "Eng";
            cmdCx3.Text = "Mate2";
            cmdCx4.Text = "DH(1)";
            cmdCx5.Text = "DH(2)";
            cmdCx6.Text = "DH(3)";

            clear_row(cbxE1, cbxS1, tbxI1, tbxA1, tbxO1, tbxP1, chkH1, lblD1, tbxR1, cmdC1);
            clear_row(cbxE2, cbxS2, tbxI2, tbxA2, tbxO2, tbxP2, chkH2, lblD2, tbxR2, cmdC2);
            clear_row(cbxE3, cbxS3, tbxI3, tbxA3, tbxO3, tbxP3, chkH3, lblD3, tbxR3, cmdC3);
            clear_row(cbxE4, cbxS4, tbxI4, tbxA4, tbxO4, tbxP4, chkH4, lblD4, tbxR4, cmdC4);

            clear_row(cbxE5, cbxS5, tbxI5, tbxA5, tbxO5, tbxP5, chkH5, lblD5, tbxR5, cmdC5);
            clear_row(cbxE6, cbxS6, tbxI6, tbxA6, tbxO6, tbxP6, chkH6, lblD6, tbxR6, cmdC6);
            clear_row(cbxE7, cbxS7, tbxI7, tbxA7, tbxO7, tbxP7, chkH7, lblD7, tbxR7, cmdC7);
            clear_row(cbxE8, cbxS8, tbxI8, tbxA8, tbxO8, tbxP8, chkH8, lblD8, tbxR8, cmdC8);

            clear_row(cbxEx1, cbxSx1, tbxIx1, tbxAx1, tbxOx1, tbxPx1, chkHx1, lblDx1, tbxRx1, cmdCx1);
            clear_row(cbxEx2, cbxSx2, tbxIx2, tbxAx2, tbxOx2, tbxPx2, chkHx2, lblDx2, tbxRx2, cmdCx2);
            clear_row(cbxEx3, cbxSx3, tbxIx3, tbxAx3, tbxOx3, tbxPx3, chkHx3, lblDx3, tbxRx3, cmdCx3);
            clear_row(cbxEx4, cbxSx4, tbxIx4, tbxAx4, tbxOx4, tbxPx4, chkHx4, lblDx4, tbxRx4, cmdCx4);
            clear_row(cbxEx5, cbxSx5, tbxIx5, tbxAx5, tbxOx5, tbxPx5, chkHx5, lblDx5, tbxRx5, cmdCx5);
            clear_row(cbxEx6, cbxSx6, tbxIx6, tbxAx6, tbxOx6, tbxPx6, chkHx6, lblDx6, tbxRx6, cmdCx6);

            //cmdC1a.Enabled = false;
            //cmdC2a.Enabled = false;
            //cmdC3a.Enabled = false;
            //cmdC4a.Enabled = false;
            //cmdC5a.Enabled = false;            

            //cbxCapt.SelectedIndex = -1;
            //cbxMate.SelectedIndex = -1;
            //cbxEng.SelectedIndex = -1;            
            //cbxDh1.SelectedIndex = -1;
            //cbxDh2.SelectedIndex = -1;


            //if (! _new)  cbxShips.SelectedIndex = -1;

            //tbxCapt.Text = string.Empty;
            //tbxMate.Text = string.Empty;
            //tbxEng.Text = string.Empty;            
            //tbxDh1.Text = string.Empty;
            //tbxDh2.Text = string.Empty;

            //chkCapt.Checked = false;
            //chkMate.Checked = false;
            //chkEng.Checked = false;            
            //chkDH1.Checked = false;
            //chkDH2.Checked = false;


            //vacant(tbxCpA, tbxCpP, tbxCpO);
            //vacant(tbxD1A, tbxD1P, tbxD1O);
            //vacant(tbxD2A, tbxD2P, tbxD2O);
            //vacant(tbxMaA, tbxMaP, tbxMaO);
            //vacant(tbxEnA, tbxEnP, tbxEnO);            

            //tbxCpA.ReadOnly = false;
            //tbxD1A.ReadOnly = false;
            //tbxD2A.ReadOnly = false;
            //tbxMaA.ReadOnly = false;
            //tbxEnA.ReadOnly = false;            

            //tbxCpO.ReadOnly = true;
            //tbxD1O.ReadOnly = true;
            //tbxD2O.ReadOnly = true;
            //tbxMaO.ReadOnly = true;
            //tbxEnO.ReadOnly = true;

            //tbxCpP.ReadOnly = true;
            //tbxD1P.ReadOnly = true;
            //tbxD2P.ReadOnly = true;
            //tbxMaP.ReadOnly = true;
            //tbxEnP.ReadOnly = true;

            //cbxCapt.Enabled = false;
            //cbxDh1.Enabled = false;
            //cbxDh2.Enabled = false;
            //cbxMate.Enabled = false;
            //cbxEng.Enabled = false;

            //cbxCaShift.Enabled = false;
            //cbxDH1Shift.Enabled = false;
            //cbxDH2Shift.Enabled = false;
            //cbxMaShift.Enabled = false;
            //cbxEnShift.Enabled = false;

            //chkCapt.Enabled = false;
            //chkDH1.Enabled = false;
            //chkDH2.Enabled = false;
            //chkMate.Enabled = false;
            //chkEng.Enabled = false;

            //lblDel1.Hide();
            //lblDel2.Hide();
            //lblDel3.Hide();
            //lblDel4.Hide();
            //lblDel5.Hide();            

            errorProvider1.Clear();
        }


        public void ModeRO()
        {
            _edit = false;
            //this.Enabled = false;

            vessel_render();
            vessel_totals_from_datatable();
        }



        public void ModeEdit(bool new_flag)
        {
            //this.Enabled = true;
            this.BackColor = Color.Olive;

            Dirty = false;           
            //_deleted = false;

            Busy = true;

            if (new_flag) { _add = true; make_dirty(); }
            if (!new_flag) _edit = true;

            if (new_flag) ChangeShift(true, 0);

            lblD1.Show();
            lblD2.Show();
            lblD3.Show();
            lblD4.Show();
            lblD5.Show();
            lblD6.Show();
            lblD7.Show();
            lblD8.Show();

            lblDx1.Show();
            lblDx2.Show();
            lblDx3.Show();
            lblDx4.Show();
            lblDx5.Show();
            lblDx6.Show();

            edit_row(cbxE1, cbxS1, tbxI1, tbxA1, tbxO1, tbxP1, chkH1, lblD1, cmdC1);
            edit_row(cbxE2, cbxS2, tbxI2, tbxA2, tbxO2, tbxP2, chkH2, lblD2, cmdC2);
            edit_row(cbxE3, cbxS3, tbxI3, tbxA3, tbxO3, tbxP3, chkH3, lblD3, cmdC3);
            edit_row(cbxE4, cbxS4, tbxI4, tbxA4, tbxO4, tbxP4, chkH4, lblD4, cmdC4);

            edit_row(cbxE5, cbxS5, tbxI5, tbxA5, tbxO5, tbxP5, chkH5, lblD5, cmdC5);
            edit_row(cbxE6, cbxS6, tbxI6, tbxA6, tbxO6, tbxP6, chkH6, lblD6, cmdC6);
            edit_row(cbxE7, cbxS7, tbxI7, tbxA7, tbxO7, tbxP7, chkH7, lblD7, cmdC7);
            edit_row(cbxE8, cbxS8, tbxI8, tbxA8, tbxO8, tbxP8, chkH8, lblD8, cmdC8);

            edit_row(cbxEx1, cbxSx1, tbxIx1, tbxAx1, tbxOx1, tbxPx1, chkHx1, lblDx1, cmdCx1);
            edit_row(cbxEx2, cbxSx2, tbxIx2, tbxAx2, tbxOx2, tbxPx2, chkHx2, lblDx2, cmdCx2);
            edit_row(cbxEx3, cbxSx3, tbxIx3, tbxAx3, tbxOx3, tbxPx3, chkHx3, lblDx3, cmdCx3);
            edit_row(cbxEx4, cbxSx4, tbxIx4, tbxAx4, tbxOx4, tbxPx4, chkHx4, lblDx4, cmdCx4);
            edit_row(cbxEx5, cbxSx5, tbxIx5, tbxAx5, tbxOx5, tbxPx5, chkHx5, lblDx5, cmdCx5);
            edit_row(cbxEx6, cbxSx6, tbxIx6, tbxAx6, tbxOx6, tbxPx6, chkHx6, lblDx6, cmdCx6);


            //cmdC1a.Enabled = true;
            //cmdC2a.Enabled = true;
            //cmdC3a.Enabled = true;
            //cmdC4a.Enabled = true;
            //cmdC5a.Enabled = true;            


            //if (!chkCapt.Checked)
            //{
            //    tbxCpA.ReadOnly = false;
            //    tbxCpO.ReadOnly = false;
            //    tbxCpP.ReadOnly = false;
            //}

            //if (!chkDH1.Checked)
            //{
            //    tbxD1A.ReadOnly = false;
            //    tbxD1O.ReadOnly = false;
            //    tbxD1P.ReadOnly = false;
            //}

            //if (!chkDH2.Checked)
            //{
            //    tbxD2A.ReadOnly = false;
            //    tbxD2O.ReadOnly = false;
            //    tbxD2P.ReadOnly = false;
            //}

            //if (!chkMate.Checked)
            //{
            //    tbxMaA.ReadOnly = false;
            //    tbxMaO.ReadOnly = false;
            //    tbxMaP.ReadOnly = false;
            //}

            //if (!chkEng.Checked)
            //{
            //    tbxEnA.ReadOnly = false;
            //    tbxEnO.ReadOnly = false;
            //    tbxEnP.ReadOnly = false;
            //}


            //cbxCapt.Enabled = true;
            //cbxDh1.Enabled = true;
            //cbxDh2.Enabled = true;
            //cbxMate.Enabled = true;
            //cbxEng.Enabled = true;            

            //cbxCaShift.Enabled = true;
            //cbxDH1Shift.Enabled = true;
            //cbxDH2Shift.Enabled = true;
            //cbxMaShift.Enabled = true;
            //cbxEnShift.Enabled = true;            

            //chkCapt.Enabled = true;
            //chkDH1.Enabled = true;
            //chkDH2.Enabled = true;
            //chkMate.Enabled = true;
            //chkEng.Enabled = true;


            //lblDel1.Show();
            //lblDel2.Show();
            //lblDel3.Show();
            //lblDel4.Show();
            //lblDel5.Show();            

            trans_start();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        //private void cbxCapt_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    set_standard(tbxCpA, tbxCpP, tbxCpO, chkCapt.Checked = true);

        //    //ComboBox cbx = (ComboBox)sender;

        //    //tbxV1A.ReadOnly = true;
        //    //if (cbx.SelectedIndex == 0)
        //    //{
        //    //    tbxV1A.BackColor = Color.PowderBlue;
        //    //    tbxV1A.Text = string.Empty;

        //    //    chkCapt.Enabled = false;
        //    //    chkCapt.Checked = false;

        //    //    chkCrew1.Enabled = false;
        //    //    chkCrew1.Checked = false;
        //    //}
        //    //else
        //    //{
        //    //    tbxV1A.BackColor = Color.Yellow;
        //    //    tbxV1A.Text = "12";                

        //    //    chkCapt.Enabled = true;
        //    //    chkCapt.Checked = true;

        //    //    chkCrew1.Enabled = true;
        //    //    chkCrew1.Checked = true;
        //    //}
        //}


        //private void cbxDh1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    set_standard(tbxD1A, tbxD1P, tbxD1O, chkDH1.Checked = true);

        //    //chkDH1.Enabled = true;
        //    //chkDH1.Checked = true;
        //}


        //private void cbxDh2_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    set_standard(tbxD2A, tbxD2P, tbxD2O, chkDH2.Checked = true);

        //    //chkDH2.Enabled = true;
        //    //chkDH2.Checked = true;            
        //}


        //private void cbxMate_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    set_standard(tbxMaA, tbxMaP, tbxMaO, chkMate.Checked = true);

        //    //chkMate.Enabled = true;
        //    //chkMate.Checked = true;            
        //}


        //private void cbxEng_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    set_standard(tbxEnA, tbxEnP, tbxEnO, chkEng.Checked = true);

        //    //chkEng.Enabled = true;
        //    //chkEng.Checked = true;
        //}

        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void test_unique(TextBox tbxID, ComboBox cbxShift)
        {
            int count_id = 0;

            errorProvider1.Clear();

            foreach (DataRow row in Timebook.Rows)
            {
                if (!Vessel.Equals(row["Vessel"])) continue;

                int shift = (int)row["Shift"];
                string eid = (string)row["EmpId"];
                if (tbxID.Text.Equals(eid) &&
                    cbxShift.SelectedIndex == shift )
                    count_id++;
            }

            //if (tbxI1.Text.Equals(tbxID.Text)) count_id++;
            //if (tbxI2.Text.Equals(tbxID.Text)) count_id++;
            //if (tbxI3.Text.Equals(tbxID.Text)) count_id++;
            //if (tbxI4.Text.Equals(tbxID.Text)) count_id++;
            //if (tbxI5.Text.Equals(tbxID.Text)) count_id++;
            //if (tbxI6.Text.Equals(tbxID.Text)) count_id++;
            //if (tbxI7.Text.Equals(tbxID.Text)) count_id++;
            //if (tbxI8.Text.Equals(tbxID.Text)) count_id++;

            if (count_id >= 1) errorProvider1.SetError(cbxShift, "Warning : duplicate crew member !");
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        //private void cbxEx1_SelectionChangeCommitted(object sender, EventArgs e)
        //{
        //    chkHx1.Enabled = true;
        //    chkHx1.Checked = true;


        //    tbxIx1.Text = cbxEx1.SelectedValue.ToString();
        //    test_unique(tbxIx1, cbxSx1);



        //    set_standard(tbxAx1, tbxPx1, tbxOx1, chkHx1.Checked);
        //    lblDx1.Show();

        //    vessel_totals_from_form();
        //    make_dirty();
        //}


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private int needInt32(TextBox tbx)
        {
            string val = tbx.Text;
            int number;
            bool ok = Int32.TryParse(val, out number);
            if (!ok) return 0;

            return number;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private Decimal needDecimal(TextBox tbx)
        {
            string val = tbx.Text;
            Decimal number;
            bool ok = Decimal.TryParse(val, out number);
            if (!ok) return 0M;

            return number;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        //private void total_item(decimal actual, decimal paid, decimal premium, int item)
        //{
        //    CrewHours += actual;
        //    OverHours += premium;
        //    PaidHours += paid;
        //    CrewCount += 1;

        //    if (item == 0) CaptHours += actual;
        //    if (item == 1) Dkh1Hours += actual;
        //    if (item == 2) Dkh2Hours += actual;
        //    if (item == 3) MateHours += actual;
        //    if (item == 4) EngrHours += actual;
        //    if (item == 5) Dkh3Hours += actual;

        //    //OnVessel = CaptHours + MateHours;
        //    //TotalHours = CaptHours + Dkh1Hours + Dkh2Hours + MateHours + EngrHours + Dkh3Hours;
        //}


        private void get_item(TextBox A, TextBox P, TextBox O, out decimal a, out decimal p, out decimal o)
        {
            a = needDecimal(A);
            p = needDecimal(P);
            o = needDecimal(O);
        }


        private void vessel_totals_from_form()
        {
            //    decimal actual = 0;
            //    decimal premium = 0;
            //    decimal paid = 0;

            //    CrewHours = 0;
            //    OverHours = 0;
            //    PaidHours = 0;
            //    CrewCount = 0;
            //    OnVessel = 0;

            //    CaptHours = 0;
            //    Dkh1Hours = 0;
            //    Dkh2Hours = 0;
            //    Dkh3Hours = 0;
            //    MateHours = 0;
            //    EngrHours = 0;

            //    if (tbxCapt.Text.Length > 0)
            //    {
            //        get_item(tbxCpA, tbxCpP, tbxCpO, out actual, out paid, out premium);
            //        total_item(actual, paid, premium, 0);
            //    }

            //    if (tbxDh1.Text.Length > 0)
            //    {
            //        get_item(tbxD1A, tbxD1P, tbxD1O, out actual, out paid, out premium);
            //        total_item(actual, paid, premium, 1);
            //    }

            //    if (tbxDh2.Text.Length > 0)
            //    {
            //        get_item(tbxD2A, tbxD2P, tbxD2O, out actual, out paid, out premium);
            //        total_item(actual, paid, premium, 2);
            //    }

            //    if (tbxMate.Text.Length > 0)
            //    {
            //        get_item(tbxMaA, tbxMaP, tbxMaO, out actual, out paid, out premium);
            //        total_item(actual, paid, premium, 3);
            //    }

            //    if (tbxEng.Text.Length > 0)
            //    {
            //        get_item(tbxEnA, tbxEnP, tbxEnO, out actual, out paid, out premium);
            //        total_item(actual, paid, premium, 4);
            //    }


            //    OnVessel = CaptHours + MateHours;
            //    TotalHours = CaptHours + Dkh1Hours + Dkh2Hours + MateHours + EngrHours + Dkh3Hours;
        }


        private void vessel_totals_from_datatable()
        {
            //    int crew_count = 0;

            //    Decimal crew_sum = 0M;
            //    Decimal skipper_sum = 0M;
            //    Decimal over_sum = 0M;
            //    Decimal paid_sum = 0M;

            //    Decimal capt_hour = 0M;
            //    Decimal dkh1_hour = 0M;
            //    Decimal mate_hour = 0M;
            //    Decimal dkh2_hour = 0M;
            //    Decimal dkh3_hour = 0M;
            //    Decimal engr_hour = 0M;

            //    int count_skip = 0;
            //    int count_deck = 0;

            //    foreach (DataRow row in Timebook.Rows)
            //    {
            //        if (!Vessel.Equals(row["Vessel"])) continue;

            //        crew_count += 1;

            //        Decimal hour = (Decimal)row["Hours"];
            //        Decimal over = (Decimal)row["Over"];
            //        bool skip = row["Duty"].ToString().Equals("SKIPPER");

            //        crew_sum += hour;
            //        over_sum += over;
            //        paid_sum += (hour >= 12M ? 12M : hour);
            //        if (skip) skipper_sum += hour;

            //        if (skip)
            //        {   
            //            count_skip += 1;
            //            if (count_skip == 1) capt_hour = hour;
            //            if (count_skip == 2) capt_hour += hour;
            //            if (count_skip > 2) mate_hour += hour;
            //        }
            //        else
            //        {
            //            count_deck += 1;
            //            if (count_deck == 1) dkh1_hour = hour;
            //            if (count_deck == 2) dkh2_hour = hour;
            //            if (count_deck > 2) dkh3_hour += hour;
            //        }
            //    }

            //    CrewHours = crew_sum;
            //    OverHours = over_sum;
            //    PaidHours = paid_sum;
            //    CrewCount = crew_count;
            //    OnVessel = skipper_sum;

            //    CaptHours = capt_hour;
            //    Dkh1Hours = dkh1_hour;
            //    Dkh2Hours = dkh2_hour;
            //    Dkh3Hours = dkh3_hour;
            //    MateHours = mate_hour;
            //    EngrHours = engr_hour;

            //    OnVessel = CaptHours + MateHours;
            //    TotalHours = CaptHours + Dkh1Hours + Dkh2Hours + MateHours + EngrHours + Dkh3Hours;
        }



        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        //private void chkCapt_CheckedChanged(object sender, EventArgs e)
        //{            
        //    //set_captainX(tbxCpA, tbxCpP, tbxCpO, chkCapt.Checked, tbxV1A, chkCrew1.Checked);
        //    set_captain(tbxCpA, tbxCpP, tbxCpO, chkCapt.Checked);
        //    make_dirty();
        //}


        //private void chkDH1_CheckedChanged(object sender, EventArgs e)
        //{            
        //    set_standard(tbxD1A, tbxD1P, tbxD1O, chkDH1.Checked);
        //    make_dirty();
        //}

        //private void chkDH2_CheckedChanged(object sender, EventArgs e)
        //{            
        //    set_standard(tbxD2A, tbxD2P, tbxD2O, chkDH2.Checked);
        //    make_dirty();
        //}

        //private void chkMate_CheckedChanged(object sender, EventArgs e)
        //{            
        //    set_standard(tbxMaA, tbxMaP, tbxMaO, chkMate.Checked);
        //    make_dirty();
        //}

        //private void chkEng_CheckedChanged(object sender, EventArgs e)
        //{            
        //    set_standard(tbxEnA, tbxEnP, tbxEnO, chkEng.Checked);
        //    make_dirty();
        //}


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void lblDelX(TextBox tbxI, ComboBox cbxE, ComboBox cbxS, 
            TextBox tbxA, TextBox tbxP, TextBox tbxO, Label lblD, TextBox tbxR)
        {
            cbxE.SelectedValue = "";
            
            tbxA.Text = "0";
            tbxP.Text = "0"; 
            tbxO.Text = "0";

            tbxI.Text = "";
            cbxS.Text = "";
            tbxR.ResetText();
                        
            lblD.Hide();
            
            errorProvider1.Clear();
            
            vessel_totals_from_form();
            make_dirty();
        }


        private void E_lblDelX(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;

            switch (lbl.Name)
            {
                case "lblD1": lblDelX(tbxI1, cbxE1, cbxS1, tbxA1, tbxP1, tbxO1, lblD1, tbxR1); break;
                case "lblD2": lblDelX(tbxI2, cbxE2, cbxS2, tbxA2, tbxP2, tbxO2, lblD2, tbxR2); break;
                case "lblD3": lblDelX(tbxI3, cbxE3, cbxS3, tbxA3, tbxP3, tbxO3, lblD3, tbxR3); break;
                case "lblD4": lblDelX(tbxI4, cbxE4, cbxS4, tbxA4, tbxP4, tbxO4, lblD4, tbxR4); break;
                case "lblD5": lblDelX(tbxI5, cbxE5, cbxS5, tbxA5, tbxP5, tbxO5, lblD5, tbxR5); break;
                case "lblD6": lblDelX(tbxI6, cbxE6, cbxS6, tbxA6, tbxP6, tbxO6, lblD6, tbxR6); break;
                case "lblD7": lblDelX(tbxI7, cbxE7, cbxS7, tbxA7, tbxP7, tbxO7, lblD7, tbxR7); break;
                case "lblD8": lblDelX(tbxI8, cbxE8, cbxS8, tbxA8, tbxP8, tbxO8, lblD8, tbxR8); break;
                case "lblDx1": lblDelX(tbxIx1, cbxEx1, cbxSx1, tbxAx1, tbxPx1, tbxOx1, lblDx1, tbxRx1); break;
                case "lblDx2": lblDelX(tbxIx2, cbxEx2, cbxSx2, tbxAx2, tbxPx2, tbxOx2, lblDx2, tbxRx2); break;
                case "lblDx3": lblDelX(tbxIx3, cbxEx3, cbxSx3, tbxAx3, tbxPx3, tbxOx3, lblDx3, tbxRx3); break;
                case "lblDx4": lblDelX(tbxIx4, cbxEx4, cbxSx4, tbxAx4, tbxPx4, tbxOx4, lblDx4, tbxRx4); break;
                case "lblDx5": lblDelX(tbxIx5, cbxEx5, cbxSx5, tbxAx5, tbxPx5, tbxOx5, lblDx5, tbxRx5); break;
                case "lblDx6": lblDelX(tbxIx6, cbxEx6, cbxSx6, tbxAx6, tbxPx6, tbxOx6, lblDx6, tbxRx6); break;
            }
        }
       

        //private void del(string eid)
        //{
        //    DataTable _dt_time = null;

        //    foreach (DataRow row in _dt_time.Rows)
        //    {
        //        //if (row.RowState == DataRowState.Deleted) continue;
        //        if ((bool)row["DelMark"]) continue;

        //        if (row["EmpId"].Equals(eid))
        //        {
        //            //DateTime book_date = (DateTime)row["BookDate"];
        //            //DataSet ds = dacTimebook.GetDS(book_date, 1);
        //            //dacTimebook.FindDel(new object[] { book_date, (string)row["EmpId"] });

        //            //row.Delete();
        //            row["DelMark"] = true;
        //            //_dt_time.AcceptChanges();
        //            _deleted = true;
        //            return;
        //        }
        //    }
        //}


        //private void lblDel1_Click(object sender, EventArgs e)
        //{
        //    //del(tbxCapt.Text);

        //    cbxCapt.SelectedValue = "";            

        //    //form_vessel(tbxSelVessel.Text, true);
        //    tbxCpA.Text = "0";
        //    tbxCpP.Text = "0";
        //    tbxCpO.Text = "0";

        //    tbxCapt.Text = "";

        //    //refresh_totals();
        //    lblDel1.Hide();
        //    //cmdOK.Show();

        //    errorProvider1.Clear();

        //    vessel_totals_from_form();
        //    make_dirty();
        //}



        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void A_TextChanged(TextBox tbxA, TextBox tbxP, TextBox tbxO, CheckBox chk)
        {
            decimal actual;
            decimal paid;
            decimal premium;

            decimal overtime;

            bool r = Decimal.TryParse(tbxA.Text, out actual);
            if (!r) return;

            if (!actual.Equals(12M))
                chk.Checked = false;
            else
                chk.Checked = true;

            r = Decimal.TryParse(tbxP.Text, out paid);
            if (r && actual >= paid)
            {
                r = Decimal.TryParse(tbxO.Text, out premium);
                overtime = actual - paid;

                tbxO.ForeColor = SystemColors.ControlText;
                if (premium != overtime)
                {
                    //if (!actual.Equals(12M) && paid.Equals(12M)) premium = actual - paid;
                    tbxO.ForeColor = Color.Red;
                    //premium = actual - paid;
                    //tbxO.Text = premium.ToString();
                }
            }

            vessel_totals_from_form();
            make_dirty();

        }

        //private void tbxCpA_TextChanged(object sender, EventArgs e)
        //{
        //    decimal actual;
        //    decimal paid;
        //    decimal premium;

        //    bool r = Decimal.TryParse(((TextBox)sender).Text, out actual);
        //    if (!r) return;

        //    //tbxV1Ca.Text = actual.ToString();            

        //    if (!actual.Equals(12M))
        //        chkCapt.Checked = false;
        //    else
        //        chkCapt.Checked = true;

        //    r = Decimal.TryParse(tbxCpP.Text, out paid);
        //    if (r && actual > paid)
        //    {
        //        r = Decimal.TryParse(tbxCpO.Text, out premium);

        //        if (!actual.Equals(12M) && paid.Equals(12M)) premium = actual - paid;
        //        tbxCpO.Text = premium.ToString();
        //    }

        //    vessel_totals_from_form();
        //    make_dirty();
        //}


        //private void tbxD1A_TextChanged(object sender, EventArgs e)
        //{
        //    decimal actual;
        //    decimal paid;
        //    decimal premium;

        //    bool r = Decimal.TryParse(((TextBox)sender).Text, out actual);
        //    if (!r) return;

        //    //tbxV1D1.Text = actual.ToString();

        //    if (!actual.Equals(12M))
        //        chkDH1.Checked = false;
        //    else
        //        chkDH1.Checked = true;

        //    r = Decimal.TryParse(tbxD1P.Text, out paid);
        //    if (r && actual > paid)
        //    {
        //        r = Decimal.TryParse(tbxD1O.Text, out premium);

        //        if (!actual.Equals(12M) && paid.Equals(12M)) premium = actual - paid;
        //        tbxD1O.Text = premium.ToString();
        //    }

        //    vessel_totals_from_form();
        //    make_dirty();
        //}

        //private void tbxD2A_TextChanged(object sender, EventArgs e)
        //{
        //    //tbxV1D2.Text = ((TextBox)sender).Text;

        //    decimal actual;
        //    decimal paid;
        //    decimal premium;

        //    bool r = Decimal.TryParse(((TextBox)sender).Text, out actual);
        //    if (!r) return;            

        //    if (!actual.Equals(12M))
        //        chkDH2.Checked = false;
        //    else
        //        chkDH2.Checked = true;

        //    r = Decimal.TryParse(tbxD2P.Text, out paid);
        //    if (r && actual > paid)
        //    {
        //        r = Decimal.TryParse(tbxD2O.Text, out premium);

        //        if (!actual.Equals(12M) && paid.Equals(12M)) premium = actual - paid;
        //        tbxD2O.Text = premium.ToString();
        //    }

        //    vessel_totals_from_form();
        //    make_dirty();
        //}

        //private void tbxMaA_TextChanged(object sender, EventArgs e)
        //{            
        //    //tbxV1Ma.Text = ((TextBox)sender).Text;
        //    decimal actual;
        //    decimal paid;
        //    decimal premium;

        //    bool r = Decimal.TryParse(((TextBox)sender).Text, out actual);
        //    if (!r) return;            

        //    if (!actual.Equals(12M))
        //        chkMate.Checked = false;
        //    else
        //        chkMate.Checked = true;

        //    r = Decimal.TryParse(tbxMaP.Text, out paid);
        //    if (r && actual > paid)
        //    {
        //        r = Decimal.TryParse(tbxMaO.Text, out premium);

        //        if (!actual.Equals(12M) && paid.Equals(12M)) premium = actual - paid;
        //        tbxMaO.Text = premium.ToString();
        //    }

        //    vessel_totals_from_form();
        //    make_dirty();
        //}

        //private void tbxEnA_TextChanged(object sender, EventArgs e)
        //{            
        //    //tbxV1En.Text = ((TextBox)sender).Text;

        //    decimal actual;
        //    decimal paid;
        //    decimal premium;

        //    bool r = Decimal.TryParse(((TextBox)sender).Text, out actual);
        //    if (!r) return;            

        //    if (!actual.Equals(12M))
        //        chkEng.Checked = false;
        //    else
        //        chkEng.Checked = true;

        //    r = Decimal.TryParse(tbxEnP.Text, out paid);
        //    if (r && actual > paid)
        //    {
        //        r = Decimal.TryParse(tbxEnO.Text, out premium);

        //        if (!actual.Equals(12M) && paid.Equals(12M)) premium = actual - paid;
        //        tbxEng.Text = premium.ToString();
        //    }

        //    vessel_totals_from_form();
        //    make_dirty();
        //}



        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        //private void cmdMate_to_Capt()
        //{
        //    cmdC4a.Text = "Capt";
        //    load_gang(cbxMate, "Mate");
        //}


        //private void cmdEng_to_Dh3()
        //{
        //    cmdC5a.Text = "DH(3)";
        //    load_gang(cbxEng, "DH(3)");
        //}


        //private void cmdCapt_Click(object sender, EventArgs e)
        //{
        //    cycle_role(sender);            

        //    load_gang(cbxCapt, ((Button)sender).Text);
        //    cbxCapt_SelectionChangeCommitted(null, null);
        //}

        //private void cmdMate_Click(object sender, EventArgs e)
        //{
        //    cycle_role(sender);            

        //    load_gang(cbxMate, ((Button)sender).Text);
        //    cbxMate_SelectionChangeCommitted(null, null);
        //}

        //private void cmdEng_Click(object sender, EventArgs e)
        //{
        //    cycle_role(sender);            

        //    load_gang(cbxEng, ((Button)sender).Text);
        //    cbxEng_SelectionChangeCommitted(null, null);
        //}

        //private void cmdDH1_Click(object sender, EventArgs e)
        //{
        //    cycle_role(sender);            

        //    load_gang(cbxDh1, ((Button)sender).Text);
        //    cbxDh1_SelectionChangeCommitted(null, null);
        //}

        //private void cmdDH2_Click(object sender, EventArgs e)
        //{
        //    cycle_role(sender);

        //    load_gang(cbxDh2, ((Button)sender).Text);
        //    cbxDh2_SelectionChangeCommitted(null, null);
        //}


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void cycle_role(object sender)
        {
            string role = ((Button)sender).Text;
            switch (role)
            {
                case "Capt": role = "DH(1)"; break;
                case "DH(1)": role = "DH(2)"; break;
                case "DH(2)": role = "Mate"; break;
                case "Mate": role = "Mate2"; break;
                case "Mate2": role = "Eng."; break;
                case "Eng.": role = "DH(3)"; break;
                case "DH(3)": role = "Capt"; break;
            }
            ((Button)sender).Text = role;
        }


        private void load_gang(ComboBox cbx, string button_name)
        {
            string gang_name = "Master";

            if (button_name.Equals("DH(1)")) gang_name = "Deckhand";
            if (button_name.Equals("DH(2)")) gang_name = "Deckhand";
            if (button_name.Equals("DH(3)")) gang_name = "Deckhand";
            if (button_name.Equals("DH(4)")) gang_name = "Deckhand";
            if (button_name.Equals("Mate")) gang_name = "Master";
            if (button_name.Equals("Mate2")) gang_name = "Master";
            if (button_name.Equals("Eng.")) gang_name = "Deckhand";

            DataTable dt = qryGang.GetView(gang_name, false);
            cbx.BindingContext = new BindingContext();
            cbx.DataSource = dt;
            cbx.DisplayMember = "EmpName";
            cbx.ValueMember = "EmpID";
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void tbxCpO_TextChanged(object sender, EventArgs e)
        {
            vessel_totals_from_form();
            make_dirty();

        }

        private void tbxD1O_TextChanged(object sender, EventArgs e)
        {
            vessel_totals_from_form();
            make_dirty();
        }

        private void tbxD2O_TextChanged(object sender, EventArgs e)
        {
            vessel_totals_from_form();
            make_dirty();
        }

        private void tbxMaO_TextChanged(object sender, EventArgs e)
        {
            vessel_totals_from_form();
            make_dirty();
        }

        private void tbxEnO_TextChanged(object sender, EventArgs e)
        {
            vessel_totals_from_form();
            make_dirty();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private bool no_shift_warning()
        {
            DialogResult OK;
            OK = MessageBox.Show("Warning : This row has no <Shift> selection !",
                "Data Verification", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            return (OK == DialogResult.OK);
        }


        private bool verify_row(TextBox tbxI, ComboBox cbxS)
        {
            int shift_value = cbxS.SelectedIndex;
            if (tbxI.Text.Length == 0) return true;
 
            if (shift_value < 0) return false;
            if (shift_value == 0) return no_shift_warning();

            return true;
        }



        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void row_del(DataTable dt, DateTime book_date, TextBox tbxI, ComboBox cbxS)
        {
            DataRow row;

            if (tbxI.Text.Length == 0) return;

            row = dt.NewRow();
            
            row["DelMark"] = true;

            row["BookDate"] = book_date;
            row["EmpID"] = tbxI.Text;
            row["Shift"] = cbxS.SelectedIndex;

            row["Vessel"] = Vessel;

            dt.Rows.Add(row);
        }


        private void row_add(DataTable dt, DateTime book_date,
            TextBox tbxI, ComboBox cbxN, TextBox tbxA, TextBox tbxO, ComboBox cbxS, TextBox tbxR)
        {
            DataRow row;

            if (tbxI.Text.Length == 0) return;
            
            row = dt.NewRow();

            row["DelMark"] = false;

            row["BookDate"] = book_date;
            row["EmpID"] = tbxI.Text;
            row["EmpName"] = cbxN.Text;
            //    //row["ToffCode"] = null;
            row["Hours"] = needDecimal(tbxA);
            
            row["Over"] = needDecimal(tbxO);
            //    //row["LogVessel"] = cbxShips.SelectedValue;
            row["Vessel"] = Vessel;
            row["Shift"] = cbxS.SelectedIndex;
            row["ShiftHour"] = cbxS.SelectedValue;

            row["Resp"] = tbxR.Text;

            //    //row["LogNote"] = null;

            dt.Rows.Add(row);
        }


        //private void row_captain(DataTable dt, DateTime book_date, bool del)
        //{
        //    DataRow row;

        //    if (tbxCapt.Text.Length == 0) return;
        //    //if (cbxCapt.SelectedIndex < 0) return;

        //    row = dt.NewRow();

        //    row["DelMark"] = del;

        //    row["BookDate"] = book_date;
        //    row["EmpID"] = tbxCapt.Text;
        //    row["EmpName"] = cbxCapt.Text;
        //    //row["ToffCode"] = null;
        //    row["Hours"] = needDecimal(tbxCpA);
        //    row["Over"] = needDecimal(tbxCpO);
        //    //row["LogVessel"] = cbxShips.SelectedValue;
        //    row["Vessel"] = Vessel;
        //    row["Shift"] = cbxCaShift.SelectedIndex;
        //    row["ShiftHour"] = cbxCaShift.SelectedValue;
        //    //row["LogNote"] = null;

        //    dt.Rows.Add(row);

        //    //dacTimebook.FindAdd(new object[] { book_date, row["EmpID"] }, row);
        //}


        //private void row_deckhand1(DataTable dt, DateTime book_date, bool del)
        //{
        //    DataRow row;

        //    if (tbxDh1.Text.Length == 0) return;
        //    //if (cbxDh1.SelectedIndex < 0) return;

        //    row = dt.NewRow();

        //    row["DelMark"] = del;

        //    row["BookDate"] = book_date;
        //    row["EmpID"] = cbxDh1.SelectedValue;
        //    row["EmpName"] = cbxDh1.Text;
        //    //row["ToffCode"] = null;
        //    row["Hours"] = needDecimal(tbxD1A);
        //    row["Over"] = needDecimal(tbxD1O);
        //    //row["LogVessel"] = cbxShips.SelectedValue;
        //    row["Vessel"] = Vessel;
        //    row["Shift"] = cbxDH1Shift.SelectedIndex;
        //    row["ShiftHour"] = cbxDH1Shift.SelectedValue;

        //    //row["LogNote"] = null;

        //    dt.Rows.Add(row);
        //    //dacTimebook.FindAdd(new object[] { book_date, row["EmpID"] }, row);
        //}


        //private void row_deckhand2(DataTable dt, DateTime book_date, bool del)
        //{
        //    DataRow row;

        //    if (tbxDh2.Text.Length == 0) return;
        //    //if (cbxDh2.SelectedIndex < 0) return;

        //    row = dt.NewRow();

        //    row["DelMark"] = del;

        //    row["BookDate"] = book_date;
        //    row["EmpID"] = cbxDh2.SelectedValue;
        //    row["EmpName"] = cbxDh2.Text;
        //    //row["ToffCode"] = null;
        //    row["Hours"] = needDecimal(tbxD2A);
        //    row["Over"] = needDecimal(tbxD2O);
        //    //row["LogVessel"] = cbxShips.SelectedValue;
        //    row["Vessel"] = Vessel;
        //    row["Shift"] = cbxDH2Shift.SelectedIndex;
        //    row["ShiftHour"] = cbxDH2Shift.SelectedValue;
        //    //row["LogNote"] = null;

        //    dt.Rows.Add(row);
        //    //dacTimebook.FindAdd(new object[] { book_date, row["EmpID"] }, row);
        //}


        //private void row_mate(DataTable dt, DateTime book_date, bool del)
        //{
        //    DataRow row;

        //    if (tbxMate.Text.Length == 0) return;
        //    //if (cbxMate.SelectedIndex < 0) return;

        //    row = dt.NewRow();

        //    row["DelMark"] = del;

        //    row["BookDate"] = book_date;
        //    row["EmpID"] = cbxMate.SelectedValue;
        //    row["EmpName"] = cbxMate.Text;
        //    //row["ToffCode"] = null;
        //    row["Hours"] = needDecimal(tbxMaA);
        //    row["Over"] = needDecimal(tbxMaO);
        //    //row["LogVessel"] = cbxShips.SelectedValue;
        //    row["Vessel"] = Vessel;
        //    row["Shift"] = cbxMaShift.SelectedIndex;
        //    row["ShiftHour"] = cbxMaShift.SelectedValue;
        //    //row["LogNote"] = null;

        //    dt.Rows.Add(row);
        //    //dacTimebook.FindAdd(new object[] { book_date, row["EmpID"] }, row);
        //}


        //private void row_engineer(DataTable dt, DateTime book_date, bool del)
        //{
        //    DataRow row;

        //    if (tbxEng.Text.Length == 0) return;
        //    //if (cbxEng.SelectedIndex < 0) return;

        //    row = dt.NewRow();

        //    row["DelMark"] = del;

        //    row["BookDate"] = book_date;
        //    row["EmpID"] = cbxEng.SelectedValue;
        //    row["EmpName"] = cbxEng.Text;
        //    //row["ToffCode"] = null;
        //    row["Hours"] = needDecimal(tbxEnA);
        //    row["Over"] = needDecimal(tbxEnO);
        //    //row["LogVessel"] = cbxShips.SelectedValue;
        //    row["Vessel"] = Vessel;
        //    row["Shift"] = cbxEnShift.SelectedIndex;
        //    row["ShiftHour"] = cbxEnShift.SelectedValue;
        //    //row["LogNote"] = null;

        //    dt.Rows.Add(row);
        //    //dacTimebook.FindAdd(new object[] { book_date, row["EmpID"] }, row);
        //}


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void trans_start()
        {
            if (Shift == -1)
            {
                row_del(Timebook, BookDate, tbxI1, cbxS1);
                row_del(Timebook, BookDate, tbxI2, cbxS2);
                row_del(Timebook, BookDate, tbxI3, cbxS3);
                row_del(Timebook, BookDate, tbxI4, cbxS4);
                row_del(Timebook, BookDate, tbxI5, cbxS5);
                row_del(Timebook, BookDate, tbxI6, cbxS6);
                row_del(Timebook, BookDate, tbxI7, cbxS7);
                row_del(Timebook, BookDate, tbxI8, cbxS8);
            }
            else
            {
                row_del(Timebook, BookDate, tbxIx1, cbxSx1);
                row_del(Timebook, BookDate, tbxIx2, cbxSx2);
                row_del(Timebook, BookDate, tbxIx3, cbxSx3);
                row_del(Timebook, BookDate, tbxIx4, cbxSx4);
                row_del(Timebook, BookDate, tbxIx5, cbxSx5);
                row_del(Timebook, BookDate, tbxIx6, cbxSx6);
            }

            //row_del(Timebook, BookDate, tbxIx1);
            //row_del(Timebook, BookDate, tbxIx2);
            //row_del(Timebook, BookDate, tbxIx3);
            //row_del(Timebook, BookDate, tbxIx4);
            //row_del(Timebook, BookDate, tbxIx5);
            //row_del(Timebook, BookDate, tbxIx6);
            

            //row_captain(Timebook, BookDate, true);
            //row_deckhand1(Timebook, BookDate, true);
            //row_deckhand2(Timebook, BookDate, true);
            //row_mate(Timebook, BookDate, true);
            //row_engineer(Timebook, BookDate, true);            
        }

        private void trans_finish()
        {
            //if (Shift == -1)
            if (!Man6)
            {
                row_add(Timebook, BookDate, tbxI1, cbxE1, tbxA1, tbxO1, cbxS1, tbxR1);
                row_add(Timebook, BookDate, tbxI2, cbxE2, tbxA2, tbxO2, cbxS2, tbxR2);
                row_add(Timebook, BookDate, tbxI3, cbxE3, tbxA3, tbxO3, cbxS3, tbxR3);
                row_add(Timebook, BookDate, tbxI4, cbxE4, tbxA4, tbxO4, cbxS4, tbxR4);
                row_add(Timebook, BookDate, tbxI5, cbxE5, tbxA5, tbxO5, cbxS5, tbxR5);
                row_add(Timebook, BookDate, tbxI6, cbxE6, tbxA6, tbxO6, cbxS6, tbxR6);
                row_add(Timebook, BookDate, tbxI7, cbxE7, tbxA7, tbxO7, cbxS7, tbxR7);
                row_add(Timebook, BookDate, tbxI8, cbxE8, tbxA8, tbxO8, cbxS8, tbxR8);
            }
            else
            {
                row_add(Timebook, BookDate, tbxIx1, cbxEx1, tbxAx1, tbxOx1, cbxSx1, tbxRx1);
                row_add(Timebook, BookDate, tbxIx2, cbxEx2, tbxAx2, tbxOx2, cbxSx2, tbxRx2);
                row_add(Timebook, BookDate, tbxIx3, cbxEx3, tbxAx3, tbxOx3, cbxSx3, tbxRx3);
                row_add(Timebook, BookDate, tbxIx4, cbxEx4, tbxAx4, tbxOx4, cbxSx4, tbxRx4);
                row_add(Timebook, BookDate, tbxIx5, cbxEx5, tbxAx5, tbxOx5, cbxSx5, tbxRx5);
                row_add(Timebook, BookDate, tbxIx6, cbxEx6, tbxAx6, tbxOx6, cbxSx6, tbxRx6);
            }
            

            //row_captain(Timebook, BookDate, false);
            //row_deckhand1(Timebook, BookDate, false);
            //row_deckhand2(Timebook, BookDate, false);
            //row_mate(Timebook, BookDate, false);
            //row_engineer(Timebook, BookDate, false);            
        }


        private void trans_abort()
        {
            Timebook.RejectChanges();
        }


        public bool ModeCancel()
        {
            trans_abort();
            return true;
        }


        public bool Save_Delete()
        {
            if (!verify_row(tbxI1, cbxS1)) return false;
            if (!verify_row(tbxI2, cbxS2)) return false;
            if (!verify_row(tbxI3, cbxS3)) return false;
            if (!verify_row(tbxI4, cbxS4)) return false;

            if (!verify_row(tbxI5, cbxS5)) return false;
            if (!verify_row(tbxI6, cbxS6)) return false;
            if (!verify_row(tbxI7, cbxS7)) return false;
            if (!verify_row(tbxI8, cbxS8)) return false;

            if (!verify_row(tbxIx1, cbxSx1)) return false;
            if (!verify_row(tbxIx2, cbxSx2)) return false;
            if (!verify_row(tbxIx3, cbxSx3)) return false;
            if (!verify_row(tbxIx4, cbxSx4)) return false;
            if (!verify_row(tbxIx5, cbxSx5)) return false;
            if (!verify_row(tbxIx6, cbxSx6)) return false;

            trans_finish();
            return true;

            //if (tbxDh1.Text.Length > 0) MessageBox.Show(tbxDh1.Text);
            //if (tbxDh2.Text.Length > 0) MessageBox.Show(tbxDh2.Text);
            //if (tbxMate.Text.Length > 0) MessageBox.Show(tbxMate.Text);
            //if (tbxEng.Text.Length > 0) MessageBox.Show(tbxEng.Text);
            //if (tbxC6.Text.Length > 0) MessageBox.Show(tbxC6.Text);


        }


        //private void do_save_delete()
        //{
        //    DataTable _dt_time = null;

        //    DataSet ds = null;

        //    if (_deleted)
        //    {
        //        foreach (DataRow row in _dt_time.Rows)
        //        {
        //            if (ds == null) ds = dacTimebook.GetDS((DateTime)row["BookDate"], 1);
        //            if ((bool)row["DelMark"])
        //                dacTimebook.FindDel(new object[] { row["BookDate"], row["EmpId"] });
        //        }

        //        dacTimebook.DeleteData();
        //        dacCache.PutTimebook();
        //        _deleted = false;
        //    }

        //    if (Dirty)
        //    {
        //        //DateTime book_date = dtpLogDate.Value.Date;
        //        DateTime book_date = DateTime.Now;

        //        if (ds == null) ds = dacTimebook.GetDS(book_date, 1);

        //        //row_captain(ds, book_date);
        //        //row_deckhand1(ds, book_date);
        //        //row_deckhand2(ds, book_date);
        //        //row_mate(ds, book_date);
        //        //row_engineer(ds, book_date);
        //        //row_C6(ds, book_date);

        //        dacTimebook.SaveData();
        //        dacCache.PutTimebook();

        //        Dirty = false;
        //    }

        //    MessageBox.Show("Saved !");
        //}


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void old_standard(TextBox tbxA, TextBox tbxP, TextBox tbxO, bool regular_hours)
        {
            if (regular_hours)
                standard(tbxA, tbxP, tbxO);
            else
                premium(tbxA, tbxP, tbxO);

            //refresh_totals();

            tbxA.Focus();
        }


        private void set_standard(ComboBox cbxE, TextBox tbxI, Label lblD,
            TextBox tbxA, TextBox tbxP, TextBox tbxO, ComboBox cbxS, Button cmdC, TextBox tbxR, bool regular_hours)
        {
            //    chkHx1.Enabled = true;
            //    chkHx1.Checked = true;

            if (cbxE.SelectedIndex < 0) { cbxE.Focus(); return; }
            //if (cbxS.Text.Length == 0) cbxS.SelectedIndex = 0;

            tbxI.Text = cbxE.SelectedValue.ToString();
            tbxR.Text = cmdC.Text;

            test_unique(tbxI, cbxS);
            
            lblD.Show();

            //    vessel_totals_from_form();
            

            if (regular_hours)
                standard(tbxA, tbxP, tbxO);
            else
                premium(tbxA, tbxP, tbxO);

            //refresh_totals();
            make_dirty();

            tbxA.Focus();
        }


        ///*
        // * Shift Select 
        // */

 

        private void E_SCI_cbxS(object sender, EventArgs e)
        {
            ComboBox cbx = (ComboBox)sender;

            if (cbx.Tag != null) cbx.SelectedIndex = (int)cbx.Tag;
        }


        private void E_SCI_cbxE(object sender, EventArgs e)
        {
            ComboBox cbx = (ComboBox)sender;

            if (cbx.Tag != null) cbx.SelectedIndex = (int)cbx.Tag;
        }


        private void E_SCC_cbxS(object sender, EventArgs e)
        {
            ComboBox cbx = (ComboBox)sender;
            if (_edit || _add)
            {
                cbx.Tag = null;

                errorProvider1.Clear();
                //vessel_totals_from_form();
                //make_dirty();         

                switch (cbx.Name)
                {
                    case "cbxS1": set_standard(cbxE1, tbxI1, lblD1, tbxA1, tbxP1, tbxO1, cbxS1, cmdC1, tbxR1, chkH1.Checked = true); break;
                    case "cbxS2": set_standard(cbxE2, tbxI2, lblD2, tbxA2, tbxP2, tbxO2, cbxS2, cmdC2, tbxR2, chkH2.Checked = true); break;
                    case "cbxS3": set_standard(cbxE3, tbxI3, lblD3, tbxA3, tbxP3, tbxO3, cbxS3, cmdC3, tbxR3, chkH3.Checked = true); break;
                    case "cbxS4": set_standard(cbxE4, tbxI4, lblD4, tbxA4, tbxP4, tbxO4, cbxS4, cmdC4, tbxR4, chkH4.Checked = true); break;
                    case "cbxS5": set_standard(cbxE5, tbxI5, lblD5, tbxA5, tbxP5, tbxO5, cbxS5, cmdC5, tbxR5, chkH5.Checked = true); break;
                    case "cbxS6": set_standard(cbxE6, tbxI6, lblD6, tbxA6, tbxP6, tbxO6, cbxS6, cmdC6, tbxR6, chkH6.Checked = true); break;
                    case "cbxS7": set_standard(cbxE7, tbxI7, lblD7, tbxA7, tbxP7, tbxO7, cbxS7, cmdC7, tbxR7, chkH7.Checked = true); break;
                    case "cbxS8": set_standard(cbxE8, tbxI8, lblD8, tbxA8, tbxP8, tbxO8, cbxS8, cmdC8, tbxR8, chkH8.Checked = true); break;
                    case "cbxSx1": set_standard(cbxEx1, tbxIx1, lblDx1, tbxAx1, tbxPx1, tbxOx1, cbxSx1, cmdCx1, tbxRx1, chkHx1.Checked = true); break;
                    case "cbxSx2": set_standard(cbxEx2, tbxIx2, lblDx2, tbxAx2, tbxPx2, tbxOx2, cbxSx2, cmdCx2, tbxRx2, chkHx2.Checked = true); break;
                    case "cbxSx3": set_standard(cbxEx3, tbxIx3, lblDx3, tbxAx3, tbxPx3, tbxOx3, cbxSx3, cmdCx3, tbxRx3, chkHx3.Checked = true); break;
                    case "cbxSx4": set_standard(cbxEx4, tbxIx4, lblDx4, tbxAx4, tbxPx4, tbxOx4, cbxSx4, cmdCx4, tbxRx4, chkHx4.Checked = true); break;
                    case "cbxSx5": set_standard(cbxEx5, tbxIx5, lblDx5, tbxAx5, tbxPx5, tbxOx5, cbxSx5, cmdCx5, tbxRx5, chkHx5.Checked = true); break;
                    case "cbxSx6": set_standard(cbxEx6, tbxIx6, lblDx6, tbxAx6, tbxPx6, tbxOx6, cbxSx6, cmdCx6, tbxRx6, chkHx6.Checked = true); break;
                }
 
            }
        }


        private void E__SCC_cbxE(object sender, EventArgs e)
        {
            ComboBox cbx = (ComboBox)sender;
            if (_edit || _add)
            {
                cbx.Tag = null;
                errorProvider1.Clear();
                //make_dirty();

                switch (cbx.Name)
                {
                    case "cbxE1": set_standard(cbxE1, tbxI1, lblD1, tbxA1, tbxP1, tbxO1, cbxS1, cmdC1, tbxR1, chkH1.Checked = true); break;
                    case "cbxE2": set_standard(cbxE2, tbxI2, lblD2, tbxA2, tbxP2, tbxO2, cbxS2, cmdC2, tbxR2, chkH2.Checked = true); break;
                    case "cbxE3": set_standard(cbxE3, tbxI3, lblD3, tbxA3, tbxP3, tbxO3, cbxS3, cmdC3, tbxR3, chkH3.Checked = true); break;
                    case "cbxE4": set_standard(cbxE4, tbxI4, lblD4, tbxA4, tbxP4, tbxO4, cbxS4, cmdC4, tbxR4, chkH4.Checked = true); break;
                    case "cbxE5": set_standard(cbxE5, tbxI5, lblD5, tbxA5, tbxP5, tbxO5, cbxS5, cmdC5, tbxR5, chkH5.Checked = true); break;
                    case "cbxE6": set_standard(cbxE6, tbxI6, lblD6, tbxA6, tbxP6, tbxO6, cbxS6, cmdC6, tbxR6, chkH6.Checked = true); break;
                    case "cbxE7": set_standard(cbxE7, tbxI7, lblD7, tbxA7, tbxP7, tbxO7, cbxS7, cmdC7, tbxR7, chkH7.Checked = true); break;
                    case "cbxE8": set_standard(cbxE8, tbxI8, lblD8, tbxA8, tbxP8, tbxO8, cbxS8, cmdC8, tbxR8, chkH8.Checked = true); break;
                    case "cbxEx1": set_standard(cbxEx1, tbxIx1, lblDx1, tbxAx1, tbxPx1, tbxOx1, cbxSx1, cmdCx1, tbxRx1, chkHx1.Checked = true); break;
                    case "cbxEx2": set_standard(cbxEx2, tbxIx2, lblDx2, tbxAx2, tbxPx2, tbxOx2, cbxSx2, cmdCx2, tbxRx2, chkHx2.Checked = true); break;
                    case "cbxEx3": set_standard(cbxEx3, tbxIx3, lblDx3, tbxAx3, tbxPx3, tbxOx3, cbxSx3, cmdCx3, tbxRx3, chkHx3.Checked = true); break;
                    case "cbxEx4": set_standard(cbxEx4, tbxIx4, lblDx4, tbxAx4, tbxPx4, tbxOx4, cbxSx4, cmdCx4, tbxRx4, chkHx4.Checked = true); break;
                    case "cbxEx5": set_standard(cbxEx5, tbxIx5, lblDx5, tbxAx5, tbxPx5, tbxOx5, cbxSx5, cmdCx5, tbxRx5, chkHx5.Checked = true); break;
                    case "cbxEx6": set_standard(cbxEx6, tbxIx6, lblDx6, tbxAx6, tbxPx6, tbxOx6, cbxSx6, cmdCx6, tbxRx6, chkHx6.Checked = true); break;
                }
            }
        }

        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void tbxA1_TextChanged(object sender, EventArgs e)
        {
            TextBox tbx = (TextBox)sender;

            switch (tbx.Name)
            {
                case "tbxA1": A_TextChanged(tbxA1, tbxP1, tbxO1, chkH1); break;
                case "tbxA2": A_TextChanged(tbxA2, tbxP2, tbxO2, chkH2); break;
                case "tbxA3": A_TextChanged(tbxA3, tbxP3, tbxO3, chkH3); break;
                case "tbxA4": A_TextChanged(tbxA4, tbxP4, tbxO4, chkH4); break;
                case "tbxA5": A_TextChanged(tbxA5, tbxP5, tbxO5, chkH5); break;
                case "tbxA6": A_TextChanged(tbxA6, tbxP6, tbxO6, chkH6); break;
                case "tbxA7": A_TextChanged(tbxA7, tbxP7, tbxO7, chkH7); break;
                case "tbxA8": A_TextChanged(tbxA8, tbxP8, tbxO8, chkH8); break;
                case "tbxAx1": A_TextChanged(tbxAx1, tbxPx1, tbxOx1, chkHx1); break;
                case "tbxAx2": A_TextChanged(tbxAx2, tbxPx2, tbxOx2, chkHx2); break;
                case "tbxAx3": A_TextChanged(tbxAx3, tbxPx3, tbxOx3, chkHx3); break;
                case "tbxAx4": A_TextChanged(tbxAx4, tbxPx4, tbxOx4, chkHx4); break;
                case "tbxAx5": A_TextChanged(tbxAx5, tbxPx5, tbxOx5, chkHx5); break;
                case "tbxAx6": A_TextChanged(tbxAx6, tbxPx6, tbxOx6, chkHx6); break;
            }
        }


        private void tbxP1_TextChanged(object sender, EventArgs e)
        {
            TextBox tbx = (TextBox)sender;

            switch (tbx.Name)
            {
                case "tbxP1": A_TextChanged(tbxA1, tbxP1, tbxO1, chkH1); break;
                case "tbxP2": A_TextChanged(tbxA2, tbxP2, tbxO2, chkH2); break;
                case "tbxP3": A_TextChanged(tbxA3, tbxP3, tbxO3, chkH3); break;
                case "tbxP4": A_TextChanged(tbxA4, tbxP4, tbxO4, chkH4); break;
                case "tbxP5": A_TextChanged(tbxA5, tbxP5, tbxO5, chkH5); break;
                case "tbxP6": A_TextChanged(tbxA6, tbxP6, tbxO6, chkH6); break;
                case "tbxP7": A_TextChanged(tbxA7, tbxP7, tbxO7, chkH7); break;
                case "tbxP8": A_TextChanged(tbxA8, tbxP8, tbxO8, chkH8); break;
                case "tbxPx1": A_TextChanged(tbxAx1, tbxPx1, tbxOx1, chkHx1); break;
                case "tbxPx2": A_TextChanged(tbxAx2, tbxPx2, tbxOx2, chkHx2); break;
                case "tbxPx3": A_TextChanged(tbxAx3, tbxPx3, tbxOx3, chkHx3); break;
                case "tbxPx4": A_TextChanged(tbxAx4, tbxPx4, tbxOx4, chkHx4); break;
                case "tbxPx5": A_TextChanged(tbxAx5, tbxPx5, tbxOx5, chkHx5); break;
                case "tbxPx6": A_TextChanged(tbxAx6, tbxPx6, tbxOx6, chkHx6); break;
            }
        }

        private void tbxO1_TextChanged(object sender, EventArgs e)
        {
            TextBox tbx = (TextBox)sender;

            switch (tbx.Name)
            {
                case "tbxO1": A_TextChanged(tbxA1, tbxP1, tbxO1, chkH1); break;
                case "tbxO2": A_TextChanged(tbxA2, tbxP2, tbxO2, chkH2); break;
                case "tbxO3": A_TextChanged(tbxA3, tbxP3, tbxO3, chkH3); break;
                case "tbxO4": A_TextChanged(tbxA4, tbxP4, tbxO4, chkH4); break;
                case "tbxO5": A_TextChanged(tbxA5, tbxP5, tbxO5, chkH5); break;
                case "tbxO6": A_TextChanged(tbxA6, tbxP6, tbxO6, chkH6); break;
                case "tbxO7": A_TextChanged(tbxA7, tbxP7, tbxO7, chkH7); break;
                case "tbxO8": A_TextChanged(tbxA8, tbxP8, tbxO8, chkH8); break;
                case "tbxOx1": A_TextChanged(tbxAx1, tbxPx1, tbxOx1, chkHx1); break;
                case "tbxOx2": A_TextChanged(tbxAx2, tbxPx2, tbxOx2, chkHx2); break;
                case "tbxOx3": A_TextChanged(tbxAx3, tbxPx3, tbxOx3, chkHx3); break;
                case "tbxOx4": A_TextChanged(tbxAx4, tbxPx4, tbxOx4, chkHx4); break;
                case "tbxOx5": A_TextChanged(tbxAx5, tbxPx5, tbxOx5, chkHx5); break;
                case "tbxOx6": A_TextChanged(tbxAx6, tbxPx6, tbxOx6, chkHx6); break;
            }
        }


        private string chg_resp(Button cmd)
        {
            switch (cmd.Text)
            {
                case "Capt": cmd.Text = "DH(1)"; break;
                case "DH(1)": cmd.Text = "DH(2)"; break;
                case "DH(2)": cmd.Text = "DH(3)"; break;
                case "DH(3)": cmd.Text = "Mate"; break;
                case "Mate": cmd.Text = "Mate2"; break;
                case "Mate2": cmd.Text = "Eng."; break;
                case "Eng.": cmd.Text = "Capt"; break;
            }

            return cmd.Text;
        }


        private void cmdC1_Click(object sender, EventArgs e)
        {
            Button cmd = (Button)sender;

            string new_resp = chg_resp(cmd);

            chg_cbx(cbxE1, cbxS1, 1, "All", tbxR1, new_resp, cmd.Name.Equals("cmdC1"));
            chg_cbx(cbxE2, cbxS2, 1, "All", tbxR2, new_resp, cmd.Name.Equals("cmdC2"));
            chg_cbx(cbxE3, cbxS3, 1, "All", tbxR3, new_resp, cmd.Name.Equals("cmdC3"));
            chg_cbx(cbxE4, cbxS4, 1, "All", tbxR4, new_resp, cmd.Name.Equals("cmdC4"));

            chg_cbx(cbxE5, cbxS5, 2, "All", tbxR5, new_resp, cmd.Name.Equals("cmdC5"));
            chg_cbx(cbxE6, cbxS6, 2, "All", tbxR6, new_resp, cmd.Name.Equals("cmdC6"));
            chg_cbx(cbxE7, cbxS7, 2, "All", tbxR7, new_resp, cmd.Name.Equals("cmdC7"));
            chg_cbx(cbxE8, cbxS8, 2, "All", tbxR8, new_resp, cmd.Name.Equals("cmdC8"));

            chg_cbx(cbxEx1, cbxSx1, 1, "All", tbxRx1, new_resp, cmd.Name.Equals("cmdCx1"));
            chg_cbx(cbxEx2, cbxSx2, 1, "All", tbxRx2, new_resp, cmd.Name.Equals("cmdCx2"));
            chg_cbx(cbxEx3, cbxSx3, 1, "All", tbxRx3, new_resp, cmd.Name.Equals("cmdCx3"));
            chg_cbx(cbxEx4, cbxSx4, 1, "All", tbxRx4, new_resp, cmd.Name.Equals("cmdCx4"));
            chg_cbx(cbxEx5, cbxSx5, 1, "All", tbxRx5, new_resp, cmd.Name.Equals("cmdCx5"));
            chg_cbx(cbxEx6, cbxSx6, 1, "All", tbxRx6, new_resp, cmd.Name.Equals("cmdCx6"));

            make_dirty();
        }

  


    }
}



/*******************************************************************************************************************\
 *                                                                                                                 *
\*******************************************************************************************************************/

//private void tbxCpP_TextChanged(object sender, EventArgs e)
//{
//    vessel_totals_from_form();
//    make_dirty();
//}

//private void tbxD1P_TextChanged(object sender, EventArgs e)
//{
//    vessel_totals_from_form();
//    make_dirty();
//}

//private void tbxD2P_TextChanged(object sender, EventArgs e)
//{
//    vessel_totals_from_form();
//    make_dirty();
//}

//private void tbxMaP_TextChanged(object sender, EventArgs e)
//{
//    vessel_totals_from_form();
//    make_dirty();
//}

//private void tbxEnP_TextChanged(object sender, EventArgs e)
//{
//    vessel_totals_from_form();
//    make_dirty();
//}

//private void tbxC6P_TextChanged(object sender, EventArgs e)
//{
//    vessel_totals_from_form();
//    make_dirty();
//}

//private void tbxCpO_TextChanged_1(object sender, EventArgs e)
//{
//    vessel_totals_from_form();
//    make_dirty();
//}

//private void tbxD1O_TextChanged_1(object sender, EventArgs e)
//{
//    vessel_totals_from_form();
//    make_dirty();
//}

//private void tbxD2O_TextChanged_1(object sender, EventArgs e)
//{
//    vessel_totals_from_form();
//    make_dirty();
//}

//private void tbxMaO_TextChanged_1(object sender, EventArgs e)
//{
//    vessel_totals_from_form();
//    make_dirty();
//}

//private void tbxEnO_TextChanged_1(object sender, EventArgs e)
//{
//    vessel_totals_from_form();
//    make_dirty();
//}

//private void tbxC6O_TextChanged(object sender, EventArgs e)
//{
//    vessel_totals_from_form();
//    make_dirty();
//}



//private void cbxCaShift_SelectionChangeCommitted(object sender, EventArgs e)
//{
//    errorProvider1.Clear();
//    vessel_totals_from_form();
//    make_dirty();
//}

//private void cbxDH1Shift_SelectedIndexChanged(object sender, EventArgs e)
//{
//    errorProvider1.Clear();
//    vessel_totals_from_form();
//    make_dirty();
//}

//private void cbxDH2Shift_SelectedIndexChanged(object sender, EventArgs e)
//{
//    errorProvider1.Clear();
//    vessel_totals_from_form();
//    make_dirty();
//}

//private void cbxMaShift_SelectedIndexChanged(object sender, EventArgs e)
//{
//    errorProvider1.Clear();
//    vessel_totals_from_form();
//    make_dirty();
//}

//private void cbxEnShift_SelectedIndexChanged(object sender, EventArgs e)
//{
//    errorProvider1.Clear();
//    vessel_totals_from_form();
//    make_dirty();
//}

//private void cbxC6Shift_SelectedIndexChanged(object sender, EventArgs e)
//{
//    errorProvider1.Clear();
//    vessel_totals_from_form();
//    make_dirty();
//}
