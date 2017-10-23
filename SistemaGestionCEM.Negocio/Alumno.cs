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
    }
}
