using MediatR;

namespace ClientHub.UseCases.LegalEntities.GetAllLegalEntities;

public sealed class GetAllLegalEntitiesQuery : IRequest<IEnumerable<ShortLegalEntityDto>>
{
    public required GetShortLegalEntities Filter { get; init; }
}

public sealed class GetAllLegalEntitiesQueryHandler
    : IRequestHandler<GetAllLegalEntitiesQuery, IEnumerable<ShortLegalEntityDto>>
{
    private readonly IUseCaseLegalEntitiesRepository _repository;

    public GetAllLegalEntitiesQueryHandler(IUseCaseLegalEntitiesRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ShortLegalEntityDto>> Handle(
        GetAllLegalEntitiesQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _repository.Get(request.Filter, cancellationToken);
    }
}
