using System;
using AutoMapper;
using Ecommerce.Application.Dtos.Create;
using Ecommerce.Application.Dtos.List;
using Ecommerce.Application.Dtos.Update;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Responses;
using Ecommerce.Domain.RequestFeatures;
using ECommerce.Domain.Entities.Exceptions;
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

    public async Task<BaseResponse<OrderItemDto>> CreateOrderItem(OrderItemForCreateDto orderItemForCreateDto)
    {
        OrderItem orderItem = _mapper.Map<OrderItem>(orderItemForCreateDto);
        await _repositoryManager.OrderItem.Add(orderItem);
        await _repositoryManager.Save();
        OrderItemDto orderItemDto = _mapper.Map<OrderItemDto>(orderItem);
        return new CreatedResponse<OrderItemDto>(orderItemDto, "Order Item Created Successfully");
    }

    public async Task<BaseResponse<IEnumerable<OrderItemDto>>> GetSpecificOrderItems(RequestParameters orderItemParameters, Guid orderId, bool trackChanges)
    {
        var orderItems = await _repositoryManager.OrderItem.GetSpecificOrderItems(orderItemParameters, orderId);
        var orderItemDtos = _mapper.Map<IEnumerable<OrderItemDto>>(orderItems);
        return new OkResponse<IEnumerable<OrderItemDto>>(orderItemDtos, "Order Items Retrieved Successfully");
    }

    public async Task<BaseResponse<object>> DeleteOrerItem(Guid id)
    {
        var orderItem = await _repositoryManager.OrderItem.GetById(id) ??
                        throw new OrderItemNotFoundException(id);

        _repositoryManager.OrderItem.Delete(orderItem);
        await _repositoryManager.Save();
        return new OkResponse<object>(new object(), "Order Item Deleted Successfully");
    }

    public async Task<BaseResponse<OrderItemDto>> UpdateOrderItem(Guid id, OrderItemForUpdateDto orderItemForUpdateDto)
    {
        var orderItem = await _repositoryManager.OrderItem.GetById(id) ??
                        throw new OrderItemNotFoundException(id);

        _mapper.Map(orderItemForUpdateDto, orderItem);
        await _repositoryManager.Save();
        var orderItemDto = _mapper.Map<OrderItemDto>(orderItem);
        return new OkResponse<OrderItemDto>(orderItemDto, "Order Item Updated Successfully");
    }

    public async Task<BaseResponse<IEnumerable<OrderItemDto>>> GetOrderItemsByProduct(Guid productId, RequestParameters orderItemParameters)
    {
        var orderItems = await _repositoryManager.OrderItem.GetOrderItemsByProductId(orderItemParameters, productId);
        var orderItemDtos = _mapper.Map<IEnumerable<OrderItemDto>>(orderItems);
        return new OkResponse<IEnumerable<OrderItemDto>>(orderItemDtos, "Order Items Retrieved Successfully");
    }
}