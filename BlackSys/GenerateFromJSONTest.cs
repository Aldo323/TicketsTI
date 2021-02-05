using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using BlackSys.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace BlackSys
{
    public class GenerateFromJSONTest
    {

        public void Poster(RangoSolicitudes value)
        {
            var url = "http://services.odata.org/V4/Northwind/Northwind.svc/Products?$format=json";  //get products from ODATA service  
            string apiurl = "http://demos.componentone.com/ASPNET/C1WebAPIService/api/excel";
            using (var clientjs = new HttpClient())
            {
                HttpResponseMessage responseJSON = clientjs.GetAsync(url).Result;
                responseJSON.EnsureSuccessStatusCode();
                var responseBody = responseJSON.Content.ReadAsStringAsync().Result;  //Get JSON from ODATA service  

                var data = JsonConvert.DeserializeObject(responseBody);  //use JsonConvert to deserialize raw json  

                using (var client = new HttpClient())
                using (var formData = new MultipartFormDataContent())
                {
                    var fileFormat = "xlsx";
                    formData.Add(new StringContent("Test"), "FileName");
                    formData.Add(new StringContent(fileFormat), "FileFormat");
                    formData.Add(new StringContent(JsonConvert.SerializeObject(data)), "Data");
                    //Call WebAPI to get Excel  
                    var response = client.PostAsync(apiurl, formData).Result;
                    if (!response.IsSuccessStatusCode)
                    {
                       
                    }
                    var tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
                    if (!Directory.Exists(tempPath))
                    {
                        Directory.CreateDirectory(tempPath);
                    }
                    //Save Excel to Tem directory.  
                    var tempFilePath = Path.Combine(tempPath, string.Format("{0}.{1}", "Test", fileFormat));
                    using (var newFile = File.Create(tempFilePath))
                    {
                        response.Content.ReadAsStreamAsync().Result.CopyTo(newFile);
                    }
                    //Open Excel to view.  
                    Process.Start(tempFilePath);
                }
            }
        }
        
    }
}