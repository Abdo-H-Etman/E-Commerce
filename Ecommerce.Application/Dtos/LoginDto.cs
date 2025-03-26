using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Application.Dtos.Create;

public record UserForAuthenticationDto
{
    [Required(ErrorMessage = "Email is required")]
    public required string Email{get; init;}

    [Required(ErrorMessage = "Password is required")]
    public required string Password{get; init;}
}
