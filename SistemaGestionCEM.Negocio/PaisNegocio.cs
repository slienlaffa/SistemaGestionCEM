using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaGestionCEM.Negocio
{
    public class PaisNegocio
    {
        public int nuevoCodigo()
        {
            SistemaGestionCEM.Datos.PAIS pais = Conector.Entidades.PAIS.OrderByDescending(e => e.COD_PAIS).First();
            return (int)(pais.COD_PAIS + 1);
        }
    }
}
