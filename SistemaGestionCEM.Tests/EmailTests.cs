using Microsoft.VisualStudio.TestTools.UnitTesting;
using SistemaGestionCEM.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionCEM.Negocio.Tests
{
    [TestClass()]
    public class EmailTests
    {
        [TestMethod()]
        public void RegistroExitosoTest()
        {
            Email.RegistroExitoso("juan", "alumnocem201@gmail.com", "usua");
        }
    }
}