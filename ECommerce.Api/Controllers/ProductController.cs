using System;
using Asp.Versioning;
using Ecommerce.Application.Dtos.Create;
using Ecommerce.Application.Dtos.List;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Responses;
using Ecommerce.Domain.RequestFeatures;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/products")]
[ApiController]
[Authorize]
// [ApiExplorerSettings(GroupName = "v1.0")]
public class ProductController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    public ProductController(IServiceManager serviceManager) => _serviceManager = serviceManager;


    [HttpPost]
    [Authorize(Roles = "Seller")]
    public async Task<IActionResult> CreateProduct([FromBody] ProductForCreateDto product)
    {
        var response = await _serviceManager.ProductService.CreateProduct(product,true);
        return StatusCode(201, response);
    }
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetProducts([FromQuery] RequestParameters requestParameters)
    {
        var products = await _serviceManager.ProductService.GetAllProducts(requestParameters, false);
        return StatusCode(200, products);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetProduct(Guid id)
    {
        var product = await _serviceManager.ProductService.GetProduct(id, false);
        return StatusCode(200, product);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Seller, Administrator")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var response = await _serviceManager.ProductService.DeleteProduct(id, trackChanges: true);
        return StatusCode(200, "Product deleted successfully");
    }
}
