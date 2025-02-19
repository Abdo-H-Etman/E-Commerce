using System.Reflection;
using Microsoft.Net.Http.Headers;
using Ecommerce.Application.Interfaces;
using ECommerce.Domain.Entities.LinkModels;
using ECommerce.Domain.Entities.Models;
using ECommerce.Domain.Generics;
// using System.Net.Http.Headers;
// using System.Net.Http.Headers;

namespace ECommerce.Api.Utility;

public class EntityLinks<T> : IEntityLinks<T> 
{
    private readonly LinkGenerator _linkGenerator;
    private readonly IDataShaper<T> _dataShaper;
    public EntityLinks(LinkGenerator linkGenerator, IDataShaper<T> dataShaper)
    {
        _linkGenerator = linkGenerator;
        _dataShaper = dataShaper;
    }

    public LinkResponse TryGenerateLinks(IEnumerable<T> entitysDto, string fields, HttpContext httpContext)
    {
        var shapedEntities = ShapeData(entitysDto, fields);
        if (ShouldGenerateLinks(httpContext))
            return ReturnLinkdedEntities(entitysDto, fields, httpContext, shapedEntities);
        return ReturnShapedEntities(shapedEntities);
    }

    private List<Entity> ShapeData(IEnumerable<T> entitysDto, string fields) =>
        _dataShaper.ShapeData(entitysDto, fields)
                   .Select(e => e.Entity)
                   .ToList();

    private bool ShouldGenerateLinks(HttpContext httpContext)
    {
        var mediaType = (System.Net.Http.Headers.MediaTypeHeaderValue)httpContext.Items["AcceptHeaderMediaType"]! ;
        // if (mediaType == null)
        // {
        //     throw new InvalidOperationException("AcceptHeaderMediaType is not present in the HttpContext items.");
        // }
        return mediaType.MediaType!.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
    }
    private LinkResponse ReturnShapedEntities(List<Entity> shapedEntities) =>
     new LinkResponse { ShapedEntities = shapedEntities };

    private LinkResponse ReturnLinkdedEntities(IEnumerable<T> entitysDto,
        string fields, HttpContext httpContext, List<Entity> shapedEntities)
    {
        var entityDtoList = entitysDto.ToList();
        for (var index = 0; index < entityDtoList.Count(); index++)
        {
            var idProperty = entityDtoList[index]!.GetType().GetProperty("Id");
            var idValue = idProperty!.GetValue(entityDtoList[index]);
            var entityLinks = CreateLinksForEntity(httpContext, (Guid)idValue!, fields);
            shapedEntities[index].Add("Links", entityLinks);
        }
        var entityCollection = new LinkCollectionWrapper<Entity>(shapedEntities);
        var linkedEntities = CreateLinksForEntities(httpContext, entityCollection);
        return new LinkResponse { HasLinks = true, LinkedEntities = linkedEntities };
    }

    private List<Link> CreateLinksForEntity(HttpContext httpContext, Guid id, string fields = "")
    {
        var type = typeof(T).Name.Remove(typeof(T).Name.Length - 3);
        var links = new List<Link>
            {
            new Link(_linkGenerator.GetUriByAction(httpContext, $"Get{type}",
            values: new {  id, fields })!,
            "self",
            "GET"),
            new Link(_linkGenerator.GetUriByAction(httpContext,
            $"Delete{type}", values: new { id })!,
            $"delete_{type.ToLower()}",
            "DELETE"),
            new Link(_linkGenerator.GetUriByAction(httpContext,
            $"Update{type}", values: new { id })!,
            $"update_{type.ToLower()}",
            "PUT")
            };
        return links;
    }
    private LinkCollectionWrapper<Entity> CreateLinksForEntities(HttpContext httpContext,
        LinkCollectionWrapper<Entity> entitysWrapper)
    {
        entitysWrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(httpContext,
        "GetAllUsers", values: new { })!,
        "self",
        "GET"));
        return entitysWrapper;
    }

    
}
