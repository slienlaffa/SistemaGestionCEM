using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SistemaGestionCEM.Datos;

namespace SistemaGestionCEM.Negocio
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IAutentificacion
    {
        [OperationContract]
        string Login(String usuario, String contraseña);

        [OperationContract]
        USUARIO Registro(USUARIO usuario);

        // TODO: agregue aquí sus operaciones de servicio
    }
}
