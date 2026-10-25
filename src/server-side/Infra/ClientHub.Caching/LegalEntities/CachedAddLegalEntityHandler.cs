using ClientHub.UseCases.LegalEntities.AddLegalEntity;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace ClientHub.Caching.LegalEntities;

public sealed class CachedAddLegalEntityHandler : IRequestHandler<AddLegalEntityCommand, int>
{
    private readonly IDistributedCache _cache;
    private readonly AddLegalEntityCommandHandler _inner;

    public CachedAddLegalEntityHandler(
        IDistributedCache cache,
        AddLegalEntityCommandHandler handler
    )
    {
        _cache = cache;
        _inner = handler;
    }

    public async Task<int> Handle(
        AddLegalEntityCommand request,
        CancellationToken cancellationToken
    )
    {
        await _cache.RemoveAsync(Constants.EmptyIndividualEntrepreneursFilter, cancellationToken);
        await _cache.RemoveAsync(Constants.EmptyLegalEntityFilter, cancellationToken);

        return await _inner.Handle(request, cancellationToken);
    }
}
