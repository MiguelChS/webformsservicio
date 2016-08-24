using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebFormsService.Repository
{
    public class Clients
    {
        public string pais { get; set; }
        public string nombre { get; set; }
        public string numero { get; set; }

        public Clients(string Pais, string Nombre, string Numero)
        {
            this.pais = Pais;
            this.nombre = Nombre;
            this.numero = Numero;
        }

        public Clients()
        {
        }

       
    }
}