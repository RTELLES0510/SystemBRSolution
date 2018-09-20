using System;
using System.Collections.Generic;
using EntitiesServices.Model;  
using ModelServices.Interfaces.Repositories;
using System.Linq;
using EntitiesServices.Work_Classes;
using System.Data.Entity;

namespace DataServices.Repositories
{
    public class CategoriaServicoRepository : RepositoryBase<CATEGORIA_SERVICO>, ICategoriaServicoRepository
    {
        public CATEGORIA_SERVICO GetItemById(Int32 id)
        {
            IQueryable<CATEGORIA_SERVICO> query = Db.CATEGORIA_SERVICO;
            query = query.Where(p => p.CASE_CD_ID == id);
            return query.FirstOrDefault();
        }

        public List<CATEGORIA_SERVICO> GetAllItens()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<CATEGORIA_SERVICO> query = Db.CATEGORIA_SERVICO;
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            return query.ToList();
        }
    }
}
