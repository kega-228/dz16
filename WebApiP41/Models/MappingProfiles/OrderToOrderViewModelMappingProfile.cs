using AutoMapper;
using WebApiP41.Models.DTO;

namespace WebApiP41.Models.MappingProfiles;

public class OrderToOrderViewModelMappingProfile : Profile
{
    public OrderToOrderViewModelMappingProfile()
    {
        CreateMap<Order, OrderViewModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.OrderNumberFormatted, opt => opt.MapFrom(src => src.OrderNumber))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.Cost, opt => opt.MapFrom(src => src.Cost))
            .ForMember(dest => dest.CompletedStatus, opt => opt.MapFrom(src => src.IsComplete == true ? "виконано" : "He виконано"))
            .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment));
    }
}