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

namespace BlackSys.Controllers
{
    [Authorize]
    public class PerfilController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();
        List<AspNetUsers> model;
        public PerfilController() { }

        public PerfilController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
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

        //[HttpGet]
        //public async Task<ActionResult> Index()
        //{


        //    return View(await UserManager.Users.ToListAsync());
        //}

       

        // GET: /Users/Edit/1
        public async Task<ActionResult> Edit()
        {

            using (BlackSysEntities dc = new BlackSysEntities())
            {
                string usuario = User.Identity.GetUserName().ToString();

                 model = dc.AspNetUsers.Where(x => x.Email == usuario).ToList();
                var s =  model[0].Id;
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

        //
        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Perfil editUser)
        {
            using (BlackSysEntities dc = new BlackSysEntities())
            {
                string usuario = User.Identity.GetUserName().ToString();
                editUser.Email = usuario;
                model = dc.AspNetUsers.Where(x => x.Email == usuario).ToList();
                var s = model[0].Id;
                editUser.Id = s;



                if (editUser.ImageFile == null)
                {
                    IEnumerable<Perfil> Informes = db.Database.SqlQuery<Perfil>("sp_ActualizarUsuario  @Email = '" + editUser.Email +
    "',@Nombre  = '" + editUser.Nombre + "', @ApellidoPat  = '" + editUser.ApellidoPat + "', @ApellidoMat  = '" + editUser.ApellidoMat + "', @ImagePathUser  = '" + "" + "', @id  = '" + editUser.Id + "'").ToList();
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

                            string Other_fileName = ConfigurationManager.AppSettings["RutaImagenPerfil"]+fileName;
                            editUser.ImagePathUser = ConfigurationManager.AppSettings["RutaImagenPerfil"] + fileName;
                            fileName = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["RutaImagenPerfil"]), fileName);
                            editUser.ImageFile.SaveAs(fileName);

                            IEnumerable<Perfil> Informes = db.Database.SqlQuery<Perfil>("sp_ActualizarUsuario  @Email = '" + editUser.Email +
                            "',@Nombre  = '" + editUser.Nombre + "', @ApellidoPat  = '" + editUser.ApellidoPat + "', @ApellidoMat  = '" + editUser.ApellidoMat + 
                            "', @ImagePathUser  = '" + Other_fileName + "', @id  = '" + editUser.Id + "'").ToList();



                            //string adsde = @"update AspNetUsers set ImagePathUser = '" + fileName + "' where Email = '" + editUser.Email + "'";
                            //SqlCommand rsf = new SqlCommand(sqlCommand, con);
                            //rsf.ExecuteReader();
                        }
                    }
                    else
                    {
                        TempData["ResultMessage"] = "El archivo no es una Imagen";
                        TempData["ResultType"] = "W";
                    }

                }
              }

            //IEnumerable<Perfil> Informe = db.Database.SqlQuery<Perfil>("sp_ActualizarUsuario  @Email = '" + editUser.Email +
            //"',@Nombre  = '" + editUser.Nombre + "', @ApellidoPat  = '" + editUser.ApellidoPat + "', @ApellidoMat  = '" + editUser.ApellidoMat + "', @ImagePathUser  = '" + editUser.Id + "', @id  = '" + editUser.Id + "'").ToList();




            return RedirectToAction("Edit");



             }
      
    }
}
