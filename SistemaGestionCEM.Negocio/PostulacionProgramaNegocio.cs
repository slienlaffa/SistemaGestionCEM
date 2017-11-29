using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaGestionCEM.Negocio
{
    public class PostulacionProgramaNegocio
    {
        public bool Crear(int codCEL, int codPrograma, int codEncargadoCEM, DateTime fecha, int codEstado)
        {
            try
            {
                //agregar validacion de requisitos (cuales son?)
                Conector.Entidades.CrearPostulacionPrograma(codCEL, codPrograma, codEncargadoCEM, fecha, nuevoCodigo(), codEstado);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(int codCEL, int codPrograma, int codEncargadoCEM, DateTime fecha, int codPostulacion, int codEstado)
        {
            try
            {
                Conector.Entidades.ActualizarPostulacionPrograma(codCEL, codPrograma, codEncargadoCEM, fecha, codPostulacion, codEstado);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(int codPostulacion)
        {
            try
            {
                Conector.Entidades.EliminarPostulacionPrograma(codPostulacion);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int nuevoCodigo()
        {
            SistemaGestionCEM.Datos.POSTULACION_PROGRAMA postulacionProgramaEstudioBusca = Conector.Entidades.POSTULACION_PROGRAMA.
                OrderByDescending(e => e.COD_POSTULACIONPROGRAMA).First();
            return (int)(postulacionProgramaEstudioBusca.COD_POSTULACIONPROGRAMA + 1);
        }
    }
}
