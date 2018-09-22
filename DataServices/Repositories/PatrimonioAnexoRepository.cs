using System;
using System.Collections.Generic;
using EntitiesServices.Model;  
using ModelServices.Interfaces.Repositories;
using System.Linq;
using EntitiesServices.Work_Classes;
using System.Data.Entity;

namespace DataServices.Repositories
{
    public class PatrimonioAnexoRepository : RepositoryBase<PATRIMONIO_ANEXO>, IPatrimonioAnexoRepository
    {
        public List<PATRIMONIO_ANEXO> GetAllItens()
        {
            return Db.PATRIMONIO_ANEXO.ToList();
        }

        public PATRIMONIO_ANEXO GetItemById(Int32 id)
        {
            IQueryable<PATRIMONIO_ANEXO> query = Db.PATRIMONIO_ANEXO.Where(p => p.PAAN_CD_ID == id);
            return query.FirstOrDefault();
        }
    }
}
