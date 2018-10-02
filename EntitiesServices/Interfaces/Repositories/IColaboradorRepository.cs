using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface IColaboradorRepository : IRepositoryBase<COLABORADOR>
    {
        List<COLABORADOR> GetAllItens();
        COLABORADOR GetItemById(Int32 id);
    }
}
