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
        }
    }
}