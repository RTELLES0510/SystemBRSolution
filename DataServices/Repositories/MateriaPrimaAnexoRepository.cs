using System;
using System.Collections.Generic;
using EntitiesServices.Model;  
using ModelServices.Interfaces.Repositories;
using System.Linq;
using EntitiesServices.Work_Classes;
using System.Data.Entity;

namespace DataServices.Repositories
{
    public class MateriaPrimaAnexoRepository : RepositoryBase<MATERIA_PRIMA_ANEXO>, IMateriaPrimaAnexoRepository
    {
        public List<MATERIA_PRIMA_ANEXO> GetAllItens()
        {
            return Db.MATERIA_PRIMA_ANEXO.ToList();
        }

        public MATERIA_PRIMA_ANEXO GetItemById(Int32 id)
        {
            IQueryable<MATERIA_PRIMA_ANEXO> query = Db.MATERIA_PRIMA_ANEXO.Where(p => p.MAPA_CD_ID == id);
            return query.FirstOrDefault();
        }
    }
}
