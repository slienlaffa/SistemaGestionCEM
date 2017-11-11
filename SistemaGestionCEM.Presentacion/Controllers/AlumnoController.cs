using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaGestionCEM.Presentacion.Controllers
{
    public class AlumnoController : Controller
    {
        

        public ActionResult Index()
        {
            //No se si tiene que cargar alguna lista en Alumno...
            return ComprobarSesion();
        }

        public bool ValidarSesion()
        {
            if (Session["SesionActual"] != null)
            {
                string session = Session["SesionActual"].ToString();
                if (session.Equals("Alumno"))
                    return true;
                else
                    return false;
            }
            return false;
        }

        public ActionResult ComprobarSesion()
        {
            if (ValidarSesion())
                return View();
            else
                return RedirectToAction("DenegarAcceso");
        }

        public ActionResult DenegarAcceso()
        {
            return View();
        }
    }
}