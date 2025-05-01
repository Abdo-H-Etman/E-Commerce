namespace Ecommerce.Application.Dtos.Create;

public record OrderForCreateDto
{
    public Guid UserId {get; set;}
    public decimal DiscountPercent {get; set;} 
}
