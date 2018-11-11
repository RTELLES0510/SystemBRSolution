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
    public class NoticiaRepository : RepositoryBase<NOTICIA>, INoticiaRepository
    {
        public NOTICIA GetItemById(Int32 id)
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<NOTICIA> query = Db.NOTICIA.Where(p => p.NOTC_IN_ATIVO == 1);
            query = query.Where(p => p.NOTC_CD_ID == id);
            query = query.Include(p => p.NOTICIA_TAG);
            query = query.Include(p => p.NOTICIA_AVALIACAO);
            query = query.Include(p => p.NOTICIA_COMENTARIO);
            return query.FirstOrDefault();
        }

        public List<NOTICIA> GetAllItens()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<NOTICIA> query = Db.NOTICIA.Where(p => p.NOTC_IN_ATIVO == 1);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            query = query.Include(p => p.NOTICIA_TAG);
            query = query.Include(p => p.NOTICIA_AVALIACAO);
            query = query.Include(p => p.NOTICIA_COMENTARIO);
            return query.ToList();
        }
    }
}
