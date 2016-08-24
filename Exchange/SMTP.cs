using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Exchange
{
    public class SMTP
    {
        private SmtpClient smtpClient;
        private int port = 25; //DEFAULT SMTP PORT
        private string host = "mailhost.daytonoh.ncr.com";

        public SMTP()
        {
            this.smtpClient = new SmtpClient(host,port);
            
        }

        private void Initialize()
        {
            this.smtpClient.Port = port;
            this.smtpClient.Host = host;
        }

        public SMTP(SmtpClient smtpClient, int port, string host)
        {
            this.smtpClient = smtpClient;
            this.port = port;
            this.host = host;
            Initialize();
        }

        public bool SendHtmlEmail(List<String> destList,List<String> destCC, String subject,String from,String fromDisplayName,String body)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from,fromDisplayName,Encoding.UTF8);
            if (destList == null
                && !destList.Any())
            {
                throw new Exception("Destinatario cannot be null");
            }
            
            foreach (var dest in destList)
            {
                    mail.To.Add(dest);
            }
            if (destCC != null)
                foreach (var cc in destCC)
                {
                    mail.CC.Add(cc);
                }

            mail.Subject = subject;
            string htmlStr = body;
            /**/
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(htmlStr,null,"text/html");
            mail.AlternateViews.Add(htmlView);
            try
            {
                smtpClient.Send(mail);
                return true;
            }
            catch (Exception e)
            {
                throw;
            }
            
        }
    }
}
