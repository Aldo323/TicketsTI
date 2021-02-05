using BlackSys.Models;
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
    public class SystemController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        public ActionResult ColorIndex()
        {
            var colors = db.Colors;

            return View(colors.ToList());
        }

        [HttpGet]
        public ActionResult ColorCreate()
        {
            return PartialView("_ColorCreate");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ColorCreate([Bind(Include = "Name, Description")] Colors color)
        {
            color.ModifiedDate = DateTime.Now.Date;
            if (ModelState.IsValid)
            {
                db.Colors.Add(color);
                var item = from a in db.Colors
                           where a.Name == color.Name
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
                    TempData["ResultMessage"] = "Color " + color.Name + " already exist in the system";
                    TempData["ResultType"] = "E";
                    //return PartialView("_ColorCreate", color);
                }
            }
            else
            {
                TempData["ResultMessage"] = "Data are not valid";
                TempData["ResultType"] = "W";
                //return PartialView("_ColorCreate", color);
            }
            return RedirectToAction("ColorIndex");
        }


        public ActionResult ColorDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Colors color = db.Colors.Find(id);
            if (color == null)
            {
                return HttpNotFound();
            }
            return View(color);
        }

        [HttpPost, ActionName("ColorDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Colors color = db.Colors.Find(id);
            try
            {
                db.Colors.Remove(color);
                db.SaveChanges();
                TempData["ResultMessage"] = "El Registro se Borró correctamente";
                TempData["ResultType"] = "S";
            }
            catch (Exception _ex)
            {
                Debug.WriteLine(_ex.Message);
                TempData["ResultMessage"] = "El Registro no se pudo borrar. "+_ex.Message;
                TempData["ResultType"] = "E";
            }
            return RedirectToAction("ColorIndex");
        }

        [HttpGet]
        public ActionResult ColorEdit(int? id)
        {            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Colors color = db.Colors.Find(id);
            if (color == null)
            {
                return HttpNotFound();
            }

            return PartialView("_ColorEdit", color);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ColorEdit([Bind(Include = "ColorID,Name")] Colors color)
        {
            color.ModifiedDate = DateTime.Now.Date;
            if (ModelState.IsValid)
            {
                db.Entry(color).State = EntityState.Modified;
                var item = from a in db.Colors
                           where a.Name == color.Name
                           select a;
                if (item.Count() == 0)
                {
                    try
                    {
                        db.SaveChanges();                        
                        TempData["ResultMessage"] = "El Registro se actualizó correctamente";
                        TempData["ResultType"] = "S";
                        return RedirectToAction("ColorIndex");
                    }
                    catch (Exception ex)
                    {

                        TempData["ResultMessage"] = "El Registro no se pudo actualizar. "+ex.Message;
                        TempData["ResultType"] = "E";
                        return RedirectToAction("ColorIndex");
                        //throw;
                    }
                }
                else
                {
                    TempData["ResultMessage"] = "El Registro no se pudo actualizar. Ya existe un registro con ese nombre ";
                    TempData["ResultType"] = "E";
                    return RedirectToAction("ColorIndex");
                }


               
            }
            return RedirectToAction("ColorIndex");
            //return PartialView("_ColorEdit", color);
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