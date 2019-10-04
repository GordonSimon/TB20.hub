using System;
using System.Collections.Generic;

using System.Configuration;
using System.Data.SqlClient;



/// <summary>
/// Summary description for ConnectionManager
/// </summary>


namespace mdlAllyKE
{
    internal sealed class ConnectionManager
    {
        public const string DBO = "dbo.";
        //public const string DBO = "Access.";

        public static SqlConnection GetConnection()
        {
            string strCon = ConfigurationManager.ConnectionStrings[DBO].ConnectionString;
            
            SqlConnection con = new SqlConnection(strCon);
            con.Open();

            return con;
        }        
    }
}
