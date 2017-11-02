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
                            Session["TipoUsuario"] = "Alumno";
                            return RedirectToAction("Index", "Alumno");
                        }
                        if (usuario.TIPO_USUARIO.COD_TIPO == 3)
                        {
                            Session["TipoUsuario"] = "Familia";
                            return RedirectToAction("Index", "Familia");
                        }
                        if (usuario.TIPO_USUARIO.COD_TIPO == 4)
                        {
                            Session["TipoUsuario"] = "EncargadoCEM";
                            return RedirectToAction("Index", "EncargadoCEM");
                        }
                        if (usuario.TIPO_USUARIO.COD_TIPO == 5)
                        {
                            Session["TipoUsuario"] = "EncargadoCEL";
                            return RedirectToAction("Index", "EncargadoCEL");
                        }
                        return RedirectToAction("Index", "Login");
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
            return View();
        }

        [HttpPost]
        public ActionResult AutoRegistroFamilia(FAMILIA_ANFITRIONA nuevaFamilia)
        {
            var usr = db.USUARIO
                    .Where(model => model.NOMBRE_USUARIO
                    == nuevaFamilia.PERSONA.USUARIO.NOMBRE_USUARIO)
                    .FirstOrDefault();

            if (usr != null)
            {
                ViewBag.Message = "El nombre de usuario '" + nuevaFamilia.PERSONA.USUARIO.NOMBRE_USUARIO +
                    "' ya existe, por favor ingrese otro distinto!";
                return View("Index");
            }

            FamiliaAnfitrionaNegocio faNegocio = new FamiliaAnfitrionaNegocio();
            FAMILIA_ANFITRIONA familiaAnfitriona = db.FAMILIA_ANFITRIONA.Create();
            familiaAnfitriona.COD_FAMILIA = faNegocio.nuevoCodigo();
            familiaAnfitriona.NUM_INTEGRANTES = nuevaFamilia.NUM_INTEGRANTES;
            familiaAnfitriona.NUM_HABITACIONES = nuevaFamilia.NUM_HABITACIONES;
            familiaAnfitriona.NUM_BANOS = nuevaFamilia.NUM_INTEGRANTES;
            familiaAnfitriona.TIPO_VIVIENDA = nuevaFamilia.TIPO_VIVIENDA;
            familiaAnfitriona.ESTACIONAMIENTO = nuevaFamilia.ESTACIONAMIENTO;
            familiaAnfitriona.MASCOTA_DESCRIPCION = nuevaFamilia.MASCOTA_DESCRIPCION;
            familiaAnfitriona.ANIO_INSCRIPCION = nuevaFamilia.ANIO_INSCRIPCION;

            PERSONA persona = db.PERSONA.Create();
            PersonaNegocio pnegocio = new PersonaNegocio();
            persona.COD_PERSONA = pnegocio.nuevoCodigo();
            persona.NOMBRE = nuevaFamilia.PERSONA.NOMBRE;
            persona.APELLIDO = nuevaFamilia.PERSONA.APELLIDO;
            persona.CORREO = nuevaFamilia.PERSONA.CORREO;
            persona.TELEFONO = nuevaFamilia.PERSONA.TELEFONO;
            persona.NACIONALIDAD = nuevaFamilia.PERSONA.NACIONALIDAD;
            CIUDAD ciudad = db.CIUDAD.Create();
            ciudad.DESCRIPCION = nuevaFamilia.PERSONA.CIUDAD.DESCRIPCION;
            ciudad.PAIS = nuevaFamilia.PERSONA.CIUDAD.PAIS;
            persona.CIUDAD = ciudad;

            GENERO g = db.GENERO.Create();
            g.DESCRIPCION = nuevaFamilia.PERSONA.GENERO.DESCRIPCION;
            persona.GENERO = g;
            familiaAnfitriona.PERSONA = persona;


            UsuarioNegocio unegocio = new UsuarioNegocio();
            USUARIO user = db.USUARIO.Create();
            user.COD_USUARIO = unegocio.nuevoCodigo();
            user.NOMBRE_USUARIO = nuevaFamilia.PERSONA.USUARIO.NOMBRE_USUARIO;
            user.CONTRASENNA = nuevaFamilia.PERSONA.USUARIO.CONTRASENNA;

            TIPO_USUARIO tipo = db.TIPO_USUARIO.Create();
            tipo.COD_TIPO = 3;
            tipo.DESCRIPCION = "Familia"; //familia
            user.TIPO_USUARIO = tipo; 
         
         
            persona.USUARIO = user;

            db.FAMILIA_ANFITRIONA.Add(familiaAnfitriona);
           
          
            // Falla. 12:24 
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult AutoRegistroAlumno()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AutoRegistroAlumno(PERSONA newUser)
        {
           
                var persona = db.PERSONA
                    .Where(model => model.USUARIO.NOMBRE_USUARIO
                    == newUser.USUARIO.NOMBRE_USUARIO)
                    .FirstOrDefault();
          
                if (persona != null)
                {
                    ViewBag.Message = "El nombre de usuario '" + newUser.USUARIO.NOMBRE_USUARIO + 
                        "' ya existe, por favor ingrese otro distinto!";
                    return View("Index");
                }
           

                PersonaNegocio pn = new PersonaNegocio();
                PERSONA persona2 = db.PERSONA.Create();
                persona2.COD_PERSONA = pn.nuevoCodigo();
                persona2.NOMBRE = newUser.NOMBRE;
                persona2.APELLIDO = newUser.APELLIDO;
                persona2.CORREO = newUser.CORREO;
                persona2.TELEFONO = newUser.TELEFONO;
                persona2.NACIONALIDAD = newUser.NACIONALIDAD;
                

                CIUDAD c = db.CIUDAD.Create();
                c.DESCRIPCION = newUser.CIUDAD.DESCRIPCION;
                c.PAIS = newUser.CIUDAD.PAIS;
                persona2.CIUDAD = c;

                GENERO g = db.GENERO.Create();
                g.DESCRIPCION = newUser.GENERO.DESCRIPCION;
                persona2.GENERO = g;

                UsuarioNegocio unegocio = new UsuarioNegocio();
                USUARIO user2 = db.USUARIO.Create();
                user2.COD_USUARIO = unegocio.nuevoCodigo();
                user2.NOMBRE_USUARIO = newUser.USUARIO.NOMBRE_USUARIO;
                user2.CONTRASENNA = newUser.USUARIO.CONTRASENNA;

                TIPO_USUARIO tipo = db.TIPO_USUARIO.Create();
                tipo.COD_TIPO = 2;
                tipo.DESCRIPCION = "Alumno";
                user2.TIPO_USUARIO = tipo; //Alumno
                persona2.USUARIO = user2;

                AlumnoNegocio alum = new AlumnoNegocio();
                ALUMNO alumno = db.ALUMNO.Create();
                alumno.COD_ALUMNO = alum.nuevoCodigo();
                alumno.PERSONA.COD_PERSONA = persona2.COD_PERSONA;


                db.ALUMNO.Add(alumno);
                db.SaveChanges();
                return RedirectToAction("Index");

                //Hasta aqui llegué, 00:00, 02-11, NReyes 
                // no funca, me sale dbupdateexception.
           
        }
    }
}