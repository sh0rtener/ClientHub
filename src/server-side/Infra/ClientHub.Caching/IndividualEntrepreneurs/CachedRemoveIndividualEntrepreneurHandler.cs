using ClientHub.UseCases.IndividualEntrepreneurs.RemoveIndividualEntrepreneur;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace ClientHub.Caching.IndividualEntrepreneurs;

public sealed class CachedRemoveIndividualEntrepreneurHandler
    : IRequestHandler<RemoveIndividualEntrepreneurCommand>
{
    private readonly RemoveIndividualEntrepreneurCommandHandler _inner;
    private readonly IDistributedCache _cache;

    public CachedRemoveIndividualEntrepreneurHandler(
        RemoveIndividualEntrepreneurCommandHandler inner,
        IDistributedCache cache
    )
    {
        _inner = inner;
        _cache = cache;
    }

    public async Task Handle(
        RemoveIndividualEntrepreneurCommand request,
        CancellationToken cancellationToken
    )
    {
        await _cache.RemoveAsync(Constants.EmptyIndividualEntrepreneursFilter, cancellationToken);
        await _cache.RemoveAsync(Constants.EmptyLegalEntityFilter, cancellationToken);
        await _cache.RemoveAsync(Constants.IndividualEntrepreneurs + request.Id, cancellationToken);
        await _inner.Handle(request, cancellationToken);
    }
}
