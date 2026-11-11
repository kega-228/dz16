using AutoMapper;
using WebApiP41.Models.DTO;

namespace WebApiP41.Models.MappingProfiles;

public class RegisterRequestToUserMappingProfile : Profile
{
    public RegisterRequestToUserMappingProfile()
    {
        CreateMap<RegisterRequest, User>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
    }
    
}