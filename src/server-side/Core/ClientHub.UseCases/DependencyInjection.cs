using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace ClientHub.UseCases;

public static class DependencyInjection
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddAutoMapper(c =>
        {
            c.AddMaps(Assembly.GetExecutingAssembly());
        });
        services.AddMediatR(c =>
            c.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
        );

        return services;
    }
}
