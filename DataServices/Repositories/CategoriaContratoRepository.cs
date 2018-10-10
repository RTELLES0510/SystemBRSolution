using System;
using System.Collections.Generic;
using EntitiesServices.Model;  
using ModelServices.Interfaces.Repositories;
using System.Linq;
using EntitiesServices.Work_Classes;
using System.Data.Entity;

namespace DataServices.Repositories
{
    public class CategoriaContratoRepository : RepositoryBase<CATEGORIA_CONTRATO>, ICategoriaContratoRepository
    {
        public CATEGORIA_CONTRATO GetItemById(Int32 id)
        {
            IQueryable<CATEGORIA_CONTRATO> query = Db.CATEGORIA_CONTRATO;
            query = query.Where(p => p.CACT_CD_ID == id);
            return query.FirstOrDefault();
        }

        public List<CATEGORIA_CONTRATO> GetAllItens()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<CATEGORIA_CONTRATO> query = Db.CATEGORIA_CONTRATO;
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            return query.ToList();
        }
    }
}
