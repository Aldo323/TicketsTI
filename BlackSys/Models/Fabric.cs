namespace BlackSys.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Fabric")]
    public partial class Fabric
    {
        [Key]
        [Column(Order = 0)]
        public int FabricID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string Code { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(200)]
        public string Description { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CategoryID { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UnitOfMeasurementID { get; set; }

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

        [StringLength(15)]
        public string Width { get; set; }

        [StringLength(5)]
        public string UnitWeight { get; set; }

        [StringLength(10)]
        public string Weight { get; set; }

        [StringLength(150)]
        public string Blend { get; set; }

        [StringLength(200)]
        public string Construction { get; set; }

        [StringLength(150)]
        public string CutDirection { get; set; }

        [StringLength(15)]
        public string FabricType { get; set; }

        [Key]
        [Column(Order = 5, TypeName = "money")]
        public decimal PurchasingPrice { get; set; }

        [StringLength(50)]
        public string PurchasingTerms { get; set; }

        public int? HtsID { get; set; }

        public int? SacID { get; set; }

        [Key]
        [Column(Order = 6)]
        public bool IsActive { get; set; }

        public DateTime? DiscontinuedDate { get; set; }

        public DateTime? CreationDate { get; set; }

        [StringLength(128)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(128)]
        public string ModifiedBy { get; set; }

        [Key]
        [Column(Order = 7)]
        public Guid rowguid { get; set; }
    }
}
