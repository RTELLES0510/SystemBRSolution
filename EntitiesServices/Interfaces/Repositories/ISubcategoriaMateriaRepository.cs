using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface ISubcategoriaMateriaRepository : IRepositoryBase<SUBCATEGORIA_MATERIA>
    {
        List<SUBCATEGORIA_MATERIA> GetAllItens();
        SUBCATEGORIA_MATERIA GetItemById(Int32 id);
        List<SUBCATEGORIA_MATERIA> GetItensByCategoria(Int32 cat);
    }
}
