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
    
    public partial class COLABORADOR_FREQUENCIA
    {
        public int COFR_CD_ID { get; set; }
        public int COLA_CD_ID { get; set; }
        public System.DateTime COFR_DT_DATA { get; set; }
        public System.DateTime COFR_DT_ENTRADA { get; set; }
        public Nullable<System.DateTime> COFR_DT_SAIDA { get; set; }
        public int COFR_IN_ATIVO { get; set; }
    
        public virtual COLABORADOR COLABORADOR { get; set; }
    }
}
