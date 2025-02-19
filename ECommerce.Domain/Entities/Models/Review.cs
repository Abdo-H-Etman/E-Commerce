using ECommerce.Domain.Generics;

namespace ECommerce.Domain.Models;

public sealed class Review : IdModel
{
    public Guid ProductId {get; set;}
    public Guid UserId {get; set;}
    public Guid Rating {get; set;}
    public string? Comment {get; set;}
    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;

    public User? User {get; set;}
    public Product? Product {get; set;}
}
