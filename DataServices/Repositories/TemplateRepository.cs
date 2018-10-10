using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;
using ModelServices.Interfaces.Repositories;
using EntitiesServices.Work_Classes;

namespace DataServices.Repositories
{
    public class TemplateRepository : RepositoryBase<TEMPLATE>, ITemplateRepository
    {
        public TEMPLATE GetByCode(String code)
        {
            return Db.TEMPLATE.Where(p => p.TEMP_SG_SIGLA == code).FirstOrDefault();
        }

        public TEMPLATE GetItemById(Int32 id)
        {
            IQueryable<TEMPLATE> query = Db.TEMPLATE;
            query = query.Where(p => p.TEMP_CD_ID == id);
            return query.FirstOrDefault();
        }

        public List<TEMPLATE> GetAllItens()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<TEMPLATE> query = Db.TEMPLATE;
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            return query.ToList();
        }
    }
}
 