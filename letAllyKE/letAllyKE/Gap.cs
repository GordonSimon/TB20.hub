using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using System.Reflection;
using System.Net;


namespace GAPP
{
    class Gap
    {
        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public static string StrVal(object s) { return (s.Equals(DBNull.Value) ? string.Empty : (string)s); }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        //static public string StorePath = Properties.Settings.Default.StorePath;

        //static public bool Test = wcfEFT.Properties.Settings.Default.Test;


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/        

        //public static string License
        //{
        //    get
        //    {
        //        try
        //        {
        //            if (Tag.Equals("EFT"))
        //                return AssemblyDesc;
        //            else
        //                return Tag;
        //        }
        //        catch
        //        {
        //            return "GarNet";
        //        }
        //    }
        //}


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public static string AssemblyDesc
        {
            get
            {
                var attribute = ((AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(
                   Assembly.GetExecutingAssembly(), typeof(AssemblyDescriptionAttribute), false)).Description;

                return attribute;
            }
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public static string AssemblyVersion
        {
            get
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                string version = fvi.FileVersion;

                return version;
            }
        }

        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

    }
}
