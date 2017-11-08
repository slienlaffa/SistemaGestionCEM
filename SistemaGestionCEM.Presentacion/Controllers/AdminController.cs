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
        private AlumnoNegocio alumNegocio = new AlumnoNegocio();
        private FamiliaAnfitrionaNegocio familiaNegocio = new FamiliaAnfitrionaNegocio();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        #region AdministracionAlumnos
        // Admin de Usuarios
        public ActionResult CrearAlumno()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CrearAlumno(ALUMNO nuevoAlumno)
        {
            return View();
        }
        public ActionResult ModificarAlumno(int id)
        {
            
            return View();
        }

        public ActionResult EliminarAlumno(int id)
        {
            alumNegocio.Eliminar(id);
            return View();
        }

        public ActionResult ListarAlumnos()
        {
            return View(alumNegocio.Mostrar());
        }

        #endregion

        #region AdministracionFamilia
        public ActionResult CrearFamilia()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CrearFamilia(FAMILIA_ANFITRIONA nuevaFamilia)
        {
            return View();
        }
        public ActionResult ModificarFamilia()
        {
            return View();
        }

        public ActionResult ListarFamilias()
        {
            return View();
        }
        #endregion

        public ActionResult CrearCEL()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CrearCEL(CENTRO_ESTUDIO_LOCAL cel)
        {
            return View();
        }

        public ActionResult ModificarCEL()
        {
            return View();
        }



        // Programas
        public ActionResult CrearPrograma()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CrearPrograma(PROGRAMA_ESTUDIO nuevoPrograma)
        {
            return View();
        }

        public ActionResult PublicarPrograma()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PublicarPrograma(PROGRAMA_ESTUDIO programa)
        {
            return View();
        }


    }
}