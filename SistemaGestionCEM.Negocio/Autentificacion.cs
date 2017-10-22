using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SistemaGestionCEM.Datos;

namespace SistemaGestionCEM.Negocio
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código y en el archivo de configuración a la vez.
    public class Autentificacion : IAutentificacion
    {
        public string Login(String usuario, String contraseña)
        {
            return string.Format("You entered: {0}", usuario);
        }

        public USUARIO Registro(USUARIO usuario)
        {            
            return usuario;
        }
    }
}
