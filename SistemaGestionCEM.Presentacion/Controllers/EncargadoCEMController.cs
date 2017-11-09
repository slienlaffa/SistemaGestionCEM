using SistemaGestionCEM.Datos;
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

        // GET: EncargadoCEM
        public ActionResult Index()
        {
            return View();
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
                EncargadoCEMNegocio cem = new EncargadoCEMNegocio();
                cem.CrearProgramaEstudio(programaEstudio, usuario);
                return RedirectToAction("Index");
            }

            ViewBag.FK_COD_ENCARGADOCEM = new SelectList(db.ENCARGADO_CEM, "COD_ENCARGADOCEM", "COD_ENCARGADOCEM", programaEstudio.FK_COD_ENCARGADOCEM);
            ViewBag.FK_COD_PAIS = new SelectList(db.PAIS, "COD_PAIS", "DESCRIPCION", programaEstudio.FK_COD_PAIS);
            ViewBag.FK_COD_TIPOPROGRAMA = new SelectList(db.TIPO_PROGRAMA, "COD_TIPOPROGRAMA", "DESCRIPCION", programaEstudio.FK_COD_TIPOPROGRAMA);
            return View(programaEstudio);
        }
    }
}