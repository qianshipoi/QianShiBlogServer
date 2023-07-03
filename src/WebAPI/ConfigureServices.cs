using Application.Common.Interfaces;

using Infrastructure.Persistence;

using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

using System.Reflection;

using WebAPI.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
  public static IServiceCollection AddWebAPIServices(this IServiceCollection services)
  {
    //services.AddDatabaseDeveloperPageExceptionFilter();

    services.AddScoped<ICurrentUserService, CurrentUserService>();

    services.AddHttpContextAccessor();

    services.AddHealthChecks()
        .AddDbContextCheck<ApplicationDbContext>();

    services.AddControllers();

    services.Configure<ApiBehaviorOptions>(options =>
      options.SuppressModelStateInvalidFilter = true);

    services.AddSwaggerGen(options =>
    {
      options.SwaggerDoc("v1", new OpenApiInfo
      {
        Version = "v1",
        Title = "ToDo API",
        Description = "An ASP.NET Core Web API for managing ToDo items",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
          Name = "Example Contact",
          Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
          Name = "Example License",
          Url = new Uri("https://example.com/license")
        }
      });

      // using System.Reflection;
      var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
      options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename), true);

      options.AddSecurityDefinition("JwtBearer", new OpenApiSecurityScheme()
      {
        Description = "这是方式一(直接在输入框中输入认证信息，不需要在开头添加Bearer)",
        Name = "Authorization",//jwt默认的参数名称
        In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
      });

      //声明一个Scheme，注意下面的Id要和上面AddSecurityDefinition中的参数name一致
      var scheme = new OpenApiSecurityScheme()
      {
        Reference = new OpenApiReference() { Type = ReferenceType.SecurityScheme, Id = "JwtBearer" }
      };
      //注册全局认证（所有的接口都可以使用认证）
      options.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        [scheme] = new string[0]
      });
    });

    return services;
  }

}
