using System;
using System.Collections.Generic;
using EntitiesServices.Model;  
using ModelServices.Interfaces.Repositories;
using System.Linq;
using EntitiesServices.Work_Classes;
using System.Data.Entity;

namespace DataServices.Repositories
{
    public class CategoriaPatrimonioRepository : RepositoryBase<CATEGORIA_PATRIMONIO>, ICategoriaPatrimonioRepository
    {
        public CATEGORIA_PATRIMONIO GetItemById(Int32 id)
        {
            IQueryable<CATEGORIA_PATRIMONIO> query = Db.CATEGORIA_PATRIMONIO;
            query = query.Where(p => p.CAPR_CD_ID == id);
            return query.FirstOrDefault();
        }

        public List<CATEGORIA_PATRIMONIO> GetAllItens()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<CATEGORIA_PATRIMONIO> query = Db.CATEGORIA_PATRIMONIO;
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            return query.ToList();
        }
    }
}
