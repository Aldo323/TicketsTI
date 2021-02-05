using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlackSys.Models
{
    public class UserConnection
    {
        [Key]
        public string UserId { get; set; }

        public string Nombre_user { get; set; }
    }
}