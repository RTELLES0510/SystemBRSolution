using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EntitiesServices.Model;

namespace SystemBRPresentation.ViewModels
{
    public class LogViewModel
    {
        [Key]
        public int LOG_CD_ID { get; set; }
        public int USUA_CD_ID { get; set; }
        public Nullable<System.DateTime> LOG_DT_DATA { get; set; }
        public string LOG_NM_OPERACAO { get; set; }
        public string LOG_TX_REGISTRO { get; set; }
        public string LOG_TX_REGISTRO_ANTES { get; set; }
        public int LOG_IN_ATIVO { get; set; }
        public Nullable<int> ASSI_CD_ID { get; set; }

        public virtual USUARIO USUARIO { get; set; }
        public virtual ASSINANTE ASSINANTE { get; set; }
    }
}