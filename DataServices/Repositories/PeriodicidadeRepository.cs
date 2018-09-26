using System;
using System.Collections.Generic;
using EntitiesServices.Model;  
using ModelServices.Interfaces.Repositories;
using System.Linq;
using EntitiesServices.Work_Classes;
using System.Data.Entity;

namespace DataServices.Repositories
{
    public class PeriodicidadeRepository : RepositoryBase<PERIODICIDADE>, IPeriodicidadeRepository
    {
        public PERIODICIDADE GetItemById(Int32 id)
        {
            IQueryable<PERIODICIDADE> query = Db.PERIODICIDADE;
            query = query.Where(p => p.PERI_CD_ID == id);
            return query.FirstOrDefault();
        }

        public List<PERIODICIDADE> GetAllItens()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<PERIODICIDADE> query = Db.PERIODICIDADE;
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            return query.ToList();
        }
    }
}
