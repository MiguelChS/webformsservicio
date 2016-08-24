using Exchange;
using Repo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Timers;
using WebFormsService.Models;

namespace WebFormsServicio
{
    public partial class Service1 : ServiceBase
    {
        ServiceExchange _exchange;
        MailRepository _repo;
        Timer _timer = null;
        private SMTP _smtp;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                Library.WriteLine("Services Stated!!");
                
                _timer = new Timer( 60 * 1000 * 10);
                _timer.Elapsed += new System.Timers.ElapsedEventHandler(this.timer_tick);
                _timer.Start();
            }
            catch(Exception e)
            {
                Library.WriteErrorLog(e);
            }

        }        


        private void timer_tick(object sender, ElapsedEventArgs e)
        {

            _timer.Stop();
            Mail[] mails = GetEmails();
            SendSmtpEmail(mails);
        }

        public Mail[] GetEmails()
        {
            Library.WriteLine("Buscando Email..");
            _repo = new MailRepository();
            Mail[] mail = null;
            try
            {
                mail = _repo.Select(null, new string[]
                {
                    @" IsNull(hasSent,'') = '' "
                });
            }
            catch (Exception exception)
            {
                Library.WriteErrorLog(exception);
                Library.WriteLine("Error query select");
            }
            finally
            {
                _timer.Start();
            }
            if (mail == null)
            {
                Console.WriteLine("Sin email para enviar");
                Library.WriteLine("Sin email para enviar");
                _timer.Start();
                return null;
            }

            Library.WriteLine("Se encontraron " + mail.Count() + " para enviar.");
            return mail;
        }

        public void SendSmtpEmail(Mail[] mails)
        {
            int count = 1;
            foreach (Mail mail in mails)
            {
                if (mail.sender?.emailAddress == null)
                {
                    Library.WriteLine("Sender incorrect.");
                }
                Library.WriteLine("Creando email " + count++ + " de " + mails.Count());
                if (mail.sender != null)
                {
                    Library.WriteLine("Emisor: " + mail.sender.emailAddress + "------- Receptor/es: " + mail.recipients );
                    _smtp = new SMTP();
                    string[] recipients = mail.recipients.Split(',');
                    List<String> destList = new List<string>();
                    if (recipients?.Length > 1)
                    {
                        destList = recipients.ToList();
                    }
                    else
                    {
                        destList.Add(recipients[0]);
                    }
                     
                    Library.WriteLine("Enviando email...");
                    Boolean result = false;
                    try
                    {
                        result = _smtp.SendHtmlEmail(
                        destList, null, mail.Subject,
                        mail.sender.emailAddress, mail.sender.name,
                        mail.body
                        );
                    }
                    catch (Exception ex)
                    {
                        
                        Library.WriteErrorLog(ex);
                    }
                     
                    if (result)
                    {
                        Library.WriteLine("Email Enviado. Actualizando Base de Datos.");
                        _repo.Update(mail);
                        Library.WriteLine("Base de datos Actualizada.");
                    }
                }
               
                
            }
        }

        /*   private void sendExchangeEmail(Mail[] mail)
           {

               int c = 1;
               foreach (Mail m in mail)
               {
                   if (m.sender?.emailAddress == null || m.sender.passwd == null)
                   {
                       Library.WriteLine("Sender incorrect.");
                       continue;
                   }
                   Library.WriteLine("Creando email " + c++ + " de " + mail.Count());
                   Library.WriteLine("Emisor: " + m.sender.emailAddress + " ---- Receptor/es: " + m.recipients);
                   _exchange = new ServiceExchange(m.sender.emailAddress, m.sender.passwd);
                   Library.WriteLine("Servidor Exchange localizado.");
                   try
                   {
                       Library.WriteLine("Archivos adjuntos: " + m.files.Count());
                       for (int x = 0; x < m.files.Count(); x++)
                       {

                           m.files[x].name =
                               @"D:\Process\WebForms\Images\" +
                               m.files[x].name;
                       }

                       //m.files = new List<File>();

                       Library.WriteLine("Enviando email..");
                       _exchange.send(m);
                       Library.WriteLine("Email Enviado. Actualizando Base de datos.");
                       _repo.Update(m);
                       Library.WriteLine("Base de datos actualizada exitosamente.");

                   }
                   catch (Exception exception)
                   {
                       Library.WriteErrorLog(exception);

                   }
                   finally
                   {
                       _timer.Start();
                   }


               }
           }*/




        protected override void OnStop()
        {
            
            Library.WriteLine("Stop Service");
        }
    }
}
