using System;

namespace ECommerce.Domain.Entities.Exceptions;

public class RefreshTokenBadRequestException : BadRequestException
{
    public RefreshTokenBadRequestException() : base("Invalid Client Request for Refresh Token")
    {
    }
}
