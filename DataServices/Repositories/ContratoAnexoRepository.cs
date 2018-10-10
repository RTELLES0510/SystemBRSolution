using System;
using System.Collections.Generic;
using EntitiesServices.Model;  
using ModelServices.Interfaces.Repositories;
using System.Linq;
using EntitiesServices.Work_Classes;
using System.Data.Entity;

namespace DataServices.Repositories
{
    public class ContratoAnexoRepository : RepositoryBase<CONTRATO_ANEXO>, IContratoAnexoRepository
    {
        public List<CONTRATO_ANEXO> GetAllItens()
        {
            return Db.CONTRATO_ANEXO.ToList();
        }

        public CONTRATO_ANEXO GetItemById(Int32 id)
        {
            IQueryable<CONTRATO_ANEXO> query = Db.CONTRATO_ANEXO.Where(p => p.COAN_CD_ID == id);
            return query.FirstOrDefault();
        }
    }
}
