using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionCEM.Biblioteca
{
    public class Usuario
    {
        private int cod_usuario;
        private string nombre_usuario;
        private string contrasenna;
        private int cod_tipo;

        public int Cod_usuario { get => cod_usuario; set => cod_usuario = value; }
        public string Nombre_usuario { get => nombre_usuario; set => nombre_usuario = value; }
        public string Contrasenna { get => contrasenna; set => contrasenna = value; }
        public int Cod_tipo { get => cod_tipo; set => cod_tipo = value; }

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
            cod_usuario = 0;
            nombre_usuario = string.Empty;
            contrasenna = string.Empty;
            cod_tipo = 0;
        }
    }
}
