using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface IContaBancariaContatoRepository : IRepositoryBase<CONTA_BANCARIA_CONTATO>
    {
        List<CONTA_BANCARIA_CONTATO> GetAllItens();
        CONTA_BANCARIA_CONTATO GetItemById(Int32 id);
    }
}
