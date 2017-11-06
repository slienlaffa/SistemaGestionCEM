using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaGestionCEM.Negocio
{
    public class GeneroNegocio
    {
        public int nuevoCodigo()
        {
            SistemaGestionCEM.Datos.GENERO genero = Conector.Entidades.GENERO.OrderByDescending(e => e.COD_GENERO).First();
            return (int)(genero.COD_GENERO + 1);
        }
    }
}
