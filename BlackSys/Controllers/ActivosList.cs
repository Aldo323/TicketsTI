using BlackSys.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Web;

namespace API_TEST.Controllers
{
    public class ActivosList
    {
        private ArrayList grupos = new ArrayList();
        private string _path;
        private ArrayList listaPropiedades = new ArrayList();
        private BlackSysEntities db = new BlackSysEntities();

        public ActivosList()
        {
         
        }

        public string GetUser(string user, string password)
        {


            string acceso = "intelexion" + @"\" + user;
            DirectoryEntry entry = new DirectoryEntry(_path, user, password);
            entry.AuthenticationType = AuthenticationTypes.Secure;
            try
            {
                object obj = entry.NativeObject;
            }
            catch
            {
                return "False";
            }
            
          
            return "OK";
        }

        public tab_Activos Mode(Guid id)
        {
            tab_Activos activos = db.tab_Activos.Find(id);
            return activos;
        }
    }
}