using System;
using Asp.Versioning;
using Ecommerce.Application.Dtos;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Validations;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/token")]
[ApiController]
// [ApiExplorerSettings(GroupName = "v1.0")]
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
