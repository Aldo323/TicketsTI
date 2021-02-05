using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Microsoft.AspNet.SignalR;
using BlackSys.Models;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;

namespace BlackSys
{
    public class NotificationComponent
    {
        private static bool _alreadySuscribed;
        List<String> lst = new List<String>();
        //Here we will add a function for register notification (will add sql dependency)
        public void RegisterNotification(DateTime currentTime)
        {
            try
            {

          
            string conStr = ConfigurationManager.ConnectionStrings["BlackSysConnection"].ConnectionString;
            // string sqlCommand = @"SELECT [Id_Solicitud],[Mensaje],[Email_user],[AddedOn]  from [dbo].[Mensajes] where [AddedOn] > @AddedOn";
            string sqlCommand = @"SELECT [IdSolicitud],[Notify],[Mail_user],[Date],[Path],[Nombre]  from [dbo].[Notify] ";
            //you can notice here I have added table name like this [dbo].[Contacts] with [dbo], its mendatory when you use Sql Dependency
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand(sqlCommand, con);
                //cmd.Parameters.AddWithValue("@AddedOn", currentTime);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                cmd.Notification = null;
            
                   
                    if(!NotificationComponent._alreadySuscribed)
                {
                    SqlDependency sqlDep = new SqlDependency(cmd);
                    sqlDep.OnChange += sqlDep_OnChange;
                    NotificationComponent._alreadySuscribed = true;
                }
               


                //we must have to execute the command here
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // nothing need to add here now
                }
                }
            }
            catch
            {

            }
        }
              
        void sqlDep_OnChange(object sender, SqlNotificationEventArgs e)
        {
                if (e.Type == SqlNotificationType.Change)
                {
                SqlDependency sqlDep = sender as SqlDependency;
                sqlDep.OnChange -= sqlDep_OnChange;
                NotificationComponent._alreadySuscribed = false;

                //string emailuser = @"SELECT Email_user FROM Mensajes WHERE Id_Mensaje = (SELECT MAX(Id_Mensaje) from Mensajes)";
                //string emailuser = @"SELECT l.Correo FROM Mensajes as c INNER JOIN  Solicitudes  l on c.Id_Solicitud = l.IdSolicitud WHERE c.Id_Mensaje =  (SELECT MAX(Id_Mensaje) from Mensajes)";
                // string emailuser = @"SELECT DISTINCT Mail_User FROM Notify WITH (NOLOCK) WHERE idSolicitud IN (SELECT idSolicitud FROM Notify WITH (NOLOCK) WHERE idNotify IN (SELECT TOP 1 idNotify    FROM Notify WITH (NOLOCK) ORDER BY idNotify DESC))";

                //string conStr = ConfigurationManager.ConnectionStrings["BlackSysConnection"].ConnectionString;
                //  using (SqlConnection con = new SqlConnection(conStr))
                //  {
                //     if (con.State != System.Data.ConnectionState.Open)
                //    {
                //con.Open();
                //   }
                //  SqlCommand cmd_2 = new SqlCommand(emailuser, con);
                //  using (SqlDataReader reader_2 = cmd_2.ExecuteReader())
                //  {
                //     if (reader_2.Read())
                //    {
              
                BlackSysEntities db = new BlackSysEntities();
                List<MensajeSP> Informe = db.Database.SqlQuery<MensajeSP>("Sp_UserNotify").ToList();
                int cantidad = Informe.Count();
              //  String s = Informe[1].ToString();
                //string contact = (string)reader_2["Mail_User"];
                NotificationHub conector = new NotificationHub();
               for(int i=0; i < cantidad; i++)
                {
                    String s = Informe[i].Email_user;
                    conector.Show(s);
                    RegisterNotification(DateTime.Now);
                }
              
                
                                
                                /// ///notificationHub.Clients.All.notify(contact);
                              //  RegisterNotification(DateTime.Now);
                           
                    //        }
                      //  }
                 //   }

                }

        }



        public List<Mensajes> GetContacts(DateTime afterDate)
        {
            using (BlackSysEntities dc = new BlackSysEntities())
            {
                return dc.Mensajes.Where(a => a.AddedOn > afterDate).OrderByDescending(a => a.AddedOn).ToList();
            }
        }
    }
}