using System;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;

using System.Windows.Forms;



/// <summary>
/// Summary description for dacCache
/// </summary>

namespace mdlAllyKE
{
    public sealed class dacCache
    {
        static public DateTime StartDate = DateTime.Now;

        //static public string GangID { get; set; }
        static public string LoginError { get; set; }


        static private DateTime _ref_week { get; set; }
        static private DataSet _ds = new DataSet("Cache");
        
        static DataTable _dt_gang;
        static DataTable _dt_timebook;
        static DataTable _dt_employee;
        static DataTable _dt_vessel;
        static DataTable _dt_shift;
        

        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        static private DateTime week_starter()
        {            
            DateTime d = StartDate;

            int offset = d.DayOfWeek - DayOfWeek.Monday;
            offset = (offset == -1 ? 6 : offset);

            DateTime lastMonday = d.AddDays(-offset);

            return lastMonday;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/
      
        static public DataTable GetGang()
        {
            return _dt_gang;
        }

        static public void SetGang(DataTable dt)
        {
            _dt_gang = dt;
        }

        static public DataTable GetEmployee()
        {
            return _dt_employee;
        }


        static public DataTable GetVessel()
        {
            return _dt_vessel;
        }


        static public DataTable GetTimebook()
        {
            return _dt_timebook;
        }


        static public DataTable GetShift()
        {
            return _dt_shift;
        }


        static public void PutTimebook()
        {
            DateTime ref_week = _ref_week;
            RefreshTimebook(ref_week, true);
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        static public void RefreshEmployee()
        {
            _dt_employee = dacEmployees.GetDT();
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \***************************************************************************************************************/
                
        static public void RefreshTimebook(DateTime new_ref, bool force_reload_)
        {
            DateTime month = new_ref;            
            DateTime first_day_in_month = new DateTime(month.Year, month.Month, 1);
            DateTime last_month = first_day_in_month.AddMonths(-1);
            DateTime next_month = first_day_in_month.AddMonths(1);
                        
            
            DateTime start = _ref_week.AddDays(-7).Date;
            DateTime finish = _ref_week.AddDays(5 * 7).Date;

            
            if (!force_reload_) // don't reload and put new_ref to _ref_week if date is already in range.
            {
                if (start.CompareTo(new_ref.AddDays(-7).Date) == -1 &&
                    finish.CompareTo(new_ref.AddDays(5 * 7).Date) == 1) return;
            //    //MessageBox.Show(string.Format("s{0} f{1} r{2}", start.ToString(),
            //    //    finish.ToString(), new_ref.ToString()) );
            }
            _ref_week = new_ref;

            _dt_timebook.Dispose();
            //_dt_timebook = dacTimebook.GetDT(_ref_week.AddDays(-7), 6 * 7);
            int count_days = (next_month.AddMonths(1).AddDays(-1) - last_month).Days;
            _dt_timebook = dacTimebook.GetDT(last_month, count_days);
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/
  
        static public DataSet GetLogin()
        {
            try
            {
                _ds.Tables.Add(dacLogin.GetDS().Tables[0].Clone());
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error (GetLogin) : {0}", ex.Message);

                //MessageBox.Show(string.Format("Error (GetLogin) : {0}", ex.Message));

                LoginError = msg;

                return null;
            }

            LoginError = null;

            return _ds;
        }


        static public void GetCache()
        {
            try
            {
                _ref_week = week_starter();

                //_ds.Tables.Add(dacVessels.GetDS().Tables[0].Clone());
                _dt_vessel = dacVessels.GetDT();
                //_ds.Tables.Add(dacEmployees.GetDS().Tables[0].Clone());
                _dt_employee = dacEmployees.GetDT();
                _dt_shift = dacShift.GetDT();
                _ds.Tables.Add(dacToff.GetDS().Tables[0].Clone());

                //_dt_gang = dacGang.GetDT();
                _dt_timebook = dacTimebook.GetDT(_ref_week.AddDays(-7), 5*7);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error (GetCache) : {0}", ex.Message));
            }
        }


        static public string GetDBInfo()
        {
            return ConnectionManager.DBO;
        }
    }
}
