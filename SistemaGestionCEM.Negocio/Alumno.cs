using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaGestionCEM.Datos;
using System.Data.Entity.Validation;

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
                postulacion.FECHA = DateTime.Now;
                postulacion.SEGURO = null;
                using (Entities db = new Entities())
                {
                    postulacion.COD_POSTULACION = GetNextSequenceValuePostulacionAlumno();
                    db.POSTULACION_ALUMNO.Add(postulacion);
                    db.SaveChanges();
                }
                return true;
            }
            catch(DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return false;
            }
        }

        public int GetNextSequenceValuePostulacionAlumno()
        {
            using (Entities db = new Entities())
            {
                var rawQuery = db.Database.SqlQuery<int>("SELECT SEQ_POSALUMNO.NEXTVAL FROM dual");
                var task = rawQuery.SingleAsync();
                int nextVal = task.Result;
                return nextVal;
            }
        }
    }
}
