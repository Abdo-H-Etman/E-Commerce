using System;
using ECommerce.Domain.Enums;

namespace Ecommerce.Application.Dtos.List;

public class UserDto
{
    public Guid Id {get; set;}
    public string? FullName {get; set;} 
    public required string Email {get; set;}
    public string? PhoneNumber {get; set;}
    public ICollection<string>? Roles {get; set;} 
    public DateTime CreatedAt {get; set;}
}
