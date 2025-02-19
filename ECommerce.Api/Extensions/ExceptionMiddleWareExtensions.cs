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
                // string responseMessage = contextFeature.Error switch
                //     {
                //         NotFoundException => "Not Found",
                //         BadRequestException => "Bad Request",
                //         _ => "Internal Server Error"
                //     };
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
                    // await context.Response.WriteAsync(new BaseResponse<object>(false, new ErrorDetails
                    // {
                    //     StatusCode = context.Response.StatusCode,
                    //     Message = contextFeature.Error.Message
                    // }.ToString(),null).ToString());
                    // if(contextFeature.Error is UnprocessableEntityException)
                    // {
                    //     await context.Response.WriteAsJsonAsync(new BaseResponse<object>(false, contextFeature.Error.Message));
                    //     return;
                    // }
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
