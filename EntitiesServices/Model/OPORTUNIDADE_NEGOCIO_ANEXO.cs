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
    
    public partial class OPORTUNIDADE_NEGOCIO_ANEXO
    {
        public int OPNA_CD_ID { get; set; }
        public int OPNE_CD_ID { get; set; }
        public string OPNA_NM_TITULO { get; set; }
        public System.DateTime OPNA_DT_ANEXO { get; set; }
        public int OPNA_IN_TIPO { get; set; }
        public string OPNA_AQ_ARQUIVO { get; set; }
        public int OPNA_IN_ATIVO { get; set; }
    
        public virtual OPORTUNIDADE_NEGOCIO OPORTUNIDADE_NEGOCIO { get; set; }
    }
}
