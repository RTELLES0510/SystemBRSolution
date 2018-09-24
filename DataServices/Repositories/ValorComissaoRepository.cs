using System;
using System.Collections.Generic;
using EntitiesServices.Model;  
using ModelServices.Interfaces.Repositories;
using System.Linq;
using EntitiesServices.Work_Classes;
using System.Data.Entity;

namespace DataServices.Repositories
{
    public class ValorComissaoRepository : RepositoryBase<VALOR_COMISSAO>, IValorComissaoRepository
    {
        public VALOR_COMISSAO CheckExist(VALOR_COMISSAO conta)
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<VALOR_COMISSAO> query = Db.VALOR_COMISSAO;
            query = query.Where(p => p.TICO_CD_ID == conta.TICO_CD_ID);
            query = query.Where(p => p.CAPR_CD_ID == conta.CAPR_CD_ID);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            return query.FirstOrDefault();
        }

        public VALOR_COMISSAO GetItemById(Int32 id)
        {
            IQueryable<VALOR_COMISSAO> query = Db.VALOR_COMISSAO;
            query = query.Where(p => p.VACO_CD_ID == id);
            return query.FirstOrDefault();
        }

        public List<VALOR_COMISSAO> GetAllItens()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<VALOR_COMISSAO> query = Db.VALOR_COMISSAO.Where(p => p.VACO_IN_ATIVO == 1);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            return query.ToList();
        }

        public List<VALOR_COMISSAO> GetAllItensAdm()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<VALOR_COMISSAO> query = Db.VALOR_COMISSAO;
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            return query.ToList();
        }

        public List<VALOR_COMISSAO> ExecuteFilter(Int32? categoria, Int32? tipo, String nome)
        {
            Int32? idAss = SessionMocks.IdAssinante;
            List<VALOR_COMISSAO> lista = new List<VALOR_COMISSAO>();
            IQueryable<VALOR_COMISSAO> query = Db.VALOR_COMISSAO;
            if (categoria != null)
            {
                query = query.Where(p => p.CATEGORIA_PRODUTO.CAPR_CD_ID == categoria);
            }
            if (tipo != null)
            {
                query = query.Where(p => p.TIPO_COMISSAO.TICO_CD_ID == tipo);
            }
            if (!String.IsNullOrEmpty(nome))
            {
                query = query.Where(p => p.VACO_NM_NOME.Contains(nome));
            }
            if (query != null)
            {
                query = query.Where(p => p.ASSI_CD_ID == idAss);
                query = query.OrderBy(a => a.TIPO_COMISSAO.TICO_NM_NOME);
                lista = query.ToList<VALOR_COMISSAO>();
            }
            return lista;
        }

    }
}
