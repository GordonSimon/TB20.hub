using System;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;



/// <summary>
/// Summary description for dacUsers
/// </summary>

namespace mdlAllyKE
{
    public sealed class dacToff
    {
        static public string[] _legend_val = new[] {
                "12", // 0
                "BL", // 1                
                "CL", // 2
                "H",  // 3
                "LD", // 4
                "LO", // 5
                "NW", // 6
                "O",  // 7
                "OT", // 8
                "Q",  // 9
                "S",  // 10
                "SH", // 11
                "SL", // 12
                "T",  // 13
                "TR", // 14
                "U",  // 15
                "W",  // 16
                "D",  // 17
                "WI", // 18
                "WS"  // 19
            };

        static public string[] _legend_show = new[] {
                "12   [Scheduled]",                
                "BL   [Bereavement Leave]",                
                "CL   [Crown Life]",
                "H    [Holidays]",
                "LD   [Lay Day Leave]",
                "LO   [Laid Off]", 
                "NW   [No Work]",
                "O    [Off]",
                "OT   [Overtime Leave]",
                "Q    [Quit]",
                "S    [Sick]",
                "SH   [Statutory Holiday]",
                "SL   [Sun Life]",
                "T    [Terminated]",
                "TR   [Training]",
                "U    [Unavailable]",
                "W    [WCB]",
                "D    [Discipline]",
                "WI   [Weekly Indeminity]",
                "WS   [Work Share Day]"
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
            DataColumn pk = table.Columns.Add("ToffKey", typeof(String)); pk.MaxLength = 8; pk.AllowDBNull = false;
            DataColumn col2 = table.Columns.Add("ToffDesc", typeof(String)); col2.MaxLength = 64;
            DataColumn col3 = table.Columns.Add("CreateyDate", typeof(DateTime));
            DataColumn col4 = table.Columns.Add("UpdateDate", typeof(DateTime));
            DataColumn col5 = table.Columns.Add("UserAudit", typeof(String)); col5.MaxLength = 32;
            
            table.PrimaryKey = new DataColumn[] { pk };
        }


        private static DataSet create_ds()
        {
            DataSet ds = new DataSet();
            DataTable tbl = ds.Tables.Add("TOFF");

            define_schema(tbl);

            return ds;
        }


        public static DataSet GetDS()
        {
            DateTime tod = DateTime.Now;

            DataSet ds = create_ds();

            DataTable table = ds.Tables[0];

            table.Rows.Add(_legend_val[0], _legend_show[0], tod, tod, "<system>");
            table.Rows.Add(_legend_val[1], _legend_show[1], tod, tod, "<system>");
            table.Rows.Add(_legend_val[2], _legend_show[2], tod, tod, "<system>");
            table.Rows.Add(_legend_val[3], _legend_show[3], tod, tod, "<system>");
            table.Rows.Add(_legend_val[4], _legend_show[4], tod, tod, "<system>");
            table.Rows.Add(_legend_val[5], _legend_show[5], tod, tod, "<system>");
            table.Rows.Add(_legend_val[6], _legend_show[6], tod, tod, "<system>");
            table.Rows.Add(_legend_val[7], _legend_show[7], tod, tod, "<system>");
            table.Rows.Add(_legend_val[8], _legend_show[8], tod, tod, "<system>");
            table.Rows.Add(_legend_val[9], _legend_show[9], tod, tod, "<system>");
            table.Rows.Add(_legend_val[10], _legend_show[10], tod, tod, "<system>");
            table.Rows.Add(_legend_val[11], _legend_show[11], tod, tod, "<system>");
            table.Rows.Add(_legend_val[12], _legend_show[12], tod, tod, "<system>");
            table.Rows.Add(_legend_val[13], _legend_show[13], tod, tod, "<system>");
            table.Rows.Add(_legend_val[14], _legend_show[14], tod, tod, "<system>");
            table.Rows.Add(_legend_val[15], _legend_show[15], tod, tod, "<system>");
            table.Rows.Add(_legend_val[16], _legend_show[16], tod, tod, "<system>");
            table.Rows.Add(_legend_val[17], _legend_show[17], tod, tod, "<system>");
            table.Rows.Add(_legend_val[18], _legend_show[18], tod, tod, "<system>");
            table.Rows.Add(_legend_val[19], _legend_show[19], tod, tod, "<system>");            


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
