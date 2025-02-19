using Ecommerce.Domain.RequestFeatures;
using ECommerce.Domain.Models;

namespace ECommerce.Domain.Interfaces;

public interface IPaymentRepository : IRepository<Payment, RequestParameters>
{
    Task<Payment?> GetByIdAndUserId(Guid id,Guid userId);
}