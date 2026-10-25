using ClientHub.UseCases.IndividualEntrepreneurs;
using ClientHub.UseCases.IndividualEntrepreneurs.UpdateIndividualEntrepreneur;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace ClientHub.Caching.IndividualEntrepreneurs;

public sealed class CachedUpdateIndividualEntrepreneursHandler
    : IRequestHandler<UpdateIndividualEntrepreneurCommand, IndividualEntrepreneursDto>
{
    private readonly UpdateIndividualEntrepreneurCommandHandler _inner;
    private readonly IDistributedCache _cache;

    public CachedUpdateIndividualEntrepreneursHandler(
        UpdateIndividualEntrepreneurCommandHandler inner,
        IDistributedCache cache
    )
    {
        _inner = inner;
        _cache = cache;
    }

    public async Task<IndividualEntrepreneursDto> Handle(
        UpdateIndividualEntrepreneurCommand request,
        CancellationToken cancellationToken
    )
    {
        await _cache.RemoveAsync(Constants.EmptyIndividualEntrepreneursFilter, cancellationToken);
        await _cache.RemoveAsync(Constants.EmptyLegalEntityFilter, cancellationToken);
        await _cache.RemoveAsync(Constants.IndividualEntrepreneurs + request.Id, cancellationToken);
        return await _inner.Handle(request, cancellationToken);
    }
}
