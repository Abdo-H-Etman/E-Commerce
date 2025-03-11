namespace Ecommerce.Application.Dtos.List;

public record class ProductDto(Guid Id, string Name, string Description, decimal Price, int Stock);
