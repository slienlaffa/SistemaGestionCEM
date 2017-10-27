using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaGestionCEM.Negocio
{
    public class CelNegocio
    {
        public bool Crear()
        {
            try
            {
                Conector.Entidades.CrearCEL("dir","History","a@a",nuevoCodigo(),123456789,
                    "Historia","asdf",6,"Daniel Sira");
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
                Conector.Entidades.ActualizarCEL("dir", "History", "a@a", nuevoCodigo(), 123456789,
                    "Historia", "asdf", 6, "Daniel Sira");
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
                Conector.Entidades.EliminarCEL(2);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string Mostrar()
        {
            List<SistemaGestionCEM.Datos.CENTRO_ESTUDIO_LOCAL> listaCEL = Conector.Entidades.CENTRO_ESTUDIO_LOCAL.ToList();
            //crear coleccionAlumnos para trabajar
            return listaCEL.Last().COD_CEL.ToString();
        }

        public int nuevoCodigo()
        {
            SistemaGestionCEM.Datos.CENTRO_ESTUDIO_LOCAL celBusca = Conector.Entidades.CENTRO_ESTUDIO_LOCAL.
                OrderByDescending(e => e.COD_CEL).First();
            return (int)(celBusca.COD_CEL + 1);
        }
    }
}
