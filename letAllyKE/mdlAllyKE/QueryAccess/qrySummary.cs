using System;
using System.Collections.Generic;

using System.Data;

using System.Windows.Forms;

using System.Linq;
using System.ComponentModel;


/// <summary>
/// Summary description for qrySummary
/// </summary>

namespace mdlAllyKE
{
    public class qrySummary
    {
        static private string _qry_summary = "default";

        static private DataTable _dt_timebook = null;

        static private bool _requery = false;

        static public DataTable GetDT() { return _dt_timebook; }
        static public void Requery() { _requery = true; }


        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        static private void add_row(DataTable rdt, int row_count,
            decimal hour, decimal over, string toff, string vessel, string emp_id, string emp_name, string note, DateTime bookdate)
        {
            string top = (hour == decimal.Zero ? string.Empty : string.Format("{0:0.0}", hour));
            string mid = (over == decimal.Zero ? string.Empty : string.Format("{0:0.0}", over));
            string bot = string.Format("{0}", vessel);

            if (row_count == 2 && toff.Equals("12"))
                bot = string.Format("{0}", vessel);
            else
            {
                if (row_count > 2)
                    bot = string.Format("{0} [{1}]", vessel, row_count);
                else
                    bot = string.Format("{0}", vessel);
            }

            //if (hour == decimal.Zero && over == decimal.Zero && toff.Length == 0 && vessel.Length == 0) bot = "*";

            if (hour == decimal.Zero)
            {
                if (toff.Equals("12"))
                    top = "!12";
                else
                    top = toff;
            }
            else
            {
                if (toff.Length != 0 && ! toff.Equals("12")) 
                    //top = string.Format("{0:0.0} <{1}>", hour, toff);
                    top = string.Format("{0:0.0}", hour);
            }


            DataRow row = rdt.NewRow();
            row["EmpId"] = emp_id;
            row["EmpName"] = emp_name;
            row["Top"] = top;
            row["Mid"] = mid;
            row["Bot"] = bot;
            row["Note"] = note;
            row["Bookdate"] = bookdate;            
            rdt.Rows.Add(row);
        }


        static private DataTable build_timebook(DateTime start_date, int days, DataTable dt_gang)
        {
            DataTable rdt = new DataTable("Summary");
            rdt.Clear();
            rdt.Columns.Add("EmpId");
            rdt.Columns.Add("EmpName");
            rdt.Columns.Add("Bookdate", typeof(DateTime));
            rdt.Columns.Add("Top");
            rdt.Columns.Add("Mid");
            rdt.Columns.Add("Bot");
            rdt.Columns.Add("Note");

            foreach (DataRow row in dt_gang.Rows)
            {
                bool check = (bool)(row["Active"]);
                if (!check) continue;

                string emp_id = (string)row["EmpId"];

                DataTable dt = build_import(start_date, days, emp_id);
                //_dt_timebook = dt;

                string emp_name = string.Empty;
                DateTime bookdate = DateTime.MinValue;
                decimal hour = 0;
                decimal over = 0;
                string toff = string.Empty;
                string vessel = string.Empty;
                string note = string.Empty;
                int row_count = 0;

                foreach (DataRow trw in dt.Rows)
                {
                    if (row_count != 0 && bookdate.CompareTo(trw["Bookdate"]) != 0)
                    {
                        //int idx = bookdate.Day + 1;

                        add_row(rdt, row_count, hour, over, toff, vessel, emp_id, emp_name, note, bookdate);

                        row_count = 0;
                    }

                    if (row_count == 0)
                    {
                        bookdate = (DateTime)trw["Bookdate"];
                        hour = 0;
                        over = 0;
                        toff = string.Empty;
                        vessel = string.Empty;
                        note = string.Empty;

                        emp_name = (string)trw["EmpName"];

                        //row_count = 0;
                    }

                    row_count += 1;
                    hour += Convert.ToDecimal(trw["LogHours"]);
                    over += Convert.ToDecimal(trw["LogOver"]);

                    if (!trw["ToffCode"].Equals(DBNull.Value))
                    {
                        if (toff.Length == 0)
                            toff = (string)trw["ToffCode"];
                        else
                        {
                            if (((string)trw["ToffCode"]).Length != 0) toff += "/" + (string)trw["ToffCode"];
                        }
                    }

                    if (!trw["LogVessel"].Equals(DBNull.Value))
                    {
                        if (((string)trw["LogVessel"]).Length != 0)
                        {
                            if ( trw["ToffCode"].Equals(DBNull.Value) || ((string)trw["ToffCode"]).Length == 0)
                            {
                                if (vessel.Length == 0)
                                    vessel = (string)trw["LogVessel"];
                                else
                                    vessel += "/" + (string)trw["LogVessel"];
                            }
                        }
                    }

                    if (!trw["LogNote"].Equals(DBNull.Value))
                    {
                        if (((string)trw["LogNote"]).Length != 0)
                        {
                            if (note.Length == 0)
                                note = (string)trw["LogNote"];
                            else
                                note += "/" + (string)trw["LogNote"];
                        }
                    }


                }

                if (row_count > 0)  // catch the edge
                {
                    //int idx = bookdate.Day + 1;
                    add_row(rdt, row_count, hour, over, toff, vessel, emp_id, emp_name, note, bookdate);
                }
            }
            //_details.Add(monthRef.Month.ToString(), detail);                    

            return rdt;
        }


        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        static private void add_employee(DataTable rdt, int row_count, 
            decimal hour, decimal over, string toff, string vessel, string emp_id, string emp_name, string note,
            DateTime bookdate)
        {
            string top = (hour == decimal.Zero ? string.Empty : string.Format("{0:0.0}", hour));
            string mid = (over == decimal.Zero ? string.Empty : string.Format("{0:0.0}", over));
            string bot = string.Format("{0}", vessel);


            if (row_count > 1) bot = string.Format("{0} [{1}]", vessel, row_count);
            if (hour == decimal.Zero && over == decimal.Zero && toff.Length == 0 && vessel.Length == 0) bot = "*";

            if (hour == decimal.Zero)
            {
                if (toff.Equals("12"))
                    top = "!12";
                else
                    top = toff;
            }
            else
            {
                if (toff.Length != 0) top = string.Format("{0:0.0} <{1}>", hour, toff);
            }

            
            DataRow row = rdt.NewRow();
            row["EmpId"] = emp_id;
            row["EmpName"] = emp_name;
            row["Top"] = top;
            row["Mid"] = mid;
            row["Bot"] = bot;
            row["Note"] = note;
            row["Bookdate"] = bookdate;
            rdt.Rows.Add(row);
        }

        
        static private DataTable build_employee(DateTime start_date, int days, string emp_id)
        {

            //DataTable tmp = dacCache.GetTimebook();
            //tmp.DefaultView.RowFilter = string.Format("Empid='{0}'", emp_id);
            //DataView dv = tmp.DefaultView;

            DataTable rdt = new DataTable("Employee");
            rdt.Clear();
            rdt.Columns.Add("EmpId");
            rdt.Columns.Add("EmpName");
            rdt.Columns.Add("Bookdate", typeof(DateTime));
            rdt.Columns.Add("Top");
            rdt.Columns.Add("Mid");
            rdt.Columns.Add("Bot");
            rdt.Columns.Add("Note");

            DataTable dt = build_import(start_date, days, emp_id);
            //_dt_timebook = dt;

            string emp_name = string.Empty;
            DateTime bookdate = DateTime.MinValue;
            decimal hour = 0;
            decimal over = 0;
            string toff = string.Empty;
            string vessel = string.Empty;
            string note = string.Empty;
            int row_count = 0;

            foreach (DataRow trw in dt.Rows)
            {
                if (row_count != 0 && bookdate.CompareTo(trw["Bookdate"]) != 0)
                {
                    //int idx = bookdate.Day + 1;

                    add_employee(rdt, row_count, hour, over, toff, vessel, emp_id, emp_name, note, bookdate);
         
                    row_count = 0;
                }

                if (row_count == 0)
                {
                    bookdate = (DateTime)trw["Bookdate"];
                    hour = 0;
                    over = 0;
                    toff = string.Empty;
                    vessel = string.Empty;

                    emp_name = (string)trw["EmpName"];

                    row_count = 0;
                }

                row_count += 1;
                hour += Convert.ToDecimal(trw["LogHours"]);
                over += Convert.ToDecimal(trw["LogOver"]);
                if (toff.Length == 0)
                    toff = (string)trw["ToffCode"];
                else
                {
                    if (((string)trw["ToffCode"]).Length != 0) toff += "/" + (string)trw["ToffCode"];
                }

                if (vessel.Length == 0)
                {
                    if (((string)trw["LogVessel"]).Length != 0) vessel = (string)trw["LogVessel"];
                    else
                    {
                        if (((string)trw["LogVessel"]).Length != 0) toff += "/" + (string)trw["LogVessel"];
                    }
                }

                if (!trw["LogNote"].Equals(DBNull.Value))
                {
                    if (((string)trw["LogNote"]).Length != 0)
                    {
                        if (note.Length == 0)
                            note = (string)trw["LogNote"];
                        else
                            note += "\n" + (string)trw["LogNote"];
                    }
                }

            }

            if (row_count > 0)  // catch the edge
            {
                //int idx = bookdate.Day + 1;
                add_employee(rdt, row_count, hour, over, toff, vessel, emp_id, emp_name, note, bookdate);
            }

            //_details.Add(monthRef.Month.ToString(), detail);                    

            return rdt;
        }


        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        static private DataTable build_import(DateTime start_date, int days, string emp_id)
        {
            DateTime beg = start_date.Date.AddDays(-1);
            DateTime end = start_date.Date.AddDays(days);

            //DataTable dtEmp = dacCache.GetEmployee();
            try
            {
                var q = from tTime in dacCache.GetTimebook().AsEnumerable()                        
                        orderby tTime["EmpId"], tTime["Bookdate"], tTime["LogShift"]
                        where ((string)tTime["EmpId"]).Equals(emp_id)
                            && DateTime.Compare(beg, ((DateTime)tTime["Bookdate"]).Date) == -1
                            && DateTime.Compare(end, ((DateTime)tTime["Bookdate"]).Date) == 1
                        select new
                        {
                            EmpId = (string)tTime["EmpId"],
                            EmpName = (string)tTime["EmpName"],
                            Bookdate = (DateTime)tTime["Bookdate"],
                            LogHours = (Decimal)tTime["LogHours"],
                            LogOver = (Decimal)tTime["LogOver"],
                            LogVessel = (tTime["LogVessel"].Equals(DBNull.Value) ? "" : (string)tTime["LogVessel"]),
                            Shift = (tTime["LogShift"].Equals(DBNull.Value) ? -1 : (int)tTime["LogShift"]),
                            ShiftHour = (tTime["LogHour"].Equals(DBNull.Value) ? "<>" : (string)tTime["LogHour"]),
                            ToffCode = (tTime["ToffCode"].Equals(DBNull.Value) ? "" : (string)tTime["ToffCode"]),                            
                            Resp = (tTime["Resp"].Equals(DBNull.Value) ? "" : (string)tTime["Resp"]),                           
                            LogNote = (tTime["LogNote"].Equals(DBNull.Value) ? "" : (string)tTime["LogNote"])
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
                MessageBox.Show(string.Format("Error (build_import) : {0}", ex.Message));
            }

            return null;
        }


        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        static private DataTable build_toffsum(DateTime year, string emp_id)
        {
            DateTime first_day = new DateTime(year.Year, 1, 1).Date;
            

            //DataTable dtTime = dacCache.GetTimebook();
            DataSet dsYear = dacTimebook.GetDSbyEmpId(first_day, 366, emp_id);
            DataTable dtTime = dsYear.Tables[0];

            try
            {
                //var s = from tTime in dtTime.AsEnumerable()
                //        where DateTime.Compare(first_day, ((DateTime)tTime["BookDate"]).Date) == -1
                //            && DateTime.Compare(last_day, ((DateTime)tTime["BookDate"]).Date) == 1
                //        where tTime["EmpId"].Equals("G8")
                //        select new
                //        {
                //            EmpId = tTime["EmpId"],
                //            Date = tTime["BookDate"],
                //            Toff = tTime["ToffCode"]                            
                //        };
                //DataTable rdt = ToDataTable(s.ToList());

                var q = from tTime in dtTime.AsEnumerable()
                        where tTime["ToffCode"] != DBNull.Value
                        && ( ((string)tTime["ToffCode"]).Equals("H") || ((string)tTime["ToffCode"]).Equals("O")
                        || ((string)tTime["ToffCode"]).Equals("U")  || ((string)tTime["ToffCode"]).Equals("S") )
                        
                        group tTime by new { a = tTime["EmpId"], b = tTime["BookDate"] } into g
                        let h = g.Count(tTime => (tTime.Field<String>("ToffCode")).Equals("H"))
                        let o = g.Count(tTime => (tTime.Field<String>("ToffCode")).Equals("O"))
                        let u = g.Count(tTime => (tTime.Field<String>("ToffCode")).Equals("U"))
                        let s = g.Count(tTime => (tTime.Field<String>("ToffCode")).Equals("S"))
                        select new
                        {
                            EmpId = g.Key.a,
                            Date = g.Key.b,
                            H = h,
                            O = o,
                            U = u,
                            S = s
                        };


                // MessageBox.Show("q=" + q.Count().ToString());
                DataTable dt = libAlly.TO_DATA_TABLE(q.ToList());
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error (build_toffsum) : {0}", ex.Message));
            }

            return null;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        static private DataTable build_timesum(DateTime month)
        {
            DateTime first_day = new DateTime(month.Year, month.Month, 1).Date;
            DateTime last_day = first_day.AddMonths(1).Date;
            first_day = first_day.AddDays(-1);

            DataTable dtTime = dacCache.GetTimebook();

            try
            {
                //var s = from tTime in dtTime.AsEnumerable()
                //        where DateTime.Compare(first_day, ((DateTime)tTime["BookDate"]).Date) == -1
                //            && DateTime.Compare(last_day, ((DateTime)tTime["BookDate"]).Date) == 1
                //        where tTime["EmpId"].Equals("G8")
                //        select new
                //        {
                //            EmpId = tTime["EmpId"],
                //            Date = tTime["BookDate"],
                //            Hours = tTime["LogHours"],
                //            Overs = tTime["LogOver"]
                //        };
                //DataTable rdt = ToDataTable(s.ToList());

                var q = from tTime in dtTime.AsEnumerable()
                        where DateTime.Compare(first_day, ((DateTime)tTime["Bookdate"]).Date) == -1
                            && DateTime.Compare(last_day, ((DateTime)tTime["Bookdate"]).Date) == 1
                        group tTime by new { a = tTime["EmpId"], b = tTime["BookDate"] } into g
                        //orderby g.Key                                                
                        let Hour = g.Sum(tTime => tTime.Field<Decimal>("LogHours"))
                        let Over = g.Sum(tTime => tTime.Field<Decimal>("LogOver"))
                        select new
                        {
                            EmpId = g.Key.a,
                            Date = g.Key.b,
                            Hours = Hour,
                            Overs = Over
                        };

                DataTable dt = libAlly.TO_DATA_TABLE(q.ToList());
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error (build_timesum) : {0}", ex.Message));
            }

            return null;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        static private DataTable build_yearsum(DateTime month, string emp_id)
        {
            DataTable qs = build_toffsum(month, emp_id);

            try
            {

                var q = from tTime in qs.AsEnumerable()
                        group tTime by tTime["EmpId"] into g
                        orderby g.Key
                        let H = g.Sum(tTime => tTime.Field<int>("H"))
                        let O = g.Sum(tTime => tTime.Field<int>("O"))
                        let U = g.Sum(tTime => tTime.Field<int>("U"))
                        let S = g.Sum(tTime => tTime.Field<int>("S"))
                        select new
                        {
                            EmpId = g.Key,
                            Holiday = H,
                            Off = O,
                            Unavail = U,
                            Sick = S
                        };

                DataTable dt = libAlly.TO_DATA_TABLE(q.ToList());

                DataColumn pk1 = dt.Columns["EmpId"]; //pk1.MaxLength = 8; pk1.AllowDBNull = false;
                dt.PrimaryKey = new DataColumn[] { pk1 };

                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error (build_yearsum) : {0}", ex.Message));
            }

            return null;
        }



        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        static private DataTable build_s12sum(DateTime month)
        {
            DateTime first_day = new DateTime(month.Year, month.Month, 1).Date;
            DateTime last_day = first_day.AddMonths(1).Date;
            first_day = first_day.AddDays(-1);

            DataTable dtTime = dacCache.GetTimebook();

            try
            {
                var w = from tTime in dtTime.AsEnumerable()
                        where DateTime.Compare(first_day, ((DateTime)tTime["BookDate"]).Date) == -1
                            && DateTime.Compare(last_day, ((DateTime)tTime["BookDate"]).Date) == 1
                        where tTime["EmpId"].Equals("A6")
                        select new
                        {
                            EmpId = tTime["EmpId"],
                            Date = tTime["BookDate"],
                            Toff = tTime["ToffCode"]
                        };
                DataTable rdt = libAlly.TO_DATA_TABLE(w.ToList());

                var q = from tTime in dtTime.AsEnumerable()
                        where DateTime.Compare(first_day, ((DateTime)tTime["BookDate"]).Date) == -1
                            && DateTime.Compare(last_day, ((DateTime)tTime["BookDate"]).Date) == 1                            
                            && tTime["ToffCode"] != DBNull.Value
                            //&& ((string)tTime["ToffCode"]).Equals("12")
                            && (((string)tTime["ToffCode"]).Equals("12") || ((string)tTime["ToffCode"]).Equals("H")
                            || ((string)tTime["ToffCode"]).Equals("O") || ((string)tTime["ToffCode"]).Equals("U") || ((string)tTime["ToffCode"]).Equals("S"))
                        group tTime by new { a = tTime["EmpId"], b = tTime["BookDate"] } into g
                        let t = g.Count(tTime => (tTime.Field<String>("ToffCode")).Equals("12"))
                        let h = g.Count(tTime => (tTime.Field<String>("ToffCode")).Equals("H"))
                        let o = g.Count(tTime => (tTime.Field<String>("ToffCode")).Equals("O"))
                        let u = g.Count(tTime => (tTime.Field<String>("ToffCode")).Equals("U"))
                        let s = g.Count(tTime => (tTime.Field<String>("ToffCode")).Equals("S"))
                        select new
                        {
                            EmpId = g.Key.a,
                            Date = g.Key.b,
                            S12 = t,
                            H = h,
                            O = o,
                            U = u,
                            S = s
                        };


                // MessageBox.Show("q=" + q.Count().ToString());
                DataTable dt = libAlly.TO_DATA_TABLE(q.ToList());
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error (build_S12sum) : {0}", ex.Message));
            }

            return null;
        }


        static private DataTable build_schedsum(DateTime month)
        {
            DataTable qs = build_s12sum(month);

            try
            {

                var q = from tTime in qs.AsEnumerable()
                        group tTime by tTime["EmpId"] into g
                        orderby g.Key
                        let Day = g.Sum(tTime => tTime.Field<int>("S12"))
                        let H = g.Sum(tTime => tTime.Field<int>("H"))
                        let O = g.Sum(tTime => tTime.Field<int>("O"))
                        let U = g.Sum(tTime => tTime.Field<int>("U"))
                        let S = g.Sum(tTime => tTime.Field<int>("S"))
                        select new
                        {
                            EmpId = g.Key,
                            Days = Day,
                            Holiday = H,
                            Off = O,
                            Unavail = U,
                            Sick = S
                        };

                DataTable dt = libAlly.TO_DATA_TABLE(q.ToList());

                DataColumn pk1 = dt.Columns["EmpId"]; //pk1.MaxLength = 8; pk1.AllowDBNull = false;
                dt.PrimaryKey = new DataColumn[] { pk1 };

                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error (build_schedsum) : {0}", ex.Message));
            }

            return null;
        }

        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        static private DataTable build_worksum(DateTime month)
        {
            DataTable qs = build_timesum(month);

            try
            {

                var q = from tTime in qs.AsEnumerable()
                        group tTime by tTime["EmpId"] into g
                        orderby g.Key
                        let Day = g.Count(tTime => tTime.Field<Decimal>("Hours") != 0M)
                        let Hour = g.Sum(tTime => tTime.Field<Decimal>("Hours"))
                        let Over = g.Sum(tTime => tTime.Field<Decimal>("Overs"))
                        select new
                        {
                            EmpId = g.Key,
                            Days = Day,
                            Hours = Hour,
                            Overs = Over
                        };

                DataTable dt = libAlly.TO_DATA_TABLE(q.ToList());

                DataColumn pk1 = dt.Columns["EmpId"]; //pk1.MaxLength = 8; pk1.AllowDBNull = false;
                dt.PrimaryKey = new DataColumn[] { pk1 };

                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error (build_worksum) : {0}", ex.Message));
            }

            return null;
        }

        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        static public DataTable GetView(string qry_name, DateTime start_date)
        {
            // Worksum
            // Shedsum

            return get_view(qry_name, start_date, 0, "", null);
        }


        static public DataTable GetView(string qry_name, DateTime start_date, int days, string emp_id, DataTable dt_gang)
        {
            // Yearsum
            // Timebook
            // Employee

            return get_view(qry_name, start_date, days, emp_id, dt_gang);
        }


        //static public DataTable GetView(string qry_name, DateTime start_week, int days)
        static private DataTable get_view(string qry_name, DateTime start_date, int days, string emp_id, DataTable dt_gang)
        {
            if (_requery)
            {
                if (_dt_timebook != null) _dt_timebook.Dispose();
                _requery = false;
                _dt_timebook = null;
            }

            dacCache.RefreshTimebook(start_date, true);
            _dt_timebook = dacCache.GetTimebook();

            //if (_qry_summary.Equals(qry_name) && _dt_timebook != null) return _dt_timebook;

            _qry_summary = qry_name;

            if (qry_name.Equals("Import")) _dt_timebook = build_import(start_date, days, emp_id);
            if (qry_name.Equals("Employee")) return build_employee(start_date, days, emp_id);
            if (qry_name.Equals("TimeBook")) return build_timebook(start_date, days, dt_gang);
            if (qry_name.Equals("Worksum")) _dt_timebook = build_worksum(start_date);
            if (qry_name.Equals("Schedsum")) _dt_timebook = build_schedsum(start_date);
            if (qry_name.Equals("Yearsum")) return build_yearsum(start_date, emp_id);

            return _dt_timebook;
        }

    }
}
