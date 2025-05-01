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

public class OrderService : IOrderService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    public OrderService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }
    public async Task<BaseResponse<OrderDto>> CreateOrder(OrderForCreateDto orderForCreateDto)
    {
        var order = _mapper.Map<Order>(orderForCreateDto);
        await _repositoryManager.Order.Add(order);
        await _repositoryManager.Save();
        var orderDto = _mapper.Map<OrderDto>(order);
        return new OkResponse<OrderDto>(orderDto, "Order Created Successfully");
    }

    public async Task<BaseResponse<IEnumerable<OrderDto>>> GetOrders(RequestParameters orderLinkParameters, bool trackChanges)
    {
        var orders = await _repositoryManager.Order.GetAll(orderLinkParameters);
        var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders);
        return new OkResponse<IEnumerable<OrderDto>>(orderDtos, "Orders Retrieved Successfully");
    }

    public async Task<BaseResponse<OrderDto>> GetOrderById(Guid id, bool trackChanges)
    {
        var order = await _repositoryManager.Order.GetById(id) ??
                    throw new OrderNotFoundException(id);
        
        var orderDto = _mapper.Map<OrderDto>(order);
        return new OkResponse<OrderDto>(orderDto, "Order Retrieved Successfully");
    }

    public async Task<BaseResponse<object>> DeleteOrder(Guid id)
    {
        var order = await _repositoryManager.Order.GetById(id) ??
                    throw new OrderNotFoundException(id);
        
        _repositoryManager.Order.Delete(order);
        await _repositoryManager.Save();
        return new OkResponse<object>(new object(), "Order Deleted Successfully");
    }

    public async Task<BaseResponse<OrderDto>> UpdateOrder(Guid id, OrderForUpdateDto orderForUpdateDto)
    {
        var order = await _repositoryManager.Order.GetById(id) ??
                    throw new OrderNotFoundException(id);
        
        _mapper.Map(orderForUpdateDto, order);
        await _repositoryManager.Save();
        var orderDto = _mapper.Map<OrderDto>(order);
        return new OkResponse<OrderDto>(orderDto, "Order Updated Successfully");
    }    
}
