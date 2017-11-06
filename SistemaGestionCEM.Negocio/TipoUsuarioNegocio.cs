using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaGestionCEM.Negocio
{
    public class TipoUsuarioNegocio
    {
        public int nuevoCodigo()
        {
            SistemaGestionCEM.Datos.TIPO_USUARIO tipoUsuario = Conector.Entidades.TIPO_USUARIO.OrderByDescending(e => e.COD_TIPO).First();
            return (int)(tipoUsuario.COD_TIPO + 1);
        }
    }
}
