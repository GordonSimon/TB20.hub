using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Windows.Forms;


namespace mdlAnnal
{
    public class dacTimebook
    {
        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private static string SqlSelTimebookWhereDuty =
            @"SELECT * FROM [Timebook] tT
                INNER JOIN [Employee] tE ON tT.Empid=tE.EmpId
            WHERE tE.Duty = '{0}'
            order by tT.[EmpId], tT.[ID]";


        public static DataTable GetDT(string dbo, string duty)
        {
            string sql = string.Format(SqlSelTimebookWhereDuty, duty);

            DataTable dt = sqlConnect.dabFill(dbo, sql);
            //dt.PrimaryKey = new DataColumn[] { dt.Columns["ID"] };

            return dt;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private static string SqlSelTimebook =
            @"SELECT * FROM [Timebook] tT
                INNER JOIN [Employee] tE ON tT.Empid=tE.EmpId
            order by tT.[EmpId], tT.[ID]";

        public static DataTable GetDTCrew(string dbo)
        {
            string sql = SqlSelTimebook;

            DataTable dt = sqlConnect.dabFill(dbo, sql);
            //dt.PrimaryKey = new DataColumn[] { dt.Columns["ID"] };
            
            return dt;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

//        private static string SqlUpdTimebook =
//            @"UPDATE [Timebook] SET
//            ToffCode={0},
//            LogHours=[1}, LogOver={2}, LogVessel={3}, LogShift={4}, Resp={5}, LogNote={6}
//            UpdateDate={7}, UserAudit={8}
//            WHERE BookDate={9} AND EmpID={10} AND LogShift={11}";


//        public static DataTable vwUpdTimebook(string dbo, DateTime bookdate, string empid, int shift,
//            string toff, string vessel, Decimal hours, Decimal over, Decimal over1,
//            string resp, string paycode)
//        {
//            string sql = SqlUpdTimebook;
//            return sqlConnect.dabFill(dbo, sql);
//        }


        private static string SqlUpdTimebook =
            @"UPDATE [Timebook] SET
            LogHours={0}, LogOver={1}, LogOver1={2},
            PayCode='{3}',
            UpdateDate='{4}', UserAudit='{5}'
            WHERE ID={6}";
            //WHERE BookDate='{6}' AND EmpID='{7}' AND LogShift={8}";

        private static string SqlInsTimebook =
            @"INSERT INTO [Timebook]
            ([BookDate]
            ,[EmpID]
            ,[EmpName]
            ,[LogHours]
            ,[LogOver]
            ,[LogShift]
            ,[PayCode]
            ,[LogOver1])
            VALUES (
            '{0}', '{1}', '{2}', {3}, {4}, {5}, '{6}', {7}
            )";

        public static void vwAddTimebook(string dbo, int id, DateTime bookdate, string empid, int shift,
            string emp_name,
            Decimal hours, Decimal over, Decimal over1,
            string paycode, DateTime update_date, string user)
        {
            string sql;
            if (id == 0)
                sql = string.Format(SqlInsTimebook, bookdate.Date, empid, emp_name,  hours, over, shift, paycode, over1);
            else
                sql = string.Format(SqlUpdTimebook, hours, over, over1, paycode, update_date.Date, user, id);
            
            int rows_changed = sqlConnect.dabNonQuery(dbo, sql);

            sql += string.Format("\n {0} Records updated or added.", rows_changed);
            MessageBox.Show(sql, "Database Update/Insert Status");
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private static string SqlDelTimebook =
            @"DELETE [Timebook]
            WHERE ID={0}";

        public static void vwDelTimebook(string dbo, string id, DateTime update_date, string user)
        {
            string sql = string.Format(SqlDelTimebook, id);

            int rows_changed = sqlConnect.dabNonQuery(dbo, sql);

            sql += string.Format("\n {0} Records deleted.", rows_changed);
            MessageBox.Show(sql, "Database Delete Status");
        }



        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public static DataTable vwInsTimebook(string dbo, DateTime bookdate, string empid, int shift,
            Decimal hours, Decimal over, Decimal over1, string paycode)
        {
            string msg = string.Format("emp_id [{0}] h[{1}] o[{2}] [{3}] [{4}] [{5}]", empid, hours, over, over1, shift, paycode);
            MessageBox.Show(msg);


            return null;
        }
    }
}
