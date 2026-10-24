using System.Reflection;
using ClientHub.Domain;
using ClientHub.Domain.IndividualEntrepreneurs;
using ClientHub.Domain.LegalEntities;
using ClientHub.Persistense.EntityFramework.Repositories;
using ClientHub.UseCases.IndividualEntrepreneurs;
using ClientHub.UseCases.LegalEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClientHub.Persistense.EntityFramework;

public static class DependencyInjection
{
    public static IServiceCollection AddEfPersistense(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString =
            configuration.GetConnectionString("Default")
            ?? throw new InvalidDataException("Connection string is empty");

        services.AddDbContext<AppDbContext>(x => x.UseNpgsql(connectionString));

        services.AddScoped<IIndividualEntrepreneurRepository, IndividualEntrepreneurRepository>();
        services.AddScoped<ILegalEntityRepository, LegalEntityRepository>();
        services.AddScoped<
            IUseCaseIndividualEntrepreneursRepository,
            UseCaseIndividualEntrepreneurRepository
        >();
        services.AddScoped<IUseCaseLegalEntitiesRepository, UseCaseLegalEntityRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
