using AutoMapper;
using ClientHub.Domain;
using ClientHub.Domain.IndividualEntrepreneurs;
using MediatR;

namespace ClientHub.UseCases.IndividualEntrepreneurs.AddIndividualEntrepreneur;

public sealed class AddIndividualEntrepreneurCommand : IRequest<int>
{
    public required IndividualEntrepreneursDto IPs { get; init; }
}

public sealed class AddIndividualEntrepreneurCommandHandler
    : IRequestHandler<AddIndividualEntrepreneurCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IIndividualEntrepreneurRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public AddIndividualEntrepreneurCommandHandler(
        IMapper mapper,
        IIndividualEntrepreneurRepository repository,
        IUnitOfWork unitOfWork
    )
    {
        _mapper = mapper;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(
        AddIndividualEntrepreneurCommand request,
        CancellationToken cancellationToken
    )
    {
        var individualEntrepreneur = _mapper.Map<IndividualEntrepreneur>(request.IPs);

        if (!await _repository.IsExist(individualEntrepreneur.Inn, cancellationToken))
            throw new InvalidDataException(
                $"ИП по переданному ИНН {individualEntrepreneur.Inn.Value} уже существует"
            );

        await _unitOfWork.BeginTransactionAsync(cancellationToken: cancellationToken);

        try
        {
            var id = await _repository.Add(individualEntrepreneur, cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            return id;
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}
