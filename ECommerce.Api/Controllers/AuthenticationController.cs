
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

[Route("api/authentication")]
[ApiController]
public class AuthenticationController : ControllerBase 
{
    private readonly IServiceManager _serviceManager;

    public AuthenticationController(IServiceManager serviceManager) => _serviceManager = serviceManager;

    [HttpGet("users")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> GetAllUsers()
    {
        var response = await _serviceManager.AuthenticationService.GetAllUsers();

        return StatusCode(200, response);
    }

    [HttpPost("register")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
    {
        var response = await _serviceManager.AuthenticationService.RegisterUserAsync(userForRegistration);

        return StatusCode(201, response);
    }

    [HttpPost("login")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthentication)
    {
        var response = await _serviceManager.AuthenticationService.ValidateUser(userForAuthentication);

        if (!response)
        {
            return Unauthorized();
        }

        var tokenDto = await _serviceManager.AuthenticationService.CreateToken(populateExp: true);
        return Ok(new OkResponse<TokenDto>(tokenDto, "Token Created Successfully"));
    }
}
