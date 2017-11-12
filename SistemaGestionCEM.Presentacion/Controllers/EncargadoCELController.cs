using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaGestionCEM.Negocio;
using SistemaGestionCEM.Datos;

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

        public ActionResult RegistrarNotas()
        {
            string usuario = Session["Nombre"].ToString();
            return View(cel.ProgramasEnCursoPorCEL(usuario));
        }

        public ActionResult EditarNotas(int id)
        {
            return View(cel.BuscarNotas(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarNotas([Bind(Include = "Id,NOTA1,NOTA2,NOTA3,NOTA4")] List<DETALLE_NOTAS> notas)
        {
            if (ModelState.IsValid)
            {
                if (cel.RegistrarNotas(notas))
                    TempData["success"] = "Notas modificadas correctamente.";
                else
                    TempData["error"] = "Error al modificar las notas";
                return RedirectToAction("EditarNotas");
            }
            return View(notas);
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