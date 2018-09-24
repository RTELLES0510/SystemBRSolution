using System;
using System.Collections.Generic;
using EntitiesServices.Model;  
using ModelServices.Interfaces.Repositories;
using System.Linq;
using EntitiesServices.Work_Classes;
using System.Data.Entity;

namespace DataServices.Repositories
{
    public class CargoRepository : RepositoryBase<CARGO>, ICargoRepository
    {
        public CARGO GetByNome(String nome)
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<CARGO> query = Db.CARGO.Where(p => p.CARG_IN_ATIVO == 1);
            query = query.Where(p => p.CARG_NM_NOME == nome);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            return query.FirstOrDefault();
        }

        public CARGO GetItemById(Int32 id)
        {
            IQueryable<CARGO> query = Db.CARGO;
            query = query.Where(p => p.CARG_CD_ID == id);
            return query.FirstOrDefault();
        }

        public List<CARGO> GetAllItens()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<CARGO> query = Db.CARGO.Where(p => p.CARG_IN_ATIVO == 1);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            return query.ToList();
        }

        public List<CARGO> GetAllItensAdm()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<CARGO> query = Db.CARGO;
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            return query.ToList();
        }

        public List<CARGO> ExecuteFilter(String nome)
        {
            Int32? idAss = SessionMocks.IdAssinante;
            List<CARGO> lista = new List<CARGO>();
            IQueryable<CARGO> query = Db.CARGO;
            if (!String.IsNullOrEmpty(nome))
            {
                query = query.Where(p => p.CARG_NM_NOME.Contains(nome));
            }
            if (query != null)
            {
                query = query.Where(p => p.ASSI_CD_ID == idAss);
                query = query.OrderBy(a => a.CARG_NM_NOME);
                lista = query.ToList<CARGO>();
            }
            return lista;
        }
    }
}
