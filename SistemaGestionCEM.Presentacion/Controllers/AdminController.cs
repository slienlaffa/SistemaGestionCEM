using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaGestionCEM.Datos;
using SistemaGestionCEM.Negocio;
using System.Threading;

namespace SistemaGestionCEM.Presentacion.Controllers
{
    public class AdminController : Controller
    {
        private AlumnoNegocio alumNegocio = new AlumnoNegocio();
        private FamiliaAnfitrionaNegocio familiaNegocio = new FamiliaAnfitrionaNegocio();
        private Entities db = new Entities();

        // GET: Admin
        public ActionResult Index()
        {
            if (ValidarSesionAdministrador() == true)
            {
                var usuarios = db.USUARIO.Include(p => p.PERSONA);

                
                return View(usuarios.ToList());
            }
            else
            {
                return RedirectToAction("DenegarAcceso");             
            }
        }

        public ActionResult DenegarAcceso()
        {
            return View();
        }

        // Sirve para seleccionar que tipo de usuario quiere crear.
        // Alumno, Familia, EncargadoCEL o EncargadoCEM
        
        public ActionResult SeleccionarUsuario()
        {
            if (ValidarSesionAdministrador())
            {
                  return View();
            }
            return RedirectToAction("DenegarAcceso");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SeleccionarUsuario(TIPO_USUARIO tipo)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                if (tipo.COD_TIPO == 2)
                {
                    return RedirectToAction("IngresarAlumno", "Admin");
                }
                if (tipo.COD_TIPO == 3)
                {
                    return RedirectToAction("IngresarFamilia", "Admin");
                }
                if (tipo.COD_TIPO == 4)
                {
                    return RedirectToAction("IngresarEncargadoCEM", "Admin");
                }
                if (tipo.COD_TIPO == 5)
                {
                    return RedirectToAction("IngresarEncargadoCEL", "Admin");
                }
                return View();
            }
        }

        public ActionResult IngresarAlumno()
        {
            ViewBag.FK_COD_CIUDAD = new SelectList(db.CIUDAD, "COD_CIUDAD", "DESCRIPCION");
            ViewBag.FK_COD_GENERO = new SelectList(db.GENERO, "COD_GENERO", "DESCRIPCION");
            ViewBag.FK_COD_USUARIO = new SelectList(db.USUARIO, "COD_USUARIO", "NOMBRE_USUARIO");
            return View();
        }

        // GET: Admin/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERSONA pERSONA = db.PERSONA.Find(id);
            if (pERSONA == null)
            {
                return HttpNotFound();
            }
            return View(pERSONA);
        }

       // Alumnos.
        public ActionResult Create()
        {
            ViewBag.FK_COD_CIUDAD = new SelectList(db.CIUDAD, "COD_CIUDAD", "DESCRIPCION");
            ViewBag.FK_COD_GENERO = new SelectList(db.GENERO, "COD_GENERO", "DESCRIPCION");
            ViewBag.FK_COD_USUARIO = new SelectList(db.USUARIO, "COD_USUARIO", "NOMBRE_USUARIO");
            return View();
        }

        // POST: Admin/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "COD_PERSONA,NOMBRE,APELLIDO,CORREO,TELEFONO,NACIONALIDAD,FK_COD_CIUDAD,FK_COD_GENERO,FK_COD_USUARIO")] PERSONA pERSONA)
        {
            if (ModelState.IsValid)
            {
                db.PERSONA.Add(pERSONA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_COD_CIUDAD = new SelectList(db.CIUDAD, "COD_CIUDAD", "DESCRIPCION", pERSONA.FK_COD_CIUDAD);
            ViewBag.FK_COD_GENERO = new SelectList(db.GENERO, "COD_GENERO", "DESCRIPCION", pERSONA.FK_COD_GENERO);
            ViewBag.FK_COD_USUARIO = new SelectList(db.USUARIO, "COD_USUARIO", "NOMBRE_USUARIO", pERSONA.FK_COD_USUARIO);
            return View(pERSONA);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERSONA pERSONA = db.PERSONA.Find(id);
            if (pERSONA == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_COD_CIUDAD = new SelectList(db.CIUDAD, "COD_CIUDAD", "DESCRIPCION", pERSONA.FK_COD_CIUDAD);
            ViewBag.FK_COD_GENERO = new SelectList(db.GENERO, "COD_GENERO", "DESCRIPCION", pERSONA.FK_COD_GENERO);
            ViewBag.FK_COD_USUARIO = new SelectList(db.USUARIO, "COD_USUARIO", "NOMBRE_USUARIO", pERSONA.FK_COD_USUARIO);
            return View(pERSONA);
        }

        // POST: Admin/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "COD_PERSONA,NOMBRE,APELLIDO,CORREO,TELEFONO,NACIONALIDAD,FK_COD_CIUDAD,FK_COD_GENERO,FK_COD_USUARIO")] PERSONA pERSONA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pERSONA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_COD_CIUDAD = new SelectList(db.CIUDAD, "COD_CIUDAD", "DESCRIPCION", pERSONA.FK_COD_CIUDAD);
            ViewBag.FK_COD_GENERO = new SelectList(db.GENERO, "COD_GENERO", "DESCRIPCION", pERSONA.FK_COD_GENERO);
            ViewBag.FK_COD_USUARIO = new SelectList(db.USUARIO, "COD_USUARIO", "NOMBRE_USUARIO", pERSONA.FK_COD_USUARIO);
            return View(pERSONA);
        }

        // GET: Admin/Delete/5
        public ActionResult EliminarUsuario(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERSONA pERSONA = db.PERSONA.Find(id);
            if (pERSONA == null)
            {
                return HttpNotFound();
            }
            return View(pERSONA);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            PERSONA pERSONA = db.PERSONA.Find(id);
            db.PERSONA.Remove(pERSONA);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // Sirve para validar que el administrador esté logueado.
        public bool ValidarSesionAdministrador()
        {
            if (Session["SesionActual"] != null) {
                string session = Session["SesionActual"].ToString();
                if (session.Equals("Administrador"))
                    return true;
                else
                    return false;
            }
            return false;
        }
    }
}
