using AutoMapper;
using WebApiP41.Models.DTO;

namespace WebApiP41.Models.MappingProfiles;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<CreateCategory, Category>();
    }
}