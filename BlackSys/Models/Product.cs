namespace BlackSys.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [Key]
        [Column(Order = 0)]
        public int ProductID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string Style { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(25)]
        public string ProductNumber { get; set; }

        [Key]
        [Column(Order = 3)]
        public bool MakeFlag { get; set; }

        [Key]
        [Column(Order = 4)]
        public bool FinishedGoodsFlag { get; set; }

        [StringLength(25)]
        public string Color { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short SafetyStockLevel { get; set; }

        [Key]
        [Column(Order = 6, TypeName = "money")]
        public decimal StandardCost { get; set; }

        [Key]
        [Column(Order = 7, TypeName = "money")]
        public decimal ListPrice { get; set; }

        [StringLength(5)]
        public string WeightUnitMeasureCode { get; set; }

        public decimal? Weight { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DaysToManufacture { get; set; }

        public int? ProductLineID { get; set; }

        public int? ProductCategory { get; set; }

        [Key]
        [Column(Order = 9)]
        public DateTime SellStartDate { get; set; }

        public DateTime? SellEndDate { get; set; }

        public DateTime? DiscontinuedDate { get; set; }

        [Key]
        [Column(Order = 10)]
        public DateTime CreationDate { get; set; }

        [Key]
        [Column(Order = 11)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(128)]
        public string ModifiedBy { get; set; }

        [Key]
        [Column(Order = 12)]
        public Guid rowguid { get; set; }
    }
}
