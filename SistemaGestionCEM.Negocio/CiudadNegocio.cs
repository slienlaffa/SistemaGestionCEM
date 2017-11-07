using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaGestionCEM.Negocio
{
    public class CiudadNegocio
    {

        public int nuevoCodigo()
        {
            SistemaGestionCEM.Datos.CIUDAD ciudad = Conector.Entidades.CIUDAD.OrderByDescending(e => e.COD_CIUDAD).First();
            return (int)(ciudad.COD_CIUDAD + 1);
        }
    }
}
