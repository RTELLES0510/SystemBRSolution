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
    
    public partial class LICENSA_ATESTADO
    {
        public int LIAT_CD_ID { get; set; }
        public int COLA_CD_ID { get; set; }
        public int LIAT_IN_TIPO { get; set; }
        public System.DateTime LIST_DT_DATA { get; set; }
        public string LIAT_DS_DESCRICAO { get; set; }
        public Nullable<int> LIAT_NR_DIAS { get; set; }
        public int LIAT_IN_ATIVO { get; set; }
    
        public virtual COLABORADOR COLABORADOR { get; set; }
    }
}
