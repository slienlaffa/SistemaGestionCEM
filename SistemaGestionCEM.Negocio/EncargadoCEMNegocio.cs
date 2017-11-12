﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SistemaGestionCEM.Datos;

namespace SistemaGestionCEM.Negocio
{
    public class EncargadoCEMNegocio : Negocio, IDisposable
    {
        private Entities db = new Entities();
        
        public bool CrearProgramaEstudio(PROGRAMA_ESTUDIO programaEstudio, string usuario)
        {
            try
            {
                using (Entities db = new Entities())
                {
                    decimal persona = db.USUARIO.Where(u => u.NOMBRE_USUARIO == usuario).FirstOrDefault().PERSONA.FirstOrDefault().COD_PERSONA;
                    var cem = db.ENCARGADO_CEM.Where(e => e.FK_COD_PERSONA == persona).FirstOrDefault();
                    if (cem != null)
                        programaEstudio.FK_COD_ENCARGADOCEM = cem.COD_ENCARGADOCEM;

                    programaEstudio.COD_PROGRAMA = new ProgramaEstudioNegocio().nuevoCodigo();

                    POSTULACION_PROGRAMA postulacion = new POSTULACION_PROGRAMA();
                    postulacion.COD_POSTULACIONPROGRAMA = new PostulacionProgramaNegocio().nuevoCodigo();
                    postulacion.FK_COD_ENCARGADOCEM = programaEstudio.FK_COD_ENCARGADOCEM;
                    postulacion.FK_COD_PROGRAMA = programaEstudio.COD_PROGRAMA;
                    postulacion.FK_COD_ESTADO = 4; // No Publicado

                    db.PROGRAMA_ESTUDIO.Add(programaEstudio);
                    db.POSTULACION_PROGRAMA.Add(postulacion);
                    db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IQueryable<POSTULACION_PROGRAMA> PostulacionesNoPublicadas()
        {            
            IQueryable<POSTULACION_PROGRAMA> postulaciones = db.POSTULACION_PROGRAMA
                .Where(p => p.FK_COD_ESTADO == NO_PUBLICADO);

            return postulaciones;            
        }

        public void PublicarPrograma(int codPostulacionPrograma)
        {
            var programa = db.POSTULACION_PROGRAMA.Find(codPostulacionPrograma);
            programa.FK_COD_ESTADO = PUBLICADO;
            programa.FECHA = DateTime.Now;
            db.SaveChanges();
        }

        public void CancelarPrograma(int codPostulacionPrograma)
        {
            var programa = db.POSTULACION_PROGRAMA.Find(codPostulacionPrograma);
            programa.FK_COD_ESTADO = CANCELADO;
            db.SaveChanges();
        }

        public List<POSTULACION_ALUMNO> PostulacionesPendientes()
        {
            List<POSTULACION_ALUMNO> postulacionesPendientes;
            using (Entities db = new Entities())
            {
                postulacionesPendientes = db.POSTULACION_ALUMNO
                    .Where(p => p.FK_COD_ESTADO == PENDIENTE)
                    .ToList();
            }
            return postulacionesPendientes;
        }

        public ws_estado_alumnos.alumno obtenerEstadoAlumno(int codigoAlumno)
        {
            ws_estado_alumnos.alumno estadoAlumno;
            using (Entities db = new Entities())
            {
                ALUMNO alumno = db.ALUMNO.Find(codigoAlumno);
                ws_estado_alumnos.WebServiceCEMClient estadoAlumnos = new ws_estado_alumnos.WebServiceCEMClient();
                estadoAlumno = estadoAlumnos.obtenerEstadoAlumnos(alumno.PERSONA.NOMBRE, alumno.PERSONA.APELLIDO);
                if(estadoAlumno != null)
                    estadoAlumno.codigoAlumno = codigoAlumno;
            }
            return estadoAlumno;
        }

        public bool SeleccionarPostulante(int codigoAlumno, bool esSeleccionado)
        {
            try
            {
                using (Entities db = new Entities())
                {
                    POSTULACION_ALUMNO postulacion = db.POSTULACION_ALUMNO
                        .Where(p => p.FK_COD_ALUMNO == codigoAlumno
                        && p.FK_COD_ESTADO == PENDIENTE)
                        .First();
                    if (esSeleccionado)
                        postulacion.FK_COD_ESTADO = APROBADO;
                    else
                        postulacion.FK_COD_ESTADO = RECHAZADO;
                    db.SaveChanges();

                    Email.ResultadoPostulacion(postulacion.ALUMNO.PERSONA.NOMBRE,
                        postulacion.ALUMNO.PERSONA.CORREO, 
                        postulacion.PROGRAMA_ESTUDIO.NOMBRE_PROGRAMA,
                        esSeleccionado);
                }
                return true;
            }
            catch(Exception e) {
                Console.WriteLine(e.Message);
                return false; }
        }

        public List<PROGRAMA_ESTUDIO> ProgramasEnCursoFinalizados()
        {
            List<PROGRAMA_ESTUDIO> programasPublicados;
            
            programasPublicados = db.PROGRAMA_ESTUDIO
                .Where(r => r.POSTULACION_PROGRAMA
                .Any(e => e.FK_COD_ESTADO == EN_CURSO
                || e.FK_COD_ESTADO == FINALIZADO))
                .ToList();

            return programasPublicados;
        }

        public List<DETALLE_NOTAS> BuscarNotas(int codigoPrograma)
        {
            IQueryable<DETALLE_ALUMNO> alumnos;
            List<DETALLE_NOTAS> notas = new List<DETALLE_NOTAS>();
                using (Entities db = new Entities())
                {
                    alumnos = db.DETALLE_ALUMNO
                     .Where(d => d.FK_COD_PROGRAMA == codigoPrograma);
                    foreach(var a in alumnos)
                    {
                        notas.Add(a.DETALLE_NOTAS.Last());
                    }
                }
                return notas;        
        }

        public bool RegistrarNotas(List<DETALLE_NOTAS> notas)
        {
            try
            {
                using (Entities db = new Entities())
                {
                    foreach (var nota in notas)
                    {
                        db.Entry(nota).State = System.Data.Entity.EntityState.Modified;
                    }
                    db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

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

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
