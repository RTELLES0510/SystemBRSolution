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
    
    public partial class CONTRATO_ANEXO
    {
        public int COAN_CD_ID { get; set; }
        public int CONT_CD_ID { get; set; }
        public string COAN_NM_TITULO { get; set; }
        public System.DateTime COAN_DT_ANEXO { get; set; }
        public int COAN_IN_TIPO { get; set; }
        public string COAN_AQ_ARQUIVO { get; set; }
        public int COAN_IN_ATIVO { get; set; }
    
        public virtual CONTRATO CONTRATO { get; set; }
    }
}
