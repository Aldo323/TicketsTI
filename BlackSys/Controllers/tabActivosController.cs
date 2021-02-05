using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using API_TEST.Controllers;
using BlackSys.Models;
using Microsoft.AspNet.Identity;
using QRCoder;

namespace BlackSys.Controllers
{
    [Authorize]
    public class tabActivosController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: tab_Activos
        public ActionResult Index()
        {
            string correo = User.Identity.GetUserName().ToString();
            string vistamg = db.Database.SqlQuery<string>("Sp_loadStringMenu  @User ='" + correo + "', @idMenu= 19 ").FirstOrDefault();
            if (vistamg == null)
            {

                return View("~/Views/Error/Denegado.cshtml");
            }
            else
            {
                return View(vistamg, db.tab_Activos.ToList());
            }
           
        }

        // GET: tab_Activos/Details/5


        public ActionResult convertirImagen(Guid Id_user)
        {
            List<tab_Activos> modelocliente;

            modelocliente = db.tab_Activos.Where(z => z.IdActivo == Id_user).ToList();
            return File(modelocliente[0].ImagePathActivo, "image/jpeg");


        }

        public byte[] ConvertBitMapToByteArray(Bitmap bitmap)
        {
            byte[] result = null;

            if (bitmap != null)
            {
                MemoryStream stream = new MemoryStream();
                bitmap.Save(stream, bitmap.RawFormat);
                result = stream.ToArray();
            }

            return result;
        }
        private static byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
        public ActionResult qrgenerate(Guid? id)
        {
            
            QRCodeGenerator qr = new QRCodeGenerator();
            QRCodeData data = qr.CreateQrCode(ConfigurationManager.AppSettings["QRData"].ToString()+id.ToString(), QRCodeGenerator.ECCLevel.Q);
            QRCode code = new QRCode(data);
            Bitmap img1 = null;
            img1 = new System.Drawing.Bitmap(code.GetGraphic(5));
            var bitmapBytes = BitmapToBytes(img1);
            //ConvertBitMapToByteArray(img1);

            return File(bitmapBytes, "image/jpeg");


        }

     
        public ActionResult EditAbierto(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tab_Activos activosfijos = db.tab_Activos.Find(id);
            if (activosfijos == null)
            {
                return HttpNotFound();
            }
            return View(activosfijos);
        }


        public ActionResult Generar()
        {

            string correo = User.Identity.GetUserName().ToString();
            string vistamg = db.Database.SqlQuery<string>("Sp_loadStringMenu  @User ='" + correo + "', @idMenu= 21 ").FirstOrDefault();
            if (vistamg == null)
            {

                return View("~/Views/Error/Denegado.cshtml");
            }
            else
            {
                //List<cat_Activos> ActivoList = db.cat_Activos.ToList();
                //ViewBag.ActivoList = new SelectList(ActivoList, "idCategoria", "Nombre");

                return View(vistamg);
            }

        }

        // GET: tab_Activos/Create
        public ActionResult Create(Guid? id)
        {
            List<cat_Activos> ActivoList = db.cat_Activos.ToList();
            ViewBag.ActivoList = new SelectList(ActivoList, "idCategoria", "Nombre");
            string correo = User.Identity.GetUserName().ToString();
            string vistamg = db.Database.SqlQuery<string>("Sp_loadStringMenu  @User ='" + correo + "', @idMenu= 18 ").FirstOrDefault();
            if (vistamg == null)
            {

                return View("~/Views/Error/Denegado.cshtml");
            }
            else
            {
                Guid verifyGUID = db.Database.SqlQuery<Guid>("Sp_guid  @uid ='" + id + "'").FirstOrDefault();
                string guid = verifyGUID.ToString();

                if (guid == "00000000-0000-0000-0000-000000000000")
                {



                    return View(vistamg);

                }
                else
                {
                    ActivosList mr = new ActivosList();
                   // mr.Mode(verifyGUID);

                    return View("Mode", mr.Mode(verifyGUID));
                    
                }

               
            }
           
        }

        

        // POST: tab_Activos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tab_Activos EditActivo)
        {
            BlackSysEntities db = new BlackSysEntities();

            if (EditActivo.ImageFile == null)
            {

                IEnumerable<RangoFechas> Informe = db.Database.SqlQuery<RangoFechas>("sp_New_Activo  @No_Categoria = '" + EditActivo.No_Categoria +
   "', @Estado = '" + EditActivo.Estado + "', @Asignado = '" + EditActivo.Asignado + "', @Re_Asignable = '" + EditActivo.Re_Asignable + "', @Ubicado = '" + EditActivo.Ubicado +
   "', @Marca = '" + EditActivo.Marca + "', @No_Serial = '" + EditActivo.No_Serial + "', @UsuarioAsig = '" + EditActivo.UsuarioAsig + "', @Nombre_Activo = '" + EditActivo.Nombre_Activo +
   "', @Modelo = '" + EditActivo.Modelo + "', @Caracteristicas = '" + EditActivo.Caracteristicas + "', @Notas = '" + EditActivo.Notas + "', @ImagePathActivo = '" + EditActivo.ImagePathActivo + "'").ToList();
                return RedirectToAction("Index");
            }
            else
            {
                if (EditActivo.ImageFile.ContentType == "image/png" || EditActivo.ImageFile.ContentType == "image/jpeg")
                {

                    string conStr = ConfigurationManager.ConnectionStrings["BlackSysConnection"].ConnectionString;
                    // string sqlCommand = @"SP_getImage @Nombre_user ='" + cliente.Nombre_user + "'";
                    // string sqlCommand = @"Select ImagePathUser FROM Clientes Where Nombre_user ='" + cliente.Nombre_user + "'";
                    string sqlCommand = @"select replace(ImagePathActivo,'" + ConfigurationManager.AppSettings["ImgActivo"] + "', '')TM from tab_Activos Where IdActivo ='" + EditActivo.IdActivo + "'";

                    //you can notice here I have added table name like this [dbo].[Contacts] with [dbo], its mendatory when you use Sql Dependency
                    using (SqlConnection con = new SqlConnection(conStr))
                    {
                        SqlCommand cmd = new SqlCommand(sqlCommand, con);

                        if (con.State != System.Data.ConnectionState.Open)
                        {
                            con.Open();
                        }
                        cmd.Notification = null;


                        string fileName = Path.GetFileNameWithoutExtension(EditActivo.ImageFile.FileName);
                        string extension = Path.GetExtension(EditActivo.ImageFile.FileName);
                        fileName = fileName + DateTime.Now.ToString("yymmssffff") + extension;
                        EditActivo.ImagePathActivo = ConfigurationManager.AppSettings["ImgActivo"] + fileName;
                        fileName = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["ImgActivo"]), fileName);
                        EditActivo.ImageFile.SaveAs(fileName);
                        //string adsde = @"update Clientes set ImagePathUser = '" + fileName + "' where Nombre_user = '" + cliente.Nombre_user + "'";
                        //SqlCommand rsf = new SqlCommand(sqlCommand, con);
                        //rsf.ExecuteReader();
                    }
                }
                if (EditActivo.ImagePathActivo == null || EditActivo.ImagePathActivo == "")
                {
                    IEnumerable<RangoFechas> Informe = db.Database.SqlQuery<RangoFechas>("sp_New_Activo  @No_Categoria = '" + EditActivo.No_Categoria +
   "', @Estado = '" + EditActivo.Estado + "', @Asignado = '" + EditActivo.Asignado + "', @Re_Asignable = '" + EditActivo.Re_Asignable + "', @Ubicado = '" + EditActivo.Ubicado +
   "', @Marca = '" + EditActivo.Marca + "', @No_Serial = '" + EditActivo.No_Serial + "', @UsuarioAsig = '" + EditActivo.UsuarioAsig + "', @Nombre_Activo = '" + EditActivo.Nombre_Activo +
   "', @Modelo = '" + EditActivo.Modelo + "', @Caracteristicas = '" + EditActivo.Caracteristicas + "', @Notas = '" + EditActivo.Notas + "', @ImagePathActivo = ''").ToList();
                }
                else
                {
                    IEnumerable<RangoFechas> Informe = db.Database.SqlQuery<RangoFechas>("sp_New_Activo  @No_Categoria = '" + EditActivo.No_Categoria +
   "', @Estado = '" + EditActivo.Estado + "', @Asignado = '" + EditActivo.Asignado + "', @Re_Asignable = '" + EditActivo.Re_Asignable + "', @Ubicado = '" + EditActivo.Ubicado +
   "', @Marca = '" + EditActivo.Marca + "', @No_Serial = '" + EditActivo.No_Serial + "', @UsuarioAsig = '" + EditActivo.UsuarioAsig + "', @Nombre_Activo = '" + EditActivo.Nombre_Activo +
   "', @Modelo = '" + EditActivo.Modelo + "', @Caracteristicas = '" + EditActivo.Caracteristicas + "', @Notas = '" + EditActivo.Notas + "', @ImagePathActivo = '" + EditActivo.ImagePathActivo + "'").ToList();
                }
            }
            return RedirectToAction("Index");
        }
        // POST: tab_Activos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.


        //---------------------------------------------------
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "IdActivo,Codigo,No_Categoria,idCategoria,Estado,Asignado,Re_Asignable,Ubicado,Marca,No_Serial,UsuarioAsig,Nombre_Activo,Notas,Modelo,Caracteristicas")] tab_Activos tab_Activos)
        //{
        //    //if (ModelState.IsValid)
        //    //{
        //    //    tab_Activos.IdActivo = Guid.NewGuid();
        //    //    db.tab_Activos.Add(tab_Activos);
        //    //    db.SaveChanges();
        //    //    return RedirectToAction("Index");
        //    //}
        //    //////BlackSysEntities db = new BlackSysEntities();
        //    //////IEnumerable<RangoFechas> Informe = db.Database.SqlQuery<RangoFechas>("SP_Reportig_ActivityTickets @DateStaring ='" + value.Fecha_inicio + "', @DateFiniched = '" + value.Fecha_fin + "'").ToList();
        //    BlackSysEntities db = new BlackSysEntities();
        //    IEnumerable<RangoFechas> Informe = db.Database.SqlQuery<RangoFechas>("sp_New_Activo  @No_Categoria = '" + tab_Activos.No_Categoria+
        //        "', @Estado = '" + tab_Activos.Estado + "', @Asignado = '" + tab_Activos.Asignado + "', @Re_Asignable = '" + tab_Activos.Re_Asignable + "', @Ubicado = '" + tab_Activos.Ubicado +
        //        "', @Marca = '" + tab_Activos.Marca + "', @No_Serial = '" + tab_Activos.No_Serial + "', @UsuarioAsig = '" + tab_Activos.UsuarioAsig + "', @Nombre_Activo = '" + tab_Activos.Nombre_Activo +
        //        "', @Modelo = '" + tab_Activos.Modelo + "', @Caracteristicas = '" + tab_Activos.Caracteristicas + "', @Notas = '" + tab_Activos.Notas + "', @ImagePathActivo = '" + tab_Activos.ImagePathActivo + "'").ToList();
        //    return RedirectToAction("Index");
        //}
        //---------------------------------------


        //public async Task<ActionResult> Edit()
        //{
        //    List<tab_Activos> model;
        //    using (BlackSysEntities dc = new BlackSysEntities())
        //    {
        //        string usuario = User.Identity.GetUserName().ToString();

        //        model = dc.tab_Activos.Where(x => x.Email == usuario).ToList();
        //        var s = model[0].Id;
        //        Perfil rec = new Perfil
        //        {
        //            Id = s
        //        };
        //        ViewData["Message"] = rec;
        //        var user = await UserManager.FindByIdAsync(s);
        //        var userRoles = await UserManager.GetRolesAsync(s);
        //        return View(new Perfil()
        //        {
        //            Id = user.Id,
        //            Email = user.Email,
        //            Nombre = user.Nombre,
        //            ApellidoPat = user.ApellidoPat,
        //            ImagePathUser = user.ImagePathUser,
        //            // Include the Addresss info:

        //        });

        //    }
        //}

        // GET: tab_Activos/Edit/5
        public ActionResult Edit(Guid? id)
        {
            string correo = User.Identity.GetUserName().ToString();
            string vistamg = db.Database.SqlQuery<string>("Sp_loadStringMenu  @User ='" + correo + "', @idMenu= 20 ").FirstOrDefault();
            if (vistamg == null)
            {

                return View("~/Views/Error/Denegado.cshtml");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tab_Activos tab_Activos = db.tab_Activos.Find(id);
                if (tab_Activos == null)
                {
                    return HttpNotFound();
                }

                return View(vistamg,tab_Activos);

            }
            
        }

        // POST: tab_Activos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tab_Activos EditActivo)
        {
            BlackSysEntities db = new BlackSysEntities();

            if(EditActivo.ImageFile == null)
            {
                
                    IEnumerable<tab_Activos> Informe = db.Database.SqlQuery<tab_Activos>("sp_Edit_Activo @idActivo ='" + EditActivo.IdActivo + "', @Codigo = '" + EditActivo.Codigo + "', @No_Categoria = '" + EditActivo.No_Categoria + "', @idCategoria = '" + EditActivo.idCategoria +
                   "', @Estado = '" + EditActivo.Estado + "', @Asignado = '" + EditActivo.Asignado + "', @Re_Asignable = '" + EditActivo.Re_Asignable + "', @Ubicado = '" + EditActivo.Ubicado +
                   "', @Marca = '" + EditActivo.Marca + "', @No_Serial = '" + EditActivo.No_Serial + "', @UsuarioAsig = '" + EditActivo.UsuarioAsig + "', @Nombre_Activo = '" + EditActivo.Nombre_Activo +
                   "', @Modelo = '" + EditActivo.Modelo + "', @Notas = '" + EditActivo.Notas+ "', @Caracteristicas = '" + EditActivo.Caracteristicas + "', @ImagePathActivo = ''").ToList();
                return RedirectToAction("Index");
            }
            else
            {
                if (EditActivo.ImageFile.ContentType == "image/png" || EditActivo.ImageFile.ContentType == "image/jpeg")
                {

                    string conStr = ConfigurationManager.ConnectionStrings["BlackSysConnection"].ConnectionString;
                    // string sqlCommand = @"SP_getImage @Nombre_user ='" + cliente.Nombre_user + "'";
                    // string sqlCommand = @"Select ImagePathUser FROM Clientes Where Nombre_user ='" + cliente.Nombre_user + "'";
                    string sqlCommand = @"select replace(ImagePathActivo,'" + ConfigurationManager.AppSettings["ImgActivo"] + "', '')TM from tab_Activos Where IdActivo ='" + EditActivo.IdActivo + "'";

                    //you can notice here I have added table name like this [dbo].[Contacts] with [dbo], its mendatory when you use Sql Dependency
                    using (SqlConnection con = new SqlConnection(conStr))
                    {
                        SqlCommand cmd = new SqlCommand(sqlCommand, con);

                        if (con.State != System.Data.ConnectionState.Open)
                        {
                            con.Open();
                        }
                        cmd.Notification = null;

                        string pathimg = db.Database.SqlQuery<string>("Sp_ImgActivo  @id ='" + EditActivo.IdActivo + "'").FirstOrDefault();
                        string acimg = Path.GetFileName(pathimg.ToString());
                        
                        System.IO.File.Delete(ConfigurationManager.AppSettings["Image_ActPath"] + acimg);
                     

                        string fileName = Path.GetFileNameWithoutExtension(EditActivo.ImageFile.FileName);
                        string extension = Path.GetExtension(EditActivo.ImageFile.FileName);
                        fileName = fileName + DateTime.Now.ToString("yymmssffff") + extension;
                        EditActivo.ImagePathActivo = ConfigurationManager.AppSettings["ImgActivo"] + fileName;
                        fileName = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["ImgActivo"]), fileName);

                        EditActivo.ImageFile.SaveAs(fileName);
                        //string adsde = @"update Clientes set ImagePathUser = '" + fileName + "' where Nombre_user = '" + cliente.Nombre_user + "'";
                        //SqlCommand rsf = new SqlCommand(sqlCommand, con);
                        //rsf.ExecuteReader();
                    }
                }
                if (EditActivo.ImagePathActivo == null || EditActivo.ImagePathActivo == "")
                {
                    IEnumerable<tab_Activos> Informe = db.Database.SqlQuery<tab_Activos>("sp_Edit_Activo @idActivo ='" + EditActivo.IdActivo + "', @Codigo = '" + EditActivo.Codigo + "', @No_Categoria = '" + EditActivo.No_Categoria + "', @idCategoria = '" + EditActivo.idCategoria +
                   "', @Estado = '" + EditActivo.Estado + "', @Asignado = '" + EditActivo.Asignado + "', @Re_Asignable = '" + EditActivo.Re_Asignable + "', @Ubicado = '" + EditActivo.Ubicado +
                   "', @Marca = '" + EditActivo.Marca + "', @No_Serial = '" + EditActivo.No_Serial + "', @UsuarioAsig = '" + EditActivo.UsuarioAsig + "', @Nombre_Activo = '" + EditActivo.Nombre_Activo +
                   "', @Modelo = '" + EditActivo.Modelo + "', @Notas = '" + EditActivo.Notas + "', @Caracteristicas = '" + EditActivo.Caracteristicas).ToList();
                }
                else
                {
                    IEnumerable<tab_Activos> Informe = db.Database.SqlQuery<tab_Activos>("sp_Edit_Activo @idActivo ='" + EditActivo.IdActivo + "', @Codigo = '" + EditActivo.Codigo + "', @No_Categoria = '" + EditActivo.No_Categoria + "', @idCategoria = '" + EditActivo.idCategoria +
                   "', @Estado = '" + EditActivo.Estado + "', @Asignado = '" + EditActivo.Asignado + "', @Re_Asignable = '" + EditActivo.Re_Asignable + "', @Ubicado = '" + EditActivo.Ubicado +
                   "', @Marca = '" + EditActivo.Marca + "', @No_Serial = '" + EditActivo.No_Serial + "', @UsuarioAsig = '" + EditActivo.UsuarioAsig + "', @Nombre_Activo = '" + EditActivo.Nombre_Activo +
                   "', @Modelo = '" + EditActivo.Modelo + "', @Notas = '" + EditActivo.Notas + "', @Caracteristicas = '" + EditActivo.Caracteristicas + "', @ImagePathActivo = '" + EditActivo.ImagePathActivo + "'").ToList();
                }
            }
           
            //if (ModelState.IsValid)
            //{
            //    db.Entry(tab_Activos).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            
           
            return RedirectToAction("Index");
        }

        private IList<SubActivos> GetSubActivos(int Activoid)
        {

            var data = db.SubActivos.Where(m => m.IdActivo == Activoid).ToList();
            return data;

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult FroalaAutoSave(string body, int? postId)
        {
            body = Microsoft.Security.Application.Sanitizer.GetSafeHtml(body);

            //todo: save body ...
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult FroalaUploadFile(HttpPostedFileBase file, int? postId)
        {
            var fileName = Path.GetFileName(file.FileName);
            var rootPath = Server.MapPath(ConfigurationManager.AppSettings["ActivoFlie"]);
            file.SaveAs(Path.Combine(rootPath, fileName));
            return Json(new { link = ConfigurationManager.AppSettings["ActivoFlie_return"] + fileName }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult FroalaUploadImage(HttpPostedFileBase file, int? postId)
        {
            var fileName = Path.GetFileName(file.FileName);
            var rootPath = Server.MapPath(ConfigurationManager.AppSettings["ActivoImg"]);
            file.SaveAs(Path.Combine(rootPath, fileName));
            return Json(new { link = ConfigurationManager.AppSettings["ActivoImg_return"] + fileName }, JsonRequestBehavior.AllowGet);
        }

        // GET: tab_Activos/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tab_Activos tab_Activos = db.tab_Activos.Find(id);
            if (tab_Activos == null)
            {
                return HttpNotFound();
            }
            return View(tab_Activos);
        }

        
        // GET: Activos/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tab_Activos activos = db.tab_Activos.Find(id);
            if (activos == null)
            {
                return HttpNotFound();
            }
            return View(activos);
        }

        // POST: tab_Activos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            tab_Activos tab_Activos = db.tab_Activos.Find(id);
            db.tab_Activos.Remove(tab_Activos);
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
