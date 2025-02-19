using System.Linq.Expressions;
using Ecommerce.Domain.RequestFeatures;
using ECommerce.Domain.Models;
using ECommerce.Domain.RequestFeatures;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

public class OrderItemRepository : GenericRepository<OrderItem, RequestParameters>
{
    public OrderItemRepository(AppDbContext context) : base(context) {}

    public override async Task<PagedList<OrderItem>> GetAll(RequestParameters requestParameters) 
    {
        var items = await _dbSet.Include(item => item.Product).Include(item => item.Order).ToListAsync();
        return PagedList<OrderItem>.ToPagedList(items, requestParameters.PageNumber, requestParameters.PageSize);
    }

        
    public override async Task<IEnumerable<OrderItem>> Filter(Expression<Func<OrderItem, bool>> filter) =>
        await _dbSet.Include(item => item.Product).Include(item => item.Order).Where(filter).ToListAsync();
}