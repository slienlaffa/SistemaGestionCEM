using SistemaGestionCEM.Datos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaGestionCEM.Negocio;

namespace SistemaGestionCEM.Presentacion.Controllers
{
    public class FamiliaController : Controller
    {

        protected Entities db = new Entities();

        // GET: Familia
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SubirAntecedentes()
        {
            string usuario = Session["Nombre"].ToString();
            decimal persona = db.USUARIO.Where(u => u.NOMBRE_USUARIO == usuario).FirstOrDefault().PERSONA.FirstOrDefault().COD_PERSONA;
            var familia = db.FAMILIA_ANFITRIONA.Where(e => e.FK_COD_PERSONA == persona).FirstOrDefault();

            if (familia.ANTECEDENTES != null)
                return View(familia.ANTECEDENTES);
            return View();
        }

        [HttpPost]
        public ActionResult SubirAntecedentes(HttpPostedFileBase FOTO1, HttpPostedFileBase FOTO2, HttpPostedFileBase FOTO3,
            HttpPostedFileBase CERT_RESIDENCIA, HttpPostedFileBase CERT_ANTECEDENTES, HttpPostedFileBase CERT_LABORAL)
        {
            try
            {
                string usuario = Session["Nombre"].ToString();
                decimal persona = db.USUARIO.Where(u => u.NOMBRE_USUARIO == usuario).FirstOrDefault().PERSONA.FirstOrDefault().COD_PERSONA;
                var familia = db.FAMILIA_ANFITRIONA.Where(e => e.FK_COD_PERSONA == persona).FirstOrDefault();

                string dir = Server.MapPath("~/AntecedentesFamilia/" + familia.COD_FAMILIA);
                if (!Directory.Exists(dir))  // if it doesn't exist, create
                    Directory.CreateDirectory(dir);

                var antecedentes = familia.ANTECEDENTES;
                if (antecedentes == null)
                {
                    int cod = new AntecedentesNegocio().nuevoCodigo();
                    familia.FK_COD_ANTECEDENTES = cod;
                    antecedentes.COD_ANTECEDENTES = cod;
                }

                if (FOTO1 != null)
                {
                    if(System.IO.File.Exists(dir + "/" + antecedentes.FOTO1))
                        System.IO.File.Delete(dir + "/" + antecedentes.FOTO1);
                    antecedentes.FOTO1 = guardar(FOTO1, dir);
                }
                if (FOTO2 != null)
                {
                    if (System.IO.File.Exists(dir + "/" + antecedentes.FOTO2))
                        System.IO.File.Delete(dir + "/" + antecedentes.FOTO2);
                    antecedentes.FOTO2 = guardar(FOTO2, dir);
                }
                if (FOTO3 != null)
                {
                    if (System.IO.File.Exists(dir + "/" + antecedentes.FOTO3))
                        System.IO.File.Delete(dir + "/" + antecedentes.FOTO3);
                    antecedentes.FOTO3 = guardar(FOTO3, dir);
                }
                if (CERT_RESIDENCIA != null)
                {
                    if (System.IO.File.Exists(dir + "/" + antecedentes.CERT_RESIDENCIA))
                        System.IO.File.Delete(dir + "/" + antecedentes.CERT_RESIDENCIA);
                    antecedentes.CERT_RESIDENCIA = guardar(CERT_RESIDENCIA, dir);
                }
                if (CERT_ANTECEDENTES != null)
                {
                    if (System.IO.File.Exists(dir + "/" + antecedentes.CERT_ANTECEDENTES))
                        System.IO.File.Delete(dir + "/" + antecedentes.CERT_ANTECEDENTES);
                    antecedentes.CERT_ANTECEDENTES = guardar(CERT_ANTECEDENTES, dir);
                }
                if (CERT_LABORAL != null)
                {
                    if (System.IO.File.Exists(dir + "/" + antecedentes.CERT_LABORAL))
                        System.IO.File.Delete(dir + "/" + antecedentes.CERT_LABORAL);
                    antecedentes.CERT_LABORAL = guardar(CERT_LABORAL, dir);
                }

                db.SaveChanges();
                TempData["success"] = "Archivos subidos correctamente.";
                return View();
            }
            catch
            {
                TempData["error"] = "Error al subir archivos.";
                return View();
            }
        }

        public string guardar(HttpPostedFileBase archivo, string dir)
        {
            string _FileName = Path.GetRandomFileName() + Path.GetExtension(archivo.FileName);
            string _path = Path.Combine(dir, _FileName);
            archivo.SaveAs(_path);
            return _FileName;
        }

        public bool ValidarSesion()
        {
            if (Session["SesionActual"] != null)
            {
                string session = Session["SesionActual"].ToString();
                if (session.Equals("Familia"))
                    return true;
                else
                    return false;
            }
            return false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}