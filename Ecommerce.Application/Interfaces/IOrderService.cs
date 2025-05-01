using System;
using Ecommerce.Application.Dtos.Create;
using Ecommerce.Application.Dtos.List;
using Ecommerce.Application.Dtos.Update;
using Ecommerce.Application.Responses;
using Ecommerce.Domain.RequestFeatures;

namespace Ecommerce.Application.Interfaces;

public interface IOrderService
{
    public Task<BaseResponse<OrderDto>> CreateOrder(OrderForCreateDto orderForCreateDto);
    public Task<BaseResponse<IEnumerable<OrderDto>>> GetOrders(RequestParameters orderLinkParameters, bool trackChanges);
    public Task<BaseResponse<OrderDto>> GetOrderById(Guid id, bool trackChanges);
    public Task<BaseResponse<object>> DeleteOrder(Guid id);
    public Task<BaseResponse<OrderDto>> UpdateOrder(Guid id, OrderForUpdateDto orderForUpdateDto);
}
