using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaGestionCEM.Negocio;
using SistemaGestionCEM.Datos;

namespace SistemaGestionCEM.Presentacion.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            AlumnoNegocio aa = new AlumnoNegocio();
            List<PROGRAMA_ESTUDIO> programas = aa.ProgramasPublicados().ToList();
            ViewBag.Message = programas.First().NOMBRE_PROGRAMA;
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