using System;

namespace ECommerce.Domain.Entities.Exceptions;

public class OrderItemNotFoundException : NotFoundException
{
    public OrderItemNotFoundException(Guid orderItemId) : base($"Order item with id: {orderItemId} does not exist in the database.")
    {
    }
}

