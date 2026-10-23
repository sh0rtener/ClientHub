using AutoMapper;
using ClientHub.Domain;
using ClientHub.Domain.LegalEntities;
using MediatR;

namespace ClientHub.UseCases.LegalEntities.AddLegalEntity;

public sealed class AddLegalEntityCommand : IRequest<int>
{
    public required LegalEntityDto LegalEntity { get; init; }
}

public sealed class AddLegalEntityCommandHandler : IRequestHandler<AddLegalEntityCommand, int>
{
    private readonly ILegalEntityRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public AddLegalEntityCommandHandler(
        ILegalEntityRepository repository,
        IMapper mapper,
        IUnitOfWork unitOfWork
    )
    {
        _repository = repository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(
        AddLegalEntityCommand request,
        CancellationToken cancellationToken
    )
    {
        var legalEntity = _mapper.Map<LegalEntity>(request.LegalEntity);

        if (!await _repository.IsExist(legalEntity.Inn, cancellationToken))
            throw new InvalidDataException(
                $"ЮЛ по переданному ИНН {legalEntity.Inn.Value} уже существует"
            );

        try
        {
            var id = await _repository.Add(legalEntity, cancellationToken);
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
