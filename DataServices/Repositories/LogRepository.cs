using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;
using ModelServices.Interfaces.Repositories;

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
            IQueryable<LOG> query = Db.LOG.Where(p => p.LOG_IN_ATIVO == 1);
            return query.ToList();
        }

        public List<LOG> ExecuteFilter(Int32? usuId, DateTime data, String operacao)
        {
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
            if (data != DateTime.MinValue)
            {
                query = query.Where(p => p.LOG_DT_DATA == data);
            }
            if (query != null)
            {
                query = query.OrderByDescending(a => a.LOG_DT_DATA);
                lista = query.ToList<LOG>();
            }
            return lista;
        }
    }
}
 