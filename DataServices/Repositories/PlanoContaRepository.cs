using System;
using System.Collections.Generic;
using EntitiesServices.Model;  
using ModelServices.Interfaces.Repositories;
using System.Linq;
using EntitiesServices.Work_Classes;
using System.Data.Entity;
using CrossCutting;

namespace DataServices.Repositories
{
    public class PlanoContaRepository : RepositoryBase<PLANO_CONTA>, IPlanoContaRepository
    {
        public PLANO_CONTA CheckExist(PLANO_CONTA conta)
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<PLANO_CONTA> query = Db.PLANO_CONTA;
            query = query.Where(p => p.PLCO_NM_CONTA == conta.PLCO_NM_CONTA);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            return query.FirstOrDefault();
        }

        public PLANO_CONTA GetByNome(String nome)
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<PLANO_CONTA> query = Db.PLANO_CONTA.Where(p => p.PLCO_IN_ATIVO == 1);
            query = query.Where(p => p.PLCO_NM_CONTA == nome);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            return query.FirstOrDefault();
        }

        public PLANO_CONTA GetItemById(Int32 id)
        {
            IQueryable<PLANO_CONTA> query = Db.PLANO_CONTA;
            query = query.Where(p => p.PLCO_CD_ID == id);
            query = query.Include(p => p.ASSINANTE);
            return query.FirstOrDefault();
        }

        public List<PLANO_CONTA> GetAllItens()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<PLANO_CONTA> query = Db.PLANO_CONTA.Where(p => p.PLCO_IN_ATIVO == 1);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            query = query.Where(p => p.NICO_CD_ID == 3);
            query = query.Where(p => p.PLCO_IN_CLASSE == 1);
            query = query.Include(p => p.ASSINANTE);
            return query.ToList();
        }

        public List<PLANO_CONTA> GetAllItensAdm()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<PLANO_CONTA> query = Db.PLANO_CONTA;
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            query = query.Include(p => p.ASSINANTE);
            return query.ToList();
        }

        //public List<PRODUTO> ExecuteFilter(Int32? catId, String nome, String descricao, Int32? filiId)
        //{
        //    Int32? idAss = SessionMocks.IdAssinante;
        //    List<PRODUTO> lista = new List<PRODUTO>();
        //    IQueryable<PRODUTO> query = Db.PRODUTO;
        //    if (catId != null)
        //    {
        //        query = query.Where(p => p.CATEGORIA_PRODUTO.CAPR_CD_ID == catId);
        //    }
        //    if (!String.IsNullOrEmpty(nome))
        //    {
        //        query = query.Where(p => p.PROD_NM_NOME.Contains(nome));
        //    }
        //    if (!String.IsNullOrEmpty(descricao))
        //    {
        //        query = query.Where(p => p.PROD_DS_DESCRICAO.Contains(descricao));
        //    }
        //    if (filiId != null)
        //    {
        //        query = query.Where(p => p.FILIAL.FILI_CD_ID == filiId);
        //    }

        //    if (query != null)
        //    {
        //        query = query.Where(p => p.ASSI_CD_ID == idAss);
        //        query = query.OrderBy(a => a.PROD_NM_NOME);
        //        lista = query.ToList<PRODUTO>();
        //    }
        //    return lista;
        //}
    }
}
