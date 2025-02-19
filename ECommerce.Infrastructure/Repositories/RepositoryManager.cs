using Ecommerce.Domain.RequestFeatures;
using ECommerce.Domain.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Infrastructure.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly AppDbContext _context;
    private IServiceProvider _serviceProvider;

    public RepositoryManager(AppDbContext context, IServiceProvider serviceProvider)
    {
        _context = context;
        _serviceProvider = serviceProvider;
    }

    public IUserRepository User => _serviceProvider.GetRequiredService<IUserRepository>();

    public IRepository<Category,RequestParameters> Categorie => _serviceProvider.GetRequiredService<IRepository<Category,RequestParameters>>();

    public IRepository<Order,RequestParameters> Order => _serviceProvider.GetRequiredService<IRepository<Order,RequestParameters>>();

    public IRepository<Product,RequestParameters> Product => _serviceProvider.GetRequiredService<IRepository<Product,RequestParameters>>();

    public IRepository<OrderItem,RequestParameters> OrderItem => _serviceProvider.GetRequiredService<IRepository<OrderItem,RequestParameters>>();

    public IPaymentRepository Payment => _serviceProvider.GetRequiredService<IPaymentRepository>();

    public IRepository<Carrier,RequestParameters> Carrier => _serviceProvider.GetRequiredService<IRepository<Carrier,RequestParameters>>();

    public IRepository<Review,RequestParameters> Review => _serviceProvider.GetRequiredService<IRepository<Review,RequestParameters>>();

    public IRepository<Shipping,RequestParameters> Shipping => _serviceProvider.GetRequiredService<IRepository<Shipping,RequestParameters>>();

    public void Dispose() => _context.Dispose();

    public async Task Save() => await _context.SaveChangesAsync();
}