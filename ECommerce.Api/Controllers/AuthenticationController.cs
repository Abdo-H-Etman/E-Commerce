
using Asp.Versioning;
using Ecommerce.Application.Dtos;
using Ecommerce.Application.Dtos.Create;
using Ecommerce.Application.Dtos.List;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Responses;
using Ecommerce.Application.Validations;
using ECommerce.Domain.Entities.Exceptions;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ECommerce.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/Authentication")]
[ApiController]
// [ApiExplorerSettings(GroupName = "v1.0")]
public class AuthenticationController : ControllerBase 
{
    private readonly IServiceManager _serviceManager;

    public AuthenticationController(IServiceManager serviceManager) => _serviceManager = serviceManager;


    [HttpPost("Register")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
    {
        var response = await _serviceManager.AuthenticationService.RegisterUserAsync(userForRegistration);

        return StatusCode(201, response);
    }

    [HttpPost("Login")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthentication)
    {
        var response = await _serviceManager.AuthenticationService.Login(userForAuthentication);
        return StatusCode(200, response);

    }

    [HttpPost("Logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var response = await _serviceManager.AuthenticationService.Logout(token);
        return StatusCode(200, response);
    }

    [HttpGet("AccessDenied")]
    public IActionResult AccessDenied() =>
        Forbid();
}
