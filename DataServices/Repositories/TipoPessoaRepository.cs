using System;
using System.Collections.Generic;
using EntitiesServices.Model;  
using ModelServices.Interfaces.Repositories;
using System.Linq;
using EntitiesServices.Work_Classes;
using System.Data.Entity;

namespace DataServices.Repositories
{
    public class TipoPessoaRepository : RepositoryBase<TIPO_PESSOA>, ITipoPessoaRepository
    {
        public TIPO_PESSOA GetItemById(Int32 id)
        {
            IQueryable<TIPO_PESSOA> query = Db.TIPO_PESSOA;
            query = query.Where(p => p.TIPE_CD_ID == id);
            return query.FirstOrDefault();
        }

        public List<TIPO_PESSOA> GetAllItens()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<TIPO_PESSOA> query = Db.TIPO_PESSOA;
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            return query.ToList();
        }
    }
}
