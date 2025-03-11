using System.Text.Json;
using Asp.Versioning;
using Ecommerce.Application.Dtos.Create;
using Ecommerce.Application.Dtos.Update;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Responses;
using Ecommerce.Application.Validations;
using Ecommerce.Domain.RequestFeatures;
using ECommerce.Domain.Entities.Exceptions;
using ECommerce.Domain.Entities.LinkModels;
using ECommerce.Domain.Entities.Models;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/users")]
    [ApiController]
    // [ResponseCache(CacheProfileName = "120SecondsDuration")]
    public class UsersController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public UsersController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet(Name = "GetUsers")]
        [HttpHead]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAllUsers([FromQuery] UserParameters userParameters)
        {
            var linkParams = new LinkParameters(userParameters, HttpContext);
            var pagedResult = await _serviceManager.UserService.GetAllUsers(linkParams,trackChanges: false);
            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
            if(pagedResult.linkResponse.HasLinks)
                return Ok(new OkResponse<IEnumerable<Entity>>(pagedResult.linkResponse.LinkedEntities.Value, "Users with links"));
            return Ok(new OkResponse<IEnumerable<Entity>>(pagedResult.linkResponse.ShapedEntities, "Users without links"));
        }

        [HttpGet("{id}")]
        // [HttpCacheExpiration(MaxAge = 60)]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _serviceManager.UserService.GetUser(id, trackChanges: false);
            return StatusCode(200, user);
        }

        [HttpGet("Filter")]
        public async Task<IActionResult> FilterUsers([FromQuery] string filter)
        {
            var users = await _serviceManager.UserService.FilterUsers(u => string.Join(" ", u.FirstName!.ToLower(), (u.LastName ?? string.Empty).ToLower()).Contains(filter.ToLower()), trackChanges: false);
            return StatusCode(200, users);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserForUpdateDto user)
        {
            var response = await _serviceManager.UserService.UpdateUser(id, user, trackChanges: false);
            return StatusCode(200, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var response = await _serviceManager.UserService.DeleteUser(id, trackChanges: true);
            return StatusCode(200, response);
        }

        [HttpOptions]
        public IActionResult GetUsersOptions()
        {
            Response.Headers.Append("Allow", "GET,OPTIONS,POST,PUT,DELETE");
            return Ok();
        }
    }
}
