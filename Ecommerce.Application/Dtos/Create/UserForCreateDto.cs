using System.ComponentModel.DataAnnotations;
using Ecommerce.Application.Validations;
using ECommerce.Infrastructure.Data;

namespace Ecommerce.Application.Dtos.Create;


public record UserForCreateDto
{
    [Required(ErrorMessage = "First Name is required")]
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    [UniqueEmail(typeof(AppDbContext))]
    public string? Email { get; set; }
    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
    [Required(ErrorMessage = "Confirm Password is required")]
    [Compare("Password", ErrorMessage = "Password and Confirm Password do not match")]
    public string? ConfirmPassword { get; set; }
    [PhoneNumber]
    public string? Phone { get; set; }
}
