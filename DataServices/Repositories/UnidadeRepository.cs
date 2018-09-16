using System;
using System.Collections.Generic;
using EntitiesServices.Model;  
using ModelServices.Interfaces.Repositories;
using System.Linq;
using EntitiesServices.Work_Classes;
using System.Data.Entity;

namespace DataServices.Repositories
{
    public class UnidadeRepository : RepositoryBase<UNIDADE>, IUnidadeRepository
    {
        public UNIDADE GetItemById(Int32 id)
        {
            IQueryable<UNIDADE> query = Db.UNIDADE;
            query = query.Where(p => p.UNID_CD_ID == id);
            return query.FirstOrDefault();
        }

        public List<UNIDADE> GetAllItens()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<UNIDADE> query = Db.UNIDADE;
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            return query.ToList();
        }
    }
}
