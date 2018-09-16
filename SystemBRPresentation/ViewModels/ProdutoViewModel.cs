using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EntitiesServices.Model;
using EntitiesServices.Attributes;

namespace SystemBRPresentation.ViewModels
{
    public class ProdutoViewModel
    {
        [Key]
        public int PROD_CD_ID { get; set; }
        [Required(ErrorMessage = "Campo ASSINANTE obrigatorio")]
        public int ASSI_CD_ID { get; set; }
        [Required(ErrorMessage = "Campo MATRIZ obrigatorio")]
        public Nullable<int> MATR_CD_ID { get; set; }
        public Nullable<int> FILI_CD_ID { get; set; }
        [Required(ErrorMessage = "Campo CATEGORIA obrigatorio")]
        public Nullable<int> CAPR_CD_ID { get; set; }
        public Nullable<int> UNID_CD_ID { get; set; }
        [Required(ErrorMessage = "Campo NOME obrigatorio")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "O NOME deve conter no minimo 1 caracteres e no máximo 50.")]
        public string PROD_NM_NOME { get; set; }
        [StringLength(1000, ErrorMessage = "A DESCRIÇÃO deve conter no máximo 1000.")]
        public string PROD_DS_DESCRICAO { get; set; }
        [Required(ErrorMessage = "Campo QUANTIDADE MÍNIMA obrigatorio")]
        [RegularExpression(@"^[0-9]+([,.][0-9]+)?$", ErrorMessage = "Deve ser um valor numérico positivo")]
        public int PROD_QN_QUANTIDADE_MINIMA { get; set; }
        [Required(ErrorMessage = "Campo QUANTIDADE INICIAL obrigatorio")]
        [RegularExpression(@"^[0-9]+([,.][0-9]+)?$", ErrorMessage = "Deve ser um valor numérico positivo")]
        public int PROD_QN_QUANTIDADE_INICIAL { get; set; }
        [Required(ErrorMessage = "Campo QUANTIDADE ESTOQUE obrigatorio")]
        [RegularExpression(@"^[0-9]+([,.][0-9]+)?$", ErrorMessage = "Deve ser um valor numérico positivo")]
        public int PROD_QN_ESTOQUE { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Deve ser uma data válida")]
        public Nullable<System.DateTime> PROD_DT_ULTIMA_MOVIMENTACAO { get; set; }
        [Required(ErrorMessage = "Campo AVISO DE MÍNIMA obrigatorio")]
        public int PROD_IN_AVISA_MINIMO { get; set; }
        public System.DateTime PROD_DT_CADASTRO { get; set; }
        public int PROD_IN_ATIVO { get; set; }
        public string PROD_AQ_FOTO { get; set; }
        public bool AvisaMinima
        {
            get
            {
                if (PROD_IN_AVISA_MINIMO == 1)
                {
                    return true;
                }
                return false;
            }
            set
            {
                PROD_IN_AVISA_MINIMO = (value == true) ? 1 : 0;
            }
        }

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
        public virtual UNIDADE UNIDADE { get; set; }
        public virtual ASSINANTE ASSINANTE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUTO_ANEXO> PRODUTO_ANEXO { get; set; }

    }
}