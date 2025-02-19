using System.ComponentModel.DataAnnotations;
using ECommerce.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Application.Validations;

public class UniqueEmailAttribute : ValidationAttribute
{
    private readonly Type _dbContextType;

    public UniqueEmailAttribute(Type contextType)
    {
        _dbContextType = contextType;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var email = value!.ToString();
        var dbContext = (DbContext)validationContext.GetService(_dbContextType)!;
        
        var emailExists = dbContext.Set<User>().Any(u => u.Email == email);
        return emailExists
            ? new ValidationResult("The email address is already in use.")
            : ValidationResult.Success;

    }
}
