using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SistemaGestionCEM;
using SistemaGestionCEM.Negocio;

namespace SistemaGestionCEM.Tests
{
    [TestClass]
    public class prueba
    {
        [TestMethod]
        public void LoginCorrecto()
        {
            Autentificacion autentificacion = new Autentificacion();
            String resultado = autentificacion.Login("prueba", "prueba");
            Assert.IsNotNull(resultado);
        }
    }
}
