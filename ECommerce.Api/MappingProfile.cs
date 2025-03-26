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
                    opt => opt.MapFrom(src => string.Join(" ", src.FirstName, src.LastName)))
            .ForMember(dest => dest.Roles, opt => opt.MapFrom<RolesResolver>());
                
        // CreateMap<UserForCreateDto, User>()s
        //     .ForMember(dest => dest.Orders, opt => opt.Ignore())
        //     .ForMember(dest => dest.Reviews, opt => opt.Ignore())
        //     .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
        CreateMap<UserForRegistrationDto, User>();
        CreateMap<UserForUpdateDto, User>(); 
        CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src =>
                src.Category!= null ?src.Category.Name : string.Empty));
        CreateMap<ProductForCreateDto, Product>();   
    }
}
