using BlackSys.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BlackSys.Controllers
{
    [Authorize]
    public class PermissionsController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        public ActionResult Index()
        {
            ViewBag.RoleID = new SelectList(db.AspNetRoles, "Id", "Name");
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email");
            IEnumerable<PermissionMenu> menus = db.Database.SqlQuery<PermissionMenu>("SP_GetMenu @UserId ='" + User.Identity.GetUserId() + "', @RoleId = NULL");
            ViewBag.Menus = from a in db.Menu
                            where a.MenuID == 0
                            select a;
            var permission = db.Permission.Include(r => r.Menu).Include(s => s.AspNetRoles);
            return View(permission.ToList());

        }
               

        [HttpPost]
        public ActionResult Index(string RoleID, string UserID)
        {

            IEnumerable<PermissionMenu> menus = db.Database.SqlQuery<PermissionMenu>("SP_GetMenu @UserId ='" + User.Identity.GetUserId() + "',@RoleId = NULL");
            if (!string.IsNullOrEmpty(UserID))
            {
                menus = null;              
                
                menus = db.Database.SqlQuery<PermissionMenu>("SP_GetMenu @UserId ='" + UserID + "', @RoleId = NULL");
            }
            if (!string.IsNullOrEmpty(RoleID))
            {
                menus = null;               
                menus = db.Database.SqlQuery<PermissionMenu>("SP_GetMenu @UserId =NULL, @RoleId = '" + RoleID + "'");
            }

            ViewBag.RoleID = new SelectList(db.AspNetRoles, "Id", "Name");
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email");

            ViewBag.Menus = menus.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult SavePermission(string query, string qtype)
        {
            string fquery = "{\"PERMISSION\":" + query + "}";           
            
            if (qtype == "r")
            {
                /*DELETING ROWS*/
                var data = Newtonsoft.Json.Linq.JObject.Parse(fquery);
                String roleId = Convert.ToString(data["PERMISSION"][0]["RoleID"]);

                var rows = from r in db.Permission
                           where r.RoleID == roleId
                           select r;
                foreach (var row in rows)
                {
                    db.Permission.Remove(row);
                }
                db.SaveChanges();
                              
                int menu;
                string role;
                for (int i = 0; i < data["PERMISSION"].Count(); i++)
                {
                    menu= (int)data["PERMISSION"][i]["MenuID"];
                    role = (string)data["PERMISSION"][i]["RoleID"];
                    db.Permission.Add(new Permission {MenuID=menu, RoleID=role });
                }
                try
                {
                    db.SaveChanges();
                    TempData["ResultMessage"] = "Los datos se guardaron correctamente";
                    TempData["ResultType"] = "S";
                }
                catch (Exception ex)
                {

                    TempData["ResultMessage"] = "Error al guardar los datos! " + ex.Message;
                    TempData["ResultType"] = "E";
                }
            }
            if (qtype == "u")
            {
                /*DELETING ROWS*/
                var data = Newtonsoft.Json.Linq.JObject.Parse(fquery);
                String userId = Convert.ToString(data["PERMISSION"][0]["UserID"]);
                var rows = from r in db.CustomPermission
                           where r.UserID == userId
                           select r;
                foreach (var row in rows)
                {
                    db.CustomPermission.Remove(row);
                }
                db.SaveChanges();

                int menu;
                string user;
                for (int i = 0; i < data["PERMISSION"].Count(); i++)
                {
                    menu= (int)data["PERMISSION"][i]["MenuID"];
                    user= (String)data["PERMISSION"][i]["UserID"];
                    db.CustomPermission.Add(new CustomPermission { MenuID = menu, UserID = user });
                }
                try
                {
                    db.SaveChanges();
                    TempData["ResultMessage"] = "Los datos se guardaron correctamente";
                    TempData["ResultType"] = "S";
                }
                catch (Exception ex)
                {

                    TempData["ResultMessage"] = "Error al guardar los datos! " + ex.Message;
                    TempData["ResultType"] = "E";
                }

            }


            return RedirectToAction("Index");
        }

        // GET: Permisions
        public ActionResult MenuIndex()
        {
            return View(db.Menu.ToList());
        }

        // GET: Permisions/Details/5
        public ActionResult MenuDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menu.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // GET: Permisions/Create
        public ActionResult MenuCreate()
        {
            return View();
        }

        // POST: Permisions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MenuCreate([Bind(Include = "MenuID,DisplayName,ParentMenuID,OrderNumber,MenuURL,MenuIcon")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Menu.Add(menu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(menu);
        }

        // GET: Permisions/Edit/5
        public ActionResult MenuEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menu.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Permisions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MenuEdit([Bind(Include = "MenuID,DisplayName,ParentMenuID,OrderNumber,MenuURL,MenuIcon")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(menu);
        }

        // GET: Permisions/Delete/5
        public ActionResult MenuDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menu.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Permisions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult MenuDeleteConfirmed(int id)
        {
            Menu menu = db.Menu.Find(id);
            db.Menu.Remove(menu);
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
