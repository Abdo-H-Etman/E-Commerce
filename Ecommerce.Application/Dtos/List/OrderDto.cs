namespace Ecommerce.Application.Dtos.List;

public record OrderDto
{
    public Guid Id { get; set; }
    public UserDto User { get; set; } = null!;
    public CarrierDto Carrier { get; set; } = null!;
    public DateTime OrderDate { get; set; }
    public IEnumerable<OrderItemDto> OrderItems { get; set; } = null!;
    public decimal TotalPrice { get; set; }
    public decimal DiscountPercent { get; set; }
    public decimal PriceAfterDiscount { get; set; }
    public string OrderStatus { get; set; } = null!;
}
