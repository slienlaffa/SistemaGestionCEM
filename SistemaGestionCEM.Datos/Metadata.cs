using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SistemaGestionCEM.Datos
{
    public class PersonaMetadata
    {
        [Required(ErrorMessage = "El campo Nombre es obligatorio.", AllowEmptyStrings = false)]
        [DisplayName("Nombre")]
        public string NOMBRE { get; set; }

        [Required(ErrorMessage = "El campo Apellido es obligatorio.", AllowEmptyStrings = false)]
        [DisplayName("Apellido")]
        public string APELLIDO { get; set; }

        [Required(ErrorMessage = "Ingrese un correo", AllowEmptyStrings = false)]
        [Display(Name = "Correo Electrónico", Description = "nombre@dominio.com")]
        [EmailAddress(ErrorMessage = "Ingrese un correo válido")]
        public string CORREO { get; set; }

        [Required]
        [DisplayName("Teléfono")]
        public decimal TELEFONO { get; set; }

        [Required]
        public string NACIONALIDAD { get; set; }

        [DisplayName("Ciudad")]
        public decimal FK_COD_CIUDAD { get; set; }
        [DisplayName("Genero")]
        public decimal FK_COD_GENERO { get; set; }
    }

    public class FamiliaMetadata
    {
        [Required]
        [DisplayName("Cantidad de Integrantes")]
        public decimal NUM_INTEGRANTES { get; set; }

        [Required]
        [DisplayName("Cantidad de Habitaciones")]
        public decimal NUM_HABITACIONES { get; set; }

        [Required]
        [DisplayName("Cantidad de Baños")]
        public decimal NUM_BANOS { get; set; }

        [Required]
        [DisplayName("Tipo de Vivienda")]
        public string TIPO_VIVIENDA { get; set; }

        [Required]
        [DisplayName("¿Posee estacionamiento?")]
        public string ESTACIONAMIENTO { get; set; }

        [Required]
        [DisplayName("¿Posee Mascotas? Sí la respuesta es si, especifique.")]
        public string MASCOTA_DESCRIPCION { get; set; }

        public decimal ANIO_INSCRIPCION { get; set; }


        public decimal FK_COD_PERSONA { get; set; }
        public decimal FK_COD_ANTECEDENTES { get; set; }

        public virtual ANTECEDENTES ANTECEDENTES { get; set; }

        public virtual PERSONA PERSONA { get; set; }
    }

    public class AlumnoMetadata
    {
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria", AllowEmptyStrings = false)]
        [DisplayName("Fecha de Nacimiento")]
        public System.DateTime FECHA_NACIMIENTO { get; set; }
    }

    public class UsuarioMetadata
    {
        [Required(ErrorMessage = "Ingrese su nombre de usuario", AllowEmptyStrings = false)]
        [DisplayName("Nombre de usuario")]
        [MaxLength(20, ErrorMessage = "No exceda los 20 caracteres"),]
        public string NOMBRE_USUARIO { get; set; }

        [Required(ErrorMessage = "Ingrese su contraseña", AllowEmptyStrings = false)]
        [DisplayName("Contraseña")]
        [DataType(DataType.Password)]
        [MaxLength(20, ErrorMessage = "No exceda los 20 caracteres"),]
        public string CONTRASENNA { get; set; }
    }

    public class ProgramaEstudioMetadata
    {
        [Required]
        [DisplayName("Nombre programa")]
        public string NOMBRE_PROGRAMA { get; set; }
        [Required]
        [DisplayName("Descripcion")]
        public string DESCRIPCION { get; set; }
        [Required(ErrorMessage = "La fecha limite de postulación es obligatoria", AllowEmptyStrings = false)]
        [DisplayName("Fecha limite de postulación")]
        public System.DateTime FECHA_LIMITE_POSTULACION { get; set; }
        [Required]
        [DisplayName("Area de conocimiento")]
        public string AREA_CONOCIMIENTO { get; set; }
        [Required]
        [DisplayName("Escriba la duración del programa de estudio")]
        public string DURACION { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Ingrese un numero mayor a 0")]
        [DisplayName("Numero maximo de alumnos")]
        public decimal NUM_MAX_ALUMNO { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Ingrese un numero mayor a 0")]
        [DisplayName("Numero minimo de alumnos")]
        public decimal NUM_MIN_ALUMNO { get; set; }
        [Required]
        [DisplayName("Tipo de Programa")]
        public decimal FK_COD_TIPOPROGRAMA { get; set; }
        [Required]
        [DisplayName("Pais")]
        public decimal FK_COD_PAIS { get; set; }
    }

    public class PostulacionProgramaMetadata
    {
        [Required]
        [DisplayName("Fecha publicación")]
        public System.DateTime FECHA { get; set; }
    }
}
