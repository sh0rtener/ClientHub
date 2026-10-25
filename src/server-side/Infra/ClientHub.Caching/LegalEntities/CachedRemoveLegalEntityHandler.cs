using ClientHub.UseCases.LegalEntities.RemoveLegalEntity;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace ClientHub.Caching.LegalEntities;

public sealed class CachedRemoveLegalEntityHandler : IRequestHandler<RemoveLegalEntityCommand>
{
    private readonly IDistributedCache _cache;
    private readonly RemoveLegalEntityCommandHandler _inner;

    public CachedRemoveLegalEntityHandler(
        IDistributedCache cache,
        RemoveLegalEntityCommandHandler inner
    )
    {
        _cache = cache;
        _inner = inner;
    }

    public async Task Handle(RemoveLegalEntityCommand request, CancellationToken cancellationToken)
    {
        await _cache.RemoveAsync(Constants.EmptyIndividualEntrepreneursFilter, cancellationToken);
        await _cache.RemoveAsync(Constants.EmptyLegalEntityFilter, cancellationToken);
        await _cache.RemoveAsync(Constants.LegalEntity + request.Id, cancellationToken);
        await _inner.Handle(request, cancellationToken);
    }
}
