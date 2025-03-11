using System;
using AutoMapper;
using Ecommerce.Application.Dtos.List;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Api.CustomResolvers;

public class RolesResolver : IValueResolver<User, UserDto, ICollection<string>>
{
    private readonly UserManager<User> _userManager;

    public RolesResolver(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public ICollection<string> Resolve(User source, UserDto destination, ICollection<string> destMember, ResolutionContext context)
    {
        var roles = Task.Run(async () => await _userManager.GetRolesAsync(source)).Result;
        return roles;
    }
}
