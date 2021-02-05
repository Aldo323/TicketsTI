namespace BlackSys.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Supplier")]
    public partial class Supplier
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Supplier()
        {
            Component = new HashSet<Component>();
        }

        public int SupplierID { get; set; }

        public Guid rowguid { get; set; }

        [Required]
        [StringLength(25)]
        public string Name { get; set; }

        public int? Code { get; set; }

        [Required]
        [StringLength(125)]
        public string CompanyName { get; set; }

        [StringLength(25)]
        public string NRC { get; set; }

        [StringLength(18)]
        public string NIT { get; set; }

        public DateTime? CreationDate { get; set; }

        [StringLength(128)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(128)]
        public string ModifiedBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Component> Component { get; set; }
    }
}
