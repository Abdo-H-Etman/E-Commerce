
namespace Ecommerce.Application.Dtos;

public record TokenDto
{
    public string? AccessToken{get; init;}
    public string? RefreshToken{get; init;}
};