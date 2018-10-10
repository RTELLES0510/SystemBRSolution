using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface IFormaPagamentoRepository : IRepositoryBase<FORMA_PAGAMENTO>
    {
        List<FORMA_PAGAMENTO> GetAllItens();
        FORMA_PAGAMENTO GetItemById(Int32 id);
    }
}
