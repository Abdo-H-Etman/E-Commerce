using System;

namespace Ecommerce.Application.Interfaces;

public interface IServiceManager
{
    IUserService UserService { get; }
}
