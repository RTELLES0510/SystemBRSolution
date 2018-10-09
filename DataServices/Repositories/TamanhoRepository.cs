using System;
using System.Collections.Generic;
using EntitiesServices.Model;  
using ModelServices.Interfaces.Repositories;
using System.Linq;
using EntitiesServices.Work_Classes;
using System.Data.Entity;

namespace DataServices.Repositories
{
    public class TamanhoRepository : RepositoryBase<TAMANHO>, ITamanhoRepository
    {
        public TAMANHO GetItemById(Int32 id)
        {
            IQueryable<TAMANHO> query = Db.TAMANHO;
            query = query.Where(p => p.TAMA_CD_ID == id);
            return query.FirstOrDefault();
        }

        public List<TAMANHO> GetAllItens()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<TAMANHO> query = Db.TAMANHO;
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            return query.ToList();
        }
    }
}
