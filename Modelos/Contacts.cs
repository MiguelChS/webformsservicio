using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFormsService.Models
{
    public class Contacts
    {
        public string numero { get; set; }
        public string direcciones { get; set; }
        public string nombres { get; set; }
        public string pais { get; set; }

        public Contacts(string Numero, string Direcciones, string Nombres, string Pais)
        {
            this.numero = Numero;
            this.direcciones = Direcciones;
            this.nombres = Nombres;
            this.pais = Pais;
        }

        public Contacts() { }
    }
}