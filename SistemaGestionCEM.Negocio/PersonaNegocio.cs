using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaGestionCEM.Negocio
{
    public class PersonaNegocio
    {
        public bool Crear(string apellido, string correo, int telefono, int codUsuario,
            string nacionalidad, int codCiudad, string nombre, int codGenero)
        {
            try
            {
                Conector.Entidades.CrearPersona(apellido, correo, nuevoCodigo(), telefono, codUsuario,
                    nacionalidad, codCiudad, nombre, codGenero);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(string apellido, string correo, int codPersona, int telefono, int codUsuario,
            string nacionalidad, int codCiudad, string nombre, int codGenero)
        {
            try
            {
                Conector.Entidades.ActualizarPersona(apellido, correo, codPersona, telefono, codUsuario,
                    nacionalidad, codCiudad, nombre, codGenero);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(int codPersona)
        {
            try
            {
                Conector.Entidades.EliminarPersona(codPersona);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int nuevoCodigo()
        {
            SistemaGestionCEM.Datos.PERSONA personaBusca = Conector.Entidades.PERSONA.OrderByDescending(e => e.COD_PERSONA).First();
            return (int)(personaBusca.COD_PERSONA + 1);
        }
    }
}
