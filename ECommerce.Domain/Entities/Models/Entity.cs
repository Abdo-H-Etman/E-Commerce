using System;
using System.Dynamic;
using System.Xml;
using ECommerce.Domain.Entities.LinkModels;

namespace ECommerce.Domain.Entities.Models;

public class Entity : Dictionary<string, object>
{

    private void WriteLinksToXml(string key, object value, XmlWriter writer)
    {
        writer.WriteStartElement(key);
        if (value.GetType() == typeof(List<Link>))
        {
            foreach (var val in value as List<Link> ?? new List<Link>())
            {
                writer.WriteStartElement(nameof(Link));
                WriteLinksToXml(nameof(val.Href), val.Href, writer);
                WriteLinksToXml(nameof(val.Method), val.Method, writer);
                WriteLinksToXml(nameof(val.Rel), val.Rel, writer);
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

