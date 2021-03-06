using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EntitiesServices.Model;
using EntitiesServices.Attributes;

namespace SystemBRPresentation.ViewModels
{
    public class TransportadoraViewModel
    {
        [Key]
        public int TRAN_CD_ID { get; set; }
        [Required(ErrorMessage = "Campo ASSINANTE obrigatorio")]
        public int ASSI_CD_ID { get; set; }
        [Required(ErrorMessage = "Campo MATRIZ obrigatorio")]
        public Nullable<int> MATR_CD_ID { get; set; }
        public Nullable<int> FILI_CD_ID { get; set; }
        [Required(ErrorMessage = "Campo NOME obrigatorio")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "O NOME deve conter no minimo 1 caracteres e no máximo 50.")]
        public string TRAN_NM_NOME { get; set; }
        [StringLength(50, ErrorMessage = "A RAZÃO SOCIAL deve conter no máximo 50.")]
        public string TRAN_NM_RAZAO { get; set; }
        [StringLength(20, MinimumLength = 14, ErrorMessage = "O CNPJ deve conter no minimo 14 caracteres e no máximo 20.")]
        [CustomValidationCNPJ(ErrorMessage = "CNPJ inválido")]
        public string TRAN_NR_CNPJ { get; set; }
        [Required(ErrorMessage = "Campo E-MAIL obrigatorio")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O E-MAIL deve conter no minimo 1 caracteres e no máximo 100.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Deve ser um e-mail válido")]
        public string TRAN_NM_EMAIL { get; set; }
        [StringLength(250, ErrorMessage = "O TELEFONE deve conter no máximo 250.")]
        public string TRAN_NR_TELEFONES { get; set; }
        [StringLength(250, ErrorMessage = "O CONTATO deve conter no máximo 250.")]
        public string TRAN_NM_CONTATOS { get; set; }
        [StringLength(50, ErrorMessage = "O ENDEREÇO deve conter no máximo 50.")]
        public string TRAN_NM_ENDERECO { get; set; }
        [StringLength(50, ErrorMessage = "O BAIRRO deve conter no máximo 50.")]
        public string TRAN_NM_BAIRRO { get; set; }
        [StringLength(50, ErrorMessage = "A CIDADE deve conter no máximo 50.")]
        public string TRAN_NM_CIDADE { get; set; }
        [StringLength(2, ErrorMessage = "A UF deve conter no máximo 2.")]
        public string TRAN_SG_UF { get; set; }
        [StringLength(10, ErrorMessage = "O CEP deve conter no máximo 10.")]
        public string TRAN_NR_CEP { get; set; }
        public System.DateTime TRAN_DT_CADASTRO { get; set; }
        public int TRAN_IN_ATIVO { get; set; }

        public virtual FILIAL FILIAL { get; set; }
        public virtual MATRIZ MATRIZ { get; set; }
        public virtual ASSINANTE ASSINANTE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TRANSPORTADORA_ANEXO> TRANSPORTADORA_ANEXO { get; set; }
    }
}