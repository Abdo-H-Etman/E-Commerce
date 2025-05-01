using System;
using Ecommerce.Application.Dtos.Create;
using Ecommerce.Application.Dtos.Update;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[ApiController]
[Route("api/V{version:apiVersion}/Orders")]
[Authorize]
public class OrderController : ControllerBase
{
    private IServiceManager _serviceManager;
    public OrderController(IServiceManager serviceManager) => _serviceManager = serviceManager;

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderForCreateDto order)
    {
        var response = await _serviceManager.OrderService.CreateOrder(order);
        return StatusCode(201, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders([FromQuery] RequestParameters requestParameters)
    {
        var orders = await _serviceManager.OrderService.GetOrders(requestParameters, trackChanges: false);
        return StatusCode(200, orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(Guid id)
    {
        var order = await _serviceManager.OrderService.GetOrderById(id, trackChanges: false);
        return StatusCode(200, order);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] OrderForUpdateDto order)
    {
        var response = await _serviceManager.OrderService.UpdateOrder(id, order);
        return StatusCode(200, response);
    }
}
