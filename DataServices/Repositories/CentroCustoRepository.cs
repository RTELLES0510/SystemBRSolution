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
    public class CentroCustoRepository : RepositoryBase<CENTRO_CUSTO>, ICentroCustoRepository
    {
        public CENTRO_CUSTO CheckExist(CENTRO_CUSTO conta)
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<CENTRO_CUSTO> query = Db.CENTRO_CUSTO;
            query = query.Where(p => p.CECU_NM_NOME == conta.CECU_NM_NOME);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            return query.FirstOrDefault();
        }

        public CENTRO_CUSTO GetByNome(String nome)
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<CENTRO_CUSTO> query = Db.CENTRO_CUSTO.Where(p => p.CECU_IN_ATIVO == 1);
            query = query.Where(p => p.CECU_NM_NOME == nome);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            return query.FirstOrDefault();
        }

        public CENTRO_CUSTO GetItemById(Int32 id)
        {
            IQueryable<CENTRO_CUSTO> query = Db.CENTRO_CUSTO;
            query = query.Where(p => p.CECU_CD_ID == id);
            query = query.Include(p => p.ASSINANTE);
            return query.FirstOrDefault();
        }

        public List<CENTRO_CUSTO> GetAllItens()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<CENTRO_CUSTO> query = Db.CENTRO_CUSTO.Where(p => p.CECU_IN_ATIVO == 1);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            query = query.Include(p => p.ASSINANTE);
            return query.ToList();
        }

        public List<CENTRO_CUSTO> GetAllItensAdm()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<CENTRO_CUSTO> query = Db.CENTRO_CUSTO;
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
