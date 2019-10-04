using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Reflection;


namespace mdlAnnal
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


        static private string build_con() { return build_con(_name); }

        static private string build_con(string dbo)
        {
            string strCon = ConfigurationManager.ConnectionStrings[dbo].ConnectionString;

            if (!dbo.Equals("dbo."))
                strCon += (dbo.Equals("SCHLAG") ? _schlag2 : _schlag1);

            return strCon;
        }


        public static string GetConnection()  { return GetConnection(_name); }

        public static string GetConnection(string dbo)
        {
            return ConfigurationManager.ConnectionStrings[dbo].ConnectionString;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public static SqlConnection Connect() { return Connect(_name); }

        public static SqlConnection Connect(string dbo)
        { 
            try
            {
                string strCon = build_con(dbo);

                SqlConnection con = new SqlConnection(strCon);
                con.Open();
                return con;
            }
            catch (Exception ex)
            {
                ErrMail.Fail(ex, System.Reflection.MethodBase.GetCurrentMethod());
                return null;
            }
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public static DataTable dabFill(string dbo, string sql)
        {
            try
            {
                using (SqlConnection con = Connect(dbo))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = new SqlCommand(sql, con);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                //ErrDash.SetLastException(ex, System.Reflection.MethodBase.GetCurrentMethod());
                ErrMail.Fail(ex, System.Reflection.MethodBase.GetCurrentMethod());
                return new DataTable("*Failure*");
            }
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        static public SqlCommand cmd_select(string tname, string where, SqlConnection con)
        {
            string sql = "SELECT * FROM " + tname;
            if (!where.Equals(string.Empty)) sql += " WHERE " + where;

            return new SqlCommand(sql, con);
        }


        static public DataTable ReadDT(string dbo, string tname, string where)
        {
            DataTable dt;

            try
            {
                using (SqlConnection con = sqlConnect.Connect(dbo))
                {
                    dt = new DataTable();

                    SqlCommand cmd = cmd_select(tname, where, con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);

                    return dt;
                }
            }
            catch (Exception ex)
            {               
                ErrMail.Fail(ex, System.Reflection.MethodBase.GetCurrentMethod());
                return new DataTable("*Failure*");
            }
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public static int dabNonQuery(string dbo, string sql)
        {
            try
            {
                using (SqlConnection con = sqlConnect.Connect(dbo))
                {
                    SqlCommand cmd = new SqlCommand(sql, con);
                    int r = cmd.ExecuteNonQuery();

                    return r;
                }
            }
            catch (Exception ex)
            {
                ErrMail.Fail(ex, System.Reflection.MethodBase.GetCurrentMethod());
                return -1;
            }
        }        
 

        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public static int dabScalar(string dbo, string sql)
        {
            try
            {
                using (SqlConnection con = sqlConnect.Connect(dbo))
                {
                    SqlCommand cmd = new SqlCommand(sql, con);
                    int r = (Int32)cmd.ExecuteScalar();
                    return r;
                }
            }
            catch (Exception ex)
            {
                ErrMail.Fail(ex, System.Reflection.MethodBase.GetCurrentMethod());
                return -1;
            }
        }        
    }

}

