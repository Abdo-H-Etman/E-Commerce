using System;

namespace ECommerce.Domain.Entities.Exceptions;

public class UnprocessableEntityException : Exception
{
    public IDictionary<string, string[]> Errors { get; }

    public UnprocessableEntityException(string message, IDictionary<string, string[]>? errors = null) 
        : base(message)
    {
        Errors = errors ?? new Dictionary<string, string[]>();
    }
}
