namespace BlackSys.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public  class QR
    {

      
        public int cantidad { get; set; }
        public byte[] Image { get; set; }

        public string idu { get; set; }

    }
}
