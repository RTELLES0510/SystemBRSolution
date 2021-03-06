using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using EntitiesServices.Model;
using SystemBRPresentation.ViewModels;

namespace MvcMapping.Mappers
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<UsuarioViewModel, USUARIO>();
            CreateMap<LogViewModel, LOG>();
            CreateMap<ConfiguracaoViewModel, CONFIGURACAO>();
            CreateMap<BancoViewModel, BANCO>();
            CreateMap<ContaBancariaViewModel, CONTA_BANCARIA>();
            CreateMap<MatrizViewModel, MATRIZ>();
            CreateMap<FilialViewModel, FILIAL>();
            CreateMap<ClienteViewModel, CLIENTE>();
            CreateMap<FornecedorViewModel, FORNECEDOR>();
            CreateMap<ProdutoViewModel, PRODUTO>();
            CreateMap<MateriaPrimaViewModel, MATERIA_PRIMA>();
            CreateMap<ServicoViewModel, SERVICO>();
            CreateMap<TransportadoraViewModel, TRANSPORTADORA>();
            CreateMap<EquipamentoViewModel, EQUIPAMENTO>();
            CreateMap<PatrimonioViewModel, PATRIMONIO>();
            CreateMap<CargoViewModel, CARGO>();
            CreateMap<ValorComissaoViewModel, VALOR_COMISSAO>();
            CreateMap<ClienteContatoViewModel, CLIENTE_CONTATO>();
            CreateMap<ClienteReferenciaViewModel, CLIENTE_REFERENCIA>();
            CreateMap<ClienteTagViewModel, CLIENTE_TAG>();
            CreateMap<ProdutoFornecedorViewModel, PRODUTO_FORNECEDOR>();
            CreateMap<ProdutoGradeViewModel, PRODUTO_GRADE>();
            CreateMap<ContratoViewModel, CONTRATO>();
            CreateMap<ContratoSolicitacaoAprovacaoViewModel, CONTRATO_SOLICITACAO_APROVACAO>();
            CreateMap<FornecedorContatoViewModel, FORNECEDOR_CONTATO>();
        }
    }
}