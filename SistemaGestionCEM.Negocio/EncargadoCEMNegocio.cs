using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SistemaGestionCEM.Datos;

namespace SistemaGestionCEM.Negocio
{
    public class EncargadoCEMNegocio
    {
        public List<PROGRAMA_ESTUDIO> ProgramasEnCursoFinalizados()
        {
            String enCurso = "En Curso";
            String finalizado = "Finalizado";
            List<PROGRAMA_ESTUDIO> programasPublicados;
            using (Entities db = new Entities())
            {
                programasPublicados = db.PROGRAMA_ESTUDIO
                    .Where(r => r.POSTULACION_PROGRAMA
                    .Any(e => e.ESTADO_POSTULACION.DESCRIPCION == enCurso
                    || e.ESTADO_POSTULACION.DESCRIPCION == finalizado))
                    .ToList();
            }
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
    }
}
