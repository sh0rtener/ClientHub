using AutoMapper;
using ClientHub.Domain.IndividualEntrepreneurs;
using MediatR;

namespace ClientHub.UseCases.IndividualEntrepreneurs.GetIndividualEntrepreneur;

public sealed class GetIndividualEntrepreneurQuery : IRequest<IndividualEntrepreneursDto?>
{
    public required int Id { get; init; }
}

public sealed class GetIndividualEntrepreneurQueryHandler
    : IRequestHandler<GetIndividualEntrepreneurQuery, IndividualEntrepreneursDto?>
{
    private readonly IIndividualEntrepreneurRepository _repository;
    private readonly IMapper _mapper;

    public GetIndividualEntrepreneurQueryHandler(
        IIndividualEntrepreneurRepository repository,
        IMapper mapper
    )
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IndividualEntrepreneursDto?> Handle(
        GetIndividualEntrepreneurQuery request,
        CancellationToken cancellationToken
    )
    {
        var individualEntrepreneur = await _repository.Get(request.Id, cancellationToken);
        var result = _mapper.Map<IndividualEntrepreneursDto?>(individualEntrepreneur);

        return result;
    }
}
