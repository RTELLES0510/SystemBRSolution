using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface ISubcategoriaProdutoRepository : IRepositoryBase<SUBCATEGORIA_PRODUTO>
    {
        List<SUBCATEGORIA_PRODUTO> GetAllItens();
        SUBCATEGORIA_PRODUTO GetItemById(Int32 id);
        List<SUBCATEGORIA_PRODUTO> GetItensByCategoria(Int32 cat);
    }
}
