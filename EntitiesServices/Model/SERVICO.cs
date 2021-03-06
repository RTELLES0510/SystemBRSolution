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
    
    public partial class SERVICO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SERVICO()
        {
            this.ITEM_PEDIDO_SERVICO = new HashSet<ITEM_PEDIDO_SERVICO>();
            this.ITEM_PROPOSTA_SERVICO = new HashSet<ITEM_PROPOSTA_SERVICO>();
            this.PRECO_SERVICO = new HashSet<PRECO_SERVICO>();
            this.SERVICO_ANEXO = new HashSet<SERVICO_ANEXO>();
        }
    
        public int SERV_CD_ID { get; set; }
        public int ASSI_CD_ID { get; set; }
        public Nullable<int> MATR_CD_ID { get; set; }
        public Nullable<int> FILI_CD_ID { get; set; }
        public Nullable<int> CASE_CD_ID { get; set; }
        public string SERV_NM_NOME { get; set; }
        public string SERV_DS_DESCRICAO { get; set; }
        public System.DateTime SERV_DT_CADASTRO { get; set; }
        public int SERV_IN_ATIVO { get; set; }
    
        public virtual ASSINANTE ASSINANTE { get; set; }
        public virtual CATEGORIA_SERVICO CATEGORIA_SERVICO { get; set; }
        public virtual FILIAL FILIAL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ITEM_PEDIDO_SERVICO> ITEM_PEDIDO_SERVICO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ITEM_PROPOSTA_SERVICO> ITEM_PROPOSTA_SERVICO { get; set; }
        public virtual MATRIZ MATRIZ { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRECO_SERVICO> PRECO_SERVICO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SERVICO_ANEXO> SERVICO_ANEXO { get; set; }
    }
}
