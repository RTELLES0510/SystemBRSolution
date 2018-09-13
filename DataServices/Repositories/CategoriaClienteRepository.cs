using System;
using System.Collections.Generic;
using EntitiesServices.Model;  
using ModelServices.Interfaces.Repositories;
using System.Linq;
using EntitiesServices.Work_Classes;
using System.Data.Entity;

namespace DataServices.Repositories
{
    public class CategoriaClienteRepository : RepositoryBase<CATEGORIA_CLIENTE>, ICategoriaClienteRepository
    {
        public CATEGORIA_CLIENTE GetItemById(Int32 id)
        {
            IQueryable<CATEGORIA_CLIENTE> query = Db.CATEGORIA_CLIENTE;
            query = query.Where(p => p.CACL_CD_ID == id);
            return query.FirstOrDefault();
        }

        public List<CATEGORIA_CLIENTE> GetAllItens()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<CATEGORIA_CLIENTE> query = Db.CATEGORIA_CLIENTE;
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            return query.ToList();
        }
    }
}
