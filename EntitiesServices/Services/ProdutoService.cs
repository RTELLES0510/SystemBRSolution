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
    public class ProdutoService : ServiceBase<PRODUTO>, IProdutoService
    {
        private readonly IProdutoRepository _baseRepository;
        private readonly ILogRepository _logRepository;
        private readonly ICategoriaProdutoRepository _tipoRepository;
        private readonly IProdutoAnexoRepository _anexoRepository;
        private readonly IFilialRepository _filialRepository;
        private readonly IUnidadeRepository _unidRepository;
        private readonly IMovimentoEstoqueProdutoRepository _movRepository;

        protected SystemBRDatabaseEntities Db = new SystemBRDatabaseEntities();

        public ProdutoService(IProdutoRepository baseRepository, ILogRepository logRepository, ICategoriaProdutoRepository tipoRepository, IProdutoAnexoRepository anexoRepository, IFilialRepository filialRepository, IUnidadeRepository unidRepository, IMovimentoEstoqueProdutoRepository movRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
            _logRepository = logRepository;
            _tipoRepository = tipoRepository;
            _anexoRepository = anexoRepository;
            _filialRepository = filialRepository;
            _unidRepository = unidRepository;
            _movRepository = movRepository;
        }

        public PRODUTO CheckExist(PRODUTO conta)
        {
            PRODUTO item = _baseRepository.CheckExist(conta);
            return item;
        }

        public PRODUTO GetItemById(Int32 id)
        {
            PRODUTO item = _baseRepository.GetItemById(id);
            return item;
        }

        public PRODUTO GetByNome(String nome)
        {
            PRODUTO item = _baseRepository.GetByNome(nome);
            return item;
        }

        public List<PRODUTO> GetAllItens()
        {
            return _baseRepository.GetAllItens();
        }

        public List<PRODUTO> GetAllItensAdm()
        {
            return _baseRepository.GetAllItensAdm();
        }

        public List<CATEGORIA_PRODUTO> GetAllTipos()
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

        public PRODUTO_ANEXO GetAnexoById(Int32 id)
        {
            return _anexoRepository.GetItemById(id);
        }

        public List<PRODUTO> ExecuteFilter(Int32? catId, String nome, String descricao, Int32? filiId)
        {
            return _baseRepository.ExecuteFilter(catId, nome, descricao, filiId);

        }

        public Int32 Create(PRODUTO item, LOG log, MOVIMENTO_ESTOQUE_PRODUTO movto)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    _logRepository.Add(log);
                    _baseRepository.Add(item);
                    _movRepository.Add(movto);
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

        public Int32 Create(PRODUTO item)
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


        public Int32 Edit(PRODUTO item, LOG log)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    PRODUTO obj = _baseRepository.GetById(item.PROD_CD_ID);
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

        public Int32 Edit(PRODUTO item)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    PRODUTO obj = _baseRepository.GetById(item.PROD_CD_ID);
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

        public Int32 Delete(PRODUTO item, LOG log)
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
