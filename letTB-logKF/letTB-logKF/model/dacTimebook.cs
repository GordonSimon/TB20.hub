using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;


namespace letTB_logKF
{    
    public sealed class dacTimebook
    {
        private const string _tname = sqlConnect.DBO + "Timebook";

        static private SqlConnection _con = sqlConnect.Connect();
        static private SqlDataAdapter _da = create_DA();


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private static SqlCommand cmd_truncate(string tname)
        {
            string sql = "TRUNCATE TABLE " + tname;
            return new SqlCommand(sql, _con);
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private static void da_select(SqlDataAdapter da)
        {
            //string sql = "SELECT * FROM " + _tname;

            //string sql = "SELECT ";
            //sql += "Resp";
            //sql += ", BookDate";
            //sql += ", EmpId";
            //sql += ", EmpName";            
            //sql += ", LogHours";
            //sql += ", LogOver";
            //sql += ", LogOver1";
            //sql += ", LogShift";
            //sql += ", LogVessel";
            //sql += ", ToffCode";
            //sql += ", PayCode";            
            //sql += " FROM " + _tname;

//            string sql =
//                            @"SELECT * FROM [Timebook] tT
//                                INNER JOIN [Employee] tE ON tT.Empid=tE.EmpId                            
//                            order by tT.[EmpId], tT.[ID]";

//            WHERE EmpName like 'BO%'                

//GS1903015 - Go back n years (n set to 4)
            string sql =
                @"SELECT * FROM [Timebook] tT
                                INNER JOIN [Employee] tE ON tT.Empid=tE.EmpId                  
                  WHERE CAST(BookDate as Date) >= DateAdd(yy, -4, GetDate())
                            order by tT.[EmpId], tT.[ID]";

//GS190201 - was this a test ?
//            @"SELECT * FROM [Timebook] tT
//                                INNER JOIN [Employee] tE ON tT.Empid=tE.EmpId                  
//                  WHERE CAST(BookDate as Date) >= DateAdd(d, -90, GetDate())
//                            order by tT.[EmpId], tT.[ID]";



//            @"SELECT * FROM [Timebook] tT
//                INNER JOIN [Employee] tE ON tT.Empid=tE.EmpId
//            WHERE tE.Duty = '{0}'
//            order by tT.[EmpId], tT.[ID]";



            da.SelectCommand = new SqlCommand(sql, _con);
        }


        private static SqlDataAdapter create_DA()
        {
            SqlDataAdapter da = new SqlDataAdapter();

            da_select(da);
            //da_insert(da);
            //da_update(da);
            //da_delete(da);

            return da;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private static SqlCommand cmd_select()
        {
            string sql = "SELECT * FROM " + _tname;

            return new SqlCommand(sql, _con);
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public static DataTable GetDT()
        {
            DataTable dt;

            dt = null;

            try
            {
                dt = new DataTable();

                _da.Fill(dt);

                //System.Windows.Forms.MessageBox.Show(dt.Rows.Count.ToString());
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error (dacTimebook.GetDT) : {0}", ex.Message);
                errDash.Error(msg);
            }

            return dt;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public static DataTable ReadDT()
        {
            DataTable dt;

            dt = null;

            try
            {
                dt = new DataTable();

                SqlCommand cmd = cmd_select();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error (dacTimebook.ReadDT) : {0}", ex.Message);
                errDash.Error(msg);
            }

            return dt;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public static int Proc()
        {
            int retval = 0;

            using (SqlCommand cmd = new SqlCommand("GBOOM_InvoiceSendElectronicByDate", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;                
                cmd.Parameters.Add("@Date", SqlDbType.VarChar).Value = @"31/08/2015"; //DBNull.Value;                
                var retcode = cmd.Parameters.Add("@Retcode", SqlDbType.Int);
                //var retmesg = cmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 500);

                retcode.Direction = ParameterDirection.ReturnValue;
                //retmesg.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                
                //var result = retcode.Value;
                //retval = (int)result;
                retval = (int)cmd.Parameters["@Retcode"].Value;
                //System.Windows.Forms.MessageBox.Show((string)cmd.Parameters["@Retmesg"].Value);
            }

            return retval;
        }
    }
}
