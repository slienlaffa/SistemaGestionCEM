using SistemaGestionCEM.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaGestionCEM.Negocio
{
    public class EncargadoCELNegocio : Negocio, IDisposable
    {
        private Entities db = new Entities();

        public IQueryable<POSTULACION_PROGRAMA> PostulacionesPublicadas()
        {
            var postulaciones = db.POSTULACION_PROGRAMA
                .Where(p => p.FK_COD_ESTADO == PUBLICADO
                && p.FK_COD_CEL == null);
            
            return postulaciones;
        }

        public void PostularPrograma(int codPostulacionPrograma, string usuario)
        {
            var programa = db.POSTULACION_PROGRAMA.Find(codPostulacionPrograma);
            decimal persona = db.USUARIO.Where(u => u.NOMBRE_USUARIO == usuario).FirstOrDefault().PERSONA.FirstOrDefault().COD_PERSONA;
            var cel = db.ENCARGADO_CEL.Where(e => e.FK_COD_PERSONA == persona).FirstOrDefault();
            if (cel != null)
                programa.FK_COD_CEL = cel.FK_COD_CEL;
            db.SaveChanges();
        }

        public bool Crear()
        {
            try
            {
                Conector.Entidades.CrearEncargadoCEL(1,8,nuevoCodigo());
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
                Conector.Entidades.ActualizarEncargadoCEL(1, 8, 2);
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
                Conector.Entidades.EliminarEncargadoCEL(2);
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

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
