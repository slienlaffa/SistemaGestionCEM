using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SistemaGestionCEM.Negocio;
using SistemaGestionCEM.Datos;

namespace SistemaGestionCEM.Tests
{
    [TestClass]
    public class CU8___Postulación_a_Programa_de_Estudio
    {
        [TestMethod]
        public void ListarProgramasPublicados()
        {
            Alumno a = new Alumno();
            List<PROGRAMA_ESTUDIO> programas = a.ListarProgramasPublicados();
            foreach(PROGRAMA_ESTUDIO p in programas)
            {
                Console.WriteLine(p.NOMBRE_PROGRAMA);
            }
            Assert.IsNotNull(programas);
        }
    }
}
