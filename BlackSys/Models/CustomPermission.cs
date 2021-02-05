namespace BlackSys.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CustomPermission")]
    public partial class CustomPermission
    {
        public int CustomPermissionID { get; set; }

        [Required]
        [StringLength(128)]
        public string UserID { get; set; }

        public int MenuID { get; set; }

        public virtual Menu Menu { get; set; }
        public virtual AspNetUsers AspNetUsers { get; set; }
    }
}
