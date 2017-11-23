using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaGestionCEM.Datos;
using System.Data.Entity.Validation;

namespace SistemaGestionCEM.Negocio
{
    public class AlumnoNegocio : Negocio
    {
        public IQueryable<POSTULACION_PROGRAMA> ProgramasPublicados()
        {
            var programas = db.POSTULACION_PROGRAMA.Where(e => e.FK_COD_ESTADO == PUBLICADO
                    && e.FK_COD_CEL != null);
            return programas;
        }

        public PROGRAMA_ESTUDIO BuscarProgramaEstudio(int codigoPrograma)
        {
            try
            {
                PROGRAMA_ESTUDIO programa;
                programa = db.PROGRAMA_ESTUDIO.Find(codigoPrograma);
                return programa;
            }
            catch
            {
                return null;
            }
        }

        public POSTULACION_ALUMNO PostularPrograma(int codigoPrograma, string usuario)
        {

            try
            {
                decimal persona = db.USUARIO.Where(u => u.NOMBRE_USUARIO == usuario).FirstOrDefault().PERSONA.FirstOrDefault().COD_PERSONA;
                var alumno = db.ALUMNO.Where(e => e.FK_COD_PERSONA == persona).FirstOrDefault();
                int codigoAlumno = (int)alumno.COD_ALUMNO;

                if (alumnoTieneOtrasPostulaciones(codigoAlumno))
                {
                    POSTULACION_ALUMNO postulacion = new POSTULACION_ALUMNO();
                    postulacion.FK_COD_ALUMNO = codigoAlumno;
                    postulacion.FK_COD_PROGRAMA = codigoPrograma;
                    postulacion.FK_COD_ESTADO = 1;
                    postulacion.FECHA = DateTime.Now;
                    postulacion.SEGURO = null;
                    postulacion.COD_POSTULACION = GetNextSequenceValuePostulacionAlumno();

                    db.POSTULACION_ALUMNO.Add(postulacion);
                    db.SaveChanges();
                    return postulacion;
               }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public bool alumnoTieneOtrasPostulaciones(int codigoAlumno)
        {
            ALUMNO alumno = db.ALUMNO.Find(codigoAlumno);
            // Si el alumno no tiene todas sus postulaciones rechazadas, finalizadas o canceladas 
            // significa que tiene una activa en algun estado
            if (alumno.POSTULACION_ALUMNO
                .Any(p => p.FK_COD_ESTADO != RECHAZADO
                && p.FK_COD_ESTADO != FINALIZADO
                && p.FK_COD_ESTADO != CANCELADO))
                return false;

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

        public IQueryable<FAMILIA_ANFITRIONA> FamiliasDisponibles()
        {
            var familiasDisponibles = db.FAMILIA_ANFITRIONA
                    .Where(f => f.POSTULACION_ALUMNO
                    .Any(p => p.FK_COD_ESTADO != RECHAZADO
                    && p.FK_COD_ESTADO != FINALIZADO
                    && p.FK_COD_ESTADO != CANCELADO));
         
            return familiasDisponibles;
        }

        public FAMILIA_ANFITRIONA RecuperarFamilia(int codigoFamilia)
        {
            FAMILIA_ANFITRIONA familiaDetalle;
            familiaDetalle = db.FAMILIA_ANFITRIONA.Find(codigoFamilia);
            return familiaDetalle;
        }

        public bool SeleccionarFamilia(int codigoAlumno, int codigoFamilia)
        {
            try
            {
                if (EsAlumnoAprobado(codigoAlumno))
                {
                    POSTULACION_ALUMNO postulacionAlumno;
                    using (Entities db = new Entities())
                    {
                        postulacionAlumno = db.ALUMNO.Find(codigoAlumno)
                            .POSTULACION_ALUMNO.Last();
                        postulacionAlumno.FK_COD_FAMILIA = codigoFamilia;
                        db.SaveChanges();
                    }
                    return true;
                }
                else
                    return false;
            }catch
            {
                return false;
            }
        }

        public bool EsAlumnoAprobado(int codigoAlumno)
        {
            int estado = (int)db.ALUMNO.Find(codigoAlumno)
                .POSTULACION_ALUMNO.Last().FK_COD_ESTADO;
            if (estado == APROBADO)
                return true;
            else
                return false; 
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
