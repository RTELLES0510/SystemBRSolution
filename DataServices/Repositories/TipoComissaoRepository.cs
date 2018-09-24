using System;
using System.Collections.Generic;
using EntitiesServices.Model;  
using ModelServices.Interfaces.Repositories;
using System.Linq;
using EntitiesServices.Work_Classes;
using System.Data.Entity;

namespace DataServices.Repositories
{
    public class TipoComissaoRepository : RepositoryBase<TIPO_COMISSAO>, ITipoComissaoRepository
    {
        public TIPO_COMISSAO GetItemById(Int32 id)
        {
            IQueryable<TIPO_COMISSAO> query = Db.TIPO_COMISSAO;
            query = query.Where(p => p.TICO_CD_ID == id);
            return query.FirstOrDefault();
        }

        public List<TIPO_COMISSAO> GetAllItens()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<TIPO_COMISSAO> query = Db.TIPO_COMISSAO;
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            return query.ToList();
        }
    }
}
