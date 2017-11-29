using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SistemaGestionCEM.Negocio;
using SistemaGestionCEM.Datos;
using System.Linq;

namespace SistemaGestionCEM.Tests
{
    [TestClass]
    public class CU8___Postulación_a_Programa_de_Estudio
    {
        [TestMethod]
        public void ListarProgramasPublicados()
        {
            AlumnoNegocio a = new AlumnoNegocio();
            var programas = a.ProgramasPublicados().ToList();
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
        public void PostulaProgramaCorrectamente()
        {
            AlumnoNegocio a = new AlumnoNegocio();
            POSTULACION_ALUMNO resultado = a.PostularPrograma(2, "Fer.DiazM");
            Assert.IsNotNull(resultado);
        }

        [TestMethod]
        public void PostulaConPostulacionPrevia()
        {
            AlumnoNegocio a = new AlumnoNegocio();
            POSTULACION_ALUMNO resultado = a.PostularPrograma(2, "Fer.DiazM");
            Assert.IsNull(resultado);
        }
    }
}
