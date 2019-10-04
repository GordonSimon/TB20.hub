using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Microsoft.Office.Interop.Excel;

using mdlAllyKE;


namespace viewAllyKE
{
    public partial class ucRptTimbebook : UserControl
    {
        private System.Data.DataTable _dt_gang { get; set; }

        //private Dictionary<string, string[]> _emp_detail;


        //public ucRptTimbebook(DataSet ds_emp)
        public ucRptTimbebook(System.Data.DataTable dt_gang)
        {
            InitializeComponent();

            _dt_gang = dt_gang;

            optSummary.Checked = true;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void mk_report(DateTime start_date, System.Data.DataTable dt)
        {
            List<Dictionary<string, string[]>> data;
            List<Dictionary<string, int>> summary;

            EmployeeSummary es = new EmployeeSummary(start_date, dt);            

            data = es.EmpData;
            summary = es.Summary;
            //excel_report(data, summary);
            RptSummary rpt = new RptSummary();
            rpt.excel_report(data, summary);

        }


        private void mk_import(DateTime start_date, System.Data.DataTable dt)
        {
            List<Dictionary<string, object[][]>> data;

            EmployeeImport ei = new EmployeeImport(start_date, dt);

            data = ei.EmpData;

            RptImport rpt = new RptImport();
            rpt.excel_report(start_date, data);

        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        private void cmdOK_Click(object sender, EventArgs e)
        {
            DateTime start_date = dtpStart.Value.Date;

            if (optSummary.Checked)
                mk_report(start_date, _dt_gang);


            if (optImport.Checked)
                mk_import(start_date, _dt_gang);

            this.ParentForm.Close();
        }


        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }


        private void test()
        {

            //aRange.Cells.Font.Size = 8;                        
            //Object[] args = new Object[1];
            //args[0] = 6;
            //aRange.GetType().InvokeMember("Value", System.Reflection.BindingFlags.SetProperty, null, aRange, args);



            //int rows = 1;
            //int columns = 32;
            //var data = new object[rows, columns];                                                                                                        
            //for (var row = 1; row <= rows; row++)
            //{
            //    for (var column = 1; column <= columns; column++)
            //    {
            //        data[row - 1, column - 1] = column.ToString();
            //    }
            //}

            //var startCell = (Range)ws.Cells[1, 1];
            //var endCell = (Range)ws.Cells[rows, columns];
            //var writeRange = ws.Range[startCell, endCell];
            //writeRange.Value2 = data;

            //aRange.Value2 = data;


            //aRange.Value2 = 21;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        internal class EmployeeImport
        {
            private DataSet _ds_emp { get; set; }

            private DataSet _ds_crew { get; set; }
            private DataSet _ds_sched { get; set; }


            private object[] sample = {                
                "A6",
                "ANZINGER",
                "X", "X", "X", "X", "X", "X", "X", "X", "X", "X",
                "X", "X", "X", "X", "X", "X", "X", "X", "X", "X",
                "X", "X", "X", "X", "X", "X", "X", "X", "X", "X",
                "X",
                "0"
                            };


            private Dictionary<string, object[][]> _details { get; set; }
            public List<Dictionary<string, object[][]>> EmpData { get; set; }



            public EmployeeImport(DateTime start_date, System.Data.DataTable dt_gang)
            {
                EmpData = new List<Dictionary<string, object[][]>>();

                if (dt_gang == null)
                {
                    MessageBox.Show("Warning : No data to report !", "EmployeeImport", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DateTime monthRef = new DateTime(start_date.Year, start_date.Month, 1);
                int days_in_month = DateTime.DaysInMonth(start_date.Year, start_date.Month);

                System.Data.DataTable tdt = qrySummary.GetView("TimeBook", monthRef, days_in_month, null, dt_gang);

                foreach (DataRow row in dt_gang.Rows)
                {
                    bool check = (bool)(row["Active"]);
                    if (!check) continue;

                    _details = new Dictionary<string, object[][]>();
                    EmpData.Add(_details);

                    string emp_id = (string)row["EmpId"];

                    object[][] detail = new object[4][];

                    detail[0] = new object[sample.Length];
                    detail[1] = new object[sample.Length];
                    detail[2] = new object[sample.Length];
                    detail[3] = new object[sample.Length];

                    detail[0][0] = emp_id;
                    detail[0][1] = row["EmpName"].ToString();
                    detail[0][sample.Length - 1] = "0";

                    detail[1][0] = "";
                    detail[1][1] = "O/T Hours";
                    detail[1][sample.Length - 1] = "0";

                    detail[2][0] = "";
                    detail[2][1] = "Boat Name";
                    detail[2][sample.Length - 1] = "";

                    detail[3][0] = "";
                    detail[3][1] = "";
                    detail[3][sample.Length - 1] = "";


                    tdt.DefaultView.RowFilter = string.Format("EmpId='{0}'", emp_id);
                    DataView dv = tdt.DefaultView;

                    foreach (DataRowView drv in dv)
                    {
                        DateTime bookdate = (DateTime)drv["Bookdate"];
                        int idx = bookdate.Day + 1;

                        detail[0][idx] = drv["Top"];
                        detail[1][idx] = drv["Mid"];
                        detail[2][idx] = drv["Bot"];
                        detail[3][idx] = drv["Note"];
                    }

                    _details.Add(monthRef.Month.ToString(), detail);

                }

            }


            /*******************************************************************************************************************\
             *                                                                                                                 *
            \*******************************************************************************************************************/

            //public EmployeeImport(DateTime start_date, System.Data.DataTable dt)
            //{
            //    EmpData = new List<Dictionary<string, object[][]>>();

            //    if (dt == null)
            //    {
            //        MessageBox.Show("Warning : No data to report !", "EmployeeImport", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return;
            //    }

            //    DateTime monthRef = new DateTime(start_date.Year, start_date.Month, 1);
            //    int days_in_month = DateTime.DaysInMonth(start_date.Year, start_date.Month);

            //    foreach (DataRow row in dt.Rows)
            //    {
            //        bool check = (bool)(row["Active"]);
            //        if (!check) continue;

            //        _details = new Dictionary<string, object[][]>();
            //        EmpData.Add(_details);

            //        object[][] detail = new object[3][];

            //        detail[0] = new object[sample.Length];
            //        detail[1] = new object[sample.Length];
            //        detail[2] = new object[sample.Length];

            //        detail[0][0] = row["EmpID"].ToString();
            //        detail[0][1] = row["EmpName"].ToString();
            //        detail[0][sample.Length - 1] = "0";

            //        detail[1][0] = "";
            //        detail[1][1] = "O/T Hours";
            //        detail[1][sample.Length - 1] = "0";

            //        detail[2][0] = "";
            //        detail[2][1] = "Boat Name";
            //        detail[2][sample.Length - 1] = "";


            //        System.Data.DataTable tds = qrySummary.GetView("Employee", monthRef, days_in_month, row["EmpID"].ToString(), null);

            //        foreach (DataRow trw in tds.Rows)
            //        {
            //            DateTime bookdate = (DateTime)trw["Bookdate"];
            //            int idx = bookdate.Day + 1;

            //            detail[0][idx] = trw["Top"];
            //            detail[1][idx] = trw["Mid"];
            //            detail[2][idx] = trw["Bot"];
            //        }

            //        _details.Add(monthRef.Month.ToString(), detail);

            //    }

            //}
        


        //    public EmployeeImport(DateTime start_date, System.Data.DataTable dt)
        //    {
        //        EmpData = new List<Dictionary<string, object[][]>>();

        //        if (dt == null)
        //        {
        //            MessageBox.Show("Warning : No data to report !", "EmployeeImport", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            return;
        //        }
                
        //        DateTime monthRef = new DateTime(start_date.Year, start_date.Month, 1);
        //        int days_in_month = DateTime.DaysInMonth(start_date.Year, start_date.Month);

        //        foreach (DataRow row in dt.Rows)
        //        {
        //            bool check = (bool)(row["Active"]);
        //            if (!check) continue;
                    
        //            _details = new Dictionary<string, object[][]>();
        //            EmpData.Add(_details);

        //            object[][] detail = new object[3][];

        //            detail[0] = new object[sample.Length];
        //            detail[1] = new object[sample.Length];
        //            detail[2] = new object[sample.Length];

        //            detail[0][0] = row["EmpID"].ToString();
        //            detail[0][1] = row["EmpName"].ToString();
        //            detail[0][sample.Length - 1] = "0";
                    
        //            detail[1][0] = "";
        //            detail[1][1] = "O/T Hours";
        //            detail[1][sample.Length - 1] = "0";

        //            detail[2][0] = "";
        //            detail[2][1] = "Boat Name";
        //            detail[2][sample.Length - 1] = "";

                    
        //            System.Data.DataTable tds = qrySummary.GetView("Import", monthRef, days_in_month, row["EmpID"].ToString());

        //            DateTime bookdate = DateTime.MinValue;
        //            decimal hour = 0;
        //            decimal over = 0;
        //            string toff = string.Empty;
        //            string vessel = string.Empty;
        //            int row_count = 0;

        //            foreach (DataRow trw in tds.Rows)
        //            {                        
        //                if (row_count != 0 && bookdate.CompareTo(trw["Bookdate"]) != 0)
        //                {
        //                    int idx = bookdate.Day + 1;
        //                    detail[0][idx] = (hour == decimal.Zero ? string.Empty : string.Format("{0:0.0}", hour));
        //                    detail[1][idx] = (over == decimal.Zero ? string.Empty : string.Format("{0:0.0}", over));
        //                    detail[2][idx] = string.Format("{0}", vessel);
                            
        //                    if (row_count > 1)  detail[2][idx] = string.Format("{0} [{1}]", vessel, row_count);
        //                    if (hour == decimal.Zero && over == decimal.Zero && toff.Length == 0 && vessel.Length == 0) detail[2][idx] = "*";

        //                    if (hour == decimal.Zero)
        //                    {
        //                        if (toff.Equals("12")) 
        //                            detail[0][idx] = "!12";
        //                        else
        //                            detail[0][idx] = toff;
        //                    }
        //                    else
        //                    {
        //                        if (toff.Length != 0)  detail[0][idx] = string.Format("{0:0.0} <{1}>", hour, toff);
        //                    }                                                     

        //                    row_count = 0;
        //                }

        //                if (row_count == 0)
        //                {
        //                    bookdate = (DateTime)trw["Bookdate"];
        //                    hour = 0;
        //                    over = 0;
        //                    toff = string.Empty;
        //                    vessel = string.Empty;

        //                    row_count = 0;
        //                }

        //                row_count += 1;
        //                hour += Convert.ToDecimal(trw["LogHours"]);
        //                over += Convert.ToDecimal(trw["LogOver"]);
        //                if (toff.Length == 0)
        //                    toff = (string)trw["ToffCode"];
        //                else
        //                {
        //                    if (((string)trw["ToffCode"]).Length != 0) toff += "/" + (string)trw["ToffCode"];
        //                }

        //                if (vessel.Length == 0)
        //                    if (((string)trw["LogVessel"]).Length != 0) vessel = (string)trw["LogVessel"];
        //                else
        //                {
        //                    if (((string)trw["LogVessel"]).Length != 0) toff += "/" + (string)trw["LogVessel"];
        //                }

        //            }

        //            if (row_count > 0)  // catch the edge
        //            {
        //                int idx = bookdate.Day + 1;
        //                detail[0][idx] = (hour == decimal.Zero ? string.Empty : string.Format("{0:0.0}", hour));
        //                detail[1][idx] = (over == decimal.Zero ? string.Empty : string.Format("{0:0.0}", over));
        //                detail[2][idx] = string.Format("{0}", vessel);

        //                if (row_count > 1) detail[2][idx] = string.Format("{0} [{1}]", vessel, row_count);
        //                if (hour == decimal.Zero && over == decimal.Zero && toff.Length == 0 && vessel.Length == 0) detail[2][idx] = "*";

        //                if (hour == decimal.Zero)
        //                {
        //                    if (toff.Equals("12"))
        //                        detail[0][idx] = "!12";
        //                    else
        //                        detail[0][idx] = toff;
        //                }
        //                else
        //                {
        //                    if (toff.Length != 0) detail[0][idx] = string.Format("{0:0.0} <{1}>", hour, toff);
        //                }
        //            }                             

        //            _details.Add(monthRef.Month.ToString(), detail);                    


        //            //DataSet eds = dacTimebook.GetDSbyEmpId(monthRef, 31, row["EmpID"].ToString());
        //            //foreach (DataRowView drv in eds.Tables[0].DefaultView)                    
        //            //{
        //            //    var bookdate_ = drv["Bookdate"];

        //            //    if (bookdate_ == DBNull.Value) continue;
        //            //    DateTime bookdate = (DateTime)bookdate_;
        //            //    if (bookdate < monthRef) continue;
        //            //    if (bookdate >= monthRef.AddDays(days_in_month)) continue;

        //            //    int idx = bookdate.Day + 1;
                       
        //            //    //decimal hours = Convert.ToDecimal(drv["LogHours"]);
        //            //    //if (hours == decimal.Zero)
        //            //    //    detail[0][idx] = drv["ToffCode"].ToString();
        //            //    //elsef
        //            //    //    detail[0][idx] = hours;


        //            //    decimal hours = Convert.ToDecimal(drv["LogHours"]);
        //            //    string sumkey = hours.ToString("#.#");
        //            //    if (hours == decimal.Zero)
        //            //    {
        //            //        if (drv["ToffCode"].ToString().Equals("12"))
        //            //            sumkey = "!";
        //            //        else
        //            //            sumkey = drv["ToffCode"].ToString();
        //            //    }
        //            //    detail[0][idx] = sumkey;
        //            //    //if (detail[0][idx] == null)  detail[0][idx] = sumkey; else detail[0][idx] += "/" + sumkey;

        //            //    decimal over = Convert.ToDecimal(drv["LogOver"]);
        //            //    string ovrkey = over.ToString("#.#");
        //            //    if (over == decimal.Zero)
        //            //        ovrkey = string.Empty;
        //            //    detail[1][idx] = ovrkey;
        //            //    //if (detail[1][idx] == null) detail[1][idx] = ovrkey; else detail[1][idx] += ovrkey;
        //            //    //detail[1][idx] = row_count;

        //            //    string vslkey = string.Empty;
        //            //    if (! drv["LogVessel"].Equals(DBNull.Value) && ! ((string)drv["LogVessel"]).Equals(new string(' ', 16)))
        //            //        vslkey = (string)drv["LogVessel"];
        //            //    detail[2][idx] = vslkey;
        //            //    if (detail[2][idx] == null) detail[2][idx] = vslkey; else detail[2][idx] += "/" + vslkey;
        //            //}

        //            //_details.Add(monthRef.Month.ToString(), detail);                    
        //        }

        //    }

        }

        
    /*******************************************************************************************************************\
     *                                                                                                                 *
    \*******************************************************************************************************************/

        internal class EmployeeSummary
        {
            private DataSet _ds_emp { get; set; }

            private DataSet _ds_crew { get; set; }
            private DataSet _ds_sched { get; set; }


            private string[] sample = {
                "Jan",
                "13",
                "A6",
                "ANZINGER",
                "X", "X", "X", "X", "X", "X", "X", "X", "X", "X",
                "X", "X", "X", "X", "X", "X", "X", "X", "X", "X",
                "X", "X", "X", "X", "X", "X", "X", "X", "X", "X",
                "X",
                "No"
                            };

            
            private Dictionary<string, string[]> _details { get; set; }
            public List<Dictionary<string, string[]>> EmpData { get; set; }

            private Dictionary<string, int> _summary { get; set; }
            public List<Dictionary<string, int>> Summary { get; set; }

            public EmployeeSummary(DateTime start_date, System.Data.DataTable dt)
            {
                EmpData = new List<Dictionary<string, string[]>>();
                Summary = new List<Dictionary<string, int>>();

                if (dt == null)
                {
                    MessageBox.Show("No employees selected !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (DataRow row in dt.Rows)
                {
                    bool check = (bool)(row["Active"]);
                    if (!check) continue;

                    DateTime monthRef = new DateTime(start_date.Year, 1, 1);

                    _details = new Dictionary<string, string[]>();
                    EmpData.Add(_details);

                    _summary = new Dictionary<string, int>();
                    Summary.Add(_summary);

                    for (int i = 0; i < 12; i++)
                    {
                        string[] detail = new string[sample.Length];

                        detail[0] = monthRef.ToString("MMM");
                        detail[1] = monthRef.ToString("yy");
                        detail[2] = row["EmpID"].ToString();
                        detail[3] = row["EmpName"].ToString();
                        detail[sample.Length - 1] = "No";


                        DataSet eds = dacTimebook.GetDSbyEmpId(monthRef, 31, row["EmpID"].ToString());

                        foreach (DataRowView drv in eds.Tables[0].DefaultView)
                        {
                            var bookdate_ = drv["Bookdate"];

                            if (bookdate_ == DBNull.Value) continue;
                            DateTime bookdate = (DateTime)bookdate_;
                            if (bookdate < monthRef) continue;
                            if (bookdate >= monthRef.AddDays(31)) continue;

                            int idx = bookdate.Day + 3;

                            string sumkey = "DW";
                            decimal hours = Convert.ToDecimal(drv["LogHours"]);
                            if (hours == decimal.Zero)
                                sumkey = drv["ToffCode"].ToString();                                

                            detail[idx] = sumkey;

                            if (_summary.ContainsKey(sumkey))
                                ++(_summary[sumkey]);
                            else
                                _summary.Add(sumkey, 1);

                        }

                        _details.Add(detail[0], detail);                        
                        monthRef = monthRef.AddMonths(1);
                    }
                }
                
            }
        }


    /*******************************************************************************************************************\
     *                                                                                                                 *
    \*******************************************************************************************************************/

        internal class RptImport
        {
            private Microsoft.Office.Interop.Excel._Application _xlApp;

            public RptImport()
            {
                _xlApp = new Microsoft.Office.Interop.Excel.Application();
                if (_xlApp == null) MessageBox.Show("Error (Excel Interop) : cannot start !");

                _xlApp.Visible = true;
            }


            /*******************************************************************************************************************\
             *                                                                                                                 *
            \*******************************************************************************************************************/

            private void mk_headings(DateTime start_date, Range rng)
            {
                string[] head = {
                "",
                "Jan-14",
                "1st", "2nd", "3rd", "4th", "5th", "6th", "7th", "8th", "9th", "10th",
                "11th", "12th", "13th", "14th", "15th", "16th", "17th", "18th", "19th", "20th",
                "21st", "22nd", "23rd", "24th", "25th", "26th", "27th", "28th", "29th", "30th",
                "31st",
                "TOTAL"
                            };

                head[1] = start_date.ToString("MMMM/yyyy");

                rng.Interior.Color = XlRgbColor.rgbLightBlue;
                rng.Cells.Font.Size = 10;
                rng.Borders.LineStyle = XlLineStyle.xlContinuous;
                rng.Cells.BorderAround2(XlLineStyle.xlContinuous, XlBorderWeight.xlThick);
                

                rng.Value2 = head;
               
                rng[1, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                
            }


            private void mk_row(Range rng, object[][] data)
            {
                rng.Cells.Font.Size = 10;
                rng.Value2 = data[0];
                rng.Offset[0].Interior.Color = XlRgbColor.rgbLightGoldenrodYellow;

                rng.Offset[1].Cells.Font.Size = 10;
                rng.Offset[1].Value2 = data[1];
                rng[2, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                

                rng.Offset[2].Cells.Font.Size = 10;
                rng.Offset[2].Value2 = data[2];
                rng[3, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

                int col = 1;
                foreach (string s in data[0])
                {
                    if (s != null && s.Equals("!12"))
                        rng[1, col].Font.Color = Color.Red;
                    col++;
                }

                col = 1;
                foreach (string s in data[3])
                {
                    if (s != null  && s.Length != 0)
                        rng[1, col].AddComment(s);
                    col++;
                }

                rng[1, 34].FormulaR1C1 = "=COUNT(R[0]C[-31]:R[0]C[-1])";
                rng[2, 34].FormulaR1C1 = "=SUM(R[0]C[-31]:R[0]C[-1])";
            }


            private void mk_employee(Range rng, Dictionary<string, object[][]> details)
            {
                int range_rows = rng.Rows.Count;
                int range_cols = rng.Columns.Count;

                //Range border_range = rng.Offset[0, 0].Resize[range_rows + 12, range_cols];
                Range border_range = rng.Offset[1, 0].Resize[range_rows + 2, range_cols];

                Borders border = border_range.Borders;
                border.LineStyle = XlLineStyle.xlContinuous;
                

                border_range.Cells.BorderAround2(XlLineStyle.xlContinuous, XlBorderWeight.xlThick);

                //FormatCondition format = border_range.Rows.FormatConditions.Add(XlFormatConditionType.xlExpression,                
                //    XlFormatConditionOperator.xlEqual, "=MOD(ROW(),2) = 0");
                //format.Interior.Color = XlRgbColor.rgbLightBlue;                


                int idx = 1;
                foreach (var detail in details)
                {
                    mk_row(rng.Offset[idx], detail.Value);
                    idx += 3;
                }
            }


            /*******************************************************************************************************************\
             *                                                                                                                 *
            \*******************************************************************************************************************/

            public void excel_report(DateTime start_date, List<Dictionary<string, object[][]>> data)
            {
                Workbook wb = _xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                Worksheet ws = (Worksheet)wb.Worksheets[1];
                if (ws == null) MessageBox.Show("Error (Excel Interop) : No worksheet !");

                Range rng = ws.get_Range("A1", "AH1");
                if (rng == null) MessageBox.Show("Error (Excel Interop) : Range problem ");

                mk_headings(start_date, rng);
                

                //var item = data[0];
                //string emp_name = (string)(item[start_date.Month.ToString()])[0][1];
                //mk_employee(rng, item);

                

                int idx = 0;
                foreach (var item in data)
                {
                    //string emp_name1 = (string)(item[start_date.Month.ToString()])[1][idx];
                    
                    mk_employee(rng, item);
                    idx++;
                    rng = rng.Offset[3, 0];
                }

                ws.Columns.AutoFit();

                _xlApp.WindowState = XlWindowState.xlMaximized;            

            }


            /*******************************************************************************************************************\
             *                                                                                                                 *
            \*******************************************************************************************************************/

            public void EndReport()
            {
            }
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        internal class RptSummary
        {
            private Microsoft.Office.Interop.Excel._Application _xlApp;

            public RptSummary()
            {
                _xlApp = new Microsoft.Office.Interop.Excel.Application();
                if (_xlApp == null) MessageBox.Show("Error (Excel Interop) : cannot start !");

                _xlApp.Visible = true;
            }


            /*******************************************************************************************************************\
             *                                                                                                                 *
            \*******************************************************************************************************************/

            private void mk_headings(Range rng)
            {
                string[] head = {
                "iMonth",
                "iYear",
                "ID",
                "Name_sur",
                "1st", "2nd", "3rd", "4th", "5th", "6th", "7th", "8th", "9th", "10th",
                "11th", "12th", "13th", "14th", "15th", "16th", "17th", "18th", "19th", "20th",
                "21st", "22nd", "23rd", "24th", "25th", "26th", "27th", "28th", "29th", "30th",
                "31st",
                "Office"
                            };

                rng.Interior.Color = XlRgbColor.rgbLightBlue;
                rng.Cells.Font.Size = 8;

                rng.Value2 = head;
            }


            private void mk_row(Range rng, string[] data)
            {
                rng.Cells.Font.Size = 8;

                rng.Value2 = data;
            }


            private void mk_employee(Range rng, Dictionary<string, string[]> details)
            {
                int range_rows = rng.Rows.Count;
                int range_cols = rng.Columns.Count;

                Range border_range = rng.Offset[0, 0].Resize[range_rows + 12, range_cols];

                Borders border = border_range.Borders;
                border.LineStyle = XlLineStyle.xlContinuous;

                //FormatCondition format = border_range.Rows.FormatConditions.Add(XlFormatConditionType.xlExpression,                
                //    XlFormatConditionOperator.xlEqual, "=MOD(ROW(),2) = 0");
                //format.Interior.Color = XlRgbColor.rgbLightBlue;

                mk_headings(rng);

                int idx = 0;
                foreach (var detail in details)
                    mk_row(rng.Offset[++idx], detail.Value);
            }


            /*******************************************************************************************************************\
             *                                                                                                                 *
            \*******************************************************************************************************************/

            //static public string[] _legend_val = new[] {
            //    "12", // 0
            //    "BL", // 1 x14
            //    "CL", // 2
            //    "D",  // 17
            //    "H",  // 3 x6
            //    "LD", // 4 x13
            //    "LO", // 5 x11
            //    "NW", // 6 x2
            //    "O",  // 7 x10
            //    "OT", // 8
            //    "Q",  // 9
            //    "S",  // 10 x9
            //    "SH", // 11
            //    "SL", // 12
            //    "T",  // 13 x12
            //    "TR", // 14
            //    "U",  // 15 x3
            //    "W",  // 16          
            //    "WI", // 18 x7
            //    "WS"  // 19
            // WCB              x5
            // UB               x4
            // STAT (SH)            x8
            // DW               x1
            //};

            private void mk_summary(Range rng, string emp_name, Dictionary<string, int> summary)
            {
                object[,] data = { 
                { "ANZINGER", "", "", "" },

                { "DW", 0,       "O", 0 },  //1 10
                { "NW", 0,       "LO", 0 }, //2 11
                { "U", 0,        "<blank>", 0 }, //3
                { "UB", 0,       "T", 0 }, //4 12
                { "W", 0,      "OT", 0 }, //5
                { "H", 0,        "LD", 0 },//6 13
                { "WI", 0,       "D", 0 }, //7
                { "SH", 0,       "BL", 0 }, //8 14
                { "S", 0,        "!12", 0 },  //9
                { "Stl", 0,    "Total", 0 } 
                            };


                int range_rows = rng.Rows.Count;
                int range_cols = rng.Columns.Count;

                Range border_range = rng.Offset[1, 0].Resize[range_rows - 1, range_cols];

                Borders border = border_range.Borders;
                border.LineStyle = XlLineStyle.xlContinuous;
                border.Weight = XlBorderWeight.xlMedium;

                Range col1 = rng.Offset[1, 0].Resize[range_rows - 1, 1];
                Range col2 = rng.Offset[1, 2].Resize[range_rows - 1, 1];
                Range hilight = rng.Offset[1, 2].Resize[range_rows - 7, 2];

                col1.Interior.Color = XlRgbColor.rgbLightBlue;
                col2.Interior.Color = XlRgbColor.rgbLightBlue;
                hilight.Interior.Color = XlRgbColor.rgbYellow;


                data[0, 0] = emp_name;

                int data_rows = data.GetLength(0) - 1;
                for (int i = 1; i < data_rows; i++)
                {
                    string nm = (string)data[i, 0];

                    int subtotal = 0;
                    if (summary.ContainsKey(nm)) subtotal = summary[nm];

                    data[i, 1] = subtotal;

                    nm = (string)data[i, 2];
                    subtotal = 0;
                    if (summary.ContainsKey(nm)) subtotal = summary[nm];

                    data[i, 3] = subtotal.ToString();
                }

                //rng.NumberFormat = "0";

                rng.Cells.Font.Size = 8;
                rng.Value2 = data;

                rng[2, 2].FormulaR1C1 = "=COUNTIF(R[-2]C[-33]:R[9]C[-3], \"=DW\")";
                rng[2, 4].FormulaR1C1 = "=COUNTIF(R[-2]C[-35]:R[9]C[-5], \"=O\")";

                rng[3, 2].FormulaR1C1 = "=COUNTIF(R[-3]C[-33]:R[8]C[-3], \"=NW\")";
                rng[3, 4].FormulaR1C1 = "=COUNTIF(R[-3]C[-35]:R[8]C[-5], \"=LO\")";

                rng[4, 2].FormulaR1C1 = "=COUNTIF(R[-4]C[-33]:R[7]C[-3], \"=U\")";
                rng[4, 4].FormulaR1C1 = "=COUNTBLANK(R[-4]C[-35]:R[7]C[-5])";

                rng[5, 2].FormulaR1C1 = "=COUNTIF(R[-5]C[-33]:R[6]C[-3], \"=UB\")";
                rng[5, 4].FormulaR1C1 = "=COUNTIF(R[-5]C[-35]:R[6]C[-5], \"=T\")";

                rng[6, 2].FormulaR1C1 = "=COUNTIF(R[-6]C[-33]:R[5]C[-3], \"=W\")";
                rng[6, 4].FormulaR1C1 = "=COUNTIF(R[-6]C[-35]:R[5]C[-5], \"=OT\")";

                rng[7, 2].FormulaR1C1 = "=COUNTIF(R[-7]C[-33]:R[4]C[-3], \"=H\")";
                rng[7, 4].FormulaR1C1 = "=COUNTIF(R[-7]C[-35]:R[4]C[-5], \"=LD\")";

                rng[8, 2].FormulaR1C1 = "=COUNTIF(R[-8]C[-33]:R[3]C[-3], \"=WI\")";
                rng[8, 4].FormulaR1C1 = "=COUNTIF(R[-8]C[-35]:R[3]C[-5], \"=D\")";

                rng[9, 2].FormulaR1C1 = "=COUNTIF(R[-9]C[-33]:R[2]C[-3], \"=SH\")";
                rng[9, 4].FormulaR1C1 = "=COUNTIF(R[-9]C[-35]:R[2]C[-5], \"=BL\")";

                rng[10, 2].FormulaR1C1 = "=COUNTIF(R[-10]C[-33]:R[1]C[-3], \"=S\")";
                rng[10, 4].FormulaR1C1 = "=COUNTIF(R[-10]C[-35]:R[1]C[-5], \"=12\")";

                //rng[11, 2].Formula = "=SUM(AL3:AL11)";
                rng[11, 2].FormulaR1C1 = "=SUM(R[-9]C[0]:R[-1]C[0]) + SUM(R[-5]C[1]:R[-1]C[1])";  // STL
                //rng[11, 4].FormulaR1C1 = "=SUM(R[-9]C[-2]:R[-1]C[-2]) + SUM(R[-9]C[0]:R[-1]C[0])"; // TTL
                rng[11, 4].FormulaR1C1 = "=SUM(R[-9]C[-2]:R[-1]C[-2]) + SUM(R[-9]C[0]:R[-1]C[0])"; // TTL

                rng[11, 1].Font.Bold = true;
                rng[11, 2].Font.Bold = true;
                rng[11, 3].Font.Bold = true;
                rng[11, 4].Font.Bold = true;

            }


            /*******************************************************************************************************************\
             *                                                                                                                 *
            \*******************************************************************************************************************/

            public void excel_report(List<Dictionary<string, string[]>> data, List<Dictionary<string, int>> summary)
            {
                Workbook wb = _xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                Worksheet ws = (Worksheet)wb.Worksheets[1];
                if (ws == null) MessageBox.Show("Error (Excel Interop) : No worksheet !");

                Range rng = ws.get_Range("A1", "AJ1");
                if (rng == null) MessageBox.Show("Error (Excel Interop) : Range problem ");
                Range xtd = ws.get_Range("AK3", "AN13");
                if (xtd == null) MessageBox.Show("Error (Excel Interop) : Range problem ");


                int idx = 0;
                foreach (var item in data)
                {
                    string emp_name = (string)(item["Jan"])[3];

                    mk_employee(rng, item);
                    mk_summary(xtd, emp_name, summary[0]);

                    idx++;

                    rng = rng.Offset[15, 0];
                    xtd = xtd.Offset[15, 0];
                }

                //ws.Columns.AutoFit();
                rng.EntireColumn.AutoFit();
                xtd.EntireColumn.AutoFit();
                ws.get_Range("AK:AK").EntireColumn.ColumnWidth = ws.get_Range("AM:AM").EntireColumn.ColumnWidth;
                
                _xlApp.WindowState = XlWindowState.xlMaximized;            

            }


            /*******************************************************************************************************************\
             *                                                                                                                 *
            \*******************************************************************************************************************/

            public void EndReport()
            {
            }
        }
    }
}
