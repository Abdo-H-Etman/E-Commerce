using System.Text.Json.Serialization;
using Asp.Versioning.ApiExplorer;
using AspNetCoreRateLimit;
using Ecommerce.Application.Interfaces;
using ECommerce.Api;
using ECommerce.Api.Extensions;
using ECommerce.Api.OpenApi;
using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
// Add services to the container.

builder.Services.AddControllers(config =>
{
    config.CacheProfiles.Add("120SecondsDuration", new CacheProfile
    {
        Duration = 120
    });
}).AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFilters();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureDataShaping();
builder.Services.ConfigureDbContextPool(builder.Configuration);

builder.Services.Configure<ApiBehaviorOptions>(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                });
builder.Services.ConfigureLinkService();                
builder.Services.ConfigureVersioning();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddRepositories();
builder.Services.AddCustomMediaTypes();
// builder.Services.ConfigureOptions<ConfigureSwaggerGenOption>();
builder.Services.ConfigureResponseCaching();
builder.Services.ConfigureHttpCacheHeaders();
builder.Services.AddMemoryCache();
builder.Services.ConfigureRateLimitingOptions();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
// builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.ConfigureCookies();
// builder.Services.AddJwtConfiguration(builder.Configuration);

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint("/swagger/v1/swagger.json", "ECommerce API v1");
        opt.SwaggerEndpoint("/swagger/v2/swagger.json", "ECommerce API v2");
        // IReadOnlyList<ApiVersionDescription>? descriptions = app.DescribeApiVersions();
        // foreach(ApiVersionDescription description in descriptions)
        // {
        //     string url = $"/swagger/{description.GroupName}/swagger.json";
        //     string name = description.GroupName.ToUpperInvariant();
        //     opt.SwaggerEndpoint(url, name);
        // }
    });
}

app.UseIpRateLimiting();
app.UseCors("CorsPolicy"); 

app.UseResponseCaching();
app.UseHttpCacheHeaders();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
