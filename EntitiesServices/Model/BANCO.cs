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
    
    public partial class BANCO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BANCO()
        {
            this.CONTA_BANCARIA = new HashSet<CONTA_BANCARIA>();
        }
    
        public int BANC_CD_ID { get; set; }
        public int ASSI_CD_ID { get; set; }
        public string BANC_SG_CODIGO { get; set; }
        public string BANC_NM_NOME { get; set; }
        public int BANCO_IN_ATIVO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONTA_BANCARIA> CONTA_BANCARIA { get; set; }
    }
}
