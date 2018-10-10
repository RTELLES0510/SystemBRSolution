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
    
    public partial class CONTRATO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CONTRATO()
        {
            this.CONTRATO_ANEXO = new HashSet<CONTRATO_ANEXO>();
        }
    
        public int CONT_CD_ID { get; set; }
        public int ASSI_CD_ID { get; set; }
        public int CLIE_CD_ID { get; set; }
        public Nullable<int> TICT_CD_ID { get; set; }
        public Nullable<int> CACT_CD_ID { get; set; }
        public Nullable<int> TEMP_CD_ID { get; set; }
        public Nullable<int> PERI_CD_ID { get; set; }
        public Nullable<int> FOPA_CD_ID { get; set; }
        public Nullable<int> PLCO_CD_ID { get; set; }
        public Nullable<int> CECU_CD_ID { get; set; }
        public Nullable<int> COLA_CD_ID { get; set; }
        public Nullable<int> NBSE_CD_ID { get; set; }
        public string CONT_NM_NOME { get; set; }
        public string CONT_DS_DESCRICAO { get; set; }
        public System.DateTime CONT_DT_INICIO { get; set; }
        public System.DateTime CONT_DT_FINAL { get; set; }
        public int CONT_IN_ATIVO { get; set; }
        public Nullable<decimal> CONT_VL_VALOR { get; set; }
        public Nullable<int> CONT_IN_STATUS { get; set; }
        public Nullable<int> CONT_NR_CARENCIA { get; set; }
        public Nullable<int> CONT_NR_DIA_PAGAMENTO { get; set; }
        public Nullable<int> CONT_IN_EMITE_NF { get; set; }
        public Nullable<System.DateTime> CONT_DT_ULTIMO_REAJUSTE { get; set; }
        public Nullable<System.DateTime> CONT_DT_PROXIMO_REAJUSTE { get; set; }
        public string CONT_DS_DESCRICAO_DETALHADA { get; set; }
        public Nullable<int> CONT_IN_SINCRONIZA { get; set; }
        public string CONT_TX_OBSERVACOES { get; set; }
        public Nullable<int> CONT_NR_PARCELAS { get; set; }
        public Nullable<decimal> CONT_PC_PERCENT_PARCELAS { get; set; }
        public string CONT_NM_NUMERO_CONTRATO { get; set; }
        public Nullable<int> CONT_IN_PERIODO_COBRANCA { get; set; }
        public Nullable<int> CONT_IN_ISS_RETIDO { get; set; }
        public Nullable<int> CONT_IN_IR_RETIDO { get; set; }
        public string CONT_DS_TEXTO_NF { get; set; }
    
        public virtual ASSINANTE ASSINANTE { get; set; }
        public virtual CATEGORIA_CONTRATO CATEGORIA_CONTRATO { get; set; }
        public virtual CENTRO_CUSTO CENTRO_CUSTO { get; set; }
        public virtual CLIENTE CLIENTE { get; set; }
        public virtual COLABORADOR COLABORADOR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONTRATO_ANEXO> CONTRATO_ANEXO { get; set; }
        public virtual FORMA_PAGAMENTO FORMA_PAGAMENTO { get; set; }
        public virtual NOMENCLATURA_BRAS_SERVICOS NOMENCLATURA_BRAS_SERVICOS { get; set; }
        public virtual PERIODICIDADE PERIODICIDADE { get; set; }
        public virtual PLANO_CONTA PLANO_CONTA { get; set; }
        public virtual TEMPLATE TEMPLATE { get; set; }
        public virtual TIPO_CONTRATO TIPO_CONTRATO { get; set; }
    }
}
