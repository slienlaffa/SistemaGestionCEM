using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaGestionCEM.Datos;


namespace SistemaGestionCEM.Negocio
{
    public class Conector
    {
        private static Entities _entidades;
        public static Entities Entidades
        {
            get
            {
                if (_entidades == null)
                {
                    _entidades = new Entities();
                }
                return _entidades;
            }
        }

        public Conector()
        {
        }
    }
}
