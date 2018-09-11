using System;
using System.Collections.Generic;
using EntitiesServices.Model;  
using ModelServices.Interfaces.Repositories;
using System.Linq;
using EntitiesServices.Work_Classes;
using System.Data.Entity;

namespace DataServices.Repositories
{
    public class TipoContaRepository : RepositoryBase<TIPO_CONTA>, ITipoContaRepository
    {
        public TIPO_CONTA GetItemById(Int32 id)
        {
            IQueryable<TIPO_CONTA> query = Db.TIPO_CONTA;
            query = query.Where(p => p.TICO_CD_ID == id);
            return query.FirstOrDefault();
        }

        public List<TIPO_CONTA> GetAllItens()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<TIPO_CONTA> query = Db.TIPO_CONTA;
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            return query.ToList();
        }
    }
}
