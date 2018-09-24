using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ApplicationServices.Interfaces
{
    public interface IValorComissaoAppService : IAppServiceBase<VALOR_COMISSAO>
    {
        Int32 ValidateCreate(VALOR_COMISSAO perfil, USUARIO usuario);
        Int32 ValidateEdit(VALOR_COMISSAO perfil, VALOR_COMISSAO perfilAntes, USUARIO usuario);
        Int32 ValidateDelete(VALOR_COMISSAO perfil, USUARIO usuario);
        Int32 ValidateReativar(VALOR_COMISSAO perfil, USUARIO usuario);
        List<VALOR_COMISSAO> GetAllItens();
        List<VALOR_COMISSAO> GetAllItensAdm();
        VALOR_COMISSAO GetItemById(Int32 id);
        VALOR_COMISSAO CheckExist(VALOR_COMISSAO conta);
        Int32 ExecuteFilter(Int32? categoria, Int32? tipo, String nome, out List<VALOR_COMISSAO> objeto);
        List<TIPO_COMISSAO> GetAllTipos();
        List<FILIAL> GetAllFiliais();
        List<CATEGORIA_PRODUTO> GetAllCategorias();
    }
}
