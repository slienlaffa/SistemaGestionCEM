using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SistemaGestionCEM.Negocio;
using SistemaGestionCEM.Datos;

namespace SistemaGestionCEM.Tests
{
    [TestClass]
    public class CU11___Registrar_Notas_de_Alumnos
    {   
        [TestMethod]
        public void RegistrarNotas()
        {
            EncargadoCEMNegocio cem = new EncargadoCEMNegocio();
            var notas = cem.BuscarNotas(1);
            foreach (var nota in notas)
            {
                nota.NOTA3 = 2;
            }
            bool resultado = cem.RegistrarNotas(notas);
            Assert.IsTrue(resultado);
        }
    }
}
