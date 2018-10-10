using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface IContratoRepository : IRepositoryBase<CONTRATO>
    {
        CONTRATO CheckExist(CONTRATO item);
        CONTRATO GetByNome(String nome);
        CONTRATO GetItemById(Int32 id);
        List<CONTRATO> GetAllItens();
        List<CONTRATO> GetAllItensAdm();
        List<CONTRATO> ExecuteFilter(Int32? catId, Int32? tipoId, String nome, String descricao);
    }
}
