using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaGestionCEM.Negocio;
using SistemaGestionCEM.Datos;
using System.Web.Mvc;

namespace SistemaGestionCEM.Presentacion.Controllers
{
    public class AdminController : Controller
    {
        private Entities db = new Entities();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

    }
}