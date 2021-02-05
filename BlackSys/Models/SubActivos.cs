using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlackSys.Models
{
    public class SubActivos
    {
        [Key]
        public int IdSubactivos { get; set; }
        public int IdActivo { get; set; }
        public string Subactivos1 { get; set; }
        [Display(Name = "SLA Cierre Bajo(hh:mm:ss)")]
        public string SLA_C_B { get; set; }
        [Display(Name = "SLA Cierre Medio(hh:mm:ss)")]
        public string SLA_C_M { get; set; }
        [Display(Name = "SLA Cierre Bajo(hh:mm:ss)")]
        public string SLA_C_A { get; set; }

        public virtual Activos Activos { get; set; }
    }
}