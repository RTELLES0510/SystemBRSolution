using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ApplicationServices.Interfaces
{
    public interface IContaBancariaAppService : IAppServiceBase<CONTA_BANCARIA>
    {
        Int32 ValidateCreate(CONTA_BANCARIA perfil, USUARIO usuario);
        Int32 ValidateEdit(CONTA_BANCARIA perfil, CONTA_BANCARIA perfilAntes, USUARIO usuario);
        Int32 ValidateDelete(CONTA_BANCARIA perfil, USUARIO usuario);
        Int32 ValidateReativar(CONTA_BANCARIA perfil, USUARIO usuario);
        List<CONTA_BANCARIA> GetAllItens();
        List<CONTA_BANCARIA> GetAllItensAdm();
        CONTA_BANCARIA GetItemById(Int32 id);
        CONTA_BANCARIA CheckExist(CONTA_BANCARIA conta);
        List<TIPO_CONTA> GetAllTipos();
    }
}
