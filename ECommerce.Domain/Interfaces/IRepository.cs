using System.Linq.Expressions;
using Ecommerce.Domain.RequestFeatures;
using ECommerce.Domain.Generics;
using ECommerce.Domain.RequestFeatures;

namespace ECommerce.Domain.Interfaces;

public interface IRepository<TModel, TParams> where TModel : class where TParams : RequestParameters
{
    Task<PagedList<TModel>> GetAll(TParams parameters);
    Task<IEnumerable<TModel>> Filter(Expression<Func<TModel,bool>> func);
    Task<TModel> GetById(Guid id);
    Task<TModel> Add(TModel model);
    TModel Update(TModel model);
    void Delete(TModel model);
}    