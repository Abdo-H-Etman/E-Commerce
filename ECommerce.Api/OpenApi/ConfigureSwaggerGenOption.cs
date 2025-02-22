using System;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ECommerce.Api.OpenApi;

public class ConfigureSwaggerGenOption : IConfigureNamedOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;
    public ConfigureSwaggerGenOption(IApiVersionDescriptionProvider provider) => _provider = provider;
    
    public void Configure(string? name, SwaggerGenOptions options)
    {
        Configure(options);
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach(var description in _provider.ApiVersionDescriptions)
        {
            var OpenApiInfo = new OpenApiInfo
            {
                Title = $"ECommerce API v{description.ApiVersion}",
                Version = description.ApiVersion.ToString(),
            };
            options.SwaggerDoc(description.GroupName, OpenApiInfo); 
        }
    }
}
