using System.Reflection;
using ClientHub.Persistense.EntityFramework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClientHub.Persistense;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistense(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddAutoMapper(c =>
        {
            c.AddMaps(Assembly.GetExecutingAssembly());
        });

        services.AddEfPersistense(configuration);

        return services;
    }
}
