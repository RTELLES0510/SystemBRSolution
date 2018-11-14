using System;
using System.Collections.Generic;
using EntitiesServices.Model;  
using ModelServices.Interfaces.Repositories;
using System.Linq;
using EntitiesServices.Work_Classes;
using System.Data.Entity;

namespace DataServices.Repositories
{
    public class SubcategoriaMateriaRepository : RepositoryBase<SUBCATEGORIA_MATERIA>, ISubcategoriaMateriaRepository
    {
        public SUBCATEGORIA_MATERIA GetItemById(Int32 id)
        {
            IQueryable<SUBCATEGORIA_MATERIA> query = Db.SUBCATEGORIA_MATERIA;
            query = query.Where(p => p.SCMA_CD_ID == id);
            return query.FirstOrDefault();
        }

        public List<SUBCATEGORIA_MATERIA> GetAllItens()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<SUBCATEGORIA_MATERIA> query = Db.SUBCATEGORIA_MATERIA;
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            return query.ToList();
        }

        public List<SUBCATEGORIA_MATERIA> GetItensByCategoria(Int32 cat)
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<SUBCATEGORIA_MATERIA> query = Db.SUBCATEGORIA_MATERIA;
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            query = query.Where(p => p.CAMA_CD_ID == cat);
            return query.ToList();
        }
    }
}
