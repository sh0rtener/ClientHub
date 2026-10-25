using ClientHub.Caching.IndividualEntrepreneurs;
using ClientHub.Caching.LegalEntities;
using ClientHub.UseCases.IndividualEntrepreneurs;
using ClientHub.UseCases.IndividualEntrepreneurs.AddIndividualEntrepreneur;
using ClientHub.UseCases.IndividualEntrepreneurs.GetAllIndividualEntrepreneurs;
using ClientHub.UseCases.IndividualEntrepreneurs.GetIndividualEntrepreneur;
using ClientHub.UseCases.IndividualEntrepreneurs.RemoveIndividualEntrepreneur;
using ClientHub.UseCases.IndividualEntrepreneurs.UpdateIndividualEntrepreneur;
using ClientHub.UseCases.LegalEntities;
using ClientHub.UseCases.LegalEntities.AddLegalEntity;
using ClientHub.UseCases.LegalEntities.GetAllLegalEntities;
using ClientHub.UseCases.LegalEntities.GetLegalEntity;
using ClientHub.UseCases.LegalEntities.RemoveLegalEntity;
using ClientHub.UseCases.LegalEntities.UpdateLegalEntity;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClientHub.Caching;

public static class DependencyInjection
{
    public static IServiceCollection AddCaching(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration =
                configuration["Caching:Redis:Configuration"]
                ?? throw new InvalidDataException("Не задано значение конфигурации Redis");
            options.InstanceName =
                configuration["Caching:Redis:InstanceName"]
                ?? throw new InvalidDataException("Не задано значение инстанса Redis");
        });

        services.AddMediatR(c =>
            c.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
        );

        services.AddScoped<AddIndividualEntrepreneurCommandHandler>();
        services.AddScoped<CachedAddIndividualEntrepreneurHandler>();
        services.AddScoped<IRequestHandler<AddIndividualEntrepreneurCommand, int>>(sp =>
            sp.GetRequiredService<CachedAddIndividualEntrepreneurHandler>()
        );

        services.AddScoped<GetAllIndividualEntrepreneursQueryHandler>();
        services.AddScoped<CachedGetAllIndividualEntrepreneurHandler>();
        services.AddScoped<
            IRequestHandler<
                GetAllIndividualEntrepreneursQuery,
                IEnumerable<ShortIndividualEntrepreneursDto>
            >
        >(sp => sp.GetRequiredService<CachedGetAllIndividualEntrepreneurHandler>());

        services.AddScoped<GetIndividualEntrepreneurQueryHandler>();
        services.AddScoped<CachedGetIndividualEntrepreneurHandler>();
        services.AddScoped<
            IRequestHandler<GetIndividualEntrepreneurQuery, IndividualEntrepreneursDto>
        >(sp => sp.GetRequiredService<CachedGetIndividualEntrepreneurHandler>());

        services.AddScoped<UpdateIndividualEntrepreneurCommandHandler>();
        services.AddScoped<CachedUpdateIndividualEntrepreneursHandler>();
        services.AddScoped<
            IRequestHandler<UpdateIndividualEntrepreneurCommand, IndividualEntrepreneursDto>
        >(sp => sp.GetRequiredService<CachedUpdateIndividualEntrepreneursHandler>());

        services.AddScoped<RemoveIndividualEntrepreneurCommandHandler>();
        services.AddScoped<CachedRemoveIndividualEntrepreneurHandler>();
        services.AddScoped<IRequestHandler<RemoveIndividualEntrepreneurCommand>>(sp =>
            sp.GetRequiredService<CachedRemoveIndividualEntrepreneurHandler>()
        );

        // ---

        services.AddScoped<AddLegalEntityCommandHandler>();
        services.AddScoped<CachedAddLegalEntityHandler>();
        services.AddScoped<IRequestHandler<AddLegalEntityCommand, int>>(sp =>
            sp.GetRequiredService<CachedAddLegalEntityHandler>()
        );

        services.AddScoped<GetAllLegalEntitiesQueryHandler>();
        services.AddScoped<CachedGetAllLegalEntitiesHandler>();
        services.AddScoped<
            IRequestHandler<GetAllLegalEntitiesQuery, IEnumerable<ShortLegalEntityDto>>
        >(sp => sp.GetRequiredService<CachedGetAllLegalEntitiesHandler>());

        services.AddScoped<GetLegalEntityQueryHandler>();
        services.AddScoped<CachedGetLegalEntityHandler>();
        services.AddScoped<IRequestHandler<GetLegalEntityQuery, LegalEntityDto>>(sp =>
            sp.GetRequiredService<CachedGetLegalEntityHandler>()
        );

        services.AddScoped<UpdateLegalEntityCommandHandler>();
        services.AddScoped<CachedUpdateLegalEntityHandler>();
        services.AddScoped<IRequestHandler<UpdateLegalEntityCommand, LegalEntityDto>>(sp =>
            sp.GetRequiredService<CachedUpdateLegalEntityHandler>()
        );

        services.AddScoped<RemoveLegalEntityCommandHandler>();
        services.AddScoped<CachedRemoveLegalEntityHandler>();
        services.AddScoped<IRequestHandler<RemoveLegalEntityCommand>>(sp =>
            sp.GetRequiredService<CachedRemoveLegalEntityHandler>()
        );

        return services;
    }
}
