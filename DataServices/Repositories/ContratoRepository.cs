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
    public class ContratoRepository : RepositoryBase<CONTRATO>, IContratoRepository
    {
        public CONTRATO CheckExist(CONTRATO conta)
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<CONTRATO> query = Db.CONTRATO;
            query = query.Where(p => p.CONT_NM_NOME == conta.CONT_NM_NOME);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            return query.FirstOrDefault();
        }

        public CONTRATO GetByNome(String nome)
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<CONTRATO> query = Db.CONTRATO.Where(p => p.CONT_IN_ATIVO == 1);
            query = query.Where(p => p.CONT_NM_NOME == nome);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            query = query.Include(p => p.ASSINANTE);
            query = query.Include(p => p.CONTRATO_ANEXO);
            return query.FirstOrDefault();
        }

        public CONTRATO GetItemById(Int32 id)
        {
            IQueryable<CONTRATO> query = Db.CONTRATO;
            query = query.Where(p => p.CONT_CD_ID == id);
            query = query.Include(p => p.ASSINANTE);
            query = query.Include(p => p.CONTRATO_ANEXO);
            return query.FirstOrDefault();
        }

        public List<CONTRATO> GetAllItens()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<CONTRATO> query = Db.CONTRATO.Where(p => p.CONT_IN_ATIVO == 1);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            query = query.Include(p => p.ASSINANTE);
            return query.ToList();
        }

        public List<CONTRATO> GetAllItensOperacao()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<CONTRATO> query = Db.CONTRATO.Where(p => p.CONT_IN_ATIVO == 1);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            query = query.Where(p => p.CONT_IN_WORKFLOW == 3);
            query = query.Include(p => p.ASSINANTE);
            return query.ToList();
        }

        public List<CONTRATO> GetAllItensAdm()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<CONTRATO> query = Db.CONTRATO;
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            query = query.Include(p => p.ASSINANTE);
            return query.ToList();
        }

        public List<CONTRATO> ExecuteFilter(Int32? catId, Int32? tipoId, Int32? statId, String nome, String descricao)
        {
            Int32? idAss = SessionMocks.IdAssinante;
            List<CONTRATO> lista = new List<CONTRATO>();
            IQueryable<CONTRATO> query = Db.CONTRATO;
            if (catId != null)
            {
                query = query.Where(p => p.CATEGORIA_CONTRATO.CACT_CD_ID == catId);
            }
            if (tipoId != null)
            {
                query = query.Where(p => p.TIPO_CONTRATO.TICT_CD_ID == tipoId);
            }
            if (statId != null)
            {
                query = query.Where(p => p.STATUS_CONTRATO.STCT_CD_ID == statId);
            }
            if (!String.IsNullOrEmpty(nome))
            {
                query = query.Where(p => p.CONT_NM_NOME.Contains(nome));
            }
            if (!String.IsNullOrEmpty(descricao))
            {
                query = query.Where(p => p.CONT_DS_DESCRICAO.Contains(descricao));
            }

            if (query != null)
            {
                query = query.Where(p => p.ASSI_CD_ID == idAss);
                query = query.OrderBy(a => a.CONT_DT_INICIO).ThenBy(a => a.CONT_NM_NOME);
                lista = query.ToList<CONTRATO>();
            }
            return lista;
        }
    }
}
