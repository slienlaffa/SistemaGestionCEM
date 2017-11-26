using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace SistemaGestionCEM.Negocio
{
    public class FamiliaAnfitrionaNegocio : Negocio
    {
        public bool Crear(int numBanos, int codAntecedentes, int anioInscripcion, int numHabitaciones,
             string tipoVivienda, int numIntegrantes,int codPersona, string estacionamiento, string mascotaDescripcion)
        {
            try
            {
                Conector.Entidades.CrearFamilia(numBanos, codAntecedentes, nuevoCodigo(), anioInscripcion, numHabitaciones,
                    tipoVivienda, numIntegrantes, codPersona, estacionamiento, mascotaDescripcion);
                    
                return true;
            }
            catch
            {
                return false;
            }
        }
  

        public bool Actualizar(int numBanos, int codAntecedentes, int codFamilia, int anioInscripcion,
            int numHabitaciones, string tipoVivienda, int numIntegrantes, int codPersona,
            string estacionamiento, string mascotaDescripcion)
        {
            try
            {
                Conector.Entidades.ActualizarFamilia(numBanos, codAntecedentes, codFamilia, anioInscripcion,
                    numHabitaciones, tipoVivienda, numIntegrantes, codPersona, estacionamiento,
                    mascotaDescripcion);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(int codFamilia)
        {
         
            try
            {
                Conector.Entidades.EliminarFamilia(codFamilia);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int nuevoCodigo()
        {
            SistemaGestionCEM.Datos.FAMILIA_ANFITRIONA familiaBusca = Conector.Entidades.FAMILIA_ANFITRIONA.OrderByDescending(e => e.COD_FAMILIA).First();
            return (int)(familiaBusca.COD_FAMILIA + 1);
        }
    }
}
