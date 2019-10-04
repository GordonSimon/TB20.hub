using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Diagnostics;
using System.Net.Mail;
using System.Reflection;


//string msg = string.Format("Error ({0}) : {1} ", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
//string id = string.Format("{0}.{1}", System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name);


namespace mdlAnnal
{
    public class ErrMail
    {
        const string EVENTSOURCE = "TugIT.V10 Service";

        static private bool MAIL_ENABLE = true;
        static private bool MAIL_CONSORT = true;
        static private bool ID_CONFLICT = false;

        static private string TITLE = "GarNet RC Tech Support : 604-512-6200";

        static private string MAILTO = "gsimon@garnet.ca";
        static private string MAILFROM = "support@garnet.ca";
        static private string SMTP1 = "mailout.garnet.ca";        
        static private string SUBJECT1 = "IGarNet.ErrMail Alert CONSORT";

        static private string SMTP2 = "smtprelay.idirectory.local";
        static private string SUBJECT2 = "IGarNet.ErrMail Alert ISLAND";

    
        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        public static void SendMailInit(bool mail_enable, bool mail_consort)
        {
            if (ID_CONFLICT && MAIL_CONSORT != mail_consort)
                Warning(string.Format("ID Conflict, MAIL_ENABLE={0}, MAIL_CONSORT={1}", MAIL_ENABLE, MAIL_CONSORT),
                    System.Reflection.MethodBase.GetCurrentMethod());

            ID_CONFLICT = true;
            MAIL_ENABLE = mail_enable;
            MAIL_CONSORT = mail_consort;
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        static private void smtp_email1(string msg, string title)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(SMTP1);

                mail.From = new MailAddress(MAILFROM);
                mail.To.Add(MAILTO);
                if (title == null || title.Equals(string.Empty))
                    mail.Subject = SUBJECT1;
                else
                    mail.Subject = title;
                mail.Body = msg;

                SmtpServer.Port = 25;
                //SmtpServer.Credentials = new System.Net.NetworkCredential("username", "password");
                //SmtpServer.EnableSsl = true;

                if (MAIL_ENABLE)
                    SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                ExDash.SetLastException(ex, System.Reflection.MethodBase.GetCurrentMethod());
            }
        }

        static private void smtp_email2(string msg, string title)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(SMTP2);

                mail.From = new MailAddress(MAILFROM);
                mail.To.Add(MAILTO);
                if (title == null || title.Equals(string.Empty))
                    mail.Subject = SUBJECT2;
                else
                    mail.Subject = title;
                mail.Body = msg;

                SmtpServer.Port = 25;
                //SmtpServer.Credentials = new System.Net.NetworkCredential("username", "password");
                //SmtpServer.EnableSsl = true;

                if (MAIL_ENABLE)
                    SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                ExDash.SetLastException(ex, System.Reflection.MethodBase.GetCurrentMethod());
            }
        }



        static private void send_email(string msg)
        {
            send_email(msg, null);
        }

        static private void send_email(string msg, string title)
        {
            if (MAIL_CONSORT)
                smtp_email1(msg, title);
            else
                smtp_email2(msg, title);
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/
        
        static public void Fail(Exception ex, System.Reflection.MethodBase mb)
        {
            //string msg = string.Format("Error ({0}.{1}) : {2} ", mb.ReflectedType.Name, mb.Name, ex.Message);
            
            string msg = ExDash.SetLastException(ex, mb);

            if (Environment.UserInteractive)
                MessageBox.Show(msg, ex.Source);
                //EventLog.WriteEntry(EVENTSOURCE + " UserInteractive", msg);

            send_email(msg, ex.Source);
        }


        static public void Warning(string err, System.Reflection.MethodBase mb)
        {            
            //string msg = string.Format("Warning ({0}.{1}) : {2} ", mb.ReflectedType.Name, mb.Name, err);

            string msg = ExDash.SetLastWarning(err, mb);

            if (!Environment.UserInteractive)
                send_email(msg);
            else
            {
                //MessageBox.Show(msg, TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);                
                EventLog.WriteEntry(EVENTSOURCE + " UserInteractive", msg);
                if (MAIL_ENABLE) send_email(msg);
            }
                       
            //Application.Exit();
        }

        static public void MailMessage(string msg)
        {
            //MessageBox.Show(msg, TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
            EventLog.WriteEntry("GarNet", msg);
            if (MAIL_ENABLE) send_email(msg);
        }

        static public void MailMessage(string msg, string title)
        {
            //MessageBox.Show(msg, title);
            EventLog.WriteEntry("GarNet", string.Format("Title {0} : {1}",  title, msg));
            if (MAIL_ENABLE) send_email(msg);
        }

    }
}
