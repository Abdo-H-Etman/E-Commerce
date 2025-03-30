using System;

namespace Ecommerce.Application.Dtos.Create;

public record OrderItemForCreateDto
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
