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
    public class EquipamentoRepository : RepositoryBase<EQUIPAMENTO>, IEquipementoRepository
    {
        public EQUIPAMENTO CheckExist(EQUIPAMENTO conta)
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<EQUIPAMENTO> query = Db.EQUIPAMENTO;
            query = query.Where(p => p.EQUI_NR_NUMERO == conta.EQUI_NR_NUMERO);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            return query.FirstOrDefault();
        }

        public EQUIPAMENTO GetByNumero(String numero)
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<EQUIPAMENTO> query = Db.EQUIPAMENTO.Where(p => p.EQUI_IN_ATIVO == 1);
            query = query.Where(p => p.EQUI_NR_NUMERO == numero);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            query = query.Include(p => p.ASSINANTE);
            query = query.Include(p => p.MATRIZ);
            query = query.Include(p => p.FILIAL);
            return query.FirstOrDefault();
        }

        public EQUIPAMENTO GetItemById(Int32 id)
        {
            IQueryable<EQUIPAMENTO> query = Db.EQUIPAMENTO;
            query = query.Where(p => p.EQUI_CD_ID == id);
            query = query.Include(p => p.ASSINANTE);
            query = query.Include(p => p.MATRIZ);
            query = query.Include(p => p.FILIAL);
            return query.FirstOrDefault();
        }

        public List<EQUIPAMENTO> GetAllItens()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<EQUIPAMENTO> query = Db.EQUIPAMENTO.Where(p => p.EQUI_IN_ATIVO == 1);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            query = query.Include(p => p.ASSINANTE);
            query = query.Include(p => p.MATRIZ);
            query = query.Include(p => p.FILIAL);
            return query.ToList();
        }

        public List<EQUIPAMENTO> GetAllItensAdm()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<EQUIPAMENTO> query = Db.EQUIPAMENTO;
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            query = query.Include(p => p.ASSINANTE);
            query = query.Include(p => p.MATRIZ);
            query = query.Include(p => p.FILIAL);
            return query.ToList();
        }

        public List<EQUIPAMENTO> ExecuteFilter(Int32? catId, String nome, String numero, Int32? filiId)
        {
            Int32? idAss = SessionMocks.IdAssinante;
            List<EQUIPAMENTO> lista = new List<EQUIPAMENTO>();
            IQueryable<EQUIPAMENTO> query = Db.EQUIPAMENTO;
            if (catId != null)
            {
                query = query.Where(p => p.CATEGORIA_EQUIPAMENTO.CAEQ_CD_ID == catId);
            }
            if (!String.IsNullOrEmpty(nome))
            {
                query = query.Where(p => p.EQUI_NM_NOME.Contains(nome));
            }
            if (!String.IsNullOrEmpty(numero))
            {
                query = query.Where(p => p.EQUI_NR_NUMERO == numero);
            }
            if (filiId != null)
            {
                query = query.Where(p => p.FILIAL.FILI_CD_ID == filiId);
            }

            if (query != null)
            {
                query = query.Where(p => p.ASSI_CD_ID == idAss);
                query = query.OrderBy(a => a.EQUI_NR_NUMERO);
                lista = query.ToList<EQUIPAMENTO>();
            }
            return lista;
        }
    }
}
