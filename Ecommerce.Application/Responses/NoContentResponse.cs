namespace Ecommerce.Application.Responses;

public record NoContentResponse(string? Message = "No content available.")
    : SuccessResponse<object>(true,Message);