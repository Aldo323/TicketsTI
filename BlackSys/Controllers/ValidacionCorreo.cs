using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.Linq;
using System.Web;

namespace API_TEST.Controllers
{
    public class VailidacionCorreo
    {
        private ArrayList grupos = new ArrayList();
        private string _user;
        private ArrayList listaPropiedades = new ArrayList();
        string conStr = ConfigurationManager.ConnectionStrings["BlackSysConnection"].ConnectionString;

        public VailidacionCorreo()
        {
           
        }

        public Boolean GetUser(string user)
        {

            string sqlComm = @"SELECT EmailConfirmed  from [dbo].[AspNetUsers] where [Email] = @user";
            //string userCommand = @"SELECT [Email]  from [dbo].[AspNetUsers] where [Email] = @user";
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand(sqlComm, con);
                cmd.Parameters.AddWithValue("@user", user);


                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {

                        bool contact = (bool)reader["EmailConfirmed"];
                        if(contact == false)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }

                    }

                }
            }
             return true;
           
        }

        public string RevisarUser(string user)
        {

            string sqlComm = @"SELECT Email  from [dbo].[AspNetUsers] where [Email] = @user";
            //string userCommand = @"SELECT [Email]  from [dbo].[AspNetUsers] where [Email] = @user";
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand(sqlComm, con);
                cmd.Parameters.AddWithValue("@user", user);


                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {

                        string contact = (string)reader["Email"];
                        var r = "";
                        return "OK";

                    }

                }
            }

            return "other";

        }

    }
}