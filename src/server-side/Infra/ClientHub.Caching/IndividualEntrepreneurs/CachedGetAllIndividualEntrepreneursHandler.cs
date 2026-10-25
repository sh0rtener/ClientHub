using System.Text.Json;
using ClientHub.UseCases.IndividualEntrepreneurs;
using ClientHub.UseCases.IndividualEntrepreneurs.GetAllIndividualEntrepreneurs;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;

namespace ClientHub.Caching.IndividualEntrepreneurs;

public sealed class CachedGetAllIndividualEntrepreneurHandler
    : IRequestHandler<
        GetAllIndividualEntrepreneursQuery,
        IEnumerable<ShortIndividualEntrepreneursDto>
    >
{
    private readonly IDistributedCache _cache;
    private readonly GetAllIndividualEntrepreneursQueryHandler _inner;
    private readonly IConfiguration _configuration;

    public CachedGetAllIndividualEntrepreneurHandler(
        IDistributedCache cache,
        GetAllIndividualEntrepreneursQueryHandler inner,
        IConfiguration configuration
    )
    {
        _inner = inner;
        _cache = cache;
        _configuration = configuration;
    }

    public async Task<IEnumerable<ShortIndividualEntrepreneursDto>> Handle(
        GetAllIndividualEntrepreneursQuery request,
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
            var cacheString = await _cache.GetStringAsync(
                Constants.EmptyIndividualEntrepreneursFilter,
                cancellationToken
            );

            List<ShortIndividualEntrepreneursDto>? result = null;

            if (cacheString != null)
                result = JsonSerializer.Deserialize<List<ShortIndividualEntrepreneursDto>>(
                    cacheString
                );

            if (result == null)
            {
                result = (await _inner.Handle(request, cancellationToken)).ToList();
                cacheString = JsonSerializer.Serialize(result);
                await _cache.SetStringAsync(
                    Constants.EmptyIndividualEntrepreneursFilter,
                    cacheString,
                    new DistributedCacheEntryOptions()
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(
                            int.Parse(_configuration["Caching:ExpirationInMinutes"] ?? "5")
                        ),
                    }, cancellationToken
                );

            }
            
            return result;
        }
        
        return await _inner.Handle(request, cancellationToken);
    }
}
