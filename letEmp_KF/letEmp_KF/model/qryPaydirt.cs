using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Windows.Forms;


namespace letEmp_KF
{

    public sealed class qryPaydirt
    {


        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        static public DataTable qInner(DataTable tb_dt, DataTable pd_dt)
        {
            DataTable tgt = tb_dt.Clone();

            try
            {
                var add_cols = pd_dt.Columns.OfType<DataColumn>().Select(dc =>
                        new DataColumn(dc.ColumnName, dc.DataType, dc.Expression, dc.ColumnMapping));

                tgt.Columns.AddRange(add_cols.ToArray());

                var q = from tbl in tb_dt.AsEnumerable()
                        join j in pd_dt.AsEnumerable()
                            on tbl["EmpId"] equals j["strEmployee_number"]
                        select tbl.ItemArray.Concat(j.ItemArray).ToArray();

                foreach (object[] v in q)
                    tgt.Rows.Add(v);

            }
            catch (Exception ex)
            {
                errDash.Fail(ex);
            }

            return tgt;
        }

        //http://stackoverflow.com/questions/17684448/how-to-left-outer-join-two-datatables-in-c

        static public DataTable qOuter_timebook(DataTable tb_dt, DataTable pd_dt)
        {
            DataTable tgt = tb_dt.Clone();

            try
            {
                var add_cols = pd_dt.Columns.OfType<DataColumn>().Select(dc =>
                        new DataColumn(dc.ColumnName, dc.DataType, dc.Expression, dc.ColumnMapping));

                tgt.Columns.AddRange(add_cols.ToArray());

                var q = (from tbl in tb_dt.AsEnumerable()
                         join j in pd_dt.AsEnumerable()
                             on tbl["EmpId"] equals j["strEmployee_number"] into lft
                         from rgh in lft.DefaultIfEmpty()
                         select tbl.ItemArray.Concat((rgh == null) ? (pd_dt.NewRow().ItemArray) : rgh.ItemArray).ToArray()).ToList();


                foreach (object[] v in q)
                    tgt.Rows.Add(v);

            }
            catch (Exception ex)
            {
                errDash.Fail(ex);
            }

            return tgt;
        }


        static public DataTable qOuter_paydirt(DataTable pd_dt, DataTable tb_dt, bool all)
        {
            DataTable tgt = pd_dt.Clone();

            try
            {
                var add_cols = tb_dt.Columns.OfType<DataColumn>().Select(dc =>
                        new DataColumn(dc.ColumnName, dc.DataType, dc.Expression, dc.ColumnMapping));

                tgt.Columns.AddRange(add_cols.ToArray());

                var q = from tbl in pd_dt.AsEnumerable()
                        join j in tb_dt.AsEnumerable()
                            on tbl["strEmployee_number"] equals j["EmpId"] into lft
                        from rgh in lft.DefaultIfEmpty()
                        select tbl.ItemArray.Concat((rgh == null) ? (pd_dt.NewRow().ItemArray) : rgh.ItemArray).ToArray();


                foreach (object[] v in q)
                {
                    if (all)
                        tgt.Rows.Add(v);
                    else
                    {
                        DataRow row = tgt.NewRow();
                        row.ItemArray = v;

                        if (row["EmpId"].Equals(DBNull.Value) || row["EmpId"].Equals(string.Empty)) tgt.Rows.Add(row);
                    }
                }
                

                foreach (DataColumn col in tb_dt.Columns)
                    if (!col.ColumnName.Equals("EmpId")) tgt.Columns.Remove(col.ColumnName);

            }
            catch (Exception ex)
            {
                errDash.Error(string.Format("Error (qryPaydirt.qOuter_paydirt) : {0}", ex.Message));
            }

            return tgt;
        }


        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        static public DataTable qImport(DataTable tb_dt, DataTable pd_dt)
        {
            DataTable tgt = pd_dt.Clone();

            try
            {
                var add_cols = tb_dt.Columns.OfType<DataColumn>().Select(dc =>
                        new DataColumn(dc.ColumnName, dc.DataType, dc.Expression, dc.ColumnMapping));

                tgt.Columns.AddRange(add_cols.ToArray());

                var q = from tbl in pd_dt.AsEnumerable()
                        join j in tb_dt.AsEnumerable()
                            on tbl["strEmployee_number"] equals j["EmpId"] into lft
                        from rgh in lft.DefaultIfEmpty()
                        select tbl.ItemArray.Concat((rgh == null) ? (tb_dt.NewRow().ItemArray) : rgh.ItemArray).ToArray();
                

                //var rowDataLeftOuter = from rowLeft in dtblLeft.AsEnumerable()
                //                       join rowRight in dtblRight.AsEnumerable() on rowLeft[colToJoinOn] equals rowRight[strTempColName] into gj
                //                       from subRight in gj.DefaultIfEmpty()
                //                       select rowLeft.ItemArray.Concat((subRight == null) ? (dtblRight.NewRow().ItemArray) : subRight.ItemArray).ToArray();

                //var q = (from t1 in dtblA.Rows.Cast<DataRow>()
                //              join t2 in dtblB.Rows.Cast<DataRow>() on t1["col1"] equals t2["col1"]
                //              select t1).CopyToDataTable();


                //var q = (from tbl in pd_dt.AsEnumerable()
                //         join j in tb_dt.AsEnumerable()
                //             on tbl["strEmployee_number"] equals j["EmpId"] into lft
                //         from rgh in lft.DefaultIfEmpty( )
                //         select tbl).CopyToDataTable();


                //var q = from tbl in pd_dt.AsEnumerable()
                //        join j in tb_dt.AsEnumerable()
                //            on tbl["strEmployee_number"] equals j["EmpId"] into lft
                //        from rgh in lft.DefaultIfEmpty()
                //        //where lft.First().Field<string>("EmpId").Equals(DBNull.Value)
                //        //where lft.FirstOrDefault()["EmpId"].Equals(DBNull.Value)                        
                //        //select tbl.ItemArray;
                //        select new
                //        {
                //            num = tbl["strEmployee_number"].ToString(),
                //            //eid = (rgh["EmpId"].Equals(DBNull.Value) ? "1" : "2")
                //        };


                //MessageBox.Show(q.Rows.Count.ToString());
                foreach (object[] v in q)
                    tgt.Rows.Add(v);

            }
            catch (Exception ex)
            {
                errDash.Fail(ex);
            }

            return tgt;
        }


        /*******************************************************************************************************************\
        *                                                                                                                 *
        \*******************************************************************************************************************/

        static public DataTable xImport(DataTable tb_dt, DataTable pd_dt)
        {
            DataTable a_dt;
            DataTable b_dt;

            //DataTable r_dt = tb_dt.Clone();
            //r_dt.Merge(pd_dt.Clone());


            try
            {
                var q = from tbl in tb_dt.AsEnumerable()
                        join j in pd_dt.AsEnumerable()
                        on tbl["EmpId"] equals j["strEmployee_number"] into lft
                        from rgh in lft.DefaultIfEmpty()
                        select tbl;
                        //select new { tbl, rgh };                        
                        //select r_dt.LoadDataRow(new object[]
                        //select new
                        //{                                 
                            //tbl.Field<string>("EmpId")
                            //                EmpId = (string)tTime["EmpId"],
                            //                EmpName = (string)tTime["EmpName"],
                            //                Bookdate = (DateTime)tTime["Bookdate"],
                            //                LogHours = (Decimal)tTime["LogHours"],
                            //                LogOver = (Decimal)tTime["LogOver"],
                            //                LogVessel = (tTime["LogVessel"].Equals(DBNull.Value) ? "" : (string)tTime["LogVessel"]),
                            //LogVessel = (string)g.Key
                            //                Shift = (tTime["LogShift"].Equals(DBNull.Value) ? -1 : (int)tTime["LogShift"]),
                            //                ShiftHour = (tTime["LogHour"].Equals(DBNull.Value) ? "<>" : (string)tTime["LogHour"]),
                            //                ToffCode = (tTime["ToffCode"].Equals(DBNull.Value) ? "" : (string)tTime["ToffCode"]),                            
                            //                Resp = (tTime["Resp"].Equals(DBNull.Value) ? "" : (string)tTime["Resp"]),                           
                            //                LogNote = (tTime["LogNote"].Equals(DBNull.Value) ? "" : (string)tTime["LogNote"])
                        //}, true);
                        //};

                a_dt = q.CopyToDataTable();

                q = from tbl in tb_dt.AsEnumerable()
                    join j in pd_dt.AsEnumerable()
                    on tbl["EmpId"] equals j["strEmployee_number"] into lft
                    from rgh in lft.DefaultIfEmpty()
                    select rgh;

                b_dt = q.CopyToDataTable();
                
                
                //return libModel.TO_DATA_TABLE(q.ToList());                
            }
            catch (Exception ex)
            {
                errDash.Fail(ex);
            }

            return null;

            //        DataTable targetTable = dataTable1.Clone();
            //var dt2Columns = dataTable2.Columns.OfType<DataColumn>().Select(dc => 
            //    new DataColumn(dc.ColumnName, dc.DataType, dc.Expression, dc.ColumnMapping));
            //targetTable.Columns.AddRange(dt2Columns.ToArray());
            //var rowData =
            //    from row1 in dataTable1.AsEnumerable()
            //    join row2 in dataTable2.AsEnumerable()
            //        on row1.Field<int>("ID") equals row2.Field<int>("ID")
            //    select row1.ItemArray.Concat(row2.ItemArray).ToArray();
            //foreach (object[] values in rowData)
            //    targetTable.Rows.Add(values);

        }
    }
}