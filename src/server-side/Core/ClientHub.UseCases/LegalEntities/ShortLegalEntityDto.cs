namespace ClientHub.UseCases.LegalEntities;

public sealed class ShortLegalEntityDto
{
    public int Id { get; set; }
    public string Inn { get; set; } = string.Empty;
    public string Ogrn { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int ClientStatus { get; set; }
    public DateTime? RegistrationDate { get; set; }
    public DateTime? LiquidationDate { get; set; }
}
