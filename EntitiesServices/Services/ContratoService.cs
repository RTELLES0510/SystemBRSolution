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
    public class ContratoService : ServiceBase<CONTRATO>, IContratoService
    {
        private readonly IContratoRepository _baseRepository;
        private readonly ILogRepository _logRepository;
        private readonly ICategoriaContratoRepository _catRepository;
        private readonly IContratoAnexoRepository _anexoRepository;
        private readonly ITipoContratoRepository _tipoRepository;
        private readonly ITemplateRepository _tempRepository;
        private readonly IPeriodicidadeRepository _perRepository;
        private readonly IFormaPagamentoRepository _forRepository;
        private readonly IPlanoContaRepository _planRepository;
        private readonly ICentroCustoRepository _ccRepository;
        private readonly IColaboradorRepository _colRepository;
        private readonly INomenclaturaRepository _nomRepository;
        private readonly IClienteRepository _cliRepository;

        protected SystemBRDatabaseEntities Db = new SystemBRDatabaseEntities();

        public ContratoService(IContratoRepository baseRepository, ILogRepository logRepository, ICategoriaContratoRepository catRepository, IContratoAnexoRepository anexoRepository, ITipoContratoRepository tipoRepository, ITemplateRepository tempRepository, IPeriodicidadeRepository perRepository, IFormaPagamentoRepository forRepository, IPlanoContaRepository planRepository, ICentroCustoRepository ccRepository, IColaboradorRepository colRepository, INomenclaturaRepository nomRepository, IClienteRepository cliRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
            _logRepository = logRepository;
            _tipoRepository = tipoRepository;
            _anexoRepository = anexoRepository;
            _catRepository = catRepository;
            _tempRepository = tempRepository;
            _perRepository = perRepository;
            _forRepository = forRepository;
            _planRepository = planRepository;
            _ccRepository = ccRepository;
            _colRepository = colRepository;
            _nomRepository = nomRepository;
            _cliRepository = cliRepository;
        }

        public CONTRATO CheckExist(CONTRATO conta)
        {
            CONTRATO item = _baseRepository.CheckExist(conta);
            return item;
        }

        public CONTRATO GetItemById(Int32 id)
        {
            CONTRATO item = _baseRepository.GetItemById(id);
            return item;
        }

        public CONTRATO GetByNome(String nome)
        {
            CONTRATO item = _baseRepository.GetByNome(nome);
            return item;
        }

        public List<CONTRATO> GetAllItens()
        {
            return _baseRepository.GetAllItens();
        }

        public List<CONTRATO> GetAllItensAdm()
        {
            return _baseRepository.GetAllItensAdm();
        }

        public List<TIPO_CONTRATO> GetAllTipos()
        {
            return _tipoRepository.GetAllItens();
        }

        public List<CATEGORIA_CONTRATO> GetAllCategorias()
        {
            return _catRepository.GetAllItens();
        }

        public List<TEMPLATE> GetAllTemplates()
        {
            return _tempRepository.GetAllItens();
        }

        public List<PERIODICIDADE> GetAllPeriodicidades()
        {
            return _perRepository.GetAllItens();
        }

        public List<FORMA_PAGAMENTO> GetAllForma()
        {
            return _forRepository.GetAllItens();
        }

        public List<PLANO_CONTA> GetAllPlanoConta()
        {
            return _planRepository.GetAllItens();
        }

        public List<CENTRO_CUSTO> GetAllCentros()
        {
            return _ccRepository.GetAllItens();
        }

        public List<COLABORADOR> GetAllVendedores()
        {
            return _colRepository.GetAllItens();
        }

        public List<NOMENCLATURA_BRAS_SERVICOS> GetAllNomenclatura()
        {
            return _nomRepository.GetAllItens();
        }

        public List<CLIENTE> GetAllClientes()
        {
            return _cliRepository.GetAllItens();
        }

        public CONTRATO_ANEXO GetAnexoById(Int32 id)
        {
            return _anexoRepository.GetItemById(id);
        }

        public List<CONTRATO> ExecuteFilter(Int32? catId, Int32? tipoId, String nome, String descricao)
        {
            return _baseRepository.ExecuteFilter(catId, tipoId, nome, descricao);

        }

        public Int32 Create(CONTRATO item, LOG log)
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

        public Int32 Create(CONTRATO item)
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


        public Int32 Edit(CONTRATO item, LOG log)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    CONTRATO obj = _baseRepository.GetById(item.CONT_CD_ID);
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

        public Int32 Edit(CONTRATO item)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    CONTRATO obj = _baseRepository.GetById(item.CONT_CD_ID);
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

        public Int32 Delete(CONTRATO item, LOG log)
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
