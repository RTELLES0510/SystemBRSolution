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
    public class MateriaPrimaRepository : RepositoryBase<MATERIA_PRIMA>, IMateriaPrimaRepository
    {
        public MATERIA_PRIMA CheckExist(MATERIA_PRIMA conta)
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<MATERIA_PRIMA> query = Db.MATERIA_PRIMA;
            query = query.Where(p => p.MAPR_NM_NOME == conta.MAPR_NM_NOME);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            return query.FirstOrDefault();
        }

        public MATERIA_PRIMA GetByNome(String nome)
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<MATERIA_PRIMA> query = Db.MATERIA_PRIMA.Where(p => p.MAPR_IN_ATIVO == 1);
            query = query.Where(p => p.MAPR_NM_NOME == nome);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            query = query.Include(p => p.ASSINANTE);
            query = query.Include(p => p.MATRIZ);
            query = query.Include(p => p.FILIAL);
            return query.FirstOrDefault();
        }

        public MATERIA_PRIMA GetItemById(Int32 id)
        {
            IQueryable<MATERIA_PRIMA> query = Db.MATERIA_PRIMA;
            query = query.Where(p => p.MAPR_CD_ID == id);
            query = query.Include(p => p.ASSINANTE);
            query = query.Include(p => p.MATRIZ);
            query = query.Include(p => p.FILIAL);
            return query.FirstOrDefault();
        }

        public List<MATERIA_PRIMA> GetAllItens()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<MATERIA_PRIMA> query = Db.MATERIA_PRIMA.Where(p => p.MAPR_IN_ATIVO == 1);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            query = query.Include(p => p.ASSINANTE);
            query = query.Include(p => p.MATRIZ);
            query = query.Include(p => p.FILIAL);
            return query.ToList();
        }

        public List<MATERIA_PRIMA> GetAllItensAdm()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<MATERIA_PRIMA> query = Db.MATERIA_PRIMA;
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            query = query.Include(p => p.ASSINANTE);
            query = query.Include(p => p.MATRIZ);
            query = query.Include(p => p.FILIAL);
            return query.ToList();
        }

        public List<MATERIA_PRIMA> ExecuteFilter(Int32? catId, String nome, String descricao, String codigo)
        {
            Int32? idAss = SessionMocks.IdAssinante;
            List<MATERIA_PRIMA> lista = new List<MATERIA_PRIMA>();
            IQueryable<MATERIA_PRIMA> query = Db.MATERIA_PRIMA;
            if (catId != 0)
            {
                query = query.Where(p => p.CATEGORIA_MATERIA.CAMA_CD_ID == catId);
            }
            if (!String.IsNullOrEmpty(nome))
            {
                query = query.Where(p => p.MAPR_NM_NOME.Contains(nome));
            }
            if (!String.IsNullOrEmpty(descricao))
            {
                query = query.Where(p => p.MAPR_DS_DESCRICAO.Contains(descricao));
            }
            if (!String.IsNullOrEmpty(codigo))
            {
                query = query.Where(p => p.MAPR_CD_CODIGO == codigo);
            }

            if (query != null)
            {
                query = query.Where(p => p.ASSI_CD_ID == idAss);
                query = query.OrderBy(a => a.MAPR_NM_NOME);
                lista = query.ToList<MATERIA_PRIMA>();
            }
            return lista;
        }
    }
}
