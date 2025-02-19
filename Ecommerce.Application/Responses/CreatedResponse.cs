namespace Ecommerce.Application.Responses;

public record CreatedResponse<T>(T Data, string? Message = "Resource created successfully.")
    : SuccessResponse<T>(true,Message,Data);
