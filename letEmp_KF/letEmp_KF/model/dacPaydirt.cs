using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.OleDb;


namespace letEmp_KF
{    
    public sealed class dacPaydirt
    {
        private const string _tname = mdbConnect.DBO + "tblEmployees";

        static private OleDbConnection _con = mdbConnect.Connect();
        static private OleDbDataAdapter _da = create_DA();

        
        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private static void da_select(OleDbDataAdapter da)
        {
            //string sql = "SELECT * FROM " + _tname;
            string sql = "SELECT";
            sql += " strEmployee_OrderName";
            sql += ", strEmployee_Status";
            sql += ", strEmployee_Telephone";
            sql += ", strEmployee_number";
            sql += ", dtmEmployee_OriginalHire";
            sql += ", strEmployee_Address1";
            sql += ", strEmployee_Address2";
            sql += ", strEmployee_City";
            sql += ", strEmployee_Province";
            sql += ", strEmployee_Postal";
            sql += ", strEmployee_Country";
            sql += ", dtmEmployee_Start";
            sql += ", dtmEmployee_End";
            sql += ", strEmployee_Frequency";
            sql += ", strEmployee_Lastname";
            sql += ", strEmployee_Firstname";
            sql += ", strEmployee_Middlename";
            sql += " FROM " + _tname;
            sql += " ORDER BY strEmployee_OrderName";
              
            da.SelectCommand = new OleDbCommand(sql, _con);
        }


        private static OleDbDataAdapter create_DA()
        {
            OleDbDataAdapter da = new OleDbDataAdapter();

            da_select(da);
            //da_insert(da);
            //da_update(da);
            //da_delete(da);

            return da;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private static OleDbCommand cmd_select()
        {
            string sql = "SELECT * FROM " + _tname;

            return new OleDbCommand(sql, _con);
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
            }
            catch (Exception ex)
            {
                string msg = string.Format("Error (dacEmployee.GetDT) : {0}", ex.Message);
                errDash.Error(msg);
            }

            return dt;
        }


    }
}
