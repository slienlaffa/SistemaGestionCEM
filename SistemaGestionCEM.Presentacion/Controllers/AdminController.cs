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

        // es para el filtro de usuarios que puse en el index...
        [HttpPost]
        public ActionResult Index(string listaTipos)
        {
            if (listaTipos != null)
            {
                if (listaTipos.Equals("1"))
                {
                    var admin = db.PERSONA.Include(p => p.USUARIO.TIPO_USUARIO).Where(x => x.USUARIO.FK_COD_TIPO == 1);
                    return View(admin.ToList());
                }
                if (listaTipos.Equals("2"))
                {
                    var alum = db.PERSONA.Include(p => p.USUARIO.TIPO_USUARIO).Where(x => x.USUARIO.FK_COD_TIPO == 2);
                    return View(alum.ToList());
                }
                if (listaTipos.Equals("3"))
                {
                    var familia = db.PERSONA.Include(p=> p.USUARIO.TIPO_USUARIO).Where(x=> x.USUARIO.FK_COD_TIPO == 3);
                    return View(familia.ToList());
                }
                if (listaTipos.Equals("4"))
                {
                    var cem = db.PERSONA.Include(p => p.USUARIO.TIPO_USUARIO).Where(x => x.USUARIO.FK_COD_TIPO == 4);
                    return View(cem.ToList());
                }
                if (listaTipos.Equals("5"))
                {
                    var cel = db.PERSONA.Include(p => p.USUARIO.TIPO_USUARIO).Where(x => x.USUARIO.FK_COD_TIPO == 5);
                    return View(cel.ToList());
                }

                return View();
            }
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
        #region Mantenedor Familia

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
        [ValidateAntiForgeryToken]
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

                TempData["success"] = "La nueva Familia Anfitriona se ha ingresado exitosamente"; //porobando xd
               
                return View();
            }
        }

        public ActionResult EditarFamilia(decimal id) {
            if (ValidarSesionAdministrador())
            {
                var familia = db.FAMILIA_ANFITRIONA.Where(a => a.FK_COD_PERSONA == id).FirstOrDefault();
                CargarDropDownList();
                return View("EditarFamilia", familia);
            }
            else
            {
                return RedirectToAction("DenegarAcceso");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarFamilia(FAMILIA_ANFITRIONA f)
        {
            if (ValidarSesionAdministrador())
            {

                personaNegocio.Actualizar(f.PERSONA.APELLIDO,
                    f.PERSONA.CORREO, (int)f.PERSONA.COD_PERSONA, (int)f.PERSONA.TELEFONO, (int)f.PERSONA.USUARIO.COD_USUARIO, f.PERSONA.NACIONALIDAD, (int)f.PERSONA.FK_COD_CIUDAD
                    , f.PERSONA.NOMBRE, (int)f.PERSONA.FK_COD_GENERO);

                familiaNegocio.Actualizar(
                    (int)f.NUM_BANOS,
                    (int)f.ANTECEDENTES.COD_ANTECEDENTES,
                    (int)f.COD_FAMILIA,
                    (int)f.ANIO_INSCRIPCION,
                    (int)f.NUM_HABITACIONES,
                    f.TIPO_VIVIENDA,
                    (int)f.NUM_INTEGRANTES,
                    (int)f.PERSONA.COD_PERSONA,
                    f.ESTACIONAMIENTO,
                    f.MASCOTA_DESCRIPCION
                    );
                var persona = db.PERSONA.Include(p => p.CIUDAD).Include(p => p.GENERO).Include(p => p.USUARIO);
                TempData["success"] = "Datos de familia actualizados correctamente!";
                return View("Index", persona.ToList());
            }
            else
            {
                return RedirectToAction("DenegarAcceso");
            }
        }

        #endregion

        #region Mantenedor Alumno

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

            TempData["success"] = "Alumno ingresado exitosamente";
            return View();
        }
        // Funcionando 18-11
        public ActionResult EditarAlumno(decimal id)
        {
            if (ValidarSesionAdministrador())
            {
                var alumno = db.ALUMNO.Where(a => a.FK_COD_PERSONA == id).FirstOrDefault();
                CargarDropDownList();
                return View("EditarAlumno", alumno);
            }
            else
            {
                return RedirectToAction("DenegarAcceso");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarAlumno(ALUMNO alumno)
        { //poor editar

            if (ValidarSesionAdministrador())
            {

                personaNegocio.Actualizar(alumno.PERSONA.APELLIDO,
                    alumno.PERSONA.CORREO, (int)alumno.PERSONA.COD_PERSONA, (int)alumno.PERSONA.TELEFONO, (int)alumno.PERSONA.USUARIO.COD_USUARIO, alumno.PERSONA.NACIONALIDAD,
                    (int)alumno.PERSONA.FK_COD_CIUDAD ,alumno.PERSONA.NOMBRE, (int)alumno.PERSONA.FK_COD_GENERO);

                alumnoNegocio.Actualizar((int)alumno.COD_ALUMNO, (int)alumno.PERSONA.COD_PERSONA, alumno.FECHA_NACIMIENTO);

                var persona = db.PERSONA.Include(p => p.CIUDAD).Include(p => p.GENERO).Include(p => p.USUARIO);

                TempData["success"] = "Datos del Alumno actualizados correctamente!";
                return View("Index", persona.ToList());
            }
            else
            {
                return RedirectToAction("DenegarAcceso");
            }
        }

        #endregion

        #region Mantenedor EncargadoCEL

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

            usuario.FK_COD_TIPO = 5;
            nuevaPersona.FK_COD_USUARIO = usuario.COD_USUARIO;


            db.PERSONA.Add(nuevaPersona);
            db.USUARIO.Add(usuario);
            db.SaveChanges();

            celNegocio.Crear((int)nuevaPersona.COD_PERSONA, (int)nuevoCEL.FK_COD_CEL);

            TempData["success"] = "Usuario Encargado CEL ingresado exitosamente";
            return View();
        }

        public ActionResult EditarEncargadoCEL(decimal id)
        {
            if (ValidarSesionAdministrador())
            {
                var cel = db.ENCARGADO_CEL.Where(a => a.FK_COD_PERSONA == id).FirstOrDefault();
                CargarDropDownList();
                return View("EditarEncargadoCEL", cel);
            }
            else
            {
                return RedirectToAction("DenegarAcceso");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarEncargadoCEL(ENCARGADO_CEL cel)
        {
            if (ValidarSesionAdministrador())
            {
                personaNegocio.Actualizar(cel.PERSONA.APELLIDO,
                    cel.PERSONA.CORREO, (int)cel.PERSONA.COD_PERSONA, (int)cel.PERSONA.TELEFONO, (int)cel.PERSONA.USUARIO.COD_USUARIO, cel.PERSONA.NACIONALIDAD,
                    (int)cel.PERSONA.FK_COD_CIUDAD, cel.PERSONA.NOMBRE, (int)cel.PERSONA.FK_COD_GENERO);

                celNegocio.Actualizar((int)cel.COD_ENCARGADOCEL, (int)cel.PERSONA.COD_PERSONA, (int)cel.CENTRO_ESTUDIO_LOCAL.COD_CEL);

                var persona = db.PERSONA.Include(p => p.CIUDAD).Include(p => p.GENERO).Include(p => p.USUARIO);

                TempData["success"] = "Datos de encargado CEL actualizados correctamente!";
                return View("Index", persona.ToList());
            }
            else
            {
                return RedirectToAction("DenegarAcceso");
            }
        }
        

        #endregion

        #region Mantenedor Encargado CEM

        public ActionResult IngresarEncargadoCEM()
        {
            if (ValidarSesionAdministrador())
            {
                CargarDropDownList();

                return View();
            }
            return RedirectToAction("DenegarAcceso");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IngresarEncargadoCEM(ENCARGADO_CEM nuevoCem)
        {
            CargarDropDownList();
            if (!ModelState.IsValid)
                return View();

            var persona = db.PERSONA
               .Where(model => model.USUARIO.NOMBRE_USUARIO
               == nuevoCem.PERSONA.USUARIO.NOMBRE_USUARIO)
               .FirstOrDefault();

            if (persona != null)
            {
                ViewBag.Message = "El nombre de usuario '" + nuevoCem.PERSONA.USUARIO.NOMBRE_USUARIO +
                    "' ya existe, por favor ingrese otro distinto!";
                return View();
            }

            PERSONA nuevaPersona = db.PERSONA.Create();

            nuevaPersona.COD_PERSONA = personaNegocio.nuevoCodigo();
            nuevaPersona.NOMBRE = nuevoCem.PERSONA.NOMBRE;
            nuevaPersona.APELLIDO = nuevoCem.PERSONA.APELLIDO;
            nuevaPersona.CORREO = nuevoCem.PERSONA.CORREO;
            nuevaPersona.TELEFONO = nuevoCem.PERSONA.TELEFONO;
            nuevaPersona.NACIONALIDAD = nuevoCem.PERSONA.NACIONALIDAD;
            nuevaPersona.FK_COD_GENERO = nuevoCem.PERSONA.FK_COD_GENERO;
            nuevaPersona.FK_COD_CIUDAD = nuevoCem.PERSONA.FK_COD_CIUDAD;

            USUARIO usuario = db.USUARIO.Create();
            usuario.COD_USUARIO = unegocio.nuevoCodigo();
            usuario.NOMBRE_USUARIO = nuevoCem.PERSONA.USUARIO.NOMBRE_USUARIO;
            usuario.CONTRASENNA = nuevoCem.PERSONA.USUARIO.CONTRASENNA;

            usuario.FK_COD_TIPO = 4;
            nuevaPersona.FK_COD_USUARIO = usuario.COD_USUARIO;

            db.PERSONA.Add(nuevaPersona);
            db.USUARIO.Add(usuario);
            db.SaveChanges();

            cemNegocio.Crear((int)nuevaPersona.COD_PERSONA);

            TempData["success"] = "Usuario Encargado CEM ingresado exitosamente!";
            return View();
        }

        public ActionResult EditarEncargadoCEM(decimal id)
        {
            if (ValidarSesionAdministrador())
            {
                var cem = db.ENCARGADO_CEM.Where(a => a.FK_COD_PERSONA == id).FirstOrDefault();
                CargarDropDownList();
                return View("EditarEncargadoCEM", cem);
            }
            else
            {
                return RedirectToAction("DenegarAcceso");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarEncargadoCEM(ENCARGADO_CEM cem)
        {
            if (ValidarSesionAdministrador())
            {
                personaNegocio.Actualizar(cem.PERSONA.APELLIDO,
                    cem.PERSONA.CORREO, (int)cem.PERSONA.COD_PERSONA, (int)cem.PERSONA.TELEFONO, (int)cem.PERSONA.USUARIO.COD_USUARIO, cem.PERSONA.NACIONALIDAD,
                    (int)cem.PERSONA.FK_COD_CIUDAD, cem.PERSONA.NOMBRE, (int)cem.PERSONA.FK_COD_GENERO);

                cemNegocio.Actualizar((int)cem.COD_ENCARGADOCEM, (int)cem.PERSONA.COD_PERSONA);

                var persona = db.PERSONA.Include(p => p.CIUDAD).Include(p => p.GENERO).Include(p => p.USUARIO);

                TempData["success"] = "Datos del encargado CEM actualizados correctamente!";
                return View("Index", persona.ToList());
            }
            else
            {
                return RedirectToAction("DenegarAcceso");
            }
        }

        #endregion

        #region Mantenedor Centros

        public ActionResult MostrarCentros()
        {
            if (ValidarSesionAdministrador())
            {
                var centros = db.CENTRO_ESTUDIO_LOCAL.Include(cel=>cel.CIUDAD);
                return View(centros.ToList());
            }
            else
            {
                return RedirectToAction("DenegarAcceso");
            }
        }

        public ActionResult IngresarCentro()
        {
            if (ValidarSesionAdministrador())
            {
                CargarDropDownList();
                return View();
            }
            return RedirectToAction("DenegarAcceso");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IngresarCentro(CENTRO_ESTUDIO_LOCAL cel)
        {
            if (!ModelState.IsValid)
            {
                CargarDropDownList();
                return View();
            }
            var nomb = db.CENTRO_ESTUDIO_LOCAL.Where(m => m.NOMBRE_CENTRO.Equals(cel.NOMBRE_CENTRO));
            if (nomb != null)
            {
                TempData["error"] = "El centro '" + cel.NOMBRE_CENTRO + "' ya ha sido ingresado, ingrese otro distinto.";
                CargarDropDownList();
                return View();
            } 

            CelNegocio negocio = new CelNegocio();
            negocio.Crear(cel.DIRECCION, cel.NOMBRE_CENTRO, cel.CORREO, (int)cel.TELEFONO, cel.AREA_ESPECIALIZACION,
                cel.DESCRIPCION, (int)cel.FK_COD_CIUDAD, cel.NOM_DIRECTOR);

            TempData["success"] ="Centro de Estudio Local ingresado exitosamente!";
            CargarDropDownList();
            return View();
            //Listo, funcionando.
        }

        // muestra informacion detallada de un centro en especifico.
        public ActionResult DetalleCentro(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CENTRO_ESTUDIO_LOCAL centro = db.CENTRO_ESTUDIO_LOCAL.Find(id);
            
            if (centro == null)
            {
                return HttpNotFound();
            }
            return View(centro);
        }

        public ActionResult EliminarCentros(decimal id)
        {
            if (ValidarSesionAdministrador())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CENTRO_ESTUDIO_LOCAL centro = db.CENTRO_ESTUDIO_LOCAL.Find(id);
                if (centro == null)
                {
                    return HttpNotFound();
                }
                return View(centro);
            }
            return RedirectToAction("DenegarAcceso");
          
        }

        [HttpPost, ActionName("EliminarCentros")]
        [ValidateAntiForgeryToken]
        public ActionResult EliminarCentrosConfirmed(decimal id)
        {
            if (ValidarSesionAdministrador())
            {
                if (id <= 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CelNegocio negocio = new CelNegocio();
                negocio.Eliminar((int)id);

                TempData["success"] = "Centro de Estudio Local eliminado con éxito";
                var centros = db.CENTRO_ESTUDIO_LOCAL.Include(cel => cel.CIUDAD);
                return View("MostrarCentros", centros.ToList());
            }
            return RedirectToAction("DenegarAcceso");
        }

        public ActionResult EditarCentro(decimal id)
        {
            if (ValidarSesionAdministrador())
            {
                if (id <= 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CENTRO_ESTUDIO_LOCAL centro = db.CENTRO_ESTUDIO_LOCAL.Find(id);
                if (centro == null)
                {
                    return HttpNotFound();
                }
                CargarDropDownList();
                return View(centro);
            }
            else
            {
                return RedirectToAction("DenegarAcceso");
            } 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarCentro(CENTRO_ESTUDIO_LOCAL centro)
        {
            if (!ModelState.IsValid)
            {
                CargarDropDownList();
                return View();
            }
            CelNegocio negocio = new CelNegocio();
            negocio.Actualizar
            (
                centro.DIRECCION,
                centro.NOMBRE_CENTRO,
                centro.CORREO, 
                (int)centro.COD_CEL,
                (int)centro.TELEFONO, 
                centro.AREA_ESPECIALIZACION,
                centro.DESCRIPCION, 
                (int)centro.FK_COD_CIUDAD,
                centro.NOM_DIRECTOR
            );
            
                TempData["success"] = "Datos actualizados exitosamente!";
                var centros = db.CENTRO_ESTUDIO_LOCAL.Include(a=> a.CIUDAD);
                return View("MostrarCentros", centros);
            // Funcionando 23-11
        }

        #endregion

        // GET: Admin/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERSONA persona = db.PERSONA.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERSONA persona = db.PERSONA.Find(id);
            USUARIO usuario = db.USUARIO.Find(persona.FK_COD_USUARIO);

            if (persona == null)
            {
                return HttpNotFound();
            }
            if (usuario.FK_COD_TIPO == 1)
            {
                TempData["error"] = "Modifique datos solo de tipo Alumno, Encargado CEL, Encargado CEM o Familia";
            }
            if (usuario.FK_COD_TIPO == 2)
            {
                return EditarAlumno(id);
            }
            if (usuario.FK_COD_TIPO == 3)
            {
                return  EditarFamilia(id);
            }
            if (usuario.FK_COD_TIPO == 4)
            {
                return EditarEncargadoCEM(id);
            }
            if (usuario.FK_COD_TIPO == 5)
            {
                return EditarEncargadoCEL(id);
            }

            CargarDropDownList();     
            return View(persona);
        }
  
        #region Eliminar

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

        #endregion

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
