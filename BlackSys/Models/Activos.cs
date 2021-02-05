using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlackSys.Models
{
    public class Activos
    {
     
        [Key]
        public int IdActivo { get; set; }
        public string Activo { get; set; }

    }
}