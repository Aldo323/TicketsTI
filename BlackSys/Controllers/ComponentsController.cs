using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlackSys.Models;

namespace BlackSys.Controllers
{
    public class ComponentsController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: Components
        public ActionResult Index()
        {
            var component = db.Component.Include(c => c.ComponentCategory).Include(c => c.Customer).Include(c => c.Supplier).Include(c => c.UnitMeasure);
            return View(component.ToList());
        }

        // GET: Components/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Component component = db.Component.Find(id);
            if (component == null)
            {
                return HttpNotFound();
            }
            return View(component);
        }

        // GET: Components/Create
        public ActionResult Create()
        {
            ViewBag.ComponentCategoryID = new SelectList(db.ComponentCategory, "ComponentCategoryID", "Name");
            ViewBag.CustomerID = new SelectList(db.Customer, "CustomerID", "CreatedBy");
            ViewBag.SupplierID = new SelectList(db.Supplier, "SupplierID", "Name");
            ViewBag.UomID = new SelectList(db.UnitMeasure, "UomID", "Code");
            return View();
        }

        // POST: Components/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ComponentID,Code,Description,ComponentCategoryID,UomID,CustomerID,CustomerCode,CountryOfOrigin,SupplierID,SupplierCode,Description2,PurchasingPrice,PurchasingTerms,ListPrice,HtsID,SacID,IsActive,DiscontinuedDate,CreationDate,CreatedBy,ModifiedDate,ModifiedBy,rowguid")] Component component)
        {
            if (ModelState.IsValid)
            {
                db.Component.Add(component);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ComponentCategoryID = new SelectList(db.ComponentCategory, "ComponentCategoryID", "Name", component.ComponentCategoryID);
            ViewBag.CustomerID = new SelectList(db.Customer, "CustomerID", "CreatedBy", component.CustomerID);
            ViewBag.SupplierID = new SelectList(db.Supplier, "SupplierID", "Name", component.SupplierID);
            ViewBag.UomID = new SelectList(db.UnitMeasure, "UomID", "Code", component.UomID);
            return View(component);
        }

        // GET: Components/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Component component = db.Component.Find(id);
            if (component == null)
            {
                return HttpNotFound();
            }
            ViewBag.ComponentCategoryID = new SelectList(db.ComponentCategory, "ComponentCategoryID", "Name", component.ComponentCategoryID);
            ViewBag.CustomerID = new SelectList(db.Customer, "CustomerID", "CreatedBy", component.CustomerID);
            ViewBag.SupplierID = new SelectList(db.Supplier, "SupplierID", "Name", component.SupplierID);
            ViewBag.UomID = new SelectList(db.UnitMeasure, "UomID", "Code", component.UomID);
            return View(component);
        }

        // POST: Components/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ComponentID,Code,Description,ComponentCategoryID,UomID,CustomerID,CustomerCode,CountryOfOrigin,SupplierID,SupplierCode,Description2,PurchasingPrice,PurchasingTerms,ListPrice,HtsID,SacID,IsActive,DiscontinuedDate,CreationDate,CreatedBy,ModifiedDate,ModifiedBy,rowguid")] Component component)
        {
            if (ModelState.IsValid)
            {
                db.Entry(component).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ComponentCategoryID = new SelectList(db.ComponentCategory, "ComponentCategoryID", "Name", component.ComponentCategoryID);
            ViewBag.CustomerID = new SelectList(db.Customer, "CustomerID", "CreatedBy", component.CustomerID);
            ViewBag.SupplierID = new SelectList(db.Supplier, "SupplierID", "Name", component.SupplierID);
            ViewBag.UomID = new SelectList(db.UnitMeasure, "UomID", "Code", component.UomID);
            return View(component);
        }

        // GET: Components/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Component component = db.Component.Find(id);
            if (component == null)
            {
                return HttpNotFound();
            }
            return View(component);
        }

        // POST: Components/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Component component = db.Component.Find(id);
            db.Component.Remove(component);
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
