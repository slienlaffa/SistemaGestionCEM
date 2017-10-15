using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaGestionCEM.Datos;

namespace SistemeGestionCEM.Negocio
{
    public class Class1
    {
        public void ejemplo()
        {
            using (Entities context = new Entities())
            {
                context.Database.SqlQuery<GENERO>("exec genero_crud.crear @p_cod_genero @p_descripcion", "1", "Macho");
            }
            
        }

    }
}
