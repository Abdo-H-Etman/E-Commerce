using System.Linq.Expressions;
using Ecommerce.Domain.RequestFeatures;
using ECommerce.Domain.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Domain.RequestFeatures;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

public class ProductRepository : GenericRepository<Product, RequestParameters>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context) {}

    public override async Task<PagedList<Product>> GetAll(RequestParameters requestParameters) 
    {
        var products = await _dbSet.Include(p => p.ProvidedBy)
                                    .Include(p => p.Reviews)
                                    .Include(p => p.Category)
                                    .OrderBy(p => p.Name)
                                    .ToListAsync();
        return PagedList<Product>.ToPagedList(products, requestParameters.PageNumber, requestParameters.PageSize);
    }
    public override async Task<IEnumerable<Product>> Filter(Expression<Func<Product, bool>> filter) =>
        await _dbSet.Include(p => p.ProvidedBy).Include(p => p.Reviews).Where(filter).ToListAsync();

    public override async Task<Product> GetById(Guid id) =>
        await _dbSet.Include(p => p.ProvidedBy).Include(p => p.Reviews).Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);

    public async Task<PagedList<Product>> GetProductsByCategory(Guid categoryId, RequestParameters requestParameters)
    {
        var products = await _dbSet.Include(p => p.ProvidedBy).Include(p => p.Reviews).Where(p => p.CategoryId == categoryId).ToListAsync();
        return PagedList<Product>.ToPagedList(products, requestParameters.PageNumber, requestParameters.PageSize);
    }
}