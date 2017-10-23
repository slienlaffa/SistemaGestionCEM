using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaGestionCEM.Presentacion.WS_Alumno;



namespace SistemaGestionCEM.Presentacion.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            AlumnoClient aa = new AlumnoClient();
            List<PROGRAMA_ESTUDIO> programas = aa.ListarProgramasPublicados().ToList();
            String programa = programas.First().NOMBRE_PROGRAMA;
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