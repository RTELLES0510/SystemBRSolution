using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface ITipoComissaoRepository : IRepositoryBase<TIPO_COMISSAO>
    {
        List<TIPO_COMISSAO> GetAllItens();
        TIPO_COMISSAO GetItemById(Int32 id);
    }
}
