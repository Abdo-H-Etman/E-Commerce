using System;

namespace ECommerce.Domain.Entities.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message)
    {
    }
    
}
