using System;
using System.Collections.Generic;
using EntitiesServices.Model;  
using ModelServices.Interfaces.Repositories;
using System.Linq;
using EntitiesServices.Work_Classes;
using System.Data.Entity;

namespace DataServices.Repositories
{
    public class ClienteAnexoRepository : RepositoryBase<CLIENTE_ANEXO>, IClienteAnexoRepository
    {
        public List<CLIENTE_ANEXO> GetAllItens()
        {
            return Db.CLIENTE_ANEXO.ToList();
        }

        public CLIENTE_ANEXO GetItemById(Int32 id)
        {
            IQueryable<CLIENTE_ANEXO> query = Db.CLIENTE_ANEXO.Where(p => p.CLAN_CD_ID == id);
            return query.FirstOrDefault();
        }
    }
}
