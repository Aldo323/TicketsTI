using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlackSys.Models;

namespace DatabaseAll.Controllers
{
    [Authorize]
    public class SubActivosController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();   

        // GET: SubActivos
        public ActionResult Index()
        {
            var subActivos = db.SubActivos.Include(s => s.Activos);
            return View(subActivos.ToList());
        }

        // GET: SubActivos/Details/5
        public ActionResult Details_Subactivos(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubActivos subActivos = db.SubActivos.Find(id);
            if (subActivos == null)
            {
                return HttpNotFound();
            }
            return View(subActivos);
        }

        [ActionName("Alta_SubActivo")]
        // GET: SubActivos/Create
        public ActionResult Create()
        {
            ViewBag.IdActivo = new SelectList(db.Activos, "IdActivo", "Activo");
            return View();
        }

        // POST: SubActivos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdSubactivos,IdActivo,Subactivos1,SLA_C_B,SLA_C_M,SLA_C_A")] SubActivos subActivos)
        {
            if (ModelState.IsValid)
            {
                db.SubActivos.Add(subActivos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdActivo = new SelectList(db.Activos, "IdActivo", "Activo", subActivos.IdActivo);
            return View(subActivos);
        }

        // GET: SubActivos/Edit/5
        public ActionResult Edit_SubActivo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubActivos subActivos = db.SubActivos.Find(id);
            if (subActivos == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdActivo = new SelectList(db.Activos, "IdActivo", "Activo", subActivos.IdActivo);
            return View(subActivos);
        }

        // POST: SubActivos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_SubActivo([Bind(Include = "IdSubactivos,IdActivo,Subactivos1,SLA_C_B,SLA_C_M,SLA_C_A")] SubActivos subActivos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subActivos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdActivo = new SelectList(db.Activos, "IdActivo", "Activo", subActivos.IdActivo);
            return View(subActivos);
        }

        // GET: SubActivos/Delete/5
        public ActionResult Borrar_Subactivo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubActivos subActivos = db.SubActivos.Find(id);
            if (subActivos == null)
            {
                return HttpNotFound();
            }
            return View(subActivos);
        }

        // POST: SubActivos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Borrar_Subactivo(int id)
        {
            SubActivos subActivos = db.SubActivos.Find(id);
            db.SubActivos.Remove(subActivos);
            db.SaveChanges();
            return RedirectToAction("Index");
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
