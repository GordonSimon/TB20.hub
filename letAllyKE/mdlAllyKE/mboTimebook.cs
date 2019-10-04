using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;


namespace mdlAllyKE
{
    public class mboTimebook
    {
        public mboTimebook()
        {
        }


        static public void Load(DateTime ref_week)
        {
            dacCache.RefreshTimebook(ref_week, false);
            //qryGang.Requery();
            qryTimebook.Requery();
        }


        static public DataTable GetTimebook(DateTime day)
        {
            DataTable dt = qryTimebook.GetView("Vessel", day);
            return dt;
        }


        static public void SetTimebook(DataTable dt, DateTime book_date, string vessel)
        {
            DataSet ds;
            DataRow ds_row;

            //if (ds == null) ds = dacTimebook.GetDS(book_date, 1);
            ds = dacTimebook.GetDS(book_date, 1);  // get one day

            DataTable dtChg = dt.GetChanges();
            if (dtChg == null) return;

            foreach (DataRow row in dtChg.Rows)
                if (row["Vessel"].ToString().Equals(vessel))
                {
                        bool del = (bool)row["DelMark"];
                        string shift = row["Shift"].ToString();
                        string emp_id = row["EmpID"].ToString();
                        string emp_name = row["EmpName"].ToString();

                        if (del)
                            dacTimebook.FindDel(new object[] { row["BookDate"], row["EmpId"], row["Shift"] });

                        //System.Windows.Forms.MessageBox.Show(emp_id +
                        //    ", " + emp_name + ", DEL[" + del.ToString() + "]" + "shift=[" + shift + "]");
                }

            dacTimebook.DeleteData();           

            //dtChg = dt.GetChanges();
            foreach (DataRow row in dtChg.Rows)
            {
                if (row["Vessel"].ToString().Equals(vessel))
                {
                    bool del = (bool)row["DelMark"];
            
                    string shift = row["Shift"].ToString();
                    string emp_id = row["EmpID"].ToString();
                    string emp_name = row["EmpName"].ToString();
                    string resp = row["Resp"].ToString();

                    if (! del)
                    {
                        ds_row = ds.Tables[0].NewRow();
                        ds_row["BookDate"] = book_date;
                        ds_row["EmpID"] = emp_id;
                        ds_row["EmpName"] = emp_name;
                        ds_row["ToffCode"] = null;
                        ds_row["LogHours"] = row["Hours"];
                        ds_row["LogOver"] = row["Over"];
                        ds_row["LogVessel"] = row["Vessel"];
                        ds_row["LogShift"] = shift;
                        ds_row["LogHour"] = "0000";
                        ds_row["Resp"] = resp;

                        //if (!lblLog.Text.Equals("<log sheet>")) row["LogSheet"] = lblLog.Text;
                        //if (!lblShift.Text.Equals("<log shift>")) row["LogShift"] = lblShift.Text;
                        //if (!lblHourStart.Text.Equals("<engine hours>")) row["LogEngineStart"] = lblHourStart.Text;
                        //if (!lblHourFinish.Text.Equals("<engine hours>")) row["LogEngineFinish"] = lblHourFinish.Text;
                        //if (!lblFuel.Text.Equals("<fuel>")) row["LogFuel"] = lblFuel.Text;

                        ds_row["LogNote"] = null;
                        //DataRow r = dacTimebook.FindSet(new object[] { book_date, emp_id, shift }, ds_row);
                        //if (r == null)
                            dacTimebook.FindAdd(new object[] { book_date, emp_id, shift }, ds_row);

                        //System.Windows.Forms.MessageBox.Show(emp_id +
                        //    ", " + emp_name + ", " + "shift=[" + shift + "]");
                    }
                }

                //dacTimebook.SaveData();
            }

            dacTimebook.SaveData();

            //dacTimebook.DeleteData();
            //dacTimebook.SaveData();
            //dacTimebook.SaveAll();
            //dacCache.PutTimebook();

        }
    }
}
