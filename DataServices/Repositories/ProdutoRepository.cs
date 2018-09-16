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
    public class ProdutoRepository : RepositoryBase<PRODUTO>, IProdutoRepository
    {
        public PRODUTO CheckExist(PRODUTO conta)
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<PRODUTO> query = Db.PRODUTO;
            query = query.Where(p => p.PROD_NM_NOME == conta.PROD_NM_NOME);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            return query.FirstOrDefault();
        }

        public PRODUTO GetByNome(String nome)
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<PRODUTO> query = Db.PRODUTO.Where(p => p.PROD_IN_ATIVO == 1);
            query = query.Where(p => p.PROD_NM_NOME == nome);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            query = query.Include(p => p.ASSINANTE);
            query = query.Include(p => p.MATRIZ);
            query = query.Include(p => p.FILIAL);
            return query.FirstOrDefault();
        }

        public PRODUTO GetItemById(Int32 id)
        {
            IQueryable<PRODUTO> query = Db.PRODUTO;
            query = query.Where(p => p.PROD_CD_ID == id);
            query = query.Include(p => p.ASSINANTE);
            query = query.Include(p => p.MATRIZ);
            query = query.Include(p => p.FILIAL);
            return query.FirstOrDefault();
        }

        public List<PRODUTO> GetAllItens()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<PRODUTO> query = Db.PRODUTO.Where(p => p.PROD_IN_ATIVO == 1);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            query = query.Include(p => p.ASSINANTE);
            query = query.Include(p => p.MATRIZ);
            query = query.Include(p => p.FILIAL);
            return query.ToList();
        }

        public List<PRODUTO> GetAllItensAdm()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<PRODUTO> query = Db.PRODUTO;
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            query = query.Include(p => p.ASSINANTE);
            query = query.Include(p => p.MATRIZ);
            query = query.Include(p => p.FILIAL);
            return query.ToList();
        }

        public List<PRODUTO> ExecuteFilter(Int32? catId, String nome, String descricao, Int32? filiId)
        {
            Int32? idAss = SessionMocks.IdAssinante;
            List<PRODUTO> lista = new List<PRODUTO>();
            IQueryable<PRODUTO> query = Db.PRODUTO;
            if (catId != null)
            {
                query = query.Where(p => p.CATEGORIA_PRODUTO.CAPR_CD_ID == catId);
            }
            if (!String.IsNullOrEmpty(nome))
            {
                query = query.Where(p => p.PROD_NM_NOME.Contains(nome));
            }
            if (!String.IsNullOrEmpty(descricao))
            {
                query = query.Where(p => p.PROD_DS_DESCRICAO.Contains(descricao));
            }
            if (filiId != null)
            {
                query = query.Where(p => p.FILIAL.FILI_CD_ID == filiId);
            }

            if (query != null)
            {
                query = query.Where(p => p.ASSI_CD_ID == idAss);
                query = query.OrderBy(a => a.PROD_NM_NOME);
                lista = query.ToList<PRODUTO>();
            }
            return lista;
        }
    }
}
