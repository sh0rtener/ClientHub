namespace ClientHub.Domain.Clients.CourtValues;

public sealed class CountrAgentName : DefaultStringValueObject
{
    private const int _maxCounterpartyNameLength = 500;

    public CountrAgentName(string value) : base(value, _maxCounterpartyNameLength)
    {
    }
}
