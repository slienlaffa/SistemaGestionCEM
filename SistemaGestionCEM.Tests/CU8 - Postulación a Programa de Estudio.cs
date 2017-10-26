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
            AlumnoNegocio a = new AlumnoNegocio();
            List<PROGRAMA_ESTUDIO> programas = a.ListarProgramasPublicados();
            foreach(PROGRAMA_ESTUDIO p in programas)
            {
                Console.WriteLine(p.NOMBRE_PROGRAMA);
            }
            Assert.IsNotNull(programas);
        }

        [TestMethod]
        public void BuscarPrograma()
        {
            AlumnoNegocio a = new AlumnoNegocio();
            PROGRAMA_ESTUDIO programa = a.BuscarProgramaEstudio(2);
            Console.WriteLine(programa.NOMBRE_PROGRAMA);
            Assert.IsNotNull(programa);
        }

        [TestMethod]
        public void BuscarProgramaInexistente()
        {
            AlumnoNegocio a = new AlumnoNegocio();
            PROGRAMA_ESTUDIO programa = a.BuscarProgramaEstudio(444);
            Assert.IsNull(programa);
        }

        [TestMethod]
        public void N53_PostulaProgramaCorrectamente()
        {
            AlumnoNegocio a = new AlumnoNegocio();
            POSTULACION_ALUMNO resultado = a.PostularPrograma(2, 2);
            Assert.IsNotNull(resultado);
        }
    }
}
