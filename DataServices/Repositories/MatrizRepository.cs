using System;
using System.Collections.Generic;
using EntitiesServices.Model;  
using ModelServices.Interfaces.Repositories;
using System.Linq;
using EntitiesServices.Work_Classes;
using System.Data.Entity;

namespace DataServices.Repositories
{
    public class MatrizRepository : RepositoryBase<MATRIZ>, IMatrizRepository
    {
        public MATRIZ GetItemById(Int32 id)
        {
            IQueryable<MATRIZ> query = Db.MATRIZ;
            query = query.Where(p => p.MATR_CD_ID == id);
            query = query.Include(p => p.CONTA_PAGAR);
            query = query.Include(p => p.CONTA_RECEBER);
            query = query.Include(p => p.ASSINANTE);
            return query.FirstOrDefault();
        }

        public List<MATRIZ> GetAllItens()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<MATRIZ> query = Db.MATRIZ.Where(p => p.MATR_IN_ATIVO == 1);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            query = query.Include(p => p.CONTA_PAGAR);
            query = query.Include(p => p.CONTA_RECEBER);
            query = query.Include(p => p.ASSINANTE);
            return query.ToList();
        }

        public List<MATRIZ> GetAllItensAdm()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<MATRIZ> query = Db.MATRIZ;
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            query = query.Include(p => p.CONTA_PAGAR);
            query = query.Include(p => p.CONTA_RECEBER);
            query = query.Include(p => p.ASSINANTE);
            return query.ToList();
        }
    }
}
