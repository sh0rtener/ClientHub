namespace ClientHub.Domain.Clients.Enforcements;

public sealed class FsspDepartament : DefaultStringValueObject
{
    private const int _maxDepartmentNameLength = 300;

    public FsspDepartament(string value)
        : base(value, _maxDepartmentNameLength) { }
}
