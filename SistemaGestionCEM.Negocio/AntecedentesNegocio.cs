using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaGestionCEM.Negocio
{
    public class AntecedentesNegocio
    {
        public bool Crear(string foto3, string foto2, string foto1, string certLaboral, 
            string certResidencia, string certAntecedentes)
        {
            try
            {
                Conector.Entidades.CrearAntecedentes(foto3,foto2,foto1,certLaboral,
                    nuevoCodigo(),certResidencia,certAntecedentes);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(string foto3, string foto2, string foto1, string certLaboral,
            int codAntecedentes, string certResidencia, string certAntecedentes)
        {
            try
            {
                Conector.Entidades.ActualizarAntecedentes(foto3, foto2, foto1, certLaboral,
                    codAntecedentes, certResidencia, certAntecedentes);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(int codAntecedentes)
        {
            try
            {
                Conector.Entidades.EliminarAntecedentes(codAntecedentes);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int nuevoCodigo()
        {
            SistemaGestionCEM.Datos.ANTECEDENTES antecedentesBusca = Conector.Entidades.ANTECEDENTES.OrderByDescending(e => e.COD_ANTECEDENTES).First();
            return (int)(antecedentesBusca.COD_ANTECEDENTES + 1);
        }
    }
}
