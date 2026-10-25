using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace ClientHub.Logging;

public static class DependencyInjection
{
    public static IServiceCollection AddLogging(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSerilog(
            (services, loggerConfiguration) =>
                loggerConfiguration
                    .ReadFrom.Configuration(configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext()
        );

        return services;
    }
}
