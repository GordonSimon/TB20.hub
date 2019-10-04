using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace letEmp_KF
{
    public partial class ucPaydirt : UserControl
    {
        public ucPaydirt()
        {
            InitializeComponent();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void show_paydirt(string emp_id, string last, string first, string status, string freq,
            string phone, string addr1, string addr2, string city, string prov, string postal, string country,
            string hire, string start, string end)
        {
            tbxFirst.Text = first;
            tbxLast.Text = last;
            tbxTab.Text = emp_id;
            tbxStatus.Text = status;

            tbxPhone.Text = phone;
            tbxAddr1.Text = addr1;
            tbxAddr2.Text = addr2;
            tbxCity.Text = city;
            tbxProv.Text = prov;
            tbxPostal.Text = postal;
            tbxCountry.Text = country;

            tbxFreq.Text = freq;

            tbxHire.Text = hire;
            tbxStart.Text = start;
            tbxEnd.Text = end;
        }


        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        private string get_str(DataRow row, string colname)
        {
            return row[colname].Equals(DBNull.Value) ? "" : (string)row[colname];
        }


        public void LoadRow(DataRow row)
        {
            try
            {
                string emp_id = (string)row["strEmployee_number"];                
                string last = (string)row["strEmployee_Lastname"];

                string first = "";
                if (row["strEmployee_Middlename"].Equals(DBNull.Value))
                    first = (string)row["strEmployee_Firstname"];
                else
                    first = string.Format("{0} {1}", (string)row["strEmployee_Firstname"], (string)row["strEmployee_Middlename"]);

                string status = get_str(row, "strEmployee_Status");
                string freq = get_str(row, "strEmployee_Frequency");

                string phone = get_str(row, "strEmployee_Telephone");
                string addr1 = get_str(row, "strEmployee_Address1");
                string addr2 = get_str(row, "strEmployee_Address2");
                string city = (string)row["strEmployee_City"];
                string prov = (string)row["strEmployee_Province"];
                string postal = (string)row["strEmployee_Postal"];
                string country = (string)row["strEmployee_Country"];

                string hire = ((DateTime)row["dtmEmployee_OriginalHire"]).ToShortDateString();
                string start = "";
                string end = "";

                if (!row["dtmEmployee_Start"].Equals(DBNull.Value))
                    start = ((DateTime)row["dtmEmployee_Start"]).ToShortDateString();

                if (!row["dtmEmployee_End"].Equals(DBNull.Value))
                    end = ((DateTime)row["dtmEmployee_End"]).ToShortDateString();

                show_paydirt(emp_id, last, first, status, freq,
                    phone, addr1, addr2, city, prov, postal, country, hire, start, end);
            }
            catch (Exception ex)
            {
                errDash.Error("Error (ucPaydirt.LoadRow) : " + ex.Message);
            }


            //sql += " strEmployee_OrderName";
            //sql += ", strEmployee_Status";
            //sql += ", strEmployee_Telephone";
            //sql += ", strEmployee_number";
            //sql += ", dtmEmployee_OriginalHire";
            //sql += ", strEmployee_Address1";
            //sql += ", strEmployee_Address2";
            //sql += ", strEmployee_City";
            //sql += ", strEmployee_Province";
            //sql += ", strEmployee_Postal";
            //sql += ", strEmployee_Country";
            //sql += ", dtmEmployee_Start";
            //sql += ", dtmEmployee_End";
            //sql += ", strEmployee_Frequency";
            //sql += ", strEmployee_Lastname";
            //sql += ", strEmployee_Firstname";
            //sql += ", strEmployee_Middlename";
            //sql += " FROM " + _tname;
        }

    }
}
