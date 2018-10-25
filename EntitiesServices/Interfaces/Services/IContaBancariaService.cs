using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;
using EntitiesServices.Work_Classes;

namespace ModelServices.Interfaces.EntitiesServices
{
    public interface IContaBancariaService : IServiceBase<CONTA_BANCARIA>
    {
        Int32 Create(CONTA_BANCARIA perfil, LOG log);
        Int32 Create(CONTA_BANCARIA perfil);
        Int32 Edit(CONTA_BANCARIA perfil, LOG log);
        Int32 Edit(CONTA_BANCARIA perfil);
        Int32 Delete(CONTA_BANCARIA perfil, LOG log);
        CONTA_BANCARIA CheckExist(CONTA_BANCARIA conta);
        CONTA_BANCARIA GetItemById(Int32 id);
        List<CONTA_BANCARIA> GetAllItens();
        List<CONTA_BANCARIA> GetAllItensAdm();
        List<TIPO_CONTA> GetAllTipos();
        CONTA_BANCARIA_CONTATO GetContatoById(Int32 id);
        Int32 EditContato(CONTA_BANCARIA_CONTATO item);
        Int32 CreateContato(CONTA_BANCARIA_CONTATO item);
    }
}
