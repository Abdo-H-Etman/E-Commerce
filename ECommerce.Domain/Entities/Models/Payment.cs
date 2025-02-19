using ECommerce.Domain.Enums;
using ECommerce.Domain.Generics;

namespace ECommerce.Domain.Models;

public sealed class Payment : IdModel
{
    public Guid OrderId {get; set;}
    public required Guid UserId {get; set;}
    public DateTime PaymentDate {get; set;} = DateTime.UtcNow;
    public decimal Amount {get; set;}
    public string? PaymentMetod {get; set;}

    public Order? Order {get; set;}

}
