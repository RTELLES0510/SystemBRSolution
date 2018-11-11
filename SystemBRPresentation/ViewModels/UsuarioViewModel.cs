using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EntitiesServices.Model;

namespace SystemBRPresentation.ViewModels
{
    public class UsuarioViewModel
    {
        [Key]
        public int USUA_CD_ID { get; set; }
        public int PERF_CD_ID { get; set; }
        public int ASSI_CD_ID { get; set; }
        public Nullable<int> COLA_CD_ID { get; set; }
        public int USUA_IN_BLOQUEADO { get; set; }
        public Nullable<System.DateTime> USUA_DT_BLOQUEIO { get; set; }
        [Required(ErrorMessage = "Campo E-MAIL obrigatorio")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O E-MAIL deve conter no minimo 1 e no máximo 100 caracteres.")]
        public string USUA_NM_EMAIL { get; set; }
        [Required(ErrorMessage = "Campo SENHA obrigatorio")]
        [StringLength(8, MinimumLength = 6, ErrorMessage = "A SENHA deve conter no minimo 6 e no máximo 8 caracteres.")]
        public string USUA_NM_SENHA { get; set; }
        [StringLength(8, MinimumLength = 6, ErrorMessage = "A SENHA deve conter no minimo 6 e no máximo 8 caracteres.")]
        public string USUA_NM_SENHA_CONFIRMA { get; set; }
        public int USUA_IN_PROVISORIA { get; set; }
        public int USUA_IN_LOGIN_PROVISORIO { get; set; }
        public int USUA_NR_ACESSOS { get; set; }
        public int USUA_NR_FALHAS { get; set; }
        public Nullable<System.DateTime> USUA_DT_ALTERACAO { get; set; }
        public Nullable<System.DateTime> USUA_DT_TROCA_SENHA { get; set; }
        public Nullable<System.DateTime> USUA_DT_ACESSO { get; set; }
        public Nullable<System.DateTime> USUA_DT_ULTIMA_FALHA { get; set; }
        [StringLength(8, MinimumLength = 6, ErrorMessage = "A SENHA deve conter no minimo 6 e no máximo 8 caracteres.")]
        public string USUA_NM_NOVA_SENHA { get; set; }
        public System.DateTime USUA_DT_CADASTRO { get; set; }
        public int USUA_IN_ATIVO { get; set; }
        public string USUA_AQ_FOTO { get; set; }
        public string USUA_TX_OBSERVACOES { get; set; }

        public virtual ASSINANTE ASSINANTE { get; set; }
        public virtual COLABORADOR COLABORADOR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LOG> LOG { get; set; }
        public virtual PERFIL PERFIL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NOTIFICACAO> NOTIFICACAO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MOVIMENTO_ESTOQUE_MATERIA_PRIMA> MOVIMENTO_ESTOQUE_MATERIA_PRIMA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NOTICIA_AVALIACAO> NOTICIA_AVALIACAO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NOTICIA_COMENTARIO> NOTICIA_COMENTARIO { get; set; }
    }
}