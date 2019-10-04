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
    public partial class ucList : UserControl
    {
        public delegate void ChangeHandler();

        public DataSet _ds { get; set; }

        public event ChangeHandler PostChange;

        public void SetGangName(string name) { tbxGangName.Text = name; }


        private int _org_w { get; set; }
        private int _org_h { get; set; }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public ucList()
        {
            InitializeComponent();

            _org_w = this.Size.Width;
            _org_h = this.Size.Height;

            clbItems.CheckOnClick = true;
        }


        private void load_dataset()
        {
            //DataSet ds = dacGang.GetDS();
            //DataTable dt = dacCache.GetGang();

            DataTable dt = qryGang.GetDT();

            //_ds = ds;
            //_ds = new DataSet();
            //_ds.Tables.Add(dt);

            if (dt == null) return;

            foreach (DataRow row in dt.Rows)
            {
                CheckListBoxItem ni = new CheckListBoxItem();
                ni.Tag = row["EmpId"];
                ni.Text = row["EmpName"].ToString();

                lbxItems.Items.Add(ni);

                clbItems.Items.Add(ni,
                    (((bool)row["Active"]) ? CheckState.Checked : CheckState.Unchecked));
            }
        }


        private void ucList_Load(object sender, EventArgs e)
        {
            load_dataset();
        }


        public void Reload()
        {
            lbxItems.Items.Clear();
            clbItems.Items.Clear();
            load_dataset();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void ucList_SizeChanged(object sender, EventArgs e)
        {            
            //MessageBox.Show(this.Size.ToString(), "list");

            int w = this.Size.Width;
            int h = this.Size.Height;


            //int bw = cmdDel.Size.Width;
            //int bh = cmdDel.Size.Height;

            //int blt = 4;
            //int bll = w - bw - 14;

            //int ilt = blt + bh + 8;
            //int ill = 4;

            //int iw = w - 2 - 2 - 10;
            //int ih = h - blt - bh - 2;

            //cmdDel.Location = new Point(bll, blt);
            //cmdAdd.Location = new Point(bll - bw - 4, blt);
            //cmdEdit.Location = new Point(bll - bw - 4 - bw - 14, blt);

            //clbItems.Location = new Point(ill, ilt);            
            //clbItems.Size = new Size(iw, ih);

            //int item_w = clbItems.Size.Width;
            //int item_h = clbItems.Size.Height;
            //clbItems.Size = new Size(item_w, h - 50);
            //clbItems.Size.ToString();
            
        }


        private void clbItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = clbItems.SelectedIndex;
            if (idx == -1) return;

            bool state = clbItems.GetItemChecked(idx);

            CheckListBoxItem bi = (CheckListBoxItem)(clbItems.SelectedItem);
            string emp_id = bi.Tag.ToString();

            //DataRow row = _ds.Tables[0].Rows.Find(emp_id);
            DataRow row = dacCache.GetGang().Rows.Find(emp_id);
            row.BeginEdit();
            row["Active"] = state;
            row.EndEdit();

            if (PostChange != null) PostChange();
        }


        private void clbItems_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //if (e.NewValue != CheckState.Checked) return;

            //bool state = clbItems.GetItemChecked(e.Index);

            //CheckListBoxItem bi = (CheckListBoxItem)(clbItems.SelectedItem);
            //string emp_id = bi.Tag.ToString();

            //DataRow row = _ds.Tables[0].Rows.Find(emp_id);
            //row.BeginEdit();
            //row["Active"] = state;
            //row.EndEdit();

            //if (PostChange != null) PostChange();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            frmGang frm = new frmGang();

            frm.ShowDialog();

            //clbItems.Items.Clear();
            lbxItems.Items.Clear();
            load_dataset();
            
            //Form frm = new Form();
            //frm.Size = new Size(660, 510);

            //ucNewEmp uc_emp = new ucNewEmp();
            //uc_emp.AddEmp();
            //frm.Controls.Add(uc_emp);
            //frm.ShowDialog();

            //DateTime tod = DateTime.Now;

            //List<string> emp = (List<string>)(frm.Tag);
            ////clbItems.Items.Add(emp[1] + ", " + emp[0], CheckState.Checked);

            //if (emp == null) return;

            //DataRow row = _ds.Tables[0].NewRow();
            //row["EmpId"] = emp[0];
            //row["EmpName"] = string.Format("{0}, {1}", emp[2], emp[1]);
            //row["HomePhone"] = emp[3];
            //row["CellPhone"] = emp[4];

            //row["Active"] = true;
            //row["CreateDate"] = tod;
            //row["UpdateDate"] = tod;
            //row["UserAudit"] = "<system>";
            //_ds.Tables[0].Rows.Add(row);

            //CheckListBoxItem ni = new CheckListBoxItem();
            //ni.Tag = row["EmpId"];
            //ni.Text = row["EmpName"].ToString();

            //clbItems.Items.Add(ni,
            //    (((bool)row["Active"]) ? CheckState.Checked : CheckState.Unchecked));


            if (PostChange != null) PostChange();
        }


        private void cmdEdit_Click(object sender, EventArgs e)
        {
            //CheckListBoxItem bi = (CheckListBoxItem)(clbItems.SelectedItem);

            CheckListBoxItem bi = (CheckListBoxItem)(lbxItems.SelectedItem);
            if (bi == null) return;

            string emp_id = bi.Tag.ToString();

            Form frm = new Form();
            frm.Size = new Size(660, 510);

            ucNewEmp uc_emp = new ucNewEmp();
            uc_emp.EditEmp(emp_id);
            frm.Controls.Add(uc_emp);
            frm.ShowDialog();

        }


        private void cmdDel_Click(object sender, EventArgs e)
        {
            //CheckListBoxItem bi = (CheckListBoxItem)(clbItems.SelectedItem);
            CheckListBoxItem bi = (CheckListBoxItem)(lbxItems.SelectedItem);
            if (bi == null) return;
            
            string emp_id = bi.Tag.ToString();
            

            //DataRow row = _ds.Tables[0].Rows.Find(emp_id);
            DataRow row = dacCache.GetGang().Rows.Find(emp_id);
            row.Delete();
            dacCache.GetGang().AcceptChanges();
            
            //dacGang.SaveData(_ds.GetChanges());

            //clbItems.Items.Clear();
            lbxItems.Items.Clear();
            load_dataset();

            if (PostChange != null) PostChange();
        }

        private void cmdEmployee_Click(object sender, EventArgs e)
        {
            frmEmployee frm = new frmEmployee();

            frm.ShowDialog();

        }
    }


    /*******************************************************************************************************************\
     *                                                                                                                 *
    \*******************************************************************************************************************/

    class CheckListBoxItem
    {
        public object Tag;
        public string Text;
        public override string ToString() { return Text; }
    }
}
