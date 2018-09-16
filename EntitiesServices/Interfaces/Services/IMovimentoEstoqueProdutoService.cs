using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;
using EntitiesServices.Work_Classes;

namespace ModelServices.Interfaces.EntitiesServices
{
    public interface IMovimentoEstoqueProdutoService : IServiceBase<MOVIMENTO_ESTOQUE_PRODUTO>
    {
        Int32 Create(MOVIMENTO_ESTOQUE_PRODUTO perfil, LOG log);
        Int32 Create(MOVIMENTO_ESTOQUE_PRODUTO perfil);
        //Int32 Edit(MOVIMENTO_ESTOQUE_PRODUTO perfil, LOG log);
        //Int32 Edit(MOVIMENTO_ESTOQUE_PRODUTO perfil);
        //Int32 Delete(MOVIMENTO_ESTOQUE_PRODUTO perfil, LOG log);
        //MOVIMENTO_ESTOQUE_PRODUTO GetItemById(Int32 id);
        //List<MOVIMENTO_ESTOQUE_PRODUTO> GetAllItens();
        //List<MOVIMENTO_ESTOQUE_PRODUTO> GetAllItensAdm();
    }
}
