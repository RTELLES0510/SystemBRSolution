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
    public class MovimentoEstoqueProdutoService : ServiceBase<MOVIMENTO_ESTOQUE_PRODUTO>, IMovimentoEstoqueProdutoService
    {
        private readonly IClienteRepository _baseRepository;
        private readonly ILogRepository _logRepository;
        private readonly ICategoriaClienteRepository _tipoRepository;
        private readonly IClienteAnexoRepository _anexoRepository;
        private readonly IFilialRepository _filialRepository;
        protected SystemBRDatabaseEntities Db = new SystemBRDatabaseEntities();

        public ClienteService(IClienteRepository baseRepository, ILogRepository logRepository, ICategoriaClienteRepository tipoRepository, IClienteAnexoRepository anexoRepository, IFilialRepository filialRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
            _logRepository = logRepository;
            _tipoRepository = tipoRepository;
            _anexoRepository = anexoRepository;
            _filialRepository = filialRepository;
        }

        public CLIENTE CheckExist(CLIENTE conta)
        {
            CLIENTE item = _baseRepository.CheckExist(conta);
            return item;
        }

        public CLIENTE GetItemById(Int32 id)
        {
            CLIENTE item = _baseRepository.GetItemById(id);
            return item;
        }

        public CLIENTE GetByEmail(String email)
        {
            CLIENTE item = _baseRepository.GetByEmail(email);
            return item;
        }

        public List<CLIENTE> GetAllItens()
        {
            return _baseRepository.GetAllItens();
        }

        public List<CLIENTE> GetAllItensAdm()
        {
            return _baseRepository.GetAllItensAdm();
        }

        public List<CATEGORIA_CLIENTE> GetAllTipos()
        {
            return _tipoRepository.GetAllItens();
        }

        public List<FILIAL> GetAllFilial()
        {
            return _filialRepository.GetAllItens();
        }

        public CLIENTE_ANEXO GetAnexoById(Int32 id)
        {
            return _anexoRepository.GetItemById(id);
        }

        public List<CLIENTE> ExecuteFilter(Int32? catId, String nome, String cpf, String cnpj, String email, String cidade, String uf, String rede)
        {
            return _baseRepository.ExecuteFilter(catId, nome, cpf, cnpj, email, cidade, uf, rede);

        }

        public Int32 Create(CLIENTE item, LOG log)
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

        public Int32 Create(CLIENTE item)
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


        public Int32 Edit(CLIENTE item, LOG log)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    CLIENTE obj = _baseRepository.GetById(item.CLIE_CD_ID);
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

        public Int32 Edit(CLIENTE item)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    CLIENTE obj = _baseRepository.GetById(item.CLIE_CD_ID);
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

        public Int32 Delete(CLIENTE item, LOG log)
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
