﻿using System.Collections.Generic;
using System.Linq;
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
            if (ValidarSesion())
            {
                string usuario = Session["Nombre"].ToString();
                return View(cel.PostulacionProgramasPorCEL(usuario));
            }
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
            TempData["id"] = id;
            return View(cel.BuscarNotas(id).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarNotas([Bind(Include = "COD_NOTAS,FK_COD_DETALUMNO,NOTA1,NOTA2,NOTA3,NOTA4")] List<DETALLE_NOTAS> notas)
        {
            int id = (int)TempData["id"];
            if (ModelState.IsValid)
            {
                if (cel.RegistrarNotas(notas))
                    TempData["success"] = "Notas modificadas correctamente.";
                else
                    TempData["error"] = "Error al modificar las notas";
                return RedirectToAction("EditarNotas");
            }
            else
                TempData["id"] = id;
            return View(cel.BuscarNotas(id).ToList());
        }

        public ActionResult Asignarse(int id)
        {
            string usuario = Session["Nombre"].ToString();
            if (cel.PostularPrograma(id, usuario))
                TempData["success"] = "Se ha asignado correctamente.";
            else
                TempData["error"] = "Ha ocurrido un error.";
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