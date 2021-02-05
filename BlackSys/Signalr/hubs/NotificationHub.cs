using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using BlackSys.Controllers;
using BlackSys.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.SignalR;


namespace BlackSys
{

    public class NotificationHub : Hub
    {

        private readonly static ConnectionMapping<string> _connections = new ConnectionMapping<string>();
        private BlackSysEntities db = new BlackSysEntities();

        //public NotificationHub()
        //{
        //    ListUser = new List<UserConnection>();
        //}

        public static List<UserConnection> ListUser { get; set; }
       
        public override Task OnConnected()
        {
            string name = Context.User.Identity.Name.ToString();
            // var us = new UserConnection();
            // string name = Context.User.Identity.Name;
            //us.UserId = Context.ConnectionId;
            //us.Nombre_user = Context.User.Identity.Name;
            //Clients.All.SendAsync("UserConnected", Context.ConnectionId);
            //us.Nombre_user = Context.QueryString["Nombr"];
            _connections.Add(name, Context.ConnectionId);
            //ListUser.Add(us);

            return base.OnConnected();
        }

       


        public void Show(string useer)
        {
            var notificationHub = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();


            foreach (var connectionId in _connections.GetConnections(useer))
            {
                notificationHub.Clients.Client(connectionId).notify("Monitoreos");
            }



            //var notificationHub = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            //try
            //{
            //    string Id = ListUser.Find(x => x.Nombre_user == useer).UserId;
            //    notificationHub.Clients.Client(Id).notify(useer);
            //}
            //catch
            //{
            //    notificationHub.Clients.All.notify(useer);
            //}


        }

        public void ShowTable(string useer)
        {
            var notificationHub = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();


                notificationHub.Clients.All.notify(useer);
            

        }
        public string UsuarioConn()
        {
            string name = Context.User.Identity.Name;


            return name;


        }

        public static void Tabla()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            context.Clients.All.displayCustomer();

        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string name = Context.User.Identity.Name;

            _connections.Remove(name, Context.ConnectionId);

            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            string name = Context.User.Identity.Name;

            if (!_connections.GetConnections(name).Contains(Context.ConnectionId))
            {
                _connections.Add(name, Context.ConnectionId);
            }

            return base.OnReconnected();
        }

        public List<Mensajes> GetContacts(DateTime afterDate)
        {
            using (BlackSysEntities dc = new BlackSysEntities())
            {
                return dc.Mensajes.Where(a => a.AddedOn > afterDate).OrderByDescending(a => a.AddedOn).ToList();
            }
        }
        //    public static List<IdentityUser> ListUser { get; set; }

        //    public static void Show(string user)
        //    {

        //        IHubContext context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
        //        //string Id = ListUser.Find(x => x.Email == user).Email;
        //        //context.Clients.Client(Id).addNotification(who); // or another data
        //        //                                                 // string t = ShowUser().Result;

        //        context.Clients.All.notify(user);
        //    }

        //        //public static  async Task<string> ShowUser()
        //        //{
        //        //    var httpClient = new HttpClient();
        //        //    var json = await httpClient.GetStringAsync("http://localhost:29586/api/user");
        //        //    string l = json.ToString();
        //        //    return l;
        //        //}

        //        //public IdentityUser EntityFramework
        //        //{
        //        //    get
        //        //    {
        //        //        return (Context.Request.Environment["server.User"] as GenericPrincipal).Identity as IdentityUser;
        //        //    }
        //        //}


        //    }
    }

}