using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlackSys.Models
{
    public class Responsable
    {
        [Key]
        public int Id_responsable { get; set; }
      
        public string Responsable_name { get; set; }

    }
}