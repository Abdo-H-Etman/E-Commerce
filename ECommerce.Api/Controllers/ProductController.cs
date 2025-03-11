using System;
using Ecommerce.Application.Dtos.Create;
using Ecommerce.Application.Dtos.List;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Responses;
using Ecommerce.Domain.RequestFeatures;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[Route("api/products")]
[ApiController]
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
}
