using Ecommerce.Domain.RequestFeatures;
using ECommerce.Domain.Generics;
using ECommerce.Domain.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Infrastructure.Utilities;

public static class DependencyInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<IdModel,RequestParameters>),typeof(GenericRepository<IdModel,RequestParameters>));
        services.AddScoped<IRepositoryManager,RepositoryManager>();
        services.AddScoped<IUserRepository,UserRepository>();
        services.AddScoped<IRepository<Order,RequestParameters>,OrderRepository>();
        services.AddScoped<IOrderItemRepository,OrderItemRepository>();
        services.AddScoped<IRepository<Product,RequestParameters>,ProductRepository>();
        services.AddScoped<IPaymentRepository,PaymentRepository>();
        return services;
    }
}