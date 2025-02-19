using AutoMapper;
using Ecommerce.Application.Dtos.Create;
using Ecommerce.Application.Dtos.List;
using Ecommerce.Application.Dtos.Update;
using ECommerce.Domain.Models;

namespace ECommerce.Api;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.FullName, 
                    opt => opt.MapFrom(src => string.Join(" ", src.FirstName, src.LastName)));
        CreateMap<UserForCreateDto, User>()
            .ForMember(dest => dest.Orders, opt => opt.Ignore())
            .ForMember(dest => dest.Reviews, opt => opt.Ignore())
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
        CreateMap<UserForUpdateDto, User>();    
    }
}
