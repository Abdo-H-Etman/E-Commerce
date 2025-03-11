using System.ComponentModel.DataAnnotations;
using Ecommerce.Application.Validations;
using ECommerce.Infrastructure.Data;

namespace Ecommerce.Application.Dtos.Create;


public record UserForRegistrationDto
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }

    [Required(ErrorMessage = "Username is required")]
    
    [UniqueUserName(typeof(AppDbContext), ErrorMessage = "Username is already in use")]
    public string? UserName { get; init; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; init; }
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string? ConfirmPassword { get; init; }
    
    [UniqueEmail(typeof(AppDbContext), ErrorMessage = "Email is already in use")]
    public string? Email { get; init; }

    [PhoneNumber(ErrorMessage = "Phone number is not valid")]
    public string? PhoneNumber { get; init; }
    public ICollection<string>? Roles { get; init; }

}
