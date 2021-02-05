using BlackSys.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BlackSys.Controllers
{
    [Authorize]
    public class ComponentCategoryController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();
        // GET: Inventory
        public ActionResult CategoryIndex()
        {
            var category = db.ComponentCategory;
            return View(category.ToList());
        }

        [HttpGet]
        public ActionResult CategoryCreate()
        {
            return PartialView("_CategoryCreate");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CategoryCreate([Bind(Include = "Name,Description")] ComponentCategory category)
        {
            category.CreationDate = DateTime.Now.Date;    
            category.CreatedBy = User.Identity.GetUserId();
            category.rowguid = Guid.NewGuid();
            
            if (ModelState.IsValid)
            {
                db.ComponentCategory.Add(category);
                var item = from a in db.ComponentCategory
                           where a.Name == category.Name
                           select a;
                if (item.Count() == 0)
                {

                    try
                    {
                        db.SaveChanges();
                        TempData["ResultMessage"] = "Data saved correctly";
                        TempData["ResultType"] = "S";

                    }
                    catch (Exception ex)
                    {
                        TempData["ResultMessage"] = "Fail while saving data. " + ex.Message;
                        TempData["ResultType"] = "E";
                    }
                }
                else
                {
                    TempData["ResultMessage"] = "Color " + category.Name + " already exist in the system";
                    TempData["ResultType"] = "E";
                }
            }
            else
            {
                TempData["ResultMessage"] = "Data are not valid";
                TempData["ResultType"] = "W";                
            }
            return RedirectToAction("CategoryIndex");
        }

        [HttpGet]
        public ActionResult CategoryEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComponentCategory category = db.ComponentCategory.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            return PartialView("_CategoryEdit", category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CategoryEdit([Bind(Include = "ComponentCategoryID,Name,Description,ModifiedDate,CreationDate,rowguid,CreatedBy")] ComponentCategory category)
        {
            category.ModifiedDate = DateTime.Now.Date;      
            category.ModifiedBy = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                var item = from a in db.Colors
                           where a.Name == category.Name
                           select a;
                if (item.Count() == 0)
                {
                    try
                    {
                        db.SaveChanges();
                        TempData["ResultMessage"] = "El Registro se actualizó correctamente";
                        TempData["ResultType"] = "S";
                        return RedirectToAction("CategoryIndex");
                    }
                    catch (Exception ex)
                    {

                        TempData["ResultMessage"] = "El Registro no se pudo actualizar. " + ex.Message;
                        TempData["ResultType"] = "E";
                        return RedirectToAction("CategoryIndex");
                        //throw;
                    }
                }
                else
                {
                    TempData["ResultMessage"] = "El Registro no se pudo actualizar. Ya existe un registro con ese nombre ";
                    TempData["ResultType"] = "E";
                    return RedirectToAction("CategoryIndex");
                }
            }
            return RedirectToAction("CategoryIndex");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CategoryDelete(int id)
        {
            ComponentCategory category = db.ComponentCategory.Find(id);
            try
            {
                db.ComponentCategory.Remove(category);
                db.SaveChanges();
                TempData["ResultMessage"] = "El Registro se Borró correctamente";
                TempData["ResultType"] = "S";
            }
            catch (Exception _ex)
            {
                Debug.WriteLine(_ex.Message);
                TempData["ResultMessage"] = "El Registro no se pudo borrar. " + _ex.Message;
                TempData["ResultType"] = "E";
            }
            return RedirectToAction("CategoryIndex");
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