﻿using System;
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
        FamiliaAnfitrionaNegocio familiaNegocio = new FamiliaAnfitrionaNegocio();
        Entities db = new Entities();

        public ActionResult VerProgramas()
        {
            return View(alumno.ProgramasPublicados());
        }

        public ActionResult FamiliasDisponibles()
        {
            return View(alumno.FamiliasDisponibles());
        }

        public ActionResult VerDetalleFamilia(decimal? id)
        {
            if (id == null || id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FAMILIA_ANFITRIONA familia = db.FAMILIA_ANFITRIONA.Find(id);
            if (familia == null)
            {
                return HttpNotFound();
            }
            return View(familia);
        }

        //Quede aqui, no he podido probar, debo correr el webservice.
        public ActionResult SeleccionarFamiliaAnfitriona(decimal id)
        {
            if (ValidarSesion())
            {
                var nombreUsuario = Session["Nombre"].ToString();
                var persona = db.PERSONA.Where(a => a.USUARIO.NOMBRE_USUARIO == nombreUsuario).FirstOrDefault();

                if (persona != null)
                {
                    var obtenerAlumno = db.ALUMNO.Where(alum => alum.PERSONA.COD_PERSONA == persona.COD_PERSONA).FirstOrDefault();

                    var result = alumno.SeleccionarFamilia((int)obtenerAlumno.COD_ALUMNO, (int)id);
                    if (!result)
                    {
                        TempData["error"] = "Lo sentimos, tu postulacion a un programa pendiente aun no ha sido aprobada, o no has postulado.";
                    }
                    else {
                        TempData["success"] = "Familia seleccionada con exito!";
                        
                    }
                    return View("FamiliasDisponibles");
                }
                return View();
            }
            else
            {
                return RedirectToAction("Denegaracceso");
            }
        }

        // funcionan3
        public ActionResult ConsultarPostulacion()
        {
            if (ValidarSesion())
            {
                var nombreUsuario = Session["Nombre"].ToString();
                var persona = db.PERSONA.Where(a => a.USUARIO.NOMBRE_USUARIO == nombreUsuario).FirstOrDefault();

                if (persona != null)
                {
                    var obtenerAlumno = db.ALUMNO.Where(alum => alum.PERSONA.COD_PERSONA == persona.COD_PERSONA).FirstOrDefault();
                    POSTULACION_ALUMNO estado = db.POSTULACION_ALUMNO.Where(e => e.ALUMNO.COD_ALUMNO == obtenerAlumno.COD_ALUMNO).FirstOrDefault();
                    if (estado != null)
                    {
                        return View(estado);
                    }
                    TempData["error"] = "Usted no posee postulaciones actualmente.";
                    return View("Index");
                }
                return View();
            }
            return RedirectToAction("DenegarAcceso");
        }

        // No funcionando
        public ActionResult GenerarCertificadoAprobacion()
        {
            if (ValidarSesion())
            {
                var nombreUsuario = Session["Nombre"].ToString();
                var persona = db.PERSONA.Where(a => a.USUARIO.NOMBRE_USUARIO == nombreUsuario).FirstOrDefault();
                if (persona != null)
                {
                    var obtenerAlumno = db.ALUMNO.Where(alum => alum.PERSONA.COD_PERSONA == persona.COD_PERSONA).FirstOrDefault();
                    DETALLE_ALUMNO detalle = db.DETALLE_ALUMNO.Find(obtenerAlumno.COD_ALUMNO);
                    if (alumno.CursoAprobado((int)obtenerAlumno.COD_ALUMNO))
                    {
                        return View(detalle);
                    }
                    TempData["error"] = "Usted no ha aprobado ningun curso";
                    return View("Index");
                }
            }
            return View();
        }

        public ActionResult VerAntecedentesFamilia(decimal? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
          FAMILIA_ANFITRIONA familia = db.FAMILIA_ANFITRIONA.Find(id);
            if (familia == null)
            {
                return HttpNotFound();
            }
            return View(familia);
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