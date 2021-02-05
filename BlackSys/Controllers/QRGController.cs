using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using BlackSys.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using QRCoder;

namespace BlackSys.Controllers
{
    [Authorize]
    public class QRGController : ApiController
    {
        List<AspNetRoles> model;
        List<AspNetUserRoles> roles;

        public string Get(int id)
        {
            return "value";
        }

        private static byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }

        [HttpPost]
    public string Post(QR obj)
    {
            
            int mr = obj.cantidad;

            List<string> Qrs = new List<string>();

            for ( int i =0; i< mr; i ++)
            {
                Qrs.Add(Guid.NewGuid().ToString());
                //QRCodeGenerator qr = new QRCodeGenerator();
                //QRCodeData data = qr.CreateQrCode(Guid.NewGuid().ToString(), QRCodeGenerator.ECCLevel.Q);
                //QRCode code = new QRCode(data);
                //Bitmap img1 = null;
                //img1 = new System.Drawing.Bitmap(code.GetGraphic(5));
                //var bitmapBytes = BitmapToBytes(img1);
             
           //     List<byte> images = new List<byte>();
           ////     images.Add(bitmapBytes);
           //     //ConvertBitMapToByteArray(img1);

               // return File(bitmapBytes, "image/jpeg");


            }


            return mr.ToString();
    }
        

        // PUT api/values/5
        public void Put(int id, [FromBody] Solicitudes value)
        {
          
            
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
