using System;
using ECommerce.Domain.Entities.LinkModels;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Interfaces;

public interface IEntityLinks<T> 
{
    LinkResponse TryGenerateLinks(IEnumerable<T> entities, string fieldsString, HttpContext httpContext);
}
