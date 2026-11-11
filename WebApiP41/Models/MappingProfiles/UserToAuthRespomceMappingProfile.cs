using AutoMapper;
using WebApiP41.Models.DTO;

namespace WebApiP41.Models.MappingProfiles;

public class UserToAuthRespomceMappingProfile : Profile
{
    public UserToAuthRespomceMappingProfile()
    {
        CreateMap<User, AuthResponce>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Token, opt => opt.MapFrom(src => src.Token));
    }
}