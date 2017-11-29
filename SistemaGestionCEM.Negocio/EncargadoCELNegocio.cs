using SistemaGestionCEM.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaGestionCEM.Negocio
{
    public class EncargadoCELNegocio : Negocio
    {

        public IQueryable<PROGRAMA_ESTUDIO> ProgramasEnCursoPorCEL(string usuario)
        {
            int codCEL = (int)ObtenerCELPorUsuario(usuario).FK_COD_CEL;

            var programasPublicados = db.PROGRAMA_ESTUDIO
                .Where(r => r.POSTULACION_PROGRAMA
                .Any(e => e.FK_COD_ESTADO == EN_CURSO
                && e.FK_COD_CEL == codCEL));

            return programasPublicados;
        }

        public IQueryable<POSTULACION_PROGRAMA> PostulacionProgramasPorCEL(string usuario)
        {
            int codCEL = (int)ObtenerCELPorUsuario(usuario).FK_COD_CEL;

            var programas = db.POSTULACION_PROGRAMA
                .Where(e => e.FK_COD_CEL == codCEL);

            return programas;
        }

        public bool RegistrarNotas(List<DETALLE_NOTAS> notas)
        {
            try
            {
                foreach (var nota in notas)
                {
                    db.Entry(nota).State = System.Data.Entity.EntityState.Modified;
                   
                }
                db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public IQueryable<POSTULACION_PROGRAMA> PostulacionesPublicadas()
        {
            var postulaciones = db.POSTULACION_PROGRAMA
                .Where(p => p.FK_COD_ESTADO == PUBLICADO
                && p.FK_COD_CEL == null);
            
            return postulaciones;
        }

        public bool PostularPrograma(int codPostulacionPrograma, string usuario)
        {
            try
            {
                var programa = db.POSTULACION_PROGRAMA.Find(codPostulacionPrograma);
                var cel = ObtenerCELPorUsuario(usuario);
                if (cel != null)
                    programa.FK_COD_CEL = cel.FK_COD_CEL;
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        private ENCARGADO_CEL ObtenerCELPorUsuario(string usuario)
        {
            decimal persona = db.USUARIO.Where(u => u.NOMBRE_USUARIO == usuario).FirstOrDefault().PERSONA.FirstOrDefault().COD_PERSONA;
            var cel = db.ENCARGADO_CEL.Where(e => e.FK_COD_PERSONA == persona).FirstOrDefault();
            return cel;
        }

        public bool Crear(int codPersona, int codigoCentroCEL)
        {
            try
            {
                Conector.Entidades.CrearEncargadoCEL(codPersona,codigoCentroCEL,nuevoCodigo());
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(int codEncargadoCEL, int codPersona, int codCEL)
        {
            try
            {
                Conector.Entidades.ActualizarEncargadoCEL(codEncargadoCEL, codPersona, codCEL);
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
                Conector.Entidades.EliminarEncargadoCEL(codigo);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int nuevoCodigo()
        {
            SistemaGestionCEM.Datos.ENCARGADO_CEL encargadoCELBusca = Conector.Entidades.ENCARGADO_CEL.OrderByDescending(e => e.COD_ENCARGADOCEL).First();
            return (int)(encargadoCELBusca.COD_ENCARGADOCEL + 1);
        }
    }
}
