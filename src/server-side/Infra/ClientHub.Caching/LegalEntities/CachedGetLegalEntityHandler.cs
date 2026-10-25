using System.Text.Json;
using ClientHub.UseCases.LegalEntities;
using ClientHub.UseCases.LegalEntities.GetLegalEntity;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;

namespace ClientHub.Caching.LegalEntities;

public sealed class CachedGetLegalEntityHandler
    : IRequestHandler<GetLegalEntityQuery, LegalEntityDto>
{
    private readonly IDistributedCache _cache;
    private readonly GetLegalEntityQueryHandler _inner;
    private readonly IConfiguration _configuration;

    public CachedGetLegalEntityHandler(
        IDistributedCache cache,
        GetLegalEntityQueryHandler inner,
        IConfiguration configuration
    )
    {
        _cache = cache;
        _inner = inner;
        _configuration = configuration;
    }

    public async Task<LegalEntityDto> Handle(
        GetLegalEntityQuery request,
        CancellationToken cancellationToken
    )
    {
        LegalEntityDto? legalEntityDto = null;
        var cacheString = await _cache.GetStringAsync(
            Constants.LegalEntity + request.Id,
            cancellationToken
        );

        if (cacheString != null)
            legalEntityDto = JsonSerializer.Deserialize<LegalEntityDto>(cacheString);

        if (legalEntityDto == null)
        {
            legalEntityDto = await _inner.Handle(request, cancellationToken);
            cacheString = JsonSerializer.Serialize(legalEntityDto);
            await _cache.SetStringAsync(
                Constants.LegalEntity + request.Id,
                cacheString,
                new DistributedCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(
                        int.Parse(_configuration["Caching:SingleExpirationInMinutes"] ?? "10")
                    ),
                },
                cancellationToken
            );
        }

        return legalEntityDto;
    }
}
