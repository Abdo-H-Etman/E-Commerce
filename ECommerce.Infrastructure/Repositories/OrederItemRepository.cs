using System.Linq.Expressions;
using Ecommerce.Domain.RequestFeatures;
using ECommerce.Domain.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Domain.RequestFeatures;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

public class OrderItemRepository : GenericRepository<OrderItem, RequestParameters> ,IOrderItemRepository
{
    public OrderItemRepository(AppDbContext context) : base(context) {}

    public override async Task<PagedList<OrderItem>> GetAll(RequestParameters requestParameters) 
    {
        var items = await _dbSet.Include(item => item.Product)
                                .Include(item => item.Order)
                                .ToListAsync();
        return PagedList<OrderItem>.ToPagedList(items, requestParameters.PageNumber, requestParameters.PageSize);
    }

    public async Task<IEnumerable<OrderItem>> GetSpecificOrderItems(Guid orderId) =>
        await _dbSet.Include(item => item.Product)
                    // .Include(item => item.Order)
                    .Where(item => item.OrderId == orderId)
                    .ToListAsync();

    public override async Task<IEnumerable<OrderItem>> Filter(Expression<Func<OrderItem, bool>> filter) =>
        await _dbSet.Include(item => item.Product)
                    .Include(item => item.Order)
                    .Where(filter)
                    .ToListAsync();

    public async Task<IEnumerable<OrderItem>> GetSpecificOrderItems(RequestParameters orderItemParameters, Guid orderId, bool trackChanges) =>
        await _dbSet.Include(item => item.Product)
                    .ThenInclude(product => product.Category)
                    .Where(item => item.OrderId == orderId)
                    .ToListAsync();
}