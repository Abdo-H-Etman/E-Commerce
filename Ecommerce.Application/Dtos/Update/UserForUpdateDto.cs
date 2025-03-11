using System.ComponentModel.DataAnnotations;
using Ecommerce.Application.Validations;
using ECommerce.Domain.Models;
using ECommerce.Infrastructure.Data;

namespace Ecommerce.Application.Dtos.Update;

public record UserForUpdateDto
{
    [Required(ErrorMessage = "First Name is required")]
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    // [UniqueEmail(typeof(AppDbContext))]
    public string? Email { get; set; }

    [PhoneNumber]        
    public string? Phone { get; set; }
}
