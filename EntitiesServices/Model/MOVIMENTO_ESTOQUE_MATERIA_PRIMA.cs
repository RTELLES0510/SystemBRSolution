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
    
    public partial class MOVIMENTO_ESTOQUE_MATERIA_PRIMA
    {
        public int MOEM_CD_ID { get; set; }
        public int ASSI_CD_ID { get; set; }
        public Nullable<int> MATR_CD_ID { get; set; }
        public Nullable<int> FILI_CD_ID { get; set; }
        public int MAPR_CD_ID { get; set; }
        public int USUA_CD_ID { get; set; }
        public System.DateTime MOEM_DT_MOVIMENTO { get; set; }
        public int MOEM_IN_TIPO_MOVIMENTO { get; set; }
        public decimal MOEM_QN_QUANTIDADE { get; set; }
        public string MOEM_NM_ORIGEM { get; set; }
        public int MOEM_IN_CHAVE_ORIGEM { get; set; }
        public int MOEM_IN_ATIVO { get; set; }
    
        public virtual FILIAL FILIAL { get; set; }
        public virtual MATERIA_PRIMA MATERIA_PRIMA { get; set; }
        public virtual MATRIZ MATRIZ { get; set; }
        public virtual ASSINANTE ASSINANTE { get; set; }
        public virtual USUARIO USUARIO { get; set; }
    }
}
