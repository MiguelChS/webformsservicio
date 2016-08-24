using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFormsService.Models;

namespace Exchange
{
    public class ServiceExchange
    {
        ExchangeService service;
        
        public ServiceExchange(String user, String passwd)
        {
            service = new ExchangeService(ExchangeVersion.Exchange2010_SP2);
            service.Credentials = new WebCredentials(user, passwd);
            service.TraceEnabled = true;
            service.TraceFlags = TraceFlags.All;

            service.AutodiscoverUrl(user, RedirectionCallback);
        } 
        public void send(Mail mail)
        {
            EmailMessage email = new EmailMessage(service);
            foreach(String rec in mail.recipients.Split(','))
            {
                email.ToRecipients.Add(rec);
            }
            email.Subject = "Nuevo Formulario";
            email.Body = new MessageBody(mail.body);
            if(mail.files != null)
            {
                foreach (File f in mail.files)
                {
                    email.Attachments.AddFileAttachment(f.name);
                }
            }
            
            email.Send();
           
        }

        static bool RedirectionCallback(string url)
        {
            // Return true if the URL is an HTTPS URL.
            return url.ToLower().StartsWith("https://");
        }
    }
}
