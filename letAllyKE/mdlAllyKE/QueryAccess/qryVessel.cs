using System;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;

using System.Windows.Forms;

using System.Linq;
using System.ComponentModel;



/// <summary>
/// Summary description for qryVessel
/// </summary>

namespace mdlAllyKE
{    
    public class qryVessel
    {
        static private string _qry_vessel = "default";
        static private bool _requery = false;

        static private DataTable _dt_vessel = null;

        static public DataTable GetDT() { return _dt_vessel; }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        static private void build_vessel_all()
        {
            try
            {
                DataTable dtVessel = dacCache.GetVessel();
                
                var q = from tVessel in dtVessel.AsEnumerable()                        
                        orderby tVessel["NumID"]                        
                        select new Vessel
                        {
                            Short = (string)tVessel["Short"],                            
                            FullName = (string)tVessel["Full Name"],
                            NumID = (string)tVessel["NumID"],
                            Ident = string.Format("{0}:{1}", (string)tVessel["NumID"], (string)tVessel["Full Name"])
                        };

                //DataTable dt = q.CopyToDataTable();
                DataTable dt = ToDataTable(q.ToList());

                _dt_vessel = dt;
                //dacCache.SetGang(dt);

                DataColumn pk1 = dt.Columns[0]; pk1.MaxLength = 8; pk1.AllowDBNull = false;
                dt.PrimaryKey = new DataColumn[] { pk1 };
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error (build_vessl_all) : {0}", ex.Message));
            }
        }

   
        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        static public DataTable GetView(string qry_name)
        {
            if (_requery)
            {
                if (_dt_vessel != null) _dt_vessel.Dispose();
                _requery = false;
                _dt_vessel = null;
            }


            if (_qry_vessel.Equals(qry_name) && _dt_vessel != null) return _dt_vessel;

            _qry_vessel = qry_name;

            //if (qry_name.Equals("All")) build_vessel_all();
            build_vessel_all();

            return _dt_vessel;

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

    class Vessel
    {
        public string Short { get; set; }
        public string FullName { get; set; }
        public string NumID { get; set; }
        public string Ident { get; set; }
    }
}
