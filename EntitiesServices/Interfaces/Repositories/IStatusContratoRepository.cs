using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface IStatusContratoRepository : IRepositoryBase<STATUS_CONTRATO>
    {
        List<STATUS_CONTRATO> GetAllItens();
        STATUS_CONTRATO GetItemById(Int32 id);
    }
}
