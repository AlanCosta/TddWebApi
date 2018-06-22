using AutoMapper;
using Dados.Models;

namespace CrudWebApi.Mappers
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            CreateMap<UsuarioDto, Usuario>();
        }
    }
}