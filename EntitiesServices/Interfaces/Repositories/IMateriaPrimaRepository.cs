using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface IMateriaPrimaRepository : IRepositoryBase<MATERIA_PRIMA>
    {
        MATERIA_PRIMA CheckExist(MATERIA_PRIMA item);
        MATERIA_PRIMA GetByNome(String nome);
        MATERIA_PRIMA GetItemById(Int32 id);
        List<MATERIA_PRIMA> GetAllItens();
        List<MATERIA_PRIMA> GetAllItensAdm();
        List<MATERIA_PRIMA> ExecuteFilter(Int32? catId, String nome, String descricao, Int32? filiId);
    }
}
