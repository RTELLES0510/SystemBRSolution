using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface ICategoriaMateriaPrimaRepository : IRepositoryBase<CATEGORIA_MATERIA>
    {
        List<CATEGORIA_MATERIA> GetAllItens();
        CATEGORIA_MATERIA GetItemById(Int32 id);
    }
}
