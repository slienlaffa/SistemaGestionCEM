using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaGestionCEM.Negocio
{
    public class PersonaNegocio
    {
        public bool Crear()
        {
            try
            {
                Conector.Entidades.CrearPersona("Sepulveda", "sepulved", nuevoCodigo(), 123456789, 8,
                    "chilena", 4, "Josefa", 1);
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
                Conector.Entidades.ActualizarPersona("Sepulveda", "josef", 8, 123456789, 8,
                    "chilena", 4, "Josefa", 1);
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
                Conector.Entidades.EliminarPersona(8);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int nuevoCodigo()
        {
            SistemaGestionCEM.Datos.PERSONA personaBusca = Conector.Entidades.PERSONA.OrderByDescending(e => e.COD_PERSONA).First();
            return (int)(personaBusca.COD_PERSONA + 1);
        }
    }
}
