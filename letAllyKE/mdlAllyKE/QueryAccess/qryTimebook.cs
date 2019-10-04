using System;
using System.Collections.Generic;

using System.Data;

using System.Windows.Forms;

using System.Linq;
using System.ComponentModel;


/// <summary>
/// Summary description for qryTimebook
/// </summary>

namespace mdlAllyKE
{
    public class qryTimebook
    {
        static private string _qry_timebook = "default";

        static private DataTable _dt_timebook = null;

        static private bool _requery = false;

        static public DataTable GetDT() { return _dt_timebook; }
        static public void Requery() { _requery = true; }


        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        static private DataTable build_tb_hours(DateTime day)
        {
            DataTable dtTime = dacCache.GetTimebook();
            try
            {
                var q = from tTime in dtTime.AsEnumerable()
                        where ((DateTime)tTime["Bookdate"]).Date.Equals(day.Date)
                        && !tTime["LogHours"].Equals(DBNull.Value)
                        && !((Decimal)tTime["LogHours"]).Equals(0.0M)
                        select new
                        {
                            EmpId = (string)tTime["EmpId"],
                            Bookdate = (DateTime)tTime["Bookdate"],
                            Hours = (Decimal)tTime["LogHours"],
                            Vessel = (tTime["LogVessel"].Equals(DBNull.Value) ? "" : (string)tTime["LogVessel"])
                        };

                //DataTable dt = ToDataTable(q.ToList());
                return libAlly.TO_DATA_TABLE(q.ToList());

                //    DataTable dtTime = dacCache.GetTimebook().DefaultView.ToTable(true, new string[] { "EmpId", "EmpName" });

                //    var q = from tTime in dtTime.AsEnumerable()
                //            join tEmp in dtEmp.AsEnumerable()
                //            on (string)tTime["EmpID"] equals (string)tEmp["EmpID"]
                //            orderby tEmp["EmpID"]
                //            //where ! tCrew["LogVessel"].Equals(DBNull.Value) && tCrew["LogVessel"].Equals(vessel)
                //            select new Gang
                //            {
                //                EmpID = (string)tEmp["EmpID"],
                //                //EmpName = (string)tCrew["EmpName"],
                //                EmpName = string.Format("{0}, {1}", (string)tEmp["Last Name"], (string)tEmp["First Name"]),
                //                HomePhone = (tEmp["Main Phone"].Equals(DBNull.Value) ? "" : (string)tEmp["Main Phone"]),
                //                CellPhone = (tEmp["Cell Phone"].Equals(DBNull.Value) ? "" : (string)tEmp["Cell Phone"]),
                //                Active = true,
                //                Duty = (string)tEmp["Duty"],
                //                Master = (tEmp["Master"].Equals(DBNull.Value) ? false : (bool)tEmp["Master"]),
                //                Employment = (tEmp["Employment"].Equals(DBNull.Value) ? "Other" : (string)tEmp["Employment"])
                //            };

                //    //DataTable dt = q.CopyToDataTable();
                //    DataTable dt = ToDataTable(q.ToList());

                //    _dt_timebook = dt;
                //    dacCache.SetGang(dt);

                //    DataColumn pk1 = dt.Columns[0]; pk1.MaxLength = 8; pk1.AllowDBNull = false;
                //    dt.PrimaryKey = new DataColumn[] { pk1 };
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error (build_tb_hours) : {0}", ex.Message));
            }

            return null;
        }


        static private DataTable build_tb_vessel(DateTime day)
        {
            DataTable dtEmp = dacCache.GetEmployee();
            DataTable dtTime = dacCache.GetTimebook();


            try
            {
                var q = from tTime in dtTime.AsEnumerable()
                        join tEmp in dtEmp.AsEnumerable()
                        on (string)tTime["EmpID"] equals (string)tEmp["EmpID"]
                        where ((DateTime)tTime["Bookdate"]).Date.Equals(day.Date)
                        && !tTime["LogVessel"].Equals(DBNull.Value)
                        && !((string)tTime["LogVessel"]).Equals(string.Empty)
                        && !((string)tTime["LogVessel"]).Equals(new string(' ', 16))
                        orderby tTime["LogVessel"], tTime["LogShift"]
                        select new
                        {
                            EmpId = (string)tTime["EmpId"],
                            EmpName = string.Format("{0}, {1}", ((string)tEmp["Last Name"]).ToUpper(), (string)tEmp["First Name"]),
                            Bookdate = (DateTime)tTime["Bookdate"],
                            Hours = (Decimal)tTime["LogHours"],
                            Over = (Decimal)tTime["LogOver"],
                            Vessel = (tTime["LogVessel"].Equals(DBNull.Value) ? "" : (string)tTime["LogVessel"]),
                            Shift = (tTime["LogShift"].Equals(DBNull.Value) ? -1 : (int)tTime["LogShift"]),
                            ShiftHour = (tTime["LogHour"].Equals(DBNull.Value) ? "<>" : (string)tTime["LogHour"]),
                            Toff = (tTime["ToffCode"].Equals(DBNull.Value) ? "" : (string)tTime["ToffCode"]),
                            Duty = ((string)tEmp["Duty"]).ToUpper(),
                            Resp = (tTime["Resp"].Equals(DBNull.Value) ? "" : (string)tTime["Resp"]),
                            DelMark = (bool)false,
                            Note = (tTime["LogNote"].Equals(DBNull.Value) ? "" : (string)tTime["LogNote"]),
                        };

                return libAlly.TO_DATA_TABLE(q.ToList());

                //DataTable dt = ToDataTable(q.ToList());
                //dt.PrimaryKey = new DataColumn[] { dt.Columns["EmpId"], dt.Columns["Bookdate"], dt.Columns["Vessel"] };


                //    DataTable dtTime = dacCache.GetTimebook().DefaultView.ToTable(true, new string[] { "EmpId", "EmpName" });

                //    var q = from tTime in dtTime.AsEnumerable()
                //            join tEmp in dtEmp.AsEnumerable()
                //            on (string)tTime["EmpID"] equals (string)tEmp["EmpID"]
                //            orderby tEmp["EmpID"]
                //            //where ! tCrew["LogVessel"].Equals(DBNull.Value) && tCrew["LogVessel"].Equals(vessel)
                //            select new Gang
                //            {
                //                EmpID = (string)tEmp["EmpID"],
                //                //EmpName = (string)tCrew["EmpName"],
                //                EmpName = string.Format("{0}, {1}", (string)tEmp["Last Name"], (string)tEmp["First Name"]),
                //                HomePhone = (tEmp["Main Phone"].Equals(DBNull.Value) ? "" : (string)tEmp["Main Phone"]),
                //                CellPhone = (tEmp["Cell Phone"].Equals(DBNull.Value) ? "" : (string)tEmp["Cell Phone"]),
                //                Active = true,
                //                Duty = (string)tEmp["Duty"],
                //                Master = (tEmp["Master"].Equals(DBNull.Value) ? false : (bool)tEmp["Master"]),
                //                Employment = (tEmp["Employment"].Equals(DBNull.Value) ? "Other" : (string)tEmp["Employment"])
                //            };

                //    //DataTable dt = q.CopyToDataTable();
                //    DataTable dt = ToDataTable(q.ToList());

                //_dt_timebook = dt;

                //    dacCache.SetGang(dt);

                //    DataColumn pk1 = dt.Columns[0]; pk1.MaxLength = 8; pk1.AllowDBNull = false;
                //    dt.PrimaryKey = new DataColumn[] { pk1 };
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error (build_tb_vessel) : {0}", ex.Message));
            }

            return null;
        }


        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        //static public DataTable GetView(string qry_name, DateTime start_week, int days)
        static public DataTable GetView(string qry_name, DateTime day)
        {
            if (_requery)
            {
                if (_dt_timebook != null) _dt_timebook.Dispose();
                _requery = false;
                _dt_timebook = null;
            }

            // unless _requery is forced, check if query is current
            if (_qry_timebook.Equals(qry_name) && _dt_timebook != null) return _dt_timebook;


            //int days_in_month = DateTime.DaysInMonth(ref_date.Year, ref_date.Month);
            //var first_day_in_month = new DateTime(ref_date.Year, ref_date.Month, 1);
            //DataTable dtTime = dacTimebook.GetDT(first_day_in_month, days_in_month);

            dacCache.RefreshTimebook(day, true);

            _qry_timebook = qry_name;

            if (qry_name.Equals("Hours"))  _dt_timebook = build_tb_hours(day); 
            if (qry_name.Equals("Vessel"))  _dt_timebook = build_tb_vessel(day);

            _dt_timebook.AcceptChanges();
            

            return _dt_timebook;
        }
    }
}
