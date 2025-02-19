using Ecommerce.Domain.RequestFeatures;
using Microsoft.AspNetCore.Http;
namespace ECommerce.Domain.Entities.LinkModels;

public record  LinkParameters(UserParameters UserParameters, HttpContext HttpContext);
