namespace BlackSys.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Component")]
    public partial class Component
    {
        public int ComponentID { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        public int ComponentCategoryID { get; set; }

        public int UomID { get; set; }

        public int? CustomerID { get; set; }

        [StringLength(50)]
        public string CustomerCode { get; set; }

        [StringLength(100)]
        public string CountryOfOrigin { get; set; }

        public int? SupplierID { get; set; }

        [StringLength(50)]
        public string SupplierCode { get; set; }

        [StringLength(200)]
        public string Description2 { get; set; }

        [Column(TypeName = "money")]
        public decimal PurchasingPrice { get; set; }

        [StringLength(50)]
        public string PurchasingTerms { get; set; }

        [Column(TypeName = "money")]
        public decimal ListPrice { get; set; }

        public int? HtsID { get; set; }

        public int? SacID { get; set; }

        public bool IsActive { get; set; }

        public DateTime? DiscontinuedDate { get; set; }

        public DateTime CreationDate { get; set; }

        [Required]
        [StringLength(128)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(128)]
        public string ModifiedBy { get; set; }

        public Guid rowguid { get; set; }

        public virtual ComponentCategory ComponentCategory { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Supplier Supplier { get; set; }

        public virtual UnitMeasure UnitMeasure { get; set; }
    }
}
