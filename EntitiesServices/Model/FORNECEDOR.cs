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
    
    public partial class FORNECEDOR
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FORNECEDOR()
        {
            this.CONTA_PAGAR = new HashSet<CONTA_PAGAR>();
            this.ITEM_PEDIDO_COMPRA_COTACAO = new HashSet<ITEM_PEDIDO_COMPRA_COTACAO>();
            this.PEDIDO_COMPRA = new HashSet<PEDIDO_COMPRA>();
        }
    
        public int FORN_CD_ID { get; set; }
        public int ASSI_CD_ID { get; set; }
        public Nullable<int> MATR_CD_ID { get; set; }
        public Nullable<int> FILI_CD_ID { get; set; }
        public Nullable<int> CAFO_CD_ID { get; set; }
        public string FORN_NM_NOME { get; set; }
        public string FORN_NM_RAZAO { get; set; }
        public int FORN_IN_TIPO_PESSOA { get; set; }
        public string FORN_NR_CPF { get; set; }
        public string FORN_NR_CNPJ { get; set; }
        public string FORN_NM_EMAIL { get; set; }
        public string FORN_NM_TELEFONES { get; set; }
        public string FORN_NM_REDES_SOCIAIS { get; set; }
        public string FORN_NM_ENDERECO { get; set; }
        public string FORN_NM_BAIRRO { get; set; }
        public string FORN_NM_CIDADE { get; set; }
        public string FORN_SG_UF { get; set; }
        public string FORN_NR_CEP { get; set; }
        public System.DateTime FORN_DT_CADASTRO { get; set; }
        public int FORN_IN_ATIVO { get; set; }
    
        public virtual CATEGORIA_FORNECEDOR CATEGORIA_FORNECEDOR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONTA_PAGAR> CONTA_PAGAR { get; set; }
        public virtual FILIAL FILIAL { get; set; }
        public virtual MATRIZ MATRIZ { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ITEM_PEDIDO_COMPRA_COTACAO> ITEM_PEDIDO_COMPRA_COTACAO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PEDIDO_COMPRA> PEDIDO_COMPRA { get; set; }
        public virtual ASSINANTE ASSINANTE { get; set; }
    }
}
