using ClientHub.Domain;
using ClientHub.Domain.LegalEntities;
using MediatR;

namespace ClientHub.UseCases.LegalEntities.RemoveLegalEntity;

public sealed class RemoveLegalEntityCommand : IRequest
{
    public required int Id { get; init; }
}

public sealed class RemoveLegalEntityCommandHandler : IRequestHandler<RemoveLegalEntityCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILegalEntityRepository _repository;

    public RemoveLegalEntityCommandHandler(
        IUnitOfWork unitOfWork,
        ILegalEntityRepository repository
    )
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    public async Task Handle(RemoveLegalEntityCommand request, CancellationToken cancellationToken)
    {
        if (!await _repository.IsExist(request.Id, cancellationToken))
            throw new InvalidDataException(
                $"ЮЛ по переданному идентификатору {request.Id} не найдено"
            );

        await _unitOfWork.BeginTransactionAsync(cancellationToken: cancellationToken);

        try
        {
            await _repository.Remove(request.Id, cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw ex;
        }
    }
}
