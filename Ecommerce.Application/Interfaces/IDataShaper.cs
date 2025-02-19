using System;
using System.Dynamic;
using ECommerce.Domain.Entities.Models;

namespace Ecommerce.Application.Interfaces;

public interface IDataShaper<T>
{
    IEnumerable<ShapedEntity> ShapeData(IEnumerable<T> entities, string fieldsString);
    ShapedEntity ShapeData(T entity, string fieldsString);

}
