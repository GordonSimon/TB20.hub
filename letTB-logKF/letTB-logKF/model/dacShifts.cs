using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;


/// <summary>
/// Summary description for dacShifts
/// </summary>

namespace letTB_logKF
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
            table.Rows.Add(_legend_val[7], _legend_num[7], _legend_show[7], tod, tod, "<system>");
            //table.Rows.Add(_legend_val[6], _legend_num[6], _legend_show[6], tod, tod, "<system>");
            //table.Rows.Add(_legend_val[7], _legend_num[7], _legend_show[7], tod, tod, "<system>");

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
            table.Rows.Add(_legend_val[7], _legend_num[7], _legend_show[7], tod, tod, "<system>");
            //table.Rows.Add(_legend_val[6], _legend_num[6], _legend_show[6], tod, tod, "<system>");
            //table.Rows.Add(_legend_val[7], _legend_num[7], _legend_show[7], tod, tod, "<system>");

            
            //ds.EnforceConstraints = false;
            //_user_da.Fill(ds.Tables["USER"]);
            //ds.EnforceConstraints = true;

            return ds;
        }
    }
}
