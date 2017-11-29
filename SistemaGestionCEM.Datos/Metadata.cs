using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SistemaGestionCEM.Datos
{
    public class PersonaMetadata
    {
        [Required]
        [DisplayName("Nombre")]
        public string NOMBRE { get; set; }

        [Required]
        [DisplayName("Apellido")]
        public string APELLIDO { get; set; }

        [Required]
        [Display(Name = "Correo Electrónico")]
        [EmailAddress(ErrorMessage = "Ingrese un correo válido")]
        public string CORREO { get; set; }

        [Required]
        [DisplayName("Teléfono")]
        public decimal TELEFONO { get; set; }

        [Required]
        [DisplayName("Nacionalidad")]
        public string NACIONALIDAD { get; set; }

        [Required]
        [DisplayName("Ciudad")]
        public decimal FK_COD_CIUDAD { get; set; }

        [Required]
        [DisplayName("Genero")]
        public decimal FK_COD_GENERO { get; set; }
    }

    public class CiudadMetadata
    {
        [DisplayName("Ciudad")]
        public  decimal COD_CIUDAD { get; set; }
        [DisplayName("Ciudad")]
        public string DESCRIPCION { get; set; }
        [DisplayName("Pais")]
        public decimal FK_COD_PAIS { get; set; }
    }

    public class EncargadoCELMetadata
    {

        public decimal COD_ENCARGADOCEL { get; set; }
        [DisplayName("Persona")]
        public decimal FK_COD_PERSONA { get; set; }
        [DisplayName("Centro de Estudio Local")]
        public decimal FK_COD_CEL { get; set; }

        public virtual CENTRO_ESTUDIO_LOCAL CENTRO_ESTUDIO_LOCAL { get; set; }
        public virtual PERSONA PERSONA { get; set; }
    }

    public class FamiliaMetadata
    {
        [Required]
        [DisplayName("Cantidad de Integrantes")]
        [Range(1, int.MaxValue, ErrorMessage = "Ingrese un numero mayor a 0")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{00}")]
        public decimal NUM_INTEGRANTES { get; set; }

        [Required]
        [DisplayName("Cantidad de Habitaciones")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{00}")]
        [Range(1, int.MaxValue, ErrorMessage = "Ingrese un numero mayor a 0")]
        public decimal NUM_HABITACIONES { get; set; }

        [Required]
        [DisplayName("Cantidad de Baños")]
        [Range(1, int.MaxValue, ErrorMessage = "Ingrese un numero mayor a 0")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{00}")]
        public decimal NUM_BANOS { get; set; }

        [Required]
        [DisplayName("Tipo de Vivienda")]
        public string TIPO_VIVIENDA { get; set; }

        [Required]
        [DisplayName("¿Posee estacionamiento?")]
        public string ESTACIONAMIENTO { get; set; }

        [Required]
        [DisplayName("¿Posee Mascotas?")]
        public string MASCOTA_DESCRIPCION { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0000}")]
        [DisplayName("Año de inscripción")]
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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
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
        /*
        [RegularExpression(@"^ (?=\w *\d)(?=\w*[A-Z])(?=\w*[a - z])\S{8,16}$", 
            ErrorMessage = "La contraseña debe contener al menos 8 caracteres, al menos 1 Mayuscula y 1 digito.")  ]*/
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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#}")]
        public decimal NUM_MAX_ALUMNO { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Ingrese un numero mayor a 0")]
        [DisplayName("Numero minimo de alumnos")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#}")]
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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime FECHA { get; set; }
    }

    public class TipoUsuarioMetadata
    {
        [DisplayName("Tipo de Usuario")]
        public string COD_TIPO { get; set; }
        [DisplayName("Tipo")]
        public string DESCRIPCION { get; set; }
    }

    public class TipoProgramaMetadata
    {
        [DisplayName("Tipo de Programa")]
        public string DESCRIPCION { get; set; }
    }

    public class DetalleNotasMetadata
    {
        [DisplayName("Nota 1")]
        [Range(0, 10, ErrorMessage = "Ingrese un numero entre 0 y 10")]
        public Nullable<decimal> NOTA1 { get; set; }
        [DisplayName("Nota 2")]
        [Range(0, 10, ErrorMessage = "Ingrese un numero entre 0 y 10")]
        public Nullable<decimal> NOTA2 { get; set; }
        [DisplayName("Nota 3")]
        [Range(0, 10, ErrorMessage = "Ingrese un numero entre 0 y 10")]
        public Nullable<decimal> NOTA3 { get; set; }
        [DisplayName("Nota 4")]
        [Range(0, 10, ErrorMessage = "Ingrese un numero entre 0 y 10")]
        public Nullable<decimal> NOTA4 { get; set; }
        [DisplayName("Promedio")]
        public Nullable<decimal> PROMEDIO { get; set; }
    }
    public class CentroEstudioLocalMetadata
    {

        public decimal COD_CEL { get; set; }
        [Required]
        [DisplayName("Nombre centro")]
        public string NOMBRE_CENTRO { get; set; }
        [Required]
        [DisplayName("Nombre director")]
        public string NOM_DIRECTOR { get; set; }
        [Required]
        [DisplayName("Área de Especialización")]
        public string AREA_ESPECIALIZACION { get; set; }
        [Required]
        [DisplayName("Dirección")]
        [MaxLength(100, ErrorMessage = "No exceda los 100 caracteres")]
        public string DIRECCION { get; set; }
        [Required]
        [DisplayName("Correo")]
        [EmailAddress(ErrorMessage ="Debe ingresar un correo válido! (nombre@dominio.com)")]
        public string CORREO { get; set; }
        [Required]
        [DisplayName("Teléfono")] //Agregar rango
        public decimal TELEFONO { get; set; }
        [Required]
        [DisplayName("Descripcion del centro")]
        [MaxLength(100, ErrorMessage = "No exceda los 100 caracteres")]
        public string DESCRIPCION { get; set; }
        [Required]
        [DisplayName("Ciudad")]
        public decimal FK_COD_CIUDAD { get; set; }
    }

    public class PostulacionAlumnoMetadata
    {
        [DisplayName("Fecha de postulación")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime FECHA { get; set; }
    }
    public class AntecedentesMetadata
    {
        [DisplayName("Foto 1")]
        public string FOTO1 { get; set; }
        [DisplayName("Foto 2")]
        public string FOTO2 { get; set; }
        [DisplayName("Foto 3")]
        public string FOTO3 { get; set; }
        [DisplayName("Certificado residencia")]
        public string CERT_RESIDENCIA { get; set; }
        [DisplayName("Certificado antecedentes")]
        public string CERT_ANTECEDENTES { get; set; }
        [DisplayName("Certificado laboral")]
        public string CERT_LABORAL { get; set; }

    }
}
