using Ecommerce.Application.Dtos.Create;
using Ecommerce.Application.Dtos.List;
using Ecommerce.Application.Responses;
using Ecommerce.Domain.RequestFeatures;

namespace Ecommerce.Application.Interfaces;

public interface IOrderItemService
{
    public Task<BaseResponse<OrderItemDto>> CreateOrderItem(OrderItemForCreateDto orderItemForCreateDto, bool trackChanges);
    public Task<BaseResponse<IEnumerable<OrderItemDto>>> GetSpecificOrderItems(RequestParameters orderItemLinkParameters, Guid orderId, bool trackChanges);
}
