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
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Enter 'Bearer' [space] and then your token in the text input below.\nExample: 'Bearer abc123'"
        });

        // Add security requirement
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new List<string>() // Scopes (empty for JWT)
            }
        });

        var addedGroupNames = new HashSet<string>(); // Track added group names to avoid duplicates
        
        // foreach(var description in _provider.ApiVersionDescriptions)
        // {
        //     if (!addedGroupNames.Add(description.GroupName))
        //     {
        //         // Skip duplicate group names
        //         continue;
        //     }
        //     var OpenApiInfo = new OpenApiInfo
        //     {
        //         Title = $"ECommerce API v{description.ApiVersion}",
        //         Version = description.ApiVersion.ToString(),
        //     };
        //     options.SwaggerDoc(description.GroupName, OpenApiInfo); 
        // }
    }
}
