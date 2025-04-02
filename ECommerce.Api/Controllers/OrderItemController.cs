using System;
using Ecommerce.Application.Dtos.Create;
using Ecommerce.Application.Dtos.Update;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/OrderItems")]
[Authorize]
public class OrderItemController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public OrderItemController(IServiceManager serviceManager) => _serviceManager = serviceManager;

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateOrderItem([FromBody] OrderItemForCreateDto orderItem)
    {
        var response = await _serviceManager.OrderItemService.CreateOrderItem(orderItem);
        return StatusCode(201, response);
    }

    [HttpGet("order/{orderId}")]
    [Authorize]
    public async Task<IActionResult> GetSpecificOrderItems(Guid orderId, [FromQuery] RequestParameters requestParameters)
    {
        var orderItems = await _serviceManager.OrderItemService.GetSpecificOrderItems(requestParameters, orderId, false);
        return StatusCode(200, orderItems);
    }

    [HttpGet("product/{productId}")]
    [Authorize] 
    public async Task<IActionResult> GetOrderItemsByProduct(Guid productId, [FromQuery] RequestParameters requestParameters)
    {
        var orderItems = await _serviceManager.OrderItemService.GetOrderItemsByProduct(productId, requestParameters);
        return StatusCode(200, orderItems);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderItem(Guid id)
    {
        await _serviceManager.OrderItemService.DeleteOrerItem(id);
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrderItem(Guid id, [FromBody] OrderItemForUpdateDto orderItem)
    {
        var response = await _serviceManager.OrderItemService.UpdateOrderItem(id, orderItem);
        return StatusCode(200, response);
    }
}
