using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaGestionCEM.Datos;
using System.Data.Entity.Validation;

namespace SistemaGestionCEM.Negocio
{
    public class AlumnoNegocio
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

        public POSTULACION_ALUMNO PostularPrograma(int codigoPrograma, int codigoAlumno)
        {

            try
            {                
                if (alumnoTieneOtrasPostulaciones(codigoAlumno))
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
                    return postulacion;
               }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public bool alumnoTieneOtrasPostulaciones(int codigoAlumno)
        {
            using (Entities db = new Entities())
            {
                int rechazado = 3;
                int finalizado = 6;
                int cancelado = 7;
                ALUMNO alumno = db.ALUMNO.Find(codigoAlumno);
                // Si el alumno no tiene todas sus postulaciones rechazadas, finalizadas o canceladas 
                // significa que tiene una activa en algun estado
                if (alumno.POSTULACION_ALUMNO
                    .Any(p => p.FK_COD_ESTADO != rechazado
                    && p.FK_COD_ESTADO != finalizado
                    && p.FK_COD_ESTADO != cancelado))
                    return false;
            }
            return true;          
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
        public bool Crear(int codPersona, DateTime fechaNacimiento)
        {
            try
            {
                Conector.Entidades.CrearAlumno(nuevoCodigo(), codPersona, fechaNacimiento);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(int codAlumno, int codPersona, DateTime fechaNacimiento)
        {
            try
            {
                Conector.Entidades.ActualizarAlumno(codAlumno, codPersona, fechaNacimiento);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(int codAlumno)
        {
            try
            {
                Conector.Entidades.EliminarAlumno(codAlumno);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string Mostrar()
        {
            List<SistemaGestionCEM.Datos.ALUMNO> listaAlumnos = Conector.Entidades.ALUMNO.ToList();
            //crear coleccionAlumnos para trabajar
            return listaAlumnos.Last().COD_ALUMNO.ToString();
        }

        public int nuevoCodigo()
        {
            SistemaGestionCEM.Datos.ALUMNO alumnoBusca = Conector.Entidades.ALUMNO.OrderByDescending(e => e.COD_ALUMNO).First();
            return (int)(alumnoBusca.COD_ALUMNO + 1);
        }
    }
}
