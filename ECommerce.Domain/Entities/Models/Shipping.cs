using ECommerce.Domain.Enums;
using ECommerce.Domain.Generics;

namespace ECommerce.Domain.Models;

public sealed class Shipping : IdModel
{
    public Guid OrderId {get; set;}
    public required string Address {get; set;}
    public string? ShippingStatus {get; set;}
    public DateTime ShippedAt {get; set;}

    public Order? Order {get; set;}

}
