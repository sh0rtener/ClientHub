namespace ClientHub.Domain.Clients.Enforcements;

public sealed class Bailiff : DefaultStringValueObject
{
    private const int _maxBailiffNameLength = 200;

    public Bailiff(string value)
        : base(value, _maxBailiffNameLength) { }
}
