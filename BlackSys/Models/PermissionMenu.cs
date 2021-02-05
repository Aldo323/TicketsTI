namespace BlackSys.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    
    public partial class PermissionMenu
    {
        [Key]
        [Column(Order = 0)]        
        public int MenuID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string DisplayName { get; set; }

        [Key]
        [Column(Order = 2)]       
        public int ParentMenuID { get; set; }

        public int PermissionType { get; set; }
        public bool Permission { get; set; }


    }
}
