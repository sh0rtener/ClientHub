using System.Text.RegularExpressions;

namespace ClientHub.Domain.Clients;

public sealed class Ogrn : ValueObject
{
    private const string _ogrnIpRegex = @"\A\d{15}\z";
    private const string _ogrnRegex = @"\A\d{13}\z";
    private ClientType _type;

    public string Value { get; }

    public Ogrn(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        value = value.Trim();
        Guard.ValidateLength(value);
        Validate(value);
        Value = value;
    }

    public Ogrn(string value, int maxLength)
    {
        ArgumentNullException.ThrowIfNull(value);
        value = value.Trim();
        Guard.ValidateLength(value, maxLength);
        Validate(value);
        Value = value;
    }

    public void Validate(string value)
    {
        string regex = _ogrnRegex;
        if (_type.Equals(ClientType.IndividualEntrepreneurs))
            regex = _ogrnIpRegex;

        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidPropertyException(nameof(value));
        if (!Regex.IsMatch(value.Trim(), regex))
            throw new InvalidPropertyException(nameof(value), "Неверный формат ОГРН");
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
