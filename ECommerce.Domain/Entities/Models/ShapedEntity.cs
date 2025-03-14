
using ECommerce.Domain.Generics;

namespace ECommerce.Domain.Entities.Models;

public class ShapedEntity
{
    public ShapedEntity()
    {
        Entity = new Entity();
    }
    public Guid Id { get; set; }
    public Entity Entity { get; set; }
}
