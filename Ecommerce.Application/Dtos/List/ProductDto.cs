namespace Ecommerce.Application.Dtos.List;

public record class ProductDto(Guid Id, string Name,string CategoryName, string Description, decimal Price, int Stock);
