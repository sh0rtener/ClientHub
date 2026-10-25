using ClientHub.UseCases.IndividualEntrepreneurs.AddIndividualEntrepreneur;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace ClientHub.Caching.IndividualEntrepreneurs;

public sealed class CachedAddIndividualEntrepreneurHandler
    : IRequestHandler<AddIndividualEntrepreneurCommand, int>
{
    private readonly AddIndividualEntrepreneurCommandHandler _inner;
    private readonly IDistributedCache _cache;

    public CachedAddIndividualEntrepreneurHandler(
        AddIndividualEntrepreneurCommandHandler inner,
        IDistributedCache cache
    )
    {
        _inner = inner;
        _cache = cache;
    }

    public async Task<int> Handle(
        AddIndividualEntrepreneurCommand request,
        CancellationToken cancellationToken
    )
    {
        await _cache.RemoveAsync(Constants.EmptyIndividualEntrepreneursFilter, cancellationToken);
        await _cache.RemoveAsync(Constants.EmptyLegalEntityFilter, cancellationToken);
        return await _inner.Handle(request, cancellationToken);
    }
}
