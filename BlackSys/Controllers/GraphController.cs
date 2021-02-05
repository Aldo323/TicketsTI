using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlackSys.Models;
using Microsoft.AspNet.Identity;

namespace BlackSys.Controllers
{
    [Authorize]
    public class GraphController : ApiController
    {
        public IHttpActionResult Get()
        {
            //string usuario = User.Identity.GetUserName().ToString();
            //NotificationHub NC = new NotificationHub();
            //var currentTime = DateTime.Now;
            //NC.RegisterNotification(currentTime, usuario);
            using (BlackSysEntities dc = new BlackSysEntities())
            {


                dc.Configuration.LazyLoadingEnabled = false;
                int abierto = dc.Solicitudes.Where(p => p.Estatus == "Abierto").Count();
                int cerrado = dc.Solicitudes.Where(p => p.Estatus == "Cerrado").Count();
                int nc = dc.Solicitudes.Where(p => p.Estatus == "N.C").Count();
                int vencidos = dc.Solicitudes.Where(p => p.Vencido == 1 && p.Estatus == "ABIERTO").Count();

                //using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BlackSysConnection"].ConnectionString))
                //{
                //    connection.Open();
                //    using (SqlCommand command = new SqlCommand(@"SELECT  [IdSolicitud],[Ubicacion] ,[Correo],[Departamento],[Telefono],[Tipo_solicitud],[Descripcion] ,[Fecha_levantamiento] ,[Estatus],[Conclusion],[SLA_C_A],[SLA_C_M] ,[SLA_C_B] ,[Responsable],[Fecha_cierre],[Activo],[Subactivo],[Categoria],[Fecha_Vencimiento]", connection))
                //    {
                //        // Make sure the command object does not already have
                //        // a notification object associated with it.
                //        command.Notification = null;

                //        SqlDependency dependency = new SqlDependency(command);
                //        dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                //        if (connection.State == ConnectionState.Closed)
                //            connection.Open();

                //        SqlDataReader reader = command.ExecuteReader();

                //    }
                //}
                return Json(new { abierto, cerrado, nc, vencidos });

            }

            //void dependency_OnChange(object sender, SqlNotificationEventArgs e)
            //{
            //    NotificationHub.Tabla();
            //}

        }
    }
}
