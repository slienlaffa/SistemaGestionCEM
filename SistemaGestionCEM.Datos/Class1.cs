using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionCEM.Datos
{
    public class Class1
    {
        public void ejemplo()
        {
            using (Entities context = new Entities())
            {
                String mensaje = context.GENERO.Find(1).DESCRIPCION;
                Console.WriteLine(mensaje);
            }
        }
    }
}
