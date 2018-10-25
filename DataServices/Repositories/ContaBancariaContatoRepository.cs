using System;
using System.Collections.Generic;
using EntitiesServices.Model;  
using ModelServices.Interfaces.Repositories;
using System.Linq;
using EntitiesServices.Work_Classes;
using System.Data.Entity;

namespace DataServices.Repositories
{
    public class ContaBancariaContatoRepository : RepositoryBase<CONTA_BANCARIA_CONTATO>, IContaBancariaContatoRepository
    {
        public List<CONTA_BANCARIA_CONTATO> GetAllItens()
        {
            return Db.CONTA_BANCARIA_CONTATO.ToList();
        }

        public CONTA_BANCARIA_CONTATO GetItemById(Int32 id)
        {
            IQueryable<CONTA_BANCARIA_CONTATO> query = Db.CONTA_BANCARIA_CONTATO.Where(p => p.CBCT_CD_ID == id);
            return query.FirstOrDefault();
        }
    }
}
