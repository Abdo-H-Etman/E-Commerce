using System;
using Ecommerce.Domain.RequestFeatures;
using ECommerce.Domain.Models;
using ECommerce.Domain.RequestFeatures;

namespace ECommerce.Domain.Interfaces;

public interface IProductRepository : IRepository<Product, RequestParameters>
{
    public Task<PagedList<Product>> GetProductsByCategory(Guid categoryId, RequestParameters requestParameters);
}
