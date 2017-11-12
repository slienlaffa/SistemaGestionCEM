using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaGestionCEM.Negocio;
using System.Net;
using SistemaGestionCEM.Datos;

namespace SistemaGestionCEM.Presentacion.Controllers
{
    public class AlumnoController : Controller
    {
        AlumnoNegocio alumno = new AlumnoNegocio();
        Entities db = new Entities();

        public ActionResult VerProgramas()
        {
            return View(alumno.ProgramasPublicados());
        }

        public ActionResult DetallesPrograma(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POSTULACION_PROGRAMA postulacion = db.POSTULACION_PROGRAMA.Find(id);
            if (postulacion == null)
            {
                return HttpNotFound();
            }
            return View(postulacion);
        }

        public ActionResult Postular(int codPrograma)
        {
            string usuario = Session["Nombre"].ToString();
            var resultado = alumno.PostularPrograma(codPrograma, usuario);
            if (resultado != null)
            {
                TempData["success"] = "Postulacion realizada correctamente.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Ya cuenta con una postulacion pendiente o un programa en curso.";
                return RedirectToAction("VerProgramas");
            }
        }

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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                alumno.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}