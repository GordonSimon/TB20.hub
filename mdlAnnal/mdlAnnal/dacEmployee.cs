using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;


namespace mdlAnnal
{
    public class dacEmployee
    {
        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private static string SqlSelEmployeeWhereDuty =
            @"SELECT * FROM [Employee] where [Duty] = '{0}'";


        public static DataTable GetDT(string dbo, string duty)
        {
            string sql = string.Format(SqlSelEmployeeWhereDuty, duty);
            return sqlConnect.dabFill(dbo, sql);
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private static string SqlSelEmployee =
            @"SELECT * FROM [Employee]";


        public static DataTable GetDT(string dbo)
        {
            string sql = SqlSelEmployee;
            return sqlConnect.dabFill(dbo, sql);
        }

    }

}
