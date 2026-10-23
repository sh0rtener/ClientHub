namespace ClientHub.UseCases.IndividualEntrepreneurs;

public record GetShortIndividualEntrepreneurs(
    string? Inn,
    string? Ogrn,
    string? Region,
    int? Status,
    DateTime? RegistrationDate,
    DateTime? LiquidationDate,
    int Limit = 100,
    int Offset = 0
);

public interface IUseCaseIndividualEntrepreneursRepository
{
    Task<IEnumerable<ShortIndividualEntrepreneursDto>> Get(
        GetShortIndividualEntrepreneurs filter,
        CancellationToken cancellationToken = default
    );
}
