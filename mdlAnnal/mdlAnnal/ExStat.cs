using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Diagnostics;


namespace mdlAnnal
{
    public class ExDash         
    {
        const string EVENTSOURCE = "TugIT.V10 Service";

        public static int log = 0;

        public static string LastException { get; set; }
        public static DateTime LastExceptionStamp { get; set; }

        public static string LastWarning { get; set; }
        public static DateTime LastWarningStamp { get; set; }

        public static Dictionary<DateTime, string> LogDash = new Dictionary<DateTime, string>();

        public static int ChkLog() { return log++; }

        public static string SetLastWarning(string msg, System.Reflection.MethodBase mb)
        {
            DateTime stamp = DateTime.Now;
            
            string err = string.Format("Warning ({0}.{1}) : {2}\n\n  [TimeStamp : {3}]", mb.ReflectedType.Name, mb.Name, msg, stamp);

            EventLog.WriteEntry(EVENTSOURCE, err);
            LogDash.Add(stamp, err);
            LastWarning = err;
            LastWarningStamp = stamp;

            return err;
        }


        public static string SetLastException(Exception ex, System.Reflection.MethodBase mb)
        {
            DateTime stamp = DateTime.Now;
            
            string err = string.Format("Error ({0}.{1}) : {2}\n\n  [TimeStamp : {3}]", mb.ReflectedType.Name, mb.Name, ex.Message, stamp);           
            LogDash.Add(stamp, err);

            EventLog.WriteEntry(EVENTSOURCE, err);
            LastException = err;
            LastExceptionStamp = stamp;

            return err;
        }
    }


    public class ExStat
    {
        const bool ExSTAT_CONSORT = true;
        const bool ExSTAT_MAIL_ENABLE = true;
        
        const string ExSTAT_TOWINGCO = "TOWINGCO";
        const string ExSTAT_FORESTCO = "FORESTCO";

        public int PollCount { get; set; }
        public int OrderCount { get; set; }
        public DateTime StartUp { get; set; }

        public bool MailEnable { get; set;  }

        public bool Consort { get; set; }
        public string TowingCo { get; set; }
        public string ForestCo { get; set; }


        public string LastWarning { get { return ExDash.LastWarning; } }
        public DateTime LastWarningStamp { get { return ExDash.LastWarningStamp; } }

        public string LastException { get { return ExDash.LastException; } }
        public DateTime LastExceptionStamp { get { return ExDash.LastExceptionStamp; } }

        public string RunTime { get { TimeSpan ts = DateTime.Now - StartUp; return ts.ToString(@"%d") + " days  " + ts.ToString(@"hh\:mm\:ss"); } }

        public DataTable DtEvent { get; set; }
        public DataTable DtNotice { get; set; }
        public DataTable DtOrders { get; set; }
        public DataTable DtNotice1 { get; set; }
        public DataTable DtNotice2 { get; set; }
        public DataTable DtNotice3 { get; set; }
        public Dictionary<string, DataTable> DtX { get; set; }
         

        public ExStat()
        {
            PollCount = 0;
            StartUp = DateTime.Now;

            Consort = ExSTAT_CONSORT;
            MailEnable = ExSTAT_MAIL_ENABLE;

            TowingCo = ExSTAT_TOWINGCO;
            ForestCo = ExSTAT_FORESTCO;

            DtX = new Dictionary<string, DataTable>();
        }
        

        public void SetProfile(bool mail_enable, bool consort, string towing_co, string forest_co)
        {
            Consort = consort;
            MailEnable = mail_enable;
            TowingCo = towing_co;
            ForestCo = forest_co;

            ErrMail.SendMailInit(MailEnable, Consort);
        }


        public void PollIncrement() { PollCount += 1; }

        public void SetOrderCount(int oc) { OrderCount = oc; }
    }
}
