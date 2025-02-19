using System;
using System.Text.Json;
using Asp.Versioning;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Responses;
using Ecommerce.Domain.RequestFeatures;
using ECommerce.Domain.Entities.LinkModels;
using ECommerce.Domain.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/users")]
[ApiController]
public class UserControllerV2 : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public UserControllerV2(IServiceManager serviceManager) => _serviceManager = serviceManager;

    [HttpGet(Name = "GetUsers")]
    public async Task<IActionResult> GetAllUsers([FromQuery] UserParameters userParameters)
    {
        var linkParams = new LinkParameters(userParameters, HttpContext);
        var pagedResult = await _serviceManager.UserService.GetAllUsers(linkParams,trackChanges: false);
        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
        if(pagedResult.linkResponse.HasLinks)
            return Ok(new OkResponse<IEnumerable<Entity>>(pagedResult.linkResponse.LinkedEntities.Value, "From V2.0 Users with links"));
        return Ok(new OkResponse<IEnumerable<Entity>>(pagedResult.linkResponse.ShapedEntities, "From V2.0 Users without links"));
    }
}
