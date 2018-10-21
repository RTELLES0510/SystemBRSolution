using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;
using EntitiesServices.Work_Classes;

namespace ModelServices.Interfaces.EntitiesServices
{
    public interface IContratoSolicitacaoAprovacaoService : IServiceBase<CONTRATO_SOLICITACAO_APROVACAO>
    {
        Int32 Create(CONTRATO_SOLICITACAO_APROVACAO perfil, LOG log);
        Int32 Create(CONTRATO_SOLICITACAO_APROVACAO perfil);
        Int32 Edit(CONTRATO_SOLICITACAO_APROVACAO perfil, LOG log);
        Int32 Edit(CONTRATO_SOLICITACAO_APROVACAO perfil);
        Int32 Delete(CONTRATO_SOLICITACAO_APROVACAO perfil, LOG log);
        CONTRATO_SOLICITACAO_APROVACAO GetItemById(Int32 id);
        List<CONTRATO_SOLICITACAO_APROVACAO> GetAllItens();
    }
}
