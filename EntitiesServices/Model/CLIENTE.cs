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
    
    public partial class CLIENTE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CLIENTE()
        {
            this.CLIENTE_ANEXO = new HashSet<CLIENTE_ANEXO>();
            this.CONTA_RECEBER = new HashSet<CONTA_RECEBER>();
            this.CONTRATO = new HashSet<CONTRATO>();
            this.OPORTUNIDADE_NEGOCIO = new HashSet<OPORTUNIDADE_NEGOCIO>();
            this.PEDIDO_SERVICO = new HashSet<PEDIDO_SERVICO>();
            this.PEDIDO_VENDA = new HashSet<PEDIDO_VENDA>();
            this.PROPOSTA_SERVICO = new HashSet<PROPOSTA_SERVICO>();
            this.PROPOSTA_VENDA = new HashSet<PROPOSTA_VENDA>();
            this.TICKET_ATENDIMENTO = new HashSet<TICKET_ATENDIMENTO>();
        }
    
        public int CLIE_CD_ID { get; set; }
        public int ASSI_CD_ID { get; set; }
        public Nullable<int> MATR_CD_ID { get; set; }
        public Nullable<int> FILI_CD_ID { get; set; }
        public Nullable<int> CACL_CD_ID { get; set; }
        public string CLIE_NM_NOME { get; set; }
        public string CLIE_NM_RAZAO { get; set; }
        public Nullable<int> CLIE_IN_TIPO_PESSOA { get; set; }
        public string CLIE_NR_CPF { get; set; }
        public string CLIE_NR_CNPJ { get; set; }
        public string CLIE_NM_EMAIL { get; set; }
        public string CLIE_NR_TELEFONES { get; set; }
        public string CLIE_NM_REDES_SOCIAIS { get; set; }
        public string CLIE_NM_ENDERECO { get; set; }
        public string CLIE_NM_BAIRRO { get; set; }
        public string CLIE_NM_CIDADE { get; set; }
        public string CLIE_SG_UF { get; set; }
        public string CLIE_NR_CEP { get; set; }
        public System.DateTime CLIE_DT_CADASTRO { get; set; }
        public int CLIE_IN_ATIVO { get; set; }
        public string CLIE_AQ_FOTO { get; set; }
    
        public virtual ASSINANTE ASSINANTE { get; set; }
        public virtual CATEGORIA_CLIENTE CATEGORIA_CLIENTE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLIENTE_ANEXO> CLIENTE_ANEXO { get; set; }
        public virtual FILIAL FILIAL { get; set; }
        public virtual MATRIZ MATRIZ { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONTA_RECEBER> CONTA_RECEBER { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONTRATO> CONTRATO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OPORTUNIDADE_NEGOCIO> OPORTUNIDADE_NEGOCIO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PEDIDO_SERVICO> PEDIDO_SERVICO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PEDIDO_VENDA> PEDIDO_VENDA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PROPOSTA_SERVICO> PROPOSTA_SERVICO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PROPOSTA_VENDA> PROPOSTA_VENDA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TICKET_ATENDIMENTO> TICKET_ATENDIMENTO { get; set; }
    }
}
