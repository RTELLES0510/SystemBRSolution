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
    
    public partial class TAREFA
    {
        public int TARE_CD_ID { get; set; }
        public int ASSI_CD_ID { get; set; }
        public Nullable<int> MATR_CD_ID { get; set; }
        public Nullable<int> FILI_CD_ID { get; set; }
        public int USUA_CD_ID { get; set; }
        public System.DateTime TARE_DT_DATA { get; set; }
        public string TARE_NM_TITULO { get; set; }
        public string TARE_DS_DESCRICAO { get; set; }
        public Nullable<System.DateTime> TARE_DT_ESTIMADA { get; set; }
        public int TARE_IN_STATUS { get; set; }
        public int TARE_IN_PRIORIDADE { get; set; }
        public int TARE_IN_ATIVO { get; set; }
    
        public virtual FILIAL FILIAL { get; set; }
        public virtual MATRIZ MATRIZ { get; set; }
    }
}
