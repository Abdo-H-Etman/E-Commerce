using System;
using System.Net.Http.Headers;
using ECommerce.Domain.Entities.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ecommerce.Application.Validations;

public class ValidateMediaTypeAttribute : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var acceptHeaderPresent = context.HttpContext
        .Request.Headers.ContainsKey("Accept");
        if (!acceptHeaderPresent)
        {
            throw new BadRequestException($"Accept header is missing.");
        }
        var mediaType = context.HttpContext.Request.Headers["Accept"].FirstOrDefault();
        if (!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue? outMediaType))
        {
            throw new BadRequestException($"Media type not present.Please add Accept header with the required media type.");
        }
        context.HttpContext.Items.Add("AcceptHeaderMediaType", outMediaType);
    }
    public void OnActionExecuted(ActionExecutedContext context) { }

}
