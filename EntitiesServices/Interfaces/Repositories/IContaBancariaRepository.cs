using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface IContaBancariaRepository : IRepositoryBase<CONTA_BANCARIA>
    {
        CONTA_BANCARIA CheckExist(CONTA_BANCARIA conta);
        CONTA_BANCARIA GetItemById(Int32 id);
        List<CONTA_BANCARIA> GetAllItens();
        List<CONTA_BANCARIA> GetAllItensAdm();
    }
}
