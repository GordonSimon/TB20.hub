using System;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;



/// <summary>
/// Summary description for dacUsers
/// </summary>

namespace mdlAllyKE
{
    public sealed class dacShift
    {
        static public string[] _legend_num = new[] {
                "0", // 0 None
                "0600", // 1 DAY (6AM-6PM)
                "1800", // 2 NIGHT (6PM-6PM)
                "0000", // 3 PROTIDE (any shift)
                "1200", // 4 NOON
                "2400", // 5 24HOUR                
                "0900", // 6 SPLIT
                "2000", // 7 SPLIT2

            };

        static public string[] _legend_val = new[] {
                "N/A", // 0
                "DAY", // 1                
                "NIGHT", // 2                
                "PROTIDE", //3
                "NOON", // 4
                "24HOUR",  // 5
                "SPLIT", // 6
                "SPLIT2", // 7
                
            };

        static public string[] _legend_show = new[] {
                "<none>",                
                "AM (6AM-6PM)",                
                "PM (6PM-6AM)",
                "ProTide",
                "12 (Noon)",                
                "24 (24 Hour)",
                "Split",
                "Split2", 
            };

        //private static SqlDataAdapter _emp_da = create_emp_DA();


        //public static SqlDataReader GetUserInfo()
        //{
        //    SqlDataReader reader;

        //    string sql = "SELECT * FROM htgEmp";

        //    using (SqlCommand command = new SqlCommand(sql, ConnectionManager.GetConnection()))
        //    {
        //        reader = command.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.CloseConnection);
        //    }

        //    return reader;
        //}


        //private static SqlDataAdapter create_user_DA()
        //{
        //    string sql = "SELECT * FROM cccUsers";

        //    string usql = "UPDATE cccUsers SET LastDate = @LVDATE";
        //    usql += ", AccessCard=@CARD, InterconID=@INTERCON, InterconDate=@IDATE";
        //    usql += ", LostCard=@LOST, LostDate=@LDATE";
        //    usql += ", AlarmCode=@ALARM, LDCode=@LD";
        //    usql += ", VisitCount=@VISIT, VerificationDate=@VDATE";
        //    usql += ", PasswordCount=@PWCOUNT, PasswordDate=@PWDATE";
        //    usql += ", curPassword=@PW, oldPassword=@OPW";
        //    usql += ", reqPassword=@RPW, PasswordStrength=@PWSTRENGTH";
        //    usql += " WHERE ID=@ID";

        //    SqlDataAdapter da = new SqlDataAdapter();

        //    da.SelectCommand = new SqlCommand(sql, ConnectionManager.GetConnection());

        //    da.UpdateCommand = new SqlCommand(usql, ConnectionManager.GetConnection());            
        //    da.UpdateCommand.Parameters.Add("@LVDATE", SqlDbType.DateTime).SourceColumn = "LastDate";

        //    da.UpdateCommand.Parameters.Add("@CARD", SqlDbType.Bit).SourceColumn = "AccessCard";
        //    da.UpdateCommand.Parameters.Add("@INTERCON", SqlDbType.NChar).SourceColumn = "InterconID";
        //    da.UpdateCommand.Parameters.Add("@IDATE", SqlDbType.DateTime).SourceColumn = "InterconDate";
        //    da.UpdateCommand.Parameters.Add("@LOST", SqlDbType.Bit).SourceColumn = "LostCard";
        //    da.UpdateCommand.Parameters.Add("@LDATE", SqlDbType.DateTime).SourceColumn = "LostDate";
        //    da.UpdateCommand.Parameters.Add("@ALARM", SqlDbType.NChar).SourceColumn = "AlarmCode";
        //    da.UpdateCommand.Parameters.Add("@LD", SqlDbType.NChar).SourceColumn = "LDCode";
            
        //    da.UpdateCommand.Parameters.Add("@VISIT", SqlDbType.Int).SourceColumn = "VisitCount";
        //    da.UpdateCommand.Parameters.Add("@VDATE", SqlDbType.DateTime).SourceColumn = "VerificationDate";

        //    da.UpdateCommand.Parameters.Add("@PWCOUNT", SqlDbType.Int).SourceColumn = "PasswordCount";
        //    da.UpdateCommand.Parameters.Add("@PWDATE", SqlDbType.DateTime).SourceColumn = "PasswordDate";

        //    da.UpdateCommand.Parameters.Add("@PW", SqlDbType.NChar).SourceColumn = "curPassword";
        //    da.UpdateCommand.Parameters.Add("@OPW", SqlDbType.NChar).SourceColumn = "oldPassword";
        //    da.UpdateCommand.Parameters.Add("@RPW", SqlDbType.NChar).SourceColumn = "reqPassword";
        //    da.UpdateCommand.Parameters.Add("@PWSTRENGTH", SqlDbType.NChar).SourceColumn = "PasswordStrength";

        //    da.UpdateCommand.Parameters.Add("@ID", SqlDbType.Int).SourceColumn = "ID";
            

        //    return da;
        //}

        private static void define_schema(DataTable table)
        {
            //DataColumn pk = table.Columns.Add("ID", typeof(Int32));  pk.AllowDBNull = false;
            DataColumn pk = table.Columns.Add("Short", typeof(String)); pk.MaxLength = 8; pk.AllowDBNull = false;
            DataColumn col2 = table.Columns.Add("NumID", typeof(String)); col2.MaxLength = 4;
            DataColumn col3 = table.Columns.Add("ShiftDesc", typeof(String)); col3.MaxLength = 64;
            DataColumn col4 = table.Columns.Add("CreateyDate", typeof(DateTime));
            DataColumn col5 = table.Columns.Add("UpdateDate", typeof(DateTime));
            DataColumn col6 = table.Columns.Add("UserAudit", typeof(String)); col6.MaxLength = 32;
            
            table.PrimaryKey = new DataColumn[] { pk };
        }


        private static DataSet create_ds()
        {
            DataSet ds = new DataSet();
            DataTable tbl = ds.Tables.Add("SHIFT");

            define_schema(tbl);

            return ds;
        }


        public static DataTable GetDT()
        {
            DateTime tod = DateTime.Now;

            DataTable table = new DataTable("SHIFT");
            define_schema(table);

            table.Rows.Add(_legend_val[0], _legend_num[0], _legend_show[0], tod, tod, "<system>");
            table.Rows.Add(_legend_val[1], _legend_num[1], _legend_show[1], tod, tod, "<system>");
            table.Rows.Add(_legend_val[2], _legend_num[2], _legend_show[2], tod, tod, "<system>");
            table.Rows.Add(_legend_val[3], _legend_num[3], _legend_show[3], tod, tod, "<system>");
            table.Rows.Add(_legend_val[4], _legend_num[4], _legend_show[4], tod, tod, "<system>");
            table.Rows.Add(_legend_val[5], _legend_num[5], _legend_show[5], tod, tod, "<system>");
            table.Rows.Add(_legend_val[6], _legend_num[6], _legend_show[6], tod, tod, "<system>");
            //table.Rows.Add(_legend_val[6], _legend_num[6], _legend_show[6], tod, tod, "<system>");
            table.Rows.Add(_legend_val[7], _legend_num[7], _legend_show[7], tod, tod, "<system>");

            //ds.EnforceConstraints = false;
            //_user_da.Fill(ds.Tables["USER"]);
            //ds.EnforceConstraints = true;

            return table;
        }


        public static DataSet GetDS()
        {
            DateTime tod = DateTime.Now;

            DataSet ds = create_ds();

            DataTable table = ds.Tables[0];

            table.Rows.Add(_legend_val[0], _legend_num[0], _legend_show[0], tod, tod, "<system>");
            table.Rows.Add(_legend_val[1], _legend_num[1], _legend_show[1], tod, tod, "<system>");
            table.Rows.Add(_legend_val[2], _legend_num[2], _legend_show[2], tod, tod, "<system>");
            table.Rows.Add(_legend_val[3], _legend_num[3], _legend_show[3], tod, tod, "<system>");
            table.Rows.Add(_legend_val[4], _legend_num[4], _legend_show[4], tod, tod, "<system>");
            table.Rows.Add(_legend_val[5], _legend_num[5], _legend_show[5], tod, tod, "<system>");
            table.Rows.Add(_legend_val[6], _legend_num[6], _legend_show[6], tod, tod, "<system>");
            //table.Rows.Add(_legend_val[6], _legend_num[6], _legend_show[6], tod, tod, "<system>");
            table.Rows.Add(_legend_val[7], _legend_num[7], _legend_show[7], tod, tod, "<system>");

            
            //ds.EnforceConstraints = false;
            //_user_da.Fill(ds.Tables["USER"]);
            //ds.EnforceConstraints = true;

            return ds;
        }


        //public static void SaveData(ref DataSet changes)
        //{
        //    DataSet ds_upd = changes.GetChanges(DataRowState.Modified);

        //    if (ds_upd != null)
        //    {
        //        _user_da.Update(ds_upd.Tables[0]);
        //        changes.Merge(ds_upd);
        //    }
        //}

    }
}
