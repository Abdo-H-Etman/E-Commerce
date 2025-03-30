using System;
using Ecommerce.Application.Dtos.Create;
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
        var response = await _serviceManager.OrderItemService.CreateOrderItem(orderItem, true);
        return StatusCode(201, response);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetOrderItems([FromQuery] RequestParameters requestParameters, [FromBody]Guid orderId)
    {
        var orderItems = await _serviceManager.OrderItemService.GetSpecificOrderItems(requestParameters, orderId, false);
        return StatusCode(200, orderItems);
    }
}
