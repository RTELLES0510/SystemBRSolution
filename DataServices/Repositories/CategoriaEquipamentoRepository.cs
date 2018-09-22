using System;
using System.Collections.Generic;
using EntitiesServices.Model;  
using ModelServices.Interfaces.Repositories;
using System.Linq;
using EntitiesServices.Work_Classes;
using System.Data.Entity;

namespace DataServices.Repositories
{
    public class CategoriaEquipamentoRepository : RepositoryBase<CATEGORIA_EQUIPAMENTO>, ICategoriaEquipamentoRepository
    {
        public CATEGORIA_EQUIPAMENTO GetItemById(Int32 id)
        {
            IQueryable<CATEGORIA_EQUIPAMENTO> query = Db.CATEGORIA_EQUIPAMENTO;
            query = query.Where(p => p.CAEQ_CD_ID == id);
            return query.FirstOrDefault();
        }

        public List<CATEGORIA_EQUIPAMENTO> GetAllItens()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<CATEGORIA_EQUIPAMENTO> query = Db.CATEGORIA_EQUIPAMENTO;
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            return query.ToList();
        }
    }
}
