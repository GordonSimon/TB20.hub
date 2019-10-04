using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;


namespace mdlAnnal
{
    public class dacToff
    {
        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private static string SqlSelToff =
            @"SELECT * FROM [Toff]";


        public static DataTable GetDT(string dbo)
        {
            string sql = SqlSelToff;
            return sqlConnect.dabFill(dbo, sql);
        }

    }
}
