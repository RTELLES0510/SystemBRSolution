using System;
using System.Collections.Generic;
using EntitiesServices.Model;  
using ModelServices.Interfaces.Repositories;
using System.Linq;
using EntitiesServices.Work_Classes;
using System.Data.Entity;

namespace DataServices.Repositories
{
    public class StatusContratoRepository : RepositoryBase<STATUS_CONTRATO>, IStatusContratoRepository
    {
        public STATUS_CONTRATO GetItemById(Int32 id)
        {
            IQueryable<STATUS_CONTRATO> query = Db.STATUS_CONTRATO;
            query = query.Where(p => p.STCT_CD_ID == id);
            return query.FirstOrDefault();
        }

        public List<STATUS_CONTRATO> GetAllItens()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<STATUS_CONTRATO> query = Db.STATUS_CONTRATO;
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            return query.ToList();
        }
    }
}
