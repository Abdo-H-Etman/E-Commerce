using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Application.Dtos.Create;

public record LoginDto
{
    [Required(ErrorMessage = "Email is required")]
    public required string Email{get; set;}

    [Required(ErrorMessage = "Password is required")]
    public required string Password{get; set;}
}
