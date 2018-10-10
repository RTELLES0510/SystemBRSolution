using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface ICentroCustoRepository : IRepositoryBase<CENTRO_CUSTO>
    {
        CENTRO_CUSTO CheckExist(CENTRO_CUSTO item);
        CENTRO_CUSTO GetItemById(Int32 id);
        List<CENTRO_CUSTO> GetAllItens();
        List<CENTRO_CUSTO> GetAllItensAdm();
        //List<PLANO_CONTA> ExecuteFilter(Int32? catId, String nome, String descricao, Int32? filiId);
    }
}
