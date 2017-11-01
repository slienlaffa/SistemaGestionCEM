using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SistemaGestionCEM.Negocio;
using SistemaGestionCEM.Datos;

namespace SistemaGestionCEM.Tests
{
    [TestClass]
    public class CU12__Seleccion_de_Postulante
    {   
        [TestMethod]
        public void ObtenerEstadoAlumno()
        {
            EncargadoCEMNegocio cem = new EncargadoCEMNegocio();
            var estadoAlumno = cem.obtenerEstadoAlumno(1);
            Assert.IsNotNull(estadoAlumno);
        }

        [TestMethod]
        public void AprobarPostulacion()
        {
            EncargadoCEMNegocio cem = new EncargadoCEMNegocio();
            var resultado = cem.SeleccionarPostulante(1, true);
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void RechazarPostulacion()
        {
            EncargadoCEMNegocio cem = new EncargadoCEMNegocio();
            var resultado = cem.SeleccionarPostulante(2, false);
            Assert.IsTrue(resultado);
        }
    }
}
