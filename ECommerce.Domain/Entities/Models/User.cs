using ECommerce.Domain.Enums;
using ECommerce.Domain.Generics;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Domain.Models;

public class User : IdentityUser<Guid>
{
    public string? FirstName {get; set;} 
    public string? LastName {get; set;}  
    public string? RefreshToken {get; set;}
    public DateTime RefreshTokenExpiryTime {get; set;}
    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
    // public IEnumerable<string>? UserRoles {get; set;}
    public IEnumerable<Order>? Orders {get; set;}
    public IEnumerable<Review>? Reviews {get; set;}

}
