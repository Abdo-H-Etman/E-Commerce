using ECommerce.Domain.Generics;

namespace ECommerce.Domain.Models;

public sealed class Product : IdModel
{
    public required List<string> Base64Imgs {get; set;}
    public required string Name {get; set;}
    public string? Description {get; set;}
    public decimal Price {get; set;}
    public int Stock {get; set;}
    public Guid CategoryId {get; set;}
    public Guid UserId {get; set;}
    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
    public User? ProvidedBy {get; set;}
    public Category? Category {get; set;}
    public ICollection<OrderItem>? OrderItems {get; set;}
    public ICollection<Review>? Reviews {get; set;}
}
