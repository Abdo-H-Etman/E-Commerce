using System.Net;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Responses;
using ECommerce.Domain.Entities.ErrorModel;
using ECommerce.Domain.Entities.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;

namespace ECommerce.Api.Extensions;

public static class ExceptionMiddleWareExtensions
{
    public static void ConfigureExceptionHandler(this WebApplication app, ILoggerManager logger)
    {
        _ = app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var contextFeature = context.Features.GetRequiredFeature<IExceptionHandlerFeature>();
                
                if (contextFeature != null)
                {
                    context.Response.StatusCode = contextFeature.Error switch
                    {
                        NotFoundException => StatusCodes.Status404NotFound,
                        BadRequestException => StatusCodes.Status400BadRequest,
                        UnprocessableEntityException => StatusCodes.Status422UnprocessableEntity,
                        _ => StatusCodes.Status500InternalServerError
                    };
                    logger.LogError($"Something went wrong {contextFeature.Error}");
                    
                    if (contextFeature.Error is UnprocessableEntityException validationException)
                    {
                        await context.Response.WriteAsJsonAsync(new FailedResponse<object>(
                            Success: false,
                            Message: validationException.Message,
                            Errors:new { validationException.Errors}.Errors 
                        ));
                        return;
                    }

                    await context.Response.WriteAsJsonAsync(new FailedResponse<object>(false,contextFeature.Error.Message));
                }
            });
        });
    }

}
