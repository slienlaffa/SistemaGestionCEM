using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaGestionCEM.Datos;

namespace SistemaGestionCEM.Negocio
{
    public class Alumno
    {
        public List<PROGRAMA_ESTUDIO> ListarProgramasPublicados()
        {
            String estado = "Publicado";
            List<PROGRAMA_ESTUDIO> programasPublicados;
            using (Entities db = new Entities())
            {
                programasPublicados = db.PROGRAMA_ESTUDIO
                    .Where(r => r.POSTULACION_PROGRAMA
                    .Any(e => e.ESTADO_POSTULACION.DESCRIPCION == estado))
                    .ToList();
            }
            return programasPublicados;
        }

        public PROGRAMA_ESTUDIO BuscarProgramaEstudio(int codigoPrograma)
        {
            try
            {
                PROGRAMA_ESTUDIO programa;
                using (Entities db = new Entities())
                {
                    programa = db.PROGRAMA_ESTUDIO.Find(codigoPrograma);
                }
                return programa;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool PostularPrograma(int codigoPrograma, int codigoAlumno)
        {
            try
            {
                POSTULACION_ALUMNO postulacion = new POSTULACION_ALUMNO();
                postulacion.FK_COD_ALUMNO = codigoAlumno;
                postulacion.FK_COD_PROGRAMA = codigoPrograma;
                postulacion.FK_COD_ESTADO = 1;
                postulacion.FECHA = new DateTime();
                using (Entities db = new Entities())
                {
                    postulacion.COD_POSTULACION = db.GetNextSequenceValuePostulacionAlumno();

                    Console.WriteLine(postulacion.COD_POSTULACION);
                    db.POSTULACION_ALUMNO.Add(postulacion);
                }
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                return false;
            }
        }
    }
}
