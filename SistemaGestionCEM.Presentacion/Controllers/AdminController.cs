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
            if (ValidarSesionAdministrador())
            {
                var usuarios = db.USUARIO.Include(p => p.PERSONA);
                return View(usuarios.ToList());
            }
           return RedirectToAction("DenegarAcceso");
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
                return View();
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

        public ActionResult IngresarFamilia()
        {
            if (ValidarSesionAdministrador())
            {
                CargarDropDownList();
                return View();
            }
            return RedirectToAction("DenegarAcceso");
        }

        [HttpPost]
        //Funcionando!
        public ActionResult IngresarFamilia(FAMILIA_ANFITRIONA nuevaFamilia)
        {
            CargarDropDownList();

            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                var persona = db.PERSONA
                    .Where(model => model.USUARIO.NOMBRE_USUARIO
                    == nuevaFamilia.PERSONA.USUARIO.NOMBRE_USUARIO)
                    .FirstOrDefault();

                if (persona != null)
                {
                    ViewBag.Message = "El nombre de usuario '" + nuevaFamilia.PERSONA.USUARIO.NOMBRE_USUARIO +
                        "' ya existe, por favor ingrese otro distinto!";
                    return View();
                }

                PersonaNegocio pn = new PersonaNegocio();
                PERSONA nuevaPersona = db.PERSONA.Create();

                nuevaPersona.COD_PERSONA = pn.nuevoCodigo();
                nuevaPersona.NOMBRE = nuevaFamilia.PERSONA.NOMBRE;
                nuevaPersona.APELLIDO = nuevaFamilia.PERSONA.APELLIDO;
                nuevaPersona.CORREO = nuevaFamilia.PERSONA.CORREO;
                nuevaPersona.TELEFONO = nuevaFamilia.PERSONA.TELEFONO;
                nuevaPersona.NACIONALIDAD = nuevaFamilia.PERSONA.NACIONALIDAD;
                nuevaPersona.FK_COD_GENERO = nuevaFamilia.PERSONA.FK_COD_GENERO;
                nuevaPersona.FK_COD_CIUDAD = nuevaFamilia.PERSONA.FK_COD_CIUDAD;

                UsuarioNegocio unegocio = new UsuarioNegocio();
                USUARIO usuario = db.USUARIO.Create();
                usuario.COD_USUARIO = unegocio.nuevoCodigo();
                usuario.NOMBRE_USUARIO = nuevaFamilia.PERSONA.USUARIO.NOMBRE_USUARIO;
                usuario.CONTRASENNA = nuevaFamilia.PERSONA.USUARIO.CONTRASENNA;

                usuario.FK_COD_TIPO = 3;
                nuevaPersona.FK_COD_USUARIO = usuario.COD_USUARIO;


                db.PERSONA.Add(nuevaPersona);
                db.USUARIO.Add(usuario);
                db.SaveChanges();

                FamiliaAnfitrionaNegocio anegocio = new FamiliaAnfitrionaNegocio();
                anegocio.Crear((int)nuevaFamilia.NUM_BANOS, 0, DateTime.Now.Year, (int)nuevaFamilia.NUM_HABITACIONES, nuevaFamilia.TIPO_VIVIENDA, (int)nuevaFamilia.NUM_INTEGRANTES,
                    (int)nuevaPersona.COD_PERSONA, nuevaFamilia.ESTACIONAMIENTO, nuevaFamilia.MASCOTA_DESCRIPCION);

                ViewBag.RegistroExitoso = "La nueva Familia Anfitriona se ha ingresado exitosamente";
                return View();
            }
        }

        public ActionResult IngresarAlumno()
        {
            if (ValidarSesionAdministrador())
            {
                CargarDropDownList();
                return View();
            }
            return RedirectToAction("DenegarAcceso");
        }

        [HttpPost]
        public ActionResult IngresarAlumno(ALUMNO nuevoAlumno)
        {
            CargarDropDownList();
            if (!ModelState.IsValid)
                return View();

            var persona = db.PERSONA
               .Where(model => model.USUARIO.NOMBRE_USUARIO
               == nuevoAlumno.PERSONA.USUARIO.NOMBRE_USUARIO)
               .FirstOrDefault();

            if (persona != null)
            {
                ViewBag.Message = "El nombre de usuario '" + nuevoAlumno.PERSONA.USUARIO.NOMBRE_USUARIO +
                    "' ya existe, por favor ingrese otro distinto!";
                return View();
            }

            PersonaNegocio pn = new PersonaNegocio();
            PERSONA nuevaPersona = db.PERSONA.Create();

            nuevaPersona.COD_PERSONA = pn.nuevoCodigo();
            nuevaPersona.NOMBRE = nuevoAlumno.PERSONA.NOMBRE;
            nuevaPersona.APELLIDO = nuevoAlumno.PERSONA.APELLIDO;
            nuevaPersona.CORREO = nuevoAlumno.PERSONA.CORREO;
            nuevaPersona.TELEFONO = nuevoAlumno.PERSONA.TELEFONO;
            nuevaPersona.NACIONALIDAD = nuevoAlumno.PERSONA.NACIONALIDAD;
            nuevaPersona.FK_COD_GENERO = nuevoAlumno.PERSONA.FK_COD_GENERO;
            nuevaPersona.FK_COD_CIUDAD = nuevoAlumno.PERSONA.FK_COD_CIUDAD;

            UsuarioNegocio unegocio = new UsuarioNegocio();
            USUARIO usuario = db.USUARIO.Create();
            usuario.COD_USUARIO = unegocio.nuevoCodigo();
            usuario.NOMBRE_USUARIO = nuevoAlumno.PERSONA.USUARIO.NOMBRE_USUARIO;
            usuario.CONTRASENNA = nuevoAlumno.PERSONA.USUARIO.CONTRASENNA;

            usuario.FK_COD_TIPO = 2;
            nuevaPersona.FK_COD_USUARIO = usuario.COD_USUARIO;


            db.PERSONA.Add(nuevaPersona);
            db.USUARIO.Add(usuario);
            db.SaveChanges();

            AlumnoNegocio anegocio = new AlumnoNegocio();
            anegocio.Crear((int)nuevaPersona.COD_PERSONA, nuevoAlumno.FECHA_NACIMIENTO);

            ViewBag.RegistroExitoso = "Alumno ingresado exitosamente";
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

        public ActionResult ComprobarSesion()
        {
            if (ValidarSesionAdministrador())
                return View();
            else
                return RedirectToAction("DenegarAcceso");
        }

        public void CargarDropDownList()
        {
            ViewBag.Generos = new SelectList(db.GENERO.ToList(), "COD_GENERO", "DESCRIPCION");
            ViewBag.Paises = new SelectList(db.PAIS.ToList(), "COD_PAIS", "DESCRIPCION");
            ViewBag.Ciudades = new SelectList(db.CIUDAD.ToList(), "COD_CIUDAD", "DESCRIPCION");
        }
    }
}
