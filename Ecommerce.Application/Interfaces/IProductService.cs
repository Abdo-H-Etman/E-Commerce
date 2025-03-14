
using Ecommerce.Application.Dtos.Create;
using Ecommerce.Application.Dtos.List;
using Ecommerce.Application.Responses;
using Ecommerce.Domain.RequestFeatures;
using ECommerce.Domain.Models;

namespace Ecommerce.Application.Interfaces;

public interface IProductService
{
    Task<BaseResponse<ProductDto>> CreateProduct(ProductForCreateDto productForCreateDto, bool trackChanges);
    Task<BaseResponse<IEnumerable<ProductDto>>> GetAllProducts(RequestParameters productLinkParameters, bool trackChanges);
}
