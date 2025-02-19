using System;

namespace Ecommerce.Application.Responses;

public record SuccessResponse<T>(
    bool Success,
    string? Message,
    T? Data = default
): BaseResponse<T>;
