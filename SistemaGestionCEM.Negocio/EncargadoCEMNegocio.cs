﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SistemaGestionCEM.Datos;

namespace SistemaGestionCEM.Negocio
{
    public class EncargadoCEMNegocio : Negocio
    {
        public Dictionary<string, int> GenerarResumenPorEstado()
        {
            Dictionary<string, int> dictionary =
                new Dictionary<string, int>();

            dictionary.Add("Terminados", ContarPorEstado(FINALIZADO));
            dictionary.Add("En curso", ContarPorEstado(EN_CURSO));
            dictionary.Add("Publicados", ContarPorEstado(PUBLICADO));
            dictionary.Add("No publicados", ContarPorEstado(NO_PUBLICADO));
            dictionary.Add("Cancelados", ContarPorEstado(CANCELADO));
            dictionary.Add("Pendientes", ContarPorEstado(PENDIENTE));
            dictionary.Add("Total", db.POSTULACION_PROGRAMA.Count());
            return dictionary;
        }

        private int ContarPorEstado(int estado)
        {
            return db.POSTULACION_PROGRAMA.Count(p => p.FK_COD_ESTADO == estado);
        }

        public bool CrearProgramaEstudio(PROGRAMA_ESTUDIO programaEstudio, string usuario)
        {
            try
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
                postulacion.FK_COD_ESTADO = NO_PUBLICADO;

                db.PROGRAMA_ESTUDIO.Add(programaEstudio);
                db.POSTULACION_PROGRAMA.Add(postulacion);
                db.SaveChanges();

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

        public bool PublicarPrograma(int codPostulacionPrograma)
        {
            try
            {
                var programa = db.POSTULACION_PROGRAMA.Find(codPostulacionPrograma);
                programa.FK_COD_ESTADO = PUBLICADO;
                programa.FECHA = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CancelarPrograma(int codPostulacionPrograma)
        {
            try
            {
                var programa = db.POSTULACION_PROGRAMA.Find(codPostulacionPrograma);
                programa.FK_COD_ESTADO = CANCELADO;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IQueryable<POSTULACION_ALUMNO> PostulacionesPendientes()
        {
            var postulacionesPendientes = db.POSTULACION_ALUMNO
                    .Where(p => p.FK_COD_ESTADO == PENDIENTE);

            return postulacionesPendientes;
        }

        public ws_estado_alumnos.alumno obtenerEstadoAlumno(int codigoAlumno)
        {
            try
            {
                ws_estado_alumnos.alumno estadoAlumno;
                
                ALUMNO alumno = db.ALUMNO.Find(codigoAlumno);
                ws_estado_alumnos.WebServiceCEMClient estadoAlumnos = new ws_estado_alumnos.WebServiceCEMClient();
                estadoAlumno = estadoAlumnos.obtenerEstadoAlumnos(alumno.PERSONA.NOMBRE, alumno.PERSONA.APELLIDO);
                if (estadoAlumno != null)
                    estadoAlumno.codigoAlumno = codigoAlumno;

                return estadoAlumno;
            }
            catch { return null; }
        }

        public bool SeleccionarPostulante(int codigoAlumno, bool esSeleccionado)
        {
            try
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

                return true;
            }
            catch(Exception e) {
                Console.WriteLine(e.Message);
                return false; }
        }

        public IQueryable<PROGRAMA_ESTUDIO> ProgramasEnCursoFinalizados()
        {
            var programasPublicados = db.PROGRAMA_ESTUDIO
                .Where(r => r.POSTULACION_PROGRAMA
                .Any(e => e.FK_COD_ESTADO == EN_CURSO
                || e.FK_COD_ESTADO == FINALIZADO));

            return programasPublicados;
        }       
        

        public bool Crear(int codPersona)
        {
            try
            {
                Conector.Entidades.CrearEncargadoCEM(codPersona,nuevoCodigo());
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(int codEncargadoCEM, int codPersona)
        {
            try
            {
                Conector.Entidades.ActualizarEncargadoCEM(codEncargadoCEM, codPersona);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(int codigo)
        {
            try
            {
                Conector.Entidades.EliminarEncargadoCEM(codigo);
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
    }
}
