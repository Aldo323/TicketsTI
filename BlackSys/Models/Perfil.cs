using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlackSys.Models
{
    public class Perfil
    {
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPat { get; set; } 
        public string ApellidoMat { get; set; }
        public string ImagePathUser { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}