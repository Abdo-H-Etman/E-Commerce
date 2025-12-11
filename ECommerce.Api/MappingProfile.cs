using AutoMapper;
using Ecommerce.Application.Dtos.Create;
using Ecommerce.Application.Dtos.List;
using Ecommerce.Application.Dtos.Update;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Identity;
using ECommerce.Api.CustomResolvers;

namespace ECommerce.Api;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.FullName, 
                    opt => opt.MapFrom(src => string.Join(" ", new object?[] { src.FirstName, src.LastName })))
            .ForMember(dest => dest.Roles, opt => opt.MapFrom<RolesResolver>());
        CreateMap<UserForRegistrationDto, User>();
        CreateMap<UserForUpdateDto, User>(); 

        CreateMap<OrderItemForCreateDto, OrderItem>();
        CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product!.Name))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Product!.Price));
        CreateMap<OrderItemForUpdateDto, OrderItem>();

        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src =>
             src.OrderItems != null ? src.OrderItems.Sum(x => x.Quantity * x.Product!.Price) : 0))
            .AfterMap((src, dest) =>
            {
                dest.PriceAfterDiscount = dest.TotalPrice - (dest.TotalPrice * (src.DiscountPercent / 100));
            });
        CreateMap<OrderForCreateDto, Order>();
        CreateMap<OrderForUpdateDto, Order>();

        CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src =>
                src.Category!= null ?src.Category.Name : string.Empty));
        CreateMap<ProductForCreateDto, Product>();

        CreateMap<Carrier, CarrierDto>();
    }
}
