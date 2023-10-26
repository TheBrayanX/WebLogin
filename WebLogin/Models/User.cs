using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebLogin.Models
{
    public class User
    {
        public int idUser { get; set; }
        public string NameUser { get; set; }
        public string clave { get; set; }
        public string confirmarClave { get; set; }
    }
}