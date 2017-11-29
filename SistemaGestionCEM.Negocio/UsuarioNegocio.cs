using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaGestionCEM.Biblioteca;

namespace SistemaGestionCEM.Negocio
{
    public class UsuarioNegocio
    {
        private Usuario usuario;

        public Usuario Usuario { get; set; }

        public bool Crear(string contrasena,string nombreUsuario, int codTipo)
        {
            usuario = new Usuario(nuevoCodigo(), nombreUsuario, contrasena, codTipo);
            try
            {
                Conector.Entidades.CrearUsuario(usuario.Contrasenna, usuario.Cod_usuario, usuario.Nombre_usuario, usuario.Cod_tipo);
                return true;
            }
            catch {
                return false;
            }
        }

        public bool Actualizar(string contrasena, int codUsuario, string nombreUsuario, int codTipo)
        {
            usuario = new Usuario(codUsuario, nombreUsuario, contrasena, codTipo);
            try
            {
                Conector.Entidades.ActualizarUsuario(usuario.Contrasenna, usuario.Cod_usuario, usuario.Nombre_usuario, usuario.Cod_tipo);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(int codUsuario)
        {
          
            try
            {
                Conector.Entidades.EliminarUsuario(codUsuario);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string Mostrar()
        {
            List<SistemaGestionCEM.Datos.USUARIO> listaUsuarios = Conector.Entidades.USUARIO.ToList();
            //crear coleccionAlumnos para trabajar
            return listaUsuarios.Last().COD_USUARIO.ToString();
        }

        public bool valida(string nombreUsuario, string password)
        {
            try
            {
                SistemaGestionCEM.Datos.USUARIO usuarioBusca = Conector.Entidades.USUARIO.First(p => p.NOMBRE_USUARIO.Equals(nombreUsuario) && p.CONTRASENNA.Equals(password));
                if (usuarioBusca.NOMBRE_USUARIO.Equals(nombreUsuario))
                {
                    usuario = new Usuario((int)usuarioBusca.COD_USUARIO, usuarioBusca.NOMBRE_USUARIO,
                                            usuarioBusca.CONTRASENNA, (int)usuarioBusca.FK_COD_TIPO);
                    return true;
                }
                else
                    return false;
            }
            catch {
                return false;
            }
        }

        public void obtineUsuario(string nombreUsuario, string password)
        {
            SistemaGestionCEM.Datos.USUARIO usuarioBusca = Conector.Entidades.USUARIO.First(p => p.NOMBRE_USUARIO.Equals(nombreUsuario) && p.CONTRASENNA.Equals(password));
            usuario = new Usuario((int)usuarioBusca.COD_USUARIO, usuarioBusca.NOMBRE_USUARIO,
                usuarioBusca.CONTRASENNA, (int)usuarioBusca.FK_COD_TIPO);
        }

        public int nuevoCodigo()
        {
            SistemaGestionCEM.Datos.USUARIO usuarioBusca = Conector.Entidades.USUARIO.OrderByDescending(e => e.COD_USUARIO).First();
            return (int)(usuarioBusca.COD_USUARIO + 1);
        }
    }
}
