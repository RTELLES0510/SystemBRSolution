using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EntitiesServices.Model;

namespace SystemBRPresentation.ViewModels
{
    public class ContaBancariaViewModel
    {
        [Key]
        public int COBA_CD_ID { get; set; }
        [Required(ErrorMessage = "Campo ASSINANTE obrigatorio")]
        public int ASSI_CD_ID { get; set; }
        [Required(ErrorMessage = "Campo BANCO obrigatorio")]
        public int BANC_CD_ID { get; set; }
        [Required(ErrorMessage = "Campo TIPO DE CONTA obrigatorio")]
        public int TICO_CD_ID { get; set; }
        [Required(ErrorMessage = "Campo NÚMERO DA AGENCIA obrigatorio")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "O NÚMERO DA AGENCIA deve conter no minimo 3 caracteres e no máximo 10.")]
        public string COBA_NR_AGENCIA { get; set; }
        [Required(ErrorMessage = "Campo NÚMERO DA CONTA obrigatorio")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "O NÚMERO DA CONTA deve conter no minimo 3 caracteres e no máximo 20.")]
        public string COBA_NR_CONTA { get; set; }
        [StringLength(50, ErrorMessage = "O NOME DA AGENCIA DA CONTA deve conter no máximo 50.")]
        public string COBA_NM_AGENCIA { get; set; }
        [StringLength(50, ErrorMessage = "O NOME DO GERENTE deve conter no máximo 50.")]
        public string COBA_NM_GERENTE { get; set; }
        [StringLength(50, ErrorMessage = "O TELEFONE deve conter no máximo 50.")]
        public string COBA_NR_TELEFONE { get; set; }
        [Required(ErrorMessage = "Campo DATA DE ABERTURA obrigatorio")]
        [DataType(DataType.Date, ErrorMessage = "Deve ser uma data válida")]
        public Nullable<System.DateTime> COBA_DT_ABERTURA { get; set; }
        public int COBA_IN_ATIVO { get; set; }
        [Required(ErrorMessage = "Campo SALDO INICIAL obrigatorio")]
        [RegularExpression(@"^[0-9]+([,.][0-9]+)?$", ErrorMessage = "Deve ser um valor numérico positivo")]
        public Nullable<decimal> COBA_VL_SALDO_INICIAL { get; set; }

        public virtual BANCO BANCO { get; set; }
        public virtual TIPO_CONTA TIPO_CONTA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONTA_PAGAR> CONTA_PAGAR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONTA_RECEBER> CONTA_RECEBER { get; set; }

    }
}