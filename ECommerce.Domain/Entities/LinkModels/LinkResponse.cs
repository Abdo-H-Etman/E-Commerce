using System;
using ECommerce.Domain.Entities.Models;

namespace ECommerce.Domain.Entities.LinkModels;

public class LinkResponse
{
    public bool HasLinks { get; set; }
    public List<Entity> ShapedEntities { get; set; }
    public LinkCollectionWrapper<Entity> LinkedEntities { get; set; }
    public LinkResponse()
    {
        LinkedEntities = new LinkCollectionWrapper<Entity>();
        ShapedEntities = new List<Entity>();
    }
}
