using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaGestionCEM.Negocio;

namespace SistemaGestionCEM.Presentacion.Controllers
{
    public class EncargadoCELController : Controller
    {
        private EncargadoCELNegocio cel = new EncargadoCELNegocio();
        // GET: EncargadoCEL
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AsignarsePrograma()
        {
            return View(cel.PostulacionesPublicadas());
        }

        public ActionResult Asignarse(int id)
        {
            string usuario = Session["Nombre"].ToString();
            cel.PostularPrograma(id, usuario);
            return RedirectToAction("AsignarsePrograma");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                cel.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}