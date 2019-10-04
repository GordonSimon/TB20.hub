using System;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;

using System.Windows.Forms;

using System.Linq;
using System.ComponentModel;



/// <summary>
/// Summary description for qryShift
/// </summary>

namespace mdlAllyKE
{    
    public class qryShift
    {
        static private string _qry_shift = "default";
        //static private bool _requery = false;

        static private DataTable _dt_shift = null;
        static private Dictionary<int, string> _dic_bind = null;

        static public DataTable GetDT() { return _dt_shift; }



        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        static private void build_shift_all()
        {
            try
            {
                DataTable dtShift = dacCache.GetShift();
                
                var q = from tShift in dtShift.AsEnumerable()                        
                        orderby tShift["NumID"]                        
                        select new Shift
                        {
                            Short = (string)tShift["Short"],                            
                            FullName = (string)tShift["ShiftDesc"],
                            NumID = (string)tShift["NumID"],
                            Ident = string.Format("{0}:{1}", (string)tShift["NumID"], (string)tShift["ShiftDesc"])
                        };

                //DataTable dt = q.CopyToDataTable();
                DataTable dt = ToDataTable(q.ToList());

                _dt_shift = dt;
                //dacCache.SetGang(dt);

                DataColumn pk1 = dt.Columns[0]; pk1.MaxLength = 8; pk1.AllowDBNull = false;
                dt.PrimaryKey = new DataColumn[] { pk1 };
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error (build_shift_all) : {0}", ex.Message));
            }
        }

   
        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        static public Dictionary<int, string> BindShifts()
        {
            if (_dic_bind == null)
            {
                _dic_bind = new Dictionary<int, string>();
                

                int idx = 0;
                foreach (DataRow row in dacShift.GetDT().Rows)
                    _dic_bind.Add(idx++, (string)row["Short"]);
            }
       

            return _dic_bind;
        }


        static public DataTable GetView(string qry_name)
        {
            //if (_requery)
            //{
            //    if (_dt_shift != null) _dt_shift.Dispose();
            //    _requery = false;
            //    _dt_shift = null;
            //}


            if (_qry_shift.Equals(qry_name) && _dt_shift != null) return _dt_shift;

            _qry_shift = qry_name;

            //if (qry_name.Equals("All")) build_vessel_all();
            build_shift_all();

            return _dt_shift;

        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public static DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

    }


    /*******************************************************************************************************************\
     *                                                                                                                 *
    \*******************************************************************************************************************/

    class Shift
    {
        public string Short { get; set; }
        public string FullName { get; set; }
        public string NumID { get; set; }
        public string Ident { get; set; }
    }
}
