using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFormsService.Models
{
    public class Mail
    {
        public bool hasAttachment { get; set; }
        public String body { get; set; }
        public String recipients { get; set; }
        public Sender sender { get; set; }
        public IList<File> files { get; set; }
        public Form form { get; set; }
        public String Subject { get; set; }
        public int id { get; set; }

        public Mail() { }
        public Mail(String b, String r, Sender s, List<File> fs)
        {
            body = b;
            recipients = r;
            sender = s;
            if (fs != null && fs.Count() > 0)
            {
                hasAttachment = true;
                files = fs;
            }
        }
    }



    public class Sender
    {
        public string passwd { get; set; }
        public string emailAddress { get; set; }
        public string name { get; set; }
        public string CSRCode { get; set; }
    }

    public class File
    {
        public string name { get; set; }
        public string blob { get; set; }
    }
}