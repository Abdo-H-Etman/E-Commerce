using Ecommerce.Domain.RequestFeatures;
using ECommerce.Domain.Models;
using ECommerce.Domain.RequestFeatures;

namespace ECommerce.Domain.Interfaces;

public interface IOrderItemRepository : IRepository<OrderItem, RequestParameters>
{
    Task<PagedList<OrderItem>> GetSpecificOrderItems(RequestParameters requestParameters, Guid orderId);
    Task<PagedList<OrderItem>> GetOrderItemsByProductId(RequestParameters requestParameters, Guid productId);
}
