using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionCEM.Biblioteca
{
    public class Usuario
    {
        public int Cod_usuario { get; set; }
        public string Nombre_usuario { get; set; }
        public string Contrasenna { get; set; }
        public int Cod_tipo { get; set; }

        public Usuario(int cod_usuario, string nombre_usuario, string contrasenna, int cod_tipo)
        {
            this.Cod_usuario = cod_usuario;
            this.Nombre_usuario = nombre_usuario;
            this.Contrasenna = contrasenna;
            this.Cod_tipo = cod_tipo;
        }

        public Usuario()
        {
            Init();
        }

        public void Init()
        {
            Cod_usuario = 0;
            Nombre_usuario = string.Empty;
            Contrasenna = string.Empty;
            Cod_tipo = 0;
        }
    }
}
