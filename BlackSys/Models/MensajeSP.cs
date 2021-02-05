using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.Web.Mvc;

namespace BlackSys.Models
{
    public partial class MensajeSP
    {


        public int Id_Mensaje { get; set; }
        public int Id_Solicitud { get; set; }
        [AllowHtml]
        public string Mensaje{ get; set; }
        public string Email_user { get; set; }

        public string Tcoment { get; set; }
       
        [DisplayName("Upload File")]
        public string ImagePath { get; set; }

        public DateTime AddedOn { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }

        [DisplayName("Upload File")]
        public string NombreUsuario { get; set; }

        [DisplayName("Upload File")]
        public string ImagePathUser { get; set; }


    }
}