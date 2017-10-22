using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;
using SistemaGestionCEM.Datos;

namespace SistemaGestionCEM.Negocio
{
    public class Alumno : IAlumno
    {
        public List<PROGRAMA_ESTUDIO> ListarProgramasPublicados()
        {
            return new List<PROGRAMA_ESTUDIO>();
        }
    }
}
