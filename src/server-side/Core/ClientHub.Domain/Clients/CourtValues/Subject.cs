namespace ClientHub.Domain.Clients.CourtValues;

public sealed class Subject : DefaultStringValueObject
{
    private const int _maxSubjectLength = 2000;

    public Subject(string value)
        : base(value, _maxSubjectLength) { }
}
