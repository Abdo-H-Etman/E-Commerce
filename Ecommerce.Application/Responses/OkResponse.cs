using System;

namespace Ecommerce.Application.Responses;

public record OkResponse<T>(T Data, string? Message = null)
    : SuccessResponse<T>(true, Message, Data);
