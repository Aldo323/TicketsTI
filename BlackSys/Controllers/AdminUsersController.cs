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
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Security.Claims;

namespace BlackSys.Controllers
{
    [Authorize]
    public class AdminUsersController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();
        List<AspNetUsers> model;
        public AdminUsersController() { }

        public AdminUsersController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        //public EditUserViewModel EditUserViewModel { get; private set; }

        [HttpGet]
        public async Task<ActionResult> Index()
        {


            return View(await UserManager.Users.ToListAsync());
        }

        [HttpPost]
        public ActionResult Index(string Search)
        {

            string errorMessage = "No se Encontraron Resultados";
            if (!String.IsNullOrEmpty(Search))
            {

                var users = (from c in UserManager.Users
                             where
                                  c.UserName.Contains(Search) || c.PhoneNumber.Contains(Search) ||
                                  c.Address.Contains(Search) || c.Email.Contains(Search)
                             select c).ToList();
                if (users.Count > 0)
                    errorMessage = "";
                return View(users);
            }
            else
            {
                var _user = UserManager.Users;
                errorMessage = "Debe escribir un valor para buscar";
                var users = _user.ToList();
                ViewBag.NotFound = errorMessage;
                return View(users);
            }

            // return View();
        }

        //
        // GET: /Users/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);

            ViewBag.RoleNames = await UserManager.GetRolesAsync(user.Id);

            return View(user);
        }

        //
        // GET: /Users/Create
        public async Task<ActionResult> Create()
            {
            //Get the list of Roles
            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
            return View();
        }



        //
        // POST: /Users/Create
        [HttpPost]
        public async Task<ActionResult> Create(RegisterViewModel userViewModel, params string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = userViewModel.Email,
                    Email = userViewModel.Email,
                    EmailConfirmed = true
                };



                // Then create:
                var adminresult = await UserManager.CreateAsync(user, userViewModel.Password);

                //Add User to the selected Roles 
                if (adminresult.Succeeded)
                {
                    if (selectedRoles != null)
                    {
                        var result = await UserManager.AddToRolesAsync(user.Id, selectedRoles);
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("", result.Errors.First());
                            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
                            ViewBag.Result = "El Registro se guardo correctamente!";
                            return View();
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", adminresult.Errors.First());
                    ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");
                    return View();

                }
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");
            return View();
        }

        //
        // GET: /Users/Edit/1
        public async Task<ActionResult> Edit(string id)
        {
            string correo = User.Identity.GetUserName().ToString();
            string vistamg = db.Database.SqlQuery<string>("Sp_loadStringMenu  @User ='" + correo + "', @idMenu= 16 ").FirstOrDefault();
            if (vistamg == null)
            {

                return View("~/Views/Error/Denegado.cshtml");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            EditUserViewModel rec = new EditUserViewModel
            {
                Id = user.Id
            };
            ViewData["Message"] = rec;
            var userRoles = await UserManager.GetRolesAsync(user.Id);
            return View(vistamg, new EditUserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                Nombre = user.Nombre,
                ApellidoPat = user.ApellidoPat,
                ImagePathUser = user.ImagePathUser,
                // Include the Addresss info:

                RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem()

                {
                    Selected = userRoles.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                })
            });
        }

        //
        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Email,Id,Address,Nombre,City,ApellidoPat,State,PostalCode,ImageFile,ImagePathUser")] EditUserViewModel editUser,  params string[] selectedRole)
        {

            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(editUser.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                if (editUser.ImageFile == null)
                {

                }
                else
                {
                    if (editUser.ImageFile.ContentType == "image/png" || editUser.ImageFile.ContentType == "image/jpeg")
                    {

                        string conStr = ConfigurationManager.ConnectionStrings["BlackSysConnection"].ConnectionString;
                        // string sqlCommand = @"SP_getImage @Nombre_user ='" + cliente.Nombre_user + "'";
                        // string sqlCommand = @"Select ImagePathUser FROM Clientes Where Nombre_user ='" + cliente.Nombre_user + "'";
                        string sqlCommand = @"select replace(ImagePathUser,'" + ConfigurationManager.AppSettings["ImagenPerfil"] + "', '')TM from AspNetUsers Where Email ='" + editUser.Email + "'";

                        //you can notice here I have added table name like this [dbo].[Contacts] with [dbo], its mendatory when you use Sql Dependency
                        using (SqlConnection con = new SqlConnection(conStr))
                        {
                            SqlCommand cmd = new SqlCommand(sqlCommand, con);

                            if (con.State != System.Data.ConnectionState.Open)
                            {
                                con.Open();
                            }
                            cmd.Notification = null;



                            //we must have to execute the command here
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                reader.Read();
                                string imagen = (string)reader["TM"];
                                if (imagen == ConfigurationManager.AppSettings["ImagenNom"])
                                {

                                }
                                else
                                {

                                    var folderPath = Server.MapPath("~/perfil/");
                                    System.IO.DirectoryInfo folderInfo = new DirectoryInfo(folderPath);
                                    try
                                    {
                                        System.IO.File.Delete(folderInfo + imagen);
                                    }
                                    catch
                                    {

                                    }



                                }
                                // nothing need to add here now
                            }


                            string fileName = Path.GetFileNameWithoutExtension(editUser.ImageFile.FileName);
                            string extension = Path.GetExtension(editUser.ImageFile.FileName);
                            fileName = fileName + DateTime.Now.ToString("yymmssffff") + extension;
                            editUser.ImagePathUser = ConfigurationManager.AppSettings["RutaImagenPerfil"] + fileName;
                            fileName = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["RutaImagenPerfil"]), fileName);
                            editUser.ImageFile.SaveAs(fileName);
                            string adsde = @"update AspNetUsers set ImagePathUser = '" + fileName + "' where Email = '" + editUser.Email + "'";
                            SqlCommand rsf = new SqlCommand(sqlCommand, con);
                            rsf.ExecuteReader();
                        }
                    }
                    else
                    {

                        TempData["ResultMessage"] = "El archivo no es una Imagen";
                        TempData["ResultType"] = "W";
                        return RedirectToAction("Index");
                    }

                }
                if (editUser.ImageFile == null)
                {
                    user.UserName = editUser.Email;
                    user.Email = editUser.Email;
                    user.Nombre = editUser.Nombre;
                    user.ApellidoPat = editUser.ApellidoPat;
                   
                }
                else
                {
                    user.UserName = editUser.Email;
                    user.Email = editUser.Email;
                    user.Nombre = editUser.Nombre;
                    user.ApellidoPat = editUser.ApellidoPat;
                    user.ImagePathUser = editUser.ImagePathUser;
                }

       

                    var userRoles = await UserManager.GetRolesAsync(user.Id);

                    selectedRole = selectedRole ?? new string[] { };

                    var result = await UserManager.AddToRolesAsync(user.Id, selectedRole.Except(userRoles).ToArray<string>());

                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", result.Errors.First());
                        return View();
                    }
                    result = await UserManager.RemoveFromRolesAsync(user.Id, userRoles.Except(selectedRole).ToArray<string>());

                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", result.Errors.First());
                        return View();
                    }



                return RedirectToAction(editUser.Id.ToString(), "AdminUsers/Edit");
            }
            ModelState.AddModelError("", "Something failed.");
            return View();
        }

      
        public async Task<ActionResult> Editar_Usuario()
        {

            using (BlackSysEntities dc = new BlackSysEntities())
            {
                string usuario = User.Identity.GetUserName().ToString();

                model = dc.AspNetUsers.Where(x => x.Email == usuario).ToList();
                var s = model[0].Id;
                Perfil rec = new Perfil
                {
                    Id = s
                };
                ViewData["Message"] = rec;
                var user = await UserManager.FindByIdAsync(s);
                var userRoles = await UserManager.GetRolesAsync(s);
                return View(new Perfil()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Nombre = user.Nombre,
                    ApellidoPat = user.ApellidoPat,
                    ImagePathUser = user.ImagePathUser,
                    // Include the Addresss info:

                });

            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar_Usuario([Bind(Include = "Email,Id,Nombre,ApellidoPat,ImageFile,ApellidoPat,ApellidoMat,ImagePathUser")] Perfil editUser)
        {
            string usuario = User.Identity.GetUserName().ToString();

            using (BlackSysEntities dc = new BlackSysEntities())
            {

                model = dc.AspNetUsers.Where(x => x.Email == usuario).ToList();
            var s = model[0].Id;
            editUser.Id = s;

            }

            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(editUser.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                if (editUser.ImageFile == null)
                {

                }
                else
                {
                    if (editUser.ImageFile.ContentType == "image/png" || editUser.ImageFile.ContentType == "image/jpeg")
                    {

                        string conStr = ConfigurationManager.ConnectionStrings["BlackSysConnection"].ConnectionString;
                        // string sqlCommand = @"SP_getImage @Nombre_user ='" + cliente.Nombre_user + "'";
                        // string sqlCommand = @"Select ImagePathUser FROM Clientes Where Nombre_user ='" + cliente.Nombre_user + "'";
                        string sqlCommand = @"select replace(ImagePathUser,'" + ConfigurationManager.AppSettings["ImagenPerfil"] + "', '')TM from AspNetUsers Where Email ='" + editUser.Email + "'";

                        //you can notice here I have added table name like this [dbo].[Contacts] with [dbo], its mendatory when you use Sql Dependency
                        using (SqlConnection con = new SqlConnection(conStr))
                        {
                            SqlCommand cmd = new SqlCommand(sqlCommand, con);

                            if (con.State != System.Data.ConnectionState.Open)
                            {
                                con.Open();
                            }
                            cmd.Notification = null;



                            //we must have to execute the command here
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                reader.Read();
                                string imagen = (string)reader["TM"];
                                if (imagen == ConfigurationManager.AppSettings["ImagenNom"])
                                {

                                }
                                else
                                {

                                    var folderPath = Server.MapPath("~/perfil/");
                                    System.IO.DirectoryInfo folderInfo = new DirectoryInfo(folderPath);
                                    try
                                    {
                                        System.IO.File.Delete(folderInfo + imagen);
                                    }
                                    catch
                                    {

                                    }



                                }
                                // nothing need to add here now
                            }


                            string fileName = Path.GetFileNameWithoutExtension(editUser.ImageFile.FileName);
                            string extension = Path.GetExtension(editUser.ImageFile.FileName);
                            fileName = fileName + DateTime.Now.ToString("yymmssffff") + extension;
                            editUser.ImagePathUser = ConfigurationManager.AppSettings["RutaImagenPerfil"] + fileName;
                            fileName = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["RutaImagenPerfil"]), fileName);
                            editUser.ImageFile.SaveAs(fileName);
                            string adsde = @"update AspNetUsers set ImagePathUser = '" + fileName + "' where Email = '" + editUser.Email + "'";
                            SqlCommand rsf = new SqlCommand(sqlCommand, con);
                            rsf.ExecuteReader();
                        }
                    }
                    else
                    {
                        TempData["ResultMessage"] = "El archivo no es una Imagen";
                        TempData["ResultType"] = "W";
                    }

                }
                if (editUser.ImageFile == null)
                {
                    user.UserName = editUser.Email;
                    user.Email = editUser.Email;
                    user.Nombre = editUser.Nombre;
                    user.ApellidoPat = editUser.ApellidoPat;

                }
                else
                {
                    user.UserName = editUser.Email;
                    user.Email = editUser.Email;
                    user.Nombre = editUser.Nombre;
                    user.ApellidoPat = editUser.ApellidoPat;
                    user.ImagePathUser = editUser.ImagePathUser;
                }

                //string usuario = User.Identity.GetUserName().ToString();
                //var roles = ((ClaimsIdentity)User.Identity).Claims
                //.Where(c => c.Type == ClaimTypes.Role)
                //.Select(c => c.Value).ToList();

                //var rol = roles[0].ToString();

               
                

                
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Something failed.");
            return View();
        }

        // GET: /Users/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var user = await UserManager.FindByIdAsync(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                //BlackSysEntities db = new BlackSysEntities();
                //db.Database.SqlQuery<decimal>("SP_delete_user @UserID ='"+id+"'").FirstOrDefault();

                string conStr = ConfigurationManager.ConnectionStrings["BlackSysConnection"].ConnectionString;
                string sqlCommand = @"SP_delete_user @UserID ='"+ id + "'";
                //you can notice here I have added table name like this [dbo].[Contacts] with [dbo], its mendatory when you use Sql Dependency
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    SqlCommand cmd = new SqlCommand(sqlCommand, con);
                    
                    if (con.State != System.Data.ConnectionState.Open)
                    {
                        con.Open();
                    }
                    cmd.Notification = null;


                    //we must have to execute the command here
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // nothing need to add here now
                    }
                }
                //var result = await UserManager.DeleteAsync(user);

                //   ModelState.AddModelError("", result.Errors.First());
                // return View();

                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
