using System;
using AutoMapper;
using Ecommerce.Application.Dtos.Create;
using Ecommerce.Application.Dtos.List;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Responses;
using Ecommerce.Domain.RequestFeatures;
using ECommerce.Domain.Interfaces;
using ECommerce.Domain.Models;

namespace Ecommerce.Application.Services;

public class ProductService : IProductService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    // private readonly IDataShaper<ProductDto> _dataShaper;
    public ProductService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<BaseResponse<ProductDto>> CreateProduct(ProductForCreateDto productForCreateDto, bool trackChanges)
    {
        Product product = _mapper.Map<Product>(productForCreateDto);
        await _repositoryManager.Product.Add(product);
        await _repositoryManager.Save();
        ProductDto productDto = _mapper.Map<ProductDto>(product);
        return new OkResponse<ProductDto>(productDto, "Product Created Successfully");
    }
    public async Task<BaseResponse<IEnumerable<ProductDto>>> GetAllProducts(RequestParameters productLinkParameters, bool trackChanges)
    {
        var products = await _repositoryManager.Product.GetAll(productLinkParameters);
        var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
        return new OkResponse<IEnumerable<ProductDto>>(productDtos, "Products Retrieved Successfully");
    }
}
