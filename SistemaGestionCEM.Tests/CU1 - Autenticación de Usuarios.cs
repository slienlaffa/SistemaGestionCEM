﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SistemaGestionCEM;
using SistemaGestionCEM.Datos;
using SistemaGestionCEM.Negocio;
using SistemaGestionCEM.Biblioteca;

namespace SistemaGestionCEM.Tests
{
    [TestClass]
    public class prueba
    {
        [TestMethod]
        public void Login()
        {
            UsuarioNegocio u = new UsuarioNegocio();
            if (u.valida("prueba", "prueba"))
                Console.Write(u.Usuario.Cod_tipo);
            else
                Console.Write("Incorrecto");
        }
        [TestMethod]
        public void CrearUsuario()
        {
            UsuarioNegocio u = new UsuarioNegocio();
       /*     if (u.Crear())
                Console.Write("se creo");
            else
                Console.Write("fallo");*/
        }
        [TestMethod]
        public void CreAlumno()
        {
            AlumnoNegocio u = new AlumnoNegocio();
            Console.Write(u.Mostrar());
        }
    }
}
