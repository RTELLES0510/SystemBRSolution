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
    public class ValorComissaoService : ServiceBase<VALOR_COMISSAO>, IValorComissaoService
    {
        private readonly IValorComissaoRepository _baseRepository;
        private readonly ILogRepository _logRepository;
        private readonly ITipoComissaoRepository _tipoRepository;
        private readonly IFilialRepository _filialRepository;
        private readonly ICategoriaProdutoRepository _catRepository;
        protected SystemBRDatabaseEntities Db = new SystemBRDatabaseEntities();

        public ValorComissaoService(IValorComissaoRepository baseRepository, ILogRepository logRepository, ITipoComissaoRepository tipoRepository, IFilialRepository filialRepository, ICategoriaProdutoRepository catRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
            _logRepository = logRepository;
            _tipoRepository = tipoRepository;
            _filialRepository = filialRepository;
            _catRepository = catRepository;
        }

        public VALOR_COMISSAO CheckExist(VALOR_COMISSAO conta)
        {
            VALOR_COMISSAO item = _baseRepository.CheckExist(conta);
            return item;
        }

        public VALOR_COMISSAO GetItemById(Int32 id)
        {
            VALOR_COMISSAO item = _baseRepository.GetItemById(id);
            return item;
        }

        public List<VALOR_COMISSAO> GetAllItens()
        {
            return _baseRepository.GetAllItens();
        }

        public List<VALOR_COMISSAO> GetAllItensAdm()
        {
            return _baseRepository.GetAllItensAdm();
        }

        public List<TIPO_COMISSAO> GetAllTipos()
        {
            return _tipoRepository.GetAllItens();
        }

        public List<CATEGORIA_PRODUTO> GetAllCategorias()
        {
            return _catRepository.GetAllItens();
        }

        public List<FILIAL> GetAllFiliais()
        {
            return _filialRepository.GetAllItens();
        }

        public List<VALOR_COMISSAO> ExecuteFilter(Int32? categoria, Int32? tipo, String nome)
        {
            List<VALOR_COMISSAO> lista = _baseRepository.ExecuteFilter(categoria, tipo, nome);
            return lista;
        }

        public Int32 Create(VALOR_COMISSAO item, LOG log)
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

        public Int32 Create(VALOR_COMISSAO item)
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


        public Int32 Edit(VALOR_COMISSAO item, LOG log)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    VALOR_COMISSAO obj = _baseRepository.GetById(item.VACO_CD_ID);
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

        public Int32 Edit(VALOR_COMISSAO item)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    VALOR_COMISSAO obj = _baseRepository.GetById(item.VACO_CD_ID);
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

        public Int32 Delete(VALOR_COMISSAO item, LOG log)
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
