using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using System.Reflection;
using System.Net;


//using System.DirectoryServices;


namespace letTB_logKF
{
    public class Gap
    {
        static public string Path = Properties.Settings.Default.Path;


        static public void SetPath(string path)
        {
            Properties.Settings.Default["Path"] = path;
            Properties.Settings.Default.Save();

            Path = Properties.Settings.Default.Path;
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
    }
}
