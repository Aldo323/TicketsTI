using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BlackSys.Models
{
    public class Cliente
    {
        [Key]
        public int Id_user { get; set; }
      
        public string Nombre_user { get; set; }

        //[Display(Name = "Imagen"), DataType(DataType.Upload)]
        //public byte[] Img_user { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        public string ImagePathUser { get; set; }
        public string ImagePath { get; set; }
        /*[DisplayName("
         * ")]
        public string ImagePath { get; set; }

        public string ImageFile { get; set; }*/
    }
}