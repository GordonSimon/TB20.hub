using System;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;

using System.Windows.Forms;



/// <summary>
/// Summary description for dacTimebook
/// </summary>

namespace mdlAllyKE{
    public sealed class dacTimebook
    {
        private static SqlDataAdapter _da = create_DA();

        private const string tblName = ConnectionManager.DBO + "Timebook";

        static DataSet _ds = null;


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private static void da_select(SqlDataAdapter da)
        {
            //string sql = "SELECT * FROM cccUsers";
            //string sql = "SELECT * FROM Access.Timebook";

            string sql = "SELECT * FROM " + tblName + " WHERE BookDate BETWEEN @BEGDATE AND @ENDDATE";

            //GS190531
            //Testing with constraints
            //string sql = "SELECT * FROM " + tblName + 
            //    " WHERE BookDate BETWEEN @BEGDATE AND @ENDDATE AND LogVessel is null and ToffCode is null";
            //    //" WHERE BookDate BETWEEN @BEGDATE AND @ENDDATE AND LogVessel is null and ToffCode is null and EmpName > 'G' and EmpName < 'I'";
            //string sql = "SELECT * FROM " + tblName + 
            //    " WHERE BookDate BETWEEN @BEGDATE AND @ENDDATE and EmpID = 'HR8201'";

            da.SelectCommand = new SqlCommand(sql, ConnectionManager.GetConnection());

            da.SelectCommand.Parameters.AddWithValue("@BEGDATE", new DateTime(2014, 01, 06));
            da.SelectCommand.Parameters.AddWithValue("@ENDDATE", new DateTime(2014, 01, 12));
            
        }



        private static void da_insert(SqlDataAdapter da)
        {
            string isql = "INSERT INTO " + tblName + " (";

            isql += "BookDate, EmpId, EmpName, ToffCode, LogHours, LogOver, LogVessel, LogShift, Resp, LogNote";
            isql += ", CreateDate, UpdateDate, UserAudit";
            isql += ") VALUES (";

            isql += "@BDATE";
            isql += ", @EID";
            isql += ", @ENAME";
            isql += ", @TOFF";

            isql += ", @HOUR";
            isql += ", @OVER";
            isql += ", @VESSEL";
            isql += ", @SHIFT";
            isql += ", @RESP";
            isql += ", @NOTE";

            isql += ", @CDATE";
            isql += ", @UDATE";
            isql += ", @USER";
            isql += ")";

            da.InsertCommand = new SqlCommand(isql, ConnectionManager.GetConnection());

            da.InsertCommand.Parameters.Add("@BDATE", SqlDbType.Date).SourceColumn = "BookDate";
            da.InsertCommand.Parameters.Add("@EID", SqlDbType.NChar).SourceColumn = "EmpId";
            da.InsertCommand.Parameters.Add("@ENAME", SqlDbType.NChar).SourceColumn = "EmpName";

            da.InsertCommand.Parameters.Add("@TOFF", SqlDbType.NChar).SourceColumn = "ToffCode";
            da.InsertCommand.Parameters.Add("@HOUR", SqlDbType.Decimal).SourceColumn = "LogHours";
            da.InsertCommand.Parameters.Add("@OVER", SqlDbType.Decimal).SourceColumn = "LogOver";
            da.InsertCommand.Parameters.Add("@VESSEL", SqlDbType.NChar).SourceColumn = "LogVessel";
            da.InsertCommand.Parameters.Add("@SHIFT", SqlDbType.Int).SourceColumn = "LogShift";
            da.InsertCommand.Parameters.Add("@RESP", SqlDbType.NChar).SourceColumn = "Resp";
            da.InsertCommand.Parameters.Add("@NOTE", SqlDbType.NChar).SourceColumn = "LogNote";
            
            da.InsertCommand.Parameters.Add("@UDATE", SqlDbType.DateTime).SourceColumn = "CreateDate";
            da.InsertCommand.Parameters.Add("@CDATE", SqlDbType.DateTime).SourceColumn = "UpdateDate";
            da.InsertCommand.Parameters.Add("@USER", SqlDbType.NChar).SourceColumn = "UserAudit";
        }


        private static void da_update(SqlDataAdapter da)
        {
            string usql = "UPDATE " + tblName + " SET";
            usql += " BookDate=@BDATE, EmpName = @ENAME, ToffCode=@TOFF";
            usql += ", LogHours=@Hour, LogOver=@OVER, LogVessel=@VESSEL, LogShift=@SHIFT, Resp=@RESP, LogNote=@NOTE";
            usql += ", CreateDate=@CDATE, UpdateDate=@UDATE, UserAudit=@USER";

            usql += " WHERE BookDate=@BDATE AND EmpID=@EID AND LogShift=@SHIFT";

            da.UpdateCommand = new SqlCommand(usql, ConnectionManager.GetConnection());

            da.UpdateCommand.Parameters.Add("@BDATE", SqlDbType.Date).SourceColumn = "BookDate";
            da.UpdateCommand.Parameters.Add("@EID", SqlDbType.NChar).SourceColumn = "EmpId";
            da.UpdateCommand.Parameters.Add("@ENAME", SqlDbType.NChar).SourceColumn = "EmpName";

            da.UpdateCommand.Parameters.Add("@TOFF", SqlDbType.NChar).SourceColumn = "ToffCode";
            da.UpdateCommand.Parameters.Add("@HOUR", SqlDbType.Decimal).SourceColumn = "LogHours";
            da.UpdateCommand.Parameters.Add("@OVER", SqlDbType.Decimal).SourceColumn = "LogOver";
            da.UpdateCommand.Parameters.Add("@VESSEL", SqlDbType.NChar).SourceColumn = "LogVessel";
            da.UpdateCommand.Parameters.Add("@SHIFT", SqlDbType.Int).SourceColumn = "LogShift";
            da.UpdateCommand.Parameters.Add("@RESP", SqlDbType.NChar).SourceColumn = "Resp";
            da.UpdateCommand.Parameters.Add("@NOTE", SqlDbType.NChar).SourceColumn = "LogNote";

            da.UpdateCommand.Parameters.Add("@UDATE", SqlDbType.DateTime).SourceColumn = "CreateDate";
            da.UpdateCommand.Parameters.Add("@CDATE", SqlDbType.DateTime).SourceColumn = "UpdateDate";
            da.UpdateCommand.Parameters.Add("@USER", SqlDbType.NChar).SourceColumn = "UserAudit";

        }


        private static void da_delete(SqlDataAdapter da)
        {
            string dsql = "DELETE " + tblName;
            dsql += " WHERE BookDate=@BDATE AND EmpID=@EID AND LogShift=@SHIFT";

            da.DeleteCommand = new SqlCommand(dsql, ConnectionManager.GetConnection());

            da.DeleteCommand.Parameters.Add("@BDATE", SqlDbType.Date).SourceColumn = "BookDate";
            da.DeleteCommand.Parameters.Add("@EID", SqlDbType.NChar).SourceColumn = "EmpID";
            da.DeleteCommand.Parameters.Add("@SHIFT", SqlDbType.Int).SourceColumn = "LogShift";
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

        private static void define_timebook_schema(DataTable table)
        {
            //DataColumn pk = table.Columns.Add("ID", typeof(Int32));  pk.AllowDBNull = false;
            DataColumn pk1 = table.Columns.Add("BookDate", typeof(DateTime)); pk1.AllowDBNull = false;
            DataColumn pk2 = table.Columns.Add("EmpID", typeof(String)); pk2.MaxLength = 8; pk2.AllowDBNull = false;

            DataColumn col3 = table.Columns.Add("EmpName", typeof(String)); col3.MaxLength = 64;
            DataColumn col4 = table.Columns.Add("ToffCode", typeof(String)); col4.MaxLength = 8;
            DataColumn col5 = table.Columns.Add("LogHours", typeof(Decimal));
            DataColumn col6 = table.Columns.Add("LogOver", typeof(Decimal));

            DataColumn col7 = table.Columns.Add("LogVessel", typeof(String)); col7.MaxLength = 16;
            //DataColumn pk3 = table.Columns.Add("LogVessel", typeof(String)); pk3.MaxLength = 16;

            DataColumn pk3 = table.Columns.Add("LogShift", typeof(Int32));
            //DataColumn col8 = table.Columns.Add("LogShift", typeof(Int32));

            DataColumn col9 = table.Columns.Add("LogHour", typeof(String)); col9.MaxLength = 16;
            DataColumn col10 = table.Columns.Add("LogSheet", typeof(String)); col10.MaxLength = 16;
            DataColumn col11 = table.Columns.Add("LogEngineStart", typeof(Decimal));
            DataColumn col12 = table.Columns.Add("LogEngineFinish", typeof(Decimal));
            DataColumn col13 = table.Columns.Add("LogFuel", typeof(Decimal));            
            DataColumn col14 = table.Columns.Add("LogNote", typeof(String)); col14.MaxLength = 512;
            DataColumn col15 = table.Columns.Add("CreateDate", typeof(DateTime));
            DataColumn col16 = table.Columns.Add("UpdateDate", typeof(DateTime));
            DataColumn col17 = table.Columns.Add("UserAudit", typeof(String)); col17.MaxLength = 32;
            DataColumn col18 = table.Columns.Add("Resp", typeof(String)); col18.MaxLength = 8;
            DataColumn col19 = table.Columns.Add("LogPaid", typeof(Decimal));

            table.PrimaryKey = new DataColumn[] { pk1, pk2, pk3 };
            //table.PrimaryKey = new DataColumn[] { pk1, pk2 };
        }


        private static void load_ds(DataTable tbl)
        {
            DateTime nwk = new DateTime(2014, 3, 17);
            DateTime tod = DateTime.Now;

            tbl.Rows.Add(tod.AddDays(0).Date, "C6", "Bill COTTON", "12", 0, 0, "HN", 1, "", tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(1).Date, "C6", "Bill COTTON", "12", 0, 0, "HN", 1, "", tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(2).Date, "C6", "Bill COTTON", "12", 0, 0, "HN", 1, "", tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(3).Date, "C6", "Bill COTTON", "12", 0, 0, "HN", 1, "", tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(4).Date, "C6", "Bill COTTON", "12", 0, 0, "HN", 1, "", tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(5).Date, "C6", "Bill COTTON", "12", 0, 0, "HN", 1, "", tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(6).Date, "C6", "Bill COTTON", "12", 0, 0, "HN", 1, "", tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(7).Date, "C6", "Bill COTTON", "12", 0, 0, "HN", 1, "", tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(8).Date, "C6", "Bill COTTON", "12", 0, 0, "HN", 1, "", tod, tod, "<system>");
            //tbl.Rows.Add(tod.AddDays(8).Date, "C6", "Bill COTTON", "O", 0, 0, "", 0, "",tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(9).Date, "C6", "Bill COTTON", "O", 0, 0, "", 0, "", tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(10).Date, "C6", "Bill COTTON", "O", 0, 0, "", 0, "", tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(11).Date, "C6", "Bill COTTON", "O", 0, 0, "", 0, "", tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(12).Date, "C6", "Bill COTTON", "O", 0, 0, "", 0, "", tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(13).Date, "C6", "Bill COTTON", "O", 0, 0, "", 0, "", tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(14).Date, "C6", "Bill COTTON", "12", 0, 0, "HN", 1, "", tod, tod, "<system>");

            tbl.Rows.Add(tod.AddDays(0).Date, "G3", "Roland GERAK", "12", 0, 0, "HR", 1, "", tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(1).Date, "G3", "Roland GERAK", "12", 0, 0, "HR", 1, "", tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(2).Date, "G3", "Roland GERAK", "12", 0, 0, "HR", 2, "", tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(3).Date, "G3", "Roland GERAK", "12", 0, 0, "HR", 3, "", tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(4).Date, "G3", "Roland GERAK", "12", 0, 0, "HR", 1, "", tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(5).Date, "G3", "Roland GERAK", "12", 0, 0, "HR", 1, "", tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(6).Date, "G3", "Roland GERAK", "12", 0, 0, "HR", 2, "", tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(7).Date, "G3", "Roland GERAK", "12", 0, 0, "HR", 3, "", tod, tod, "<system>");

            tbl.Rows.Add(tod.AddDays(-4).Date, "G5", "Shawn GERAK", "12", 0, 0, "HR", 1, "", tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(-3).Date, "G5", "Shawn GERAK", "12", 0, 0, "HR", 1, "", tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(-2).Date, "G5", "Shawn GERAK", "12", 0, 0, "HR", 1, "", tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(-1).Date, "G5", "Shawn GERAK", "12", 0, 0, "HR", 1, "", tod, tod, "<system>");

            tbl.Rows.Add(tod.AddDays(0).Date, "G5", "Shawn GERAK", "12", 0, 0, "HR", 1, "", tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(1).Date, "G5", "Shawn GERAK", "12", 0, 0, "HR", 1, "", tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(2).Date, "G5", "Shawn GERAK", "12", 0, 0, "HR", 1, "", tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(3).Date, "G5", "Shawn GERAK", "12", 0, 0, "HR", 1, "", tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(4).Date, "G5", "Shawn GERAK", "12", 0, 0, "HR", 1, "", tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(5).Date, "G5", "Shawn GERAK", "12", 0, 0, "HR", 1, "", tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(6).Date, "G5", "Shawn GERAK", "12", 0, 0, "HR", 1, "", tod, tod, "<system>");
        }


        private static DataTable create_dt()
        {
            DataTable tbl = new DataTable("TOFF");

            define_timebook_schema(tbl);
            //load_ds(tbl);

            return tbl;
        }


        private static DataSet create_ds()
        {            
            DataSet ds = new DataSet();
            DataTable tbl = ds.Tables.Add("TOFF");

            define_timebook_schema(tbl);

            _ds = ds;

            //load_ds(tbl);

            return ds;
        }


        public static DataRow FindDel(Object[] pk)
        {
            //DataSet ds;

            if (_ds == null)
                _ds = create_ds();

            //ds = _ds;

            DataRow fr = _ds.Tables[0].Rows.Find(pk);
            if (fr != null)
            {
                fr.Delete();
            }
            //else //- GS150209 (deleta all)
            //    _ds.Tables[0].Rows[0].Delete();

            return null;
        }


        public static DataRow FindSet(Object[] pk, DataRow row)
        {
            DataSet ds;

            if (_ds == null)
                _ds = create_ds();

            ds = _ds;

   
            Object[] pk_set = new Object[] { pk[0], pk[1], "0" };
            DataRow fr = ds.Tables[0].Rows.Find(pk_set);

            if (fr != null)
            {
                string toff = (string)fr["ToffCode"];

                if (toff.Equals("12"))
                {
                    fr.BeginEdit();

                    fr["LogShift"] = pk[2];

                    fr["ToffCode"] = null;
                    fr["LogHours"] = row["LogHours"];
                    fr["LogOver"] = row["LogOver"];
                    fr["LogVessel"] = row["LogVessel"];
                    fr["Resp"] = row["Resp"];
                    //fr["LogShift"] = row["LogShift"];

                    fr["LogNote"] = row["LogNote"];

                    fr["UpdateDate"] = DateTime.Now;
                    fr["UserAudit"] = "<update>";
                    fr.EndEdit();

                    return fr;
                }
            }
           
            return null;
        }


        public static DataRow FindAdd(Object[] pk, DataRow row)
        {
            DataSet ds;

            if (_ds == null)
                _ds = create_ds();

            ds = _ds;

            DataRow fr = ds.Tables[0].Rows.Find(pk);
            if (fr == null)
            {
                row["EmpID"] = pk[1];
                row["BookDate"] = pk[0];
                row["LogShift"] = pk[2];

                row["CreateDate"] = DateTime.Now;
                row["UserAudit"] = "<system>";

                ds.Tables[0].Rows.Add(row);                
            }
            else
            {
                //MessageBox.Show("Error (Update Request) : Overwrite rejected !",
                //    "Module Message(dacTimebook)", 
                //    MessageBoxButtons.OK,
                //    MessageBoxIcon.Error);

                fr["ToffCode"] = row["ToffCode"];
                fr["LogHours"] = row["LogHours"];
                fr["LogOver"] = row["LogOver"];
                fr["LogVessel"] = row["LogVessel"];
                fr["Resp"] = row["Resp"];
                //fr["LogShift"] = row["LogShift"];
                
                fr["LogNote"] = row["LogNote"];

                fr["UpdateDate"] = DateTime.Now;
                fr["UserAudit"] = "<update>";
                
            }
            
            return null;
        }


        public static DataTable GetDT(DateTime start_week, int days)
        {
            DataTable dt = null;

            try
            {
                dt = create_dt();

                // GS190531 - debug
                // testing constraints
                //DateTime b = start_week.AddDays(42);
                //DateTime e = start_week.AddDays(42);
                //_da.SelectCommand.Parameters["@BEGDATE"].Value = b;
                //_da.SelectCommand.Parameters["@ENDDATE"].Value = e;

                _da.SelectCommand.Parameters["@BEGDATE"].Value = start_week.AddDays(-1);
                _da.SelectCommand.Parameters["@ENDDATE"].Value = start_week.AddDays(days);

                //dt.Constraints.Clear();

                _da.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error (GetDT) : " + ex.Message,
                    "Module Debug(dacTimebook)",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            
            return dt;
        }


        public static DataSet GetDS(DateTime start_week, int days)
        {
            DataSet ds = null;

            try
            {
                if (_ds == null) _ds = create_ds();

                _da.SelectCommand.Parameters["@BEGDATE"].Value = start_week.AddDays(-1);
                _da.SelectCommand.Parameters["@ENDDATE"].Value = start_week.AddDays(days);

                _ds.EnforceConstraints = false;
                _da.Fill(_ds.Tables["TOFF"]);                
                _ds.EnforceConstraints = true;
                ds = _ds;
                                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error (GetDS) : " + ex.Message,
                    "Module Debug(dacTimebook)",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            //if (_ds == null)
            //{
            //    _ds = create_ds();                    
            //    _ds.EnforceConstraints = false;
            //    _da.Fill(_ds.Tables["TOFF"]);                
            //    _ds.EnforceConstraints = true;
            //}

            //string where = string.Format("BookDate >= '{0}' and BookDate <= '{1}'",
            //   start_week.ToShortDateString(), start_week.AddDays(days).ToShortDateString());
            //try
            //{
            //    ds.Tables[0].DefaultView.RowFilter = where;
            //}
            //catch (Exception ex)
            //{   
            //    MessageBox.Show(ex.Message);
            //}
                                            
            return ds;
        }


        public static DataSet GetDSbyEmpId(DateTime start_week, int days, string empid)
        {
            DataSet ds;

            //if (_ds == null)
            //{
            //    _ds = create_ds();
            //
            //    _da.SelectCommand.Parameters["@BEGDATE"].Value = start_week;
            //    _da.SelectCommand.Parameters["@ENDDATE"].Value = start_week.AddDays(days);
            //
            //    _ds.EnforceConstraints = false;
            //    _da.Fill(_ds.Tables["TOFF"]);
            //    _ds.EnforceConstraints = true;
            //}

            //if (_ds == null)
            //{
            //    _ds = create_ds();
            //    _ds.EnforceConstraints = false;
            //    _da.Fill(_ds.Tables["TOFF"]);
            //    _ds.EnforceConstraints = true;
            //}


            ds = create_ds();

            _da.SelectCommand.Parameters["@BEGDATE"].Value = start_week.AddDays(-1);
            _da.SelectCommand.Parameters["@ENDDATE"].Value = start_week.AddDays(days);

            _ds.EnforceConstraints = false;
            _da.Fill(ds.Tables["TOFF"]);
            //_ds.EnforceConstraints = true;

            //ds = _ds;
            
            string where = string.Format("EmpID = '{0}' and BookDate >= '{1}' and BookDate <= '{2}'",
                empid, start_week.ToShortDateString(), start_week.AddDays(days).ToShortDateString());

            try
            {
                ds.Tables[0].DefaultView.RowFilter = where;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return ds;
        }


        private static DataSet create_timebook_ds()
        {
            DataSet ds = new DataSet();
            DataTable user = ds.Tables.Add("TOFF");

            define_timebook_schema(user);

            return ds;
        }


        public static DataSet GetUserDS()
        {
            DataSet ds = create_ds();

            ds.EnforceConstraints = false;
            _da.Fill(ds.Tables["TOFF"]);
            ds.EnforceConstraints = true;

            return ds;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        //public static void SaveData(DataSet changes)
        public static void SaveData()
        {
            //if (changes == null) return;

            DataSet ds_upd = _ds.GetChanges(DataRowState.Added | DataRowState.Modified);

            if (ds_upd != null)
            {
                _da.Update(ds_upd.Tables[0]);
                _ds.Merge(ds_upd);

                _ds.Tables[0].AcceptChanges();
            }
        }


        public static void DeleteData()
        {
            //if (changes == null) return;

            DataSet ds_upd = _ds.GetChanges(DataRowState.Deleted);
            
            if (ds_upd != null)
            {
                _da.Update(ds_upd.Tables[0]);
                _ds.Merge(ds_upd);

                _ds.Tables[0].AcceptChanges();
            }
        }


        public static void SaveAll()
        {
            //if (changes == null) return;

            DataSet ds_upd = _ds.GetChanges(DataRowState.Deleted);

            if (ds_upd != null)
            {
                _da.Update(ds_upd.Tables[0]);
                _ds.Merge(ds_upd);

                _ds.Tables[0].AcceptChanges();
            }

            ds_upd = _ds.GetChanges(DataRowState.Added | DataRowState.Modified);

            if (ds_upd != null)
            {
                _da.Update(ds_upd.Tables[0]);
                _ds.Merge(ds_upd);

                _ds.Tables[0].AcceptChanges();
            }

        }

    }

}


/*******************************************************************************************************************\
 *                                                                                                                 *
\*******************************************************************************************************************/

/*
namespace mdlAllyKE
{
    public sealed class dacTimebook
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
            DataColumn pk1 = table.Columns.Add("BookDate", typeof(DateTime)); pk1.AllowDBNull = false;
            DataColumn pk2 = table.Columns.Add("EmpID", typeof(String)); pk2.MaxLength = 8; pk2.AllowDBNull = false;
            DataColumn col3 = table.Columns.Add("EmpName", typeof(String)); col3.MaxLength = 64;
            DataColumn col4 = table.Columns.Add("ToffCode", typeof(String)); col4.MaxLength = 8;
            DataColumn col5 = table.Columns.Add("LogHours", typeof(Int32));
            DataColumn col6 = table.Columns.Add("LogOver", typeof(Int32));
            DataColumn col7 = table.Columns.Add("LogVessel", typeof(String)); col7.MaxLength = 16;
            DataColumn col8 = table.Columns.Add("LogShift", typeof(Int32));
            DataColumn col9 = table.Columns.Add("CreateyDate", typeof(DateTime));
            DataColumn col10 = table.Columns.Add("UpdateDate", typeof(DateTime));
            DataColumn col11 = table.Columns.Add("UserAudit", typeof(String)); col11.MaxLength = 32;
            
            table.PrimaryKey = new DataColumn[] { pk1, pk2 };
        }


        private static DataSet create_ds()
        {
            DataSet ds = new DataSet();
            DataTable tbl = ds.Tables.Add("TOFF");

            define_schema(tbl);

            DateTime nwk = new DateTime(2014, 3, 17);
            DateTime tod = DateTime.Now;

            //tbl.Rows.Add(nwk.AddDays(0).Date, "C6", "Bill COTTON", "O", 0, 0, "", 0, tod, tod, "<system>");
            //tbl.Rows.Add(nwk.AddDays(1).Date, "C6", "Bill COTTON", "O", 0, 0, "", 0, tod, tod, "<system>");
            //tbl.Rows.Add(nwk.AddDays(2).Date, "C6", "Bill COTTON", "O", 0, 0, "", 0, tod, tod, "<system>");
            //tbl.Rows.Add(nwk.AddDays(3).Date, "C6", "Bill COTTON", "O", 0, 0, "", 0, tod, tod, "<system>");
            //tbl.Rows.Add(nwk.AddDays(4).Date, "C6", "Bill COTTON", "O", 0, 0, "", 0, tod, tod, "<system>");
            //tbl.Rows.Add(nwk.AddDays(5).Date, "C6", "Bill COTTON", "O", 0, 0, "", 0, tod, tod, "<system>");
            //tbl.Rows.Add(nwk.AddDays(6).Date, "C6", "Bill COTTON", "O", 0, 0, "", 0, tod, tod, "<system>");

            //tbl.Rows.Add(nwk.AddDays(0).Date, "G5", "Shawn GERAK", "O", 0, 0, "", 0, tod, tod, "<system>");
            //tbl.Rows.Add(nwk.AddDays(1).Date, "G5", "Shawn GERAK", "O", 0, 0, "", 0, tod, tod, "<system>");
            //tbl.Rows.Add(nwk.AddDays(2).Date, "G5", "Shawn GERAK", "O", 0, 0, "", 0, tod, tod, "<system>");
            //tbl.Rows.Add(nwk.AddDays(3).Date, "G5", "Shawn GERAK", "O", 0, 0, "", 0, tod, tod, "<system>");
            //tbl.Rows.Add(nwk.AddDays(4).Date, "G5", "Shawn GERAK", "O", 0, 0, "", 0, tod, tod, "<system>");
            //tbl.Rows.Add(nwk.AddDays(5).Date, "G5", "Shawn GERAK", "O", 0, 0, "", 0, tod, tod, "<system>");
            //tbl.Rows.Add(nwk.AddDays(6).Date, "G5", "Shawn GERAK", "O", 0, 0, "", 0, tod, tod, "<system>");


            tbl.Rows.Add(tod.AddDays(0).Date, "C6", "Bill COTTON", "12", 0, 0, "HN", 1, tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(1).Date, "C6", "Bill COTTON", "12", 0, 0, "HN", 1, tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(2).Date, "C6", "Bill COTTON", "12", 0, 0, "HN", 1, tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(3).Date, "C6", "Bill COTTON", "12", 0, 0, "HN", 1, tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(4).Date, "C6", "Bill COTTON", "12", 0, 0, "HN", 1, tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(5).Date, "C6", "Bill COTTON", "12", 0, 0, "HN", 1, tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(6).Date, "C6", "Bill COTTON", "12", 0, 0, "HN", 1, tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(7).Date, "C6", "Bill COTTON", "12", 0, 0, "HN", 1, tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(8).Date, "C6", "Bill COTTON", "12", 0, 0, "HN", 1, tod, tod, "<system>");
            //tbl.Rows.Add(tod.AddDays(8).Date, "C6", "Bill COTTON", "O", 0, 0, "", 0, tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(9).Date, "C6", "Bill COTTON", "O", 0, 0, "", 0, tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(10).Date, "C6", "Bill COTTON", "O", 0, 0, "", 0, tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(11).Date, "C6", "Bill COTTON", "O", 0, 0, "", 0, tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(12).Date, "C6", "Bill COTTON", "O", 0, 0, "", 0, tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(13).Date, "C6", "Bill COTTON", "O", 0, 0, "", 0, tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(14).Date, "C6", "Bill COTTON", "12", 0, 0, "HN", 1, tod, tod, "<system>");

            tbl.Rows.Add(tod.AddDays(0).Date, "G3", "Roland GERAK", "12", 0, 0, "HR", 1, tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(1).Date, "G3", "Roland GERAK", "12", 0, 0, "HR", 1, tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(2).Date, "G3", "Roland GERAK", "12", 0, 0, "HR", 2, tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(3).Date, "G3", "Roland GERAK", "12", 0, 0, "HR", 3, tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(4).Date, "G3", "Roland GERAK", "12", 0, 0, "HR", 1, tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(5).Date, "G3", "Roland GERAK", "12", 0, 0, "HR", 1, tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(6).Date, "G3", "Roland GERAK", "12", 0, 0, "HR", 2, tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(7).Date, "G3", "Roland GERAK", "12", 0, 0, "HR", 3, tod, tod, "<system>");

            tbl.Rows.Add(tod.AddDays(-4).Date, "G5", "Shawn GERAK", "12", 0, 0, "HR", 1, tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(-3).Date, "G5", "Shawn GERAK", "12", 0, 0, "HR", 1, tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(-2).Date, "G5", "Shawn GERAK", "12", 0, 0, "HR", 1, tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(-1).Date, "G5", "Shawn GERAK", "12", 0, 0, "HR", 1, tod, tod, "<system>");

            tbl.Rows.Add(tod.AddDays(0).Date, "G5", "Shawn GERAK", "12", 0, 0, "HR", 1, tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(1).Date, "G5", "Shawn GERAK", "12", 0, 0, "HR", 1, tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(2).Date, "G5", "Shawn GERAK", "12", 0, 0, "HR", 1, tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(3).Date, "G5", "Shawn GERAK", "12", 0, 0, "HR", 1, tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(4).Date, "G5", "Shawn GERAK", "12", 0, 0, "HR", 1, tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(5).Date, "G5", "Shawn GERAK", "12", 0, 0, "HR", 1, tod, tod, "<system>");
            tbl.Rows.Add(tod.AddDays(6).Date, "G5", "Shawn GERAK", "12", 0, 0, "HR", 1, tod, tod, "<system>");            

            return ds;
        }


        public static DataSet GetDS(DateTime start_week, int days)
        {
            DataSet ds = create_ds();

            string where = string.Format("BookDate >= '{0}' and BookDate <= '{1}'",
                start_week.ToShortDateString(), start_week.AddDays(days).ToShortDateString());

            try
            {
                ds.Tables[0].DefaultView.RowFilter = where;
            }
            catch (Exception ex)
            {   
                MessageBox.Show(ex.Message);
            }
                
                
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
*/