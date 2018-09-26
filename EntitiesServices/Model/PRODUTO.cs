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
    
    public partial class PRODUTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRODUTO()
        {
            this.INVENTARIO_ITEM = new HashSet<INVENTARIO_ITEM>();
            this.ITEM_PEDIDO_COMPRA = new HashSet<ITEM_PEDIDO_COMPRA>();
            this.ITEM_PEDIDO_VENDA = new HashSet<ITEM_PEDIDO_VENDA>();
            this.ITEM_PROPOSTA_VENDA = new HashSet<ITEM_PROPOSTA_VENDA>();
            this.MOVIMENTO_ESTOQUE_PRODUTO = new HashSet<MOVIMENTO_ESTOQUE_PRODUTO>();
            this.PRECO_PRODUTO = new HashSet<PRECO_PRODUTO>();
            this.PRODUTO_ANEXO = new HashSet<PRODUTO_ANEXO>();
            this.FICHA_TECNICA = new HashSet<FICHA_TECNICA>();
        }
    
        public int PROD_CD_ID { get; set; }
        public int ASSI_CD_ID { get; set; }
        public Nullable<int> MATR_CD_ID { get; set; }
        public Nullable<int> FILI_CD_ID { get; set; }
        public Nullable<int> CAPR_CD_ID { get; set; }
        public Nullable<int> UNID_CD_ID { get; set; }
        public string PROD_NM_NOME { get; set; }
        public string PROD_DS_DESCRICAO { get; set; }
        public int PROD_QN_QUANTIDADE_MINIMA { get; set; }
        public int PROD_QN_QUANTIDADE_INICIAL { get; set; }
        public int PROD_QN_ESTOQUE { get; set; }
        public Nullable<System.DateTime> PROD_DT_ULTIMA_MOVIMENTACAO { get; set; }
        public int PROD_IN_AVISA_MINIMO { get; set; }
        public System.DateTime PROD_DT_CADASTRO { get; set; }
        public int PROD_IN_ATIVO { get; set; }
        public string PROD_AQ_FOTO { get; set; }
    
        public virtual ASSINANTE ASSINANTE { get; set; }
        public virtual CATEGORIA_PRODUTO CATEGORIA_PRODUTO { get; set; }
        public virtual FILIAL FILIAL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INVENTARIO_ITEM> INVENTARIO_ITEM { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ITEM_PEDIDO_COMPRA> ITEM_PEDIDO_COMPRA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ITEM_PEDIDO_VENDA> ITEM_PEDIDO_VENDA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ITEM_PROPOSTA_VENDA> ITEM_PROPOSTA_VENDA { get; set; }
        public virtual MATRIZ MATRIZ { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MOVIMENTO_ESTOQUE_PRODUTO> MOVIMENTO_ESTOQUE_PRODUTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRECO_PRODUTO> PRECO_PRODUTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUTO_ANEXO> PRODUTO_ANEXO { get; set; }
        public virtual UNIDADE UNIDADE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FICHA_TECNICA> FICHA_TECNICA { get; set; }
    }
}
