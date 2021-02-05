namespace BlackSys.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FabricCategory")]
    public partial class FabricCategory
    {
        [Key]
        [Column(Order = 0)]
        public int FabricCategoryID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(200)]
        public string Description { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime CreationDate { get; set; }

        [Key]
        [Column(Order = 4)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(128)]
        public string ModifiedBy { get; set; }

        [Key]
        [Column(Order = 5)]
        public Guid rowguid { get; set; }
    }
}
