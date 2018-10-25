using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EntitiesServices.Model;
using EntitiesServices.Attributes;

namespace SystemBRPresentation.ViewModels
{
    public class ContaBancariaContatoViewModel
    {
        [Key]
        public int CBCT_CD_ID { get; set; }
        [Required(ErrorMessage = "Campo CONTA obrigatorio")]
        public int COBA_CD_ID { get; set; }
        [Required(ErrorMessage = "Campo NOME obrigatorio")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "O NOME deve conter no minimo 1 caracteres e no máximo 50.")]
        public string CBCT_NM_NOME { get; set; }
        [StringLength(50, ErrorMessage = "O CARGO deve conter no máximo 50.")]
        public string CBCT_NM_CARGO { get; set; }
        [Required(ErrorMessage = "Campo E-MAIL obrigatorio")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O NOME deve conter no minimo 1 caracteres e no máximo 100.")]
        public string CBCT_NM_EMAIL { get; set; }
        [StringLength(50, ErrorMessage = "O TELEFONE deve conter no máximo 50.")]
        public string CBCT_NR_TELEFONES { get; set; }
        [StringLength(50, ErrorMessage = "AS OBSERVAÇÕES deve conter no máximo 500.")]
        public string CLCO_DS_OBSERVACOES { get; set; }
        public int CBCT_IN_ATIVO { get; set; }

        public virtual CONTA_BANCARIA CONTA_BANCARIA { get; set; }
    }
}