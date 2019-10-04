using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.ComponentModel.Composition;
using System.Drawing;
using System.IO;


using GarNet;


namespace plgTB_logKF
{
    [Export(typeof(IPlugin))]    
    public class plugin : IPlugin
    {
        public bool _Service { get { return false; } }

        private const string exeName = @"\letTB-logKF.exe";
        //private readonly string exePath = Application.StartupPath;
        private readonly string exePath = Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);

        public string _letName { get { return "Pay Export"; } }        
        //public Bitmap _GetBitmap { get { return plgTB_logKF.Properties.Resources.icon_Quotes; } }
        public Bitmap _GetBitmap { get { return plgTB_logKF.Properties.Resources.icon_Layouts;  } }


        public string _letPath { get { return exePath + exeName; } }
        public string _PlugName { get { return "letTB-logKF"; } }

        public string _UserLogin { get; set; }

        public bool PlugDo()
        {
            MessageBox.Show("letDE-logKF Plugin exe");

            return true;
        }

        

        /**************************************************************************************************************\
        *  
        \**************************************************************************************************************/
    }
}
