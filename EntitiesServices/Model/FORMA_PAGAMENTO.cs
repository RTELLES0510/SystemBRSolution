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
    
    public partial class FORMA_PAGAMENTO
    {
        public int FOPA_CD_ID { get; set; }
        public int ASSI_CD_ID { get; set; }
        public string FOPA_NM_NOME { get; set; }
    
        public virtual ASSINANTE ASSINANTE { get; set; }
    }
}
