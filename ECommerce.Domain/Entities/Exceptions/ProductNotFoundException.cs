using System;

namespace ECommerce.Domain.Entities.Exceptions;

public class ProductNotFoundException : NotFoundException
{
    protected ProductNotFoundException(int productId) : 
    base($"Product with id: {productId} doesn't exist in the database")
    {
    }
}
