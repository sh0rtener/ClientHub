using AutoMapper;
using ClientHub.Domain;
using ClientHub.Domain.IndividualEntrepreneurs;
using MediatR;

namespace ClientHub.UseCases.IndividualEntrepreneurs.UpdateIndividualEntrepreneur;

public sealed class UpdateIndividualEntrepreneurCommand : IRequest<IndividualEntrepreneursDto>
{
    public required int Id { get; init; }
    public required IndividualEntrepreneursDto UpdatedModel { get; init; }
}

public sealed class UpdateIndividualEntrepreneurCommandHandler
    : IRequestHandler<UpdateIndividualEntrepreneurCommand, IndividualEntrepreneursDto>
{
    private readonly IIndividualEntrepreneurRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateIndividualEntrepreneurCommandHandler(
        IIndividualEntrepreneurRepository repository,
        IMapper mapper,
        IUnitOfWork unitOfWork
    )
    {
        _repository = repository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IndividualEntrepreneursDto> Handle(
        UpdateIndividualEntrepreneurCommand request,
        CancellationToken cancellationToken
    )
    {
        var individualEntrepreneur = await _repository.Get(request.Id, cancellationToken);
        if (individualEntrepreneur == null)
            throw new InvalidDataException(
                $"ИП по переданному идентификатору {request.Id} не найдено"
            );

        var updatedIndividualEntrepreneur = _mapper.Map<IndividualEntrepreneur>(
            request.UpdatedModel
        );

        await _unitOfWork.BeginTransactionAsync(cancellationToken: cancellationToken);

        try
        {
            var result = await _repository.Update(
                request.Id,
                updatedIndividualEntrepreneur,
                cancellationToken
            );
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            return _mapper.Map<IndividualEntrepreneursDto>(result);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}
