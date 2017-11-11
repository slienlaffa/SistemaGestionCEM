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
            if(ValidarSesion())
                return View();
            return RedirectToAction("DenegarAcceso");
        }

        public ActionResult AsignarsePrograma()
        {
            if (ValidarSesion())
            {
                return View(cel.PostulacionesPublicadas());
            }
            return RedirectToAction("DenegarAcceso");
            
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

        public bool ValidarSesion()
        {
            if (Session["SesionActual"] != null)
            {
                string session = Session["SesionActual"].ToString();
                if (session.Equals("EncargadoCEL"))
                    return true;
                else
                    return false;
            }
            return false;
        }

        public ActionResult DenegarAcceso()
        {
            return View();
        }
    }
}