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
    
    public partial class CENTRO_CUSTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CENTRO_CUSTO()
        {
            this.CENTRO_CUSTO1 = new HashSet<CENTRO_CUSTO>();
            this.CONTRATO = new HashSet<CONTRATO>();
        }
    
        public int CECU_CD_ID { get; set; }
        public int ASSI_CD_ID { get; set; }
        public string CECU_NR_NUMERO { get; set; }
        public string CECU_NM_NOME { get; set; }
        public Nullable<int> CECU_ID_PAI { get; set; }
        public int CECU_IN_ATIVO { get; set; }
    
        public virtual ASSINANTE ASSINANTE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CENTRO_CUSTO> CENTRO_CUSTO1 { get; set; }
        public virtual CENTRO_CUSTO CENTRO_CUSTO2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONTRATO> CONTRATO { get; set; }
    }
}
