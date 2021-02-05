namespace BlackSys.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductPhoto")]
    public partial class ProductPhoto
    {
        [Key]
        [Column(Order = 0)]
        public int ProductPhotoID { get; set; }

        public byte[] ThumbNailPhoto { get; set; }

        [StringLength(50)]
        public string ThumbnailPhotoFileName { get; set; }

        public byte[] LargePhoto { get; set; }

        [StringLength(50)]
        public string LargePhotoFileName { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime ModifiedDate { get; set; }
    }
}
