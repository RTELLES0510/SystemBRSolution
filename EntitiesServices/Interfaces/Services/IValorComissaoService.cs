using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;
using EntitiesServices.Work_Classes;

namespace ModelServices.Interfaces.EntitiesServices
{
    public interface IValorComissaoService : IServiceBase<VALOR_COMISSAO>
    {
        Int32 Create(VALOR_COMISSAO perfil, LOG log);
        Int32 Create(VALOR_COMISSAO perfil);
        Int32 Edit(VALOR_COMISSAO perfil, LOG log);
        Int32 Edit(VALOR_COMISSAO perfil);
        Int32 Delete(VALOR_COMISSAO perfil, LOG log);
        VALOR_COMISSAO CheckExist(VALOR_COMISSAO conta);
        VALOR_COMISSAO GetItemById(Int32 id);
        List<VALOR_COMISSAO> GetAllItens();
        List<VALOR_COMISSAO> GetAllItensAdm();
        List<VALOR_COMISSAO> ExecuteFilter(Int32? categoria, Int32? tipo, String nome);
        List<TIPO_COMISSAO> GetAllTipos();
        List<CATEGORIA_PRODUTO> GetAllCategorias();
        List<FILIAL> GetAllFiliais();
    }
}
