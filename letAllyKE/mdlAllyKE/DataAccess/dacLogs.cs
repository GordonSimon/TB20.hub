using System;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;



/// <summary>
/// Summary description for dacUsers
/// </summary>

namespace mdlAllyKE
{
    public sealed class dacLogs
    {
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
            DataColumn pk = table.Columns.Add("ID", typeof(Int32));  pk.AllowDBNull = false;
            DataColumn col2 = table.Columns.Add("LogDate", typeof(DateTime));
            DataColumn col3 = table.Columns.Add("CaptCode", typeof(String)); col3.MaxLength = 8;
            DataColumn col4 = table.Columns.Add("CaptName", typeof(String)); col4.MaxLength = 64;
            DataColumn col5 = table.Columns.Add("CaptActual", typeof(Int32));
            DataColumn col6 = table.Columns.Add("CaptPaid", typeof(Int32));
            DataColumn col7 = table.Columns.Add("CaptPremium", typeof(Int32));
            DataColumn col8 = table.Columns.Add("MateCode", typeof(String)); col8.MaxLength = 8;
            DataColumn col9 = table.Columns.Add("MateName", typeof(String)); col9.MaxLength = 64;
            DataColumn col10 = table.Columns.Add("MateActual", typeof(Int32));
            DataColumn col11 = table.Columns.Add("MatePaid", typeof(Int32));
            DataColumn col12 = table.Columns.Add("MatePremium", typeof(Int32));
            DataColumn col13 = table.Columns.Add("EngrCode", typeof(String)); col8.MaxLength = 8;
            DataColumn col14 = table.Columns.Add("EngrName", typeof(String)); col9.MaxLength = 64;
            DataColumn col15 = table.Columns.Add("EngrActual", typeof(Int32));
            DataColumn col16 = table.Columns.Add("EngrPaid", typeof(Int32));
            DataColumn col17 = table.Columns.Add("EngrPremium", typeof(Int32));
            DataColumn col18 = table.Columns.Add("Dkh1Code", typeof(String)); col18.MaxLength = 8;
            DataColumn col19 = table.Columns.Add("Dkh1Name", typeof(String)); col19.MaxLength = 64;
            DataColumn col20 = table.Columns.Add("Dkh1Actual", typeof(Int32));
            DataColumn col21 = table.Columns.Add("Dkh1Paid", typeof(Int32));
            DataColumn col22 = table.Columns.Add("Dkh1Premium", typeof(Int32));
            DataColumn col23 = table.Columns.Add("Dkh2Code", typeof(String)); col23.MaxLength = 8;
            DataColumn col24 = table.Columns.Add("Dkh2Name", typeof(String)); col24.MaxLength = 64;
            DataColumn col25 = table.Columns.Add("Dkh2Actual", typeof(Int32));
            DataColumn col26 = table.Columns.Add("Dkh2Paid", typeof(Int32));
            DataColumn col27 = table.Columns.Add("Dkh2Premium", typeof(Int32));
                        
            
            table.PrimaryKey = new DataColumn[] { pk };
        }


        private static DataSet create_ds()
        {
            DataSet ds = new DataSet();
            DataTable tbl = ds.Tables.Add("LOG");

            define_schema(tbl);

            return ds;
        }


        public static DataSet GetLogDS()
        {
            DataSet ds = create_ds();
            
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
