using System;
using System.ComponentModel.DataAnnotations;
using ECommerce.Domain.Models;
using ECommerce.Infrastructure.Data;

namespace Ecommerce.Application.Validations;

public class UniqueUserNameAttribute : ValidationAttribute
{
    private readonly Type _dbContextType;

    public UniqueUserNameAttribute(Type contextType)
    {
        _dbContextType = contextType;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var userName = value!.ToString();
        var dbContext = (AppDbContext)validationContext.GetService(_dbContextType)!;
        
        var userNameExists = dbContext.Set<User>().Any(u => u.UserName == userName);
        return userNameExists
            ? new ValidationResult("The username is already in use.")
            : ValidationResult.Success;

    }
}
