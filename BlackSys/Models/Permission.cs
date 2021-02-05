namespace BlackSys.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Permission")]
    public partial class Permission
    {
        public int PermissionID { get; set; }

        [Required]
        [StringLength(128)]
        public string RoleID { get; set; }

        public int MenuID { get; set; }

        public virtual Menu Menu { get; set; }
        public virtual AspNetRoles AspNetRoles { get; set; }
    }
}
