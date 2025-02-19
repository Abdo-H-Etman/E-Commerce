
using ECommerce.Domain.Entities.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ecommerce.Application.Validations;

public class ValidationFilterAttribute : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var action = context.RouteData.Values["action"];
        var controller = context.RouteData.Values["controller"];
        var param = context.ActionArguments.SingleOrDefault(x => x.Value != null && x.Value.ToString().Contains("Dto")).Value;

        if (param == null)
        {
            throw new BadRequestException("Object is null");
        }

        // if (!context.ModelState.IsValid)
        //     throw new UnprocessableEntityException("unproccessable entity");
        //     // context.Result = new UnprocessableEntityObjectResult(context.ModelState);
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .Where(ms => ms.Value.Errors.Count > 0)
                .ToDictionary(
                    ms => ms.Key,
                    ms => ms.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );

            throw new UnprocessableEntityException("Validation failed for the request.", errors);
        }
    }
}
