using System;
using System.Collections.Generic;
using EntitiesServices.Model;  
using ModelServices.Interfaces.Repositories;
using System.Linq;
using EntitiesServices.Work_Classes;
using System.Data.Entity;

namespace DataServices.Repositories
{
    public class ColaboradorRepository : RepositoryBase<COLABORADOR>, IColaboradorRepository
    {
        public COLABORADOR GetItemById(Int32 id)
        {
            IQueryable<COLABORADOR> query = Db.COLABORADOR;
            query = query.Where(p => p.COLA_CD_ID == id);
            return query.FirstOrDefault();
        }

        public List<COLABORADOR> GetAllItens()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<COLABORADOR> query = Db.COLABORADOR;
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            query = query.Where(p => p.CARGO.CARG_NM_NOME.Contains("endedor"));
            return query.ToList();
        }
    }
}
