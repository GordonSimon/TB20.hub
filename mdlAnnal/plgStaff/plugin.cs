using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.ComponentModel.Composition;
using System.Drawing;
using System.IO;


using GarNet;


namespace plgStaff
{
    [Export(typeof(IPlugin))]    
    public class plugin : IPlugin
    {
        public bool _Service { get { return false; } }

        private const string exeName = @"\letStaff.exe";
        //private readonly string exePath = Application.StartupPath;
        private readonly string exePath = Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);

        public string _letName { get { return "PayCodes"; } }        
        //public Bitmap _GetBitmap { get { return plgTB_logKF.Properties.Resources.icon_Quotes; } }
        public Bitmap _GetBitmap { get { return plgStaff.Properties.Resources.Plus1Hour;  } }


        public string _letPath { get { return exePath + exeName; } }
        public string _PlugName { get { return "letStaff"; } }

        public string _UserLogin { get; set; }

        public bool PlugDo()
        {
            MessageBox.Show("letStaff Plugin exe");

            return true;
        }

        

        /**************************************************************************************************************\
        *  
        \**************************************************************************************************************/
    }
}
