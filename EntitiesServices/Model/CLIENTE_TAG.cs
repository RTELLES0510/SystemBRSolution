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
    
    public partial class CLIENTE_TAG
    {
        public int CLTA_CD_ID { get; set; }
        public int CLIE_CD_ID { get; set; }
        public string CLTA_NM_TAG { get; set; }
        public Nullable<int> CLTA_IN_ATIVO { get; set; }
        public Nullable<int> CLTA_IN_TIPO { get; set; }
    
        public virtual CLIENTE CLIENTE { get; set; }
    }
}
