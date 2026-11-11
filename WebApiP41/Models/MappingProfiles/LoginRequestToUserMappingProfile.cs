using AutoMapper;
using WebApiP41.Models.DTO;

namespace WebApiP41.Models.MappingProfiles;

public class LoginRequestToUserMappingProfile : Profile
{
    public LoginRequestToUserMappingProfile()
    {
        CreateMap<LoginRequest, User>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
    }
    
}