using System.Text;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Asp.Versioning;
using AspNetCoreRateLimit;
using Ecommerce.Application.Dtos.List;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Services;
using Ecommerce.Application.Services.DataShaping;
using Ecommerce.Application.Services.LoggerService;
using Ecommerce.Application.Validations;
using ECommerce.Api.Utility;
using ECommerce.Domain.Entities.ConfigurationModels;
using ECommerce.Domain.Interfaces;
using ECommerce.Domain.Models;
using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Repositories;
using Marvin.Cache.Headers;
using System.Net;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;

namespace ECommerce.Api.Extensions;

public static class ServicesExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithExposedHeaders("X-Pagination"));
        });

    public static void ConfigureIISIntegration(this IServiceCollection services) =>
        services.Configure<IISOptions>(options =>
        {

        });

    public static void ConfigureLoggerService(this IServiceCollection services) =>
        services.AddSingleton<ILoggerManager, LoggerManager>();

    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager, RepositoryManager>();

    public static void ConfigureServiceManager(this IServiceCollection services) =>
        services.AddScoped<IServiceManager, ServiceManager>();
    public static IServiceCollection ConfigureDbContextPool(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContextPool<AppDbContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    public static void AddFilters(this IServiceCollection services)
    {
        services.AddScoped<ValidationFilterAttribute>();
        services.AddScoped<ValidateMediaTypeAttribute>();
    }
    public static void ConfigureDataShaping(this IServiceCollection services)
    {
        services.AddScoped<IDataShaper<UserDto>, DataShaper<UserDto>>();
    }
    public static void AddCustomMediaTypes(this IServiceCollection services)
    {
        services.Configure<MvcOptions>(config =>
        {
            var systemTextJsonOutputFormatter = config.OutputFormatters
            .OfType<SystemTextJsonOutputFormatter>()?.FirstOrDefault();
            if (systemTextJsonOutputFormatter != null)
            {
                systemTextJsonOutputFormatter.SupportedMediaTypes
                .Add("application/vnd.codemaze.hateoas+json");
                systemTextJsonOutputFormatter.SupportedMediaTypes
                .Add("application/vnd.codemaze.apiroot+json");
            }
            var xmlOutputFormatter = config.OutputFormatters
                .OfType<XmlDataContractSerializerOutputFormatter>()?
                .FirstOrDefault();
            if (xmlOutputFormatter != null)
            {
                xmlOutputFormatter.SupportedMediaTypes
                .Add("application/vnd.codemaze.hateoas+xml");
                xmlOutputFormatter.SupportedMediaTypes
                .Add("application/vnd.codemaze.apiroot+xml");
            }
        });
    }

    public static void ConfigureVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(opt =>
        {
            opt.ReportApiVersions = true;
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.DefaultApiVersion = new ApiVersion(1,0);
            opt.ApiVersionReader = new UrlSegmentApiVersionReader();
        })
        .AddMvc()
        .AddApiExplorer(opt =>
        {
            opt.GroupNameFormat = "'v'VVV";
            opt.SubstituteApiVersionInUrl = true;
        });
    }
    public static void ConfigureLinkService(this IServiceCollection services) =>
        services.AddScoped<IEntityLinks<UserDto>, EntityLinks<UserDto>>();

    public static void ConfigureResponseCaching(this IServiceCollection services) =>
        services.AddResponseCaching();

    public static void ConfigureHttpCacheHeaders(this IServiceCollection services) =>
        services.AddHttpCacheHeaders(
            (expirationOpt) =>
            {
                expirationOpt.MaxAge = 65;
                expirationOpt.CacheLocation = CacheLocation.Private;
            },
            (validationOpt) =>
            {
                validationOpt.MustRevalidate = true;
            });

    public static void ConfigureRateLimitingOptions(this IServiceCollection services)
    {
        var rateLimitRules = new List<RateLimitRule>
        {
            new RateLimitRule
            {
                Endpoint = "*",
                Limit = 100,
                Period = "5m"
            }
        };
        services.Configure<IpRateLimitOptions>(opt =>
        {
            opt.GeneralRules = rateLimitRules;
        });
        services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
        services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
    } 

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        var builder = services.AddIdentity<User, IdentityRole<Guid>>(o =>
        {
            o.Password.RequireDigit = false;
            o.Password.RequireLowercase = false;
            o.Password.RequireUppercase = false;
            o.Password.RequireNonAlphanumeric = false;
            o.Password.RequiredLength = 10;
            o.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();
    }

    public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
    {

        var jwtConfiguration = new JwtConfiguration();
        configuration.Bind(jwtConfiguration.Section, jwtConfiguration);
        var secretKey = Environment.GetEnvironmentVariable("SECRET");

        services.AddAuthentication()
        .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtConfiguration.ValidIssuer,
                ValidAudience = jwtConfiguration.ValidAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))
            };
        });
        
    }

    public static void ConfigureCookies(this IServiceCollection services)
    {
        // services.AddAuthorization()
        //         .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        //         .AddCookie();
        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/api/v1/Authentication/Login";
            options.LogoutPath = "/api/v1/Authentication/Logout";
            options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            options.SlidingExpiration = true;
            options.AccessDeniedPath = "/api/v1/Authentication/AccessDenied";
            options.Cookie.HttpOnly = true;
            options.Cookie.SameSite = SameSiteMode.None;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        });        
    }
    public static void AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)=>
        services.Configure<JwtConfiguration>(configuration.GetSection("JwtSettings"));
    
    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(s =>
        {
            s.SwaggerDoc("v1", new OpenApiInfo { 
                Title = "ECommerce API", 
                Version = "v1", 
                Description = "ECommerce API by SE Abdo Hatem",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Abdo Hatem",
                    Email = "abdoetman010@gmail.com",
                    Url = new Uri("https://www.linkedin.com/in/abdelrahman-othman-430402316/")
                },
                License = new OpenApiLicense
                {
                    Name = "Use under LICX",
                    Url = new Uri("https://example.com/license")
                }});

            s.SwaggerDoc("v2", new OpenApiInfo { Title = "ECommerce API", Version = "v2" });

            var xmlFile = $"{typeof(Program).Assembly.GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            s.IncludeXmlComments(xmlPath);

            
        });
    }
}
