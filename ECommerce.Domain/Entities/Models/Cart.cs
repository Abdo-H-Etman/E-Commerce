using ECommerce.Domain.Generics;

namespace ECommerce.Domain.Models;

public class Cart : IdModel
{
    public int UserId {get; set;}
    public int ProductId {get; set;}
    public int Quantity {get; set;}

    public User? User {get; set;}
    public Product? Product {get; set;}
}
