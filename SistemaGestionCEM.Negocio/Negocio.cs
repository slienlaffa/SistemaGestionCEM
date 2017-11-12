using SistemaGestionCEM.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionCEM.Negocio
{
    public class Negocio : IDisposable
    {
        protected Entities db = new Entities();

        protected const int PENDIENTE = 1;
        protected const int APROBADO = 2;
        protected const int RECHAZADO = 3;
        protected const int NO_PUBLICADO = 4;
        protected const int PUBLICADO = 5;
        protected const int FINALIZADO = 6;
        protected const int CANCELADO = 7;
        protected const int EN_CURSO = 8;

        public IQueryable<DETALLE_NOTAS> BuscarNotas(int codigoPrograma)
        {
            var notas = db.DETALLE_NOTAS
                .Where(d => d.DETALLE_ALUMNO.FK_COD_PROGRAMA == codigoPrograma);

            return notas;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
