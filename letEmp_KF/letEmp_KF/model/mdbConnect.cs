using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;
using System.Data.OleDb;


namespace letEmp_KF
{
    internal sealed class mdbConnect
    {
        public const string DBO = "";

        static private string _name = "mdb.";


        /*******************************************************************************************************************\
        *                                                                                                                 *
       \*******************************************************************************************************************/

        static private string _schlag1 = "";
        static private string _schlag2 = "User ID=sa;Password=comp@q2000";


        static private string build_con()
        {
            string strCon = ConfigurationManager.ConnectionStrings[_name].ConnectionString;

            if (!_name.Equals("mdb."))
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

        static public OleDbConnection Connect()
        {
            OleDbConnection con = null;

            try
            {
                string strCon = build_con();

                con = new OleDbConnection(strCon);
                con.Open();
            }
            catch (Exception ex)
            {
                errDash.Fail(ex);
            }

            return con;
        }
    }


}
