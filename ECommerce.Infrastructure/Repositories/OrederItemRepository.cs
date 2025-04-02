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

    public override async Task<IEnumerable<OrderItem>> Filter(Expression<Func<OrderItem, bool>> filter) =>
        await _dbSet.Include(item => item.Product)
                    .Include(item => item.Order)
                    .Where(filter)
                    .ToListAsync();

    public async Task<PagedList<OrderItem>> GetSpecificOrderItems(RequestParameters requestParameters ,Guid orderId)
    {
        var products = await _dbSet.Include(item => item.Product)
                    .ThenInclude(product => product!.Category)
                    .Where(item => item.OrderId == orderId)
                    .ToListAsync();
        return PagedList<OrderItem>.ToPagedList(products, requestParameters.PageNumber, requestParameters.PageSize);
    }

    public async Task<PagedList<OrderItem>> GetOrderItemsByProductId(RequestParameters requestParameters ,Guid productId)
    {
        var products = await _dbSet.Include(item => item.Product)
                    .ThenInclude(product => product!.Category)
                    .Where(item => item.ProductId == productId)
                    .ToListAsync();  
        return PagedList<OrderItem>.ToPagedList(products, requestParameters.PageNumber, requestParameters.PageSize);
    }              
}