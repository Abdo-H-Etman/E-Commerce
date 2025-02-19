using ECommerce.Domain.Generics;

namespace ECommerce.Domain.Models;

public sealed class OrderItem : IdModel
{
    public Guid OrderId {get; set;}
    public Guid ProductId {get; set;}
    public Guid Quantity {get; set;}
    public decimal UnitPrice {get; set;}

    public Order? Order {get; set;}
    public Product? Product {get; set;}
}
