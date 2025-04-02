using Ecommerce.Application.Dtos.Create;
using Ecommerce.Application.Dtos.List;
using Ecommerce.Application.Dtos.Update;
using Ecommerce.Application.Responses;
using Ecommerce.Domain.RequestFeatures;

namespace Ecommerce.Application.Interfaces;

public interface IOrderItemService
{
    public Task<BaseResponse<OrderItemDto>> CreateOrderItem(OrderItemForCreateDto orderItemForCreateDto);
    public Task<BaseResponse<IEnumerable<OrderItemDto>>> GetSpecificOrderItems(RequestParameters orderItemLinkParameters, Guid orderId, bool trackChanges);
    public Task<BaseResponse<object>> DeleteOrerItem(Guid id);
    public Task<BaseResponse<OrderItemDto>> UpdateOrderItem(Guid id, OrderItemForUpdateDto orderItemForUpdateDto);
    public Task<BaseResponse<IEnumerable<OrderItemDto>>> GetOrderItemsByProduct(Guid productId, RequestParameters orderItemLinkParameters);
}
