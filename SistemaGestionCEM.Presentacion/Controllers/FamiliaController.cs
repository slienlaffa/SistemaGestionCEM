using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaGestionCEM.Presentacion.Controllers
{
    public class FamiliaController : Controller
    {
        // GET: Familia
        public ActionResult Index()
        {
            return View();
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
    }
}