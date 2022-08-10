using CUNApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using CUNApp.Models;

namespace CUNApp.Controllers
{
    public class AdminController : Controller
    {
        /*Logistica de la tabla*/
        public string draw = "";
        public string start = "";
        public string length = "";
        public string sortColumn = "";
        public string sortColumnDir = "";
        public string searchValue = "";
        public int pageSize, skip, recordsTotal;
        public DateTime fecha = DateTime.Now;
        private readonly Helpers help = new Helpers();
        public ActionResult Index()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (Session["Rol"].Equals("Estudiante"))
            {
                return RedirectToAction("Index", "Estudiante");
            }
            if (Session["Rol"].Equals("Docente"))
            {
                return RedirectToAction("Index", "Docente");
            }

            return View();
        }
        public ActionResult Estudiantes()
        {
         /*   if (Session["User"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (Session["Rol"].Equals("Estudiante"))
            {
                return RedirectToAction("Index", "Estudiante");
            }
            if (Session["Rol"].Equals("Docente"))
            {
                return RedirectToAction("Index", "Docente");
            }*/

            return View();
        }
        [HttpPost]
        public ActionResult JsonEstudiantes()
        {
            List<EstudiantesVM> lst = new List<EstudiantesVM>();
            //logistica datatable
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
            pageSize = length != null ? Convert.ToInt32(length) : 0;
            skip = start != null ? Convert.ToInt32(start) : 0;
            recordsTotal = 0;
            //Conexion con la base de datos
            using (Models.CUNEntities1 db = new Models.CUNEntities1())
            {
                IQueryable<EstudiantesVM> query = (from cli in db.Estudiante
                                               
                                                select new EstudiantesVM
                                                {
                                                    Est_ID = cli.Est_ID,
                                                    Est_Nombre = cli.Est_Nombre,
                                                    Est_Apellido = cli.Est_Apellido,
                                                    Est_Email = cli.Est_Email
                                                });
                if (searchValue != "")
                    query = query.Where(Est => Est.Est_Email.Contains(searchValue));
                //Sorting    
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    query = query.OrderBy(sortColumn + " " + sortColumnDir);
                }
                if (!sortColumn.Equals("Acciones"))
                {
                    recordsTotal = query.Count();
                }
                else
                {
                    ViewBag.Error = "No se puede ordenar por este tipo de elemento";
                }
                if (recordsTotal != 0)
                {
                    lst = query.Skip(skip).Take(pageSize).ToList();
                }
                foreach (EstudiantesVM buscador in lst)
                {

                    buscador.Acciones = "<button class=\"btn btn-primary btn-sm btnEditEst\" onclick=\"fntEditEst(" + buscador.Est_ID + ")\" title=\"Editar\"><i class=\"fas fa-pencil-alt\" aria-hidden=\"true\"></i></button> ";
                    buscador.Acciones += "<button class=\"btn btn-danger btn-sm btnElimEst\" onclick=\"fntDelEst(" + buscador.Est_ID + ")\" title=\"Eliminar\"><i class=\"far fa-trash-alt\" aria-hidden=\"true\"></i></button> ";
                }

            }
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = lst });
        }

        [HttpPost]
        public ActionResult SelEst(int id)
        {
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                using (Models.CUNEntities1 db = new Models.CUNEntities1())
                {

                    var oValidAl = (from d in db.Estudiante
                                    where d.Est_ID == id
                                    select d).FirstOrDefault();
                    if (oValidAl == null)
                    {
                        return Json(new { Success = false, msg = "Error al precargar datos." });
                    }

                    return Json(new
                    {
                        Success = true,
                        idpersona = oValidAl.Est_ID,
                        nombres = oValidAl.Est_Nombre,
                        apellidos = oValidAl.Est_Apellido,
                        email_user = oValidAl.Est_Email
                    });
                }
            }
            else
            {
                return Json(new { Success = false, msg = "No posee los permisos suficientes para dicha acción" });
            }
        }
        public ActionResult Docentes()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (Session["Rol"].Equals("Estudiante"))
            {
                return RedirectToAction("Index", "Estudiante");
            }
            if (Session["Rol"].Equals("Docente"))
            {
                return RedirectToAction("Index", "Docente");
            }

            return View();
        }
        public ActionResult Materias()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (Session["Rol"].Equals("Estudiante"))
            {
                return RedirectToAction("Index", "Estudiante");
            }
            if (Session["Rol"].Equals("Docente"))
            {
                return RedirectToAction("Index", "Docente");
            }

            return View();
        }
        public ActionResult Cursos()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (Session["Rol"].Equals("Estudiante"))
            {
                return RedirectToAction("Index", "Estudiante");
            }
            if (Session["Rol"].Equals("Docente"))
            {
                return RedirectToAction("Index", "Docente");
            }

            return View();
        }
    }
}