using Ecommerce.Application.Validations;

namespace Ecommerce.Application.Dtos.List;

public record CarrierDto
{
    public string Name { get; set; } = null!;
    
    [PhoneNumber(ErrorMessage = "Phone is not valid")]
    public string Phone { get; set; } = null!;
}
