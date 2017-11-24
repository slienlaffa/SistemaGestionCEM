using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaGestionCEM.Negocio
{
    public class CelNegocio
    {
        public bool Crear(string direccion, string nombreCentro, string correo, int telefono,
            string areaEspecializacion, string descripcion, int codCiudad, string nomDirector)
        {
            try
            {
                Conector.Entidades.CrearCEL(direccion, nombreCentro, correo, nuevoCodigo(), telefono,
                    areaEspecializacion,descripcion,codCiudad, nomDirector);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(string direccion, string nombreCentro, string correo, int codCel, int telefono,
            string areaEspecializacion, string descripcion, int codCiudad, string nomDirector)
        {
            try
            {
                Conector.Entidades.ActualizarCEL(direccion, nombreCentro, correo, codCel, telefono,
                    areaEspecializacion, descripcion, codCiudad, nomDirector);
                return true;
            }
            catch
            {
                return false;
            }
        }
      
        public bool Eliminar(int id)
        {
            try
            {
                Conector.Entidades.EliminarCEL(id);
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
