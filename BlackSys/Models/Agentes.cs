﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BlackSys.Models
{
    public class Agentes
    {
        
        public string Nombre { get; set; }

        public string Email { get; set; }
        
    }
}