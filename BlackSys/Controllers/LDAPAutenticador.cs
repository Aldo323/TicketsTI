using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Web;

namespace API_TEST.Controllers
{
    public class LDAPAutenticador
    {
        private ArrayList grupos = new ArrayList();
        private string _path;
        private ArrayList listaPropiedades = new ArrayList();

        public LDAPAutenticador(string path)
        {
            _path = path;
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

    }
}