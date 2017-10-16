using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SistemaGestionCEM;
using SistemaGestionCEM.Datos;

namespace SistemaGestionCEM.Tests
{
    [TestClass]
    public class prueba
    {
        [TestMethod]
        public void ConexionTest()
        {
            prueba1 a = new prueba1();
            a.ejemplo();
        }
        [TestMethod]
        public void ConexionTest2()
        {
            Class1 b = new Class1();
            b.ejemplo();
        }
    }
}
