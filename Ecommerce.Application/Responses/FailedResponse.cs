namespace Ecommerce.Application.Responses;

// public record class FailedResponse<T>(string Message) : BaseResponse<object>(false, Message);
public record class FailedResponse<T>(
    bool Success,
    string? Message,
    T? Errors = default
): BaseResponse<T>;