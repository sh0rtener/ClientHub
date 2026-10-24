using Microsoft.OpenApi.Models;

namespace ClientHub.WebApi;

public static class WebConfiguration
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(o =>
        {
            o.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ClientHub API Documentation",
                    Description = "Документация для API ClientHub.",
                    Contact = new OpenApiContact
                    {
                        Name = "Github разработчика",
                        Url = new Uri("https://github.com/sh0rtener"),
                    },
                }
            );

            o.AddSecurityRequirement(
                new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "Jwt",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        Array.Empty<string>()
                    },
                }
            );

            var basePath = AppContext.BaseDirectory;
            var xmlPath = Path.Combine(basePath, "ClientHub.WebApi.xml");
            o.IncludeXmlComments(xmlPath);
        });

        return services;
    }
}
