using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface ICargoRepository : IRepositoryBase<CARGO>
    {
        CARGO GetByNome(String nome);
        CARGO GetItemById(Int32 id);
        List<CARGO> GetAllItens();
        List<CARGO> GetAllItensAdm();
        List<CARGO> ExecuteFilter(String nome);
    }
}
