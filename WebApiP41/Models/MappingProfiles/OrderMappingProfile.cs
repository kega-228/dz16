using WebApiP41.Models.DTO;

namespace WebApiP41.Models.MappingProfiles;
using AutoMapper;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<Order, OrderViewModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.OrderNumberFormatted, opt => opt.MapFrom(src => "ORD-" + src.OrderNumber.ToString("D4")))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.Cost, opt => opt.MapFrom(src => src.Cost))
            .ForMember(dest => dest.CompletedStatus, opt => opt.MapFrom(src => src.IsComplete == true ? "виконано" : "He виконано"))
            .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Cost * src.Quantity));

        CreateMap<AddRequest, Order>()
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.Cost, opt => opt.MapFrom(src => src.Cost))
            .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
            .ForMember(dest => dest.Date, opt => opt.Ignore())
            .ForMember(dest => dest.IsComplete, opt => opt.Ignore())
            .ForMember(dest => dest.OrderNumber, opt => opt.Ignore());
    }
}