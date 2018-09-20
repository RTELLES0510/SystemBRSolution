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
    public class ServicoRepository : RepositoryBase<SERVICO>, IServicoRepository
    {
        public SERVICO CheckExist(SERVICO conta)
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<SERVICO> query = Db.SERVICO;
            query = query.Where(p => p.SERV_NM_NOME == conta.SERV_NM_NOME);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            return query.FirstOrDefault();
        }

        public SERVICO GetByNome(String nome)
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<SERVICO> query = Db.SERVICO.Where(p => p.SERV_IN_ATIVO == 1);
            query = query.Where(p => p.SERV_NM_NOME == nome);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            query = query.Include(p => p.ASSINANTE);
            query = query.Include(p => p.MATRIZ);
            query = query.Include(p => p.FILIAL);
            return query.FirstOrDefault();
        }

        public SERVICO GetItemById(Int32 id)
        {
            IQueryable<SERVICO> query = Db.SERVICO;
            query = query.Where(p => p.SERV_CD_ID == id);
            query = query.Include(p => p.ASSINANTE);
            query = query.Include(p => p.MATRIZ);
            query = query.Include(p => p.FILIAL);
            return query.FirstOrDefault();
        }

        public List<SERVICO> GetAllItens()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<SERVICO> query = Db.SERVICO.Where(p => p.SERV_IN_ATIVO == 1);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            query = query.Include(p => p.ASSINANTE);
            query = query.Include(p => p.MATRIZ);
            query = query.Include(p => p.FILIAL);
            return query.ToList();
        }

        public List<SERVICO> GetAllItensAdm()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<SERVICO> query = Db.SERVICO;
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            query = query.Include(p => p.ASSINANTE);
            query = query.Include(p => p.MATRIZ);
            query = query.Include(p => p.FILIAL);
            return query.ToList();
        }

        public List<SERVICO> ExecuteFilter(Int32? catId, String nome, String descricao, Int32? filiId)
        {
            Int32? idAss = SessionMocks.IdAssinante;
            List<SERVICO> lista = new List<SERVICO>();
            IQueryable<SERVICO> query = Db.SERVICO;
            if (catId != null)
            {
                query = query.Where(p => p.CATEGORIA_SERVICO.CASE_CD_ID == catId);
            }
            if (!String.IsNullOrEmpty(nome))
            {
                query = query.Where(p => p.SERV_NM_NOME.Contains(nome));
            }
            if (!String.IsNullOrEmpty(descricao))
            {
                query = query.Where(p => p.SERV_DS_DESCRICAO.Contains(descricao));
            }
            if (filiId != null)
            {
                query = query.Where(p => p.FILIAL.FILI_CD_ID == filiId);
            }

            if (query != null)
            {
                query = query.Where(p => p.ASSI_CD_ID == idAss);
                query = query.OrderBy(a => a.SERV_NM_NOME);
                lista = query.ToList<SERVICO>();
            }
            return lista;
        }
    }
}
