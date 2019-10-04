using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.ComponentModel.Composition;
using System.Drawing;
using System.IO;


using GarNet;


namespace plgAllyKE
{
    [Export(typeof(IPlugin))]
    public class plugin : IPlugin
    {
        public bool _Service { get { return false; } }

        private const string exeName = @"\letAllyKE.exe";
        //private readonly string exePath = Application.StartupPath;
        private readonly string exePath = Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);


        public string _letName { get { return "TimeBook"; } }
        public Bitmap _GetBitmap { get { return plgAllyKE.Properties.Resources.player_time; } }


        public string _letPath { get { return exePath + exeName; } }
        public string _PlugName { get { return "letAllyKE"; } }

        public string _UserLogin { get; set; }

        public bool PlugDo()
        {
            MessageBox.Show("letAllyKE Plugin exe");

            return true;
        }



        /**************************************************************************************************************\
        *  
        \**************************************************************************************************************/
    }
}
