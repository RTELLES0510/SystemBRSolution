using System;
using System.Collections.Generic;
using EntitiesServices.Model;  
using ModelServices.Interfaces.Repositories;
using System.Linq;
using EntitiesServices.Work_Classes;
using System.Data.Entity;

namespace DataServices.Repositories
{
    public class FormaPagamentoRepository : RepositoryBase<FORMA_PAGAMENTO>, IFormaPagamentoRepository
    {
        public FORMA_PAGAMENTO GetItemById(Int32 id)
        {
            IQueryable<FORMA_PAGAMENTO> query = Db.FORMA_PAGAMENTO;
            query = query.Where(p => p.FOPA_CD_ID == id);
            return query.FirstOrDefault();
        }

        public List<FORMA_PAGAMENTO> GetAllItens()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<FORMA_PAGAMENTO> query = Db.FORMA_PAGAMENTO;
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            return query.ToList();
        }
    }
}
