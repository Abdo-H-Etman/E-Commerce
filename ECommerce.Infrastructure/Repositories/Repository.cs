using System.Linq.Expressions;
using Ecommerce.Domain.RequestFeatures;
using ECommerce.Domain.Generics;
using ECommerce.Domain.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Domain.RequestFeatures;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

public class GenericRepository<TModel,TParams> : IRepository<TModel, TParams> where TModel : class where TParams : RequestParameters
{
    protected readonly AppDbContext _context;
    protected DbSet<TModel> _dbSet;
    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TModel>();
    }

    public async virtual Task<PagedList<TModel>> GetAll(TParams requestParameters)
    {
        var models = await _dbSet.ToListAsync();
        var count = await _dbSet.CountAsync();
        return new PagedList<TModel>(models, count, requestParameters.PageNumber, requestParameters.PageSize);
    }

    public async virtual Task<IEnumerable<TModel>> Filter(Expression<Func<TModel, bool>> func)=>
        await _dbSet.Where(func).ToListAsync();

    public async virtual Task<TModel> GetById(Guid id)=>
        await _dbSet.FindAsync(id);

    public async virtual Task<TModel> Add(TModel model)=>
        (await _dbSet.AddAsync(model)).Entity;

    public virtual TModel Update(TModel model)=>
        _dbSet.Update(model).Entity;
    public virtual void Delete(TModel model)=>
        _dbSet.Remove(model);
}