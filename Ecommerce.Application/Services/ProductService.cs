using System;
using AutoMapper;
using Ecommerce.Application.Dtos.Create;
using Ecommerce.Application.Dtos.List;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Responses;
using Ecommerce.Domain.RequestFeatures;
using ECommerce.Domain.Entities.Exceptions;
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
    public async Task<BaseResponse<ProductDto>> GetProduct(Guid id, bool trackChanges)
    {
        var product = await _repositoryManager.Product.GetById(id) ??
                        throw new ProductNotFoundException(id);
        
        var productDto = _mapper.Map<ProductDto>(product);
        return new OkResponse<ProductDto>(productDto, "Product Retrieved Successfully");
    }
    public async Task<BaseResponse<bool>> DeleteProduct(Guid id, bool trackChanges)
    {
        var product = await _repositoryManager.Product.GetById(id) ??
                        throw new ProductNotFoundException(id);
        
        _repositoryManager.Product.Delete(product);
        await _repositoryManager.Save();
        return new OkResponse<bool>(true, "Product Deleted Successfully");
    }
}
