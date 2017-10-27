using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaGestionCEM.Negocio
{
    public class AlumnoNegocio
    {
        public bool Crear(int codPersona, DateTime fechaNacimiento)
        {
            try
            {
                Conector.Entidades.CrearAlumno(nuevoCodigo(),codPersona,fechaNacimiento);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(int codAlumno, int codPersona, DateTime fechaNacimiento)
        {
            try
            {
                Conector.Entidades.ActualizarAlumno(codAlumno,codPersona,fechaNacimiento);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(int codAlumno)
        {
            try
            {
                Conector.Entidades.EliminarAlumno(codAlumno);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string Mostrar()
        {
            List<SistemaGestionCEM.Datos.ALUMNO> listaAlumnos = Conector.Entidades.ALUMNO.ToList();
            //crear coleccionAlumnos para trabajar
            return listaAlumnos.Last().COD_ALUMNO.ToString();
        }

        public int nuevoCodigo()
        {
            SistemaGestionCEM.Datos.ALUMNO alumnoBusca = Conector.Entidades.ALUMNO.OrderByDescending(e => e.COD_ALUMNO).First();
            return (int)(alumnoBusca.COD_ALUMNO + 1);
        }
    }
}
