using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using EntitiesServices.Model;
using EntitiesServices.Work_Classes;
using ModelServices.Interfaces.Repositories;
using ModelServices.Interfaces.EntitiesServices;
using CrossCutting;
using System.Data.Entity;
using System.Data;

namespace ModelServices.EntitiesServices
{
    public class CargoService : ServiceBase<CARGO>, ICargoService
    {
        private readonly ICargoRepository _baseRepository;
        private readonly ILogRepository _logRepository;
        private readonly IValorComissaoRepository _valorRepository;
        protected SystemBRDatabaseEntities Db = new SystemBRDatabaseEntities();

        public CargoService(ICargoRepository baseRepository, ILogRepository logRepository, IValorComissaoRepository valorRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
            _logRepository = logRepository;
            _valorRepository = valorRepository;
        }

        public CARGO GetByNome(String nome)
        {
            CARGO item = _baseRepository.GetByNome(nome);
            return item;
        }

        public CARGO GetItemById(Int32 id)
        {
            CARGO item = _baseRepository.GetItemById(id);
            return item;
        }

        public List<CARGO> GetAllItens()
        {
            return _baseRepository.GetAllItens();
        }

        public List<VALOR_COMISSAO> GetAllValores()
        {
            return _valorRepository.GetAllItens();
        }

        public List<CARGO> GetAllItensAdm()
        {
            return _baseRepository.GetAllItensAdm();
        }

        public List<CARGO> ExecuteFilter(String nome)
        {
            List<CARGO> lista = _baseRepository.ExecuteFilter(nome);
            return lista;
        }

        public Int32 Create(CARGO item, LOG log)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    _logRepository.Add(log);
                    _baseRepository.Add(item);
                    transaction.Commit();
                    return 0;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public Int32 Create(CARGO item)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    _baseRepository.Add(item);
                    transaction.Commit();
                    return 0;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }


        public Int32 Edit(CARGO item, LOG log)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    CARGO obj = _baseRepository.GetById(item.CARG_CD_ID);
                    _baseRepository.Detach(obj);
                    _logRepository.Add(log);
                    _baseRepository.Update(item);
                    transaction.Commit();
                    return 0;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public Int32 Edit(CARGO item)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    CARGO obj = _baseRepository.GetById(item.CARG_CD_ID);
                    _baseRepository.Detach(obj);
                    _baseRepository.Update(item);
                    transaction.Commit();
                    return 0;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public Int32 Delete(CARGO item, LOG log)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    _logRepository.Add(log);
                    _baseRepository.Remove(item);
                    transaction.Commit();
                    return 0;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}
