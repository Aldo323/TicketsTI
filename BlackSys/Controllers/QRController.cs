using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlackSys.Models;

namespace BlackSys.Controllers
{
    public class QRController : Controller
    {
        // GET: QR
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Index(QR data)
        {
            int mr = data.cantidad;

            

            return RedirectToAction("Codigos", new { brands = mr });
        }

        public ActionResult Codigos(int brands)
        {
            List<string> Qrs = new List<string>();
            List<QR> codigos = new List<QR>();
           
            for (int i = 0; i < brands; i++)
            {
                codigos.Add(new QR
                {
                   idu = Guid.NewGuid().ToString()
                });
                //Qrs.Add(Guid.NewGuid().ToString());

            }
            ViewBag.Customers = codigos;
            return View();
        }

    }
}