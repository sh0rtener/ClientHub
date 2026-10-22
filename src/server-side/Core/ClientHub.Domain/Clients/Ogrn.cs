using System.Text.RegularExpressions;

namespace ClientHub.Domain.Clients;

public sealed class Ogrn : DefaultStringValueObject
{
    private const string _ogrnIpRegex = @"\A\d{15}\z";
    private const string _ogrnRegex = @"\A\d{13}\z";
    private ClientType _type;

    public Ogrn(string value, ClientType type)
        : base(value)
    {
        _type = type;
    }

    public override void Validate(string value)
    {
        string regex = _ogrnIpRegex;
        if (_type.Equals(ClientType.IndividualEntrepreneurs))
            regex = _ogrnIpRegex;
        
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidPropertyException(nameof(value));
        if (!Regex.IsMatch(value.Trim(), regex))
            throw new InvalidPropertyException(nameof(value), "Неверный формат ОГРН");
    }
}
