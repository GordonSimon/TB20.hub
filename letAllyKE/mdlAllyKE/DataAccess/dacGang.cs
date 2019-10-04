using System;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;

using System.Windows.Forms;



/// <summary>
/// Summary description for dacUsers
/// </summary>

namespace mdlAllyKE
{
    public sealed class dacGang
    {
        private static SqlDataAdapter _da = create_DA();

        private const string tblName = ConnectionManager.DBO + "Gang";


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private static void da_select(SqlDataAdapter da)
        {
            string sql = "SELECT * FROM "+ tblName;

            da.SelectCommand = new SqlCommand(sql, ConnectionManager.GetConnection());
        }


        private static void da_insert(SqlDataAdapter da)
        {
            string isql = "INSERT INTO " + tblName + " (";

            isql += "EmpId,EmpName, HomePhone, CellPhone, Active, userAudit";
            isql += ") VALUES (";
            
            isql += "@EID";
            isql += ", @ENAME";
            isql += ", @HPHONE";
            isql += ", @CPHONE";
            isql += ", @ACT";
            // CreateDate
            // UpdateDate
            isql += ", @USER";
            isql += ")";

            da.InsertCommand = new SqlCommand(isql, ConnectionManager.GetConnection());

            da.InsertCommand.Parameters.Add("@EID", SqlDbType.NChar).SourceColumn = "EmpId";
            da.InsertCommand.Parameters.Add("@ENAME", SqlDbType.NChar).SourceColumn = "EmpName";

            da.InsertCommand.Parameters.Add("@HPHONE", SqlDbType.NChar).SourceColumn = "HomePhone";
            da.InsertCommand.Parameters.Add("@CPHONE", SqlDbType.NChar).SourceColumn = "CellPhone";
            da.InsertCommand.Parameters.Add("@ACT", SqlDbType.NChar).SourceColumn = "Active";
            da.InsertCommand.Parameters.Add("@USER", SqlDbType.NChar).SourceColumn = "UserAudit";
        }


        private static void da_update(SqlDataAdapter da)
        {
            string usql = "UPDATE " + tblName + " SET Active = @ACT";
            usql += " WHERE EmpId=@EID";

            da.UpdateCommand = new SqlCommand(usql, ConnectionManager.GetConnection());
            
            da.UpdateCommand.Parameters.Add("@ACT", SqlDbType.Bit).SourceColumn = "Active";

            da.UpdateCommand.Parameters.Add("@EID", SqlDbType.NChar).SourceColumn = "EmpId";

            //string usql = "UPDATE "+ tblName + " SET EmpName = @ENAME";
            //usql += " WHERE ID=@ID";
            //da.UpdateCommand = new SqlCommand(usql, ConnectionManager.GetConnection());
            //da.UpdateCommand.Parameters.Add("@ENAME", SqlDbType.NChar).SourceColumn = "EmpName";
            //da.UpdateCommand.Parameters.Add("@ID", SqlDbType.Int).SourceColumn = "ID";
        }


        private static void da_delete(SqlDataAdapter da)
        {
            string dsql = "DELETE " + tblName;
            dsql += " WHERE EmpID=@EID";

            da.DeleteCommand = new SqlCommand(dsql, ConnectionManager.GetConnection());
            da.DeleteCommand.Parameters.Add("@EID", SqlDbType.NChar).SourceColumn = "EmpID";
        }


        private static SqlDataAdapter create_DA()
        {
            SqlDataAdapter da = new SqlDataAdapter();
            
            da_select(da);
            da_insert(da);
            da_update(da);
            da_delete(da);

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
            DataColumn col8 = table.Columns.Add("Gang", typeof(String)); col8.MaxLength = 16;
            DataColumn col9 = table.Columns.Add("CreateDate", typeof(DateTime));
            DataColumn col10 = table.Columns.Add("UpdateDate", typeof(DateTime));
            DataColumn col11 = table.Columns.Add("UserAudit", typeof(String)); col11.MaxLength = 32;

            table.PrimaryKey = new DataColumn[] { pk1 };
        }


        private static void load_ds(DataTable tbl)
        {
            DateTime tod = DateTime.Now;
            tbl.Rows.Add("C6", "Bill COTTON", "604-244-0457", "000-000-0000", true, tod, tod, "<system>");
            tbl.Rows.Add("G3", "Roland GERAK", "604-317-0559", "000-000-0000", true, tod, tod, "<system>");
            tbl.Rows.Add("G5", "Shawn GERAK", "778-885-8424", "000-000-0000", true, tod, tod, "<system>");
            tbl.Rows.Add("G6", "Erik GUDBRANSON", "604-842-1279", "000-000-0000", false, tod, tod, "<system>");
            tbl.Rows.Add("H2", "Randy HARMON", "604-868-1151", "000-000-0000", false, tod, tod, "<system>");            
        }


        private static DataTable create_dt()
        {
            DataTable tbl = new DataTable("CREW");

            define_schema(tbl);
            //load_ds(tbl);

            return tbl;
        }


        private static DataSet create_ds()
        {
            DataSet ds = new DataSet();
            DataTable tbl = ds.Tables.Add("CREW");

            define_schema(tbl);
            //load_ds(tbl);
            
            return ds;
        }


        public static bool IsKey(string emp_id)
        {
            bool ok = false;

            DataSet ds = create_ds();

            ds.EnforceConstraints = false;
            _da.Fill(ds.Tables["CREW"]);
            ds.EnforceConstraints = true;

            string where = string.Format("EmpId = '{0}'", emp_id);

            try
            {
                DataTable dt = ds.Tables[0].Select(where).CopyToDataTable();

                if (dt != null && dt.Rows.Count == 1) ok = true;
            }
            catch
            {
                //MessageBox.Show("dacGang.IsKey : " + ex.Message);
                return false;
            }

            return ok;
        }

        
        public static DataSet GetKey(string emp_id)
        {
            DataSet ds = create_ds();

            ds.EnforceConstraints = false;
            _da.Fill(ds.Tables["CREW"]);
            ds.EnforceConstraints = true;

            string where = string.Format("EmpId = '{0}'", emp_id);

            try
            {
                DataTable dt = ds.Tables[0].Select(where).CopyToDataTable();
                ds.Tables.Clear();
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("dacGang.GetKey : " + ex.Message);
            }

            return ds;

        }


        public static DataTable GetDT()
        {
            DataTable dt = create_dt();

            //ds.EnforceConstraints = false;
            _da.Fill(dt);

            //ds.EnforceConstraints = true;

            return dt;
        }


        public static DataSet GetDS()
        {
            DataSet ds = create_ds();
            
            ds.EnforceConstraints = false;
            _da.Fill(ds.Tables["CREW"]);

            ds.EnforceConstraints = true;

            return ds;
        }
        

        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public static void SaveData(DataTable changes)
        {
            if (changes == null) return;

            DataTable dt_upd = changes.GetChanges();

            if (dt_upd != null)
            {
                _da.Update(dt_upd);
                changes.Merge(dt_upd);
            }
        }


        public static void SaveData(DataSet changes)
        {
            if (changes == null) return;

            DataSet ds_upd = changes.GetChanges(DataRowState.Deleted | DataRowState.Added);

            if (ds_upd != null)
            {
                _da.Update(ds_upd.Tables[0]);
                changes.Merge(ds_upd);
            }
        }


    }
}
