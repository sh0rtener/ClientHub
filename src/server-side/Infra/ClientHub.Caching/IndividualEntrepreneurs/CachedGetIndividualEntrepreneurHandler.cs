using System.Text.Json;
using ClientHub.UseCases.IndividualEntrepreneurs;
using ClientHub.UseCases.IndividualEntrepreneurs.GetIndividualEntrepreneur;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;

namespace ClientHub.Caching.IndividualEntrepreneurs;

public sealed class CachedGetIndividualEntrepreneurHandler
    : IRequestHandler<GetIndividualEntrepreneurQuery, IndividualEntrepreneursDto>
{
    private readonly IDistributedCache _cache;
    private readonly GetIndividualEntrepreneurQueryHandler _inner;
    private readonly IConfiguration _configuration;

    public CachedGetIndividualEntrepreneurHandler(
        IDistributedCache cache,
        GetIndividualEntrepreneurQueryHandler inner,
        IConfiguration configuration
    )
    {
        _cache = cache;
        _inner = inner;
        _configuration = configuration;
    }

    public async Task<IndividualEntrepreneursDto> Handle(
        GetIndividualEntrepreneurQuery request,
        CancellationToken cancellationToken
    )
    {
        IndividualEntrepreneursDto? ip = null;
        var cacheString = await _cache.GetStringAsync(
            Constants.IndividualEntrepreneurs + request.Id,
            cancellationToken
        );

        if (cacheString != null)
            ip = JsonSerializer.Deserialize<IndividualEntrepreneursDto>(cacheString);

        if (ip == null)
        {
            ip = await _inner.Handle(request, cancellationToken);
            cacheString = JsonSerializer.Serialize(ip);
            await _cache.SetStringAsync(
                Constants.IndividualEntrepreneurs + request.Id,
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

        return ip;
    }
}
