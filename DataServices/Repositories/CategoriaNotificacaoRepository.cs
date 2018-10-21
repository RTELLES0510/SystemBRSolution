using System;
using System.Collections.Generic;
using EntitiesServices.Model;  
using ModelServices.Interfaces.Repositories;
using System.Linq;
using EntitiesServices.Work_Classes;
using System.Data.Entity;

namespace DataServices.Repositories
{
    public class CategoriaNotificacaoRepository : RepositoryBase<CATEGORIA_NOTIFICACAO>, ICategoriaNotificacaoRepository
    {
        public CATEGORIA_NOTIFICACAO GetItemById(Int32 id)
        {
            IQueryable<CATEGORIA_NOTIFICACAO> query = Db.CATEGORIA_NOTIFICACAO;
            query = query.Where(p => p.CANO_CD_ID == id);
            return query.FirstOrDefault();
        }

        public List<CATEGORIA_NOTIFICACAO> GetAllItens()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<CATEGORIA_NOTIFICACAO> query = Db.CATEGORIA_NOTIFICACAO;
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            return query.ToList();
        }
    }
}
