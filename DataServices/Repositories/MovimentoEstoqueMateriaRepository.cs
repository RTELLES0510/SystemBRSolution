using System;
using System.Collections.Generic;
using EntitiesServices.Model;  
using ModelServices.Interfaces.Repositories;
using System.Linq;
using EntitiesServices.Work_Classes;
using System.Data.Entity;

namespace DataServices.Repositories
{
    public class MovimentoEstoqueMateriaRepository : RepositoryBase<MOVIMENTO_ESTOQUE_MATERIA_PRIMA>, IMovimentoEstoqueMateriaRepository
    {
        public MOVIMENTO_ESTOQUE_MATERIA_PRIMA GetItemById(Int32 id)
        {
            IQueryable<MOVIMENTO_ESTOQUE_MATERIA_PRIMA> query = Db.MOVIMENTO_ESTOQUE_MATERIA_PRIMA;
            query = query.Where(p => p.MOEM_CD_ID == id);
            return query.FirstOrDefault();
        }

        public List<MOVIMENTO_ESTOQUE_MATERIA_PRIMA> GetAllItens()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<MOVIMENTO_ESTOQUE_MATERIA_PRIMA> query = Db.MOVIMENTO_ESTOQUE_MATERIA_PRIMA;
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            return query.ToList();
        }
    }
}
