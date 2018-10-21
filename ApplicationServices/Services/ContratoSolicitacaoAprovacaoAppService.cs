using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;
using EntitiesServices.Work_Classes;
using ApplicationServices.Interfaces;
using ModelServices.Interfaces.EntitiesServices;
using CrossCutting;
using System.Text.RegularExpressions;

namespace ApplicationServices.Services
{
    public class ContratoSolicitacaoAprovacaoAppService : AppServiceBase<CONTRATO_SOLICITACAO_APROVACAO>, IContratoSolicitacaoAprovacaoAppService
    {
        private readonly IContratoSolicitacaoAprovacaoService _baseService;

        public ContratoSolicitacaoAprovacaoAppService(IContratoSolicitacaoAprovacaoService baseService): base(baseService)
        {
            _baseService = baseService;
        }

        public List<CONTRATO_SOLICITACAO_APROVACAO> GetAllItens()
        {
            List<CONTRATO_SOLICITACAO_APROVACAO> lista = _baseService.GetAllItens();
            return lista;
        }

        public CONTRATO_SOLICITACAO_APROVACAO GetItemById(Int32 id)
        {
            CONTRATO_SOLICITACAO_APROVACAO item = _baseService.GetItemById(id);
            return item;
        }

    }
}
