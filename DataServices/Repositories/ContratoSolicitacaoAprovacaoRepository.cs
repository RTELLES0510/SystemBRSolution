using System;
using System.Collections.Generic;
using EntitiesServices.Model;  
using ModelServices.Interfaces.Repositories;
using System.Linq;
using EntitiesServices.Work_Classes;
using System.Data.Entity;
using CrossCutting;

namespace DataServices.Repositories
{
    public class ContratoSolicitacaoAprovacaoRepository : RepositoryBase<CONTRATO_SOLICITACAO_APROVACAO>, IContratoSolicitacaoAprovacaoRepository
    {
        public CONTRATO_SOLICITACAO_APROVACAO GetItemById(Int32 id)
        {
            IQueryable<CONTRATO_SOLICITACAO_APROVACAO> query = Db.CONTRATO_SOLICITACAO_APROVACAO;
            query = query.Where(p => p.CTSA_CD_ID == id);
            query = query.Include(p => p.CONTRATO);
            return query.FirstOrDefault();
        }

        public List<CONTRATO_SOLICITACAO_APROVACAO> GetAllItens()
        {
            Int32? idAss = SessionMocks.contrato.CONT_CD_ID;
            IQueryable<CONTRATO_SOLICITACAO_APROVACAO> query = Db.CONTRATO_SOLICITACAO_APROVACAO.Where(p => p.CTSA_IN_ATIVO == 1);
            query = query.Where(p => p.CONT_CD_ID == idAss);
            return query.ToList();
        }
    }
}
