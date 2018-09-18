using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface IMateriaPrimaAnexoRepository : IRepositoryBase<MATERIA_PRIMA_ANEXO>
    {
        List<MATERIA_PRIMA_ANEXO> GetAllItens();
        MATERIA_PRIMA_ANEXO GetItemById(Int32 id);
    }
}
