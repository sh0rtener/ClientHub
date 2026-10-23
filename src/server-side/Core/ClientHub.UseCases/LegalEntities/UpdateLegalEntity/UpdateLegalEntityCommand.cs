using AutoMapper;
using ClientHub.Domain;
using ClientHub.Domain.LegalEntities;
using MediatR;

namespace ClientHub.UseCases.LegalEntities.UpdateLegalEntity;

public sealed class UpdateLegalEntityCommand : IRequest<LegalEntityDto>
{
    public required int Id { get; init; }
    public required LegalEntityDto LegalEntity { get; init; }
}

public sealed class UpdateLegalEntityCommandHandler
    : IRequestHandler<UpdateLegalEntityCommand, LegalEntityDto>
{
    private readonly ILegalEntityRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateLegalEntityCommandHandler(
        ILegalEntityRepository repository,
        IMapper mapper,
        IUnitOfWork unitOfWork
    )
    {
        _repository = repository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<LegalEntityDto> Handle(
        UpdateLegalEntityCommand request,
        CancellationToken cancellationToken
    )
    {
        var legalEntity = await _repository.Get(request.Id, cancellationToken);
        if (legalEntity == null)
            throw new InvalidDataException(
                $"ЮЛ по переданному идентификатору {request.Id} не найдено"
            );

        var updatedLegalEntity = _mapper.Map<LegalEntity>(request.LegalEntity);

        await _unitOfWork.BeginTransactionAsync(cancellationToken: cancellationToken);

        try
        {
            var result = await _repository.Update(
                request.Id,
                updatedLegalEntity,
                cancellationToken
            );
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            return _mapper.Map<LegalEntityDto>(result);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}
