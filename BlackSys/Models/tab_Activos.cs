using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlackSys.Models
{
    public class tab_Activos
    {
     
        [Key]
        
        public Guid IdActivo { get; set; }
        public string Codigo { get; set; }
        public string No_Categoria { get; set; }
        public int idCategoria { get; set; }
        public string Estado { get; set; }
        public Boolean Asignado { get; set; }
        public Boolean Re_Asignable { get; set; }
        public string Ubicado { get; set; }
        public string Marca { get; set; }
        public string No_Serial { get; set; }
        public string UsuarioAsig { get; set; }
        public string Nombre_Activo { get; set; }
        public string Modelo { get; set; }
        public string Caracteristicas { get; set; }
        [AllowHtml]
        public string Notas { get; set; }
        //[NotMapped]
        //[Display(Name = "Imagen"), DataType(DataType.Upload)]
        //public byte[] Img_user { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
        public string ImagePathActivo { get; set; }


    }
}