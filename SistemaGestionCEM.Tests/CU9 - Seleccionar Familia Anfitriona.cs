using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SistemaGestionCEM.Negocio;
using SistemaGestionCEM.Datos;
using System.Collections.Generic;

namespace SistemaGestionCEM.Tests
{
    [TestClass]
    public class CU9__Seleccionar_Familia_Anfitriona
    {
        [TestMethod]
        public void SeleccionarFamiliaAprobado()
        {
            AlumnoNegocio a = new AlumnoNegocio();
            bool resultado = a.SeleccionarFamilia(3, 1);
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void SeleccionarFamiliaPendiente()
        {
            AlumnoNegocio a = new AlumnoNegocio();
            bool resultado = a.SeleccionarFamilia(1, 1);
            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void ListarFamiliasDisponibles()
        {
            AlumnoNegocio a = new AlumnoNegocio();
            List<FAMILIA_ANFITRIONA> familias = a.FamiliasDisponibles();
            foreach (FAMILIA_ANFITRIONA p in familias)
            {
                Console.WriteLine(p.COD_FAMILIA);
            }
            Assert.IsNotNull(familias);
        }

        [TestMethod]
        public void RecuperarFamilia()
        {
            AlumnoNegocio a = new AlumnoNegocio();
            FAMILIA_ANFITRIONA familia = a.RecuperarFamilia(1);
            Console.WriteLine(familia.COD_FAMILIA);
            Assert.IsNotNull(familia);
        }

        [TestMethod]
        public void BuscarProgramaInexistente()
        {
            AlumnoNegocio a = new AlumnoNegocio();
            FAMILIA_ANFITRIONA familia = a.RecuperarFamilia(444);
            Assert.IsNull(familia);
        }
    }
}
