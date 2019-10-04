using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Net.Mail;


namespace letEmp_KF
{
    class errDash  //V2.1
    {
        static private bool MAIL_ENABLE = false;

        static private string MAILTO = "gsimon@garnet.ca";
        static private string MAILFROM = "support@garnet.ca";
        static private string SUBJECT = "APP Alert";
        static private string SMTP = "mx1.garnet.ca";


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        static private void send_email(string msg, string title)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(SMTP);

                mail.From = new MailAddress(MAILFROM);
                mail.To.Add(MAILTO);
                if (title == null || title.Equals(string.Empty))
                    mail.Subject = SUBJECT;
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
                MessageBox.Show(ex.Message);
            }

        }


        static private void send_email(string msg)
        {
            send_email(msg, null);
        }


        /*******************************************************************************************************************\
         *                                                                                                                 *
        \*******************************************************************************************************************/

        static public void Fail(Exception ex)
        {
            MessageBox.Show(ex.Message);
            send_email(ex.Message, ex.Source);
        }


        static public void Error(string msg)
        {
            MessageBox.Show(msg);
            send_email(msg);
            //Application.Exit();
        }

        static public void Message(string msg)
        {
            MessageBox.Show(msg);
            send_email(msg);
        }

        static public void Message(string msg, string title)
        {
            MessageBox.Show(msg, title);
            send_email(msg, title);
        }

    }
}
