using System;
using System.Configuration;
using System.Net.Mail;

namespace EmailWebApi.Models
{
    public class Email
    {
        string m_MailTo = "";
        string m_MailFrom = EmailWebApi.Properties.Settings.Default.EmailAddress;
        string m_MailSubject = "";
        string m_MailBody = "";
        int m_SmtpPort = EmailWebApi.Properties.Settings.Default.SmtpPort;
        string m_SmtpServer = EmailWebApi.Properties.Settings.Default.SmtpServer;

        public string MailTo
        {
            get { return m_MailTo; }
            set { m_MailTo = value; }
        }

        public string MailFrom
        {
            get { return m_MailFrom; }
            set { m_MailFrom = value; }
        }

        public string MailSubject
        {
            get { return m_MailSubject; }
            set { m_MailSubject = value; }
        }

        public string MailBody
        {
            get { return m_MailBody; }
            set { m_MailBody = value; }
        }

        public int smtpPort
        {
            get { return m_SmtpPort; }
            set { m_SmtpPort = value; }

        }

        public string smtpServer
        {
            get { return m_SmtpServer; }
            set { m_SmtpServer = value; }
        }

        // SEND Function for all Email messages
        public bool Send()
        {

            MailMessage objMailMessage = new MailMessage();
            //Create mail message
            objMailMessage = new MailMessage();
            objMailMessage.IsBodyHtml = true;
            objMailMessage.From = new MailAddress(MailFrom);
            objMailMessage.To.Add(MailTo);
            objMailMessage.Subject = MailSubject;
            objMailMessage.Body = MailBody;

            try
            {
                //send the message
                SmtpClient smtp = new SmtpClient(smtpServer);
                //smtp.Credentials = new System.Net.NetworkCredential("username", "password");
                smtp.Port = smtpPort;
                smtp.Send(objMailMessage);

                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}