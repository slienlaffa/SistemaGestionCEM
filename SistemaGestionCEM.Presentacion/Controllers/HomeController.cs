using System;
using System.Linq;
using System.Web.Mvc;
using SistemaGestionCEM.Negocio;
using SistemaGestionCEM.Datos;

namespace SistemaGestionCEM.Presentacion.Controllers
{
    public class HomeController : Controller
    {
        private Entities db = new Entities();
        FamiliaAnfitrionaNegocio familiaNegocio = new FamiliaAnfitrionaNegocio();
        PersonaNegocio pn = new PersonaNegocio();
        UsuarioNegocio unegocio = new UsuarioNegocio();
        AlumnoNegocio alumnoNegocio = new AlumnoNegocio();


        public ActionResult Index()
        {
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
                        TempData["info"] = "Bienvenido.";
                        return CrearSesion(user);
                    }
                    else
                    {
                        ViewBag.Message = "Usuario o contraseña incorrectos.";
                        return View();
                    }

                }   
            }
            
            return View();
        }

        public ActionResult CrearSesion(string nombreUsuario)
        {
            using(Entities db =new Entities()){
                var usuario = db.USUARIO
                            .Where(u => u.NOMBRE_USUARIO == nombreUsuario).FirstOrDefault();

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

            if (! ModelState.IsValid)
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

                nuevaPersona.COD_PERSONA = pn.nuevoCodigo();
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
                TempData["info"] = "Bienvenido.";
                return CrearSesion(usuario.NOMBRE_USUARIO);
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

            if (! ModelState.IsValid)
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
            //api
            Negocio.Email.RegistroExitoso(nuevaPersona.NOMBRE,nuevaPersona.CORREO,nuevaPersona.USUARIO.NOMBRE_USUARIO);
            TempData["info"] = "Bienvenido.";
            return CrearSesion(usuario.NOMBRE_USUARIO);

        }
        public void CargarDropDownList()
        {
            ViewBag.Generos = new SelectList(db.GENERO.ToList(), "COD_GENERO",  "DESCRIPCION");
            ViewBag.Paises = new SelectList(db.PAIS.ToList(), "COD_PAIS", "DESCRIPCION");
            ViewBag.Ciudades = new SelectList(db.CIUDAD.ToList(), "COD_CIUDAD", "DESCRIPCION" );
        }
    }
}