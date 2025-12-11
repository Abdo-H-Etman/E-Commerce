

using Ecommerce.Domain.RequestFeatures;

namespace ECommerce.Domain.RequestFeatures;

public class OrderParameters : RequestParameters
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; } = DateTime.UtcNow;
    public bool ValidDateRange => EndDate >= StartDate;
    public string? SearchTerm { get; set; }
    public string OrderBy { get; set; } = "createdAt";
}
