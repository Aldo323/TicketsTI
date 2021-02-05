using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BlackSys.Models
{
    public class EstadosTickets
    {
        [Key]
        public int Id_Estado { get; set; }

        public string Estado { get; set; }
        
    }
}