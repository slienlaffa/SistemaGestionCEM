//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SistemaGestionCEM.Datos
{
    using System;
    using System.Collections.Generic;
    
    public partial class CIUDAD
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CIUDAD()
        {
            this.CENTRO_ESTUDIO_LOCAL = new HashSet<CENTRO_ESTUDIO_LOCAL>();
            this.PERSONA = new HashSet<PERSONA>();
        }
    
        public decimal COD_CIUDAD { get; set; }
        public string DESCRIPCION { get; set; }
        public decimal FK_COD_PAIS { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CENTRO_ESTUDIO_LOCAL> CENTRO_ESTUDIO_LOCAL { get; set; }
        public virtual PAIS PAIS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PERSONA> PERSONA { get; set; }
    }
}
