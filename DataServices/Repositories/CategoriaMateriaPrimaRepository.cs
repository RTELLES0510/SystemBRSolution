using System;
using System.Collections.Generic;
using EntitiesServices.Model;  
using ModelServices.Interfaces.Repositories;
using System.Linq;
using EntitiesServices.Work_Classes;
using System.Data.Entity;

namespace DataServices.Repositories
{
    public class CategoriaMateriaPrimaRepository : RepositoryBase<CATEGORIA_MATERIA>, ICategoriaMateriaPrimaRepository
    {
        public CATEGORIA_MATERIA GetItemById(Int32 id)
        {
            IQueryable<CATEGORIA_MATERIA> query = Db.CATEGORIA_MATERIA;
            query = query.Where(p => p.CAMA_CD_ID == id);
            return query.FirstOrDefault();
        }

        public List<CATEGORIA_MATERIA> GetAllItens()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<CATEGORIA_MATERIA> query = Db.CATEGORIA_MATERIA;
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            return query.ToList();
        }
    }
}
