using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace BlackSys.Models
{
    public partial class Agnom
    {
     
        
        
        public int Id_Mensaje { get; set; }
        public int Eficacia { get; set; }
        public int Tickets_Asignados { get; set; }
        public int Tickets_Abiertos { get; set; }
        public int Tickets_Cerrados { get; set; }
        public int Tickets_Cerrados_en_SLA { get; set; }
        public int Tickets_Vencido_en_SLA { get; set; }
        public string Agente { get; set; }
        public string URLImage { get; set; }






    }
}