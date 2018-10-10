using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface ICategoriaContratoRepository : IRepositoryBase<CATEGORIA_CONTRATO>
    {
        List<CATEGORIA_CONTRATO> GetAllItens();
        CATEGORIA_CONTRATO GetItemById(Int32 id);
    }
}
