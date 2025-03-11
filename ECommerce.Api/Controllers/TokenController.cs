using System;
using Ecommerce.Application.Dtos;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Validations;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[Route("api/token")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public TokenController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpPost("refresh")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> RefreshToken([FromBody] TokenDto tokenDto)
    {
        var response = await _serviceManager.AuthenticationService.RefreshToken(tokenDto);
        return StatusCode(200, response);
    }
}
