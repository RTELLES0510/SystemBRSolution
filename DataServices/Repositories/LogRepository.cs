using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;
using ModelServices.Interfaces.Repositories;
using EntitiesServices.Work_Classes;
using System.Data.Entity;

namespace DataServices.Repositories
{
    public class LogRepository : RepositoryBase<LOG>, ILogRepository
    {
        public LOG GetById(Int32 id)
        {
            IQueryable<LOG> query = Db.LOG.Where(p => p.LOG_CD_ID == id);
            query = query.Include(p => p.USUARIO);
            return query.FirstOrDefault();
        }

        public List<LOG> GetAllItens()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<LOG> query = Db.LOG.Where(p => p.LOG_IN_ATIVO == 1);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            query = query.OrderByDescending(a => a.LOG_DT_DATA);
            return query.ToList();
        }

        public List<LOG> ExecuteFilter(Int32? usuId, DateTime? data, String operacao)
        {
            Int32? idAss = SessionMocks.IdAssinante;
            List<LOG> lista = new List<LOG>();
            IQueryable<LOG> query = Db.LOG;
            if (!String.IsNullOrEmpty(operacao))
            {
                query = query.Where(p => p.LOG_NM_OPERACAO == operacao);
            }
            if (usuId != 0)
            {
                query = query.Where(p => p.USUARIO.USUA_CD_ID == usuId);
            }
            if (data != null)
            {
                query = query.Where(p => DbFunctions.TruncateTime(p.LOG_DT_DATA) == DbFunctions.TruncateTime(data));
            }
            if (query != null)
            {
                query = query.Where(p => p.ASSI_CD_ID == idAss);
                query = query.OrderByDescending(a => a.LOG_DT_DATA);
                lista = query.ToList<LOG>();
            }
                return lista;
        }
    }
}
 