using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaGestionCEM.Negocio
{
    public class FamiliaAnfitrionaNegocio
    {
        public bool Crear()
        {
            try
            {
                Conector.Entidades.CrearFamilia(2,2,nuevoCodigo(),2010,3,"Dpto",2,8,"1","Gato");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar()
        {
            try
            {
                Conector.Entidades.ActualizarFamilia(2, 2, nuevoCodigo(), 2011, 3, "Dpto", 2, 8, "1", "Gato");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar()
        {
            try
            {
                Conector.Entidades.EliminarFamilia(2);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int nuevoCodigo()
        {
            SistemaGestionCEM.Datos.FAMILIA_ANFITRIONA familiaBusca = Conector.Entidades.FAMILIA_ANFITRIONA.OrderByDescending(e => e.COD_FAMILIA).First();
            return (int)(familiaBusca.COD_FAMILIA + 1);
        }
    }
}
