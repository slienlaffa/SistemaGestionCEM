using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaGestionCEM.Negocio
{
    public class EncargadoCEMNegocio
    {
        public bool Crear()
        {
            try
            {
                Conector.Entidades.CrearEncargadoCEM(8,nuevoCodigo());
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
                Conector.Entidades.ActualizarEncargadoCEM(8, 2);
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
                Conector.Entidades.EliminarEncargadoCEM(2);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int nuevoCodigo()
        {
            SistemaGestionCEM.Datos.ENCARGADO_CEM encargadoCEMBusca = Conector.Entidades.ENCARGADO_CEM.OrderByDescending(e => e.COD_ENCARGADOCEM).First();
            return (int)(encargadoCEMBusca.COD_ENCARGADOCEM + 1);
        }
    }
}
