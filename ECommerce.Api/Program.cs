using Asp.Versioning.ApiExplorer;
using Ecommerce.Application.Interfaces;
using ECommerce.Api;
using ECommerce.Api.Extensions;
using ECommerce.Api.OpenApi;
using ECommerce.Infrastructure.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFilters();
builder.Services.AddSwaggerGen();
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
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddRepositories();
builder.Services.AddCustomMediaTypes();
builder.Services.ConfigureOptions<ConfigureSwaggerGenOption>();

var app = builder.Build();
var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        IReadOnlyList<ApiVersionDescription>? descriptions = app.DescribeApiVersions();
        foreach(ApiVersionDescription description in descriptions)
        {
            string url = $"/swagger/{description.GroupName}/swagger.json";
            string name = description.GroupName.ToUpperInvariant();
            opt.SwaggerEndpoint(url, name);
        }
    });
}

app.UseCors("CorsPolicy"); 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
