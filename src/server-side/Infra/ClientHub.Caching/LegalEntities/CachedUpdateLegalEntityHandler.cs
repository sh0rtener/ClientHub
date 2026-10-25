using ClientHub.UseCases.LegalEntities;
using ClientHub.UseCases.LegalEntities.UpdateLegalEntity;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace ClientHub.Caching.LegalEntities;

public sealed class CachedUpdateLegalEntityHandler
    : IRequestHandler<UpdateLegalEntityCommand, LegalEntityDto>
{
    private readonly IDistributedCache _cache;
    private readonly UpdateLegalEntityCommandHandler _inner;

    public CachedUpdateLegalEntityHandler(
        IDistributedCache cache,
        UpdateLegalEntityCommandHandler handler
    )
    {
        _cache = cache;
        _inner = handler;
    }

    public async Task<LegalEntityDto> Handle(
        UpdateLegalEntityCommand request,
        CancellationToken cancellationToken
    )
    {
        await _cache.RemoveAsync(Constants.EmptyIndividualEntrepreneursFilter, cancellationToken);
        await _cache.RemoveAsync(Constants.EmptyLegalEntityFilter, cancellationToken);
        await _cache.RemoveAsync(Constants.LegalEntity + request.Id, cancellationToken);
        return await _inner.Handle(request, cancellationToken);
    }
}
