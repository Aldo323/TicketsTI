using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BlackSys.Models
{
    public partial class Images
    {
        [Key]
        public int ImageId { get; set; }
        public string Title { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        [DisplayName("Upload File")]
        public string ImagePath { get; set; }
    }
}