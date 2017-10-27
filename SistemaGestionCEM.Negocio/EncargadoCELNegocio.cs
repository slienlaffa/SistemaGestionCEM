using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaGestionCEM.Negocio
{
    public class EncargadoCELNegocio
    {
        public bool Crear()
        {
            try
            {
                Conector.Entidades.CrearEncargadoCEL(1,8,nuevoCodigo());
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
                Conector.Entidades.ActualizarEncargadoCEL(1, 8, 2);
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
                Conector.Entidades.EliminarEncargadoCEL(2);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int nuevoCodigo()
        {
            SistemaGestionCEM.Datos.ENCARGADO_CEL encargadoCELBusca = Conector.Entidades.ENCARGADO_CEL.OrderByDescending(e => e.COD_ENCARGADOCEL).First();
            return (int)(encargadoCELBusca.COD_ENCARGADOCEL + 1);
        }
    }
}
