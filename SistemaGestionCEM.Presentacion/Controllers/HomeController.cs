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

                        Session["NombreUsuario"] = usuario.NOMBRE_USUARIO;
                        if (usuario.TIPO_USUARIO.COD_TIPO == 1)
                        {
                            Session["TipoUsuario"] = "Administrador";
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
        public ActionResult AutoRegistroFamilia(PERSONA persona)
        {
            
                return View();
        }

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
                user2.TIPO_USUARIO.COD_TIPO = 2; //Alumno
                persona2.USUARIO = user2;

                db.PERSONA.Add(persona2);
                db.SaveChanges();
                return RedirectToAction("Index");

                //Hasta aqui llegué, 00:00, 02-11, NReyes 
                // no probé.
            
           
        }
    }
}