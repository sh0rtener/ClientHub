using MediatR;

namespace ClientHub.UseCases.IndividualEntrepreneurs.GetAllIndividualEntrepreneurs;

public sealed class GetAllIndividualEntrepreneursQuery
    : IRequest<IEnumerable<ShortIndividualEntrepreneursDto>>
{
    public required GetShortIndividualEntrepreneurs Filter { get; init; }
}

public sealed class GetAllIndividualEntrepreneursQueryHandler
    : IRequestHandler<
        GetAllIndividualEntrepreneursQuery,
        IEnumerable<ShortIndividualEntrepreneursDto>
    >
{
    private readonly IUseCaseIndividualEntrepreneursRepository _repository;

    public GetAllIndividualEntrepreneursQueryHandler(   
        IUseCaseIndividualEntrepreneursRepository repository
    )
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ShortIndividualEntrepreneursDto>> Handle(
        GetAllIndividualEntrepreneursQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _repository.Get(request.Filter, cancellationToken);
    }
}
