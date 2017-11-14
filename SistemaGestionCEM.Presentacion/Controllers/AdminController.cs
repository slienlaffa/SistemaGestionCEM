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

namespace SistemaGestionCEM.Presentacion.Controllers
{
    public class AdminController : Controller
    {
        private Entities db = new Entities();
        FamiliaAnfitrionaNegocio familiaNegocio = new FamiliaAnfitrionaNegocio();
        AlumnoNegocio alumnoNegocio = new AlumnoNegocio();
        PersonaNegocio personaNegocio = new PersonaNegocio();
        UsuarioNegocio unegocio = new UsuarioNegocio();
        EncargadoCEMNegocio cemNegocio = new EncargadoCEMNegocio();
        EncargadoCELNegocio celNegocio = new EncargadoCELNegocio();

        // GET: Admin
        public ActionResult Index()
        {
            if (ValidarSesionAdministrador())
            {
                var persona = db.PERSONA.Include(p => p.CIUDAD).Include(p => p.GENERO).Include(p => p.USUARIO);
                return View(persona.ToList());
            }
            else {
                return RedirectToAction("DenegarAcceso");
            }

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

                PERSONA nuevaPersona = db.PERSONA.Create();

                nuevaPersona.COD_PERSONA = personaNegocio.nuevoCodigo();
                nuevaPersona.NOMBRE = nuevaFamilia.PERSONA.NOMBRE;
                nuevaPersona.APELLIDO = nuevaFamilia.PERSONA.APELLIDO;
                nuevaPersona.CORREO = nuevaFamilia.PERSONA.CORREO;
                nuevaPersona.TELEFONO = nuevaFamilia.PERSONA.TELEFONO;
                nuevaPersona.NACIONALIDAD = nuevaFamilia.PERSONA.NACIONALIDAD;
                nuevaPersona.FK_COD_GENERO = nuevaFamilia.PERSONA.FK_COD_GENERO;
                nuevaPersona.FK_COD_CIUDAD = nuevaFamilia.PERSONA.FK_COD_CIUDAD;

                USUARIO usuario = db.USUARIO.Create();
                usuario.COD_USUARIO = unegocio.nuevoCodigo();
                usuario.NOMBRE_USUARIO = nuevaFamilia.PERSONA.USUARIO.NOMBRE_USUARIO;
                usuario.CONTRASENNA = nuevaFamilia.PERSONA.USUARIO.CONTRASENNA;

                usuario.FK_COD_TIPO = 3;
                nuevaPersona.FK_COD_USUARIO = usuario.COD_USUARIO;


                db.PERSONA.Add(nuevaPersona);
                db.USUARIO.Add(usuario);
                db.SaveChanges();


                familiaNegocio.Crear((int)nuevaFamilia.NUM_BANOS, 1, DateTime.Now.Year, (int)nuevaFamilia.NUM_HABITACIONES, nuevaFamilia.TIPO_VIVIENDA, (int)nuevaFamilia.NUM_INTEGRANTES,
                    (int)nuevaPersona.COD_PERSONA, nuevaFamilia.ESTACIONAMIENTO, nuevaFamilia.MASCOTA_DESCRIPCION);

                TempData["succes"] = "La nueva Familia Anfitriona se ha ingresado exitosamente"; //porobando xd
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


            PERSONA nuevaPersona = db.PERSONA.Create();

            nuevaPersona.COD_PERSONA = personaNegocio.nuevoCodigo();
            nuevaPersona.NOMBRE = nuevoAlumno.PERSONA.NOMBRE;
            nuevaPersona.APELLIDO = nuevoAlumno.PERSONA.APELLIDO;
            nuevaPersona.CORREO = nuevoAlumno.PERSONA.CORREO;
            nuevaPersona.TELEFONO = nuevoAlumno.PERSONA.TELEFONO;
            nuevaPersona.NACIONALIDAD = nuevoAlumno.PERSONA.NACIONALIDAD;
            nuevaPersona.FK_COD_GENERO = nuevoAlumno.PERSONA.FK_COD_GENERO;
            nuevaPersona.FK_COD_CIUDAD = nuevoAlumno.PERSONA.FK_COD_CIUDAD;


            USUARIO usuario = db.USUARIO.Create();
            usuario.COD_USUARIO = unegocio.nuevoCodigo();
            usuario.NOMBRE_USUARIO = nuevoAlumno.PERSONA.USUARIO.NOMBRE_USUARIO;
            usuario.CONTRASENNA = nuevoAlumno.PERSONA.USUARIO.CONTRASENNA;

            usuario.FK_COD_TIPO = 2;
            nuevaPersona.FK_COD_USUARIO = usuario.COD_USUARIO;


            db.PERSONA.Add(nuevaPersona);
            db.USUARIO.Add(usuario);
            db.SaveChanges();


            alumnoNegocio.Crear((int)nuevaPersona.COD_PERSONA, nuevoAlumno.FECHA_NACIMIENTO);

            ViewBag.RegistroExitoso = "Alumno ingresado exitosamente";
            return View();
        }
        public ActionResult IngresarEncargadoCEL()
        {
            if (ValidarSesionAdministrador())
            {
                CargarDropDownList();
                return View();
            }
            return RedirectToAction("DenegarAcceso");
        }

        [HttpPost]
        public ActionResult IngresarEncargadoCEL(ENCARGADO_CEL nuevoCEL)
        {
            CargarDropDownList();
            if (!ModelState.IsValid)
                return View();

            var persona = db.PERSONA
               .Where(model => model.USUARIO.NOMBRE_USUARIO
               == nuevoCEL.PERSONA.USUARIO.NOMBRE_USUARIO)
               .FirstOrDefault();

            if (persona != null)
            {
                ViewBag.Message = "El nombre de usuario '" + nuevoCEL.PERSONA.USUARIO.NOMBRE_USUARIO +
                    "' ya existe, por favor ingrese otro distinto!";
                return View();
            }


            PERSONA nuevaPersona = db.PERSONA.Create();

            nuevaPersona.COD_PERSONA = personaNegocio.nuevoCodigo();
            nuevaPersona.NOMBRE = nuevoCEL.PERSONA.NOMBRE;
            nuevaPersona.APELLIDO = nuevoCEL.PERSONA.APELLIDO;
            nuevaPersona.CORREO = nuevoCEL.PERSONA.CORREO;
            nuevaPersona.TELEFONO = nuevoCEL.PERSONA.TELEFONO;
            nuevaPersona.NACIONALIDAD = nuevoCEL.PERSONA.NACIONALIDAD;
            nuevaPersona.FK_COD_GENERO = nuevoCEL.PERSONA.FK_COD_GENERO;
            nuevaPersona.FK_COD_CIUDAD = nuevoCEL.PERSONA.FK_COD_CIUDAD;


            USUARIO usuario = db.USUARIO.Create();
            usuario.COD_USUARIO = unegocio.nuevoCodigo();
            usuario.NOMBRE_USUARIO = nuevoCEL.PERSONA.USUARIO.NOMBRE_USUARIO;
            usuario.CONTRASENNA = nuevoCEL.PERSONA.USUARIO.CONTRASENNA;

            usuario.FK_COD_TIPO = 2;
            nuevaPersona.FK_COD_USUARIO = usuario.COD_USUARIO;


            db.PERSONA.Add(nuevaPersona);
            db.USUARIO.Add(usuario);
            db.SaveChanges();

            celNegocio.Crear((int)nuevaPersona.COD_PERSONA, (int)nuevoCEL.FK_COD_CEL);

            TempData["success"] = "Usuario Encargado CEL ingresado exitosamente";
            return View();
        }

        public ActionResult IngresarEncargadoCEM()
        {
            if (ValidarSesionAdministrador())
            {
                CargarDropDownList();

                return View();
            }
            return RedirectToAction("DenegarAcceso");
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

        // GET: Admin/Create
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
        public ActionResult Delete(decimal id)
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



        // aqui viene despues del "Delete"
        // Funcionando
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
          
            PERSONA persona = db.PERSONA.Find(id);
            USUARIO usuario = db.USUARIO.Find(persona.FK_COD_USUARIO);
            if (usuario.FK_COD_TIPO == 1)
            {
                TempData["error"] = "No puede eliminarse a si mismo!";
                return View(persona);
            }
            if (usuario.FK_COD_TIPO == 2) //Alumno
            {
                ALUMNO alumno = db.ALUMNO.Where(a => a.FK_COD_PERSONA == id).FirstOrDefault();
                alumnoNegocio.Eliminar((int)alumno.COD_ALUMNO);
            }
               
            if (usuario.FK_COD_TIPO == 3) //familia
            {
                FAMILIA_ANFITRIONA familia = db.FAMILIA_ANFITRIONA.Where(f => f.FK_COD_PERSONA == id).FirstOrDefault();
                familiaNegocio.Eliminar((int)familia.COD_FAMILIA);
              
            }
            if (usuario.FK_COD_TIPO == 4) //Encargado CEM
            {
                ENCARGADO_CEM cem = db.ENCARGADO_CEM.Where(a => a.FK_COD_PERSONA == id).FirstOrDefault();
                cemNegocio.Eliminar((int)cem.COD_ENCARGADOCEM);
            }
            if (usuario.FK_COD_TIPO == 5) //Encargado CEL
            {
                ENCARGADO_CEL CEL = db.ENCARGADO_CEL.Where(a => a.FK_COD_PERSONA == id).FirstOrDefault();
                celNegocio.Eliminar((int)CEL.COD_ENCARGADOCEL);
            }

            personaNegocio.Eliminar((int)id);
            unegocio.Eliminar((int)persona.FK_COD_USUARIO);

            TempData["success"] = "Usuario eliminado con éxito";
            return View("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public bool ValidarSesionAdministrador()
        {
            if (Session["SesionActual"] != null)
            {
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
            ViewBag.FK_CEL = new SelectList(db.CENTRO_ESTUDIO_LOCAL.ToList(), "COD_CEL", "NOMBRE_CENTRO");
        }

        public ActionResult DenegarAcceso()
        {
            return View();
        }
    }
}
