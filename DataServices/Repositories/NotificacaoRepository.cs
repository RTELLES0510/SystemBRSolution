using System;
using System.Collections.Generic;
using EntitiesServices.Model;  
using ModelServices.Interfaces.Repositories;
using System.Linq;
using EntitiesServices.Work_Classes;
using System.Data.Entity;
using CrossCutting;

namespace DataServices.Repositories
{
    public class NotificacaoRepository : RepositoryBase<NOTIFICACAO>, INotificacaoRepository
    {
        public NOTIFICACAO GetItemById(Int32 id)
        {
            IQueryable<NOTIFICACAO> query = Db.NOTIFICACAO;
            query = query.Where(p => p.NOTI_CD_ID == id);
            return query.FirstOrDefault();
        }

        public List<NOTIFICACAO> GetAllItens()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<NOTIFICACAO> query = Db.NOTIFICACAO.Where(p => p.NOTI_IN_ATIVO == 1);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            return query.ToList();
        }

        public List<NOTIFICACAO> GetAllItensAdm()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<NOTIFICACAO> query = Db.NOTIFICACAO;
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            return query.ToList();
        }
    }
}
