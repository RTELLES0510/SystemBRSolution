using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface IProdutoRepository : IRepositoryBase<PRODUTO>
    {
        PRODUTO CheckExist(PRODUTO item);
        PRODUTO GetByNome(String nome);
        PRODUTO GetItemById(Int32 id);
        List<PRODUTO> GetAllItens();
        List<PRODUTO> GetAllItensAdm();
        List<PRODUTO> ExecuteFilter(Int32? catId, String nome, String descricao, Int32? filiId);
    }
}
