using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using EntitiesServices.Model;
using SystemBRPresentation.ViewModels;

namespace MvcMapping.Mappers
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<USUARIO, UsuarioViewModel>();
            CreateMap<LOG, LogViewModel>();
            CreateMap<CONFIGURACAO, ConfiguracaoViewModel>();
            CreateMap<BANCO, BancoViewModel>();
            CreateMap<CONTA_BANCARIA, ContaBancariaViewModel>();
            CreateMap<MATRIZ, MatrizViewModel>();
            CreateMap<FILIAL, FilialViewModel>();
            CreateMap<CLIENTE, ClienteViewModel>();
            CreateMap<FORNECEDOR, FornecedorViewModel>();
            CreateMap<PRODUTO, ProdutoViewModel>();
            CreateMap<MATERIA_PRIMA, MateriaPrimaViewModel>();
            CreateMap<SERVICO, ServicoViewModel>();
            CreateMap<TRANSPORTADORA, TransportadoraViewModel>();
            CreateMap<EQUIPAMENTO, EquipamentoViewModel>();
            CreateMap<PATRIMONIO, PatrimonioViewModel>();
            CreateMap<CARGO, CargoViewModel>();
            CreateMap<VALOR_COMISSAO, ValorComissaoViewModel>();
            CreateMap<CLIENTE_CONTATO, ClienteContatoViewModel>();
            CreateMap<CLIENTE_REFERENCIA, ClienteReferenciaViewModel>();
            CreateMap<CLIENTE_TAG, ClienteTagViewModel>();
            CreateMap<PRODUTO_FORNECEDOR, ProdutoFornecedorViewModel>();
            CreateMap<PRODUTO_GRADE, ProdutoGradeViewModel>();
        }
    }
}
