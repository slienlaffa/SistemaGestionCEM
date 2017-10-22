using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaGestionCEM.Presentacion.WS_Autentificacion;


namespace SistemaGestionCEM.Presentacion.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            AutentificacionClient web_service = new AutentificacionClient();
            @ViewBag.Message = web_service.Login("gato", "perro");
            WS_Alumno.AlumnoClient aa = new WS_Alumno.AlumnoClient();
            aa.ListarProgramasPublicados();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}