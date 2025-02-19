using Ecommerce.Domain.RequestFeatures;
using ECommerce.Domain.Models;

namespace ECommerce.Domain.Interfaces;

public interface IUserRepository : IRepository<User,UserParameters>
{
    Task<User?> GetByEmail(string email);
}
