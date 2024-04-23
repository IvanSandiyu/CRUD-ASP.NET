using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoASP.Models
{
    public class Contacto
    {
        public int idContacto { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Telefono { get; set; }
        public string Correo { get; set; }

    }
}