using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlackSys.Models;
using Microsoft.AspNet.Identity;

namespace BlackSys.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customer.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,Name,Code,CompanyName,NRC,NIT")] Customer customer)
        {
            customer.CreationDate = DateTime.Now.Date;
            customer.CreatedBy = User.Identity.GetUserId();
            customer.rowguid = Guid.NewGuid();
            if (ModelState.IsValid)
            {
                db.Customer.Add(customer);
                var item = from a in db.Customer
                           where a.Name == customer.Name || a.NIT == customer.NIT ||a.NRC==customer.NRC
                           select a;
                if (item.Count() == 0)
                {

                    try
                    {
                        db.SaveChanges();
                        TempData["ResultMessage"] = "Data saved correctly";
                        TempData["ResultType"] = "S";
                        return View("Index");
                    }
                    catch (Exception ex)
                    {
                        TempData["ResultMessage"] = "Fail while saving data. " + ex.Message;
                        TempData["ResultType"] = "E";
                        return View(customer);
                    }
                }
                else
                {
                    TempData["ResultMessage"] = "Color " + customer.Name + " already exist in the system";
                    TempData["ResultType"] = "E";
                    return View(customer);
                }
            }
            else
            {
                TempData["ResultMessage"] = "Data are not valid";
                TempData["ResultType"] = "W";
                return View(customer);
            }

           
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,rowguid,Name,Code,CompanyName,NRC,NIT,CreationDate,CreatedBy,ModifiedDate,ModifiedBy")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customer.Find(id);
            db.Customer.Remove(customer);
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
