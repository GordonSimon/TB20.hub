using System;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;

using System.Windows.Forms;



/// <summary>
/// Summary description for dacEmployees
/// </summary>

namespace letEmp_KF
{
    public sealed class dacEmployees
    {
        static public DataTable DT { get; set; }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private const string tblName = sqlConnect.DBO + "Employee";        

        static private SqlConnection _con = sqlConnect.Connect();
        static private SqlDataAdapter _da = create_DA();


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

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


        private static void da_select(SqlDataAdapter da)
        {
            string sql = "SELECT * FROM " + tblName;
            sql += " ORDER BY [Last Name]";
            
            da.SelectCommand = new SqlCommand(sql, _con);
        }


        private static void da_update(SqlDataAdapter da)
        {
            string usql = "UPDATE " + tblName + " SET [Last Name] = @LNAME";
            
            usql += ", [First Name]=@FNAME, Duty=@DUTY, Employment=@EMPLOYMENT, DefPayCode=@PAYCODE, Archive=@ARCH";
            
            usql += ", Master=@MAST";
            usql += ", [Master Certification]=@MASTCERT, [Master Expire Date]=@MASTDATE, [Master Classification]=@MASTCLAS";
            
            usql += ", [Marine Emergency Duties]=@MED, [Marine Med Exp Date]=@MEDDATE, [MED Certificate]=@MEDCERT";
            usql += ", [Marine Medical]=@MEDICAL, [Marine Medical Certificate Date]=@MEDICALDATE";

            usql += " WHERE EmpId=@ID";

            da.UpdateCommand = new SqlCommand(usql, _con);

            da.UpdateCommand.Parameters.Add("@LNAME", SqlDbType.NChar).SourceColumn = "Last Name";
            da.UpdateCommand.Parameters.Add("@FNAME", SqlDbType.NChar).SourceColumn = "First Name";
            da.UpdateCommand.Parameters.Add("@DUTY", SqlDbType.NChar).SourceColumn = "Duty";
            da.UpdateCommand.Parameters.Add("@EMPLOYMENT", SqlDbType.NChar).SourceColumn = "Employment";
            da.UpdateCommand.Parameters.Add("@PAYCODE", SqlDbType.NChar).SourceColumn = "DefPayCode";
            da.UpdateCommand.Parameters.Add("@ARCH", SqlDbType.Bit).SourceColumn = "Archive";

            da.UpdateCommand.Parameters.Add("@MAST", SqlDbType.Bit).SourceColumn = "Master";
            da.UpdateCommand.Parameters.Add("@MASTCERT", SqlDbType.NChar).SourceColumn = "Master Certification";
            da.UpdateCommand.Parameters.Add("@MASTDATE", SqlDbType.DateTime).SourceColumn = "Master Expire Date";
            da.UpdateCommand.Parameters.Add("@MASTCLAS", SqlDbType.NChar).SourceColumn = "Master Classification";

            da.UpdateCommand.Parameters.Add("@MED", SqlDbType.Bit).SourceColumn = "Marine Emergency Duties";
            da.UpdateCommand.Parameters.Add("@MEDCERT", SqlDbType.NChar).SourceColumn = "MED Certificate";
            da.UpdateCommand.Parameters.Add("@MEDDATE", SqlDbType.DateTime).SourceColumn = "Marine Med Exp Date";

            da.UpdateCommand.Parameters.Add("@MEDICAL", SqlDbType.Bit).SourceColumn = "Marine Medical";
            da.UpdateCommand.Parameters.Add("@MEDICALDATE", SqlDbType.DateTime).SourceColumn = "Marine Medical Certificate Date";
            
            //da.UpdateCommand.Parameters.Add("@VISIT", SqlDbType.Int).SourceColumn = "VisitCount";
            
            da.UpdateCommand.Parameters.Add("@ID", SqlDbType.NChar).SourceColumn = "EmpId";
        }


        private static void da_insert(SqlDataAdapter da)
        {
            string isql = "INSERT INTO " + tblName + " (";

            isql += "EmpId";
            isql += ", [First Name]";
            isql += ", [Last Name]";
            isql += ") VALUES (";

            isql += "@EID";
            isql += ", @FNAME";
            isql += ", @LNAME";
            isql += ")";

            da.InsertCommand = new SqlCommand(isql, _con);

            da.InsertCommand.Parameters.Add("@EID", SqlDbType.NChar).SourceColumn = "EmpId";
            da.InsertCommand.Parameters.Add("@LNAME", SqlDbType.NChar).SourceColumn = "Last Name";
            da.InsertCommand.Parameters.Add("@FNAME", SqlDbType.NChar).SourceColumn = "First Name";
            
            //da.InsertCommand.Parameters.Add("@UDATE", SqlDbType.DateTime).SourceColumn = "CreateDate";
            //da.InsertCommand.Parameters.Add("@CDATE", SqlDbType.DateTime).SourceColumn = "UpdateDate";
            //da.InsertCommand.Parameters.Add("@USER", SqlDbType.NChar).SourceColumn = "UserAudit";
        }


        private static SqlDataAdapter create_DA()
        {
            SqlDataAdapter da = new SqlDataAdapter();

            da_select(da);
            da_update(da);
            da_insert(da);

            return da;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private static void define_schema(DataTable table)
        {
            //DataColumn col1 = table.Columns.Add("ID", typeof(Int32));  //col1.AllowDBNull = false;
            //                                                            // Let SQL handle null

            DataColumn pk = table.Columns.Add("EmpId", typeof(String)); pk.MaxLength = 8; pk.AllowDBNull = false;
            //DataColumn col3 = table.Columns.Add("Last Name", typeof(String));  col3.MaxLength = 64;
            //DataColumn col4 = table.Columns.Add("First Name", typeof(String)); col4.MaxLength = 64;
            //DataColumn col5 = table.Columns.Add("Duty", typeof(String)); col5.MaxLength = 16;
            //DataColumn col6 = table.Columns.Add("Main Phone", typeof(String)); col6.MaxLength = 16;
            //DataColumn col7 = table.Columns.Add("Cell Phone", typeof(String)); col7.MaxLength = 16;

            //DataColumn col8 = table.Columns.Add("Master", typeof(bool));
            //DataColumn col9 = table.Columns.Add("Master Certification", typeof(String)); col9.MaxLength = 16;
            //DataColumn col10 = table.Columns.Add("Master Expire Date", typeof(DateTime));
            //DataColumn col11 = table.Columns.Add("Master Classification", typeof(String)); col11.MaxLength = 16;

            //DataColumn col12 = table.Columns.Add("Marine Emergency Duties", typeof(bool));
            //DataColumn col13 = table.Columns.Add("Marine Med Exp Date", typeof(DateTime));
            //DataColumn col14 = table.Columns.Add("MED Certificate", typeof(String)); col14.MaxLength = 32;
                                   
            //DataColumn col15 = table.Columns.Add("Marine Medical", typeof(bool));
            //DataColumn col16 = table.Columns.Add("Marine Medical Certificate Date", typeof(DateTime));

            //DataColumn col17 = table.Columns.Add("Employment", typeof(String)); col17.MaxLength = 16;
            
            //DataColumn col18 = table.Columns.Add("HireDate", typeof(DateTime));
            //DataColumn col19 = table.Columns.Add("Comments", typeof(String)); col19.MaxLength = 255;
            //DataColumn col20 = table.Columns.Add("Verification Date", typeof(DateTime));
            //DataColumn col21 = table.Columns.Add("VisitCount", typeof(Int32));
            //DataColumn col22 = table.Columns.Add("LastDate", typeof(DateTime));
            //DataColumn col23 = table.Columns.Add("Terminate", typeof(bool));
            //DataColumn col24 = table.Columns.Add("Termination Date", typeof(DateTime));

            //DataColumn col25 = table.Columns.Add("Archive", typeof(bool));

            //DataColumn col26 = table.Columns.Add("CreateDate", typeof(DateTime));
            //DataColumn col27 = table.Columns.Add("UpdateDate", typeof(DateTime));
            //DataColumn col28 = table.Columns.Add("UserAudit", typeof(String)); col28.MaxLength = 32;
            
            // OLD ONE
            //DataColumn col10 = table.Columns.Add("ADUser", typeof(String)); col10.MaxLength = 32;
            //DataColumn col11 = table.Columns.Add("AccessCard", typeof(bool));            
            //DataColumn col12 = table.Columns.Add("curPassword", typeof(String)); col12.MaxLength = 32;
            //DataColumn col13 = table.Columns.Add("PasswordStrength", typeof(String)); col13.MaxLength = 16;
            //DataColumn col14 = table.Columns.Add("AlarmCode", typeof(String)); col14.MaxLength = 8;
            //DataColumn col15 = table.Columns.Add("AssignedAlarmCode", typeof(String)); col15.MaxLength = 8;
            //DataColumn col16 = table.Columns.Add("HireDate", typeof(DateTime));
            //DataColumn col17 = table.Columns.Add("Comments", typeof(String)); col16.MaxLength = 255;
            //DataColumn col18 = table.Columns.Add("VerificationDate", typeof(DateTime));
            //DataColumn col19 = table.Columns.Add("VisitCount", typeof(Int32));
            //DataColumn col20 = table.Columns.Add("LastDate", typeof(DateTime));
            //DataColumn col21 = table.Columns.Add("Active", typeof(bool));
            //DataColumn col22 = table.Columns.Add("TerminationDate", typeof(DateTime));
            //DataColumn col23 = table.Columns.Add("PasswordDate", typeof(DateTime));
            //DataColumn col24 = table.Columns.Add("PasswordCount", typeof(Int32));
            // END OF OLD ONE

            table.PrimaryKey = new DataColumn[] { pk };
        }


        private static DataTable create_dt()
        {
            DataTable tbl = new DataTable();

            define_schema(tbl);
            //load_ds(tbl);

            return tbl;
        }


        private static DataSet create_ds()
        {
            DataSet ds = new DataSet();
            DataTable tbl = ds.Tables.Add("EMPLOYEE");

            define_schema(tbl);
            //load_ds(tbl);
        
            return ds;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public static void MoveColumn(DataTable dt, string name)
        {
            dt.Columns[name].SetOrdinal(dt.Columns.Count - 1);
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public static void LoadDT()
        {
            if (DT == null)
                DT = create_dt();

            _da.Fill(DT);
        }


        public static void SaveDT()
        {
            DataTable dt = DT.GetChanges(DataRowState.Modified);
            if (dt != null)
            {
                _da.Update(dt);
                DT.Merge(dt);
            }
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public static DataTable GetDT()
        {
            DataTable dt;

            dt = create_dt();
            
            _da.Fill(dt);           

            return dt;
        }

       
        public static DataSet GetDS()
        {
            DataSet ds = create_ds();

            ds.EnforceConstraints = false;
            _da.Fill(ds.Tables["EMPLOYEE"]);
            ds.EnforceConstraints = true;

            return ds;
        }


        public static DataSet GetKey(string emp_id)
        {
            DataSet ds = create_ds();

            ds.EnforceConstraints = false;
            _da.Fill(ds.Tables["EMPLOYEE"]);
            ds.EnforceConstraints = true;

            string where = string.Format("EmpId = '{0}'", emp_id);               

            try
            {
                DataRow[] rows = ds.Tables[0].Select(where);
                if (rows.Length == 0) return null;

                DataTable dt = rows.CopyToDataTable();
                ds.Tables.Clear();
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("dacEmployee.GetKey : " + ex.Message);
            }

            return ds;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public static void AddKey(string emp_id, string fname, string lname)
        {
            DataSet ds = create_ds();

            ds.EnforceConstraints = false;
            _da.Fill(ds.Tables["EMPLOYEE"]);
            ds.EnforceConstraints = true;

            DataTable dt = ds.Tables["EMPLOYEE"];
            DataRow row = dt.NewRow();
            //row["ID"] = -1;
            row["EmpId"] = emp_id;
            row["Last Name"] = lname;
            row["First Name"] = fname;            

            dt.Rows.Add(row);

            DataSet ds_ins = ds.GetChanges(DataRowState.Added);
            if (ds_ins != null)
            {
                _da.Update(ds_ins.Tables[0]);
                ds.AcceptChanges();
            }
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public static void SaveData(DataSet changes)
        {
            DataSet ds_upd = changes.GetChanges(DataRowState.Modified);            

            if (ds_upd != null)
            {
                _da.Update(ds_upd.Tables[0]);
                changes.Merge(ds_upd);
            }
        }


        
        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public static void InsertRow(string employee_number)
        {
            //SqlCommand myCommand = new SqlCommand(query, myConnection);
            //myCommand.Parameters.AddWithValue("@ArticleTitle", ArticleTitleTextBox.Text);
            //myCommand.Parameters.AddWithValue("@ArticleContent", ArticleContentTextBox.Text);
            //// ... other parameters
            //myCommand.ExecuteNonQuery();
        }

        
        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public static int ProcInsert(string emp_id, string firstname, string lastname)
        {
            int retval = 0;

            using (SqlCommand cmd = new SqlCommand("dbproc_Import_Employee", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Empid", SqlDbType.VarChar).Value = emp_id;
                cmd.Parameters.Add("@First_name", SqlDbType.VarChar).Value = firstname;
                cmd.Parameters.Add("@Last_name", SqlDbType.VarChar).Value = lastname;
                
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


        public static int ProcUpdate(int id, string emp_id, string firstname, string lastname)
        {
            int retval = 0;

            using (SqlCommand cmd = new SqlCommand("dbproc_Update_Employee", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cmd.Parameters.Add("@Empid", SqlDbType.VarChar).Value = emp_id;
                cmd.Parameters.Add("@First_name", SqlDbType.VarChar).Value = firstname;
                cmd.Parameters.Add("@Last_name", SqlDbType.VarChar).Value = lastname;

                var retcode = cmd.Parameters.Add("@Retcode", SqlDbType.Int);

                retcode.Direction = ParameterDirection.ReturnValue;                

                cmd.ExecuteNonQuery();

                retval = (int)cmd.Parameters["@Retcode"].Value;
            }

            return retval;
        }

    }
}
