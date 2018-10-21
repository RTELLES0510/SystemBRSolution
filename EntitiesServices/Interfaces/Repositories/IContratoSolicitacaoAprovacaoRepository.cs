using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface IContratoSolicitacaoAprovacaoRepository : IRepositoryBase<CONTRATO_SOLICITACAO_APROVACAO>
    {
        CONTRATO_SOLICITACAO_APROVACAO GetItemById(Int32 id);
        List<CONTRATO_SOLICITACAO_APROVACAO> GetAllItens();
    }
}
