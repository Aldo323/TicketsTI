//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Linq;
//using System.Web;
//using System.Data.SqlClient;
//using Microsoft.AspNet.SignalR;
//using BlackSys.Models;
//using System.Runtime.Remoting.Contexts;
//using System.Threading.Tasks;

//namespace BlackSys
//{
//    public class NotificationTables
//    {
//        private static bool _alreadySuscribed;
//        //Here we will add a function for register notification (will add sql dependency)
//        public void RegisterNotification(DateTime currentTime)
//        {
//            string conStr = ConfigurationManager.ConnectionStrings["BlackSysConnection"].ConnectionString;
//            string sqlCommand = @"SELECT [Id_Solicitud],[Mensaje],[Email_user],[AddedOn]  from [dbo].[Mensajes] where [AddedOn] > @AddedOn";
//            //you can notice here I have added table name like this [dbo].[Contacts] with [dbo], its mendatory when you use Sql Dependency
//            using (SqlConnection con = new SqlConnection(conStr))
//            {
//                SqlCommand cmd = new SqlCommand(sqlCommand, con);
//                cmd.Parameters.AddWithValue("@AddedOn", currentTime);
//                if (con.State != System.Data.ConnectionState.Open)
//                {
//                    con.Open();
//                }
//                cmd.Notification = null;
            
                   
//                    if(!NotificationComponent._alreadySuscribed)
//                {
//                    SqlDependency sqlDep = new SqlDependency(cmd);
//                    sqlDep.OnChange += sqlDep_OnChange;
//                    NotificationComponent._alreadySuscribed = true;
//                }
               


//                //we must have to execute the command here
//                using (SqlDataReader reader = cmd.ExecuteReader())
//                {
//                    // nothing need to add here now
//                }
//            }
//        }

//        void sqlDep_OnChange(object sender, SqlNotificationEventArgs e)
//        {
//                if (e.Type == SqlNotificationType.Change)
//                {
//                SqlDependency sqlDep = sender as SqlDependency;
//                sqlDep.OnChange -= sqlDep_OnChange;
//                NotificationComponent._alreadySuscribed = false;

//                //string emailuser = @"SELECT Email_user FROM Mensajes WHERE Id_Mensaje = (SELECT MAX(Id_Mensaje) from Mensajes)";
//                string emailuser = @"SELECT l.Correo FROM Mensajes as c INNER JOIN  Solicitudes  l on c.Id_Solicitud = l.IdSolicitud WHERE c.Id_Mensaje =  (SELECT MAX(Id_Mensaje) from Mensajes)";
//                string conStr = ConfigurationManager.ConnectionStrings["BlackSysConnection"].ConnectionString;
//                    using (SqlConnection con = new SqlConnection(conStr))
//                    {
//                        if (con.State != System.Data.ConnectionState.Open)
//                        {
//                            con.Open();
//                        }
//                        SqlCommand cmd_2 = new SqlCommand(emailuser, con);
//                        using (SqlDataReader reader_2 = cmd_2.ExecuteReader())
//                        {
//                            if (reader_2.Read())
//                            {

//                                string contact = (string)reader_2["Correo"];
//                                NotificationHub conector = new NotificationHub();
//                                conector.Show(contact);
//                                ///notificationHub.Clients.All.notify(contact);
//                                RegisterNotification(DateTime.Now);
                           
//                            }
//                        }
//                    }

//                }

//        }



//        public List<Mensajes> GetContacts(DateTime afterDate)
//        {
//            using (BlackSysEntities dc = new BlackSysEntities())
//            {
//                return dc.Mensajes.Where(a => a.AddedOn > afterDate).OrderByDescending(a => a.AddedOn).ToList();
//            }
//        }
//    }
//}