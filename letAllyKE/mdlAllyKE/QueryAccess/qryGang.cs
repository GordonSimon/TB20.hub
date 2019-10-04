using System;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;

using System.Windows.Forms;

using System.Linq;
using System.ComponentModel;



/// <summary>
/// Summary description for qryGang
/// </summary>

namespace mdlAllyKE
{
    public class qryGang
    {
        static private string _qry_gang = "default";
        static private DataTable _dt_gang = null;
        static private bool _requery = false;
        static private bool _archive = false;

        static public DataTable GetDT() { return _dt_gang; }
        static public void Requery() { _requery = true; }
        static public void SetArchive(bool v) { _archive = v; }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        static public bool ProfileAdd(string eid)
        {
            if (_dt_gang == null) return false;

            DataRow row = _dt_gang.Rows.Find(eid);
            if (row != null)
            {
                row.BeginEdit();
                row["Profile"] = "Guest";
                row.EndEdit();
            }

            return (row != null);
        }


        static public void BuildProfile()
        {
            if (_dt_gang == null) return;

            bool is_empty = true;
            foreach (DataRow row in _dt_gang.Rows)
                if (row["Profile"].Equals("Guest")) { is_empty = false; break; }

            if (is_empty) return;

            _dt_gang.AcceptChanges();
            foreach (DataRow row in _dt_gang.Rows)
            {
                if (!row["Profile"].Equals("Guest"))
                    row.Delete();
            }
            _dt_gang.AcceptChanges();
        }

        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        static public bool IsKeyActive(string emp_id)
        {
            try
            {
                if (_dt_gang == null) return false;

                DataRow row = _dt_gang.Rows.Find(new object[] { emp_id });

                bool active = false;
                if (row != null)
                    active = (bool)row["Active"];

                return active;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(" Error (IsKey) : {0}", ex.Message));
            }

            return false;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        static private DataTable build_gang_working()
        {
            try
            {
                // select distinct            
                //DataTable dtTime = dacCache.GetTimebook().DefaultView.ToTable(true, new string[] { "EmpId" });

                DataTable dtEmp = dacCache.GetEmployee();
                //DataTable dtGang = dacCache.GetGang();
                DataTable dtGang = dacGang.GetDT();


                //var q = from tGang in dtGang.AsEnumerable()
                //        join tCrew in dtCrew.AsEnumerable()
                //        on (string)tGang["EmpId"] equals (string)tCrew["EmpId"]
                //        join tEmp in dtEmp.AsEnumerable()
                //        on (string)tGang["EmpID"] equals (string)tEmp["EmpID"]
                //        select new
                //        {
                //            EmpID = (string)tGang["EmpID"],
                //            EmpName = (string)tGang["EmpName"],
                //            HomePhone = (tGang["HomePhone"].Equals(DBNull.Value) ? "" : (string)tGang["HomePhone"]),
                //            CellPhone = (tGang["CellPhone"].Equals(DBNull.Value) ? "" : (string)tGang["CellPhone"]),
                //            Active = (bool)tGang["Active"],
                //            Duty = (string)tEmp["Duty"],
                //            Master = (tEmp["Master"].Equals(DBNull.Value) ? false : (bool)tEmp["Master"])
                //        };


                var q = from l in dtEmp.AsEnumerable()
                        join r in dtGang.AsEnumerable()
                        on l["EmpId"] equals r["EmpId"] into lr
                        from r in lr.DefaultIfEmpty()
                        //where !(bool)r["Active"]
                        orderby l["Last Name"], l["First Name"]
                        select new
                        {
                            EmpID = (string)l["EmpId"],
                            EmpName = string.Format("{0}, {1}", ((string)l["Last Name"]).ToUpper(), (string)l["First Name"]),
                            EmpNum = alpha2num((string)l["EmpID"]),
                            Active = (r == null ? false : (bool)r["Active"]),
                            Master = (l["Master"].Equals(DBNull.Value) ? false : (bool)l["Master"]),
                            HomePhone = (l["Main Phone"].Equals(DBNull.Value) ? "" : (string)l["Main Phone"]),
                            CellPhone = (l["Cell Phone"].Equals(DBNull.Value) ? "" : (string)l["Cell Phone"]),
                            Duty = (l["Duty"].Equals(DBNull.Value) ? "None" : (string)l["Duty"]),
                            Employment = (l["Employment"].Equals(DBNull.Value) ? "Other" : (string)l["Employment"]),
                            //Archive = (bool)l["Archive"],
                            Profile = "Working"                            
                        };

                //var q = from tTime in dtTime.AsEnumerable()
                //        join tEmp in dtEmp.AsEnumerable()
                //        on (string)tTime["EmpID"] equals (string)tEmp["EmpID"]
                //        where !(bool)tEmp["Archive"]                        
                //        //orderby tEmp["EmpID"]                        
                //        orderby tEmp["Last Name"], tEmp["First Name"]
                //        //where ! tCrew["LogVessel"].Equals(DBNull.Value) && tCrew["LogVessel"].Equals(vessel)
                //        select new Gang
                //        {
                //            EmpID = (string)tEmp["EmpID"],
                //            //EmpName = (string)tCrew["EmpName"],
                //            EmpName = string.Format("{0}, {1}", ((string)tEmp["Last Name"]).ToUpper(), (string)tEmp["First Name"]),
                //            EmpNum = alpha2num((string)tEmp["EmpID"]),
                //            HomePhone = (tEmp["Main Phone"].Equals(DBNull.Value) ? "" : (string)tEmp["Main Phone"]),
                //            CellPhone = (tEmp["Cell Phone"].Equals(DBNull.Value) ? "" : (string)tEmp["Cell Phone"]),
                //            Active = true,
                //            Duty = (string)tEmp["Duty"],
                //            Master = (tEmp["Master"].Equals(DBNull.Value) ? false : (bool)tEmp["Master"]),
                //            Employment = (tEmp["Employment"].Equals(DBNull.Value) ? "Other" : (string)tEmp["Employment"]),
                //            Profile = "Working"
                //        };

                DataTable dt = libAlly.TO_DATA_TABLE(q.ToList());

                DataColumn pk1 = dt.Columns[0]; pk1.MaxLength = 8; pk1.AllowDBNull = false;
                dt.PrimaryKey = new DataColumn[] { pk1 };

                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error (build_gang_working) : {0}", ex.Message));
            }

            return null;
        }


        static private DataTable build_gang_offwork()
        {
            try
            {
                // select distinct                
                DataTable dtTime = dacCache.GetTimebook().DefaultView.ToTable(true, new string[] { "EmpId", "EmpName", "ToffCode" });

                DataTable dtEmp = dacCache.GetEmployee();

                var q = from tTime in dtTime.AsEnumerable()
                        join tEmp in dtEmp.AsEnumerable()
                        on (string)tTime["EmpID"] equals (string)tEmp["EmpID"]
                        where !tTime["ToffCode"].Equals(DBNull.Value) && (string)tTime["ToffCode"] == "O"
                        //orderby tEmp["EmpID"]
                        orderby (string)tEmp["Last Name"], (string)tEmp["First Name"]
                        //where ! tCrew["LogVessel"].Equals(DBNull.Value) && tCrew["LogVessel"].Equals(vessel)
                        select new Gang
                        {
                            EmpID = (string)tEmp["EmpID"],
                            //EmpName = (string)tCrew["EmpName"],
                            EmpName = string.Format("{0}, {1}", ((string)tEmp["Last Name"]).ToUpper(), (string)tEmp["First Name"]),
                            EmpNum = alpha2num((string)tEmp["EmpID"]),
                            HomePhone = (tEmp["Main Phone"].Equals(DBNull.Value) ? "" : (string)tEmp["Main Phone"]),
                            CellPhone = (tEmp["Cell Phone"].Equals(DBNull.Value) ? "" : (string)tEmp["Cell Phone"]),
                            Active = true,
                            Duty = (string)tEmp["Duty"],
                            Master = (tEmp["Master"].Equals(DBNull.Value) ? false : (bool)tEmp["Master"]),
                            Employment = (tEmp["Employment"].Equals(DBNull.Value) ? "Other" : (string)tEmp["Employment"]),
                            Profile = "OffWork"
                        };

                DataTable dt = libAlly.TO_DATA_TABLE(q.ToList());

                DataColumn pk1 = dt.Columns[0]; pk1.MaxLength = 8; pk1.AllowDBNull = false;
                dt.PrimaryKey = new DataColumn[] { pk1 };

                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error (build_gang_working) : {0}", ex.Message));
            }

            return null;
        }

        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        static private DataTable build_gang_vessel(string vessel, DateTime start_week, int days)
        {
            try
            {
                DateTime beg = start_week.Date.AddDays(-1);
                DateTime end = start_week.Date.AddDays(days);

                // select distinct
                DataTable dtAll = dacCache.GetTimebook();
                var rows = from row in dtAll.AsEnumerable()
                           where row["LogVessel"].Equals(vessel)
                              && DateTime.Compare(beg, ((DateTime)row["Bookdate"]).Date) == -1
                              && DateTime.Compare(end, ((DateTime)row["Bookdate"]).Date) == 1
                           select row;
                DataTable dtv = rows.Any() ? rows.CopyToDataTable() : dtAll.Clone();
                DataTable dtCrew = dtv.DefaultView.ToTable(true, new string[] { "EmpId", "LogVessel" });                           
                //DataTable dtCrew = dacCache.GetTimebook().DefaultView.ToTable(true, new string[] { "EmpId", "LogVessel" });

                DataTable dtEmp = dacCache.GetEmployee();
                //DataTable dtGang = dacCache.GetGang();


                //var q = from tGang in dtGang.AsEnumerable()
                //        join tCrew in dtCrew.AsEnumerable()
                //        on (string)tGang["EmpId"] equals (string)tCrew["EmpId"]
                //        join tEmp in dtEmp.AsEnumerable()
                //        on (string)tGang["EmpID"] equals (string)tEmp["EmpID"]
                //        select new
                //        {
                //            EmpID = (string)tGang["EmpID"],
                //            EmpName = (string)tGang["EmpName"],
                //            HomePhone = (tGang["HomePhone"].Equals(DBNull.Value) ? "" : (string)tGang["HomePhone"]),
                //            CellPhone = (tGang["CellPhone"].Equals(DBNull.Value) ? "" : (string)tGang["CellPhone"]),
                //            Active = (bool)tGang["Active"],
                //            Duty = (string)tEmp["Duty"],
                //            Master = (tEmp["Master"].Equals(DBNull.Value) ? false : (bool)tEmp["Master"])
                //        };

                //var q = from tCrew in dtCrew.AsEnumerable()
                //        join tEmp in dtEmp.AsEnumerable()
                //        on (string)tCrew["EmpID"] equals (string)tEmp["EmpID"]
                //        where !tCrew["LogVessel"].Equals(DBNull.Value) && tCrew["LogVessel"].Equals(vessel)
                //        //orderby tEmp["EmpID"]
                //        orderby (string)tEmp["Last Name"], (string)tEmp["First Name"]
                //        select new Gang
                //        {
                //            EmpID = (string)tEmp["EmpID"],
                //            EmpName = string.Format("{0}, {1}", ((string)tEmp["Last Name"]).ToUpper(), (string)tEmp["First Name"]),
                //            EmpNum = alpha2num((string)tEmp["EmpID"]),
                //            HomePhone = (tEmp["Main Phone"].Equals(DBNull.Value) ? "" : (string)tEmp["Main Phone"]),
                //            CellPhone = (tEmp["Cell Phone"].Equals(DBNull.Value) ? "" : (string)tEmp["Cell Phone"]),
                //            Active = true,
                //            Duty = (string)tEmp["Duty"],
                //            Master = (tEmp["Master"].Equals(DBNull.Value) ? false : (bool)tEmp["Master"]),
                //            Employment = (tEmp["Employment"].Equals(DBNull.Value) ? "Other" : (string)tEmp["Employment"]),
                //            Profile = "Vessel"
                //        };


                //var q = from l in dtEmp.AsEnumerable()
                //        join r in dtCrew.AsEnumerable()                        
                //        on (string)l["EmpID"] equals (string)r["EmpID"]
                //        where !r["LogVessel"].Equals(DBNull.Value) && r["LogVessel"].Equals(vessel)
                //        orderby (string)l["Last Name"], (string)l["First Name"]
                //        select new
                //        {
                //            EmpID = (string)l["EmpId"],
                //            EmpName = string.Format("{0}, {1}", ((string)l["Last Name"]).ToUpper(), (string)l["First Name"]),
                //            EmpNum = alpha2num((string)l["EmpID"]),
                //            //Active = !(bool)l["Archive"] && !r["LogVessel"].Equals(DBNull.Value) && r["LogVessel"].Equals(vessel),
                //            Active = true,
                //            Master = (l["Master"].Equals(DBNull.Value) ? false : (bool)l["Master"]),
                //            HomePhone = (l["Main Phone"].Equals(DBNull.Value) ? "" : (string)l["Main Phone"]),
                //            CellPhone = (l["Cell Phone"].Equals(DBNull.Value) ? "" : (string)l["Cell Phone"]),
                //            Duty = (l["Duty"].Equals(DBNull.Value) ? "None" : (string)l["Duty"]),
                //            Employment = (l["Employment"].Equals(DBNull.Value) ? "Other" : (string)l["Employment"]),
                //            Ident = string.Format("{0,-3}:{1,-6} {2}, {3}",
                //                (string)l["EmpID"], alpha2num((string)l["EmpID"]),
                //                ((string)l["Last Name"]).ToUpper(), (string)l["First Name"]),
                //            Profile = "Vessel"
                //        };

                var q = from l in dtEmp.AsEnumerable()
                        join r in dtCrew.AsEnumerable()
                        on (string)l["EmpID"] equals (string)r["EmpID"] into rl
                        from r in rl.DefaultIfEmpty()
                        orderby (string)l["Last Name"], (string)l["First Name"]
                        select new
                        {
                            EmpID = (string)l["EmpId"],
                            EmpName = string.Format("{0}, {1}", ((string)l["Last Name"]).ToUpper(), (string)l["First Name"]),
                            EmpNum = alpha2num((string)l["EmpID"]),
                            Active = !(bool)l["Archive"] && r != null,
                            //Active = true,
                            Master = (l["Master"].Equals(DBNull.Value) ? false : (bool)l["Master"]),
                            HomePhone = (l["Main Phone"].Equals(DBNull.Value) ? "" : (string)l["Main Phone"]),
                            CellPhone = (l["Cell Phone"].Equals(DBNull.Value) ? "" : (string)l["Cell Phone"]),
                            Duty = (l["Duty"].Equals(DBNull.Value) ? "None" : (string)l["Duty"]),
                            Employment = (l["Employment"].Equals(DBNull.Value) ? "Other" : (string)l["Employment"]),
                            Ident = string.Format("{0,-3}:{1,-6} {2}, {3}",
                                (string)l["EmpID"], alpha2num((string)l["EmpID"]),
                                ((string)l["Last Name"]).ToUpper(), (string)l["First Name"]),
                            Profile = "Vessel"
                        };
                DataTable dt = libAlly.TO_DATA_TABLE(q.ToList());

                DataColumn pk1 = dt.Columns[0]; pk1.MaxLength = 8; pk1.AllowDBNull = false;
                dt.PrimaryKey = new DataColumn[] { pk1 };

                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error (build_gang_vessel) : {0}", ex.Message));
            }

            return null;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        static private string alpha2num(string emp_id)
        {
            string alpha = "";

            foreach (char c in emp_id.ToUpper())
            {
                if (char.IsLetter(c))
                {
                    int v = ((int)c - (int)'A' + 1);
                    alpha += v.ToString("00");
                }
                if (char.IsDigit(c))
                {
                    int v = (int)c - (int)'0';
                    alpha += v.ToString();
                }
            }

            return alpha;
        }


        static private DataTable build_gang_all()
        {
            DataTable dtEmp = dacCache.GetEmployee();

            //var q = from tEmp in dtEmp.AsEnumerable()
            //        where !(bool)tEmp["Archive"]
            //        //orderby tEmp["EmpID"]
            //        orderby (string)tEmp["Last Name"], (string)tEmp["First Name"]
            //        select new Gang
            //        {
            //            EmpID = (string)tEmp["EmpID"],
            //            EmpName = string.Format("{0}, {1}", ((string)tEmp["Last Name"]).ToUpper(), (string)tEmp["First Name"]),
            //            EmpNum = alpha2num((string)tEmp["EmpID"]),
            //            HomePhone = (tEmp["Main Phone"].Equals(DBNull.Value) ? "" : (string)tEmp["Main Phone"]),
            //            CellPhone = (tEmp["Cell Phone"].Equals(DBNull.Value) ? "" : (string)tEmp["Cell Phone"]),
            //            Active = true,
            //            Duty = (string)tEmp["Duty"],
            //            Master = (tEmp["Master"].Equals(DBNull.Value) ? false : (bool)tEmp["Master"]),
            //            Employment = (tEmp["Employment"].Equals(DBNull.Value) ? "Other" : (string)tEmp["Employment"]),
            //            Ident = string.Format("{0,-3}:{1,-6} {2}, {3}",
            //              (string)tEmp["EmpID"], alpha2num((string)tEmp["EmpID"]),
            //              ((string)tEmp["Last Name"]).ToUpper(), (string)tEmp["First Name"]),
            //            Profile = "All"
            //        };
            var q = from l in dtEmp.AsEnumerable()
                    //where !l["Employment"].Equals(DBNull.Value) && (string)l["Employment"] == "FullTime"
                    orderby l["Last Name"], l["First Name"]
                    select new
                    {
                        EmpID = (string)l["EmpId"],
                        EmpName = string.Format("{0}, {1}", ((string)l["Last Name"]).ToUpper(), (string)l["First Name"]),
                        EmpNum = alpha2num((string)l["EmpID"]),
                        Active = !(bool)l["Archive"] && !l["Duty"].Equals(DBNull.Value) && (string)l["Duty"] != "Office",
                        Master = (l["Master"].Equals(DBNull.Value) ? false : (bool)l["Master"]),
                        HomePhone = (l["Main Phone"].Equals(DBNull.Value) ? "" : (string)l["Main Phone"]),
                        CellPhone = (l["Cell Phone"].Equals(DBNull.Value) ? "" : (string)l["Cell Phone"]),
                        Duty = (l["Duty"].Equals(DBNull.Value) ? "None" : (string)l["Duty"]),
                        Employment = (l["Employment"].Equals(DBNull.Value) ? "Other" : (string)l["Employment"]),
                        Ident = string.Format("{0,-3}:{1,-6} {2}, {3}",
                          (string)l["EmpID"], alpha2num((string)l["EmpID"]),
                          ((string)l["Last Name"]).ToUpper(), (string)l["First Name"]),
                        Profile = "All"
                    };

            DataTable dt = libAlly.TO_DATA_TABLE(q.ToList());

            DataColumn pk1 = dt.Columns[0]; pk1.MaxLength = 8; pk1.AllowDBNull = false;
            dt.PrimaryKey = new DataColumn[] { pk1 };

            return dt;
        }


        static private DataTable build_gang_all_archive()
        {
            DataTable dtEmp = dacCache.GetEmployee();

            //var q = from tEmp in dtEmp.AsEnumerable()
            //        where (bool)tEmp["Archive"]
            //        //orderby tEmp["EmpID"]
            //        orderby (string)tEmp["Last Name"], (string)tEmp["First Name"]
            //        select new Gang
            //        {
            //            EmpID = (string)tEmp["EmpID"],
            //            EmpName = string.Format("{0}, {1}", ((string)tEmp["Last Name"]).ToUpper(), (string)tEmp["First Name"]),
            //            EmpNum = alpha2num((string)tEmp["EmpID"]),
            //            HomePhone = (tEmp["Main Phone"].Equals(DBNull.Value) ? "" : (string)tEmp["Main Phone"]),
            //            CellPhone = (tEmp["Cell Phone"].Equals(DBNull.Value) ? "" : (string)tEmp["Cell Phone"]),
            //            Active = true,
            //            Duty = (string)tEmp["Duty"],
            //            Master = (tEmp["Master"].Equals(DBNull.Value) ? false : (bool)tEmp["Master"]),
            //            Employment = (tEmp["Employment"].Equals(DBNull.Value) ? "Other" : (string)tEmp["Employment"]),
            //            Ident = string.Format("{0,-3}:{1,-6} {2}, {3}",
            //              (string)tEmp["EmpID"], alpha2num((string)tEmp["EmpID"]),
            //              ((string)tEmp["Last Name"]).ToUpper(), (string)tEmp["First Name"]),
            //            Profile = "Archive"
            //        };

            var q = from l in dtEmp.AsEnumerable()
                    //where !l["Employment"].Equals(DBNull.Value) && (string)l["Employment"] == "FullTime"
                    orderby l["Last Name"], l["First Name"]
                    select new
                    {
                        EmpID = (string)l["EmpId"],
                        EmpName = string.Format("{0}, {1}", ((string)l["Last Name"]).ToUpper(), (string)l["First Name"]),
                        EmpNum = alpha2num((string)l["EmpID"]),                       
                        Active = (bool)l["Archive"] && !l["Duty"].Equals(DBNull.Value) && (string)l["Duty"] != "Office",
                        Master = (l["Master"].Equals(DBNull.Value) ? false : (bool)l["Master"]),
                        HomePhone = (l["Main Phone"].Equals(DBNull.Value) ? "" : (string)l["Main Phone"]),
                        CellPhone = (l["Cell Phone"].Equals(DBNull.Value) ? "" : (string)l["Cell Phone"]),
                        Duty = (l["Duty"].Equals(DBNull.Value) ? "None" : (string)l["Duty"]),
                        Employment = (l["Employment"].Equals(DBNull.Value) ? "Other" : (string)l["Employment"]),
                        Ident = string.Format("{0,-3}:{1,-6} {2}, {3}",
                          (string)l["EmpID"], alpha2num((string)l["EmpID"]),
                          ((string)l["Last Name"]).ToUpper(), (string)l["First Name"]),
                        Profile = "Archive"
                    };

            DataTable dt = libAlly.TO_DATA_TABLE(q.ToList());

            DataColumn pk1 = dt.Columns[0]; pk1.MaxLength = 8; pk1.AllowDBNull = false;
            dt.PrimaryKey = new DataColumn[] { pk1 };

            return dt;
        }


        static private DataTable build_gang_crew()
        {
            DataTable dtEmp = dacCache.GetEmployee();

            //var q = from tEmp in dtEmp.AsEnumerable()
            //        //where !(bool)tEmp["Archive"] && !tEmp["Duty"].Equals(DBNull.Value) && ((string)tEmp["Duty"] == "Skipper" || (string)tEmp["Duty"] == "Deckhand")
            //        //orderby tEmp["EmpID"]
            //        orderby (string)tEmp["Last Name"], (string)tEmp["First Name"]
            //        select new Gang
            //        {
            //            EmpID = (string)tEmp["EmpID"],
            //            EmpName = string.Format("{0}, {1}", ((string)tEmp["Last Name"]).ToUpper(), (string)tEmp["First Name"]),
            //            EmpNum = alpha2num((string)tEmp["EmpID"]),
            //            HomePhone = (tEmp["Main Phone"].Equals(DBNull.Value) ? "" : (string)tEmp["Main Phone"]),
            //            CellPhone = (tEmp["Cell Phone"].Equals(DBNull.Value) ? "" : (string)tEmp["Cell Phone"]),
            //            //Active = true,
            //            Active = !(bool)tEmp["Archive"] && !tEmp["Duty"].Equals(DBNull.Value) && ((string)tEmp["Duty"] == "Skipper" || (string)tEmp["Duty"] == "Deckhand"),
            //            Duty = (string)tEmp["Duty"],
            //            Master = (tEmp["Master"].Equals(DBNull.Value) ? false : (bool)tEmp["Master"]),
            //            Employment = (tEmp["Employment"].Equals(DBNull.Value) ? "Other" : (string)tEmp["Employment"]),
            //            //Profile = "FullTime"
            //            Profile = "Crew"
            //        };
            var q = from l in dtEmp.AsEnumerable()
                    //where !l["Employment"].Equals(DBNull.Value) && (string)l["Employment"] == "FullTime"
                    orderby l["Last Name"], l["First Name"]
                    select new
                    {
                        EmpID = (string)l["EmpId"],
                        EmpName = string.Format("{0}, {1}", ((string)l["Last Name"]).ToUpper(), (string)l["First Name"]),
                        EmpNum = alpha2num((string)l["EmpID"]),
                        Active = !(bool)l["Archive"] && !l["Duty"].Equals(DBNull.Value) && (string)l["Duty"] != "Office",
                        Master = (l["Master"].Equals(DBNull.Value) ? false : (bool)l["Master"]),
                        HomePhone = (l["Main Phone"].Equals(DBNull.Value) ? "" : (string)l["Main Phone"]),
                        CellPhone = (l["Cell Phone"].Equals(DBNull.Value) ? "" : (string)l["Cell Phone"]),
                        Duty = (l["Duty"].Equals(DBNull.Value) ? "None" : (string)l["Duty"]),
                        Employment = (l["Employment"].Equals(DBNull.Value) ? "Other" : (string)l["Employment"]),
                        Ident = string.Format("{0,-3}:{1,-6} {2}, {3}",
                          (string)l["EmpID"], alpha2num((string)l["EmpID"]),
                          ((string)l["Last Name"]).ToUpper(), (string)l["First Name"]),
                        Profile = "Crew"
                    };


            DataTable dt = libAlly.TO_DATA_TABLE(q.ToList());

            DataColumn pk1 = dt.Columns[0]; pk1.MaxLength = 8; pk1.AllowDBNull = false;
            dt.PrimaryKey = new DataColumn[] { pk1 };

            return dt;
        }


        static private DataTable build_gang_fulltime()
        {
            DataTable dtEmp = dacCache.GetEmployee();
            

            //var q = from tEmp in dtEmp.AsEnumerable()
            //        where !(bool)tEmp["Archive"] && !tEmp["Employment"].Equals(DBNull.Value) && (string)tEmp["Employment"] == "FullTime"
            //        //orderby tEmp["EmpID"]
            //        orderby (string)tEmp["Last Name"], (string)tEmp["First Name"]

            //        select new Gang
            //        {
            //            EmpID = (string)tEmp["EmpID"],
            //            EmpName = string.Format("{0}, {1}", ((string)tEmp["Last Name"]).ToUpper(), (string)tEmp["First Name"]),
            //            EmpNum = alpha2num((string)tEmp["EmpID"]),
            //            Active = true,
            //            Master = (tEmp["Master"].Equals(DBNull.Value) ? false : (bool)tEmp["Master"]),
            //            HomePhone = (tEmp["Main Phone"].Equals(DBNull.Value) ? "" : (string)tEmp["Main Phone"]),
            //            CellPhone = (tEmp["Cell Phone"].Equals(DBNull.Value) ? "" : (string)tEmp["Cell Phone"]),
            //            Duty = (string)tEmp["Duty"],                        
            //            Employment = (tEmp["Employment"].Equals(DBNull.Value) ? "Other" : (string)tEmp["Employment"]),
            //            Profile = "FullTime"
            //        };

            var q = from l in dtEmp.AsEnumerable()
                    //where !l["Employment"].Equals(DBNull.Value) && (string)l["Employment"] == "FullTime"
                    orderby l["Last Name"], l["First Name"]
                    select new
                    {
                        EmpID = (string)l["EmpId"],
                        EmpName = string.Format("{0}, {1}", ((string)l["Last Name"]).ToUpper(), (string)l["First Name"]),
                        EmpNum = alpha2num((string)l["EmpID"]),
                        //Active = (r == null ? false : (bool)r["Active"]),
                        Active = !(bool)l["Archive"] && !l["Employment"].Equals(DBNull.Value) && (string)l["Employment"] == "FullTime",
                        Master = (l["Master"].Equals(DBNull.Value) ? false : (bool)l["Master"]),
                        HomePhone = (l["Main Phone"].Equals(DBNull.Value) ? "" : (string)l["Main Phone"]),
                        CellPhone = (l["Cell Phone"].Equals(DBNull.Value) ? "" : (string)l["Cell Phone"]),
                        Duty = (l["Duty"].Equals(DBNull.Value) ? "None" : (string)l["Duty"]),
                        Employment = (l["Employment"].Equals(DBNull.Value) ? "Other" : (string)l["Employment"]),
                        //Archive = (bool)l["Archive"],
                        Profile = "FullTime"
                    };


            DataTable dt = libAlly.TO_DATA_TABLE(q.ToList());

            DataColumn pk1 = dt.Columns[0]; pk1.MaxLength = 8; pk1.AllowDBNull = false;
            dt.PrimaryKey = new DataColumn[] { pk1 };

            return dt;
        }


        static private DataTable build_gang_parttime()
        {
            DataTable dtEmp = dacCache.GetEmployee();

            //var q = from tEmp in dtEmp.AsEnumerable()
            //        where !tEmp["Employment"].Equals(DBNull.Value) && (string)tEmp["Employment"] == "PartTime"
            //        //orderby tEmp["EmpID"]
            //        orderby (string)tEmp["Last Name"], (string)tEmp["First Name"]
            //        select new Gang
            //        {
            //            EmpID = (string)tEmp["EmpID"],
            //            EmpName = string.Format("{0}, {1}", ((string)tEmp["Last Name"]).ToUpper(), (string)tEmp["First Name"]),
            //            EmpNum = alpha2num((string)tEmp["EmpID"]),
            //            HomePhone = (tEmp["Main Phone"].Equals(DBNull.Value) ? "" : (string)tEmp["Main Phone"]),
            //            CellPhone = (tEmp["Cell Phone"].Equals(DBNull.Value) ? "" : (string)tEmp["Cell Phone"]),
            //            Active = true,
            //            Duty = (string)tEmp["Duty"],
            //            Master = (tEmp["Master"].Equals(DBNull.Value) ? false : (bool)tEmp["Master"]),
            //            Employment = (tEmp["Employment"].Equals(DBNull.Value) ? "Other" : (string)tEmp["Employment"]),
            //            Profile = "PartTime"
            //        };
            var q = from l in dtEmp.AsEnumerable()
                    //where !l["Employment"].Equals(DBNull.Value) && (string)l["Employment"] == "FullTime"
                    orderby l["Last Name"], l["First Name"]
                    select new
                    {
                        EmpID = (string)l["EmpId"],
                        EmpName = string.Format("{0}, {1}", ((string)l["Last Name"]).ToUpper(), (string)l["First Name"]),
                        EmpNum = alpha2num((string)l["EmpID"]),
                        Active = !(bool)l["Archive"] && !l["Employment"].Equals(DBNull.Value) && (string)l["Employment"] == "PartTime",
                        Master = (l["Master"].Equals(DBNull.Value) ? false : (bool)l["Master"]),
                        HomePhone = (l["Main Phone"].Equals(DBNull.Value) ? "" : (string)l["Main Phone"]),
                        CellPhone = (l["Cell Phone"].Equals(DBNull.Value) ? "" : (string)l["Cell Phone"]),
                        Duty = (l["Duty"].Equals(DBNull.Value) ? "None" : (string)l["Duty"]),
                        Employment = (l["Employment"].Equals(DBNull.Value) ? "Other" : (string)l["Employment"]),
                        Ident = string.Format("{0,-3}:{1,-6} {2}, {3}",
                          (string)l["EmpID"], alpha2num((string)l["EmpID"]),
                          ((string)l["Last Name"]).ToUpper(), (string)l["First Name"]),
                        Profile = "PartTime"
                    };

            DataTable dt = libAlly.TO_DATA_TABLE(q.ToList());

            DataColumn pk1 = dt.Columns[0]; pk1.MaxLength = 8; pk1.AllowDBNull = false;
            dt.PrimaryKey = new DataColumn[] { pk1 };

            return dt;
        }


        static private DataTable build_gang_casual()
        {
            DataTable dtEmp = dacCache.GetEmployee();

            //var q = from tEmp in dtEmp.AsEnumerable()
            //        where !tEmp["Employment"].Equals(DBNull.Value) && (string)tEmp["Employment"] == "Casual"
            //        || (!tEmp["Employment"].Equals(DBNull.Value) && (string)tEmp["Employment"] == "Trainee")
            //        //orderby tEmp["EmpID"]
            //        orderby (string)tEmp["Last Name"], (string)tEmp["First Name"]
            //        select new Gang
            //        {
            //            EmpID = (string)tEmp["EmpID"],
            //            EmpName = string.Format("{0}, {1}", ((string)tEmp["Last Name"]).ToUpper(), (string)tEmp["First Name"]),
            //            EmpNum = alpha2num((string)tEmp["EmpID"]),
            //            HomePhone = (tEmp["Main Phone"].Equals(DBNull.Value) ? "" : (string)tEmp["Main Phone"]),
            //            CellPhone = (tEmp["Cell Phone"].Equals(DBNull.Value) ? "" : (string)tEmp["Cell Phone"]),
            //            Active = true,
            //            Duty = (string)tEmp["Duty"],
            //            Master = (tEmp["Master"].Equals(DBNull.Value) ? false : (bool)tEmp["Master"]),
            //            Employment = (tEmp["Employment"].Equals(DBNull.Value) ? "Other" : (string)tEmp["Employment"]),
            //            Profile = "Casual"
            //        };


            var q = from l in dtEmp.AsEnumerable()
                    //where !l["Employment"].Equals(DBNull.Value) && (string)l["Employment"] == "FullTime"
                    orderby l["Last Name"], l["First Name"]
                    select new
                    {
                        EmpID = (string)l["EmpId"],
                        EmpName = string.Format("{0}, {1}", ((string)l["Last Name"]).ToUpper(), (string)l["First Name"]),
                        EmpNum = alpha2num((string)l["EmpID"]),
                        Active = !(bool)l["Archive"] && !l["Employment"].Equals(DBNull.Value) && (string)l["Employment"] == "Casual",
                        Master = (l["Master"].Equals(DBNull.Value) ? false : (bool)l["Master"]),
                        HomePhone = (l["Main Phone"].Equals(DBNull.Value) ? "" : (string)l["Main Phone"]),
                        CellPhone = (l["Cell Phone"].Equals(DBNull.Value) ? "" : (string)l["Cell Phone"]),
                        Duty = (l["Duty"].Equals(DBNull.Value) ? "None" : (string)l["Duty"]),
                        Employment = (l["Employment"].Equals(DBNull.Value) ? "Other" : (string)l["Employment"]),
                        Ident = string.Format("{0,-3}:{1,-6} {2}, {3}",
                          (string)l["EmpID"], alpha2num((string)l["EmpID"]),
                          ((string)l["Last Name"]).ToUpper(), (string)l["First Name"]),
                        Profile = "Casual"
                    };

            DataTable dt = libAlly.TO_DATA_TABLE(q.ToList());

            DataColumn pk1 = dt.Columns[0]; pk1.MaxLength = 8; pk1.AllowDBNull = false;
            dt.PrimaryKey = new DataColumn[] { pk1 };

            return dt;
        }

        //GS181202 - add Other duty (Office & Other)
        static private DataTable build_gang_office()
        {
            DataTable dtEmp = dacCache.GetEmployee();

            //var q = from tEmp in dtEmp.AsEnumerable()
            //        where !(bool)tEmp["Archive"] && !tEmp["Duty"].Equals(DBNull.Value) && (string)tEmp["Duty"] == "Office"
            //        //(! tEmp["Employment"].Equals(DBNull.Value) && !tEmp["Duty"].Equals(DBNull.Value) && (string)tEmp["Duty"] == "Office")
            //        //&& (string)tEmp["Employment"] == "Other")
            //        //|| (! tEmp["Employment"].Equals(DBNull.Value) && (string)tEmp["Employment"] == "Trainee")
            //        //|| tEmp["Employment"].Equals(DBNull.Value)
            //        //orderby tEmp["EmpID"]
            //        orderby (string)tEmp["Last Name"], (string)tEmp["First Name"]
            //        select new Gang
            //        {
            //            EmpID = (string)tEmp["EmpID"],
            //            EmpName = string.Format("{0}, {1}", ((string)tEmp["Last Name"]).ToUpper(), (string)tEmp["First Name"]),
            //            EmpNum = alpha2num((string)tEmp["EmpID"]),
            //            HomePhone = (tEmp["Main Phone"].Equals(DBNull.Value) ? "" : (string)tEmp["Main Phone"]),
            //            CellPhone = (tEmp["Cell Phone"].Equals(DBNull.Value) ? "" : (string)tEmp["Cell Phone"]),
            //            Active = true,
            //            Duty = (string)tEmp["Duty"],
            //            Master = (tEmp["Master"].Equals(DBNull.Value) ? false : (bool)tEmp["Master"]),
            //            Employment = (tEmp["Employment"].Equals(DBNull.Value) ? "Other" : (string)tEmp["Employment"]),
            //            Profile = "Office"
            //        };

            var q = from l in dtEmp.AsEnumerable()
                    //where !l["Employment"].Equals(DBNull.Value) && (string)l["Employment"] == "FullTime"
                    orderby l["Last Name"], l["First Name"]
                    select new
                    {
                        EmpID = (string)l["EmpId"],
                        EmpName = string.Format("{0}, {1}", ((string)l["Last Name"]).ToUpper(), (string)l["First Name"]),
                        EmpNum = alpha2num((string)l["EmpID"]),
                        Active = !(bool)l["Archive"] && !l["Duty"].Equals(DBNull.Value) && 
                                    ((string)l["Duty"] == "Office" || (string)l["Duty"] == "Other"),
                        Master = (l["Master"].Equals(DBNull.Value) ? false : (bool)l["Master"]),
                        HomePhone = (l["Main Phone"].Equals(DBNull.Value) ? "" : (string)l["Main Phone"]),
                        CellPhone = (l["Cell Phone"].Equals(DBNull.Value) ? "" : (string)l["Cell Phone"]),
                        Duty = (l["Duty"].Equals(DBNull.Value) ? "None" : (string)l["Duty"]),
                        Employment = (l["Employment"].Equals(DBNull.Value) ? "Other" : (string)l["Employment"]),
                        Ident = string.Format("{0,-3}:{1,-6} {2}, {3}",
                          (string)l["EmpID"], alpha2num((string)l["EmpID"]),
                          ((string)l["Last Name"]).ToUpper(), (string)l["First Name"]),
                        Profile = "Office"
                    };

            DataTable dt = libAlly.TO_DATA_TABLE(q.ToList());

            DataColumn pk1 = dt.Columns[0]; pk1.MaxLength = 8; pk1.AllowDBNull = false;
            dt.PrimaryKey = new DataColumn[] { pk1 };

            return dt;
        }

        //GS181202 - add Other duty (Office & Other)
        static private DataTable xbuild_gang_office()  // so not used
        {
            DataTable dtEmp = dacCache.GetEmployee();

            //var q = from tEmp in dtEmp.AsEnumerable()
            //        where !(bool)tEmp["Archive"] && !tEmp["Duty"].Equals(DBNull.Value) && (string)tEmp["Duty"] == "Office"
            //        //(! tEmp["Employment"].Equals(DBNull.Value) && !tEmp["Duty"].Equals(DBNull.Value) && (string)tEmp["Duty"] == "Office")
            //        //&& (string)tEmp["Employment"] == "Other")
            //        //|| (! tEmp["Employment"].Equals(DBNull.Value) && (string)tEmp["Employment"] == "Trainee")
            //        //|| tEmp["Employment"].Equals(DBNull.Value)
            //        //orderby tEmp["EmpID"]
            //        orderby (string)tEmp["Last Name"], (string)tEmp["First Name"]
            //        select new Gang
            //        {
            //            EmpID = (string)tEmp["EmpID"],
            //            EmpName = string.Format("{0}, {1}", ((string)tEmp["Last Name"]).ToUpper(), (string)tEmp["First Name"]),
            //            EmpNum = alpha2num((string)tEmp["EmpID"]),
            //            HomePhone = (tEmp["Main Phone"].Equals(DBNull.Value) ? "" : (string)tEmp["Main Phone"]),
            //            CellPhone = (tEmp["Cell Phone"].Equals(DBNull.Value) ? "" : (string)tEmp["Cell Phone"]),
            //            Active = true,
            //            Duty = (string)tEmp["Duty"],
            //            Master = (tEmp["Master"].Equals(DBNull.Value) ? false : (bool)tEmp["Master"]),
            //            Employment = (tEmp["Employment"].Equals(DBNull.Value) ? "Other" : (string)tEmp["Employment"]),
            //            Profile = "Office"
            //        };

            var q = from l in dtEmp.AsEnumerable()
                    //where !l["Employment"].Equals(DBNull.Value) && (string)l["Employment"] == "FullTime"
                    orderby l["Last Name"], l["First Name"]
                    select new
                    {
                        EmpID = (string)l["EmpId"],
                        EmpName = string.Format("{0}, {1}", ((string)l["Last Name"]).ToUpper(), (string)l["First Name"]),
                        EmpNum = alpha2num((string)l["EmpID"]),
                        Active = !(bool)l["Archive"] && !l["Duty"].Equals(DBNull.Value) && (string)l["Duty"] == "Office",
                        Master = (l["Master"].Equals(DBNull.Value) ? false : (bool)l["Master"]),
                        HomePhone = (l["Main Phone"].Equals(DBNull.Value) ? "" : (string)l["Main Phone"]),
                        CellPhone = (l["Cell Phone"].Equals(DBNull.Value) ? "" : (string)l["Cell Phone"]),
                        Duty = (l["Duty"].Equals(DBNull.Value) ? "None" : (string)l["Duty"]),
                        Employment = (l["Employment"].Equals(DBNull.Value) ? "Other" : (string)l["Employment"]),
                        Ident = string.Format("{0,-3}:{1,-6} {2}, {3}",
                          (string)l["EmpID"], alpha2num((string)l["EmpID"]),
                          ((string)l["Last Name"]).ToUpper(), (string)l["First Name"]),
                        Profile = "Office"
                    };

            DataTable dt = libAlly.TO_DATA_TABLE(q.ToList());

            DataColumn pk1 = dt.Columns[0]; pk1.MaxLength = 8; pk1.AllowDBNull = false;
            dt.PrimaryKey = new DataColumn[] { pk1 };

            return dt;
        }


        static private DataTable build_gang_skipper()
        {
            DataTable dtEmp = dacCache.GetEmployee();

            //var q = from tEmp in dtEmp.AsEnumerable()
            //        where !(bool)tEmp["Archive"] && !tEmp["Duty"].Equals(DBNull.Value) && (string)tEmp["Duty"] == "Skipper"
            //        //&& (bool)tEmp["Master"] == true
            //        //orderby tEmp["EmpID"]
            //        orderby (string)tEmp["Last Name"], (string)tEmp["First Name"]
            //        select new Gang
            //        {
            //            EmpID = (string)tEmp["EmpID"],
            //            EmpName = string.Format("{0}, {1}", ((string)tEmp["Last Name"]).ToUpper(), (string)tEmp["First Name"]),
            //            EmpNum = alpha2num((string)tEmp["EmpID"]),
            //            HomePhone = (tEmp["Main Phone"].Equals(DBNull.Value) ? "" : (string)tEmp["Main Phone"]),
            //            CellPhone = (tEmp["Cell Phone"].Equals(DBNull.Value) ? "" : (string)tEmp["Cell Phone"]),
            //            Active = true,
            //            Duty = (string)tEmp["Duty"],
            //            Master = (tEmp["Master"].Equals(DBNull.Value) ? false : (bool)tEmp["Master"]),
            //            Employment = (tEmp["Employment"].Equals(DBNull.Value) ? "Other" : (string)tEmp["Employment"]),
            //            Ident = string.Format("{0,-3}:{1,-6} {2}, {3}",
            //              (string)tEmp["EmpID"], alpha2num((string)tEmp["EmpID"]),
            //              ((string)tEmp["Last Name"]).ToUpper(), (string)tEmp["First Name"]),
            //            Profile = "Master"
            //        };

            var q = from l in dtEmp.AsEnumerable()
                    //where !l["Employment"].Equals(DBNull.Value) && (string)l["Employment"] == "FullTime"
                    orderby l["Last Name"], l["First Name"]
                    select new
                    {
                        EmpID = (string)l["EmpId"],
                        EmpName = string.Format("{0}, {1}", ((string)l["Last Name"]).ToUpper(), (string)l["First Name"]),
                        EmpNum = alpha2num((string)l["EmpID"]),
                        Active = !(bool)l["Archive"] && !l["Duty"].Equals(DBNull.Value) && (string)l["Duty"] == "Master",
                        Master = (l["Master"].Equals(DBNull.Value) ? false : (bool)l["Master"]),
                        HomePhone = (l["Main Phone"].Equals(DBNull.Value) ? "" : (string)l["Main Phone"]),
                        CellPhone = (l["Cell Phone"].Equals(DBNull.Value) ? "" : (string)l["Cell Phone"]),
                        Duty = (l["Duty"].Equals(DBNull.Value) ? "None" : (string)l["Duty"]),
                        Employment = (l["Employment"].Equals(DBNull.Value) ? "Other" : (string)l["Employment"]),
                        Ident = string.Format("{0,-3}:{1,-6} {2}, {3}",
                          (string)l["EmpID"], alpha2num((string)l["EmpID"]),
                          ((string)l["Last Name"]).ToUpper(), (string)l["First Name"]),
                        Profile = "Casual"
                    };

            DataTable dt = libAlly.TO_DATA_TABLE(q.ToList());

            DataColumn pk1 = dt.Columns[0]; pk1.MaxLength = 8; pk1.AllowDBNull = false;
            dt.PrimaryKey = new DataColumn[] { pk1 };

            return dt;
        }


        static private DataTable build_gang_deckhand()
        {
            DataTable dtEmp = dacCache.GetEmployee();

            //var q = from tEmp in dtEmp.AsEnumerable()
            //        where !(bool)tEmp["Archive"] && !tEmp["Duty"].Equals(DBNull.Value) && (string)tEmp["Duty"] == "Deckhand"
            //        //&& (bool)tEmp["Master"] == false 
            //        //orderby tEmp["EmpID"]
            //        orderby (string)tEmp["Last Name"], (string)tEmp["First Name"]
            //        select new Gang
            //        {
            //            EmpID = (string)tEmp["EmpID"],
            //            EmpName = string.Format("{0}, {1}", ((string)tEmp["Last Name"]).ToUpper(), (string)tEmp["First Name"]),
            //            EmpNum = alpha2num((string)tEmp["EmpID"]),
            //            HomePhone = (tEmp["Main Phone"].Equals(DBNull.Value) ? "" : (string)tEmp["Main Phone"]),
            //            CellPhone = (tEmp["Cell Phone"].Equals(DBNull.Value) ? "" : (string)tEmp["Cell Phone"]),
            //            Active = true,
            //            Duty = (string)tEmp["Duty"],
            //            Master = (tEmp["Master"].Equals(DBNull.Value) ? false : (bool)tEmp["Master"]),
            //            Employment = (tEmp["Employment"].Equals(DBNull.Value) ? "Other" : (string)tEmp["Employment"]),
            //            Ident = string.Format("{0,-3}:{1,-6} {2}, {3}",
            //                (string)tEmp["EmpID"], alpha2num((string)tEmp["EmpID"]),
            //                ((string)tEmp["Last Name"]).ToUpper(), (string)tEmp["First Name"]),
            //            Profile = "DeckHand"
            //        };

            var q = from l in dtEmp.AsEnumerable()
                    //where !l["Employment"].Equals(DBNull.Value) && (string)l["Employment"] == "FullTime"
                    orderby l["Last Name"], l["First Name"]
                    select new
                    {
                        EmpID = (string)l["EmpId"],
                        EmpName = string.Format("{0}, {1}", ((string)l["Last Name"]).ToUpper(), (string)l["First Name"]),
                        EmpNum = alpha2num((string)l["EmpID"]),
                        Active = !(bool)l["Archive"] && !l["Duty"].Equals(DBNull.Value) && (string)l["Duty"] == "Deckhand",
                        Master = (l["Master"].Equals(DBNull.Value) ? false : (bool)l["Master"]),
                        HomePhone = (l["Main Phone"].Equals(DBNull.Value) ? "" : (string)l["Main Phone"]),
                        CellPhone = (l["Cell Phone"].Equals(DBNull.Value) ? "" : (string)l["Cell Phone"]),
                        Duty = (l["Duty"].Equals(DBNull.Value) ? "None" : (string)l["Duty"]),
                        Employment = (l["Employment"].Equals(DBNull.Value) ? "Other" : (string)l["Employment"]),
                        Ident = string.Format("{0,-3}:{1,-6} {2}, {3}",
                          (string)l["EmpID"], alpha2num((string)l["EmpID"]),
                          ((string)l["Last Name"]).ToUpper(), (string)l["First Name"]),
                        Profile = "Deckhand"
                    };


            DataTable dt = libAlly.TO_DATA_TABLE(q.ToList());

            DataColumn pk1 = dt.Columns[0]; pk1.MaxLength = 8; pk1.AllowDBNull = false;
            dt.PrimaryKey = new DataColumn[] { pk1 };

            return dt;
        }



        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        static public DataTable GetView(string qry_name, DateTime start_week, int days)
        {
            
            return get_view(qry_name, true, start_week, days);
        }

        static public DataTable GetView(string qry_name)
        {
            return get_view(qry_name, true, DateTime.Now, 0);
        }


        static public DataTable GetView(string qry_name, bool gang_set_)
        {
            return get_view(qry_name, gang_set_, DateTime.Now, 0);
        }

        //static public DataTable GetView(string qry_name, DateTime start_week, int days)
        static private DataTable get_view(string qry_name, bool gang_set_, DateTime start_week, int days)
        {
            if (_requery)
            {
                //if (_dt_gang != null)  _dt_gang.Dispose();
                _requery = false;
                _dt_gang = null;
            }


            if (gang_set_ && _qry_gang.Equals(qry_name) && _dt_gang != null) return _dt_gang;


            DataTable dt;
            _qry_gang = qry_name;

            if (qry_name.Equals("FullTime")) dt = build_gang_fulltime();
            else if (qry_name.Equals("Archive")) dt = build_gang_all_archive();
            //else if (qry_name.Equals("PartTime")) build_gang_parttime();
            //else if (qry_name.Equals("PartTime")) build_gang_offwork();
            else if (qry_name.Equals("Casual")) dt = build_gang_casual();
            else if (qry_name.Equals("Office")) dt = build_gang_office();
            //else if (qry_name.Equals("Trainee")) build_gang_trainee();
            else if (qry_name.Equals("Master")) dt = build_gang_skipper();
            else if (qry_name.Equals("Deckhand")) dt = build_gang_deckhand();
            //else if (qry_name.Equals("Working")) build_gang_crew();
            //else if (qry_name.Equals("Working")) dt = build_gang_working();
            else if (qry_name.Equals("Profile")) dt = build_gang_working();
            else if (qry_name.Equals("Crew")) dt = build_gang_crew();
            else if (qry_name.Equals("All")) dt = build_gang_all();
            else dt = build_gang_vessel(qry_name, start_week, days);


            if (!gang_set_) return dt;

            dt.AcceptChanges();
            dacCache.SetGang(dt);
            _dt_gang = dt;

            return _dt_gang;
        }


    /*******************************************************************************************************************\
     *                                                                                                                 *
    \*******************************************************************************************************************/

    }
        
    /*******************************************************************************************************************\
     *                                                                                                                 *
    \*******************************************************************************************************************/

    class Gang
    {
        public string EmpID { get; set; }
        public string EmpName { get; set; }
        public string EmpNum { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public bool Active { get; set; }
        public string Duty { get; set; }
        public bool Master { get; set; }
        public string Employment { get; set; }
        public string Ident { get; set; }
        public string Profile { get; set; }
    }
}



        //static private void old_build_gang_deckhand()
        //{
        //    DataTable dtPk = dacCache.GetEmployee();
        //    DataTable dtFk = dacCache.GetGang();

        //    var q = from tFk in dtFk.AsEnumerable()
        //            join tPk in dtPk.AsEnumerable()
        //            on (string)tFk["EmpID"] equals (string)tPk["EmpID"]
        //            where (bool)tPk["Master"] == false
        //            select new
        //            {
        //                EmpID = (string)tFk["EmpID"],
        //                EmpName = (string)tFk["EmpName"],
        //                HomePhone = (tFk["HomePhone"].Equals(DBNull.Value) ? "" : (string)tFk["HomePhone"]),
        //                CellPhone = (tFk["CellPhone"].Equals(DBNull.Value) ? "" : (string)tFk["CellPhone"]),
        //                Active = (bool)tFk["Active"],
        //                Duty = (string)tPk["Duty"],
        //                Master = (tPk["Master"].Equals(DBNull.Value) ? false : (bool)tPk["Master"])
        //            };

        //    //DataTable dt = q.CopyToDataTable();
        //    DataTable dt = ToDataTable(q.ToList());

        //    _dt_gang = dt;
        //}


