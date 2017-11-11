using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaGestionCEM.Datos
{
    [MetadataType(typeof(PersonaMetadata))]
    public partial class PERSONA
    {
    }

    [MetadataType(typeof(UsuarioMetadata))]
    public partial class USUARIO
    {
    }

    [MetadataType(typeof(FamiliaMetadata))]
    public partial class FAMILIA_ANFITRIONA
    {
    }

    [MetadataType(typeof(AlumnoMetadata))]
    public partial class ALUMNO
    {
    }

    [MetadataType(typeof(ProgramaEstudioMetadata))]
    public partial class PROGRAMA_ESTUDIO
    {
    }
}
