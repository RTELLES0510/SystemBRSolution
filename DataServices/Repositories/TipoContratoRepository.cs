using System;
using System.Collections.Generic;
using EntitiesServices.Model;  
using ModelServices.Interfaces.Repositories;
using System.Linq;
using EntitiesServices.Work_Classes;
using System.Data.Entity;

namespace DataServices.Repositories
{
    public class TipoContratoRepository : RepositoryBase<TIPO_CONTRATO>, ITipoContratoRepository
    {
        public TIPO_CONTRATO GetItemById(Int32 id)
        {
            IQueryable<TIPO_CONTRATO> query = Db.TIPO_CONTRATO;
            query = query.Where(p => p.TICT_CD_ID == id);
            return query.FirstOrDefault();
        }

        public List<TIPO_CONTRATO> GetAllItens()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<TIPO_CONTRATO> query = Db.TIPO_CONTRATO;
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            return query.ToList();
        }
    }
}
