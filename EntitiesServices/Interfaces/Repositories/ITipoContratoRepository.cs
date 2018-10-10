using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface ITipoContratoRepository : IRepositoryBase<TIPO_CONTRATO>
    {
        List<TIPO_CONTRATO> GetAllItens();
        TIPO_CONTRATO GetItemById(Int32 id);
    }
}
