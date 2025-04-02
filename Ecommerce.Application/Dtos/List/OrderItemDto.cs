namespace Ecommerce.Application.Dtos.List;

public record OrderItemDto
{
    public Guid Id { get; set; }
    public string ProductName { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

}
