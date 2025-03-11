using System.Linq.Expressions;
using Ecommerce.Domain.RequestFeatures;
using ECommerce.Domain.Models;
using ECommerce.Domain.RequestFeatures;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

public class ProductRepository : GenericRepository<Product, RequestParameters>
{
    public ProductRepository(AppDbContext context) : base(context) {}

    public override async Task<PagedList<Product>> GetAll(RequestParameters requestParameters) 
    {
        var products = await _dbSet.OrderBy(p => p.Name)/*.Include(item => item.ProvidedBy)*/.Include(item => item.Reviews).ToListAsync();
        return PagedList<Product>.ToPagedList(products, requestParameters.PageNumber, requestParameters.PageSize);
    }
    public override async Task<IEnumerable<Product>> Filter(Expression<Func<Product, bool>> filter) =>
        await _dbSet./*Include(item => item.ProvidedBy).*/Include(item => item.Reviews).Where(filter).ToListAsync();

       

}