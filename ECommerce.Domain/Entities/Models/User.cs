using ECommerce.Domain.Enums;
using ECommerce.Domain.Generics;

namespace ECommerce.Domain.Models;

public class User : IdModel
{
    public string? FirstName {get; set;} 
    public string? LastName {get; set;} 
    public required string Email {get; set;}
    public required string Password {get; set;}
    public string? Phone {get; set;}
    public string? Role {get; set;} 
    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;

    public IEnumerable<Order>? Orders {get; set;}
    public IEnumerable<Review>? Reviews {get; set;}

}
