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
        }
    }
}