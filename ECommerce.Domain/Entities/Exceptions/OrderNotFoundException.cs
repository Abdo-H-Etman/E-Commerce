using System;

namespace ECommerce.Domain.Entities.Exceptions;

public class OrderNotFoundException : NotFoundException
{
    public OrderNotFoundException(Guid orderId) : base($"Order with id: {orderId} does not exist in the database.")
    {
    }
}
