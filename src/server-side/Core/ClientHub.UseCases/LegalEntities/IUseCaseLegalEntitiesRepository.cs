namespace ClientHub.UseCases.LegalEntities;

public sealed record GetShortLegalEntities(
    string? Inn,
    string? Ogrn,
    string? Region,
    int? Status,
    DateTime? RegistrationDate,
    DateTime? LiquidationDate,
    int Limit = 100,
    int Offset = 0
);

public interface IUseCaseLegalEntitiesRepository
{
    Task<IEnumerable<ShortLegalEntityDto>> Get(
        GetShortLegalEntities filter,
        CancellationToken cancellationToken = default
    );
}
