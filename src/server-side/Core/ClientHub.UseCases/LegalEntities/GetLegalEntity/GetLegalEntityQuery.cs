using AutoMapper;
using ClientHub.Domain.LegalEntities;
using MediatR;

namespace ClientHub.UseCases.LegalEntities.GetLegalEntity;

public sealed class GetLegalEntityQuery : IRequest<LegalEntityDto>
{
    public required int Id { get; init; }
}

public sealed class GetLegalEntityQueryHandler
    : IRequestHandler<GetLegalEntityQuery, LegalEntityDto>
{
    private readonly IMapper _mapper;
    private readonly ILegalEntityRepository _repository;

    public GetLegalEntityQueryHandler(IMapper mapper, ILegalEntityRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<LegalEntityDto> Handle(
        GetLegalEntityQuery request,
        CancellationToken cancellationToken
    )
    {
        var legalEntity = await _repository.Get(request.Id, cancellationToken);
        var result = _mapper.Map<LegalEntityDto>(legalEntity);

        return result;
    }
}
