using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ApplicationServices.Interfaces
{
    public interface ICargoAppService : IAppServiceBase<CARGO>
    {
        Int32 ValidateCreate(CARGO perfil, USUARIO usuario);
        Int32 ValidateEdit(CARGO perfil, CARGO perfilAntes, USUARIO usuario);
        Int32 ValidateDelete(CARGO perfil, USUARIO usuario);
        Int32 ValidateReativar(CARGO perfil, USUARIO usuario);
        List<CARGO> GetAllItens();
        List<CARGO> GetAllItensAdm();
        CARGO GetItemById(Int32 id);
        CARGO GetByNome(String nome);
        Int32 ExecuteFilter(String nome, out List<CARGO> objeto);
        List<VALOR_COMISSAO> GetAllValores();
    }
}
