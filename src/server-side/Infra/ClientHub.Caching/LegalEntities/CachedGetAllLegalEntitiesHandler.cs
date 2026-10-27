using System.Text.Json;
using ClientHub.UseCases.LegalEntities;
using ClientHub.UseCases.LegalEntities.GetAllLegalEntities;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;

namespace ClientHub.Caching.LegalEntities;

public sealed class CachedGetAllLegalEntitiesHandler
    : IRequestHandler<GetAllLegalEntitiesQuery, IEnumerable<ShortLegalEntityDto>>
{
    private readonly IDistributedCache _cache;
    private readonly GetAllLegalEntitiesQueryHandler _inner;
    private readonly IConfiguration _configuration;

    public CachedGetAllLegalEntitiesHandler(
        IDistributedCache cache,
        GetAllLegalEntitiesQueryHandler inner,
        IConfiguration configuration
    )
    {
        _cache = cache;
        _inner = inner;
        _configuration = configuration;
    }

    public async Task<IEnumerable<ShortLegalEntityDto>> Handle(
        GetAllLegalEntitiesQuery request,
        CancellationToken cancellationToken
    )
    {
        if (
            string.IsNullOrEmpty(request.Filter.Inn)
            && string.IsNullOrEmpty(request.Filter.Ogrn)
            && string.IsNullOrEmpty(request.Filter.Region)
            && request.Filter.Status == null
            && request.Filter.RegistrationDate == null
            && request.Filter.LiquidationDate == null
            && request.Filter.Limit == 100
            && request.Filter.Offset == 0
        )
        {
            IEnumerable<ShortLegalEntityDto>? dtos = null;
            var cacheString = await _cache.GetStringAsync(
                Constants.EmptyLegalEntityFilter,
                cancellationToken
            );

            if (cacheString != null)
                dtos = JsonSerializer.Deserialize<IEnumerable<ShortLegalEntityDto>>(cacheString);

            if (dtos == null)
            {
                dtos = await _inner.Handle(request, cancellationToken);
                cacheString = JsonSerializer.Serialize(dtos);
                await _cache.SetStringAsync(
                    Constants.EmptyLegalEntityFilter,
                    cacheString,
                    new DistributedCacheEntryOptions()
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(
                            int.Parse(_configuration["Caching:ExpirationInMinutes"] ?? "5")
                        ),
                    },
                    cancellationToken
                );
            }

            return dtos;
        }

        return await _inner.Handle(request, cancellationToken);
    }
}
