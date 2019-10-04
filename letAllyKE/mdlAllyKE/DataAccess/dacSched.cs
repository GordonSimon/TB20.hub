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
    public sealed class dacSched
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
            //DataColumn pk = table.Columns.Add("ID", typeof(Int32));  pk.AllowDBNull = false;
            DataColumn pk1 = table.Columns.Add("ShipDate", typeof(DateTime)); pk1.AllowDBNull = false;
            DataColumn pk2 = table.Columns.Add("BoatCode", typeof(String)); pk2.MaxLength = 8; pk2.AllowDBNull = false;
            DataColumn pk3 = table.Columns.Add("ShiftID", typeof(Int32));
            DataColumn col4 = table.Columns.Add("BoatName", typeof(String)); col4.MaxLength = 64;            
            DataColumn col5 = table.Columns.Add("Shift", typeof(String)); col5.MaxLength = 8;
            DataColumn col6 = table.Columns.Add("BoatStart", typeof(DateTime));
            DataColumn col7 = table.Columns.Add("BoatFinish", typeof(DateTime));
            DataColumn col8 = table.Columns.Add("Year", typeof(Int32));
            DataColumn col9 = table.Columns.Add("Week", typeof(Int32)); 
            DataColumn col10 = table.Columns.Add("OnM", typeof(Boolean));
            DataColumn col11 = table.Columns.Add("OnT", typeof(Boolean));
            DataColumn col12 = table.Columns.Add("OnW", typeof(Boolean));
            DataColumn col13 = table.Columns.Add("OnTh", typeof(Boolean));
            DataColumn col14 = table.Columns.Add("OnF", typeof(Boolean));
            DataColumn col15 = table.Columns.Add("OnSa", typeof(Boolean)); 
            DataColumn col16 = table.Columns.Add("OnSu", typeof(Boolean));            
            DataColumn col17 = table.Columns.Add("CreateyDate", typeof(DateTime));
            DataColumn col18 = table.Columns.Add("UpdateDate", typeof(DateTime));
            DataColumn col19 = table.Columns.Add("UserAudit", typeof(String)); col19.MaxLength = 32;

            table.PrimaryKey = new DataColumn[] { pk1, pk2, pk3 };
        }


        private static DataSet create_ds()
        {
            DataSet ds = new DataSet();
            DataTable tbl = ds.Tables.Add("SCHED");

            define_schema(tbl);

            DateTime nwk = new DateTime(2014, 3, 24);
            DateTime tod = DateTime.Now;
            DateTime tm = (DateTime.Now).Date + new TimeSpan(6, 0, 0);
            string am = tm.ToString("hh:mm tt");
            string pm = tm.AddHours(12).ToString("hh:mm tt");
            
            tbl.Rows.Add(nwk.AddDays(0).Date, "HR", 1, "Hodder Ranger", "AM", am, pm, 2014, 0, true, true, true, true, true, true, true, tod, tod, "<system>");
            tbl.Rows.Add(nwk.AddDays(0).Date, "HR", 2, "Hodder Ranger", "PM", pm, am, 2014, 0, true, true, true, true, true, false, false, tod, tod, "<system>");

            tbl.Rows.Add(nwk.AddDays(7).Date, "HR", 1, "Hodder Ranger", "AM", am, pm, 2014, 0, false, false, false, true, true, true, true, tod, tod, "<system>");




            return ds;
        }


        public static DataSet GetDS(DateTime start_week, int days)
        {
            DataSet ds = create_ds();

            string where = string.Format("ShipDate >= '{0}' and ShipDate <= '{1}'",
                start_week.ToShortDateString(), start_week.AddDays(days).ToShortDateString());

            try
            {
                ds.Tables[0].DefaultView.RowFilter = where;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


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
