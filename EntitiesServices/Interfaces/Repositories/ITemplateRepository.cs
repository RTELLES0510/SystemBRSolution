using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface ITemplateRepository : IRepositoryBase<TEMPLATE>
    {
        TEMPLATE GetByCode(String code);
        List<TEMPLATE> GetAllItens();
        TEMPLATE GetItemById(Int32 id);
    }
}
