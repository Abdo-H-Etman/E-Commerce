

using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Ecommerce.Application.Validations;

public class PhoneNumberAttribute : ValidationAttribute
{
    private const string Pattern = @"^(?:\+20|0)(10|11|12|15)\d{8}$";
    private const string DefaultErrorMessage = "The phone number is not a valid Egyptian phone number.";

    public PhoneNumberAttribute()
    {
        ErrorMessage = DefaultErrorMessage;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
        {
            return ValidationResult.Success; // Assume null is valid. Use [Required] for non-null validation.
        }

        if (value is string phoneNumber && Regex.IsMatch(phoneNumber, Pattern))
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(ErrorMessage);
    }
}
