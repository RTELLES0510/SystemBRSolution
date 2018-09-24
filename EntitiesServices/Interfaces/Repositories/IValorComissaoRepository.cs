using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface IValorComissaoRepository : IRepositoryBase<VALOR_COMISSAO>
    {
        VALOR_COMISSAO CheckExist(VALOR_COMISSAO conta);
        VALOR_COMISSAO GetItemById(Int32 id);
        List<VALOR_COMISSAO> GetAllItens();
        List<VALOR_COMISSAO> GetAllItensAdm();
        List<VALOR_COMISSAO> ExecuteFilter(Int32? categoria, Int32? tipo, String nome);
    }
}
