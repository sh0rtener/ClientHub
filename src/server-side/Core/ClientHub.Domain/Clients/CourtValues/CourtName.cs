namespace ClientHub.Domain.Clients.CourtValues;

public sealed class CourtName : DefaultStringValueObject
{
    private const int _maxCourtNameLength = 300;

    public CourtName(string value)
        : base(value, _maxCourtNameLength) { }
}
