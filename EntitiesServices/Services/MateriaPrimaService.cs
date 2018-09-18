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
    public class MateriaPrimaService : ServiceBase<MATERIA_PRIMA>, IMateriaPrimaService
    {
        private readonly IMateriaPrimaRepository _baseRepository;
        private readonly ILogRepository _logRepository;
        private readonly ICategoriaMateriaPrimaRepository _tipoRepository;
        private readonly IMateriaPrimaAnexoRepository _anexoRepository;
        private readonly IFilialRepository _filialRepository;
        private readonly IUnidadeRepository _unidRepository;
        private readonly IMovimentoEstoqueMateriaRepository _movRepository;

        protected SystemBRDatabaseEntities Db = new SystemBRDatabaseEntities();

        public MateriaPrimaService(IMateriaPrimaRepository baseRepository, ILogRepository logRepository, ICategoriaMateriaPrimaRepository tipoRepository, IMateriaPrimaAnexoRepository anexoRepository, IFilialRepository filialRepository, IUnidadeRepository unidRepository, IMovimentoEstoqueMateriaRepository movRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
            _logRepository = logRepository;
            _tipoRepository = tipoRepository;
            _anexoRepository = anexoRepository;
            _filialRepository = filialRepository;
            _unidRepository = unidRepository;
            _movRepository = movRepository;
        }

        public MATERIA_PRIMA CheckExist(MATERIA_PRIMA conta)
        {
            MATERIA_PRIMA item = _baseRepository.CheckExist(conta);
            return item;
        }

        public MATERIA_PRIMA GetItemById(Int32 id)
        {
            MATERIA_PRIMA item = _baseRepository.GetItemById(id);
            return item;
        }

        public MATERIA_PRIMA GetByNome(String nome)
        {
            MATERIA_PRIMA item = _baseRepository.GetByNome(nome);
            return item;
        }

        public List<MATERIA_PRIMA> GetAllItens()
        {
            return _baseRepository.GetAllItens();
        }

        public List<MATERIA_PRIMA> GetAllItensAdm()
        {
            return _baseRepository.GetAllItensAdm();
        }

        public List<CATEGORIA_MATERIA> GetAllTipos()
        {
            return _tipoRepository.GetAllItens();
        }

        public List<UNIDADE> GetAllUnidades()
        {
            return _unidRepository.GetAllItens();
        }

        public List<FILIAL> GetAllFilial()
        {
            return _filialRepository.GetAllItens();
        }

        public MATERIA_PRIMA_ANEXO GetAnexoById(Int32 id)
        {
            return _anexoRepository.GetItemById(id);
        }

        public List<MATERIA_PRIMA> ExecuteFilter(Int32? catId, String nome, String descricao, Int32? filiId)
        {
            return _baseRepository.ExecuteFilter(catId, nome, descricao, filiId);

        }

        public Int32 Create(MATERIA_PRIMA item, LOG log, MOVIMENTO_ESTOQUE_MATERIA_PRIMA movto)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    _logRepository.Add(log);
                    _baseRepository.Add(item);
                    //_movRepository.Add(movto);
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

        public Int32 Create(MATERIA_PRIMA item)
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


        public Int32 Edit(MATERIA_PRIMA item, LOG log)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    MATERIA_PRIMA obj = _baseRepository.GetById(item.MAPR_CD_ID);
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

        public Int32 Edit(MATERIA_PRIMA item)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    MATERIA_PRIMA obj = _baseRepository.GetById(item.MAPR_CD_ID);
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

        public Int32 Delete(MATERIA_PRIMA item, LOG log)
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
