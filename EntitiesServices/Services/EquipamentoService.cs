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
    public class EquipamentoService : ServiceBase<EQUIPAMENTO>, IEquipamentoService
    {
        private readonly IEquipementoRepository _baseRepository;
        private readonly ILogRepository _logRepository;
        private readonly ICategoriaEquipamentoRepository _tipoRepository;
        private readonly IEquipamentoAnexoRepository _anexoRepository;
        private readonly IEquipamentoManutencaoRepository _manRepository;
        private readonly IPeriodicidadeRepository _perRepository;
        private readonly IFilialRepository _filialRepository;

        protected SystemBRDatabaseEntities Db = new SystemBRDatabaseEntities();

        public EquipamentoService(IEquipementoRepository baseRepository, ILogRepository logRepository, ICategoriaEquipamentoRepository tipoRepository, IEquipamentoAnexoRepository anexoRepository, IFilialRepository filialRepository, IPeriodicidadeRepository perRepository, IEquipamentoManutencaoRepository manRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
            _logRepository = logRepository;
            _tipoRepository = tipoRepository;
            _anexoRepository = anexoRepository;
            _filialRepository = filialRepository;
            _perRepository = perRepository;
            _manRepository = manRepository;
        }

        public EQUIPAMENTO CheckExist(EQUIPAMENTO conta)
        {
            EQUIPAMENTO item = _baseRepository.CheckExist(conta);
            return item;
        }

        public EQUIPAMENTO GetItemById(Int32 id)
        {
            EQUIPAMENTO item = _baseRepository.GetItemById(id);
            return item;
        }

        public EQUIPAMENTO_MANUTENCAO GetItemManutencaoById(Int32 id)
        {
            EQUIPAMENTO_MANUTENCAO item = _manRepository.GetItemById(id);
            return item;
        }

        public EQUIPAMENTO GetByNumero(String numero)
        {
            EQUIPAMENTO item = _baseRepository.GetByNumero(numero);
            return item;
        }

        public List<EQUIPAMENTO> GetAllItens()
        {
            return _baseRepository.GetAllItens();
        }

        public List<EQUIPAMENTO> GetAllItensAdm()
        {
            return _baseRepository.GetAllItensAdm();
        }

        public List<CATEGORIA_EQUIPAMENTO> GetAllTipos()
        {
            return _tipoRepository.GetAllItens();
        }

        public List<PERIODICIDADE> GetAllPeriodicidades()
        {
            return _perRepository.GetAllItens();
        }

        public List<FILIAL> GetAllFilial()
        {
            return _filialRepository.GetAllItens();
        }

        public EQUIPAMENTO_ANEXO GetAnexoById(Int32 id)
        {
            return _anexoRepository.GetItemById(id);
        }

        public List<EQUIPAMENTO> ExecuteFilter(Int32? catId, String nome, String numero, Int32? filiId)
        {
            return _baseRepository.ExecuteFilter(catId, nome, numero, filiId);

        }

        public Int32 CalcularManutencaoVencida()
        {
            return _baseRepository.CalcularManutencaoVencida();
        }

        public Int32 CalcularDepreciados()
        {
            return _baseRepository.CalcularDepreciados();
        }

        public Int32 Create(EQUIPAMENTO item, LOG log)
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

        public Int32 Create(EQUIPAMENTO item)
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


        public Int32 Edit(EQUIPAMENTO item, LOG log)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    EQUIPAMENTO obj = _baseRepository.GetById(item.EQUI_CD_ID);
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

        public Int32 Edit(EQUIPAMENTO item)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    EQUIPAMENTO obj = _baseRepository.GetById(item.EQUI_CD_ID);
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

        public Int32 Delete(EQUIPAMENTO item, LOG log)
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
