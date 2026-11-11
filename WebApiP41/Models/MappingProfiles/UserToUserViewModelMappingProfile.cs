using AutoMapper;
using WebApiP41.Models.DTO;

namespace WebApiP41.Models.MappingProfiles;

public class UserToUserViewModelMappingProfile : Profile
{
    public UserToUserViewModelMappingProfile()
    {
        CreateMap<User, UserViewModel>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
    }
}