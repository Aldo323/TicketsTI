using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlackSys.Models
{
    public class cat_Activos
    {

        [Key]
        public int idCategoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string id { get; set; }
       
    }
}