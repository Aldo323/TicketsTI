using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace BlackSys.Models
{
    public partial class Mensajes
    {
     
        
        [Key]
        public int Id_Mensaje { get; set; }
        public int Id_Solicitud { get; set; }
        public string Mensaje{ get; set; }
        public string Email_user { get; set; }
        public string Tcoment { get; set; }
        public string ImagePath { get; set; }

        [NotMapped]
        public DateTime AddedOn { get; set; }
        [NotMapped]
        public string Responsable { get; set; }
        [NotMapped]
        public string Estado { get; set; }
        [NotMapped]
        public int Id_Estado { get; set; }
        [NotMapped]
        public int Id_responsable { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        [DisplayName("Upload File")]
        public string NombreUsuario { get; set; }

        [DisplayName("Upload File")]
        public string ImagePathUser { get; set; }


    }
}