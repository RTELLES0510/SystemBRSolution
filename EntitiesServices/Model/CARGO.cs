//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntitiesServices.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class CARGO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CARGO()
        {
            this.COLABORADOR = new HashSet<COLABORADOR>();
        }
    
        public int CARG_CD_ID { get; set; }
        public int ASSI_CD_ID { get; set; }
        public Nullable<int> VACO_CD_ID { get; set; }
        public string CARG_NM_NOME { get; set; }
        public int CARG_IN_COMISSAO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COLABORADOR> COLABORADOR { get; set; }
        public virtual ASSINANTE ASSINANTE { get; set; }
        public virtual VALOR_COMISSAO VALOR_COMISSAO { get; set; }
    }
}
