using System.Xml;
using ECommerce.Domain.Entities.LinkModels;

namespace ECommerce.Domain.Generics;

public abstract class IdModel
{
    public Guid Id {get; set;}

    private void WriteLinksToXml(string key, object value, XmlWriter writer)
    {
        writer.WriteStartElement(key);
        if (value.GetType() == typeof(List<Link>))
        {
            foreach (var val in value as List<Link> ?? new List<Link>())
            {
                writer.WriteStartElement(nameof(Link));
                WriteLinksToXml(nameof(val.Href), val.Href ?? string.Empty, writer);
                WriteLinksToXml(nameof(val.Method), val.Method ?? string.Empty, writer);
                WriteLinksToXml(nameof(val.Rel), val.Rel ?? string.Empty, writer);
                writer.WriteEndElement();
            }
        }
        else
        {
            writer.WriteString(value.ToString());
        }
        writer.WriteEndElement();
    }

}
