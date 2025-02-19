using System.Linq.Expressions;
using Ecommerce.Domain.RequestFeatures;
using ECommerce.Domain.Models;
using ECommerce.Domain.RequestFeatures;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

public class OrderRepository : GenericRepository<Order, RequestParameters>
{
    public OrderRepository(AppDbContext context) : base(context) {}
    public override async Task<PagedList<Order>> GetAll(RequestParameters requestParameters) 
    {
        var orders = await _dbSet.Include(item => item.User).Include(item => item.carrier).ToListAsync();
    
        return PagedList<Order>.ToPagedList(orders, requestParameters.PageNumber, requestParameters.PageSize);
    }

    public override async Task<IEnumerable<Order>> Filter(Expression<Func<Order, bool>> filter) =>
        await _dbSet.Include(item => item.User).Include(item => item.carrier).Where(filter).ToListAsync();
}