using Ecommerce.Domain.RequestFeatures;
using ECommerce.Domain.Models;

namespace ECommerce.Domain.Interfaces;

public interface IOrderItemRepository : IRepository<OrderItem, RequestParameters>
{
    Task<IEnumerable<OrderItem>> GetSpecificOrderItems(RequestParameters orderItemLinkParameters, Guid orderId, bool trackChanges);
}
