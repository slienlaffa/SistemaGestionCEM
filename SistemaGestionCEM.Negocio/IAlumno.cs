using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using SistemaGestionCEM.Datos;

namespace SistemaGestionCEM.Negocio
{
    [ServiceContract]
    public interface IAlumno
    {
        [OperationContract]
        List<PROGRAMA_ESTUDIO> ListarProgramasPublicados();
    }

}
