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
    public class ContratoSolicitacaoAprovacaoService : ServiceBase<CONTRATO_SOLICITACAO_APROVACAO>, IContratoSolicitacaoAprovacaoService
    {
        private readonly IContratoSolicitacaoAprovacaoRepository _baseRepository;
        private readonly ILogRepository _logRepository;

        protected SystemBRDatabaseEntities Db = new SystemBRDatabaseEntities();

        public ContratoSolicitacaoAprovacaoService(IContratoSolicitacaoAprovacaoRepository baseRepository, ILogRepository logRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
            _logRepository = logRepository;
        }

        public CONTRATO_SOLICITACAO_APROVACAO GetItemById(Int32 id)
        {
            CONTRATO_SOLICITACAO_APROVACAO item = _baseRepository.GetItemById(id);
            return item;
        }

        public List<CONTRATO_SOLICITACAO_APROVACAO> GetAllItens()
        {
            return _baseRepository.GetAllItens();
        }

        public Int32 Create(CONTRATO_SOLICITACAO_APROVACAO item, LOG log)
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

        public Int32 Create(CONTRATO_SOLICITACAO_APROVACAO item)
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


        public Int32 Edit(CONTRATO_SOLICITACAO_APROVACAO item, LOG log)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    CONTRATO_SOLICITACAO_APROVACAO obj = _baseRepository.GetById(item.CTSA_CD_ID);
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

        public Int32 Edit(CONTRATO_SOLICITACAO_APROVACAO item)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    CONTRATO_SOLICITACAO_APROVACAO obj = _baseRepository.GetById(item.CTSA_CD_ID);
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

        public Int32 Delete(CONTRATO_SOLICITACAO_APROVACAO item, LOG log)
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
