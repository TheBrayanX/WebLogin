using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using WebLogin.Models;
using System.Data.SqlClient;
using System.Data;
using System.Web.Services.Description;
using System.Configuration;

namespace WebLogin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CerrarSesion()
        {
            Session["NameUser"] = null;
            return RedirectToAction("Login", "Home");
        }


        [HttpPost]
        public ActionResult Register(User User)
        {
            try
            {
                string conectar = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
                string consulta = string.Format("insert into Usuario(NameUser,clave) values ('{0}','{1}')", User.NameUser, User.clave);

                using (SqlConnection sqlConectar = new SqlConnection(conectar))
                {
                    SqlCommand cmd = new SqlCommand(consulta, sqlConectar);
                    cmd.Connection.Open();
                    int cantidadFilasAfectadas = cmd.ExecuteNonQuery();
                    cmd.Connection.Close();

                    TempData["MSG"] = string.Format("La persona se agregó al sistema: {0} {1}", User.NameUser, User.clave);
                    return View("Login");
                }
            }
            catch (Exception e)
            {
                TempData["NoAgregado"] = "No se agrego con exito el dato: " + string.Format("{0},{1} - Error {2}", User.NameUser, User.clave, e.Message);
                return View("Register");
            }
        }
           
        
        [HttpPost]
        public ActionResult Login(User User)
        {   
            User.clave = User.clave;

            string conectar = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
            using (SqlConnection sqlConectar = new SqlConnection(conectar))
            {
                SqlCommand cmd = new SqlCommand("spValidarUsuario", sqlConectar);
                cmd.Parameters.AddWithValue("NameUser", User.NameUser);
                cmd.Parameters.AddWithValue("Clave", User.clave);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Connection.Open();
                User.idUser = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            }

            if (User.idUser != 0)
            {
                Session["NameUser"] = User;
                return RedirectToAction("Index","Home");
            } else
            {
                return View("Login");
            }
        }
    }
}