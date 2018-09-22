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
    public class PatrimonioService : ServiceBase<PATRIMONIO>, IPatrimonioService
    {
        private readonly IPatrimonioRepository _baseRepository;
        private readonly ILogRepository _logRepository;
        private readonly ICategoriaPatrimonioRepository _tipoRepository;
        private readonly IPatrimonioAnexoRepository _anexoRepository;
        private readonly IFilialRepository _filialRepository;

        protected SystemBRDatabaseEntities Db = new SystemBRDatabaseEntities();

        public PatrimonioService(IPatrimonioRepository baseRepository, ILogRepository logRepository, ICategoriaPatrimonioRepository tipoRepository, IPatrimonioAnexoRepository anexoRepository, IFilialRepository filialRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
            _logRepository = logRepository;
            _tipoRepository = tipoRepository;
            _anexoRepository = anexoRepository;
            _filialRepository = filialRepository;
        }

        public PATRIMONIO CheckExist(PATRIMONIO conta)
        {
            PATRIMONIO item = _baseRepository.CheckExist(conta);
            return item;
        }

        public PATRIMONIO GetItemById(Int32 id)
        {
            PATRIMONIO item = _baseRepository.GetItemById(id);
            return item;
        }

        public PATRIMONIO GetByNumero(String numero)
        {
            PATRIMONIO item = _baseRepository.GetByNumero(numero);
            return item;
        }

        public List<PATRIMONIO> GetAllItens()
        {
            return _baseRepository.GetAllItens();
        }

        public List<PATRIMONIO> GetAllItensAdm()
        {
            return _baseRepository.GetAllItensAdm();
        }

        public List<CATEGORIA_PATRIMONIO> GetAllTipos()
        {
            return _tipoRepository.GetAllItens();
        }

        public List<FILIAL> GetAllFilial()
        {
            return _filialRepository.GetAllItens();
        }

        public PATRIMONIO_ANEXO GetAnexoById(Int32 id)
        {
            return _anexoRepository.GetItemById(id);
        }

        public List<PATRIMONIO> ExecuteFilter(Int32? catId, String nome, String numero, Int32? filiId)
        {
            return _baseRepository.ExecuteFilter(catId, nome, numero, filiId);

        }

        public Int32 Create(PATRIMONIO item, LOG log)
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

        public Int32 Create(PATRIMONIO item)
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


        public Int32 Edit(PATRIMONIO item, LOG log)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    PATRIMONIO obj = _baseRepository.GetById(item.PATR_CD_ID);
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

        public Int32 Edit(PATRIMONIO item)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    PATRIMONIO obj = _baseRepository.GetById(item.PATR_CD_ID);
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

        public Int32 Delete(PATRIMONIO item, LOG log)
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
