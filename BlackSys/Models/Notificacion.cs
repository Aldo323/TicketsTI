﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlackSys.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Notificacion
    {


        public int idSolicitud { get; set; }
        public string Notify { get; set; }
        public string Mail_User { get; set; }
        public string Nombre { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Path { get; set; }


    }
}
