using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.Web.Mvc;

namespace BlackSys.Models
{
    public partial class Search
    {


        public int idSolicitud { get; set; }
        
        public string Descripcion { get; set; }

        public string Asunto { get; set; }



    }
}