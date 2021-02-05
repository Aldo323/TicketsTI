using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlackSys.Models;
using System.IO;
using Microsoft.AspNet.Identity;
using System.Web.UI;
using System.Configuration;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace BlackSys.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();


 

        public ActionResult convertirImagen(string Id_user)
        {
            List<AspNetUsers> modelocliente;

            modelocliente = db.AspNetUsers.Where(z => z.Id == Id_user).ToList();
            return File(modelocliente[0].ImagePathUser, "image/jpeg");


        }

        public ActionResult PathUser(string nombre)
        {
            List<AspNetUsers> modelocliente;
            try
            {
                modelocliente = db.AspNetUsers.Where(z => z.Email == nombre).ToList();
                return File(modelocliente[0].ImagePathUser, "image/jpeg");
            }
            catch
            {
                //var imagenCliente = db.Clientes.Where(z => z.Id_user == 1).FirstOrDefault();
                //return File(imagenCliente.Img_user, "image/jpeg");
                modelocliente = db.AspNetUsers.Where(z => z.Email == nombre).ToList();
                return File(modelocliente[0].ImagePathUser, "image/jpeg");
            }


        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
