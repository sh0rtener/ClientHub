using ClientHub.Domain;
using ClientHub.Domain.IndividualEntrepreneurs;
using MediatR;

namespace ClientHub.UseCases.IndividualEntrepreneurs.RemoveIndividualEntrepreneur;

public sealed class RemoveIndividualEntrepreneurCommand : IRequest
{
    public required int Id { get; init; }
}

public sealed class RemoveIndividualEntrepreneurCommandHandler
    : IRequestHandler<RemoveIndividualEntrepreneurCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIndividualEntrepreneurRepository _repository;

    public RemoveIndividualEntrepreneurCommandHandler(
        IUnitOfWork unitOfWork,
        IIndividualEntrepreneurRepository repository
    )
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    public async Task Handle(
        RemoveIndividualEntrepreneurCommand request,
        CancellationToken cancellationToken
    )
    {
        if (!await _repository.IsExist(request.Id, cancellationToken))
            throw new InvalidDataException(
                $"ИП по переданному идентификатору {request.Id} не найдено"
            );

        await _unitOfWork.BeginTransactionAsync(cancellationToken: cancellationToken);

        try
        {
            await _repository.Remove(request.Id, cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}
