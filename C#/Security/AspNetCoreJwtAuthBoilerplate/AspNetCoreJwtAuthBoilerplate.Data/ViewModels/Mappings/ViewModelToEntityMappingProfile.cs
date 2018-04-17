using AspNetCoreJwtAuthBoilerplate.Data.Models.Entities;
using AutoMapper;

namespace AspNetCoreJwtAuthBoilerplate.Data.ViewModels.Mappings
{
    public class ViewModelToEntityMappingProfile : Profile
    {
        public ViewModelToEntityMappingProfile()
        {
            CreateMap<RegistrationViewModel, AppUser>()
                .ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));
        }
    }
}