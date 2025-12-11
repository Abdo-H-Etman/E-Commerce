using ECommerce.Domain.Generics;

namespace ECommerce.Domain.Models;

public sealed class OrderItem : IdModel
{
    public Guid OrderId {get; set;}
    public Guid ProductId {get; set;}
    public int Quantity {get; set;}

    public Order? Order {get; set;}
    public Product? Product {get; set;}
    public decimal SumAmount()
    {
        if (Product != null)
        {
            return Product.Price * Quantity;
        }
        return 0;
    }
}
