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
    
    public partial class USUARIO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USUARIO()
        {
            this.PERSONA = new HashSet<PERSONA>();
        }
    
        public decimal COD_USUARIO { get; set; }
        public string NOMBRE_USUARIO { get; set; }
        public string CONTRASENNA { get; set; }
        public decimal FK_COD_TIPO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PERSONA> PERSONA { get; set; }
        public virtual TIPO_USUARIO TIPO_USUARIO { get; set; }
    }
}
