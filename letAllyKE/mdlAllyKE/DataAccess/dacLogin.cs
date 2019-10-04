using System;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;

using System.Windows.Forms;



/// <summary>
/// Summary description for dacLogin
/// </summary>

namespace mdlAllyKE
{
    public sealed class dacLogin
    {
        private static SqlDataAdapter _da = create_DA();
        
        private const string tblName = ConnectionManager.DBO + "Login";


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private static void da_select(SqlDataAdapter da)
        {
            string sql = "SELECT * FROM "+ tblName;

            da.SelectCommand = new SqlCommand(sql, ConnectionManager.GetConnection());
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

        private static void define_schema(DataTable table)
        {
            //DataColumn pk = table.Columns.Add("ID", typeof(Int32));  pk.AllowDBNull = false;
            DataColumn pk1 = table.Columns.Add("EmpID", typeof(String)); pk1.MaxLength = 8; pk1.AllowDBNull = false;
            DataColumn col3 = table.Columns.Add("EmpName", typeof(String)); col3.MaxLength = 64;
            DataColumn col4 = table.Columns.Add("HomePhone", typeof(String)); col4.MaxLength = 16;
            DataColumn col5 = table.Columns.Add("CellPhone", typeof(String)); col5.MaxLength = 16;
            DataColumn col6 = table.Columns.Add("Duty", typeof(String)); col6.MaxLength = 16;
            DataColumn col7 = table.Columns.Add("Active", typeof(bool));
            DataColumn col8 = table.Columns.Add("CreateDate", typeof(DateTime));
            DataColumn col9 = table.Columns.Add("UpdateDate", typeof(DateTime));
            DataColumn col10 = table.Columns.Add("UserAudit", typeof(String)); col10.MaxLength = 32;

            table.PrimaryKey = new DataColumn[] { pk1 };
        }


        private static void load_ds(DataTable tbl)
        {
            DateTime tod = DateTime.Now;
            tbl.Rows.Add("DP1", "Chris HODDER", "000-000-0000", "000-000-0000", "Disptach", true, tod, tod, "<system>");
            tbl.Rows.Add("DP2", "Robert DUNN", "000-000-0000", "000-000-0000", "Disptach", true, tod, tod, "<system>");
            tbl.Rows.Add("DP3", "Travis DUNN", "000-000-0000", "000-000-0000", "Disptach", true, tod, tod, "<system>");
            tbl.Rows.Add("DP4", "Caroline SIMON", "000-000-0000", "000-000-0000", "Disptach", false, tod, tod, "<system>");
            tbl.Rows.Add("DP5", "Mike HODDER", "000-000-0000", "000-000-0000", "Disptach", false, tod, tod, "<system>");
        }


        private static DataSet create_ds()
        {
            DataSet ds = new DataSet();
            DataTable tbl = ds.Tables.Add("LOGIN");

            define_schema(tbl);
            load_ds(tbl);

            return ds;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        static public DataSet GetDS()
        {
            DataSet ds = null;

            try
            {
                ds = create_ds();

                //ds.EnforceConstraints = false;
                //_da.Fill(ds.Tables["LOGIN"]);

                //ds.EnforceConstraints = true;

                return ds;

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error (dacLogin.GetDS) : {0}", ex.Message));
            }

            return ds;
        }
    }
}
