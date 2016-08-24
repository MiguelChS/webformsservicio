using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFormsService.Storage
{
    public class ImageFile
    {
        public String fileName { get; set; }
        public String path { get; set; }
        public String ext { get; set; }

        public string Read()
        {
            throw new NotImplementedException();
        }

        public void Write(string base64String)
        {
            if (base64String == null)
            {
                throw new ArgumentNullException();
            }

            byte[] blob = Convert.FromBase64String(base64String);
            System.IO.File.WriteAllBytes((path + "\\" + fileName), blob);
        }
    }
}