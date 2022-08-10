using CUNApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CUNApp.Controllers
{
    public class LoginController : Controller
    {

        private Helpers help = new Helpers();

        public ActionResult Login()
        
        {
            if (Session["User"] != null)
            {
                return RedirectToAction("Index", "Acceso");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (username == null || password == null)
            {
                ViewBag.Error = "Todos los campos son obligatorios";
                return View();
            }
            
           // password = help.GetSHA256(password);
           
                using (Models.CUNEntities1 db = new Models.CUNEntities1())
                {
                    var oUserAd = (from d in db.Administrador
                                 where d.Adm_Email.ToString().Equals(username.ToString().Trim()) && d.Adm_Password.ToString().Equals(password.ToString().Trim())
                                 select d).FirstOrDefault();
                    var oUserEs = (from e in db.Estudiante
                                 where e.Est_Email.ToString().Equals(username.ToString().Trim()) && e.Est_Password.Equals(password.ToString().Trim())
                                 select e).FirstOrDefault();
                    var oUserDo = (from a in db.Docente
                                 where a.Doc_Email.ToString().Equals(username.ToString().Trim()) && a.Doc_Password.ToString().Equals(password.ToString().Trim())
                                 select a).FirstOrDefault();

                    if (oUserAd == null||oUserDo==null||oUserEs==null)
                    {
                        ViewBag.Error = "Contraseña o usuario incorrectos.";
                        return View();
                    }
                    if (oUserAd != null)
                    {
                        Session["User"] = oUserAd;
                        Session["ID"] = oUserAd.Adm_ID;
                        Session["Rol"] = "Administrador";
                        Session["Nombres"] = oUserAd.Adm_Nombre + " " + oUserAd.Adm_Apellido;
                        Session["Correo"] = oUserAd.Adm_Email;
                    }
                    if (oUserDo != null)
                    {
                        Session["User"] = oUserDo;
                        Session["ID"] = oUserDo.Doc_ID;
                        Session["Rol"] = "Docente";
                        Session["Nombres"] = oUserDo.Doc_Nombre + " " + oUserDo.Doc_Apellido;
                        Session["Correo"] = oUserDo.Doc_Email;
                    }
                    if (oUserEs != null)
                    {
                        Session["User"] = oUserEs;
                        Session["ID"] = oUserEs.Est_ID;
                        Session["Rol"] = "Estudiante";
                        Session["Nombres"] = oUserEs.Est_Nombre + " " + oUserEs.Est_Apellido;
                        Session["Correo"] = oUserEs.Est_Email;
                    }
                    return RedirectToAction("Index", "Admin");
                }
          
        }
    }
}