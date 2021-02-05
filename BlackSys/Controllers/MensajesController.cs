using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using BlackSys.Models;

namespace BlackSys.Controllers
{
    [Authorize]
    public class MensajesController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: Mensajes
        public ActionResult Index()
        {
            return View(db.Mensajes.ToList());
        }

        // GET: Mensajes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mensajes mensajes = db.Mensajes.Find(id);
            if (mensajes == null)
            {
                return HttpNotFound();
            }
            return View(mensajes);
        }

        public ActionResult Formulario(int? id)
        {
           
            return View();
        }

        // GET: Mensajes/Create
        public ActionResult Create(int? id)
        {
            List<Solicitudes> ActivoList = db.Solicitudes.Where(x => x.IdSolicitud == id).ToList();
            List<Responsable> RespList = db.Responsables.ToList();
            List<EstadosTickets> EstadoList = db.EstadosTickets.ToList();
            
            ViewBag.RespList = new SelectList(RespList, "Id_responsable", "Responsable_name");
            ViewBag.EstadoList = new SelectList(EstadoList, "Id_Estado", "Estado");
            // var estatus = db.Solicitudes.Where(x => x.IdSolicitud == id).Select(x => new { x.Estatus});
            Session["solicitud"] = id;
            Session["estatus"] = ActivoList[0].Estatus.ToString();
            Session["responsabilidad"] = ActivoList[0].Responsable.ToString();

            var r = "";
            //ViewBag.RealEstatus = estatus;
            return View();
        }

        // POST: Mensajes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MensajeSP mensajes)
        {
            List<AspNetUsers> modelocliente;
            if (mensajes.ImageFile == null)
            {

            }
            else
            {
                if (mensajes.ImageFile.ContentType == "image/png" || mensajes.ImageFile.ContentType == "image/jpeg")
                {
                string fileName = Path.GetFileNameWithoutExtension(mensajes.ImageFile.FileName);
                string extension = Path.GetExtension(mensajes.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssffff") + extension;
                mensajes.ImagePath = ConfigurationManager.AppSettings["RutaImagen"] + fileName;
                fileName = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["RutaImagen"]), fileName);
                mensajes.ImageFile.SaveAs(fileName);
                   
                       
                }
                else
                {
                    TempData["ResultMessage"] = "El archivo no es una Imagen";
                }
                
            }


            if (ModelState.IsValid)
            {
                using (BlackSysEntities dc = new BlackSysEntities())
                {
                    dc.Configuration.LazyLoadingEnabled = false;
                    //int id_solicitud = int.Parse(gard);
                    try
                    {
                        string user = mensajes.Email_user.ToString();
                        modelocliente = dc.AspNetUsers.Where(x => x.Email == user).ToList();
                        string tool = modelocliente[0].ImagePathUser.ToString();
                        mensajes.ImagePathUser = tool;
                    }
                    catch (Exception e)
                    {

                    }
                }
                //db.Mensajes.Add(mensajes);
               
                IEnumerable<MensajeSP> Informe = db.Database.SqlQuery<MensajeSP>("sp_Message  @id_solicitud = '" + mensajes.Id_Solicitud +
                    "', @Mensaje  = '" + mensajes.Mensaje + "', @Email_User  = '" + mensajes.Email_user + "', @ImagePathUser  = '" + mensajes.ImagePathUser +"'").ToList();
               // db.SaveChanges();
                return RedirectToAction("Create");
            }

            return RedirectToAction("Create");
        }

        [HttpPost]
        public ActionResult FroalaUploadFile(HttpPostedFileBase file, int? postId)
        {
            var fileName = Path.GetFileName(file.FileName);
            var rootPath = Server.MapPath(ConfigurationManager.AppSettings["MensajeFlie"]);
            file.SaveAs(Path.Combine(rootPath, fileName));
            return Json(new { link = ConfigurationManager.AppSettings["MensajeFlie_return"] + fileName }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult FroalaUploadImage(HttpPostedFileBase file, int? postId)
        {
            var fileName = Path.GetFileName(file.FileName);
            var rootPath = Server.MapPath(ConfigurationManager.AppSettings["MensajeImg"]);
            file.SaveAs(Path.Combine(rootPath, fileName));
            return Json(new { link = ConfigurationManager.AppSettings["MensajeImg_return"] + fileName }, JsonRequestBehavior.AllowGet);
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
