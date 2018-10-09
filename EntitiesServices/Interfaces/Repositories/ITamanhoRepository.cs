using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface ITamanhoRepository : IRepositoryBase<TAMANHO>
    {
        List<TAMANHO> GetAllItens();
        TAMANHO GetItemById(Int32 id);
    }
}
