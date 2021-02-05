using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlackSys.Models
{
    public class RoleName
    {
     
        [Key]
        
        //AspNetUsers
        public string Id { get; set; }
        public string Email { get; set; }

        //AspNetRoles
        public string Id_rol { get; set; }
        public string Name { get; set; }

        //AspNetUserRoles
        public string UserId { get; set; }
        public string RoleId { get; set; }


    }
}