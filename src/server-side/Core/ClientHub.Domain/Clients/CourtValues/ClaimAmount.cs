namespace ClientHub.Domain.Clients.CourtValues;

public sealed class ClaimAmount : ValueObject
{
    public decimal Value { get; }

    public ClaimAmount(decimal value)
    {
        Guard.MustGreaterThan(value);
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
