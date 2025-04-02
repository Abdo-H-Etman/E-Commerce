using System;

namespace Ecommerce.Application.Dtos.Update;

public record OrderItemForUpdateDto
{
    public Guid ProductId { get; init; }
    public int Quantity { get; init; }
}
