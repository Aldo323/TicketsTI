using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlackSys.Models;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Net;


namespace BlackSys.Controllers
{
    [Authorize]
    public class ActivosController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();
        // GET: Activos
        
        public ActionResult Index()
        {
            return View(db.Activos.ToList());

        }

        [ActionName ("Detalles_Activos")]
        // GET: Activos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activos activos = db.Activos.Find(id);
            if (activos == null)
            {
                return HttpNotFound();
            }
            return View(activos);
        }

        // GET: Activos/Create
        [ActionName("Alta_Activo")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Activos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdActivo,Activo")] Activos activos)
        {
            if (ModelState.IsValid)
            {
                db.Activos.Add(activos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(activos);
        }

        // GET: Activos/Edit/5
        [ActionName("Search")]
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activos activos = db.Activos.Find(id);
            if (activos == null)
            {
                return HttpNotFound();
            }
            // return View(activos);
            return View(activos);
        }

        // POST: Activos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
       
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "IdActivo,Activo")] Activos activos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(activos);
        }

    
        // GET: Activos/Delete/5
        public ActionResult Borrar (int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activos activos = db.Activos.Find(id);
            if (activos == null)
            {
                return HttpNotFound();
            }
            return View(activos);
        }

        // POST: Activos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Borrar(int id)
        {
           
                Activos activos = db.Activos.Find(id);
                db.Activos.Remove(activos);
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
