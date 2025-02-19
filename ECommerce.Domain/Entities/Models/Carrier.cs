using ECommerce.Domain.Generics;

namespace ECommerce.Domain.Models;

public sealed class Carrier : IdModel
{
    public required string Name {get; set;}
    public required string Email {get; set;}
    public required string Phone {get; set;}
}