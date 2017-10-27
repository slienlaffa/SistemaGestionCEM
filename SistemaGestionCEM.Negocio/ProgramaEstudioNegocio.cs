using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaGestionCEM.Negocio
{
    public class ProgramaEstudioNegocio
    {
        public bool Crear(DateTime fechaLimitePostulacion, int minAlumno, int codTipoPrograma, int codEncargadoCEM, 
            string duracion, string areaConocimiento, int maxAlumno, int codPais, string descripcion, string nombrePrograma)
        {
            try
            {
                Conector.Entidades.CrearProgramaEstudio(fechaLimitePostulacion,minAlumno,codTipoPrograma,codEncargadoCEM,nuevoCodigo(),duracion,
                    areaConocimiento,maxAlumno,codPais,descripcion,nombrePrograma);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(DateTime fechaLimitePostulacion, int minAlumno, int codTipoPrograma, int codEncargadoCEM,
            int codPrograma, string duracion, string areaConocimiento, int maxAlumno, int codPais, string descripcion, string nombrePrograma)
        {
            try
            {
                Conector.Entidades.ActualizarProgramaEstudio(fechaLimitePostulacion, minAlumno, codTipoPrograma, codEncargadoCEM, codPrograma, duracion,
                    areaConocimiento, maxAlumno, codPais, descripcion, nombrePrograma);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(int codPrograma)
        {
            try
            {
                Conector.Entidades.EliminarProgramaEstudio(codPrograma);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string Mostrar()
        {
            List<SistemaGestionCEM.Datos.PROGRAMA_ESTUDIO> listaProgramaEstudio = Conector.Entidades.
                PROGRAMA_ESTUDIO.ToList();
            //crear coleccionProgramas para trabajar
            return listaProgramaEstudio.Last().COD_PROGRAMA.ToString();
        }

        public int nuevoCodigo()
        {
            SistemaGestionCEM.Datos.PROGRAMA_ESTUDIO programaEstudioBusca = Conector.Entidades.PROGRAMA_ESTUDIO.
                OrderByDescending(e => e.COD_PROGRAMA).First();
            return (int)(programaEstudioBusca.COD_PROGRAMA + 1);
        }

        public bool publicarPrograma(int codCEL, int codPrograma, int codEncargadoCEM, DateTime fecha, int codPostulacion, int codEstado)
        {
            PostulacionProgramaNegocio modificaPostulacion = new PostulacionProgramaNegocio();
            if (modificaPostulacion.Actualizar(codCEL, codPrograma, codEncargadoCEM, fecha, codPostulacion, 5))
                return true;
            else
                return false;
        }
    }
}
