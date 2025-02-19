using Ecommerce.Domain.RequestFeatures;
using ECommerce.Domain.Models;

namespace ECommerce.Domain.Interfaces;

public interface IRepositoryManager : IDisposable
{
    IUserRepository User {get; }
    IRepository<Category,RequestParameters> Categorie {get; }
    IRepository<Order,RequestParameters> Order {get; }
    IRepository<Product,RequestParameters> Product {get; }
    IRepository<OrderItem,RequestParameters> OrderItem {get; }
    IPaymentRepository Payment {get; }
    IRepository<Carrier,RequestParameters> Carrier {get; }
    IRepository<Review,RequestParameters> Review {get; }
    IRepository<Shipping,RequestParameters> Shipping {get; }
    Task Save();

}