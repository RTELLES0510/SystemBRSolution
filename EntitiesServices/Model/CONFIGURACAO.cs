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
    
    public partial class CONFIGURACAO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CONFIGURACAO()
        {
            this.ASSINANTE = new HashSet<ASSINANTE>();
        }
    
        public int CONF_CD_ID { get; set; }
        public int CONF_NR_FALHAS_DIA { get; set; }
        public string CONF_NM_HOST_SMTP { get; set; }
        public string CONF_NM_PORTA_SMTP { get; set; }
        public string CONF_NM_EMAIL_EMISSOR { get; set; }
        public string CONF_NM_SENHA_EMISSOR { get; set; }
        public Nullable<System.TimeSpan> CONF_TM_INICIO_AGENDA { get; set; }
        public Nullable<System.TimeSpan> CONF_TM_FINAL_AGENDA { get; set; }
        public Nullable<int> ASSI_CD_ID { get; set; }
        public string CONF_SG_TEMPLATE_APROVACAO_CONTRATO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ASSINANTE> ASSINANTE { get; set; }
    }
}
