using System;

namespace ECommerce.Domain.Entities.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }
}
