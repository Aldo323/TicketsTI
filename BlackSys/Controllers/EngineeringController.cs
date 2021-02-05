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

        public ActionResult Index()
        {
          
            return View();
        }

       
        public ActionResult Product()
        {

            string correo = User.Identity.GetUserName().ToString();
            string vistamg = db.Database.SqlQuery<string>("Sp_loadStringMenu  @User ='" + correo + "', @idMenu= 15 ").FirstOrDefault();

            return View(vistamg);

        }
     
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
        public ActionResult Create([Bind(Include = "Ubicacion,Correo,Departamento,Telefono,Asunto,Descripcion,Activo,Subactivo")] Solicitudes solicitudes)
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
            catch(Exception e)
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

            var str ="";

            NotificationHub conector = new NotificationHub();


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

     
        public ActionResult SubActivo()
        {
            return View();
        }

        

      
        public ActionResult EditAbierto(int? id)
        {
            string correo = User.Identity.GetUserName().ToString();
            string vistamg = db.Database.SqlQuery<string>("Sp_loadStringMenu  @User ='" + correo + "', @idMenu= 2 ").FirstOrDefault();
            IEnumerable<Agentes> Resposable = db.Database.SqlQuery<Agentes>("SP_Agents").ToList();
            ViewBag.Agente = new SelectList(Resposable, "Email", "Nombre");
            if ( vistamg == null)
            {
              
                return View("~/Views/Error/Denegado.cshtml");
            }
            else
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
                return View(vistamg,solicitudes);
            }
           
        }

        
        public ActionResult EditAsignado(int? id)
        {
           

            string correo = User.Identity.GetUserName().ToString();
            string vistamg = db.Database.SqlQuery<string>("Sp_loadStringMenu  @User ='" + correo + "', @idMenu= 3 ").FirstOrDefault();
            if (vistamg == null)
            {

                return View("~/Views/Error/Denegado.cshtml");
            }
            else
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
                return View(vistamg,solicitudes);
            }

           
        }

        
        public ActionResult EditNC(int? id)
        {
            IEnumerable<Agentes> Responsable = db.Database.SqlQuery<Agentes>("SP_Agents").ToList();
            ViewBag.Agente = new SelectList(Responsable, "Email", "Nombre");

            string correo = User.Identity.GetUserName().ToString();
            string vistamg = db.Database.SqlQuery<string>("Sp_loadStringMenu  @User ='" + correo + "', @idMenu= 4 ").FirstOrDefault();
            if (vistamg == null)
            {

                return View("~/Views/Error/Denegado.cshtml");
            }
            else
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
                return View(vistamg, solicitudes);
            }

        }


        //public ActionResult EditCerrado(int? id)
        //{
        //    IEnumerable<Agentes> Responsable = db.Database.SqlQuery<Agentes>("SP_Agents").ToList();
        //    ViewBag.Agente = new SelectList(Responsable, "Email", "Nombre");

        //    string correo = User.Identity.GetUserName().ToString();
        //    string vistamg = db.Database.SqlQuery<string>("Sp_loadStringMenu  @User ='" + correo + "', @idMenu= 2 ").FirstOrDefault();
        //    if (vistamg == null)
        //    {

        //        return View("~/Views/Error/Denegado.cshtml");
        //    }
        //    else
        //    {
        //        if (id == null)
        //        {
        //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        }
        //        Solicitudes solicitudes = db.Solicitudes.Find(id);
        //        if (solicitudes == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        return View(vistamg, solicitudes);
        //    }
        //}

        
        public ActionResult EditVencido(int? id)
        {
            string correo = User.Identity.GetUserName().ToString();
            string vistamg = db.Database.SqlQuery<string>("Sp_loadStringMenu  @User ='" + correo + "', @idMenu= 2 ").FirstOrDefault();
            if (vistamg == null)
            {

                return View("~/Views/Error/Denegado.cshtml");
            }
            else
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
                return View(vistamg, solicitudes);
            }
        }

      
        public ActionResult Bitacora()
        {
       
            return View("Bitacora_Customer");
        }

       
        

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAsignado([Bind(Include = "IdSolicitud,Asunto, Ubicacion,Correo,Departamento,Telefono,Tipo_solicitud,Descripcion,Fecha_levantamiento,Estatus,Conclusion,Categoria,SLA_C_A,SLA_C_M,SLA_C_B,Responsable,Activo,Subactivo")] Solicitudes solicitudes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solicitudes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Agente", "Home");

            }
            return RedirectToAction("Agente", "Home");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VewEdit([Bind(Include = "IdSolicitud,Estado,Responsable")] SolicitudModal solicitudes)
        {


            if (ModelState.IsValid)
            {
               
                try
                {
                    IEnumerable<SolicitudModal> Informe = db.Database.SqlQuery<SolicitudModal>("SP_EditSolicitudModal  @idSolicitud = " + solicitudes.IdSolicitud+ ", @Responsable  = '" + solicitudes.Responsable + "', @Estatus  = '" + solicitudes.Estado + "'").ToList();
                }
                catch (Exception e)
                {
                    return RedirectToAction("Abierto", "Home");
                }



            }
            return RedirectToAction("Abierto", "Home");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAbierto([Bind(Include = "IdSolicitud,Asunto,Ubicacion,Correo,Departamento,Telefono,Tipo_solicitud,Descripcion,Fecha_levantamiento,Estatus,Conclusion,Categoria,SLA_C_A,SLA_C_M,SLA_C_B,Responsable,Activo,Subactivo")] Solicitudes solicitudes)
        {

            
            if (ModelState.IsValid)
            {
                db.Entry(solicitudes).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Abierto", "Home");
                }
                catch (Exception e)
                {
                   return RedirectToAction("Abierto", "Home");
                }
              
                

            }
            return RedirectToAction("Abierto", "Home");
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditNC([Bind(Include = "IdSolicitud,Asunto,Ubicacion,Correo,Departamento,Telefono,Tipo_solicitud,Descripcion,Fecha_levantamiento,Estatus,Conclusion,Categoria,SLA_C_A,SLA_C_M,SLA_C_B,Responsable,Activo,Subactivo")] Solicitudes solicitudes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solicitudes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("NC", "Home");

            }
            return RedirectToAction("NC", "Home");
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCerrado([Bind(Include = "IdSolicitud,Asunto,Ubicacion,Correo,Departamento,Telefono,Tipo_solicitud,Descripcion,Fecha_levantamiento,Estatus,Conclusion,Categoria,SLA_C_A,SLA_C_M,SLA_C_B,Responsable,Activo,Subactivo")] Solicitudes solicitudes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solicitudes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Cerrado", "Home");

            }
            return RedirectToAction("Cerrado", "Home");
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditVencidos([Bind(Include = "IdSolicitud,Ubicacion,Correo,Departamento,Telefono,Tipo_solicitud,Descripcion,Fecha_levantamiento,Estatus,Conclusion,Categoria,SLA_C_A,SLA_C_M,SLA_C_B,Responsable,Activo,Subactivo")] Solicitudes solicitudes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solicitudes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Vencido", "Home");

            }
            return RedirectToAction("Vencido", "Home");
        }

    }
}