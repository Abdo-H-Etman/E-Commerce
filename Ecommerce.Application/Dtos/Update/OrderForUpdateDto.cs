namespace Ecommerce.Application.Dtos.Update;

public record OrderForUpdateDto
{
    public Guid CarrierId { get; set; }
    public decimal DiscountPercent { get; set; }
    public string OrderStatus { get; set; } = null!;
    public IEnumerable<OrderItemForUpdateDto> OrderItems { get; set; } = null!;
}
