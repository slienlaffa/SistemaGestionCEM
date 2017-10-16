using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaGestionCEM.Datos;

namespace SistemaGestionCEM
{
    public class prueba1
    {
        public void ejemplo()
        {
            using (Entities context = new Entities())
            {
                String mensaje = context.GENERO.Find(2).DESCRIPCION;
                Console.WriteLine(mensaje);
            }
        }

    }
}