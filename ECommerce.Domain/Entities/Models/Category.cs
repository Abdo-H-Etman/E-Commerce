using ECommerce.Domain.Generics;

namespace ECommerce.Domain.Models;

public sealed class Category : IdModel
{
    public required string Name {get; set;}
    public required string Description  {get; set;}
    public IEnumerable<Product>? Products { get; set; }
}
