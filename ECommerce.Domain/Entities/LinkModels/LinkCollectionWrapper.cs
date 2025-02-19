using System;

namespace ECommerce.Domain.Entities.LinkModels;

public class LinkCollectionWrapper<T> : LinkResourceBase
{
    public List<T> Value { get; set; } = new List<T>();
    public LinkCollectionWrapper(List<T> value) => Value = value;
    public LinkCollectionWrapper()
    {
    }
}
