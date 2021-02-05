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
    public class GenerateFromJSON
    {

        public async Task<object> GetUser(RangoSolicitudes value)
        {
            var url = @System.Configuration.ConfigurationManager.AppSettings["Request"] +"/api/Xls";  //get products from ODATA service  

            string apiurl = "http://demos.componentone.com/ASPNET/C1WebAPIService/api/excel";

                IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
                    {
                    new KeyValuePair<string, string>("Fecha_inicio", value.Fecha_inicio.Value.ToString("yyyy-MM-dd")),
                    new KeyValuePair<string, string>("Fecha_fin", value.Fecha_fin.Value.ToString("yyyy-MM-dd"))
                    };
         
                HttpContent q = new FormUrlEncodedContent(queries);
                using (HttpClient clientj = new HttpClient())
                {
                    using (HttpResponseMessage responseJSON = await clientj.PostAsync(url,q))
                    {
                        responseJSON.EnsureSuccessStatusCode();
                        var responseBody = await responseJSON.Content.ReadAsStringAsync();  //Get JSON from ODATA service  
                        var data = JsonConvert.DeserializeObject(responseBody);  //use JsonConvert to deserialize raw json 

                    using (var client = new HttpClient())
                    using (HttpContent content = responseJSON.Content)
                        {
                            string  mycontent = await content.ReadAsStringAsync();
                            HttpContentHeaders headers = content.Headers;                   
                      using (var formData = new MultipartFormDataContent())
                            {
                                var fileFormat = "xlsx";
                                formData.Add(new StringContent("Test"), "FileName");
                                formData.Add(new StringContent(fileFormat), "FileFormat");
                                formData.Add(new StringContent(JsonConvert.SerializeObject(mycontent)), "Data");


                                //Call WebAPI to get Excel  
                                var response = await client.PostAsync(apiurl, formData);
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
                                    (await response.Content.ReadAsStreamAsync()).CopyTo(newFile);
                                }
                                //Open Excel to view.  
                                Process.Start(tempFilePath);
                            }
                        }
                    }
                }
               


            return ""; 
        }
        
    }
}