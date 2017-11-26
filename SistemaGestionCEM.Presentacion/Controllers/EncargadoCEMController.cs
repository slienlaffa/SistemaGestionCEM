﻿using SistemaGestionCEM.Datos;
using SistemaGestionCEM.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaGestionCEM.Presentacion.Controllers
{
    public class EncargadoCEMController : Controller
    {
        private Entities db = new Entities();
        private EncargadoCEMNegocio cem = new EncargadoCEMNegocio();

        // GET: EncargadoCEM
        public ActionResult Index()
        {
            if (ValidarSesion()) 
                return View();
            
            return RedirectToAction("DenegarAcceso");
        }

        public ActionResult CrearPrograma()
        {
            ViewBag.FK_COD_ENCARGADOCEM = new SelectList(db.ENCARGADO_CEM, "COD_ENCARGADOCEM", "COD_ENCARGADOCEM");
            ViewBag.FK_COD_PAIS = new SelectList(db.PAIS, "COD_PAIS", "DESCRIPCION");
            ViewBag.FK_COD_TIPOPROGRAMA = new SelectList(db.TIPO_PROGRAMA, "COD_TIPOPROGRAMA", "DESCRIPCION");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearPrograma([Bind(Include = "NOMBRE_PROGRAMA,DESCRIPCION,FECHA_LIMITE_POSTULACION,AREA_CONOCIMIENTO,DURACION,NUM_MAX_ALUMNO,NUM_MIN_ALUMNO,FK_COD_TIPOPROGRAMA,FK_COD_PAIS")] PROGRAMA_ESTUDIO programaEstudio)
        {           
            if (ModelState.IsValid)
            {
                string usuario = Session["Nombre"].ToString();
                cem.CrearProgramaEstudio(programaEstudio, usuario);
                return RedirectToAction("Index");
            }

            ViewBag.FK_COD_ENCARGADOCEM = new SelectList(db.ENCARGADO_CEM, "COD_ENCARGADOCEM", "COD_ENCARGADOCEM", programaEstudio.FK_COD_ENCARGADOCEM);
            ViewBag.FK_COD_PAIS = new SelectList(db.PAIS, "COD_PAIS", "DESCRIPCION", programaEstudio.FK_COD_PAIS);
            ViewBag.FK_COD_TIPOPROGRAMA = new SelectList(db.TIPO_PROGRAMA, "COD_TIPOPROGRAMA", "DESCRIPCION", programaEstudio.FK_COD_TIPOPROGRAMA);
            return View(programaEstudio);
        }

        public ActionResult PublicarPrograma()
        {
            return View(cem.PostulacionesNoPublicadas());
        }

        public ActionResult Publicar(int id)
        {
            cem.PublicarPrograma(id);
            return RedirectToAction("PublicarPrograma");
        }

        public ActionResult Cancelar(int id)
        {
            cem.CancelarPrograma(id);
            return RedirectToAction("PublicarPrograma");
        }

        public ActionResult PostulacionesPendientes()
        {
            return View(cem.PostulacionesPendientes());
        }

        public ActionResult MostrarEstado(int id)
        {
            var estado = cem.obtenerEstadoAlumno(id);
            if(estado == null)
            {
                TempData["error"] = "No se puede encontrar el alumno en el sistema del CEM.";
                return RedirectToAction("PostulacionesPendientes");
            }
            return View(estado);
        }

        public ActionResult PostulacionAprobar(int id)
        {
            if (cem.SeleccionarPostulante(id, true))
                TempData["success"] = "Se ha aprobado correctamente al alumno.";
            else
                TempData["error"] = "Ha habido un error.";
            return RedirectToAction("PostulacionesPendientes");
        }

        public ActionResult PostulacionRechazar(int id)
        {
            if(cem.SeleccionarPostulante(id, false))
                TempData["success"] = "Se ha rechazado correctamente al alumno.";
            else
                TempData["error"] = "Ha habido un error.";
            return RedirectToAction("PostulacionesPendientes");
        }

        public ActionResult GenerarResumenProgramas()
        {
            ViewData["resumen"] = cem.GenerarResumenPorEstado();
            return View(db.POSTULACION_PROGRAMA.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
                cem.Dispose();
            }
            base.Dispose(disposing);
        }

        public bool ValidarSesion()
        {
            if (Session["SesionActual"] != null)
            {
                string session = Session["SesionActual"].ToString();
                if (session.Equals("EncargadoCEM"))
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