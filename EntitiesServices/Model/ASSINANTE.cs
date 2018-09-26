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
    
    public partial class ASSINANTE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ASSINANTE()
        {
            this.ASSINANTE_PLANO = new HashSet<ASSINANTE_PLANO>();
            this.CARGO = new HashSet<CARGO>();
            this.CLIENTE = new HashSet<CLIENTE>();
            this.EQUIPAMENTO = new HashSet<EQUIPAMENTO>();
            this.FORNECEDOR = new HashSet<FORNECEDOR>();
            this.LOG = new HashSet<LOG>();
            this.MATERIA_PRIMA = new HashSet<MATERIA_PRIMA>();
            this.MATRIZ = new HashSet<MATRIZ>();
            this.MOVIMENTO_ESTOQUE_PRODUTO = new HashSet<MOVIMENTO_ESTOQUE_PRODUTO>();
            this.PATRIMONIO = new HashSet<PATRIMONIO>();
            this.PRODUTO = new HashSet<PRODUTO>();
            this.SERVICO = new HashSet<SERVICO>();
            this.TRANSPORTADORA = new HashSet<TRANSPORTADORA>();
            this.USUARIO = new HashSet<USUARIO>();
            this.VALOR_COMISSAO = new HashSet<VALOR_COMISSAO>();
            this.EQUIPAMENTO_MANUTENCAO = new HashSet<EQUIPAMENTO_MANUTENCAO>();
            this.FICHA_TECNICA = new HashSet<FICHA_TECNICA>();
            this.FICHA_TECNICA_DETALHE = new HashSet<FICHA_TECNICA_DETALHE>();
            this.FORMA_PAGAMENTO = new HashSet<FORMA_PAGAMENTO>();
            this.PERIODICIDADE = new HashSet<PERIODICIDADE>();
            this.SEGMENTO = new HashSet<SEGMENTO>();
        }
    
        public int ASSI_CD_ID { get; set; }
        public Nullable<int> CONF_CD_ID { get; set; }
        public string ASSI_NM_EMAIL { get; set; }
        public System.DateTime ASSI_DT_INICIO { get; set; }
        public string ASSI_NR_CPF { get; set; }
        public string ASSI_NM_EMPRESA { get; set; }
        public string ASSI_NM_RAZAO { get; set; }
        public string ASSI_NR_CNPJ { get; set; }
        public int ASSI_IN_ATIVO { get; set; }
    
        public virtual CONFIGURACAO CONFIGURACAO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ASSINANTE_PLANO> ASSINANTE_PLANO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CARGO> CARGO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLIENTE> CLIENTE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EQUIPAMENTO> EQUIPAMENTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FORNECEDOR> FORNECEDOR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LOG> LOG { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MATERIA_PRIMA> MATERIA_PRIMA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MATRIZ> MATRIZ { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MOVIMENTO_ESTOQUE_PRODUTO> MOVIMENTO_ESTOQUE_PRODUTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PATRIMONIO> PATRIMONIO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUTO> PRODUTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SERVICO> SERVICO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TRANSPORTADORA> TRANSPORTADORA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<USUARIO> USUARIO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VALOR_COMISSAO> VALOR_COMISSAO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EQUIPAMENTO_MANUTENCAO> EQUIPAMENTO_MANUTENCAO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FICHA_TECNICA> FICHA_TECNICA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FICHA_TECNICA_DETALHE> FICHA_TECNICA_DETALHE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FORMA_PAGAMENTO> FORMA_PAGAMENTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PERIODICIDADE> PERIODICIDADE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SEGMENTO> SEGMENTO { get; set; }
    }
}
