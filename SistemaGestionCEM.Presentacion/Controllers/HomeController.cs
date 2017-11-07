using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaGestionCEM.Negocio;
using SistemaGestionCEM.Datos;

namespace SistemaGestionCEM.Presentacion.Controllers
{
    public class HomeController : Controller
    {
        private Entities db = new Entities();

        public ActionResult Index()
        {
            AlumnoNegocio aa = new AlumnoNegocio();
            List<PROGRAMA_ESTUDIO> programas = aa.ProgramasPublicados().ToList();
            ViewBag.Message = programas.First().NOMBRE_PROGRAMA;
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(USUARIO usuario)
        {
            if (ModelState.IsValid)
            {
                using (SistemaGestionCEM.Datos.Entities db = new Entities())
                {
                    

                    UsuarioNegocio user2 = new UsuarioNegocio();

                    string user = usuario.NOMBRE_USUARIO;
                    string pass = usuario.CONTRASENNA;

                    if (user2.valida(user, pass))
                    {
                        usuario = db.USUARIO
                            .Where(u => u.NOMBRE_USUARIO == user 
                            && u.CONTRASENNA == pass).FirstOrDefault();

                        Session["Nombre"] = usuario.NOMBRE_USUARIO;
                        if (usuario.TIPO_USUARIO.COD_TIPO == 1)
                        {
                            Session["SesionActual"] = "Administrador";
                            return RedirectToAction("Index", "Admin");
                        }
                        if (usuario.TIPO_USUARIO.COD_TIPO == 2)
                        {
                            Session["SesionActual"] = "Alumno";
                            return RedirectToAction("Index", "Alumno");
                        }
                        if (usuario.TIPO_USUARIO.COD_TIPO == 3)
                        {
                            Session["SesionActual"] = "Familia";
                            return RedirectToAction("Index", "Familia");
                        }
                        if (usuario.TIPO_USUARIO.COD_TIPO == 4)
                        {
                            Session["SesionActual"] = "EncargadoCEM";
                            return RedirectToAction("Index", "EncargadoCEM");
                        }
                        if (usuario.TIPO_USUARIO.COD_TIPO == 5)
                        {
                            Session["SesionActual"] = "EncargadoCEL";
                            return RedirectToAction("Index", "EncargadoCEL");
                        }
                        return View();
                    }
                    ViewBag.Message = "Nombre de Usuario o Contraseña incorrectos";
                    return View();

                }   
            }
            return View();
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Login", "Home");
        }

        // En este tira el formulario
        [AllowAnonymous]
        public ActionResult AutoRegistroFamilia()
        {
            CargarDropDownList();
            return View();
        }

        [HttpPost]
        public ActionResult AutoRegistroFamilia(FAMILIA_ANFITRIONA nuevaFamilia)
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
                UsuarioNegocio unegocio = new UsuarioNegocio();
                unegocio.Crear(nuevaFamilia.PERSONA.USUARIO.CONTRASENNA, nuevaFamilia.PERSONA.USUARIO.NOMBRE_USUARIO, 3);

                PersonaNegocio pn = new PersonaNegocio();
                pn.Crear(nuevaFamilia.PERSONA.APELLIDO, nuevaFamilia.PERSONA.CORREO, (int)nuevaFamilia.PERSONA.TELEFONO, unegocio.nuevoCodigo(), nuevaFamilia.PERSONA.NACIONALIDAD,
                   (int)nuevaFamilia.PERSONA.CIUDAD.COD_CIUDAD, nuevaFamilia.PERSONA.NOMBRE, (int)nuevaFamilia.PERSONA.GENERO.COD_GENERO);

                FamiliaAnfitrionaNegocio anegocio = new FamiliaAnfitrionaNegocio();
                anegocio.Crear((int)nuevaFamilia.NUM_BANOS, 0, DateTime.Now.Year, (int)nuevaFamilia.NUM_HABITACIONES, nuevaFamilia.TIPO_VIVIENDA, (int)nuevaFamilia.NUM_INTEGRANTES, pn.nuevoCodigo(),
                    nuevaFamilia.ESTACIONAMIENTO, nuevaFamilia.MASCOTA_DESCRIPCION);


                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult AutoRegistroAlumno()
        {
            CargarDropDownList();
            return View();
        }

        [HttpPost]
        public ActionResult AutoRegistroAlumno(ALUMNO nuevoAlumno)
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
                    == nuevoAlumno.PERSONA.USUARIO.NOMBRE_USUARIO)
                    .FirstOrDefault();

                if (persona != null)
                {
                    ViewBag.Message = "El nombre de usuario '" + nuevoAlumno.PERSONA.USUARIO.NOMBRE_USUARIO +
                        "' ya existe, por favor ingrese otro distinto!";
                    return View("Index");
                }
                // Cambiar por pn.Crear();
                PersonaNegocio pn = new PersonaNegocio();
                PERSONA persona2 = db.PERSONA.Create();
        
                persona2.COD_PERSONA = pn.nuevoCodigo();
                persona2.NOMBRE = nuevoAlumno.PERSONA.NOMBRE;
                persona2.APELLIDO = nuevoAlumno.PERSONA.APELLIDO;
                persona2.CORREO = nuevoAlumno.PERSONA.CORREO;
                persona2.TELEFONO = nuevoAlumno.PERSONA.TELEFONO;
                persona2.NACIONALIDAD = nuevoAlumno.PERSONA.NACIONALIDAD;

                PAIS p = db.PAIS.Create();
                PaisNegocio pnegocio = new PaisNegocio();
                p.COD_PAIS = pnegocio.nuevoCodigo();
                p.DESCRIPCION = nuevoAlumno.PERSONA.CIUDAD.PAIS.DESCRIPCION;
               
                CIUDAD c = db.CIUDAD.Create();
                CiudadNegocio cnegocio = new CiudadNegocio();
                c.COD_CIUDAD = cnegocio.nuevoCodigo();
                c.DESCRIPCION = nuevoAlumno.PERSONA.CIUDAD.DESCRIPCION;
                c.PAIS = p;
                persona2.CIUDAD = c;

                GENERO g = db.GENERO.Create();
                GeneroNegocio gnegocio = new GeneroNegocio();
                g.COD_GENERO = gnegocio.nuevoCodigo();
                g.DESCRIPCION = nuevoAlumno.PERSONA.GENERO.DESCRIPCION;
                persona2.GENERO = g;

                UsuarioNegocio unegocio = new UsuarioNegocio();
                USUARIO user2 = db.USUARIO.Create();
                user2.COD_USUARIO = unegocio.nuevoCodigo();
                user2.NOMBRE_USUARIO = nuevoAlumno.PERSONA.USUARIO.NOMBRE_USUARIO;
                user2.CONTRASENNA = nuevoAlumno.PERSONA.USUARIO.CONTRASENNA;

                TIPO_USUARIO tipo = db.TIPO_USUARIO.Create();
                TipoUsuarioNegocio tipoUsuarioNegocio = new TipoUsuarioNegocio();
                tipo.COD_TIPO = tipoUsuarioNegocio.nuevoCodigo();
                tipo.DESCRIPCION = "Alumno";
                user2.TIPO_USUARIO = tipo;
                persona2.USUARIO = user2;

                AlumnoNegocio anegocio = new AlumnoNegocio();
                anegocio.Crear((int)persona2.COD_PERSONA, nuevoAlumno.FECHA_NACIMIENTO);


               // db.PERSONA.Add(persona2);
                db.SaveChanges();
                return RedirectToAction("Index");

                // Funcionando, guarda todo. pero el pais, ciudad y genero me lo guarda como el codigo y no como string XD
            }
        }
        public void CargarDropDownList()
        {
            ViewBag.Generos = new SelectList(db.GENERO, "COD_GENERO", "DESCRIPCION");
            ViewBag.Paises = new SelectList(db.PAIS,"COD_PAIS", "DESCRIPCION");
            ViewBag.Ciudades = new SelectList(db.CIUDAD, "COD_CIUDAD", "DESCRIPCION");
        }
    }
}