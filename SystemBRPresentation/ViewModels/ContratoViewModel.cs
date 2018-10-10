using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EntitiesServices.Model;
using EntitiesServices.Attributes;

namespace SystemBRPresentation.ViewModels
{
    public class ContratoViewModel
    {
        [Key]
        public int CONT_CD_ID { get; set; }
        [Required(ErrorMessage = "Campo ASSINANTE obrigatorio")]
        public int ASSI_CD_ID { get; set; }
        [Required(ErrorMessage = "Campo CLIENTE obrigatorio")]
        public int CLIE_CD_ID { get; set; }
        public Nullable<int> TICT_CD_ID { get; set; }
        public Nullable<int> CACT_CD_ID { get; set; }
        public Nullable<int> TEMP_CD_ID { get; set; }
        public Nullable<int> PERI_CD_ID { get; set; }
        public Nullable<int> FOPA_CD_ID { get; set; }
        public Nullable<int> PLCO_CD_ID { get; set; }
        public Nullable<int> CECU_CD_ID { get; set; }
        public Nullable<int> COLA_CD_ID { get; set; }
        [Required(ErrorMessage = "Campo NOME obrigatorio")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "O NOME deve conter no minimo 1 caracteres e no máximo 50.")]
        public string CONT_NM_NOME { get; set; }
        [StringLength(1000, ErrorMessage = "A DESCRIÇÃO deve conter no máximo 1000.")]
        public string CONT_DS_DESCRICAO { get; set; }
        [Required(ErrorMessage = "Campo DATA DE INÍCIO obrigatorio")]
        [DataType(DataType.Date, ErrorMessage = "Deve ser uma data válida")]
        public System.DateTime CONT_DT_INICIO { get; set; }
        [Required(ErrorMessage = "Campo DATA DE FINAL obrigatorio")]
        [DataType(DataType.Date, ErrorMessage = "Deve ser uma data válida")]
        public System.DateTime CONT_DT_FINAL { get; set; }
        public int CONT_IN_ATIVO { get; set; }
        [Required(ErrorMessage = "Campo VALOR obrigatorio")]
        [RegularExpression(@"^[0-9]+([,.][0-9]+)?$", ErrorMessage = "Deve ser um valor numérico positivo")]
        public Nullable<decimal> CONT_VL_VALOR { get; set; }
        public Nullable<int> CONT_IN_STATUS { get; set; }
        [RegularExpression(@"^[0-9]+([,.][0-9]+)?$", ErrorMessage = "Deve ser um valor numérico positivo")]
        public Nullable<int> CONT_NR_CARENCIA { get; set; }
        [RegularExpression(@"^[0-9]+([,.][0-9]+)?$", ErrorMessage = "Deve ser um valor numérico positivo")]
        public Nullable<int> CONT_NR_DIA_PAGAMENTO { get; set; }
        public Nullable<int> CONT_IN_EMITE_NF { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Deve ser uma data válida")]
        public Nullable<System.DateTime> CONT_DT_ULTIMO_REAJUSTE { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Deve ser uma data válida")]
        public Nullable<System.DateTime> CONT_DT_PROXIMO_REAJUSTE { get; set; }
        [StringLength(5000, ErrorMessage = "A DESCRIÇÃO DETALHADA deve conter no máximo 5000.")]
        public string CONT_DS_DESCRICAO_DETALHADA { get; set; }
        public Nullable<int> CONT_IN_SINCRONIZA { get; set; }
        public string CONT_TX_OBSERVACOES { get; set; }
        [RegularExpression(@"^[0-9]+([,.][0-9]+)?$", ErrorMessage = "Deve ser um valor numérico positivo")]
        public Nullable<int> CONT_NR_PARCELAS { get; set; }
        [RegularExpression(@"^[0-9]+([,.][0-9]+)?$", ErrorMessage = "Deve ser um valor numérico positivo")]
        public Nullable<decimal> CONT_PC_PERCENT_PARCELAS { get; set; }
        public Nullable<int> NBSE_CD_ID { get; set; }
        [StringLength(50, ErrorMessage = "A DESCRIÇÃO DETALHADA deve conter no máximo 50.")]
        public string CONT_NM_NUMERO_CONTRATO { get; set; }
        public Nullable<int> CONT_IN_PERIODO_COBRANCA { get; set; }
        public Nullable<int> CONT_IN_ISS_RETIDO { get; set; }
        public Nullable<int> CONT_IN_IR_RETIDO { get; set; }
        [StringLength(500, ErrorMessage = "A DESCRIÇÃO DETALHADA deve conter no máximo 500.")]
        public string CONT_DS_TEXTO_NF { get; set; }

        public virtual ASSINANTE ASSINANTE { get; set; }
        public virtual CATEGORIA_CONTRATO CATEGORIA_CONTRATO { get; set; }
        public virtual CENTRO_CUSTO CENTRO_CUSTO { get; set; }
        public virtual CLIENTE CLIENTE { get; set; }
        public virtual COLABORADOR COLABORADOR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONTRATO_ANEXO> CONTRATO_ANEXO { get; set; }
        public virtual FORMA_PAGAMENTO FORMA_PAGAMENTO { get; set; }
        public virtual PERIODICIDADE PERIODICIDADE { get; set; }
        public virtual PLANO_CONTA PLANO_CONTA { get; set; }
        public virtual TEMPLATE TEMPLATE { get; set; }
        public virtual TIPO_CONTRATO TIPO_CONTRATO { get; set; }
        public virtual NOMENCLATURA_BRAS_SERVICOS NOMENCLATURA_BRAS_SERVICOS { get; set; }
    }
}