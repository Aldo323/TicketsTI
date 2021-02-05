using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using BlackSys.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace BlackSys.Controllers
{
    [Authorize]
    public class EngineeringController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();
        // GET: Engineering
        [Authorize(Roles = "Sistemas, Customer Care")]
        public ActionResult Index()
        {
          
            return View();
        }

        [Authorize(Roles = "Sistemas, Customer Care")]
        public ActionResult Product()
        {
            /*var permiso = db.AspNetUsers.Join(db.AspNetUserRoles, u => u.Id, b => b.UserId,
                  (u, b) => new { u, b }).Where(x => x.u.Email == "aldo.mor3@gmail.com");*/
            // var user = User.Identity.GetUserName().ToString();
            // var result = db.Database.SqlQuery<AspNetRoles>("Select r.Name FROM AspNetUsers as u INNER JOIN AspNetUserRoles b on u.Id = b.UserId INNER JOIN AspNetRoles  r on r.Id = b.RoleId WHERE u.Email = 'aldo.mor3@gmail.com'").ToList();
            // var i_u = db.AspNetUsers.FirstOrDefault(x => x.Email == user).Id;
            /* var sql = @"Select 
                         r.Name
                         FROM AspNetUsers as u
                         INNER JOIN AspNetUserRoles b on u.Id = b.UserId
                         INNER JOIN AspNetRoles  r on r.Id= b.RoleId 
                         WHERE u.Email = @mail";*/
            //var result = db.Database.SqlQuery(sql, param);
            //  string rolename = UserManager.GetRoles(i_u);

            /*var roleNames = UserManager.GetRoles(i_u);
            string m = roles.ToString();*/
            /*string id_rol = i_r.ToString();
            var n_r = db.AspNetRoles.FirstOrDefault(x => x.Id == id_rol).Name;
            var lost = n_r;*/
            var roles = ((ClaimsIdentity)User.Identity).Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value).ToList();

            var rol = roles[0].ToString();
          

            if (rol == "Sistemas")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Bitacora");
            }
            
        }
        [Authorize(Roles = "Sistemas, Customer Care")]
        public ActionResult Create()
        {
            List<Activos> ActivoList = db.Activos.ToList();
            ViewBag.ActivoList = new SelectList(ActivoList, "IdActivo", "Activo");

            /*List<SubActivos> SubactivoList = db.SubActivos.ToList();
            ViewBag.SubactivoList = new SelectList(SubactivoList, "IdSubactivos", "Subactivos1");*/
            return View();
        }

        // POST: Solicitudes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ubicacion,Correo,Departamento,Telefono,Descripcion,Activo,Subactivo")] Solicitudes solicitudes)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(solicitudes.Activo == null || solicitudes.Subactivo == "0" || solicitudes.Ubicacion == null || solicitudes.Descripcion == null || solicitudes.Departamento == null)
                    {
                        if(solicitudes.Subactivo == "0")
                        {
                            TempData["ResultMessage"] = "Favor de seleccionar un SubActivos";
                            return RedirectToAction("Create", "Engineering");
                        }
                        TempData["ResultMessage"] = "Agregaun comentario a tu Ticket";
                        return RedirectToAction("Create", "Engineering");
                    }
                    db.Solicitudes.Add(solicitudes);
                    db.SaveChanges();
                    TempData["ResultMessage"] = "Tu solicitud se ha generado correctamente";
                    TempData["ResultType"] = "S";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
               
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                db.Solicitudes.Add(solicitudes);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(solicitudes);
        }



        private IList<SubActivos> GetSubActivos(int Activoid)
        {
         
                var data = db.SubActivos.Where(m => m.IdActivo == Activoid).ToList();
                return data;
            
        }

        [AcceptVerbs(HttpVerbs.Get)]

        public JsonResult LoadSubActivosByActivosId(string Activoid, string cn)
        {
            if(Activoid == null)
            {
                return Json("");
            }
            else
            {
                string Cname = cn;

                try
                {
                    var SubActivosList = this.GetSubActivos(Convert.ToInt32(Activoid));
                    var SubActivosData = SubActivosList.Select(m => new SelectListItem()
                    {
                        Text = m.Subactivos1,
                        Value = m.Subactivos1.ToString(),
                    });
                    return Json(SubActivosData, JsonRequestBehavior.AllowGet);
                }
                catch { 

                    }

                return Json("");

            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult FroalaAutoSave(string body, int? postId)
        {
            body = Microsoft.Security.Application.Sanitizer.GetSafeHtml(body);

            //todo: save body ...
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult Search(String ser)
        {
            BlackSysEntities db = new BlackSysEntities();
            List<Search> Informe = db.Database.SqlQuery<Search>("SP_SearchAll @valor='" + ser + "'").ToList();
            int cantidad = Informe.Count();
            //var str = "<table border='1'>";
            //str += "<tr><th>Id</th><th>Descripcion</th></tr>";
            var str ="";
            //str += "<th>Descripcion</th></tr>";
            //  String s = Informe[1].ToString();
            //string contact = (string)reader_2["Mail_User"];
            NotificationHub conector = new NotificationHub();
            //for (int i = 0; i < cantidad; i++)
            //{
            //    //int id = Informe[i].idSolicitud;
            //   //string r = Regex.Replace(Informe[i].Descripcion, "<.*?>", String.Empty);
            //   // str += "<p style='width: 200px;'>" + Informe[i].idSolicitud + "&Tab;" + r + "</p>";

            //           //"<br>" +
            //           //"<pre>&Tab;Content Continues</pre>";

            //}
            //if(ser == "")
            //{
            //    //return "";
            //}
           

            return Json(new { Informe });
        }

        [HttpPost]
        public ActionResult FroalaUploadFile(HttpPostedFileBase file, int? postId)
        {
            var fileName = Path.GetFileName(file.FileName);
            var rootPath = Server.MapPath(ConfigurationManager.AppSettings["SolicitudFlie"]);
            file.SaveAs(Path.Combine(rootPath, fileName));
            return Json(new { link = ConfigurationManager.AppSettings["SolicitudFlie_return"] + fileName }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult FroalaUploadImage(HttpPostedFileBase file, int? postId)
        {
            var fileName = Path.GetFileName(file.FileName);
            var rootPath = Server.MapPath(ConfigurationManager.AppSettings["SolicitudImg"]);
            file.SaveAs(Path.Combine(rootPath, fileName));
            return Json(new { link = ConfigurationManager.AppSettings["SolicitudImg_return"] + fileName }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Graficas()
        {
           
                return View("Graficas");
     
        }

        public ActionResult Contenido()
        {

            return View();

        }

        public ActionResult SubActivo()
        {
            return View();
        }

        [Authorize(Roles = "Sistemas")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitudes solicitudes = db.Solicitudes.Find(id);
            if (solicitudes == null)
            {
                return HttpNotFound();
            }
            return View(solicitudes);
        }

        [Authorize(Roles = "Sistemas")]
        public ActionResult EditAbierto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitudes solicitudes = db.Solicitudes.Find(id);
            if (solicitudes == null)
            {
                return HttpNotFound();
            }
            return View(solicitudes);
        }

        [Authorize(Roles = "Sistemas")]
        public ActionResult EditAsignado(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitudes solicitudes = db.Solicitudes.Find(id);
            if (solicitudes == null)
            {
                return HttpNotFound();
            }
            return View(solicitudes);
        }

        [Authorize(Roles = "Sistemas")]
        public ActionResult EditNC(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitudes solicitudes = db.Solicitudes.Find(id);
            if (solicitudes == null)
            {
                return HttpNotFound();
            }
            return View(solicitudes);
        }

        [Authorize(Roles = "Sistemas")]
        public ActionResult EditCerrado(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitudes solicitudes = db.Solicitudes.Find(id);
            if (solicitudes == null)
            {
                return HttpNotFound();
            }
            return View(solicitudes);
        }

        [Authorize(Roles = "Sistemas")]
        public ActionResult EditVencido(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitudes solicitudes = db.Solicitudes.Find(id);
            if (solicitudes == null)
            {
                return HttpNotFound();
            }
            return View(solicitudes);
        }

        [Authorize(Roles = "Customer Care")]
        public ActionResult Bitacora()
        {
       
            return View("Bitacora_Customer");
        }

        [Authorize(Roles = "Sistemas")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdSolicitud,Ubicacion,Correo,Departamento,Telefono,Tipo_solicitud,Descripcion,Fecha_levantamiento,Estatus,Conclusion,Categoria,SLA_C_A,SLA_C_M,SLA_C_B,Responsable,Activo,Subactivo")] Solicitudes solicitudes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solicitudes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Product");
            }
            return View(solicitudes);
        }

        [Authorize(Roles = "Sistemas")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAsignado([Bind(Include = "IdSolicitud,Ubicacion,Correo,Departamento,Telefono,Tipo_solicitud,Descripcion,Fecha_levantamiento,Estatus,Conclusion,Categoria,SLA_C_A,SLA_C_M,SLA_C_B,Responsable,Activo,Subactivo")] Solicitudes solicitudes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solicitudes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Agente", "Home", new { nombre = "D" });

            }
            return RedirectToAction("Agente", "Home", new { nombre = "D" });
        }

        [Authorize(Roles = "Sistemas")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAbierto([Bind(Include = "IdSolicitud,Ubicacion,Correo,Departamento,Telefono,Tipo_solicitud,Descripcion,Fecha_levantamiento,Estatus,Conclusion,Categoria,SLA_C_A,SLA_C_M,SLA_C_B,Responsable,Activo,Subactivo")] Solicitudes solicitudes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solicitudes).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Agente", "Home", new { nombre = "C" });
                }
                catch (Exception e)
                {
                    return RedirectToAction("Agente", "Home", new { nombre = "C" });
                }
              
                

            }
            return RedirectToAction("Agente", "Home", new { nombre = "C" });
        }

        [Authorize(Roles = "Sistemas")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditNC([Bind(Include = "IdSolicitud,Ubicacion,Correo,Departamento,Telefono,Tipo_solicitud,Descripcion,Fecha_levantamiento,Estatus,Conclusion,Categoria,SLA_C_A,SLA_C_M,SLA_C_B,Responsable,Activo,Subactivo")] Solicitudes solicitudes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solicitudes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Agente", "Home", new { nombre = "B" });

            }
            return RedirectToAction("Agente", "Home", new { nombre = "B" });
        }

        [Authorize(Roles = "Sistemas")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCerrado([Bind(Include = "IdSolicitud,Ubicacion,Correo,Departamento,Telefono,Tipo_solicitud,Descripcion,Fecha_levantamiento,Estatus,Conclusion,Categoria,SLA_C_A,SLA_C_M,SLA_C_B,Responsable,Activo,Subactivo")] Solicitudes solicitudes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solicitudes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Agente", "Home", new { nombre = "A" });

            }
            return RedirectToAction("Agente", "Home", new { nombre = "A" });
        }

        [Authorize(Roles = "Sistemas")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditVencidos([Bind(Include = "IdSolicitud,Ubicacion,Correo,Departamento,Telefono,Tipo_solicitud,Descripcion,Fecha_levantamiento,Estatus,Conclusion,Categoria,SLA_C_A,SLA_C_M,SLA_C_B,Responsable,Activo,Subactivo")] Solicitudes solicitudes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solicitudes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Agente", "Home", new { nombre = "E" });

            }
            return RedirectToAction("Agente", "Home", new { nombre = "E" });
        }

    }
}