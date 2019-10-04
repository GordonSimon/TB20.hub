using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;
using System.Data.SqlClient;


namespace letTB_logKF
{
    internal sealed class sqlConnect
    {
        public const string DBO = "dbo.";

        static private string _name = "dbo.";


        /*******************************************************************************************************************\
        *                                                                                                                 *
       \*******************************************************************************************************************/

        static private string _schlag1 = "";
        static private string _schlag2 = "User ID=sa;Password=comp@q2000";


        static private string build_con()
        {
            string strCon = ConfigurationManager.ConnectionStrings[_name].ConnectionString;

            if (!_name.Equals("dbo."))
                strCon += (_name.Equals("SCHLAG") ? _schlag1 : _schlag2);

            return strCon;
        }

        static public string GetConnection()
        {
            return ConfigurationManager.ConnectionStrings[_name].ConnectionString;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        static public SqlConnection Connect()
        {
            SqlConnection con = null;

            try
            {
                string strCon = build_con();

                con = new SqlConnection(strCon);
                con.Open();
            }
            catch (Exception ex)
            {
                errDash.Fail(System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }

            return con;
        }
    }


}
