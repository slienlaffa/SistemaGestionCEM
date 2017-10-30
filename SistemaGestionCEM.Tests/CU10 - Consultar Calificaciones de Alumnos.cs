using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SistemaGestionCEM.Negocio;
using SistemaGestionCEM.Datos;

namespace SistemaGestionCEM.Tests
{
    [TestClass]
    public class CU10___Consultar_Calificaciones_de_Alumnos
    {
        [TestMethod]
        public void ProgramasEnCursoFinalizados()
        {
            EncargadoCEMNegocio cem = new EncargadoCEMNegocio();
            var programas = cem.ProgramasEnCursoFinalizados();
            Assert.IsNotNull(programas);
        }

        [TestMethod]
        public void BuscarNotas()
        {
            EncargadoCEMNegocio cem = new EncargadoCEMNegocio();
            var programas = cem.BuscarNotas(1);
            Assert.IsNotNull(programas);
        }
    }
}
