using System;

namespace ECommerce.Domain.Entities.Exceptions;

public class EndDateBadRequestException : BadRequestException
{
    public EndDateBadRequestException(string message) : base(message)
    {
    }
}
