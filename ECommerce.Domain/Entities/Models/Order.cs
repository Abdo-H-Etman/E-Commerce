using ECommerce.Domain.Enums;
using ECommerce.Domain.Generics;

namespace ECommerce.Domain.Models;

public sealed class Order : IdModel
{
    public Guid UserId {get; set;}
    public Guid? CarrierId {get; set;}
    public DateTime OrderDate {get; set;} = DateTime.UtcNow;
    public string? OrderStatus {get; set;}
    public decimal TotalAmount {get; set;}
    public decimal DiscountPercent {get; set;} 
    public User? User {get; set;}
    public IEnumerable<OrderItem>? OrderItems {get; set;} = new List<OrderItem>();
    public Payment? Payment {get; set;}
    public Shipping? Shipping {get; set;}
    public Carrier? carrier {get; set;}
    
}
