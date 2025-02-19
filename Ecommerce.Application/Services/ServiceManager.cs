using System;
using AutoMapper;
using Ecommerce.Application.Dtos.List;
using Ecommerce.Application.Interfaces;
using ECommerce.Domain.Interfaces;

namespace Ecommerce.Application.Services;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IUserService> _userService;

    public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper, IDataShaper<UserDto> dataShaper, IEntityLinks<UserDto> userLinks)
    {
        _userService = new Lazy<IUserService>(() => new UserServices(repositoryManager, mapper, dataShaper, userLinks));
    }


    public IUserService UserService => _userService.Value;
}
