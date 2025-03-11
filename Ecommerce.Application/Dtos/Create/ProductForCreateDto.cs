using System;

namespace Ecommerce.Application.Dtos.Create;

public record  ProductForCreateDto(Guid UserId, Guid CategoryId,List<string> Base64Imgs, string Name, string Description, decimal Price, int Stock);
