using System;
using System.Collections.Generic;
using EntitiesServices.Model;  
using ModelServices.Interfaces.Repositories;
using System.Linq;
using EntitiesServices.Work_Classes;
using System.Data.Entity;

namespace DataServices.Repositories
{
    public class ContaBancariaRepository : RepositoryBase<CONTA_BANCARIA>, IContaBancariaRepository
    {
        public CONTA_BANCARIA CheckExist(CONTA_BANCARIA conta)
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<CONTA_BANCARIA> query = Db.CONTA_BANCARIA;
            query = query.Where(p => p.COBA_NR_AGENCIA == conta.COBA_NR_AGENCIA);
            query = query.Where(p => p.COBA_NR_CONTA == conta.COBA_NR_CONTA);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            return query.FirstOrDefault();
        }

        public CONTA_BANCARIA GetItemById(Int32 id)
        {
            IQueryable<CONTA_BANCARIA> query = Db.CONTA_BANCARIA;
            query = query.Where(p => p.COBA_CD_ID == id);
            query = query.Include(p => p.CONTA_PAGAR);
            query = query.Include(p => p.CONTA_RECEBER);
            query = query.Include(p => p.BANCO);
            query = query.Include(p => p.TIPO_CONTA);
            query = query.Include(p => p.CONTA_BANCARIA_CONTATO);
            return query.FirstOrDefault();
        }

        public List<CONTA_BANCARIA> GetAllItens()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<CONTA_BANCARIA> query = Db.CONTA_BANCARIA.Where(p => p.COBA_IN_ATIVO == 1);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            query = query.Include(p => p.CONTA_PAGAR);
            query = query.Include(p => p.CONTA_RECEBER);
            query = query.Include(p => p.BANCO);
            query = query.Include(p => p.TIPO_CONTA);
            return query.ToList();
        }

        public List<CONTA_BANCARIA> GetAllItensAdm()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<CONTA_BANCARIA> query = Db.CONTA_BANCARIA;
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            query = query.Include(p => p.CONTA_PAGAR);
            query = query.Include(p => p.CONTA_RECEBER);
            query = query.Include(p => p.BANCO);
            query = query.Include(p => p.TIPO_CONTA);
            return query.ToList();
        }
    }
}
