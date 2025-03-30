using System;
using AutoMapper;
using Ecommerce.Application.Dtos.Create;
using Ecommerce.Application.Dtos.List;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Responses;
using Ecommerce.Domain.RequestFeatures;
using ECommerce.Domain.Interfaces;
using ECommerce.Domain.Models;

namespace Ecommerce.Application.Services;

public class OrderItemService : IOrderItemService
{
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repositoryManager;

    public OrderItemService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _mapper = mapper;
        _repositoryManager = repositoryManager;
    }

    public async Task<BaseResponse<OrderItemDto>> CreateOrderItem(OrderItemForCreateDto orderItemForCreateDto, bool trackChanges)
    {
        OrderItem orderItem = _mapper.Map<OrderItem>(orderItemForCreateDto);
        await _repositoryManager.OrderItem.Add(orderItem);
        await _repositoryManager.Save();
        OrderItemDto orderItemDto = _mapper.Map<OrderItemDto>(orderItem);
        return new CreatedResponse<OrderItemDto>(orderItemDto, "Order Item Created Successfully");
    }

    public async Task<BaseResponse<IEnumerable<OrderItemDto>>> GetSpecificOrderItems(RequestParameters orderItemLinkParameters, Guid orderId, bool trackChanges)
    {
        var orderItems = await _repositoryManager.OrderItem.GetSpecificOrderItems(orderItemLinkParameters, orderId, trackChanges);
        var orderItemDtos = _mapper.Map<IEnumerable<OrderItemDto>>(orderItems);
        return new OkResponse<IEnumerable<OrderItemDto>>(orderItemDtos, "Order Items Retrieved Successfully");
    }
}
