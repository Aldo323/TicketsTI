namespace BlackSys.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Colors
    {
        [Key]
        public int ColorID { get; set; }

        [Required]
        [StringLength(25)]
        public string Name { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
