using System;
using AutoMapper;
using Ecommerce.Application.Dtos.List;
using Ecommerce.Application.Interfaces;
using ECommerce.Domain.Entities.ConfigurationModels;
using ECommerce.Domain.Interfaces;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Ecommerce.Application.Services;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IUserService> _userService;
    private readonly Lazy<IAuthenticationService> _authenticationService;
    private readonly Lazy<IProductService> _productService;
    private readonly Lazy<IOrderItemService> _orderItemService;
    public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper, IDataShaper<UserDto> dataShaper
        , IEntityLinks<UserDto> userLinks, UserManager<User> userManager,
        ILoggerManager logger, IOptions<JwtConfiguration> configuration, SignInManager<User> signInManager)
    {
        _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager, logger, mapper, configuration, signInManager));
        _productService = new Lazy<IProductService>(() => new ProductService(repositoryManager, mapper));
        _userService = new Lazy<IUserService>(() => new UserServices(repositoryManager, mapper, dataShaper, userLinks));
        _orderItemService = new Lazy<IOrderItemService>(() => new OrderItemService(repositoryManager, mapper));
    }
    
    public IAuthenticationService AuthenticationService => _authenticationService.Value;
    public IProductService ProductService => _productService.Value;
    public IUserService UserService => _userService.Value;
    public IOrderItemService OrderItemService => _orderItemService.Value;
}
