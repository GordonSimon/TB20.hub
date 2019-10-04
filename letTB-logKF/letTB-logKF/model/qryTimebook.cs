using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;


namespace letTB_logKF
{
    public sealed class qryTimebook
    {


        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        static public DataTable qBoats(DataTable dt, DateTime d)
        {
            try
            {
                var q = (from tbl in dt.AsEnumerable()
                        where ! tbl["LogVessel"].Equals(DBNull.Value)
                              && DateTime.Compare(d, ((DateTime)tbl["Bookdate"]).Date) == 0                              
                        group tbl by tbl["LogVessel"] into g
                        orderby g.Key
                        //            where ((string)tH["EmpId"]).Equals(emp_id)
                        //                && DateTime.Compare(beg, ((DateTime)tH["Bookdate"]).Date) == -1
                        //                && DateTime.Compare(end, ((DateTime)tH["Bookdate"]).Date) == 1
                        //from row in g
                        select new
                        {
                            //                EmpId = (string)tTime["EmpId"],
                            //                EmpName = (string)tTime["EmpName"],
                            //                Bookdate = (DateTime)tTime["Bookdate"],
                            //                LogHours = (Decimal)tTime["LogHours"],
                            //                LogOver = (Decimal)tTime["LogOver"],
                            //                LogVessel = (tTime["LogVessel"].Equals(DBNull.Value) ? "" : (string)tTime["LogVessel"]),
                            LogVessel = (string)g.Key
                            //                Shift = (tTime["LogShift"].Equals(DBNull.Value) ? -1 : (int)tTime["LogShift"]),
                            //                ShiftHour = (tTime["LogHour"].Equals(DBNull.Value) ? "<>" : (string)tTime["LogHour"]),
                            //                ToffCode = (tTime["ToffCode"].Equals(DBNull.Value) ? "" : (string)tTime["ToffCode"]),                            
                            //                Resp = (tTime["Resp"].Equals(DBNull.Value) ? "" : (string)tTime["Resp"]),                           
                            //                LogNote = (tTime["LogNote"].Equals(DBNull.Value) ? "" : (string)tTime["LogNote"])
                        }
                        ).Distinct();

                return libModel.TO_DATA_TABLE(q.ToList());
            }
            catch (Exception ex)
            {
                errDash.Fail(System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }

            return null;
        }


        static public DataTable qBoats(DataTable dt, DateTime d, DateTime f)
        {
            DateTime ad = d.AddDays(-1);
            DateTime af = f.AddDays(1);

            try
            {
                var q = (from tbl in dt.AsEnumerable()
                         where !tbl["LogVessel"].Equals(DBNull.Value)
                               && DateTime.Compare(ad, ((DateTime)tbl["Bookdate"]).Date) == -1
                               && DateTime.Compare(f, ((DateTime)tbl["Bookdate"]).Date) == 1
                         group tbl by tbl["LogVessel"] into g
                         orderby g.Key
                         //            where ((string)tH["EmpId"]).Equals(emp_id)
                         //                && DateTime.Compare(beg, ((DateTime)tH["Bookdate"]).Date) == -1
                         //                && DateTime.Compare(end, ((DateTime)tH["Bookdate"]).Date) == 1
                         //from row in g
                         select new
                         {
                             //                EmpId = (string)tTime["EmpId"],
                             //                EmpName = (string)tTime["EmpName"],
                             //                Bookdate = (DateTime)tTime["Bookdate"],
                             //                LogHours = (Decimal)tTime["LogHours"],
                             //                LogOver = (Decimal)tTime["LogOver"],
                             //                LogVessel = (tTime["LogVessel"].Equals(DBNull.Value) ? "" : (string)tTime["LogVessel"]),
                             LogVessel = (string)g.Key
                             //                Shift = (tTime["LogShift"].Equals(DBNull.Value) ? -1 : (int)tTime["LogShift"]),
                             //                ShiftHour = (tTime["LogHour"].Equals(DBNull.Value) ? "<>" : (string)tTime["LogHour"]),
                             //                ToffCode = (tTime["ToffCode"].Equals(DBNull.Value) ? "" : (string)tTime["ToffCode"]),                            
                             //                Resp = (tTime["Resp"].Equals(DBNull.Value) ? "" : (string)tTime["Resp"]),                           
                             //                LogNote = (tTime["LogNote"].Equals(DBNull.Value) ? "" : (string)tTime["LogNote"])
                         }
                        ).Distinct();

                return libModel.TO_DATA_TABLE(q.ToList());
            }
            catch (Exception ex)
            {
                errDash.Fail(System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }

            return null;
        }


        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        static public DataTable qShifts(DataTable dt, DateTime d, string boat)
        {
            try
            {
                var q = from tbl in dt.AsEnumerable()
                        where !tbl["LogVessel"].Equals(DBNull.Value)
                              && tbl["LogVessel"].Equals(boat)
                              && DateTime.Compare(d, ((DateTime)tbl["Bookdate"]).Date) == 0
                        group tbl by new { v = tbl["LogVessel"], s = tbl["LogShift"] } into g
                        //orderby g.Key
                        select new
                        {
                            LogVessel = (string)g.Key.v,
                            LogShift = (int)g.Key.s
                        };


                return libModel.TO_DATA_TABLE(q.ToList());
            }
            catch (Exception ex)
            {
                errDash.Fail(System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }

            return null;
        }


        static public DataTable qShifts(DataTable dt, DateTime d)
        {
            try
            {
                var q = from tbl in dt.AsEnumerable()
                         where !tbl["LogVessel"].Equals(DBNull.Value)
                               && DateTime.Compare(d, ((DateTime)tbl["Bookdate"]).Date) == 0
                         group tbl by new { v = tbl["LogVessel"], s = tbl["LogShift"] } into g
                         //orderby g.Key
                         select new
                         {
                             LogVessel = (string)g.Key.v,
                             LogShift = (int)g.Key.s
                         };
                                        

                return libModel.TO_DATA_TABLE(q.ToList());
            }
            catch (Exception ex)
            {
                errDash.Fail(System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }

            return null;
        }


        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        static public DataTable qCrew(DataTable dt, DateTime d)
        {
            try
            {
                var q = (from tbl in dt.AsEnumerable()
                         where !tbl["Duty"].Equals("Office")
                                && DateTime.Compare(d, ((DateTime)tbl["Bookdate"]).Date) == 0
                                && (!tbl["LogVessel"].Equals(DBNull.Value) || (int)tbl["LogShift"] >= 10)
                         //where !tbl["LogVessel"].Equals(DBNull.Value)
                         //      && ! tbl["Duty"].Equals("Office")
                         //      && DateTime.Compare(d, ((DateTime)tbl["Bookdate"]).Date) == 0
                         //group tbl by tbl["EmpName"] into g
                         group tbl by tbl["EmpId"] into g
                         orderby g.Key
                         select new
                         {
                             EmpId = (string)g.Key,
                             EmpName = (string)(g.Last())["EmpName"]
                             //EmpName = (string)g.Key
                         }
                        ).Distinct();

                return libModel.TO_DATA_TABLE(q.ToList());
            }
            catch (Exception ex)
            {
                errDash.Fail(System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }

            return null;
        }


        static public DataTable qCrew(DataTable dt, DateTime d, DateTime f)
        {
            DateTime ad = d.AddDays(-1);
            DateTime af = f.AddDays(1);

            try
            {
                var q = (from tbl in dt.AsEnumerable()
                         where !tbl["Duty"].Equals("Office")
                               && DateTime.Compare(ad, ((DateTime)tbl["Bookdate"]).Date) == -1
                               && DateTime.Compare(f, ((DateTime)tbl["Bookdate"]).Date) == 1
                               && (!tbl["LogVessel"].Equals(DBNull.Value) || (int)tbl["LogShift"] >= 10)

                         //where !tbl["LogVessel"].Equals(DBNull.Value)
                         //      && !tbl["Duty"].Equals("Office")
                         //      && DateTime.Compare(ad, ((DateTime)tbl["Bookdate"]).Date) == -1
                         //      && DateTime.Compare(f, ((DateTime)tbl["Bookdate"]).Date) == 1
                               
                         //group tbl by tbl["EmpName"] into g
                         group tbl by tbl["EmpId"] into g
                         orderby g.Key
                         select new
                         {
                             EmpId = (string)g.Key,
                             EmpName = (string)(g.Last())["EmpName"]
                             //EmpName = (string)g.Key
                         }
                        ).Distinct();

                return libModel.TO_DATA_TABLE(q.ToList());
            }
            catch (Exception ex)
            {
                errDash.Fail(System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }

            return null;
        }

        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        static public DataTable qStaff(DataTable dt, DateTime d)
        {
            try
            {
                var q = (from tbl in dt.AsEnumerable()
                         where 
                               (tbl["Duty"].Equals("Office") || tbl["Duty"].Equals("Other"))
                               && DateTime.Compare(d, ((DateTime)tbl["Bookdate"]).Date) == 0
                               && !tbl["LogHours"].Equals(DBNull.Value)
                               && (decimal)tbl["LogHours"] > 0.0M
                         //group tbl by tbl["EmpName"] into g
                         group tbl by tbl["EmpId"] into g
                         orderby g.Key
                         select new
                         {
                             EmpId = (string)g.Key,
                             EmpName = (string)(g.Last())["EmpName"]
                             //EmpName = (string)g.Key
                         }
                        ).Distinct();

                return libModel.TO_DATA_TABLE(q.ToList());
            }
            catch (Exception ex)
            {
                errDash.Fail(System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }

            return null;
        }


        static public DataTable qxStaff(DataTable dt, DateTime d)
        {
            try
            {
                var q = (from tbl in dt.AsEnumerable()
                         where tbl["LogVessel"].Equals(DBNull.Value)                                
                               && tbl["Duty"].Equals("Office")
                               && DateTime.Compare(d, ((DateTime)tbl["Bookdate"]).Date) == 0
                               && !tbl["LogHours"].Equals(DBNull.Value)
                               && (decimal)tbl["LogHours"] > 0.0M
                         //group tbl by tbl["EmpName"] into g
                         group tbl by tbl["EmpId"] into g
                         orderby g.Key
                         select new
                         {
                             EmpId = (string)g.Key,
                             EmpName = (string)(g.Last())["EmpName"]
                             //EmpName = (string)g.Key
                         }
                        ).Distinct();

                return libModel.TO_DATA_TABLE(q.ToList());
            }
            catch (Exception ex)
            {
                errDash.Fail(System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }

            return null;
        }


        static public DataTable qxStaff(DataTable dt, DateTime d, DateTime f)
        {
            DateTime ad = d.AddDays(-1);
            DateTime af = f.AddDays(1);

            try
            {
                var q = (from tbl in dt.AsEnumerable()
                         where tbl["LogVessel"].Equals(DBNull.Value)
                               && tbl["Duty"].Equals("Office")
                               && DateTime.Compare(ad, ((DateTime)tbl["Bookdate"]).Date) == -1
                               && DateTime.Compare(f, ((DateTime)tbl["Bookdate"]).Date) == 1
                               && !tbl["LogHours"].Equals(DBNull.Value)
                               && (decimal)tbl["LogHours"] > 0.0M

                         //group tbl by tbl["EmpName"] into g
                         group tbl by tbl["EmpId"] into g
                         orderby g.Key
                         select new
                         {
                             EmpId = (string)g.Key,
                             EmpName = (string)(g.Last())["EmpName"]
                             //EmpName = (string)g.Key
                         }
                        ).Distinct();

                return libModel.TO_DATA_TABLE(q.ToList());
            }
            catch (Exception ex)
            {
                errDash.Fail(System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }

            return null;
        }


        static public DataTable qStaff(DataTable dt, DateTime d, DateTime f)
        {
            DateTime ad = d.AddDays(-1);
            DateTime af = f.AddDays(1);

            try
            {
                var q = (from tbl in dt.AsEnumerable()
                         where 
                               (tbl["Duty"].Equals("Office") || tbl["Duty"].Equals("Other"))
                               && DateTime.Compare(ad, ((DateTime)tbl["Bookdate"]).Date) == -1
                               && DateTime.Compare(f, ((DateTime)tbl["Bookdate"]).Date) == 1
                               && !tbl["LogHours"].Equals(DBNull.Value)
                               && (decimal)tbl["LogHours"] > 0.0M

                         //group tbl by tbl["EmpName"] into g
                         group tbl by tbl["EmpId"] into g
                         orderby g.Key
                         select new
                         {
                             EmpId = (string)g.Key,
                             EmpName = (string)(g.Last())["EmpName"]
                             //EmpName = (string)g.Key
                         }
                        ).Distinct();

                return libModel.TO_DATA_TABLE(q.ToList());
            }
            catch (Exception ex)
            {
                errDash.Fail(System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }

            return null;
        }


        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        static public DataTable qDays(DataTable dt)
        {
            //DateTime beg = new DateTime(m.Year, m.Month, 1);
            //DateTime end = beg.AddMonths(1).AddDays(-1);

            try
            {
                var q = from tbl in dt.AsEnumerable()
                        where !tbl["LogVessel"].Equals(DBNull.Value)
                        //                && DateTime.Compare(beg, ((DateTime)tbl["Bookdate"]).Date) == -1
                        //                && DateTime.Compare(end, ((DateTime)tbl["Bookdate"]).Date) == 1
                        group tbl by tbl["Bookdate"] into g
                        orderby g.Key
                        select new
                        {
                            Bookdate = (DateTime)g.Key
                        };
                        

                return libModel.TO_DATA_TABLE(q.ToList());
            }
            catch (Exception ex)
            {
                errDash.Fail(System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }

            return null;
        }
    }
}
