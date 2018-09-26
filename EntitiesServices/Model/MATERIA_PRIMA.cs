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
    
    public partial class MATERIA_PRIMA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MATERIA_PRIMA()
        {
            this.INVENTARIO_ITEM = new HashSet<INVENTARIO_ITEM>();
            this.ITEM_PEDIDO_COMPRA = new HashSet<ITEM_PEDIDO_COMPRA>();
            this.MATERIA_PRIMA_ANEXO = new HashSet<MATERIA_PRIMA_ANEXO>();
            this.MOVIMENTO_ESTOQUE_MATERIA_PRIMA = new HashSet<MOVIMENTO_ESTOQUE_MATERIA_PRIMA>();
            this.FICHA_TECNICA_DETALHE = new HashSet<FICHA_TECNICA_DETALHE>();
        }
    
        public int MAPR_CD_ID { get; set; }
        public int ASSI_CD_ID { get; set; }
        public Nullable<int> MATR_CD_ID { get; set; }
        public Nullable<int> FILI_CD_ID { get; set; }
        public Nullable<int> CAMA_CD_ID { get; set; }
        public Nullable<int> UNID_CD_ID { get; set; }
        public string MAPR_NM_NOME { get; set; }
        public string MAPR_DS_DESCRICAO { get; set; }
        public int MAPR_QN_QUANTIDADE_M { get; set; }
        public int MAPR_QN_QUANTIDADE_INICIAL { get; set; }
        public int MAPR_QN_ESTOQUE { get; set; }
        public Nullable<System.DateTime> MAPR_DT_ULTIMA_MOVIMENTACAO { get; set; }
        public int MAPR_IN_AVISA_MINIMO { get; set; }
        public System.DateTime MAPR_DT_CADASTRO { get; set; }
        public int MAPR_IN_ATIVO { get; set; }
    
        public virtual ASSINANTE ASSINANTE { get; set; }
        public virtual CATEGORIA_MATERIA CATEGORIA_MATERIA { get; set; }
        public virtual FILIAL FILIAL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INVENTARIO_ITEM> INVENTARIO_ITEM { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ITEM_PEDIDO_COMPRA> ITEM_PEDIDO_COMPRA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MATERIA_PRIMA_ANEXO> MATERIA_PRIMA_ANEXO { get; set; }
        public virtual MATRIZ MATRIZ { get; set; }
        public virtual UNIDADE UNIDADE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MOVIMENTO_ESTOQUE_MATERIA_PRIMA> MOVIMENTO_ESTOQUE_MATERIA_PRIMA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FICHA_TECNICA_DETALHE> FICHA_TECNICA_DETALHE { get; set; }
    }
}
