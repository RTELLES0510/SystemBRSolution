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
    
    public partial class PATRIMONIO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PATRIMONIO()
        {
            this.PATRIMONIO_ANEXO = new HashSet<PATRIMONIO_ANEXO>();
        }
    
        public int PATR_CD_ID { get; set; }
        public int ASSI_CD_ID { get; set; }
        public Nullable<int> MATR_CD_ID { get; set; }
        public Nullable<int> FILI_CD_ID { get; set; }
        public Nullable<int> CAPA_CD_ID { get; set; }
        public string PATR_NR_NUMERO_PATRIMONIO { get; set; }
        public string PATR_NM_NOME { get; set; }
        public string PATR_DS_DESCRICAO { get; set; }
        public Nullable<System.DateTime> PATR_DT_COMPRA { get; set; }
        public Nullable<decimal> PATR_VL_VALOR { get; set; }
        public Nullable<int> PATR_NR_VIDA_UTIL { get; set; }
        public Nullable<System.DateTime> PATR_DT_BAIXA { get; set; }
        public string PATR_DS_MOTIVO_BAIXA { get; set; }
        public int PATR_IN_ATIVO { get; set; }
        public System.DateTime PATR_DT_CADASTRO { get; set; }
        public string PATR_AQ_FOTO { get; set; }
        public string PATR_TX_OBSERVACOES { get; set; }
    
        public virtual ASSINANTE ASSINANTE { get; set; }
        public virtual CATEGORIA_PATRIMONIO CATEGORIA_PATRIMONIO { get; set; }
        public virtual FILIAL FILIAL { get; set; }
        public virtual MATRIZ MATRIZ { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PATRIMONIO_ANEXO> PATRIMONIO_ANEXO { get; set; }
    }
}
