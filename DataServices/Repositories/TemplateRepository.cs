using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;
using ModelServices.Interfaces.Repositories;

namespace DataServices.Repositories
{
    public class TemplateRepository : RepositoryBase<TEMPLATE>, ITemplateRepository
    {
        public TEMPLATE GetByCode(String code)
        {
            return Db.TEMPLATE.Where(p => p.TEMP_SG_SIGLA == code).FirstOrDefault();
        }
    }
}
 