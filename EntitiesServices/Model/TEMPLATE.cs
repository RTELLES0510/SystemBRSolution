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
    
    public partial class TEMPLATE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TEMPLATE()
        {
            this.CONTRATO = new HashSet<CONTRATO>();
        }
    
        public int TEMP_CD_ID { get; set; }
        public int ASSI_CD_ID { get; set; }
        public string TEMP_SG_SIGLA { get; set; }
        public string TEMP_NM_NOME { get; set; }
        public string TEMP_TX_CONTEUDO { get; set; }
        public string TEMP_AQ_ARQUIVO { get; set; }
        public int TEMP_IN_ATIVO { get; set; }
    
        public virtual ASSINANTE ASSINANTE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONTRATO> CONTRATO { get; set; }
    }
}
